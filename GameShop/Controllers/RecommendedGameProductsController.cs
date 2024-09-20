using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShop.Repository.Repositories.Interfaces;
using GameShopModel.Repositories;
using GameShopModel.Repositories.Interfaces;
using GameShop.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.Controllers
{
    public class RecommendedGameProductsController(IRepositoryRecommendedGameProduct repositoryRecommendedGameProduct,
        IGameProductRepository gameProductRepository) : Controller
    {

        // GET: RecommendedGameProducts
        public async Task<IActionResult> Index() =>
               View(await repositoryRecommendedGameProduct.GetAllRecommendedGameProductsAsync());

        // GET: RecommendedGameProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedGameProduct = await repositoryRecommendedGameProduct.GetRecommendedGameProductAsync(id.Value);
            if (recommendedGameProduct == null)
            {
                return NotFound();
            }

            return View(recommendedGameProduct);
        }

        // GET: RecommendedGameProducts/Create
        //public async Task<IActionResult> Create()
        //{
        //    IEnumerable<GameProduct> gameProducts = await gameProductRepository.GetAllGameProductsAsync();
        //    ViewData["GameProductId"] = new SelectList(gameProducts, "Id", "Title");
        //    return View();
        //}

        //// POST: RecommendedGameProducts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("GameProductId,ExpertName,ExpertSurname")] RecommendedGameProduct recommendedGameProduct)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await repositoryRecommendedGameProduct.AddRecommendedGameProductAsync(recommendedGameProduct);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    IEnumerable<GameProduct> gameProducts = await gameProductRepository.GetAllGameProductsAsync();

        //    // Фильтрация по SelectedGameProduct, если указано
        //    if (!string.IsNullOrEmpty(recommendedGameProduct.SelectedGameProduct))
        //    {
        //        gameProducts = gameProducts.Where(g => g.Title.Contains(recommendedGameProduct.SelectedGameProduct, StringComparison.OrdinalIgnoreCase));
        //    }
        //    ViewData["GameProductId"] = new SelectList(gameProducts, "Id", "Title", recommendedGameProduct.GameProduct!.Id);
        //    return View(recommendedGameProduct);
        //}

        public async Task<IActionResult> Create()
        {
            var recommendedVM = new RecommendedGameProductVM
            {
                ExpertName = string.Empty,
                ExpertSurname = string.Empty,
                SelectedGameProduct = string.Empty,
                SearchGameProduct = string.Empty,
                GameProducts = new SelectList((await gameProductRepository.GetAllAsync()).Select(x => x.Title))
            };
            return View(recommendedVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecommendedGameProductVM recommendedGameProductVM)
        {
            if (ModelState.IsValid)
            {
                var gameProduct = await gameProductRepository.GetByTitleAsync(recommendedGameProductVM.SelectedGameProduct);
                var recommendedGameProduct = new RecommendedGameProduct
                {
                    ExpertName = recommendedGameProductVM.ExpertName,
                    ExpertSurname = recommendedGameProductVM.ExpertSurname,
                    GameProduct = gameProduct
                    
                };
                await repositoryRecommendedGameProduct.AddRecommendedGameProductAsync(recommendedGameProduct);
                return RedirectToAction(nameof(Index));
            }

            var gameProducts = (await gameProductRepository.GetAllAsync()).Select(x => x.Title);
            if (!string.IsNullOrEmpty(recommendedGameProductVM.SearchGameProduct))
            {
                gameProducts = gameProducts.Where(g => g.ToUpper().Contains(recommendedGameProductVM.SearchGameProduct.ToUpper()));
            }
            recommendedGameProductVM.GameProducts = new(gameProducts);
            return View(recommendedGameProductVM);
        }

        // GET: RecommendedGameProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedGameProduct = await repositoryRecommendedGameProduct.GetRecommendedGameProductAsync(id.Value);
            if (recommendedGameProduct == null)
            {
                return NotFound();
            }
            IEnumerable<GameProduct> gameProducts = await gameProductRepository.GetAllAsync();
            ViewData["GameProductId"] = new SelectList(gameProducts, "Id", "Title", recommendedGameProduct.GameProduct!.Id);
            return View(recommendedGameProduct);
        }

        // POST: RecommendedGameProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpertName,ExpertSurname,GameProductId")] RecommendedGameProduct recommendedGameProduct)
        {
            //if (id != recommendedGameProduct.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    await repositoryRecommendedGameProduct.EditGameProductAsync(id, recommendedGameProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await RecommendedGameProductExists(recommendedGameProduct.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<GameProduct> gameProducts = await gameProductRepository.GetAllAsync();
            ViewData["GameProductId"] = new SelectList(gameProducts, "Id", "Description", recommendedGameProduct.GameProduct!.Id);
            return View(recommendedGameProduct);
        }

        // GET: RecommendedGameProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendedGameProduct = await repositoryRecommendedGameProduct.GetRecommendedGameProductAsync(id.Value);
            if (recommendedGameProduct == null)
            {
                return NotFound();
            }

            return View(recommendedGameProduct);
        }

        // POST: RecommendedGameProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repositoryRecommendedGameProduct.RemoveRecommendedGameProductAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RecommendedGameProductExists(int id)
        {
            var recommendedGameProduct = await repositoryRecommendedGameProduct.GetRecommendedGameProductAsync(id);
            return recommendedGameProduct != null;
        }
    }
}
