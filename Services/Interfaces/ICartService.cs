using ECommerce.Models;

namespace ECommerce.Services.Interfaces
{
  public interface ICartService
  {
    Task<Cart> GetCartAsync();
    Task AddToCartAsync(int productId);
    Task RemoveAsync(int itemId);
    Task IncreaseQtyAsync(int itemId);
    Task DecreaseQtyAsync(int itemId);
  }
}
