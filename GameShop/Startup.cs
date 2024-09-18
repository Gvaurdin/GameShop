using GameShop.Repository;
using GameShop.Repository.Interfaces;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories;
using GameShopModel.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameShop
{
    public class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IGameProductRepository, GameProductRepository>();
            services.AddSingleton<IRepositoryCart, RepositoryCart>();
            services.AddDbContext<GameShopContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GameShopContext") ??
            throw new InvalidOperationException("Connection string 'GameShopContext' not found.")));
            services.AddIdentity<User,IdentityRole>()
                .AddEntityFrameworkStores<GameShopContext>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddHttpContextAccessor();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
