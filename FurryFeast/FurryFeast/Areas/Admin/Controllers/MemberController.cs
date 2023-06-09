using FurryFeast.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MemberController : Controller
	{
		private readonly db_a989fb_furryfeastContext _context;

		public MemberController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Edit()
		{
			
			return View();
		}

		
	}
}
