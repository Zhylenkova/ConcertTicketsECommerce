using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Clients
{
    public class NotificationServiceClient
    {
        private readonly HttpClient _httpClient;

        public NotificationServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendPaymentSuccess(int userId, int ticketId)
        {
            var notification = new { userId, ticketId, message = "Payment successful." };
            await _httpClient.PostAsJsonAsync("/notifications/payment", notification);
        }
    }
}
