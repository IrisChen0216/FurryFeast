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

		public object GetAdmin()
		{
			return _context.Admins
				.Select(x => new
					{
						x.AdminId,
						x.AdminAccount
					}
				).ToList();
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

		[HttpPost] //edit
		public object UpdateArticle([FromBody] ArticleViewModel model)
		{
			Article editedArticle = _context.Articles.FirstOrDefault(x => x.ArticleId == model.ArticleId);
			if (editedArticle != null)
			{
				editedArticle.ArticleTitle = model.ArticleTitle;
				editedArticle.ArticleText = model.ArticleText;
				editedArticle.ArticleDate = model.ArticleDate;

				_context.Articles.Update(editedArticle);
				_context.SaveChanges();
				return new { success = true, message = "Article updated successfully" };
			}
			else
			{
				return new { success = false, message = "Article not found" };
			}
		}

		[HttpDelete("{id}")]
		public async Task<string> deleteAticle(int id)
		{

			var aticle = await _context.Articles.FindAsync(id);
			if (aticle != null)
			{
				_context.Articles.Remove(aticle);
			}

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				return "刪除文章失敗";
			}

			//return NoContent();
			return "刪除文章成功!";
		}

		[HttpPost]
		public async Task<string> AddArticle([FromBody] ArticleViewModel model)
		{

			Article newArticle = new Article();

			newArticle.AdminId = model.AdminId;
			newArticle.ArticleTitle = model.ArticleTitle;
			newArticle.ArticleText = model.ArticleText;
			newArticle.ArticleDate = model.ArticleDate;

			_context.Articles.Add(newArticle);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{

				return "新增文章失敗";

			}

			return "Add Success";
		}

	}

}
