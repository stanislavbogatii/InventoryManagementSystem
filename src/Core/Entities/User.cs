using InventoryManagement.Core.Enums;

namespace InventoryManagement.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }
    }
}
