using FurryFeast.Models;
using FurryFeast.ViewModels;
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
			return _context.Articles.Include(x => x.Admin)
				.Select(x => new
					{
						x.Admin.AdminAccount,
						x.AdminId,
						x.ArticleTitle,
						x.ArticleText,
						x.ArticleDate,
						x.ArticleId
					}
				).ToList();

		}


		[HttpGet("{id}")]
		public object GetArticle(int id)
		{

			return _context.Articles.Include(x => x.Admin)
				.Where(x => x.ArticleId == id)
				.Select(x => new
				{
					x.Admin.AdminAccount,
					x.AdminId,
					x.ArticleTitle,
					x.ArticleText,
					x.ArticleDate,
					x.ArticleId
				});

		}
	}

}
