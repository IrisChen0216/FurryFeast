using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockImagesApiController/[action]")]
	[ApiController]
	public class StockImagesApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockImagesApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockImages == null) {
				return NotFound();
			} else {
				var result = _context.StockImages.Select(data => new StockImageViewModel {
					ImagesId = data.ImagesId,
					ImagesCode = data.ImagesCode,
					ImagesDescription = data.ImagesDescription,
					ImagesFileCrc = data.ImagesFileCrc,
					ImagesBitmapFile = data.ImagesBitmapFile
				});
				return Ok(result);
			}
		}
	}
}
