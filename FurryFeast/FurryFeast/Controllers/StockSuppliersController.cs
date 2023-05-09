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
    public class StockSuppliersController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public StockSuppliersController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: StockSuppliers
        public async Task<IActionResult> Index()
        {
            var db_a989fb_furryfeastContext = _context.StockSuppliers.Include(s => s.SupplierGroup);
            return View(await db_a989fb_furryfeastContext.ToListAsync());
        }

        // GET: StockSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockSuppliers == null)
            {
                return NotFound();
            }

            var stockSupplier = await _context.StockSuppliers
                .Include(s => s.SupplierGroup)
                .FirstOrDefaultAsync(m => m.SuppliersId == id);
            if (stockSupplier == null)
            {
                return NotFound();
            }

            return View(stockSupplier);
        }

        // GET: StockSuppliers/Create
        public IActionResult Create()
        {
            ViewData["SupplierGroupId"] = new SelectList(_context.StockSuppliersGroups, "SuppliersGroupsId", "SuppliersGroupsId");
            return View();
        }

        // POST: StockSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SuppliersId,SuppliersCode,SuppliersDescription,SuppliersStreet,SuppliersZipCode,SuppliersCity,SuppliersCountry,SuppliersNation,SuppliersPhone,SuppliersFax,SuppliersEmail,SuppliersUrl,SupplierGroupId")] StockSupplier stockSupplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockSupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierGroupId"] = new SelectList(_context.StockSuppliersGroups, "SuppliersGroupsId", "SuppliersGroupsId", stockSupplier.SupplierGroupId);
            return View(stockSupplier);
        }

        // GET: StockSuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockSuppliers == null)
            {
                return NotFound();
            }

            var stockSupplier = await _context.StockSuppliers.FindAsync(id);
            if (stockSupplier == null)
            {
                return NotFound();
            }
            ViewData["SupplierGroupId"] = new SelectList(_context.StockSuppliersGroups, "SuppliersGroupsId", "SuppliersGroupsId", stockSupplier.SupplierGroupId);
            return View(stockSupplier);
        }

        // POST: StockSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SuppliersId,SuppliersCode,SuppliersDescription,SuppliersStreet,SuppliersZipCode,SuppliersCity,SuppliersCountry,SuppliersNation,SuppliersPhone,SuppliersFax,SuppliersEmail,SuppliersUrl,SupplierGroupId")] StockSupplier stockSupplier)
        {
            if (id != stockSupplier.SuppliersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockSupplierExists(stockSupplier.SuppliersId))
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
            ViewData["SupplierGroupId"] = new SelectList(_context.StockSuppliersGroups, "SuppliersGroupsId", "SuppliersGroupsId", stockSupplier.SupplierGroupId);
            return View(stockSupplier);
        }

        // GET: StockSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockSuppliers == null)
            {
                return NotFound();
            }

            var stockSupplier = await _context.StockSuppliers
                .Include(s => s.SupplierGroup)
                .FirstOrDefaultAsync(m => m.SuppliersId == id);
            if (stockSupplier == null)
            {
                return NotFound();
            }

            return View(stockSupplier);
        }

        // POST: StockSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockSuppliers == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.StockSuppliers'  is null.");
            }
            var stockSupplier = await _context.StockSuppliers.FindAsync(id);
            if (stockSupplier != null)
            {
                _context.StockSuppliers.Remove(stockSupplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockSupplierExists(int id)
        {
          return (_context.StockSuppliers?.Any(e => e.SuppliersId == id)).GetValueOrDefault();
        }
    }
}
