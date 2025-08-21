using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;

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
    }
}