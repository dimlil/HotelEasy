using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;
using HotelEasy.Services.DTO;

namespace HotelEasy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class hotelsController : Controller
    {
        private readonly HotelsServices _hotelsService;

        public hotelsController(HotelsServices hotelsService)
        {
            _hotelsService = hotelsService;
        }

        [HttpGet("getAllHotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            var result = await _hotelsService.GetAllHotelsAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

      
    }

}