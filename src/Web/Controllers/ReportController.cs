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
        public async Task<IActionResult> GenerateReport([FromQuery] string reportType, [FromQuery] string format)
        {
            var reportContent = await _reportService.GenerateReportAsync(reportType, format);
            return Ok(reportContent);
        }
    }
}
