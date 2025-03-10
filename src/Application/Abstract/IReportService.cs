using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Abstract
{
    public interface IReportService
    {
        Task<string> GenerateInventoryReportAsync(string format);
        Task<string> GenerateOrderReportAsync(string format);

    }
}
