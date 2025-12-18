using System.Text.Json;
using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ECommerce.DTO;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public ProductService(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task SyncProductsFromApiAsync()
        {
            if (await _context.Product.AsNoTracking().AnyAsync())
                return;

            var response = await _httpClient.GetAsync("https://fakestoreapi.com/products");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var apiProducts = JsonSerializer.Deserialize<List<ProductDTO>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (apiProducts == null) return;

            var products = apiProducts.Select(p => new Product
            {
                Title = p.Title,
                Price = p.Price,
                Category = p.Category,
                Image = p.Image,
                Description = p.Description
            }).ToList();

            _context.Product.AddRange(products);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Product.ToListAsync();
        }
    }
}
