using InventoryManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers
{
    [ApiController]
    [Route("api/audits")]
    public class AuditController : ControllerBase
    {
        private readonly AuditService _service;

        public AuditController(AuditService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("year/{year}")]
        public IActionResult GetByYear(int year) => Ok(_service.GetByYear(year));

        [HttpGet("chronological")]
        public IActionResult GetInChronologicalOrder() => Ok(_service.GetInChronologicalOrder());
    }
}
