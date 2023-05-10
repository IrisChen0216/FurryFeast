using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;

namespace FurryFeast.Controllers {

    [Area(@"STOCK\WareHouseGroup")]
    public class StockWarehouseGroupsController : Controller {
        private readonly db_a989fb_furryfeastContext _context;

        public StockWarehouseGroupsController(db_a989fb_furryfeastContext context) {
            _context = context;
        }

        // GET: StockWarehouseGroups
        public async Task<IActionResult> Index() {
            return _context.StockWarehouseGroups != null ?
                        View(await _context.StockWarehouseGroups.ToListAsync()) :
                        Problem("Entity set 'db_a989fb_furryfeastContext.StockWarehouseGroups'  is null.");
        }

        // GET: StockWarehouseGroups/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.StockWarehouseGroups == null) {
                return NotFound();
            }

            var stockWarehouseGroup = await _context.StockWarehouseGroups
                .FirstOrDefaultAsync(m => m.WarehouseGroupsId == id);
            if (stockWarehouseGroup == null) {
                return NotFound();
            }

            return View(stockWarehouseGroup);
        }

        // GET: StockWarehouseGroups/Create
        public IActionResult Create() {
            return View();
        }

        // POST: StockWarehouseGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WarehouseGroupsId,WarehouseGroupsCode,WarehouseGroupsDescription")] StockWarehouseGroup stockWarehouseGroup) {
            if (ModelState.IsValid) {
                _context.Add(stockWarehouseGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockWarehouseGroup);
        }

        // GET: StockWarehouseGroups/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.StockWarehouseGroups == null) {
                return NotFound();
            }

            var stockWarehouseGroup = await _context.StockWarehouseGroups.FindAsync(id);
            if (stockWarehouseGroup == null) {
                return NotFound();
            }
            return View(stockWarehouseGroup);
        }

        // POST: StockWarehouseGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseGroupsId,WarehouseGroupsCode,WarehouseGroupsDescription")] StockWarehouseGroup stockWarehouseGroup) {
            if (id != stockWarehouseGroup.WarehouseGroupsId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(stockWarehouseGroup);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!StockWarehouseGroupExists(stockWarehouseGroup.WarehouseGroupsId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stockWarehouseGroup);
        }

        // GET: StockWarehouseGroups/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.StockWarehouseGroups == null) {
                return NotFound();
            }

            var stockWarehouseGroup = await _context.StockWarehouseGroups
                .FirstOrDefaultAsync(m => m.WarehouseGroupsId == id);
            if (stockWarehouseGroup == null) {
                return NotFound();
            }

            return View(stockWarehouseGroup);
        }

        // POST: StockWarehouseGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.StockWarehouseGroups == null) {
                return Problem("Entity set 'db_a989fb_furryfeastContext.StockWarehouseGroups'  is null.");
            }
            var stockWarehouseGroup = await _context.StockWarehouseGroups.FindAsync(id);
            if (stockWarehouseGroup != null) {
                _context.StockWarehouseGroups.Remove(stockWarehouseGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockWarehouseGroupExists(int id) {
            return (_context.StockWarehouseGroups?.Any(e => e.WarehouseGroupsId == id)).GetValueOrDefault();
        }
    }
}
