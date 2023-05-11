using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;

namespace FurryFeast.Controllers
{
    public class StockWarehousesController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public StockWarehousesController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: StockWarehouses
        public async Task<IActionResult> Index()
        {
            var db_a989fb_furryfeastContext = _context.StockWarehouses.Include(s => s.WarehouseGroup);
            return View(await db_a989fb_furryfeastContext.ToListAsync());
        }

        // GET: StockWarehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockWarehouses == null)
            {
                return NotFound();
            }

            var stockWarehouse = await _context.StockWarehouses
                .Include(s => s.WarehouseGroup)
                .FirstOrDefaultAsync(m => m.WarehousesId == id);
            if (stockWarehouse == null)
            {
                return NotFound();
            }

            return View(stockWarehouse);
        }

        // GET: StockWarehouses/Create
        public IActionResult Create()
        {
            ViewData["WarehouseGroupId"] = new SelectList(_context.StockWarehouseGroups, "WarehouseGroupsId", "WarehouseGroupsId");
            return View();
        }

        // POST: StockWarehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WarehousesId,WarehousesCode,WarehousesDescription,WarehousesStreet,WarehousesZipCode,WarehousesCity,WarehousesCountry,WarehousesNation,WarehouseGroupId")] StockWarehouse stockWarehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockWarehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WarehouseGroupId"] = new SelectList(_context.StockWarehouseGroups, "WarehouseGroupsId", "WarehouseGroupsId", stockWarehouse.WarehouseGroupId);
            return View(stockWarehouse);
        }

        // GET: StockWarehouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockWarehouses == null)
            {
                return NotFound();
            }

            var stockWarehouse = await _context.StockWarehouses.FindAsync(id);
            if (stockWarehouse == null)
            {
                return NotFound();
            }
            ViewData["WarehouseGroupId"] = new SelectList(_context.StockWarehouseGroups, "WarehouseGroupsId", "WarehouseGroupsId", stockWarehouse.WarehouseGroupId);
            return View(stockWarehouse);
        }

        // POST: StockWarehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehousesId,WarehousesCode,WarehousesDescription,WarehousesStreet,WarehousesZipCode,WarehousesCity,WarehousesCountry,WarehousesNation,WarehouseGroupId")] StockWarehouse stockWarehouse)
        {
            if (id != stockWarehouse.WarehousesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockWarehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockWarehouseExists(stockWarehouse.WarehousesId))
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
            ViewData["WarehouseGroupId"] = new SelectList(_context.StockWarehouseGroups, "WarehouseGroupsId", "WarehouseGroupsId", stockWarehouse.WarehouseGroupId);
            return View(stockWarehouse);
        }

        // GET: StockWarehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockWarehouses == null)
            {
                return NotFound();
            }

            var stockWarehouse = await _context.StockWarehouses
                .Include(s => s.WarehouseGroup)
                .FirstOrDefaultAsync(m => m.WarehousesId == id);
            if (stockWarehouse == null)
            {
                return NotFound();
            }

            return View(stockWarehouse);
        }

        // POST: StockWarehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockWarehouses == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.StockWarehouses'  is null.");
            }
            var stockWarehouse = await _context.StockWarehouses.FindAsync(id);
            if (stockWarehouse != null)
            {
                _context.StockWarehouses.Remove(stockWarehouse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockWarehouseExists(int id)
        {
          return (_context.StockWarehouses?.Any(e => e.WarehousesId == id)).GetValueOrDefault();
        }
    }
}
