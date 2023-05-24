using FurryFeast.Helper;
using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipes;

namespace FurryFeast.API
{
	[Route("api/products/[action]")]
	[ApiController]

	
	public class ProductAPIController : ControllerBase
	{
		private readonly db_a989fb_furryfeastContext _context;
		public ProductAPIController(db_a989fb_furryfeastContext context)
		{
			_context=context;
		}
		
		public object AllProducts()
		{
			return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Where(x=>x.ProductState==1).Select(x => new
			{
				product = new
				{
					productId = x.ProductId,
					productName = x.ProductName,
					productDescription = x.ProductDescription,
					productPrice = x.ProductPrice,
					productAmount = x.ProductAmount,
					productPicId = x.ProductPicId,
					productLauchedTime = x.ProductLaunchedTime,
					

				},
				pics = x.ProductPics.Select(p=>p.ProductPicImage),
				type = x.ProductType.ProductTypeName

			});
			

		}

		//給後臺用的商品
        public object backEndProducts()
        {
						
            return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Select(x => new
            {
                backEndProduct = new
                {
                    productId = x.ProductId,
                    productName = x.ProductName,
                    productDescription = x.ProductDescription,
                    productPrice = x.ProductPrice,
                    productAmount = x.ProductAmount,
                    productPicId = x.ProductPicId,
                    productLauchedTime = x.ProductLaunchedTime,
                    productSoldTime=x.ProductSoldTime,
                    productState=x.ProductState

                },
                backEndPics = x.ProductPics.Select(p => p.ProductPicImage),
                backEndType = x.ProductType.ProductTypeName

            });


        }
    }
}
