using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockSuppliersApiController/[action]")]
	[ApiController]
	public class StockSuppliersApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockSuppliersApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockSuppliers == null) {
				return NotFound("StockSuppliers is null.");
			} else {
				var result = await _context.StockSuppliers.Select(data => new StockSupplierViewModel {
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
				}).ToListAsync();
				return Ok(result);
			}
		}
	}
}
