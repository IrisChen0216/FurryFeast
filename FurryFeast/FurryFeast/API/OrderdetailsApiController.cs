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

        [HttpGet()]
        public object GetOrderDetail()
        {
            var myId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);
            return _context.OrderDetails.Include(x => x.Product).Select(x => new
            {
                x.OrderId,
                x.OrderDetailId,
                x.ProductId,
                x.Product,
                x.OrderPrice,
                x.OrderQuantity,

            });
        }
    }
}
