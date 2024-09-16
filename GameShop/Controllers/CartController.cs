using GameShop.Repository;
using GameShop.Repository.Interfaces;
using GameShop.ViewModel;
using GameShopModel.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers
{
    public class CartController(IGameProductRepository gameProductRepository,IRepositoryCart repositoryCart) : Controller
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

        public async Task<IActionResult> Delete(int id)
        {
            var product = await gameProductRepository.GetGameProductAsync(id);
            repositoryCart.Delete(id);
            return View("Index", repositoryCart.GetProducts());
        }

        public IActionResult PlaceOrder()
        {
            var products = repositoryCart.GetProducts();

            // сохраняю в бд
            repositoryCart.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
