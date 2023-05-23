using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Data;
using FurryFeast.Models;

namespace FurryFeast.Controllers
{
	public class RecipesController : Controller
	{
		private readonly db_a989fb_furryfeastContext _context;

		public RecipesController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}

		// 在這個方法中，我們將從數據庫獲取所有的 `Recipe` 對象並返回到視圖。
		public async Task<IActionResult> Recipes()
		{
			var recipes = await _context.Recipes.ToListAsync();
			return View(recipes);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRecipes()
		{
			var recipes = await _context.Recipes.ToListAsync();
			return View(recipes);
		}

		// 可以使用這個方法來獲取特定的 `Recipe` 對象。
		[HttpGet]
		public async Task<IActionResult> GetRecipe(int id)
		{
			var recipe = await _context.Recipes.FindAsync(id);

			if (recipe == null)
			{
				return NotFound();
			}

			return View(recipe);
		}
	}
}