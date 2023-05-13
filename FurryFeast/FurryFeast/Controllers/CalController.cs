using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
	public class CalController : Controller
	{
		public IActionResult CalIndex()
		{
			return View();
		}

	}
}
