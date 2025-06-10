using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Commands;
using ShoppingCart.Domain.Queries;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {

        private readonly IMediator _mediator;
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpPost("remove-product")]
        public async Task<IActionResult> RemoveProductFromCart([FromBody] RemoveProductFromCartCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(int cartId)
        {
            var query = new GetCartQuery { CartId = cartId };
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var query = new GetAllCartsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
