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
        public async Task<IActionResult> GetAllRooms()
        {
            var result = await _reservationService.GetAllReservationAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
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

    }
}