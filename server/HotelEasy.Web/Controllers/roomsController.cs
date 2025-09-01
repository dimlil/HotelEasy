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
        public async Task<IActionResult> CreateRoom([FromForm] CreateRoomDTO dto)
        {
            var result = await _roomsService.CreateRoomAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        
        [HttpPost("search")]
        public async Task<IActionResult> searchRoom([FromBody] SearchRoomsDTO dto)
        {
            var result = await _roomsService.SearchRoomsAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromForm] CreateRoomDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Room data is required.");
            }

            var result = await _roomsService.UpdateRoomAsync(id, dto);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _roomsService.DeleteRoomAsync(id);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}