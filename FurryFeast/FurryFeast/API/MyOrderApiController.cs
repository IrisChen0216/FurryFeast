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

    [Authorize]
    public object GetMyOrder()
    {
        var myId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);

        return _context.Orders.Include(x => x.OrderDetails).Where(x => x.MemberId == myId).Select(x => new
        {
            x.MemberId,
            x.OrderId,
            x.OrderShipDate,
            x.OrderStatus
        });
        //return _context.Orders.Include(x => x.OrderDetails).ThenInclude(x=>x.Product).Where(x => x.MemberId == myId).Select(x => new
        //{
        //    x.OrderId,
        //    x.OrderShipDate,
        //    x.OrderStatus,
        //    x.OrderTotalPrice,
        //    OrderDetail = x.OrderDetails.Select(x => new
        //    {
        //        x.OrderDetailId,
        //        x.Product.ProductName, 
        //        x.Product.ProductPrice,
        //        x.Product.ProductType,
                
        //    })
           
       
        //});
    }

   
    
}