using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/[controller]/[action]")]
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

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockMeasureUnits == null) {
				return Problem("StockMeasureUnits is null.");
			} else {
				var result = await _context.StockMeasureUnits.Select(data => new StockMeasureUnitsViewModel {
					MeasureUnitsId = data.MeasureUnitsId,
					MeasureUnitsCode = data.MeasureUnitsCode,
					MeasureUnitsDescription = data.MeasureUnitsDescription,
				}).ToListAsync();
				return Ok(result);
			}
		}

		// 新增一筆資料
		[HttpPost]
		public async Task<object> PostData(StockMeasureUnitsViewModel data) {
			if (_context.StockMeasureUnits == null) {
				return Problem("StockMeasureUnits is null.");
			}

			// 如果資料重複
			if (_context.StockMeasureUnits?.Any(e => e.MeasureUnitsCode == data.MeasureUnitsCode) == true) {
				return Problem($"Data duplicate, Code: {data.MeasureUnitsCode}");
			}

			StockMeasureUnit result = new StockMeasureUnit {
				MeasureUnitsId = data.MeasureUnitsId,
				MeasureUnitsCode = data.MeasureUnitsCode,
				MeasureUnitsDescription = data.MeasureUnitsDescription,
			};
			_context.StockMeasureUnits?.Add(result);
			await _context.SaveChangesAsync();
			return Ok($"Post success, Code: {data.MeasureUnitsCode}.");
		}
	}
}
