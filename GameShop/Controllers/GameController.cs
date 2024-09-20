using GameShop.Repository;
using GameShop.Repository.Interfaces;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interfaces;
using GameShopModel.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers
{
    public class GameController(IHttpContextAccessor httpContextAccessor,
        GameShopContext gameShopContext,
        IGameProductRepository gameProductRepository,
        IRepositoryCart repositoryCart,
        IRepositoryWishList repositoryWishList) : Controller
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

            var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // проверяем, находиться ли игра в списке желаемых
            var isInWishList = await gameShopContext.WishLists
                .AnyAsync(wl => wl.GameProduct.Id == id && wl.User.Id == idUser);

            // проверяем, находится ли игра в корзине
            var isInCart = repositoryCart.GetProducts().Any(p => p.Id == id);

            // проверяем, куплена ли игра и находится ли она в библиотеке
            var isInLibrary = await gameShopContext.Carts
                .Include(c => c.GameProducts)
                .AnyAsync(c => c.User.Id == idUser && c.GameProducts.Any(p => p.Id == id));

            if (isInLibrary && isInWishList)
            {
                var wishListEntry = await gameShopContext.WishLists
                    .FirstOrDefaultAsync(wl => wl.GameProduct.Id == id && wl.User.Id == idUser);

                if (wishListEntry != null)
                {
                    await repositoryWishList.DeleteAsync(wishListEntry.User.Id, wishListEntry.GameProduct.Id);
                }

                // Обновляем статус, что игра больше не в списке желаемого
                isInWishList = false;
            }
            var vmDetails = new GameDetailsViewModel
            {
                GameProduct = gameProduct,
                IsInWishlist = isInWishList,
                IsInCart = isInCart,
                IsInLibrary = isInLibrary
            };

            return View(vmDetails);

        }

        public async Task<IActionResult> Add(int id)
        {
            var gameProduct = await gameProductRepository.GetByIdAsync(id);
            repositoryCart.Add(gameProduct);

            return Redirect("~/Cart/Index");
        }

        public async Task<IActionResult> AddWishList(int id)
        {
            var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await gameShopContext.Users.FirstAsync(user => user.Id == idUser);
            var gameProduct = await gameProductRepository.GetByIdAsync(id);
            await repositoryWishList.AddAsync(user, gameProduct);
            return RedirectToAction("Details", "Game",new { id = id });
        }

        public async Task<IActionResult> DeleteWishList(int id)
        {
            var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await gameShopContext.Users.FirstAsync(user => user.Id == idUser);
            var gameProduct = await gameProductRepository.GetByIdAsync(id);
            await repositoryWishList.DeleteAsync(user.Id, gameProduct.Id);

            return RedirectToAction("WishList", "Home");
        }

    }
}
