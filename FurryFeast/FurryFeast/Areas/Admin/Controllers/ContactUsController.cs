using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Controllers
{
	public class ContactUsController : Controller
	{
		public IActionResult ContactUs()
		{
			return View();
		}
	}
}
