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

		
		public async Task<IActionResult> CartAdd([FromBody]CardAddViewModel model)
		{
			int finalAnount = model.amount == 0 ? 1 : model.amount;
			CartViewModel cartItem = new CartViewModel
			{
				ProductID = _context.Products.Single(p => p.ProductId == model.Id).ProductId,
				ProductName = _context.Products.Single(p => p.ProductId == model.Id).ProductName,
				Amount = finalAnount,
				Price = _context.Products.Single(p => p.ProductId == model.Id).ProductPrice,
				Subtotal = _context.Products.Single(p => p.ProductId == model.Id).ProductPrice,
			};


			if (SessionHelper.GetProductCartSession<List<CartViewModel>>(HttpContext.Session, "cart") == null)
			{

				List<CartViewModel> cart = new List<CartViewModel>();
				cart.Add(cartItem);
				SessionHelper.SetProductCartSession(HttpContext.Session, "cart", cart);
			}
			else
			{
				List<CartViewModel> cart = SessionHelper.GetProductCartSession<List<CartViewModel>>(HttpContext.Session, "cart");
				int productIndex = cart.FindIndex(p => p.ProductID == model.Id);
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

		//public async Task<IActionResult> CartAdd(int? id, int amount)
		//{
		//	int finalAnount = amount == 0 ? 1 : amount;
		//	CartViewModel cartItem = new CartViewModel
		//	{
		//		ProductID = _context.Products.Single(p => p.ProductId == id).ProductId,
		//		ProductName = _context.Products.Single(p => p.ProductId == id).ProductName,
		//		Amount = finalAnount,
		//		Price = _context.Products.Single(p => p.ProductId == id).ProductPrice,
		//		Subtotal = _context.Products.Single(p => p.ProductId == id).ProductPrice,
		//	};


		//	if (SessionHelper.GetProductCartSession<List<CartViewModel>>(HttpContext.Session, "cart") == null)
		//	{

		//		List<CartViewModel> cart = new List<CartViewModel>();
		//		cart.Add(cartItem);
		//		SessionHelper.SetProductCartSession(HttpContext.Session, "cart", cart);
		//	}
		//	else
		//	{
		//		List<CartViewModel> cart = SessionHelper.GetProductCartSession<List<CartViewModel>>(HttpContext.Session, "cart");
		//		int productIndex = cart.FindIndex(p => p.ProductID == id);
		//		if (productIndex >= 0)
		//		{
		//			cart[productIndex].Amount += cartItem.Amount;
		//			cart[productIndex].Subtotal += cartItem.Subtotal;
		//		}
		//		else
		//		{
		//			cart.Add(cartItem);
		//		}
		//		SessionHelper.SetProductCartSession(HttpContext.Session, "cart", cart);
		//	}

		//	return NoContent();
		//}

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

        public async Task<IActionResult> CartUpdate([FromBody] CardAddViewModel model)
        {
            
            List<CartViewModel> cart = SessionHelper.GetProductCartSession<List<CartViewModel>>(HttpContext.Session, "cart");

            int productIndex = cart.FindIndex(c => c.ProductID == model.Id);
			cart[productIndex].Amount = model.amount;
            cart[productIndex].Subtotal = (model.amount * cart[productIndex].Price);
			
            if (cart.Count <= 0)
            {
                SessionHelper.RemoveProductCartSession(HttpContext.Session, "cart");
            }
            else
            {
                SessionHelper.SetProductCartSession(HttpContext.Session, "cart", cart);
            }

            return NoContent();
        }
    }
}
