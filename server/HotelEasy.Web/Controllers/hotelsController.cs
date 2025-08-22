using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;
using HotelEasy.Services.DTO;

namespace HotelEasy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class hotelsController : Controller
    {
        private readonly HotelsServices _hotelsService;

        public hotelsController(HotelsServices hotelsService)
        {
            _hotelsService = hotelsService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllHotels()
        {
            var result = await _hotelsService.GetAllHotelsAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(int id)
        {
            var result = await _hotelsService.GetHotelByIdAsync(id);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            if (result.Data == null)
            {
                return NotFound("Hotel not found.");
            }

            return Ok(result.Data);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO dto)
        {
            var result = await _hotelsService.CreateHotelAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Hotel data is required.");
            }

            var result = await _hotelsService.UpdateHitelAsync(id, dto);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _hotelsService.DeleteHotelAsync(id);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }

}