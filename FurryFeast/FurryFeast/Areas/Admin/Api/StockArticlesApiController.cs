using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockArticlesApiController/[action]")]
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
	}
}
