using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockSuppliersApiController/[action]")]
	[ApiController]
	public class StockSuppliersApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockSuppliersApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockSuppliers == null) {
				return NotFound();
			} else {
				var result = _context.StockSuppliers.Select(data => new StockSupplierViewModel {
					SuppliersId = data.SuppliersId,
					SuppliersCode = data.SuppliersCode,
					SuppliersDescription = data.SuppliersDescription,
					SuppliersStreet = data.SuppliersStreet,
					SuppliersZipCode = data.SuppliersZipCode,
					SuppliersCity = data.SuppliersCity,
					SuppliersCountry = data.SuppliersCountry,
					SuppliersNation = data.SuppliersNation,
					SuppliersPhone = data.SuppliersPhone,
					SuppliersFax = data.SuppliersFax,
					SuppliersEmail = data.SuppliersEmail,
					SuppliersUrl = data.SuppliersUrl,
					SupplierGroupId = data.SupplierGroupId
				});
				return Ok(result);
			}
		}
	}
}
