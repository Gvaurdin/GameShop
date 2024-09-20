using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
