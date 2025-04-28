using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Abstract;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Security
{
    public class ProductAccessControlProxy : IProductRepository
    {
        private readonly IProductRepository _realRepository; private readonly User _currentUser; private readonly ILogger _logger;

        public ProductAccessControlProxy(IProductRepository realRepository, User currentUser, ILogger<ProductAccessControlProxy> logger)
        {
            _realRepository = realRepository ?? throw new ArgumentNullException(nameof(realRepository));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Product> AddAsync(Product product)
        {
            if (!_currentUser.Role.Equals(UserRole.Admin) && !_currentUser.Role.Equals(UserRole.Manager))
            {
                _logger.LogWarning("User {Username} (Role: {Role}) attempted to add product without permission.", _currentUser.Username, _currentUser.Role);
                throw new UnauthorizedAccessException("Only Admins or Managers can add products.");
            }

            _logger.LogInformation("User {Username} (Role: {Role}) is adding product {ProductName}.", _currentUser.Username, _currentUser.Role, product.Name);
            return await _realRepository.AddAsync(product);
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            if (!_currentUser.Role.Equals(UserRole.Admin) && !_currentUser.Role.Equals(UserRole.Manager) && !_currentUser.Role.Equals(UserRole.Customer))
            {
                _logger.LogWarning("User {Username} (Role: {Role}) attempted to get products by category without permission.", _currentUser.Username, _currentUser.Role);
                throw new UnauthorizedAccessException("Only Admins, Managers, or Users can view products by category.");
            }
            _logger.LogInformation("User {Username} (Role: {Role}) is retrieving products by category ID {CategoryId}.", _currentUser.Username, _currentUser.Role, categoryId);
            return await _realRepository.GetProductsByCategoryId(categoryId);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            _logger.LogInformation("User {Username} (Role: {Role}) is retrieving product with ID {ProductId}.", _currentUser.Username, _currentUser.Role, id);
            return await _realRepository.GetByIdAsync(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            _logger.LogInformation("User {Username} (Role: {Role}) is retrieving all products.", _currentUser.Username, _currentUser.Role);
            return await _realRepository.GetAllAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            if (!_currentUser.Role.Equals(UserRole.Admin) && !_currentUser.Role.Equals(UserRole.Manager))
            {
                _logger.LogWarning("User {Username} (Role: {Role}) attempted to update product without permission.", _currentUser.Username, _currentUser.Role);
                throw new UnauthorizedAccessException("Only Admins or Managers can update products.");
            }

            _logger.LogInformation("User {Username} (Role: {Role}) is updating product {ProductName}.", _currentUser.Username, _currentUser.Role, product.Name);
            await _realRepository.UpdateAsync(product);
        }

    }

}