using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryDbContext _context;

        public CategoryRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Category> UpdateAsync(Category Category)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetByCategoryId(int categoryId)
        {
            return await _context.Categories.Where(c => c.ParentCategoryId == categoryId).ToListAsync();
        }
    }
}
