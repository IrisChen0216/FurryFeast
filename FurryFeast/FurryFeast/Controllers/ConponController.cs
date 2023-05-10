using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
    public class ConponController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
