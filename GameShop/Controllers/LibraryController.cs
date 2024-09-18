using GameShopModel.Data;
using GameShopModel.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers
{
    public class LibraryController(IHttpContextAccessor httpContextAccessor,GameShopContext gameShopContext) : Controller
    {
        public async Task<IActionResult> Index()
        {
            Cart cart;
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                var idUser = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

                   cart = await gameShopContext.Carts
                    .AsNoTracking()
                    .Include(cart => cart.User)
                    .Include(cart => cart.GameProducts)
                    .Where(cart => cart.User.Id == idUser)
                    .FirstOrDefaultAsync();

                if (cart == null || !cart.GameProducts.Any())
                {
                    ViewBag.Message = "Ваша библиотека игр пуста.";
                    return View(new List<GameProduct>()); 
                }
                return View(cart.GameProducts);

            }
            else
            {
                return RedirectToAction("Account", "Login");
            }
        }
    }
}
