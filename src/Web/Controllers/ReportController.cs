using InventoryManagement.Application.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateOrderReport([FromQuery] string format)
        {
            var reportContent = await _reportService.GenerateOrderReportAsync(format);
            return Ok(reportContent);
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateInventoryReport([FromQuery] string format)
        {
            var reportContent = await _reportService.GenerateInventoryReportAsync(format);
            return Ok(reportContent);
        }
    }
}
