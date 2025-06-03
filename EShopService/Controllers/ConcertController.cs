using EShop.Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EShop.Application;

namespace EShopService.Controllers
{
    [ApiController]
    [Route("concerts")]
    public class ConcertController : ControllerBase
    {
        private readonly IConcertService _concertService;

        public ConcertController(IConcertService concertService)
        {
            _concertService = concertService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var concerts = await _concertService.GetAllConcertsAsync();
            return Ok(concerts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var concert = await _concertService.GetConcertByIdAsync(id);
            return concert == null ? NotFound() : Ok(concert);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? artist, [FromQuery] string? city)
        {
            var results = await _concertService.SearchConcertsAsync(artist, city);
            return Ok(results);
        }
    }

}
