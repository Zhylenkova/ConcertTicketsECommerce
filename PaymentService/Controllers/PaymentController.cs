using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTOs;
using PaymentService.Application.Interfaces;
using System.Security.Claims;
namespace PaymentService.Controllers
{

        [ApiController]
        [Route("payments")]
        public class PaymentController : ControllerBase
        {
            private readonly IPaymentService _paymentService;

            public PaymentController(IPaymentService paymentService)
            {
                _paymentService = paymentService;
            }

            [HttpPost]
            [Authorize]
            public async Task<IActionResult> Pay([FromBody] CreatePaymentRequest request)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var result = await _paymentService.ProcessPaymentAsync(request, userId);
                return Ok(result);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var payment = await _paymentService.GetPaymentByIdAsync(id);
                return payment is null ? NotFound() : Ok(payment);
            }

            [HttpGet("user/{userId}")]
            public async Task<IActionResult> GetByUser(int userId)
            {
                var payments = await _paymentService.GetPaymentsByUserAsync(userId);
                return Ok(payments);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var success = await _paymentService.DeletePaymentAsync(id);
                return success ? NoContent() : NotFound();
            }
        }
    }
