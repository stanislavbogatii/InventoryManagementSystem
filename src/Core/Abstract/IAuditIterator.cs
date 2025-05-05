using InventoryManagement.Core.Entities;

namespace InventoryManagement.Core.Abstract
{
    public interface IAuditIterator
    {
        bool HasNext();
        AuditRecord? Next();
    }

}
