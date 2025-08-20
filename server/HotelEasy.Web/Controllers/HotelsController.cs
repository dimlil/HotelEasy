using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;

namespace HotelEasy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly HotelsServices _hotelsService;

        public HotelsController(HotelsServices hotelsService)
        {
            _hotelsService = hotelsService;
        }

        [HttpGet("getAllHotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            var result = await _hotelsService.GetAllHotelsAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        // [HttpPost("login")]
        // public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        // {
        //     var result = await _authService.LoginAsync(dto);
        //     return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        // }
    }

}