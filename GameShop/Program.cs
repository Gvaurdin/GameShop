//using GameShop;
//using GameShopModel.Data;
//using GameShopModel.Repositories;
//using GameShopModel.Repositories.Interfaces;
//using Microsoft.EntityFrameworkCore;
//public class Program
//{
//    public static void Main(string[] args)
//    {
//        CreateHostBuilder(args).Build().Run();
//    }

//    public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//        .ConfigureWebHostDefaults(webBuilder =>
//        {
//            webBuilder.UseStartup<Startup>();
//        });
//}
using GameShopModel.Data;
using GameShopModel.Repositories.Interfaces;
using GameShopModel.Repositories;
using Microsoft.EntityFrameworkCore;
using GameShop.Repository;
using GameShop.Repository.Interfaces;
using GameShopModel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using GameShop.Repository.Repositories.Interfaces;
using GameShop.Core;
using GameShop.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IGameProductRepository, GameProductRepository>();
builder.Services.AddSingleton<IRepositoryCart, RepositoryCart>();
builder.Services.AddTransient<IRepositoryWishList, RepositoryWishList>();
builder.Services.AddTransient<IGameStatusService,GameStatusService>();
builder.Services.AddTransient<IRepositoryRecommendedGameProduct, RepositoryRecommendedGameProduct>();
builder.Services.AddDbContext<GameShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameShopContext") ??
    throw new InvalidOperationException("Connection string 'GameShopContext' not found.")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<GameShopContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
var app = builder.Build();
await Seed.SeedUserAndRolesAsync(app);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();