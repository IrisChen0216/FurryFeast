using FurryFeast.Helper;
using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.API
{
    [Route("api/cart/[action]")]
    [ApiController]
    public class CartApiController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;
        public CartApiController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        public object CartProducts()
        {
            List<CartItem> cartItems = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");
            return cartItems;
        }
    }
}
