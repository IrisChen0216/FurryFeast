using FurryFeast.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Security.Claims;

namespace FurryFeast.API
{
    [Route("api/member/[action]")]
    [ApiController]
    public class MyClassApiController : ControllerBase
    {
        private readonly db_a989fb_furryfeastContext _context;

        public MyClassApiController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  IActionResult GetMyClass()
        {
          
            var id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            var data= _context.ClassReservetions.Where(x => x.MemberId == id).Select(x => new

            {
                //petClass = new

                a=x.PetClassId,
                x.ClassReservetionDate,
                x.ClassReservetionId,
                x.ClassReservetionState,

                x.PetClass.PetClassInformation,
                x.PetClass.PetClassDate,
                x.PetClass.PetClassName,
                x.PetClass.PetClassPrice,



            }).FirstOrDefault();
            return Ok(data);
        }
    }
}
