using Microsoft.AspNetCore.Mvc;
using ECommerce.Services.Interfaces;

namespace ECommerce.Controllers
{
  public class CartController : Controller
  {
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
      _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
      var cart = await _cartService.GetCartAsync();
      return View(cart);
    }

    public async Task<IActionResult> Add(int productId)
    {
      await _cartService.AddToCartAsync(productId);
      return RedirectToAction("Index", "Product");
    }

    public async Task<IActionResult> Remove(int itemId)
    {
      await _cartService.RemoveAsync(itemId);
      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Increase(int itemId)
    {
      await _cartService.IncreaseQtyAsync(itemId);
      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Decrease(int itemId)
    {
      await _cartService.DecreaseQtyAsync(itemId);
      return RedirectToAction(nameof(Index));
    }

  }
}
