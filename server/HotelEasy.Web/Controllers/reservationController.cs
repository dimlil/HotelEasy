using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;
using HotelEasy.Services.DTO;

namespace HotelEasy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class reservationController : Controller
    {
        private readonly ReservationServices _reservationService;

        public reservationController(ReservationServices reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllReservation()
        {
            var result = await _reservationService.GetAllReservationAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation(int id)
        {
            var result = await _reservationService.GetReservationByIdAsync(id);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            if (result.Data == null)
            {
                return NotFound("Reservation not found.");
            }

            return Ok(result.Data);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDTO dto)
        {
            var result = await _reservationService.CreateReservationAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] CreateReservationDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Reservation data is required.");
            }

            var result = await _reservationService.UpdateReservationAsync(id, dto);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _reservationService.DeleteReservationAsync(id);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}