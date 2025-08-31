using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;
using HotelEasy.Services.DTO;
using dotenv.net; 

namespace HotelEasy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class paymentsController : Controller
    {
        private readonly PaymentsServices _paymentService;

        public paymentsController(PaymentsServices paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] ReservationDTO dto)
        {
            DotEnv.Load();
            string successUrl = Environment.GetEnvironmentVariable("CLIENT_URL") + "/success";
            string cancelUrl = Environment.GetEnvironmentVariable("CLIENT_URL") + "/cancel";

            var result = await _paymentService.CreateCheckoutSessionAsync(dto.Room.Price, dto.Room.RoomNumber.ToString(), successUrl, cancelUrl);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(new { url = result.Data });
        }
    }

}