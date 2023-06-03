using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactUsController : Controller
	{
		public IActionResult ContactUs()
		{
			return View();
		}
	}
}
