using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services
{
  public class CartService : ICartService
  {
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
      _context = context;
    }

    public async Task<Cart> GetCartAsync()
    {
      var cart = await _context.Carts
          .Include(c => c.Items)
          .ThenInclude(i => i.Product)
          .FirstOrDefaultAsync();

      if (cart == null)
      {
        cart = new Cart();
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
      }

      return cart;
    }

    public async Task AddToCartAsync(int productId)
    {
      var cart = await GetCartAsync();
      var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

      if (item == null)
      {
        cart.Items.Add(new CartItem
        {
          ProductId = productId,
          Quantity = 1
        });
      }
      else
      {
        item.Quantity++;
      }

      await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int itemId)
    {
      var item = await _context.CartItems.FindAsync(itemId);
      if (item == null) return;

      _context.CartItems.Remove(item);
      await _context.SaveChangesAsync();
    }

    public async Task IncreaseQtyAsync(int itemId)
    {
      var item = await _context.CartItems.FindAsync(itemId);
      if (item == null) return;

      item.Quantity++;
      await _context.SaveChangesAsync();
    }

    public async Task DecreaseQtyAsync(int itemId)
    {
      var item = await _context.CartItems.FindAsync(itemId);
      if (item == null) return;

      item.Quantity--;

      if (item.Quantity <= 0)
        _context.CartItems.Remove(item);

      await _context.SaveChangesAsync();
    }

  }
}
