using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;
using Microsoft.AspNetCore.Identity.Data;
using HotelEasy.Services.DTO;

namespace HotelEasy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class authController : Controller
    {
        private readonly AuthServices _authService;

        public authController(AuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCredentialsDTO dto)
        {
            var result = await _authService.RegisterUserAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }

}