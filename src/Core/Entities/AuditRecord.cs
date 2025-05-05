namespace InventoryManagement.Core.Entities
{
    public class AuditRecord
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime Date { get; private set; }
        public string PerformedBy { get; private set; }
        public string Notes { get; private set; }

        public AuditRecord(DateTime date, string performedBy, string notes)
        {
            Date = date;
            PerformedBy = performedBy;
            Notes = notes;
        }
    }
}
