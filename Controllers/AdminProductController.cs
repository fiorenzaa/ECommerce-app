using Microsoft.AspNetCore.Mvc;
using ECommerce.Services.Interfaces;
using ECommerce.Models;

namespace ECommerce.Controllers
{
  public class AdminProductController : Controller
  {
    private readonly IAdminProductService _productService;

    public AdminProductController(IAdminProductService productService)
    {
      _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
      await _productService.SyncProductsFromApiAsync();
      var products = await _productService.GetProductsAsync();
      return View(products);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
      if (!ModelState.IsValid) return View(product);

      await _productService.AddAsync(product);
      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
      var product = await _productService.GetByIdAsync(id);
      if (product == null) return NotFound();

      return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
      if (!ModelState.IsValid) return View(product);

      await _productService.UpdateAsync(product);
      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
      var product = await _productService.GetByIdAsync(id);
      if (product == null) return NotFound();

      return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _productService.DeleteAsync(id);
      return RedirectToAction(nameof(Index));
    }

  }
}
