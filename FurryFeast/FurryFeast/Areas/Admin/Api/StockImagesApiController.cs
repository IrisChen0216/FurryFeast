using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockImagesApiController/[action]")]
	[ApiController]
	public class StockImagesApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockImagesApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockImages == null) {
				return NotFound("StockImages is null.");
			} else {
				var result = await _context.StockImages.Select(data => new StockImageViewModel {
					ImagesId = data.ImagesId,
					ImagesCode = data.ImagesCode,
					ImagesDescription = data.ImagesDescription,
					ImagesFileCrc = data.ImagesFileCrc,
					ImagesBitmapFile = data.ImagesBitmapFile
				}).ToListAsync();
				return Ok(result);
			}
		}
	}
}
