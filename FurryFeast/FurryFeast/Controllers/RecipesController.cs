using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
	public class RecipesController : Controller
	{
		public IActionResult Recipes()
		{
			return View();
		}
	}
}
