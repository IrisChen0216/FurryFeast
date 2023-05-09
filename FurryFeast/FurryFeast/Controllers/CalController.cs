using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
	public class CalController : Controller
	{
		public IActionResult CalIndex()
		{
			return View();
		}

		public static class Controller
		{
			public static double CalculateRER(double weightInKg)
			{
				return 70 * Math.Pow(weightInKg, 0.75);
			}
		}
	}
}
