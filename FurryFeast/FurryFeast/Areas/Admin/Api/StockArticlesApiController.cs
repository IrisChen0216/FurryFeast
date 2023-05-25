using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StockArticlesApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockArticlesApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockArticles == null) {
				return NotFound("StockArticles is null.");
			} else {
				var result = await _context.StockArticles.Select(data => new StockArticleViewModel {
					ArticlesId = data.ArticlesId,
					AarticlesCode = data.AarticlesCode,
					ArticlesIsValid = data.ArticlesIsValid,
					ArticlesDescription = data.ArticlesDescription,
					ArticlesNotes = data.ArticlesNotes,
					ArticlesPrice = data.ArticlesPrice,
					ArticlesQuantity = data.ArticlesQuantity,
					GroupId = data.GroupId,
					MeasureUnitId = data.MeasureUnitId,
					WarehousesId = data.WarehousesId,
					SuppliersId = data.SuppliersId,
					ImagesId = data.ImagesId
				}).ToListAsync();
				return Ok(result);
			}
		}

		// 新增一筆資料
		[HttpPost]
		public async Task<object> PostData([FromBody] StockArticleViewModel data) {
			if (_context.StockArticles == null) {
				return NotFound("StockArticles is null.");

				// 如果資料重複
			} else if (_context.StockArticles.Any(e => e.AarticlesCode == data.AarticlesCode) == true) {
				return Conflict($"Data duplicate, AarticlesCode: {data.AarticlesCode}");
			} else {
				StockArticle result = new StockArticle {
					ArticlesId = data.ArticlesId,
					AarticlesCode = data.AarticlesCode,
					ArticlesIsValid = data.ArticlesIsValid,
					ArticlesDescription = data.ArticlesDescription,
					ArticlesNotes = data.ArticlesNotes,
					ArticlesPrice = data.ArticlesPrice,
					ArticlesQuantity = data.ArticlesQuantity,
					GroupId = data.GroupId,
					MeasureUnitId = data.MeasureUnitId,
					WarehousesId = data.WarehousesId,
					SuppliersId = data.SuppliersId,
					ImagesId = data.ImagesId
				};
				_context.StockArticles.Add(result);
				await _context.SaveChangesAsync();
				return Ok($"Post success, AarticlesCode: {data.AarticlesCode}.");
			}
		}
	}
}
