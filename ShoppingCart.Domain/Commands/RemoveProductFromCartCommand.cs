using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ShoppingCart.Domain.Commands
{
    public class RemoveProductFromCartCommand : IRequest
    {
        public int CartId { get; set; }
        public int ConcertId { get; set; }
    }
}
