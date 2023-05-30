using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Controllers {

    [Area("Admin")]
    public class HomeController : Controller {

		public IActionResult Billing() {
            return View();
        }

        public IActionResult Dashboard() {
            return View();
        }

        public IActionResult Icons() {
            return View();
        }

        public IActionResult Map() {
            return View();
        }

        public IActionResult Notifications() {
            return View();
        }

        public IActionResult Profile() {
            return View();
        }

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

        public IActionResult Tables() {
            return View();
        }

        public IActionResult Template() {
            return View();
        }

        public IActionResult Typography() {
            return View();
        }

        public IActionResult VirtualReality() {
            return View();
        }
    }
}
