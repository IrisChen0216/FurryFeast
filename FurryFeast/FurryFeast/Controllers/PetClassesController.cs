using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;
using System.Security.Claims;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Authorization;

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

		//VUE用
		public async Task<IActionResult> PetClassReservationNew(int? id)
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


        [Authorize]
        public async Task<IActionResult> PetClassReservation(int? id)
        {
            var petClass = await _context.PetClasses

                .Include(p => p.PetClassType)
                .Include(p => p.PetTypes)
                .Include(p => p.Teacher)
                .FirstOrDefaultAsync(m => m.PetClassId == id);

            int userID = int.Parse(User.FindFirstValue("Id"));
            var member = await _context.Members.FindAsync(userID);
            PetClassReservViewModel reservetion = new PetClassReservViewModel
            {
                PetClassId=petClass.PetClassId,
                ClassReservetionDate=DateTime.Now,
                PetClassName=petClass.PetClassName,
                PetClassPrice=petClass.PetClassPrice,
                PetClassDate= petClass.PetClassDate,
                TeacherId = petClass.TeacherId,
                PetClassTypeId = petClass.PetClassTypeId,
                MemberId= userID,
                MemberName=member.MemberName,
                MemberEmail=member.MemberEmail,
                MemberPhone=member.MemberPhone
            };

            return View(reservetion);
        }

        //課程確認頁面
		public async Task<IActionResult> PetClassAddReservation(AddReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                ClassReservetion classReservetion = new ClassReservetion
                {
                    MemberId = model.MemberId,
                    PetClassId = model.PetClassId,
                    ClassReservetionDate = DateTime.Now,
                    ClassReservetionState = 0,
                };

                _context.ClassReservetions.Add(classReservetion);
                await _context.SaveChangesAsync();
                return View("PetClassPayReservation");
            }
            return View("PetClassAddReservation");
        }

        //付款頁面
        public async Task<IActionResult> PetClassPayReservation(AddReservationViewModel model)
        {
            return View();
        }
		private bool PetClassExists(int id)
        {
          return (_context.PetClasses?.Any(e => e.PetClassId == id)).GetValueOrDefault();
        }
    }
}
