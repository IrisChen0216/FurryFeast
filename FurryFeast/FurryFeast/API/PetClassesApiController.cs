using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API
{
	[Route("api/petclasses/[action]")]
	[ApiController]
	public class PetClassesApiController : ControllerBase
	{
		private readonly db_a989fb_furryfeastContext _context;

		public PetClassesApiController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}

		
		public object AllClasses()
		{
			return _context.PetClasses.Include(x => x.PetClassPics).Include(x=>x.PetClassType).Select(x => new {

				petClasses = new
				{
					petClassId = x.PetClassId,
					petClassName = x.PetClassName,
					petClassPrice=x.PetClassPrice,
                    petClassInformation=x.PetClassInformation,
                    petClassDate=x.PetClassDate
                },
				petClassPic = x.PetClassPics.Select(x => x.PetClassPicImage).FirstOrDefault(),
				petClassType = new
				{
					petClassTypeName = x.PetClassType.PetClassTypeName,
					petClassTypeId = x.PetClassTypeId
				}

			});

			

        }
        public object PetClassType(int type)
        {
            return _context.PetClasses.Include(x => x.PetClassPics).Include(x => x.PetClassType).Where(x=>x.PetClassTypeId==type).Select(x => new {

                petClasses = new
                {
                    petClassId = x.PetClassId,
                    petClassName = x.PetClassName,
                    petClassPrice = x.PetClassPrice,
                    petClassInformation = x.PetClassInformation,
                    petClassDate = x.PetClassDate
                },
                petClassPic = x.PetClassPics.Select(x => x.PetClassPicImage).FirstOrDefault(),
                petClassType = new
                {
                    petClassTypeName = x.PetClassType.PetClassTypeName,
                    petClassTypeId = x.PetClassTypeId
                }

            });
        }
    }
}
