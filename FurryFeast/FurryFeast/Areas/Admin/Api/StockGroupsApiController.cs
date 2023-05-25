using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/StockGroupsApiController/[action]")]
	[ApiController]
	public class StockGroupsApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockGroupsApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<object> GetAll() {
			if (_context.StockGroups == null) {
				return NotFound("StockGroups is null.");
			} else {
				var result = await _context.StockGroups.Select(data => new StockGroupViewModel {
					GroupsId = data.GroupsId,
					GroupsCode = data.GroupsCode,
					GgroupsDescription = data.GgroupsDescription,
					GroupsNotes = data.GroupsNotes
				}).ToListAsync();
				return Ok(result);
			}
		}
	}
}
