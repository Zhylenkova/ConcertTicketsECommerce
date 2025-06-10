using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using ShoppingCart.Infrastructure.Repositories;



namespace ShoppingCart.Application.Services
{

    public class CartService : ICartAdder, ICartRemover, ICartReader
    {
        private readonly ICartRepository _repository;

        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }

        public void AddProductToCart(int cartId, Concert product)
        {
            var cart = _repository.FindById(cartId) ?? new Cart { Id = cartId };
            cart.Concerts.Add(product);
            _repository.Add(cart);
        }

        public void RemoveProductFromCart(int cartId, int productId)
        {
            var cart = _repository.FindById(cartId);
            if (cart != null)
            {
                var product = cart.Concerts.FirstOrDefault(p => p.concert_id == productId);
                if (product != null)
                {
                    cart.Concerts.Remove(product);
                    _repository.Update(cart);
                }
            }
        }

        public Cart GetCart(int cartId)
        {
            var cart = _repository.FindById(cartId);
            if (cart == null) return null;

            return new Cart
            {
                Id = cart.Id,
                Concerts = cart.Concerts.Select(p => new Concert
                {
                    concert_id = p.concert_id
                }).ToList()
            };
        }

        public List<Cart> GetAllCarts()
        {
            return _repository.GetAll().Select(c => new Cart
            {
                Id = c.Id,
                Concerts = c.Concerts.Select(p => new Concert
                {
                    concert_id = p.concert_id
                }).ToList()
            }).ToList();
        }
    }
}
