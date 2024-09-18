using GameShop.Repository;
using GameShop.Repository.Interfaces;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Repositories;
using GameShopModel.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers
{
    public class HomeController(IHttpContextAccessor httpContextAccessor,GameShopContext gameShopContext,IGameProductRepository gameProductRepository, IRepositoryCart repositoryCart) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var gameProducts = await gameProductRepository.GetAllGameProductsAsync();
            var idUser = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cartProducts = repositoryCart.GetProducts();
            var libraryGames = await gameShopContext.Carts
                .AsNoTracking()
                .Include(cart => cart.GameProducts)
                .Where(cart => cart.User.Id == idUser)
                .SelectMany(cart => cart.GameProducts)
                .ToListAsync();
            var gameStatuses = gameProducts.ToDictionary(game => game.Id, game => new GameStatusViewModel
            {
                IsInCart = cartProducts.Any(p => p.Id == game.Id),
                IsInLibrary = libraryGames.Any(p => p.Id == game.Id)
            });

            var viewmodel = new GameIndexViewModel
            {
                Games = gameProducts,
                GameStatuses = gameStatuses
            };
            return View(viewmodel);
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
            var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           var wishList = await gameShopContext.WishLists
                .Include(wishList => wishList.User)
                .Include(wishList => wishList.GameProduct)
                .Where(wishList => wishList.User.Id == idUser).ToListAsync();
            return View(wishList);
        }

        public async Task<IActionResult> DeleteWishList(int id)
        {
            var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await gameShopContext.Users.FirstAsync(user => user.Id == idUser);
            var gameProduct = await gameProductRepository.GetGameProductAsync(id);

            var wishList = await gameShopContext.WishLists
                .Include(wishlist => wishlist.User)
                .Include(wishlist => wishlist.GameProduct)
                .Where(wishlist => wishlist.GameProduct.Id == id && wishlist.User.Id == idUser)
                .ExecuteDeleteAsync();

            await gameShopContext.SaveChangesAsync();

            return RedirectToAction("WishList", "Home");
        }
    }
}
