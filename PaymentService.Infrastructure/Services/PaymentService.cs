using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentService.Application.DTOs;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.Models;
using PaymentService.Infrastructure.Clients;
using PaymentService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace PaymentService.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentDbContext _context;
        private readonly TicketServiceClient _ticketClient;
        private readonly NotificationServiceClient _notificationClient;

        public PaymentService(PaymentDbContext context, TicketServiceClient ticketClient, NotificationServiceClient notificationClient)
        {
            _context = context;
            _ticketClient = ticketClient;
            _notificationClient = notificationClient;
        }

        public async Task<Payment> ProcessPaymentAsync(CreatePaymentRequest request, int userId)
        {
            var success = request.Method == "TestCard" || request.Method == "PayPal";

            var payment = new Payment
            {
                TicketId = request.TicketId,
                UserId = userId,
                Amount = request.Amount,
                Method = request.Method,
                Status = success ? "Success" : "Failed",
                Timestamp = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            if (success)
            {
                await _ticketClient.UpdateTicketStatus(request.TicketId, "Paid");
                await _notificationClient.SendPaymentSuccess(userId, request.TicketId);
            }

            return payment;
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id) =>
            await _context.Payments.FindAsync(id);

        public async Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId) =>
            await _context.Payments.Where(p => p.UserId == userId).ToListAsync();

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
