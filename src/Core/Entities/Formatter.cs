

using InventoryManagement.Core.Entities.InventoryManagement.Core.Reports;

namespace InventoryManagement.Core.Entities
{
        public interface IReportFormatter
        {
            string Format(Report report);
        }

        public class PdfFormatter : IReportFormatter
        {
            public string Format(Report report)
            {
                return $"PDF Format\nTitle: {report.Title}\nContent:\n";
            }
        }

        public class ExcelFormatter : IReportFormatter
        {
            public string Format(Report report)
            {
                return $"Excel Format\nTitle: {report.Title}\nContent:\n";
            }
        }
}
