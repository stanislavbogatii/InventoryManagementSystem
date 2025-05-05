using InventoryManagement.Core.Abstract;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class AuditRecordCollection
    {
        private readonly List<AuditRecord> _records = new();

        public void Add(AuditRecord record) => _records.Add(record);

        public IAuditIterator CreateIterator() => new StandardAuditIterator(_records);
        public IAuditIterator CreateYearIterator(int year) => new YearAuditIterator(_records, year);
        public IAuditIterator CreateReverseIterator() => new ReverseAuditIterator(_records);
    }

}
