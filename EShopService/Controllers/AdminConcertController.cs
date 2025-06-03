using EShop.Application.Service;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopService.Controllers
{
    [ApiController]
    [Route("admin/concert")]
    //[Authorize(Roles = "Admin")] // ВЕРРНУТЬ ЭТО КОГДА БУДУТ ТОКЕНЫ!!!

    public class AdminConcertController : ControllerBase
    {
        private readonly IAdminConcertService _adminConcertService;

        public AdminConcertController(IAdminConcertService adminConcertService)
        {
            _adminConcertService = adminConcertService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Concert concert)
        {
            var result = await _adminConcertService.AddConcertAsync(concert);
            return Ok(new
            {
                message = $"Concert '{result.name}' was added successfully.",
                concert = result
            });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Concert concert)
        {
            var result = await _adminConcertService.UpdateConcertAsync(id, concert);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminConcertService.DeleteConcertAsync(id);
            return Ok(new { message = $"Concert with ID {id} was deleted successfully." });
        }
    }

}
