namespace InventoryManagement.Core.Entities
{
    public class Warehouse : IPrototype<Warehouse>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int? TotalCapacity { get; set; }

        public Warehouse(string code, string address, int? totalCapacity)
        {
            Code = code;
            Address = address;
            TotalCapacity = totalCapacity;
        }

        public Warehouse(string code, string address)
        {
            Code = code;
            Address = address;
        }

        public Warehouse Clone()
        {
            return new Warehouse(this.Code, this.Address, this.TotalCapacity);
        }
    }
}
