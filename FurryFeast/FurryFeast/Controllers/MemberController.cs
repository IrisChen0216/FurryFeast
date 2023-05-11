using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
