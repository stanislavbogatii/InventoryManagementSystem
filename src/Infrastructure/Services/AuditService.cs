using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Repositories;

namespace InventoryManagement.Infrastructure.Services
{
    public class AuditService
    {
        private readonly AuditRecordCollection _collection;

        public AuditService(AuditRecordCollection collection)
        {
            _collection = collection;
        }

        public IEnumerable<AuditRecord> GetAll()
        {
            var iterator = _collection.CreateIterator();
            while (iterator.HasNext())
            {
                yield return iterator.Next();
            }
        }

        public IEnumerable<AuditRecord> GetByYear(int year)
        {
            var iterator = _collection.CreateYearIterator(year);
            while (iterator.HasNext())
            {
                yield return iterator.Next();
            }
        }

        public IEnumerable<AuditRecord> GetInChronologicalOrder()
        {
            var iterator = _collection.CreateReverseIterator();
            while (iterator.HasNext())
            {
                yield return iterator.Next();
            }
        }
    }
}
