using GameShop.Repository;
using GameShop.Repository.Interfaces;
using GameShopModel.Data;
using GameShopModel.Repositories.Interfaces;
using GameShopModel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Controllers
{
    public class GameController(GameShopContext gameShopContext, IGameProductRepository gameProductRepository, IRepositoryCart repositoryCart) : Controller
    {
        public async Task<IActionResult> Details(int id)
        {
            var gameProduct = await gameShopContext.GameProducts
                   .Include(gameProduct => gameProduct.Genres)
                   .Include(gameProduct => gameProduct.Images)
                   .Include(gameProduct => gameProduct.Videos)
                   .Include(gameProduct => gameProduct.MinimumSystemRequirement)
                   .Include(gameProduct => gameProduct.RecommendedSystemRequirement)
                   .FirstAsync(gameProduct => gameProduct.Id == id);
            return View(gameProduct);

        }

        public async Task<IActionResult> Add(int id)
        {
            var gameProduct = await gameProductRepository.GetGameProductAsync(id);
            repositoryCart.Add(gameProduct);

            return Redirect("~/Cart/Index");
        }
    }
}
