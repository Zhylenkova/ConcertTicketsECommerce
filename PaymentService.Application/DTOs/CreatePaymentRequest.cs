using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs
{
    public class CreatePaymentRequest
    {
        public int TicketId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
    }
}
