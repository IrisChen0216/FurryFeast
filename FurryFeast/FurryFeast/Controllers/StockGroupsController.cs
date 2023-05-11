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
    public class StockGroupsController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public StockGroupsController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: StockGroups
        public async Task<IActionResult> Index()
        {
              return _context.StockGroups != null ? 
                          View(await _context.StockGroups.ToListAsync()) :
                          Problem("Entity set 'db_a989fb_furryfeastContext.StockGroups'  is null.");
        }

        // GET: StockGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockGroups == null)
            {
                return NotFound();
            }

            var stockGroup = await _context.StockGroups
                .FirstOrDefaultAsync(m => m.GroupsId == id);
            if (stockGroup == null)
            {
                return NotFound();
            }

            return View(stockGroup);
        }

        // GET: StockGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupsId,GroupsCode,GgroupsDescription,GroupsNotes")] StockGroup stockGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockGroup);
        }

        // GET: StockGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockGroups == null)
            {
                return NotFound();
            }

            var stockGroup = await _context.StockGroups.FindAsync(id);
            if (stockGroup == null)
            {
                return NotFound();
            }
            return View(stockGroup);
        }

        // POST: StockGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupsId,GroupsCode,GgroupsDescription,GroupsNotes")] StockGroup stockGroup)
        {
            if (id != stockGroup.GroupsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockGroupExists(stockGroup.GroupsId))
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
            return View(stockGroup);
        }

        // GET: StockGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockGroups == null)
            {
                return NotFound();
            }

            var stockGroup = await _context.StockGroups
                .FirstOrDefaultAsync(m => m.GroupsId == id);
            if (stockGroup == null)
            {
                return NotFound();
            }

            return View(stockGroup);
        }

        // POST: StockGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockGroups == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.StockGroups'  is null.");
            }
            var stockGroup = await _context.StockGroups.FindAsync(id);
            if (stockGroup != null)
            {
                _context.StockGroups.Remove(stockGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockGroupExists(int id)
        {
          return (_context.StockGroups?.Any(e => e.GroupsId == id)).GetValueOrDefault();
        }
    }
}
