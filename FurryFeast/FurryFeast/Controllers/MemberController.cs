
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;
using FurryFeast.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using System.Net.Mail;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Net;
using System.Text.Json;
using FurryFeast.Services;
using System.Web;

namespace FurryFeast.Controllers {
	public class MemberController : Controller {
		private readonly db_a989fb_furryfeastContext _context;
		private readonly EncryptService encrypt;

		public MemberController(db_a989fb_furryfeastContext context, EncryptService encrypt) {
			_context = context;
			this.encrypt = encrypt;
		}

		// GET: Members

		[Authorize]
		public async Task<IActionResult> MemberIndex() {
			if (User.Identity.IsAuthenticated) {
				var id = User.FindFirstValue("Id");
				var member = _context.Members.Include(m => m.Conpon).Where(m => m.MemberId == int.Parse(id)).FirstOrDefault();
				return View(member);
			}
			return ViewBag.Error("錯");
		}

		[Authorize]

		public IActionResult MemberAfter() {
			return View();
		}

		[Authorize]

		public IActionResult MyOrder() {
			return View();
		}

		[Authorize]
		public IActionResult OrderDetail([FromRoute]int id)
		{
			
			return View(id);
		}

		[Authorize]
		public async Task<IActionResult> MyClass() {
			return View();
		}
		[Authorize]
		public async Task<IActionResult> MyConpon() {
			return View();
		}

		public IActionResult RegisterIndex() {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RegisterIndex(RegisterViewModel list) {
			var Member = _context.Members.FirstOrDefault(x => x.MemberAccount == list.MemberAccount);
			if (Member != null) {
				ViewBag.Error = "已有帳號存在!";
				return View("Login");
			}

			var verifyDate = DateTime.Now;

			// DB 的 MemberPhone 是 string, viewModel 送到後端 ModelState 會是 True
			// viewModel 驗證, 出生日期不能大於註冊日
			if (ModelState.IsValid && list.MemberBirthday <= verifyDate) {
				_context.Members.Add(new Member() {
					MemberAccount = list.MemberAccount,
					MemberPassord = list.MemberPassord,
					MemberName = list.MemberName,
					MemberEmail = list.MemberAccount,
					MemberPhone = list.MemberPhone,
					MemberBirthday = list.MemberBirthday,
					MemberGender = list.MemberGender,
					MemberId = list.MemberId,
				});
				_context.SaveChanges();
				var obj = new AesValidationDto(list.MemberPhone, DateTime.Now.AddDays(3));
				var jstring = JsonSerializer.Serialize(obj);
				var code = encrypt.AesEncryptToBase64(jstring);
				string encodedStr = HttpUtility.UrlEncode(code);


				var Mail = new MailMessage() {
					From = new MailAddress("thm101777@gmail.com"),
					Subject = "FurryFeast驗證信",
					Body = @$"歡迎加入FurryFeast，請點擊<a href='https://localhost:7110/Member/Enable?code={encodedStr}'>這裡</a>以啟用你的帳號",
					IsBodyHtml = true,
					BodyEncoding = Encoding.UTF8
				};

				Mail.To.Add(new MailAddress(list.MemberAccount));

				using (var sm = new SmtpClient("smtp.gmail.com", 587)) {
					sm.EnableSsl = true;
					sm.Credentials = new NetworkCredential("thm101777@gmail.com", "krzjbxvibrueypdy");
					sm.Send(Mail);
				}
				return RedirectToAction("GetMail", "Member");
			}
			ViewBag.regFail = "註冊資料錯誤!";
			return View();
		}

		public async Task<IActionResult> Enable(string code) {

			var str = encrypt.AesDecryptToString(code);
			var obj = JsonSerializer.Deserialize<AesValidationDto>(str);
			if (DateTime.Now > obj.ExpiredDate) {
				return BadRequest("過期");
			}
			var user = _context.Members.FirstOrDefault(x => x.MemberAccount == obj.MemberAccount);
			if (user != null) {
				_context.SaveChanges();
			}
			return Ok($@"code:{code}  str:{str}");
		}

		public IActionResult GetMail() { return View(); }
		public IActionResult Login() {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel list, [FromQuery] string typeID = null, [FromQuery] string recipeID = null) {
			var Member = _context.Members.FirstOrDefault(x => x.MemberAccount == list.MemberAccount && x.MemberPassord == list.MemberPassord);

			if (Member == null) {

                ViewBag.Error = "帳號密碼錯誤";
                return View("Login");
            }

			

			var ClaimList = new List<Claim>() {
			new Claim(ClaimTypes.Name, Member.MemberName),
			new Claim("Id",Member.MemberId.ToString())
		};

			var ClaimIndentity = new ClaimsIdentity(ClaimList, CookieAuthenticationDefaults.AuthenticationScheme);
			var ClaimPrincipal = new ClaimsPrincipal(ClaimIndentity);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ClaimPrincipal);
			int typeIdInt, recipeIdInt;

			if (typeID != null && recipeID != null) {
				if (int.TryParse(typeID, out typeIdInt) && int.TryParse(recipeID, out recipeIdInt)) {
					return RedirectToAction("Recipes", "Recipes", new { typeID = typeIdInt, recipeID = recipeIdInt });
				} else {
					return View();

				}
			} else {
				
				return RedirectToAction("Index", "Home");

			}
			

		}

		public IActionResult GoogleLogin() {
			var prop = new AuthenticationProperties {
				RedirectUri = Url.Action("GoogleResponse")
			};
			return Challenge(prop, GoogleDefaults.AuthenticationScheme);
		}

		public async Task<IActionResult> GoogleResponse() {
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			if (result.Succeeded) {
				var claims = result.Principal.Claims.Select(x => new {
					x.Type,
					x.Value
				});
				return RedirectToAction("Index", "Home");
			}
			return Ok();
		}

		public IActionResult Facebook() {
			var prop = new AuthenticationProperties {
				RedirectUri = Url.Action("FacebookResponse")
			};
			return Challenge(prop, FacebookDefaults.AuthenticationScheme);
		}

		public async Task<IActionResult> FacebookResponse() {
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			if (result.Succeeded) {
				var claims = result.Principal.Claims.Select(x => new {
					x.Type,
					x.Value,
				});
				return RedirectToAction("Index", "Home");
			}
			return Ok();
		}

		public async Task<IActionResult> Logout() {
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public async Task<IActionResult> UpdateMemberData() {
			//var id = User.FindFirstValue("Id");
			//var id = int.Parse(User.Claims.FirstOrDefault(x=>x.Type == "Id").Value);
			//var member = _context.Members.Where(x => x.MemberId == int.Parse(id)).FirstOrDefault();
			//return View(member);
			return View();
		}

		public async Task<IActionResult> ForgetPassword() {
			return View();
		}
		private bool MemberExists(int id) {
			return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
		}
	}


}