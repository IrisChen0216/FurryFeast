using FurryFeast.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public object GetPetClass()
        {
            var id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            return _context.ClassReservetions.Include(x=>x.PetClass).Where(x => x.MemberId == id).Select(x => new
            {
                x.PetClassId,
                x.ClassReservetionDate,
                x.ClassReservetionId,
                x.ClassReservetionState,
                x.PetClass.PetClassInformation,
                x.PetClass.PetClassDate,
                x.PetClass.PetClassName,
                x.PetClass.PetClassPrice,
 
            });
        }
    }
}
