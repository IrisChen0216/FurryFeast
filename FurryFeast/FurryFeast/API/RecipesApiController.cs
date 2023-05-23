using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API
{
	[Route("api/recipes/[action]")]
	[ApiController]
	public class RecipesApiController : ControllerBase
	{
		private readonly db_a989fb_furryfeastContext _context;

		public RecipesApiController(db_a989fb_furryfeastContext context)
		{
			_context=context;	
		}

		public object AllRecipes()
		{
			return _context.Recipes.Include(x => x.MsgBoards).Select(x => new
			{
				AllRecipes=new
				{
					
					recipesName=x.RecipesName,
				}
			});
		}
	}
}
