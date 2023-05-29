using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Data;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FurryFeast.Controllers
{
	public class RecipesController : Controller
	{
		private readonly db_a989fb_furryfeastContext _context;

		public RecipesController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Recipes()
		{
			return View();
		}
	}
}
