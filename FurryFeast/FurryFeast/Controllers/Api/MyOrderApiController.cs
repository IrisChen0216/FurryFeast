using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers.Api
{
    public class MyOrderApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
