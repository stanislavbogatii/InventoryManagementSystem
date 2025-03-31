using InventoryManagement.Application.Components;

namespace InventoryManagement.Application.Abstract
{
    public interface ICategoryComposite
    {
        public CategoryComponent Composite(int rootCategoryId);
    }
}
