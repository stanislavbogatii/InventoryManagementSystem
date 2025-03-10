using InventoryManagement.Core.Entities.InventoryManagement.Core.Reports;
using InventoryManagement.Core.Entities;


namespace InventoryManagement.Application.Factories
{
        public abstract class ReportFactory
        {
            public abstract Report CreateInventoryReport(List<Product> products);
            public abstract Report CreateOrderReport(List<Order> orders);

            public abstract IReportFormatter CreateFormatter();
        }

        public class PdfReportFactory : ReportFactory
        {

            public override Report CreateInventoryReport(List<Product> products)
            {
                return new InventoryPdfReport(products);
            }

            public override Report CreateOrderReport(List<Order> orders)
            {
                return new OrderPdfReport(orders);
            }

            public override IReportFormatter CreateFormatter()
            {
                return new PdfFormatter();
            }
        }

        public class ExcelReportFactory : ReportFactory
        {
            public override Report CreateInventoryReport(List<Product> products)
            {
                return new InventoryExcelReport(products);
            }

            public override Report CreateOrderReport(List<Order> orders)
            {
                return new OrderExcelReport(orders);
            }

        public override IReportFormatter CreateFormatter()
            {
                return new ExcelFormatter();
            }
        }
}
