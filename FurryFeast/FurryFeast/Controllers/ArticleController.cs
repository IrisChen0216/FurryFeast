using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Donates()
        {
            return View();
        }
        public IActionResult Shelter()
        {
            return View();
        }
    }
}
