using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;

namespace FurryFeast.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockSuppliersGroupsController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public StockSuppliersGroupsController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: StockSuppliersGroups
        public async Task<IActionResult> Index()
        {
              return _context.StockSuppliersGroups != null ? 
                          View(await _context.StockSuppliersGroups.ToListAsync()) :
                          Problem("Entity set 'db_a989fb_furryfeastContext.StockSuppliersGroups'  is null.");
        }

        // GET: StockSuppliersGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockSuppliersGroups == null)
            {
                return NotFound();
            }

            var stockSuppliersGroup = await _context.StockSuppliersGroups
                .FirstOrDefaultAsync(m => m.SuppliersGroupsId == id);
            if (stockSuppliersGroup == null)
            {
                return NotFound();
            }

            return View(stockSuppliersGroup);
        }

        // GET: StockSuppliersGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockSuppliersGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SuppliersGroupsId,SuppliersGroupsCode,SuppliersGroupsDescription")] StockSuppliersGroup stockSuppliersGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockSuppliersGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockSuppliersGroup);
        }

        // GET: StockSuppliersGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockSuppliersGroups == null)
            {
                return NotFound();
            }

            var stockSuppliersGroup = await _context.StockSuppliersGroups.FindAsync(id);
            if (stockSuppliersGroup == null)
            {
                return NotFound();
            }
            return View(stockSuppliersGroup);
        }

        // POST: StockSuppliersGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SuppliersGroupsId,SuppliersGroupsCode,SuppliersGroupsDescription")] StockSuppliersGroup stockSuppliersGroup)
        {
            if (id != stockSuppliersGroup.SuppliersGroupsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockSuppliersGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockSuppliersGroupExists(stockSuppliersGroup.SuppliersGroupsId))
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
            return View(stockSuppliersGroup);
        }

        // GET: StockSuppliersGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockSuppliersGroups == null)
            {
                return NotFound();
            }

            var stockSuppliersGroup = await _context.StockSuppliersGroups
                .FirstOrDefaultAsync(m => m.SuppliersGroupsId == id);
            if (stockSuppliersGroup == null)
            {
                return NotFound();
            }

            return View(stockSuppliersGroup);
        }

        // POST: StockSuppliersGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockSuppliersGroups == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.StockSuppliersGroups'  is null.");
            }
            var stockSuppliersGroup = await _context.StockSuppliersGroups.FindAsync(id);
            if (stockSuppliersGroup != null)
            {
                _context.StockSuppliersGroups.Remove(stockSuppliersGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockSuppliersGroupExists(int id)
        {
          return (_context.StockSuppliersGroups?.Any(e => e.SuppliersGroupsId == id)).GetValueOrDefault();
        }
    }
}
