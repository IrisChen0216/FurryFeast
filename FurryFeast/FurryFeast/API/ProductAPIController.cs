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
				pics = x.ProductPics.Select(p=>p.ProductPicImage).FirstOrDefault(),
				type = new
				{
					productypeName=x.ProductType.ProductTypeName,
					productypeId= x.ProductType.ProductTypeId
				}

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

		[HttpPost]
		public async Task<string> PostProduct([FromBody] PetMarketViewModel model)
		{
			
			Product product = new Product
			{
				ProductId = model.ProductId,
				ProductName = model.ProductName,
				ProductPrice = model.ProductPrice,
				ProductAmount = model.ProductAmount,
				ProductDescription=model.ProductDescription,
				ProductState=model.ProductState,
				ProductTypeId = model.ProductTypeId,
				ProductPicId = model.ProductPicId,
				ArticlesId = model.ArticlesId
			};
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return $"新增成功!{product.ProductId}";
		}

		[HttpPut("{id}")]
		public async Task<string> PutProduct(int id, [FromBody]PetMarketViewModel model)
		{


			Product product = await _context.Products.FindAsync(model.ProductId);

			product.ProductId = model.ProductId;
			product.ProductName = model.ProductName;
			product.ProductPrice = model.ProductPrice;
			product.ProductAmount = model.ProductAmount;
			//ProductDescription = model.ProductDescription,
			//ProductState = model.ProductState,
			//ProductTypeId = model.ProductTypeId,
			//ProductPicId = model.ProductPicId,
			//ArticlesId = model.ArticlesId
			//待補
			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
				{
					return "修改商品失敗";
				}
				else
				{
					throw;
				}
			}

			return "修改商品成功";
		}
		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
		}
	}
	
}
