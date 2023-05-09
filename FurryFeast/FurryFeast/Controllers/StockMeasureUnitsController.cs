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
    public class StockMeasureUnitsController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public StockMeasureUnitsController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: StockMeasureUnits
        public async Task<IActionResult> Index()
        {
              return _context.StockMeasureUnits != null ? 
                          View(await _context.StockMeasureUnits.ToListAsync()) :
                          Problem("Entity set 'db_a989fb_furryfeastContext.StockMeasureUnits'  is null.");
        }

        // GET: StockMeasureUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockMeasureUnits == null)
            {
                return NotFound();
            }

            var stockMeasureUnit = await _context.StockMeasureUnits
                .FirstOrDefaultAsync(m => m.MeasureUnitsId == id);
            if (stockMeasureUnit == null)
            {
                return NotFound();
            }

            return View(stockMeasureUnit);
        }

        // GET: StockMeasureUnits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockMeasureUnits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeasureUnitsId,MeasureUnitsCode,MeasureUnitsDescription")] StockMeasureUnit stockMeasureUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockMeasureUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockMeasureUnit);
        }

        // GET: StockMeasureUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockMeasureUnits == null)
            {
                return NotFound();
            }

            var stockMeasureUnit = await _context.StockMeasureUnits.FindAsync(id);
            if (stockMeasureUnit == null)
            {
                return NotFound();
            }
            return View(stockMeasureUnit);
        }

        // POST: StockMeasureUnits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeasureUnitsId,MeasureUnitsCode,MeasureUnitsDescription")] StockMeasureUnit stockMeasureUnit)
        {
            if (id != stockMeasureUnit.MeasureUnitsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockMeasureUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockMeasureUnitExists(stockMeasureUnit.MeasureUnitsId))
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
            return View(stockMeasureUnit);
        }

        // GET: StockMeasureUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockMeasureUnits == null)
            {
                return NotFound();
            }

            var stockMeasureUnit = await _context.StockMeasureUnits
                .FirstOrDefaultAsync(m => m.MeasureUnitsId == id);
            if (stockMeasureUnit == null)
            {
                return NotFound();
            }

            return View(stockMeasureUnit);
        }

        // POST: StockMeasureUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockMeasureUnits == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.StockMeasureUnits'  is null.");
            }
            var stockMeasureUnit = await _context.StockMeasureUnits.FindAsync(id);
            if (stockMeasureUnit != null)
            {
                _context.StockMeasureUnits.Remove(stockMeasureUnit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockMeasureUnitExists(int id)
        {
          return (_context.StockMeasureUnits?.Any(e => e.MeasureUnitsId == id)).GetValueOrDefault();
        }
    }
}
