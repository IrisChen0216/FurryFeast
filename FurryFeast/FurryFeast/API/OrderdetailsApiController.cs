using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API
{
    [Route("api/orderdetails/[action]")]
    [ApiController]
    public class OrderdetailsApiController : ControllerBase
    {
        private readonly db_a989fb_furryfeastContext _context;

        public OrderdetailsApiController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public object GetOrderDetails(int id)
        {

            return _context.Orders.Where(x => x.OrderId == id).Include(x => x.OrderDetails).
                ThenInclude(x => x.Product).Select(x => new
                {

                    x.OrderId,
                    x.OrderRecipientName,
                    x.OrderRecipientPhone,
                    x.OrderCreateDate,
                    x.OrderShipDate,
                    x.OrderStatus,
                    x.OrderRecipientAdress,
                    OrderDetail = x.OrderDetails.Select(x => new
                    {
                        x.OrderId,
                        x.ProductId,
                        x.Product.ProductName,
                        x.Product.ProductPrice,
                        x.Product.ProductType,
                        x.Order.OrderTotalPrice,
                        x.OrderPrice,
                    })
                });
        }
    }
}
