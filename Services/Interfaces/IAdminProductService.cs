using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IAdminProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task SyncProductsFromApiAsync();
    }
}
