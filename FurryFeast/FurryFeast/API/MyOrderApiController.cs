using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public Object GetMyOrder(MyOrderViewModel list)
        {

            return _context.Orders.Select(x=>x.MemberId ==  list.MemberId).ToList();
        }
    }
}
