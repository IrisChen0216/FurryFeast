using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API
{
    [Route("api/orders/[action]")]
    [ApiController]
    public class OrderListApiController : ControllerBase
    {
        private readonly db_a989fb_furryfeastContext _context;

        public OrderListApiController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        public object GetData()
        {
            return _context.Orders
                .Include(x => x.Member)
                .Include(x => x.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Select(x => new
                {
                    x.Member.MemberName,
                    x.Member.MemberAccount,
                    x.MemberId,
                    x.OrderId,
                    x.OrderCreateDate,
                    x.OrderRecipientAdress,
                    x.OrderRecipientName,
                    x.OrderRecipientPhone,
                    x.OrderShipDate,
                    x.OrderTotalPrice,
                    x.OrderStatus,
                    OrderDetail = x.OrderDetails.Select(y => new
                    {
                        y.OrderId,
                        y.ProductId,
                        y.OrderDetailId,
                        y.OrderPrice,
                        y.OrderQuantity,
                        y.Product.ProductName,
                        y.Product.ProductPrice
                        //y.Product.ProductPics
                    }).ToList()
                }).ToList();
        }

        [HttpGet("{id}")]
        public object GetData(int id)
        {
	        return _context.Orders
		        .Where(x => x.OrderId == id)
		        .Include(x => x.Member)
		        .Include(x => x.OrderDetails)
		        .ThenInclude(od => od.Product)
		        .Select(x => new
		        {
			        x.Member.MemberName,
                    x.Member.MemberAccount,
			        x.MemberId,
			        x.OrderId,
			        x.OrderCreateDate,
			        x.OrderRecipientAdress,
			        x.OrderRecipientName,
			        x.OrderRecipientPhone,
			        x.OrderShipDate,
			        x.OrderTotalPrice,
			        x.OrderStatus,
			        OrderDetail = x.OrderDetails.Select(y => new
			        {
				        y.OrderId,
				        y.ProductId,
				        y.OrderDetailId,
				        y.OrderPrice,
				        y.OrderQuantity,
				        y.Product.ProductName,
				        y.Product.ProductPrice
			        }).ToList()
		        }).ToList();
        }

        [HttpPost]
        public object Update([FromBody]OrdersListViewModel model)
        {
	        try
	        {
		        var editedOrder = _context.Orders.FirstOrDefault(x => x.OrderId == model.OrderId);
		        if (editedOrder == null) return new { success = false, message = "Order not found" };
		        editedOrder.OrderId = model.OrderId;
		        editedOrder.OrderRecipientName = model.OrderRecipientName;
		        editedOrder.OrderRecipientAdress = model.OrderRecipientAdress;
		        editedOrder.OrderRecipientPhone = model.OrderRecipientPhone;

		        _context.Orders.Update(editedOrder);
		        _context.SaveChanges();
		        return new { success = true, message = "Order updated successfully" };
	        }
	        catch (Exception e)
	        {
		        return new { success = false, message = e.Message };
	        }
        }

    }
}
