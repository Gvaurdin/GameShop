using GameShopModel.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Controllers
{
    public class GameController(GameShopContext gameShopContext) : Controller
    {
        public async Task<IActionResult> Details(int id)
        {
         var t = await gameShopContext.GameProducts
                .Include(gameProduct => gameProduct.Genres)
                .Include(gameProduct => gameProduct.ImagesUrl)
                .FirstAsync(gameProduct => gameProduct.Id == id);
            return View();
        }
    }
}
