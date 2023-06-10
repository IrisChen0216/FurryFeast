using FurryFeast.Helper;
using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;

namespace FurryFeast.Controllers
{
    public class CartController : Controller
	{
		private readonly db_a989fb_furryfeastContext _context;

		public CartController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}

		public string GetUserId()
		{
			return User.FindFirstValue("Id");
		}

		[HttpPost]
		public async Task<IActionResult> CartAdd([FromBody]CardAddViewModel model)
		{
			if( User.Claims.FirstOrDefault(x => x.Type == "Id") == null){
				var url = @"\Products\ProductCartNew";
				return NotFound(url);
			}

			//int userID= int.Parse(GetUserId());

			//if (myId == 0)
			//{
			//	return RedirectToAction("Login", "Member");
			//}
			int finalAnount = model.amount == 0 ? 1 : model.amount;
			var productPic = _context.ProductPics.FirstOrDefault(p => p.ProductId == model.Id).ProductPicImage;
			string productImageBase64 = Convert.ToBase64String(productPic);
			CartItem cartItem = new CartItem
			{
				ProductId = _context.Products.FirstOrDefault(p => p.ProductId == model.Id).ProductId,
				ProductName = _context.Products.FirstOrDefault(p => p.ProductId == model.Id).ProductName,
				OrderQuantity = finalAnount,
				OrderPrice = _context.Products.FirstOrDefault(p => p.ProductId == model.Id).ProductPrice,
				Subtotal = _context.Products.FirstOrDefault(p => p.ProductId == model.Id).ProductPrice,
				ProductImage = productImageBase64
			};


			if (SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart") == null)
			{

				List<CartItem> cart = new List<CartItem>();
				cart.Add(cartItem);
				SessionHelper.SetProductCartSession(HttpContext.Session, "cart", cart);
			}
			else
			{
				List<CartItem> cart = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");
				int productIndex = cart.FindIndex(p => p.ProductId == model.Id);
				if (productIndex >= 0)
				{
					cart[productIndex].OrderQuantity += cartItem.OrderQuantity;
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

		[HttpDelete]
		public async Task<List<CartItem>> Remove(int? id)
		{
            List<CartItem> cart = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");

			int productIndex = cart.FindIndex(c => c.ProductId == id);
			cart.RemoveAt(productIndex);

			if(cart.Count <= 0)
			{
				SessionHelper.RemoveProductCartSession(HttpContext.Session,"cart");				
			}
			else
			{
                SessionHelper.SetProductCartSession(HttpContext.Session, "cart", cart);
            }
			List<CartItem> newcart = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");
			return newcart;
			

		}

        public async Task<IActionResult> CartUpdate([FromBody] CardAddViewModel model)
        {
            
            List<CartItem> cart = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");

            int productIndex = cart.FindIndex(c => c.ProductId == model.Id);
			cart[productIndex].OrderQuantity = model.amount;
            cart[productIndex].Subtotal = (model.amount * cart[productIndex].OrderPrice);
			
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
