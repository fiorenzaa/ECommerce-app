using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task SyncProductsFromApiAsync();
    }
}
