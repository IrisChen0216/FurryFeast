using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StockWarehousesApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockWarehousesApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockWarehouses == null) {
				return NotFound("StockWarehouses is null.");
			} else {
				var result = await _context.StockWarehouses.Select(data => new StockWarehouseViewModel {
					WarehousesId = data.WarehousesId,
					WarehousesCode = data.WarehousesCode,
					WarehousesDescription = data.WarehousesDescription,
					WarehousesStreet = data.WarehousesStreet,
					WarehousesZipCode = data.WarehousesZipCode,
					WarehousesCity = data.WarehousesCity,
					WarehousesCountry = data.WarehousesCountry,
					WarehousesNation = data.WarehousesNation,
					WarehouseGroupId = data.WarehouseGroupId
				}).ToListAsync();
				return Ok(result);
			}
		}

		// 新增一筆資料
		[HttpPost]
		public async Task<object> PostData(StockWarehouseViewModel data) {
			if (_context.StockWarehouses == null) {
				return NotFound("StockWarehouses is null.");

				// 如果資料重複
			} else if (_context.StockWarehouses?.Any(e => e.WarehousesCode == data.WarehousesCode) == true) {
				return Conflict($"Data duplicate, WarehousesCode: {data.WarehousesCode}");
			} else {
				StockWarehouse result = new StockWarehouse {
					WarehousesId = data.WarehousesId,
					WarehousesCode = data.WarehousesCode,
					WarehousesDescription = data.WarehousesDescription,
					WarehousesStreet = data.WarehousesStreet,
					WarehousesZipCode = data.WarehousesZipCode,
					WarehousesCity = data.WarehousesCity,
					WarehousesCountry = data.WarehousesCountry,
					WarehousesNation = data.WarehousesNation,
					WarehouseGroupId = data.WarehouseGroupId
				};
				_context.StockWarehouses?.Add(result);
				await _context.SaveChangesAsync();
				return Ok($"Post success, WarehousesCode: {data.WarehousesCode}.");
			}
		}
	}
}
