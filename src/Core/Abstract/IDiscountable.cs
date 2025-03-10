using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Abstract
{
    public interface IDiscountable
    {
        decimal CalculateDiscount();
    }
}
