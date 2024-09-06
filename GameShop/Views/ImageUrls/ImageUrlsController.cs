using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShopModel.Data;
using GameShopModel.Entities;

namespace GameShop.Views.ImageUrls
{
    public class ImageUrlsController : Controller
    {
        private readonly GameShopContext _context;

        public ImageUrlsController(GameShopContext context)
        {
            _context = context;
        }

        // GET: ImageUrls
        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }

        // GET: ImageUrls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageUrl = await _context.Images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageUrl == null)
            {
                return NotFound();
            }

            return View(imageUrl);
        }

        // GET: ImageUrls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImageUrls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,URL")] ImageUrl imageUrl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imageUrl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageUrl);
        }

        // GET: ImageUrls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageUrl = await _context.Images.FindAsync(id);
            if (imageUrl == null)
            {
                return NotFound();
            }
            return View(imageUrl);
        }

        // POST: ImageUrls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,URL")] ImageUrl imageUrl)
        {
            if (id != imageUrl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageUrl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageUrlExists(imageUrl.Id))
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
            return View(imageUrl);
        }

        // GET: ImageUrls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageUrl = await _context.Images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageUrl == null)
            {
                return NotFound();
            }

            return View(imageUrl);
        }

        // POST: ImageUrls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageUrl = await _context.Images.FindAsync(id);
            if (imageUrl != null)
            {
                _context.Images.Remove(imageUrl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageUrlExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
