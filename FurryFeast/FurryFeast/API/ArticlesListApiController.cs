using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API
{
	[Route("api/articles/[action]")]
	[ApiController]
	public class ArticlesListController : ControllerBase
	{
		private readonly db_a989fb_furryfeastContext _context;

		public ArticlesListController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}

		public object GetModel()
		{
			return _context.Articles
				.Select(x => new
				{
					x.AdminId,
					x.ArticleTitle,
					x.ArticleText,
					x.ArticleDate,
					x.ArticleId
				}
					).ToList();

		}
	}
	



}
