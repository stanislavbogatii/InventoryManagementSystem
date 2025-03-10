using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Factories;


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

            public async Task<string> GenerateInventoryReportAsync(string format)
            {
                ReportFactory factory = format switch
                {
                    "PDF" => new PdfReportFactory(),
                    "Excel" => new ExcelReportFactory(),
                    _ => throw new ArgumentException("Unsupported report type or format")
                };

                var report = factory.CreateInventoryReport(await _productManagementService.GetAllProductsAsync());
                var formatter = factory.CreateFormatter();
                return formatter.Format(report);
            }

            public async Task<string> GenerateOrderReportAsync(string format)
            {
                ReportFactory factory = format switch
                {
                    "PDF" => new PdfReportFactory(),
                    "Excel" => new ExcelReportFactory(),
                    _ => throw new ArgumentException("Unsupported report type or format")
                };

                var report = factory.CreateOrderReport(await _orderService.GetAllOrdersAsync());
                var formatter = factory.CreateFormatter();
                return formatter.Format(report);
            }
        }
}
