using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FurryFeast.API
{
    [Route("api/MyOrder/[Action]")]
    [ApiController]
    public class MyOrderApiController : ControllerBase
    {
        private readonly db_a989fb_furryfeastContext _context;
        public MyOrderApiController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        [Authorize]
        public Object GetMyOrder()
        {
            var myId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            return _context.Orders.Include(x=>x.OrderDetails).Where(x => x.MemberId == myId).Select(x => new
            {
                x.OrderId,
                x.OrderShipDate,
                x.OrderStatus,
                x.OrderTotalPrice,
                
            }).ToList();
        }
    }
}