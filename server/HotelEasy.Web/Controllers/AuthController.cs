using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;
using Microsoft.AspNetCore.Identity.Data;
using HotelEasy.Services.DTO;

namespace ViaPlan.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthServices _authService;

        public AuthController(AuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }

}