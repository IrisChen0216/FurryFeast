using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Controllers {

    [Area("Admin")]
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

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

        public IActionResult SignIn() {
            return View();
        }

        public IActionResult SignUp() {
            return View();
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
