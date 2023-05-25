using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StockSuppliersGroupsApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockSuppliersGroupsApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockSuppliersGroups == null) {
				return NotFound("StockSuppliersGroups is null.");
			} else {
				var result = await _context.StockSuppliersGroups.Select(data => new StockSuppliersGroupViewModel {
					SuppliersGroupsId = data.SuppliersGroupsId,
					SuppliersGroupsCode = data.SuppliersGroupsCode,
					SuppliersGroupsDescription = data.SuppliersGroupsDescription
				}).ToListAsync();
				return Ok(result);
			}
		}

		// 新增一筆資料
		[HttpPost]
		public async Task<object> PostData([FromBody] StockSuppliersGroupViewModel data) {
			if (_context.StockSuppliersGroups == null) {
				return NotFound("StockSuppliersGroups is null.");

				// 如果資料重複
			} else if (_context.StockSuppliersGroups?.Any(e => e.SuppliersGroupsCode == data.SuppliersGroupsCode) == true) {
				return Conflict($"Data duplicate, SuppliersGroupsCode: {data.SuppliersGroupsCode}");
			} else {
				StockSuppliersGroup result = new StockSuppliersGroup {
					SuppliersGroupsId = data.SuppliersGroupsId,
					SuppliersGroupsCode = data.SuppliersGroupsCode,
					SuppliersGroupsDescription = data.SuppliersGroupsDescription
				};
				_context.StockSuppliersGroups?.Add(result);
				await _context.SaveChangesAsync();
				return Ok($"Post success, SuppliersGroupsCode: {data.SuppliersGroupsCode}.");
			}
		}
	}
}
