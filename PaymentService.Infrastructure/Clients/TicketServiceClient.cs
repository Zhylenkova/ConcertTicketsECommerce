using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Clients
{
    public class TicketServiceClient
    {
        private readonly HttpClient _httpClient;

        public TicketServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UpdateTicketStatus(int ticketId, string status)
        {
            var content = new { ticketStatus = status };
            await _httpClient.PatchAsJsonAsync($"/tickets/{ticketId}/status", content);
        }
    }
}
