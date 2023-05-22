using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API
{
	[Route("api/Member/[action]")]
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
            return _context.Members.Select(x => new memberData
            {
                memberName = x.MemberName,
                memberBirthday=x.MemberBirthday,
                memberGender=x.MemberGender,
                memberPhone=x.MemberPhone,
                memberEmail=x.MemberEmail,
                memberAdress = x.MemberAdress,

            }).ToList();
            
        }

    }
}
