using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public object One()
        {
            var id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            return _context.PetClasses.Select(x => new
            {
                x.PetClassDate,
                x.PetClassPrice,
                x.PetClassInformation,
                x.PetClassName,
                x.PetClassType,
                x.PetClassId,
            });
        }
    }
}
