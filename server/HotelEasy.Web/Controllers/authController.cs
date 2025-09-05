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

        [HttpGet()]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _authService.GetAllUserAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _authService.GetUserByIdAsync(id);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            if (result.Data == null)
            {
                return NotFound("User not found.");
            }

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] EditUserDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("User data is required.");
            }

            var result = await _authService.UpdateUserAsync(id, dto);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _authService.DeleteUserAsync(id);

            if (!result.Success)
            {
                return StatusCode(500, result.ErrorMessage);
            }

            return Ok(result.Data);
        }

    }

}