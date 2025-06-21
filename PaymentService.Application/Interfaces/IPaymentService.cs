using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentService.Domain.Models;
using PaymentService.Application.DTOs;
namespace PaymentService.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> ProcessPaymentAsync(CreatePaymentRequest request, int userId);
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId);
        Task<bool> DeletePaymentAsync(int id);
    }
}
