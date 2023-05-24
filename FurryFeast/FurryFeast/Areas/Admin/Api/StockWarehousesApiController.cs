using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockWarehousesApiController/[action]")]
	[ApiController]
	public class StockWarehousesApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockWarehousesApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockWarehouses == null) {
				return NotFound();
			} else {
				var result = _context.StockWarehouses.Select(data => new StockWarehouseViewModel {
					WarehousesId = data.WarehousesId,
					WarehousesCode = data.WarehousesCode,
					WarehousesDescription = data.WarehousesDescription,
					WarehousesStreet = data.WarehousesStreet,
					WarehousesZipCode = data.WarehousesZipCode,
					WarehousesCity = data.WarehousesCity,
					WarehousesCountry = data.WarehousesCountry,
					WarehousesNation = data.WarehousesNation,
					WarehouseGroupId = data.WarehouseGroupId
				});
				return Ok(result);
			}
		}
	}
}
