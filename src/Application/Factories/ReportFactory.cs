using InventoryManagement.Core.Entities.InventoryManagement.Core.Reports;
using InventoryManagement.Core.Entities;


namespace InventoryManagement.Application.Factories
{
    namespace InventoryManagement.Application.Factories
    {
        public abstract class ReportFactory
        {
            public abstract Report CreateReport();
            public abstract IReportFormatter CreateFormatter();
        }

        public class InventoryPdfReportFactory : ReportFactory
        {
            private readonly List<Product> _products;

            public InventoryPdfReportFactory(List<Product> products)
            {
                _products = products;
            }

            public override Report CreateReport()
            {
                return new InventoryReport(_products);
            }

            public override IReportFormatter CreateFormatter()
            {
                return new PdfFormatter();
            }
        }

        public class OrderExcelReportFactory : ReportFactory
        {
            private readonly List<Order> _orders;

            public OrderExcelReportFactory(List<Order> orders)
            {
                _orders = orders;
            }

            public override Report CreateReport()
            {
                return new OrderReport(_orders);
            }

            public override IReportFormatter CreateFormatter()
            {
                return new ExcelFormatter();
            }
        }
    }
}
