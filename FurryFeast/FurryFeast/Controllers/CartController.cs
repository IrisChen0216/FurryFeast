using FurryFeast.Helper;
using FurryFeast.Models;
using FurryFeast.ViewModels;
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
			CartViewModel cartItem = new CartViewModel {
				ProductID = _context.Products.Single(p => p.ProductId == id).ProductId,
				ProductName = _context.Products.Single(p => p.ProductId == id).ProductName,
				Amount = 1,
				Price = _context.Products.Single(p => p.ProductId == id).ProductPrice,
				Subtotal = _context.Products.Single(p => p.ProductId == id).ProductPrice,
            };
		

			if (SessionHelper.GetProductCartSession<List<CartViewModel>>(HttpContext.Session,"cart")==null)
			{
				
				List <CartViewModel> cart = new List <CartViewModel>();
				cart.Add(cartItem);
				SessionHelper.SetProductCartSession(HttpContext.Session,"cart",cart);
			}
			else
			{
				List<CartViewModel> cart = SessionHelper.GetProductCartSession<List<CartViewModel>>(HttpContext.Session, "cart");
				int productIndex = cart.FindIndex(p=>p.ProductID==id);
				if (productIndex >= 0)
				{
					cart[productIndex].Amount += cartItem.Amount;
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

        public async Task<IActionResult> Remove(int? id)
		{
            List<CartViewModel> cart = SessionHelper.GetProductCartSession<List<CartViewModel>>(HttpContext.Session, "cart");

			int productIndex = cart.FindIndex(c => c.ProductID == id);
			cart.RemoveAt(productIndex);

			if(cart.Count <= 0)
			{
				SessionHelper.RemoveProductCartSession(HttpContext.Session,"cart");				
			}
			else
			{
                SessionHelper.SetProductCartSession(HttpContext.Session, "cart", cart);
            }
			return RedirectToAction("ProductCart","Products");
        }

    }
}
