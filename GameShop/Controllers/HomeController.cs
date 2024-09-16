using GameShopModel.Data;
using GameShopModel.Repositories;
using GameShopModel.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Controllers
{
    public class HomeController(GameShopContext gameShopContext,IGameProductRepository gameProductRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var gameProducts = await gameProductRepository.GetAllGameProductsAsync();
            return View(gameProducts);
        }

        public IActionResult PopularGames()
        {
            return View();
        }

        public IActionResult RecommendationGames()
        {
            return View();
        }

        public async Task<IActionResult> WishList()
        {
           var wishList = await gameShopContext.WishLists
                .Include(wishList => wishList.User)
                .Include(wishList => wishList.GameProduct)
                .Where(wishList => wishList.User.Id == "8b8f505f-91ff-4ac0-abf3-b980e62d4b4d").ToListAsync();
            return View(wishList);
        }
    }
}
