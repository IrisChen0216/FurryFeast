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

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockMeasureUnits == null) {
				return NotFound("StockMeasureUnits is null.");
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
		public async Task<object> PostData([FromBody] StockMeasureUnitsViewModel data) {
			if (_context.StockMeasureUnits == null) {
				return NotFound("StockMeasureUnits is null.");
				// 如果資料重複
			} else if (_context.StockMeasureUnits?.Any(e => e.MeasureUnitsCode == data.MeasureUnitsCode) == true) {
				return Conflict($"Data duplicate, MeasureUnitsCode: {data.MeasureUnitsCode}");
			} else {
				StockMeasureUnit result = new StockMeasureUnit {
					MeasureUnitsId = data.MeasureUnitsId,
					MeasureUnitsCode = data.MeasureUnitsCode,
					MeasureUnitsDescription = data.MeasureUnitsDescription
				};
				_context.StockMeasureUnits?.Add(result);
				await _context.SaveChangesAsync();
				return Ok($"Post success, MeasureUnitsCode: {data.MeasureUnitsCode}.");
			}
		}

		// 刪除一筆資料
		//[HttpDelete]
		//public async Task<object> DeleteData(StockMeasureUnitsViewModel data) {
		//	if (_context.StockMeasureUnits == null) {
		//		return NotFound("StockMeasureUnits is null");
		//	} else if () {

		//	}
		//}
	}
}
