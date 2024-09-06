using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShopModel.Data;
using GameShopModel.Entities;

namespace GameShop.Views.GameProducts
{
    public class GameProductsController : Controller
    {
        private readonly GameShopContext _context;

        public GameProductsController(GameShopContext context)
        {
            _context = context;
        }

        // GET: GameProducts
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.GameProducts.ToListAsync());
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        // GET: GameProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameProduct = await _context.GameProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameProduct == null)
            {
                return NotFound();
            }

            return View(gameProduct);
        }

        // GET: GameProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PresentationImageURL,Title,Description,Price,ReleaseDate")] GameProduct gameProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameProduct);
        }

        // GET: GameProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameProduct = await _context.GameProducts.FindAsync(id);
            if (gameProduct == null)
            {
                return NotFound();
            }
            return View(gameProduct);
        }

        // POST: GameProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PresentationImageURL,Title,Description,Price,ReleaseDate")] GameProduct gameProduct)
        {
            if (id != gameProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameProductExists(gameProduct.Id))
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
            return View(gameProduct);
        }

        // GET: GameProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameProduct = await _context.GameProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameProduct == null)
            {
                return NotFound();
            }

            return View(gameProduct);
        }

        // POST: GameProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameProduct = await _context.GameProducts.FindAsync(id);
            if (gameProduct != null)
            {
                _context.GameProducts.Remove(gameProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameProductExists(int id)
        {
            return _context.GameProducts.Any(e => e.Id == id);
        }
    }
}
