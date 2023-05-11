using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
