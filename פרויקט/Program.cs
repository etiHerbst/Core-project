using core.Interfaces;
using core.Middleware;
using core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// רישום שכבת השירות — Singleton כדי שהתפריט יישמר בין בקשות
builder.Services.AddSingleton<IRestaurantMenu, RestaurantMenuService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// חוסם בקשות כאשר ShopSettings:IsShopClosed == true
app.UseShopClosed();

app.UseAuthorization();

app.MapControllers();

app.Run();
