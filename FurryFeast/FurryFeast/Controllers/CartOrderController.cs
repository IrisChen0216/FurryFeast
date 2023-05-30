using FurryFeast.API;
using FurryFeast.Helper;
using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurryFeast.Controllers
{
	public class CartOrderController : Controller		
	{
		private readonly db_a989fb_furryfeastContext _context;

		public CartOrderController(db_a989fb_furryfeastContext context) { 
			_context=context;
		}

        public string GetUserId()
        {
            return User.FindFirstValue("Id");
        }

        public IActionResult CheckOrder()
		{
			if (SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart")==null)
			{
				RedirectToAction("ProductCart", "Products");
			}

			
			int userID=int.Parse(GetUserId());
            //補抓空值
            var person = _context.Members.FirstOrDefault(x => x.MemberId == userID);
			List<CartItem> cartItems = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");
            int Total=cartItems.Sum(x => x.Subtotal);
			var memberOrder = _context.Orders.Include(x => x.Member).Where(x => x.MemberId == userID).Select(x => new CartOrderViewModel
            {
                Order = new Order
                {
                    MemberId = userID,
                    OrderDetails = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart").ToArray(),
                    OrderTotalPrice = Total
				},
                Member =new Member
                {
                    MemberName = x.Member.MemberName,
                    MemberEmail = x.Member.MemberEmail,
                    MemberPhone = x.Member.MemberPhone,
                    
                }
                
            }).FirstOrDefault();
            //         var memberOrder = new Order
            //{
            //	MemberId = person.MemberId,
            //	//MemberName = _context.Members.Where(x=>x.MemberId==userID)
            //	OrderDetails = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart").ToArray(),


            //};


            memberOrder.Order.OrderTotalPrice = memberOrder.Order.OrderDetails.Sum(x=>x.OrderPrice);
			ViewBag.CartItem = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");
            
            return View(memberOrder);
		}

        public async Task<IActionResult> AddOrder(AddOrderOrderDetailsViewModel model)
        {
            Order order = new Order
            {
                OrderCreateDate = DateTime.Now,
                OrderShipDate= DateTime.Now.AddDays(3),
				OrderRecipientName=model.OrderRecipientName,
                OrderRecipientPhone=model.OrderRecipientPhone,
                OrderTotalPrice=model.OrderTotalPrice,
                OrderRecipientAdress=model.OrderRecipientAdress,
                OrderStatus=0,
                MemberId=model.MemberId,
			};

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			List<CartItem> cartItems = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart");

            foreach(CartItem item in cartItems)
            {
				OrderDetail orderDetail = new OrderDetail
				{
                    OrderPrice = item.OrderPrice,
                    OrderQuantity= item.OrderQuantity,
                    ProductId= item.ProductId,
                    OrderId = order.OrderId,
                };
				_context.OrderDetails.Add(orderDetail);
			};
			await _context.SaveChangesAsync();

			//if (ModelState.IsValid)
			//	{
					//order.OrderCreateDate = DateTime.Now;
					//order.OrderShipDate = DateTime.Now.AddDays(3);
					//order.OrderDetails = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session, "cart").ToArray();
					//order.OrderStatus = 0;

				//	_context.Add(order);
				//	await _context.SaveChangesAsync();
				//	return RedirectToAction("Index");
				//}
			
			return View();
        }
	}
}
