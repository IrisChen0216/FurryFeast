using FurryFeast.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
	{
		private readonly db_a989fb_furryfeastContext _context;

		public ContactUsController(db_a989fb_furryfeastContext context) {
			_context = context;
		}
		public IActionResult ContactUs()
		{
			var data = _context.ContactUs;
			return View(data);
		}
	}
}
