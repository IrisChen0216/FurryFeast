using FurryFeast.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FurryFeast.Areas.Admin.Controllers {

    [Area("Admin")]
    public class HomeController : Controller {

        private readonly db_a989fb_furryfeastContext _context;

        public HomeController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Billing() {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Dashboard() {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Icons() {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Map() {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Notifications() {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Profile() {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Rtl() {
            return View();
        }

        // 登入 PartialView
        public IActionResult SignIn() {
            return PartialView();
        }


        // 註冊 PartialView
        public IActionResult SignUp() {
            return PartialView();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Tables() {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Template() {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
        public IActionResult Typography() {
            return View();
        }
        [Authorize(Roles=("Admin"))]
        public IActionResult VirtualReality() {
            return View();
        }

        public IActionResult AdminRegister(AdminResgisterViewModel list)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.AdminAccount == list.AdminAccount && x.AdminPassword == list.AdminPassword);
            if (admin != null)
            {
                ViewBag.Error = "已有帳號存在!";
                return View("Signin");
            }
                _context.Admins.Add(new Models.Admin
                {
                    AdminAccount = list.AdminAccount,
                    AdminPassword = list.AdminPassword,
                    AdminName = list.AdminName,
                });
                _context.SaveChanges();
                return RedirectToAction("Profile","Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.AdminAccount == request.AdminAccount && x.AdminPassword == request.AdminPassword);
            if (admin == null) return View("Signup");

            var ClaimList = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, admin.AdminName),
                new Claim("Id",admin.AdminId.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var ClaimIndentity = new ClaimsIdentity(ClaimList, CookieAuthenticationDefaults.AuthenticationScheme);
            var ClaimPrincipal = new ClaimsPrincipal(ClaimIndentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ClaimPrincipal);
            return Ok("登入成功");
        }
    }
    
}
