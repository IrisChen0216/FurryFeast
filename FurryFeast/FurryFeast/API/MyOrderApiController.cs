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
    [HttpGet]
    public IActionResult GetMyOrder()
    {
        return Ok(_context.Orders
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
            }).ToList());
    }

}