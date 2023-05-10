using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
    public class PetFriendlyMapController : Controller
    {
        public IActionResult PetFriendlyMap()
        {
            return View();
        }
    }
}
