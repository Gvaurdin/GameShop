using GameShopModel.Data;
using GameShopModel.Repositories;
using GameShopModel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameShop
{
    public class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IGameProductRepository, GameProductRepository>();
            services.AddDbContext<GameShopContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("GameShopContext") ??
    throw new InvalidOperationException("Connection string 'GameShopContext' not found.")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: default,
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
