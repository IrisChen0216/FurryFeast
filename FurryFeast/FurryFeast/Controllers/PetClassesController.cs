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
        //舊版
        public async Task<IActionResult> Index(int id)
        {
            var petClasses = _context.PetClasses.Include(p => p.PetClassType).Include(p => p.PetTypes).Include(p => p.Teacher);
            if (id == 1)
            {
                petClasses = _context.PetClasses.Include(p => p.PetClassType).Include(p => p.PetTypes).Include(p => p.Teacher);               
            }
            return View(petClasses);
        }

		//VUE用
		public async Task<IActionResult> PetClassIndexNew()
		{
			
			return View();
		}

        //VUE用
		public async Task<IActionResult> PetClassDetailNew(int? id)
		{
			
            ViewBag.Id = id;
			return View();
		}

		public async Task<FileResult> GetPicture(int id)
        {
            PetClass p = await _context.PetClasses.FindAsync(id);     
            byte[] content = p?.PetClassPics.FirstOrDefault(pc => pc.PetClassId == id).PetClassPicImage;
            return File(content, "image/jpeg");
        }

        // GET: PetClasses/Details/5
        //public async Task<IActionResult> PetClassDetail(int? id)
        //{
        //    var petClass = await _context.PetClasses
        //        .Include(p => p.PetClassType)
        //        .Include(p => p.PetTypes)
        //        .Include(p => p.Teacher)
        //        .FirstOrDefaultAsync(m => m.PetClassId == id);
           
        //    return View(petClass);
        //}

        public async Task<IActionResult> PetClassReservation(int? id)
        {
            var petClass = await _context.PetClasses
                
                .Include(p => p.PetClassType)
                .Include(p => p.PetTypes)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync(m => m.PetClassId == id);

            return View(petClass);
        }
        
        
        private bool PetClassExists(int id)
        {
          return (_context.PetClasses?.Any(e => e.PetClassId == id)).GetValueOrDefault();
        }
    }
}
