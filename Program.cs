using EShopApi.Cache;
using EShopApi.Data;
using EShopApi.Profiles;
using EShopApi.Repositories;
using EShopApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache(); 

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSingleton<ICacheService>(sp =>
{
    var config = builder.Configuration["Cache:Type"] ?? "InMemory";
    return CacheFactory.CreateCache(config, sp);
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 
}

app.Run();
