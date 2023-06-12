using FurryFeast.Areas.Admin.ViewModels;
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
		
		public async Task<string>EditMember(EditMemberDto list)
		{
			try
			{
				//var member =await _context.Members.FindAsync(list.MemberId) ;
				var result = _context.Members.FirstOrDefault(x => x.MemberId == list.MemberId);
				result.MemberName = list.MemberName;
				result.MemberBirthday = list.MemberBirthday;
				result.MemberGender = list.MemberGender;
				result.MemberEmail = list.MemberEmail;
				result.MemberPhone = list.MemberPhone;
				result.MemberAdress = list.MemberAdress;

				//member.MemberName = list.MemberName;
				//member.MemberBirthday = list.MemberBirthday;
				//member.MemberGender = list.MemberGender;
				//member.MemberEmail = list.MemberEmail;
				//member.MemberPhone = list.MemberPhone;
				//member.MemberAdress = list.MemberAdress;

				await _context.SaveChangesAsync();
				return "成功";
			}
			catch (Exception )
			{
				return "失敗";
			}

		}
		[HttpDelete("{id}")]
		public async Task<string> DeleteMember(int id)
		{
			try
			{
				var member =await _context.Members.FindAsync(id);
				if (member == null) return "失敗";
				_context.Members.Remove(member);
				await _context.SaveChangesAsync();
				return "刪除成功";

			}
			catch (Exception ) { 
				return "刪除失敗"; 
			}
		}
		[HttpPost]

		public async Task<string> CreateMember([FromBody]CreateMemberDto list)
		{
			try
			{
				_context.Members.Add(new Member
				{
					MemberAccount = list.MemberAccount,
					MemberName = list.MemberName,
					MemberBirthday = list.MemberBirthday,
					MemberGender = list.MemberGender,
					MemberEmail = list.MemberEmail,
					MemberPhone = list.MemberPhone,
					MemberAdress = list.MemberAdress,
					MemberPassord = list.MemberPassord,
				});
				await _context.SaveChangesAsync();
				return "成功";
			}
			catch(Exception ) 
			{
				return "失敗";
			}
		}


	}
}
