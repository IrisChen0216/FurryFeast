using FurryFeast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API;

[Route("api/MyOrder/[Action]")]
[ApiController]
public class MyOrderApiController : ControllerBase
{
    private readonly db_a989fb_furryfeastContext _context;

    public MyOrderApiController(db_a989fb_furryfeastContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public IActionResult GetMyOrder()
    {
        var id = int.Parse(User.Claims.FirstOrDefault(x=>x.Type == "Id").Value);
        var data = _context.Orders.Include(x => x.OrderDetails).Where(x => x.MemberId == id)
			.Select(x => new
            {

                x.OrderId,
                x.OrderCreateDate,
                x.OrderShipDate,
                x.OrderTotalPrice,


            });
        return Ok(data);
    }

}