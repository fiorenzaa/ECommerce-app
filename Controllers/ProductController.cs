using Microsoft.AspNetCore.Mvc;
using ECommerce.Services.Interfaces;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            await _productService.SyncProductsFromApiAsync();
            var products = await _productService.GetProductsAsync();
            return View(products);
        }
    }
}
