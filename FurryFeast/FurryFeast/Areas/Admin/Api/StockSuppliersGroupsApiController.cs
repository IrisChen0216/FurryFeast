using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockSuppliersGroupsApiController/[action]")]
	[ApiController]
	public class StockSuppliersGroupsApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockSuppliersGroupsApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockSuppliersGroups == null) {
				return NotFound();
			} else {
				var result = _context.StockSuppliersGroups.Select(data => new StockSuppliersGroupViewModel {
					SuppliersGroupsId = data.SuppliersGroupsId,
					SuppliersGroupsCode = data.SuppliersGroupsCode,
					SuppliersGroupsDescription = data.SuppliersGroupsDescription
				});
				return Ok(result);
			}
		}
	}
}
