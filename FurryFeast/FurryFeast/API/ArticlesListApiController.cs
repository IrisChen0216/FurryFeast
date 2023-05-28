using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.API
{
	[Route("api/Member/[action]")]
	[ApiController]
	public class ArticlesListController : ControllerBase
	{
		private readonly db_a989fb_furryfeastContext _context;

		public ArticlesListController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}
		public object GetArtclesList()
		{
			return _context.Articles
		}
	}
	



}
