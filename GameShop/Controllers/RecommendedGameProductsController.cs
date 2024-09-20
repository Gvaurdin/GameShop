using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShop.Repository.Repositories.Interfaces;
using GameShopModel.Repositories;
using GameShopModel.Repositories.Interfaces;

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
        public async Task<IActionResult> Create()
        {
            IEnumerable<GameProduct> gameProducts = await gameProductRepository.GetAllGameProductsAsync();
            ViewData["GameProductId"] = new SelectList(gameProducts, "Id", "Title");
            return View();
        }

        // POST: RecommendedGameProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameProductId,ExpertName,ExpertSurname")] RecommendedGameProduct recommendedGameProduct)
        {
            if (ModelState.IsValid)
            {
                await repositoryRecommendedGameProduct.AddRecommendedGameProductAsync(recommendedGameProduct);
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<GameProduct> gameProducts = await gameProductRepository.GetAllGameProductsAsync();
            ViewData["GameProductId"] = new SelectList(gameProducts, "Id", "Title", recommendedGameProduct.GameProduct!.Id);
            return View(recommendedGameProduct);
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
            IEnumerable<GameProduct> gameProducts = await gameProductRepository.GetAllGameProductsAsync();
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
            IEnumerable<GameProduct> gameProducts = await gameProductRepository.GetAllGameProductsAsync();
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
