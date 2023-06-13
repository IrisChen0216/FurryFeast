using FurryFeast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FurryFeast.API;

//[Authorize]
[Route("/api/Member/[action]")]
[ApiController]
public class MemberApiController : ControllerBase {
	private readonly db_a989fb_furryfeastContext _context;



	public MemberApiController(db_a989fb_furryfeastContext context) {
		_context = context;
	}


	public object One() {
		var id = User.FindFirstValue("Id");
		return _context.Members.Where(x => x.MemberId == int.Parse(id)).Select(x => new {
			Updatemember = new {
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
	public async Task<IActionResult> Edit([FromBody] MemberEditDto list) {
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
	public object EditPassord([FromBody] PassordEditDto item) {
		var id = User.FindFirstValue("Id");
		var result = _context.Members.Where(x => x.MemberId == int.Parse(id)).FirstOrDefault();

		// 找到登入者的鹽
		var salt = result.Salt;

		var oriPassword = item.checkPassord;

		var passWord = GetNewHash(oriPassword, salt);

		try {
			if (result.MemberPassord == passWord) {

				var newiPassword = item.newPassord;

				// 加點鹽, 亂數
				var strSalt = GetSalt();
				// SHA-256
				var newHashCode = GetNewHash(newiPassword, strSalt);

				result.Salt = strSalt;
				result.MemberPassord = newHashCode;

				_context.SaveChanges();
				return Ok("修改密碼成功");
			} else {
				return BadRequest("修改密碼失敗");
			}
		} catch (Exception) {
			return BadRequest("bad");
		}
	}

	// 會員密碼跟鹽尬再一起雜湊
	private string GetNewHash(string oriPassword, string strSalt) {
		// 先把密碼跟鹽尬再一起
		string messageString = strSalt + oriPassword;

		// 轉成 byte
		byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);

		// 雜湊 SHA 256
		byte[] hashValue = SHA256.HashData(messageBytes);

		// 轉回字串
		string passWord = Convert.ToHexString(hashValue);

		return passWord;
	}

	// 加點鹽, 亂數
	public string GetSalt() {
		var byte16 = new byte[16];
		// 亂數產生器
		var rng = new RNGCryptoServiceProvider();
		rng.GetBytes(byte16);
		string strSalt = Convert.ToBase64String(byte16);
		return strSalt;
	}
}



