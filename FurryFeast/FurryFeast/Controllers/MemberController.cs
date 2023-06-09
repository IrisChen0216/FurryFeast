﻿
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
using System.Security.Cryptography;

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
		public IActionResult OrderDetail([FromRoute] int id) {

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

			var oriPassword = list.MemberPassord;

			// 加點鹽, 亂數
			var strSalt = GetSalt();
			// SHA-256
			var passWord = GetNewHash(oriPassword, strSalt);

			// DB 的 MemberPhone 是 string, viewModel 送到後端 ModelState 會是 True
			// viewModel 驗證, 出生日期不能大於註冊日
			if (ModelState.IsValid && list.MemberBirthday <= verifyDate) {
				_context.Members.Add(new Member() {
					MemberAccount = list.MemberAccount,
					MemberPassord = passWord,
					Salt = strSalt,
					MemberName = list.MemberName,
					MemberEmail = list.MemberAccount,
					MemberPhone = list.MemberPhone,
					MemberBirthday = list.MemberBirthday,
					MemberGender = list.MemberGender,
					MemberId = list.MemberId,
				});
				_context.SaveChanges();
				var obj = new AesValidationDto(list.MemberAccount, DateTime.Now.AddDays(3));
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

		public async Task<IActionResult> Enable(string code) {

			var str = encrypt.AesDecryptToString(code);
			var obj = JsonSerializer.Deserialize<AesValidationDto>(str);
			if (DateTime.Now > obj.ExpiredDate) {
				return View();
			}
			var user = _context.Members.FirstOrDefault(x => x.MemberAccount == obj.MemberAccount);
			if (user != null) {
				user.Active = true;
				_context.SaveChanges();
			}
			//return Ok($@"code:{code}  str:{str}");
			return RedirectToAction("Index", "Home");
		}

		public IActionResult GetMail() { return View(); }

		[HttpGet]
		public IActionResult Login() {

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel list, [FromQuery] string typeID = null, [FromQuery] string recipeID = null, [FromQuery] int marketID = -1) {

			// 找到登入者的鹽
			var memberLogin = _context.Members.FirstOrDefault(s => s.MemberAccount == list.MemberAccount);
			var salt = memberLogin.Salt;

			var oriPassword = list.MemberPassord;
			var password = GetNewHash(oriPassword, salt);

			var Member = _context.Members.FirstOrDefault(x => x.MemberAccount == list.MemberAccount && x.MemberPassord == password && x.Active==true);

			if (Member == null) {

				ViewBag.Error = "帳號密碼錯誤!請再輸入一次";
				return View("Login");
			}

			var ClaimList = new List<Claim>() {
			new Claim(ClaimTypes.Name, Member.MemberName),
			new Claim("Id",Member.MemberId.ToString()),
			//new Claim(ClaimTypes.Role,"user")
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
			}
			if (marketID == 1) {
				return RedirectToAction("IndexNewOne", "Products");
			}
			if (marketID == 2) {
				return RedirectToAction("PetClassIndexNew", "PetClasses");
			}
			if (marketID == 3) {
				return RedirectToAction("Donate", "Products");
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