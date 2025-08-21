using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;
using HotelEasy.Services.DTO;

namespace HotelEasy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class roomsController : Controller
    {
        private readonly RoomsServices _roomsService;

        public roomsController(RoomsServices roomsService)
        {
            _roomsService = roomsService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllRooms()
        {
            var result = await _roomsService.GetAllRoomsAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var result = await _roomsService.GetRoomByIdAsync(id);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            if (result.Data == null)
            {
                return NotFound("Room not found.");
            }

            return Ok(result.Data);
        }

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] CreateRoomDTO dto)
        {
            var result = await _roomsService.CreateRoomAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}