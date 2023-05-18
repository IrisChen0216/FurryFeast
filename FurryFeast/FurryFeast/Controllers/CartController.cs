using FurryFeast.Helper;
using FurryFeast.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Controllers
{
	public class CartController : Controller
	{
		private readonly db_a989fb_furryfeastContext _context;

		public CartController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> CartAdd(int? id)
		{
			CartItem cartItem = new CartItem {
			ProductID = _context.Products.Single(p => p.ProductId == id).ProductId,
			ProductName = _context.Products.Single(p=>p.ProductId == id).ProductName,
			Amount = 1,
			Subtotal = _context.Products.Single(p => p.ProductId == id).ProductPrice
				};
		

			if (SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session,"cart")==null)
			{
				
				List <CartItem> cart = new List <CartItem>();
				cart.Add(cartItem);
				SessionHelper.SetProductCartSession(HttpContext.Session,"cart",cart);
			}
			else
			{
				List<CartItem> cart = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");
				int productIndex = cart.FindIndex(p=>p.ProductID==id);
				if (productIndex >= 0)
				{
					cart[productIndex].Amount += 1;
					cart[productIndex].Subtotal += cartItem.Subtotal;
				}
				else
				{
					cart.Add(cartItem);
				}
				SessionHelper.SetProductCartSession(HttpContext.Session, "cart", cart);
			}
			
			return NoContent();
		}
	}
}
