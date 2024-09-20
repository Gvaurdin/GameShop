using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShopModel.Data;
using GameShopModel.Entities;

namespace GameShop.Controllers
{
    public class WishListsController : Controller
    {
        private readonly GameShopContext _context;

        public WishListsController(GameShopContext context)
        {
            _context = context;
        }

        // GET: WishLists
        public async Task<IActionResult> Index()
        {
            var gameShopContext = _context.WishLists.Include(w => w.GameProduct);
            return View(await gameShopContext.ToListAsync());
        }

        // GET: WishLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists
                .Include(w => w.GameProduct)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishList == null)
            {
                return NotFound();
            }

            return View(wishList);
        }

        // GET: WishLists/Create
        public IActionResult Create()
        {
            ViewData["GameProductId"] = new SelectList(_context.GameProducts, "Id", "Description");
            return View();
        }

        // POST: WishLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameProductId")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameProductId"] = new SelectList(_context.GameProducts, "Id", "Description", wishList.GameProductId);
            return View(wishList);
        }

        // GET: WishLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists.FindAsync(id);
            if (wishList == null)
            {
                return NotFound();
            }
            ViewData["GameProductId"] = new SelectList(_context.GameProducts, "Id", "Description", wishList.GameProductId);
            return View(wishList);
        }

        // POST: WishLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameProductId")] WishList wishList)
        {
            if (id != wishList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishListExists(wishList.Id))
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
            ViewData["GameProductId"] = new SelectList(_context.GameProducts, "Id", "Description", wishList.GameProductId);
            return View(wishList);
        }

        // GET: WishLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists
                .Include(w => w.GameProduct)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishList == null)
            {
                return NotFound();
            }

            return View(wishList);
        }

        // POST: WishLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wishList = await _context.WishLists.FindAsync(id);
            if (wishList != null)
            {
                _context.WishLists.Remove(wishList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishListExists(int id)
        {
            return _context.WishLists.Any(e => e.Id == id);
        }
    }
}
