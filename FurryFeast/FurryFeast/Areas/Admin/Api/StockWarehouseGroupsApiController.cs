using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockWarehouseGroupsApiController/[action]")]
	[ApiController]
	public class StockWarehouseGroupsApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockWarehouseGroupsApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockWarehouseGroups == null) {
				return NotFound("StockWarehouseGroups is null.");
			} else {
				var result = await _context.StockWarehouseGroups.Select(data => new StockWarehouseGroupViewModel {
					WarehouseGroupsId = data.WarehouseGroupsId,
					WarehouseGroupsCode = data.WarehouseGroupsCode,
					WarehouseGroupsDescription = data.WarehouseGroupsDescription
				}).ToListAsync();
				return Ok(result);
			}
		}
	}
}
