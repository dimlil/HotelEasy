using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;

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
    }
}