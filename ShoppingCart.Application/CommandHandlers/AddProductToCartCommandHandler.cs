using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;
using MediatR;
using ShoppingCart.Domain.Commands;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Application.CommandHandlers
{
    public class AddProductToCartCommandHandler : IRequestHandler<AddProductToCartCommand>
    {
        private readonly ICartAdder _cartAdder;

        public AddProductToCartCommandHandler(ICartAdder cartAdder)
        {
            _cartAdder = cartAdder;
        }

        public Task Handle(AddProductToCartCommand command, CancellationToken cancellationToken)
        {
            var product = new Concert
            {
                concert_id = command.ConcertId
            };
            _cartAdder.AddProductToCart(command.CartId, product);
            return Task.CompletedTask;
        }
    }
}
