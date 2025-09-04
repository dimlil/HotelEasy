using Microsoft.AspNetCore.Mvc;
using HotelEasy.Services;

namespace HotelEasy.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class logsController : Controller
    {
        private readonly LogServices _logsService;

        public logsController(LogServices logsService)
        {
            _logsService = logsService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllLogs()
        {
            var result = await _logsService.GetAllLogs();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportLogs()
        {
            var csvBytes = await _logsService.ExportLogsToCsvAsync();

            return File(
                csvBytes,
                "text/csv",
                "logs.csv"
            );
        }
    }
}