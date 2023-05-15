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
        public async Task<IActionResult> Index(int id)
        {
            var petClasses = _context.PetClasses.Include(p => p.PetClassType).Include(p => p.PetTypes).Include(p => p.Teacher);
            if (id == 1)
            {
                petClasses = _context.PetClasses.Include(p => p.PetClassType).Include(p => p.PetTypes).Include(p => p.Teacher);               
            }
            return View(petClasses);
        }

        public async Task<FileResult> GetPicture(int id)
        {
            PetClass p = await _context.PetClasses.FindAsync(id);     
            byte[] content = p?.PetClassPics.FirstOrDefault(pc => pc.PetClassId == id).PetClassPicImage;
            return File(content, "image/jpeg");
        }

        // GET: PetClasses/Details/5
        public async Task<IActionResult> PetClassDetail(int? id)
        {
            var petClass = await _context.PetClasses
                .Include(p => p.PetClassType)
                .Include(p => p.PetTypes)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync(m => m.PetClassId == id);
           
            return View(petClass);
        }

        public async Task<IActionResult> PetClassReservation(int? id)
        {
            var petClass = await _context.PetClasses
                
                .Include(p => p.PetClassType)
                .Include(p => p.PetTypes)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync(m => m.PetClassId == id);

            return View(petClass);
        }
        
        // GET: PetClasses/Create
        public IActionResult Create()
        {
            ViewData["PetClassTypeId"] = new SelectList(_context.PetClassTypes, "PetClassTypeId", "PetClassTypeId");
            ViewData["PetTypesId"] = new SelectList(_context.PetTypes, "PetTypesId", "PetTypesId");
            ViewData["TeacherId"] = new SelectList(_context.Teacheres, "TeacherId", "TeacherId");
            return View();
        }

        // POST: PetClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetClassId,PetClassName,PetClassPrice,PetClassInformation,PetClassDate,TeacherId,PetTypesId,PetClassTypeId")] PetClass petClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetClassTypeId"] = new SelectList(_context.PetClassTypes, "PetClassTypeId", "PetClassTypeId", petClass.PetClassTypeId);
            ViewData["PetTypesId"] = new SelectList(_context.PetTypes, "PetTypesId", "PetTypesId", petClass.PetTypesId);
            ViewData["TeacherId"] = new SelectList(_context.Teacheres, "TeacherId", "TeacherId", petClass.TeacherId);
            return View(petClass);
        }

        // GET: PetClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["PetClassTypeId"] = new SelectList(_context.PetClassTypes, "PetClassTypeId", "PetClassTypeId", petClass.PetClassTypeId);
            ViewData["PetTypesId"] = new SelectList(_context.PetTypes, "PetTypesId", "PetTypesId", petClass.PetTypesId);
            ViewData["TeacherId"] = new SelectList(_context.Teacheres, "TeacherId", "TeacherId", petClass.TeacherId);
            return View(petClass);
        }

        // POST: PetClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetClassId,PetClassName,PetClassPrice,PetClassInformation,PetClassDate,TeacherId,PetTypesId,PetClassTypeId")] PetClass petClass)
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
            ViewData["PetClassTypeId"] = new SelectList(_context.PetClassTypes, "PetClassTypeId", "PetClassTypeId", petClass.PetClassTypeId);
            ViewData["PetTypesId"] = new SelectList(_context.PetTypes, "PetTypesId", "PetTypesId", petClass.PetTypesId);
            ViewData["TeacherId"] = new SelectList(_context.Teacheres, "TeacherId", "TeacherId", petClass.TeacherId);
            return View(petClass);
        }

        // GET: PetClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PetClasses == null)
            {
                return NotFound();
            }

            var petClass = await _context.PetClasses
                .Include(p => p.PetClassType)
                .Include(p => p.PetTypes)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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

        private bool PetClassExists(int id)
        {
          return (_context.PetClasses?.Any(e => e.PetClassId == id)).GetValueOrDefault();
        }
    }
}
