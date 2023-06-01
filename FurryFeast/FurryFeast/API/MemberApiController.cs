using FurryFeast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.API;

[Authorize]
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
        return _context.Members.Select(x => new {
            member = new
            {
                memberName = x.MemberName,
                memberBirthday = x.MemberBirthday,
                memberGender = x.MemberGender,
                memberPhone = x.MemberPhone,
                memberEmail = x.MemberEmail,
                memberAdress = x.MemberAdress,
                memberId = x.MemberId
            }
        }).ToList();
    }
}