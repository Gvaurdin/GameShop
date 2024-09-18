using GameShop.Repository;
using GameShop.Repository.Interfaces;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameShop.Controllers
{
    public class CartController(IHttpContextAccessor httpContextAccessor,
        GameShopContext gameShopContext,
        IRepositoryCart repositoryCart,
        IRepositoryWishList repositoryWishList) : Controller
    {
        public IActionResult Index()
        {
            var products = repositoryCart.GetProducts();
            var cartViewModel = new CartViewModel
            { 
                GameProducts = products,
                Sum = repositoryCart.GetSum
            };
            return View(cartViewModel);

        }

        public IActionResult Delete(int id)
        {
            repositoryCart.Delete(id);
            var products = repositoryCart.GetProducts();
            var cartViewModel = new CartViewModel
            {
                GameProducts = products,
                Sum = repositoryCart.GetSum
            };
            return View("Index",cartViewModel);
        }

        public async Task<IActionResult> PlaceOrder()
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = gameShopContext.Users.Where(x => x.Id == idUser).First();
                var products = repositoryCart.GetProducts();

                var cart = new Cart
                {
                    User = user,
                    GameProducts = [],
                    Sum = repositoryCart.GetSum,
                    DatePurchase = DateTime.Now
                };

                foreach (var product in products)
                {
                    var gameProduct = gameShopContext.GameProducts
                        .Where(gameProduct => gameProduct.Id == product.Id)
                        .First();
                    var wishGameProduct = gameShopContext.WishLists
                        .FirstOrDefault(wl => wl.GameProduct.Id == product.Id && wl.User.Id == user.Id);
                    if(wishGameProduct != null)
                    {
                        await repositoryWishList.DeleteAsync(wishGameProduct.User.Id, wishGameProduct.GameProduct.Id);
                    }
                    cart.GameProducts.Add(gameProduct);
                }

                await gameShopContext.Carts.AddAsync(cart);
                await gameShopContext.SaveChangesAsync();
                repositoryCart.Clear();
            }
            else
            {
                return Unauthorized();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
