using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Factories.InventoryManagement.Application.Factories;


namespace InventoryManagement.Application.Services
{

        public class ReportService : IReportService
        {
            private readonly IProductManagementService _productManagementService;
            private readonly IOrderService _orderService;

            public ReportService(IProductManagementService productManagementService, IOrderService orderService)
            {
                _productManagementService = productManagementService;
                _orderService = orderService;
            }

            public async Task<string> GenerateReportAsync(string reportType, string format)
            {
                ReportFactory factory = (reportType, format) switch
                {
                    ("Inventory", "PDF") => new InventoryPdfReportFactory(await _productManagementService.GetAllProductsAsync()),
                    ("Order", "Excel") => new OrderExcelReportFactory(await _orderService.GetAllOrdersAsync()),
                    _ => throw new ArgumentException("Unsupported report type or format")
                };

                var report = factory.CreateReport();
                var formatter = factory.CreateFormatter();
                return formatter.Format(report);
            }
        }
}
