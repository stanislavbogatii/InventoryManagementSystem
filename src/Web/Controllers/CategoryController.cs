using InventoryManagement.Application.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryComposite _categoryComposite;

        public CategoryController(ICategoryComposite categoryComposite)
        {
            _categoryComposite = categoryComposite;
        }

        [HttpGet("/warehouse/{id}")]
        public ActionResult GetWarehouseByCategory(int id)
        {
            var warehouse = _categoryComposite.Composite(id);
            decimal total_price = warehouse.CalculateTotalCost();
            return Ok(total_price);
        }

    }
}
