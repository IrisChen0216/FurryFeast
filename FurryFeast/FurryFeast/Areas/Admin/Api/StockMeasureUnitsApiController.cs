using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockMeasureUnitsApiController/[action]")]
	[ApiController]
	public class StockMeasureUnitsApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockMeasureUnitsApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		//[HttpGet]
		//public async Task<IEnumerable<StockMeasureUnitsViewModel>> GetAll() {
		//	var result = await _context.StockMeasureUnits.Select(data => new StockMeasureUnitsViewModel {
		//		MeasureUnitsId = data.MeasureUnitsId,
		//		MeasureUnitsCode = data.MeasureUnitsCode,
		//		MeasureUnitsDescription = data.MeasureUnitsDescription,
		//	}).ToListAsync();
		//	return result;
		//}

		//[HttpGet]
		//public async Task<object> GetAll() {
		//	if (_context.StockArticles == null) {
		//		return NotFound();
		//	} else {
		//		var result = await _context.StockMeasureUnits.Select(data => new StockMeasureUnitsViewModel {
		//			MeasureUnitsId = data.MeasureUnitsId,
		//			MeasureUnitsCode = data.MeasureUnitsCode,
		//			MeasureUnitsDescription = data.MeasureUnitsDescription,
		//		}).ToListAsync();
		//		return Ok(result);
		//	}
		//}

		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockMeasureUnits == null) {
				return NotFound();
			} else {
				var result = _context.StockMeasureUnits.Select(data => new StockMeasureUnitsViewModel {
					MeasureUnitsId = data.MeasureUnitsId,
					MeasureUnitsCode = data.MeasureUnitsCode,
					MeasureUnitsDescription = data.MeasureUnitsDescription,
				});
				return Ok(result);
			}
		}
	}
}
