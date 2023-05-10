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
    public class ConponsController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public ConponsController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: Conpons
        public async Task<IActionResult> Index()
        {
              return _context.Conpons != null ? 
                          View(await _context.Conpons.ToListAsync()) :
                          Problem("Entity set 'db_a989fb_furryfeastContext.Conpons'  is null.");
        }

        // GET: Conpons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Conpons == null)
            {
                return NotFound();
            }

            var conpon = await _context.Conpons
                .FirstOrDefaultAsync(m => m.ConponId == id);
            if (conpon == null)
            {
                return NotFound();
            }

            return View(conpon);
        }

        // GET: Conpons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conpons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConponId,ConponContent,ConponName,ConponDiscount,ConponStartTime,ConponEndTime")] Conpon conpon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conpon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conpon);
        }

        // GET: Conpons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Conpons == null)
            {
                return NotFound();
            }

            var conpon = await _context.Conpons.FindAsync(id);
            if (conpon == null)
            {
                return NotFound();
            }
            return View(conpon);
        }

        // POST: Conpons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConponId,ConponContent,ConponName,ConponDiscount,ConponStartTime,ConponEndTime")] Conpon conpon)
        {
            if (id != conpon.ConponId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conpon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConponExists(conpon.ConponId))
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
            return View(conpon);
        }

        // GET: Conpons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Conpons == null)
            {
                return NotFound();
            }

            var conpon = await _context.Conpons
                .FirstOrDefaultAsync(m => m.ConponId == id);
            if (conpon == null)
            {
                return NotFound();
            }

            return View(conpon);
        }

        // POST: Conpons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Conpons == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.Conpons'  is null.");
            }
            var conpon = await _context.Conpons.FindAsync(id);
            if (conpon != null)
            {
                _context.Conpons.Remove(conpon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConponExists(int id)
        {
          return (_context.Conpons?.Any(e => e.ConponId == id)).GetValueOrDefault();
        }
    }
}
