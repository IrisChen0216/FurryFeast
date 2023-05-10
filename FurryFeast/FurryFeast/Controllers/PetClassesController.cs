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
    public class PetClassesController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public PetClassesController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: PetClasses
        public async Task<IActionResult> Index()
        {
            var db_a989fb_furryfeastContext = _context.PetClasses.Include(p => p.Teacher);
            return View(await db_a989fb_furryfeastContext.ToListAsync());
        }

        // GET: PetClasses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PetClasses == null)
            {
                return NotFound();
            }

            var petClass = await _context.PetClasses
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync(m => m.PetClassId == id);
            if (petClass == null)
            {
                return NotFound();
            }

            return View(petClass);
        }

        // GET: PetClasses/Create
        public IActionResult Create()
        {
            ViewData["TeacherId"] = new SelectList(_context.Teacheres, "TeacherId", "TeacherId");
            return View();
        }

        public async Task<IActionResult> PetClassDetail()
        {
            
            return View();
        }
        public async Task<IActionResult> PetClassReservation()
        {
            return View();
        }
        

        // POST: PetClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetClassId,PetClassName,PetClassPrice,PetClassInformation,TeacherId,PetClassDate")] PetClass petClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(_context.Teacheres, "TeacherId", "TeacherId", petClass.TeacherId);
            return View(petClass);
        }

        // GET: PetClasses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PetClasses == null)
            {
                return NotFound();
            }

            var petClass = await _context.PetClasses.FindAsync(id);
            if (petClass == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Teacheres, "TeacherId", "TeacherId", petClass.TeacherId);
            return View(petClass);
        }

        // POST: PetClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PetClassId,PetClassName,PetClassPrice,PetClassInformation,TeacherId,PetClassDate")] PetClass petClass)
        {
            if (id != petClass.PetClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetClassExists(petClass.PetClassId))
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
            ViewData["TeacherId"] = new SelectList(_context.Teacheres, "TeacherId", "TeacherId", petClass.TeacherId);
            return View(petClass);
        }

        // GET: PetClasses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PetClasses == null)
            {
                return NotFound();
            }

            var petClass = await _context.PetClasses
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync(m => m.PetClassId == id);
            if (petClass == null)
            {
                return NotFound();
            }

            return View(petClass);
        }

        // POST: PetClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PetClasses == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.PetClasses'  is null.");
            }
            var petClass = await _context.PetClasses.FindAsync(id);
            if (petClass != null)
            {
                _context.PetClasses.Remove(petClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetClassExists(string id)
        {
          return (_context.PetClasses?.Any(e => e.PetClassId == id)).GetValueOrDefault();
        }
    }
}
