using FurryFeast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Security.Claims;


namespace FurryFeast.API;

//[Authorize]
[Route("/api/Member/[action]")]
[ApiController]
public class MemberApiController : ControllerBase
{
    private readonly db_a989fb_furryfeastContext _context;



    public MemberApiController(db_a989fb_furryfeastContext context)
    {
        _context = context;
    }


    public object One()
    {
        var id = User.FindFirstValue("Id");
        return _context.Members.Where(x => x.MemberId == int.Parse(id)).Select(x => new
        {
            Updatemember = new
            {
                x.MemberEmail,
                x.MemberPhone,
                x.MemberAdress,
                x.MemberBirthday,
                x.MemberGender,
                x.MemberName
            }
        });
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] MemberEditDto list)
    {
        var id = User.FindFirstValue("Id");
        var result = _context.Members.Include(m => m.Conpon).Where(m => m.MemberId == int.Parse(id)).FirstOrDefault();
        result.MemberAdress = list.MemberAdress;
        result.MemberGender = list.MemberGender;
        result.MemberPhone = list.MemberPhone;
        result.MemberEmail = list.MemberEmail;
        result.MemberName = list.MemberName;
        result.MemberBirthday = list.MemberBirthday;


        _context.SaveChanges();
        return NoContent();

    }

    [HttpPut]
    public object EditPassord([FromBody] PassordEditDto item)
    {
        var id = User.FindFirstValue("Id");
        var result = _context.Members.Where(x => x.MemberId == int.Parse(id)).FirstOrDefault();
        try
        {
			if (item.checkPassord == result.MemberPassord)
			{
				result.MemberPassord = item.newPassord;

			}
			_context.SaveChanges();
			return "good";
		}
        catch (Exception) {
            return "bad";
        }

	}

    }



