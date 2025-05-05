using InventoryManagement.Core.Abstract;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class StandardAuditIterator : IAuditIterator
    {
        private readonly List<AuditRecord> _records;
        private int _position = 0;

        public StandardAuditIterator(List<AuditRecord> records)
        {
            _records = records.OrderByDescending(r => r.Date).ToList();
        }

        public bool HasNext() => _position < _records.Count;

        public AuditRecord? Next() => HasNext() ? _records[_position++] : null;
    }

    public class YearAuditIterator : IAuditIterator
    {
        private readonly List<AuditRecord> _records;
        private int _position = 0;

        public YearAuditIterator(List<AuditRecord> records, int year)
        {
            _records = records
                .Where(r => r.Date.Year == year)
                .OrderByDescending(r => r.Date)
                .ToList();
        }

        public bool HasNext() => _position < _records.Count;

        public AuditRecord? Next() => HasNext() ? _records[_position++] : null;
    }

    public class ReverseAuditIterator : IAuditIterator
    {
        private readonly List<AuditRecord> _records;
        private int _position = 0;

        public ReverseAuditIterator(List<AuditRecord> records)
        {
            _records = records.OrderBy(r => r.Date).ToList();
        }

        public bool HasNext() => _position < _records.Count;

        public AuditRecord? Next() => HasNext() ? _records[_position++] : null;
    }
}
