using FurryFeast.Helper;
using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
	public class CartOrderController : Controller		
	{
		private readonly db_a989fb_furryfeastContext _context;

		public CartOrderController(db_a989fb_furryfeastContext context) { 
			_context=context;
		}
		public IActionResult CheckOrder()
		{
			if (SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart")==null)
			{
				RedirectToAction("ProductCart", "Products");
			}

			var user= User.Claims.FirstOrDefault(x => x.Type == "id").Value;
			int userID = int.Parse(user);

            var memberOrder = new Order
			{
				MemberId = userID,
				//MemberName = _context.Members.Where(x=>x.MemberId==userID)
				OrderDetails = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart").ToArray(),

			};
			memberOrder.OrderTotalPrice = memberOrder.OrderDetails.Sum(x=>x.OrderPrice);
			return View(memberOrder);
		}
	}
}
