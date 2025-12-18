using ECommerce.Data;
using ECommerce.Services;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??

throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connectionString)); // Gunakan UseSqlServer untuk SQL Server

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IProductService, ProductService>();

builder.Services.AddScoped<IAdminProductService, AdminProductService>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

// Pastikan database terbuat dan migrasi diterapkan saat startup (opsional, untuk pengembangan)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Menerapkan migrasi yang tertunda

        var productService = services.GetRequiredService<IProductService>();
        await productService.SyncProductsFromApiAsync(); // ⭐ WAJIB
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}
app.Run();
