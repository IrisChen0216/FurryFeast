using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Controllers
{
	[Route("/api/Member/[action]")]
	[ApiController]
	public class MemberApiController : ControllerBase
	{
		private readonly db_a989fb_furryfeastContext _context;



		public MemberApiController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}


		public object All()
		{

			return _context.Members.Select(x => new
			{
				member = new
				{
					x.MemberEmail,
					x.MemberPhone,
					x.MemberAdress,
					x.MemberBirthday,
					x.MemberGender,
					x.MemberName,
					x.MemberId
				}
			});
		}
	}
}
