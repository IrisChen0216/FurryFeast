using FurryFeast.Data;
using FurryFeast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
	public class RecipesController : Controller
	{
		private readonly db_a989fb_furryfeastContext _context;
		public RecipesController(db_a989fb_furryfeastContext furryFeastContext)
		{
			_context = furryFeastContext;
		}
		public IActionResult Recipes()
		{
			return View();
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddComment(int ID, string UserComment)
		{
			var comment = new MsgBoard()
			{
				MsgRecipesId = ID,
				MsgContent = UserComment,
				UserId = HttpContext.User.Identity.Name, //取得登入中的帳號
				MsgDateTime = DateTime.Now //取得當下時間
			};
			_context.Add(comment);
			await _context.SaveChangesAsync();
			return RedirectToAction("Recipes");
		}
	}

}
