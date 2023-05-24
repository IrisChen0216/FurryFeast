using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockGroupsApiController/[action]")]
	[ApiController]
	public class StockGroupsApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockGroupsApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockGroups == null) {
				return NotFound();
			} else {
				var result = _context.StockGroups.Select(data => new StockGroupViewModel {
					GroupsId = data.GroupsId,
					GroupsCode = data.GroupsCode,
					GgroupsDescription = data.GgroupsDescription,
					GroupsNotes = data.GroupsNotes
				});
				return Ok(result);
			}
		}
	}
}
