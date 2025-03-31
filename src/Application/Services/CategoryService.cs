using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Components;
using InventoryManagement.Infrastructure.Abstract;

namespace InventoryManagement.Application.Services
{
    public class CategoryService : ICategoryComposite
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productsRepository;


        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productsRepository)
        {
            _categoryRepository = categoryRepository;
            _productsRepository = productsRepository;
        }

        public CategoryComponent Composite(int rootCategoryId)
        {
            var rootCategory = _categoryRepository.GetByIdAsync(rootCategoryId);

            if (rootCategory == null) throw new InvalidOperationException("Root category not found.");

            var warehouse = new CategoryComponent(rootCategory.Id);
            PopulateCategory(warehouse, rootCategory.Id);

            return warehouse;
        }

        private async void PopulateCategory(CategoryComponent categoryComponent, int categoryId)
        {
            var products = await _productsRepository.GetProductsByCategoryId(categoryId);

            foreach (var product in products)
            {
                categoryComponent.Add(new ProductComponent(product.Id, product.Price));
            }

            var subCategories = await _categoryRepository.GetByCategoryId(categoryId);
            foreach (var subCategory in subCategories)
            {
                var subCategoryComponent = new CategoryComponent(subCategory.Id);
                categoryComponent.Add(subCategoryComponent);
                PopulateCategory(subCategoryComponent, subCategory.Id);
            }
        }
    }
}
