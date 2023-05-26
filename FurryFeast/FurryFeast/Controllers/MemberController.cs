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

namespace FurryFeast.Controllers
{
    public class MemberController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public MemberController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: Members
<<<<<<< HEAD
   
        public async Task<IActionResult> MemberIndex(MemberViewModel list)

		{
            if (int.TryParse(User.FindFirstValue("Id"), out int memberId))
            {
                var member = _context.Members
                    .Include(m => m.Conpon)
                    .FirstOrDefault(m => m.MemberId == memberId);

                return View(member);
            }

            return NotFound();

            //string id = User.FindFirstValue("Id");
            //var member = _context.Members.Include(m => m.Conpon).Where(m=>m.MemberId == id).FirstOrDefault();
            //return View(member);

        }


=======
        [Authorize]
        public  IActionResult MemberIndex()

		{
            if (User.Identity.IsAuthenticated)
            {
                var s = User.FindFirstValue("Id");
                var member = _context.Members.Include(m => m.Conpon).Where(m => m.MemberId == int.Parse(s)).FirstOrDefault();
                return View(member);
            }

            return ViewBag.Error("錯");

        }

        [Authorize]
        [HttpGet]
        public IActionResult MemberAfter()
        {
            var a = _context.Members.FirstOrDefault();
            return View();
        }
        
>>>>>>> MemberData3

        [Authorize]
        [HttpGet]
        public IActionResult MyOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                var s = User.FindFirstValue("Id");
                var order = _context.Orders.Include(x=>x.OrderDetails).Where(x=>x.MemberId == int.Parse(s)).FirstOrDefault();
                return View(order);
            }
            return ViewBag.Error("錯啦");
        }

        [Authorize]
        public async Task<IActionResult> MyClass()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> MyConpon()
        {
            return View();
        }

        public IActionResult RegisterIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterIndex(RegisterViewModel list)
        {
            var Member = _context.Members.FirstOrDefault(x => x.MemberAccount == list.MemberAccount
                        && x.MemberPassord == list.MemberPassord );
            if (Member != null)
            {
                ViewBag.Error = "已有帳號存在!";
                return View("Login");
            }
            _context.Members.Add(new Member()
            {
                MemberAccount = list.MemberAccount,
                MemberPassord = list.MemberPassord,
                MemberAdress = list.MemberAdress,
                MemberName = list.MemberName,
                MemberEmail = list.MemberEmail,
                MemberPhone = list.MemberPhone,
                MemberBirthday = list.MemberBirthday,
                MemberGender = list.MemberGender,
                MemberId = list.MemberId
               
            });
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel list)

        {
            var Member = _context.Members.FirstOrDefault(x => x.MemberAccount == list.MemberAccount && x.MemberPassord == list.MemberPassord);

            if (Member == null)
            {
                ViewBag.Error = "帳號密碼錯誤!";
                return View("Login");
            }
            //return Ok(model.MemberAccount + model.MemberPassord);
            var ClaimList=new List<Claim>() {
			new Claim(ClaimTypes.Name, Member.MemberName),
            new Claim("Id",Member.MemberId.ToString())
		};
         

            var ClaimIndentity = new ClaimsIdentity(ClaimList, CookieAuthenticationDefaults.AuthenticationScheme);
            var ClaimPrincipal = new ClaimsPrincipal(ClaimIndentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,ClaimPrincipal);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Google()
        {

            string? formCredential = Request.Form["credential"]; //回傳憑證
            string? formToken = Request.Form["g_csrf_token"]; //回傳令牌
            string? cookiesToken = Request.Cookies["g_csrf_token"]; //Cookie 令牌



            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> UpdateMemberData()
        {
            return View();
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }

	
}
