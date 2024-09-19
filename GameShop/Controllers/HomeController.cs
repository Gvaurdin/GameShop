using GameShop.Exstensions;
using GameShop.Repository;
using GameShop.Repository.Interfaces;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories;
using GameShopModel.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers
{
    public class HomeController(IHttpContextAccessor httpContextAccessor,
        GameShopContext gameShopContext,
        IGameProductRepository gameProductRepository,
        IRepositoryCart repositoryCart,
        IGameStatusService gameStatusService) : Controller
    {
        private const int countPopularGame = 100;
        public async Task<IActionResult> Index(string gameGenre,string titleSearchString)
       {
            //var gameProducts = await gameProductRepository.GetAllGameProductsAsync();
            var gameProductGenre = from g in gameShopContext.Genres
                               select g;
            var gameProducts = from g in gameShopContext.GameProducts
                               select g;
            if(!string.IsNullOrEmpty(titleSearchString))
            {
                gameProducts = gameProducts.Where(gp => gp.Title.ToUpper().Contains(titleSearchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(gameGenre))
            {
                gameProducts = gameProducts
                    .Include(gp => gp.Genres)
                    .Where(gp => 
                          (gp.Genres.Where(genre =>
                                           genre.Title.Contains(gameGenre))).Any());
            }
            var idUser = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var gameStatuses = await gameStatusService.GetGameStatusesAsync([.. gameProducts], idUser!);
            var viewmodel = new GameIndexViewModel
            {
                Games = await gameProducts.ToListAsync(),
                GameStatuses = gameStatuses,
                GameGenres = new SelectList(await gameProductGenre.Select(genre => genre.Title).ToListAsync())
            };
            return View(viewmodel);
        }

        //public async Task<ActionResult> Index(string searchString)
        //{
        //    var gameProducts = await gameProductRepository.GetAllGameProductsAsync();
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        gameProducts = gameProducts.Where(gameProduct => gameProduct.Title.ToUpper().Contains(searchString.ToUpper())).ToList();
        //    }
        //    var idUser = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var gameStatuses = await gameStatusService.GetGameStatusesAsync(gameProducts, idUser!);

        //    var viewmodel = new GameIndexViewModel
        //    {
        //        Games = gameProducts,
        //        GameStatuses = gameStatuses
        //    };
        //    return View(viewmodel);
        //}

        public async Task<IActionResult> PopularGames()
        {
            var currentDate = DateTime.Now;
            var monthAgo = currentDate.AddMonths(-1);
            var gamesCarts = await gameShopContext.Carts
                .Include(cart => cart.GameProducts)
                .Between(cart => cart.DatePurchase, monthAgo, currentDate)
                .Take(countPopularGame).ToListAsync();
            var games = gamesCarts.SelectMany(cart => cart.GameProducts).Distinct().ToList();
            var idUser = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var gameStatuses = await gameStatusService.GetGameStatusesAsync(games, idUser!);

            var viewmodel = new GameIndexViewModel
            {
                Games = games,
                GameStatuses = gameStatuses
            };

            return View(viewmodel);
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

    }
}
