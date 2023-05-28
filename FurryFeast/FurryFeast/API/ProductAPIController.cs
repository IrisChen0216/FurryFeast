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

		public object ProductsType(int type)
		{
			return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Where(x => x.ProductState == 1 && x.ProductTypeId==type).Select(x => new
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
				pics = x.ProductPics.Select(p => p.ProductPicImage).FirstOrDefault(),
				type = new
				{
					productypeName = x.ProductType.ProductTypeName,
					productypeId = x.ProductType.ProductTypeId
				}

			});


		}

		//給後臺用的商品
		public object backEndProducts()
        {
						
            return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Include(x=>x.Articles).Select(x => new
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
                    productState=x.ProductState,
					productArticleId=x.ArticlesId
				},
                backEndPics = x.ProductPics.Select(p => p.ProductPicImage),
                backEndType = x.ProductType.ProductTypeName,
				backEndArticle=new
				{
					ArticleId=x.ArticlesId,
					ArticleName=x.Articles.ArticlesDescription
				}

			});


        }

		public object backEndArticle()
		{

			return _context.StockArticles.Select(x => new
			{
				
				backEndArticle = new
				{
					ArticleId = x.ArticlesId,
					ArticleName = x.ArticlesDescription
				}

			});


		}

		[HttpGet("{id}")]
		public async Task<PetMarketViewModel> GetProduct(int id)
		{
			var product =await _context.Products.FindAsync(id);

			PetMarketViewModel model = new PetMarketViewModel
			{
				ProductId = product.ProductId,
				ProductName=product.ProductName,
				ProductDescription=product.ProductDescription,
				ProductPrice=product.ProductPrice,
				ProductAmount=product.ProductAmount,
				ProductTypeId=product.ProductTypeId,
				ProductLaunchedTime=product.ProductLaunchedTime,
				ProductSoldTime=product.ProductSoldTime,
				ProductState=product.ProductState,
				ProductPicImage=product.ProductPics.First().ProductPicImage,
				ProductTypeName=product.ProductType.ProductTypeName,
				ArticlesId=product.ArticlesId
			};

			return model;
		}

		//[HttpPost]
		//public async Task<string> PostProduct([FromBody] PetMarketViewModel model)
		//{
			
		//	Product product = new Product
		//	{
		//		ProductId = model.ProductId,
		//		ProductName = model.ProductName,
		//		ProductPrice = model.ProductPrice,
		//		ProductAmount = model.ProductAmount,
		//		ProductDescription=model.ProductDescription,
		//		ProductState=model.ProductState,
		//		ProductTypeId = model.ProductTypeId,
			
		//		ArticlesId = model.ArticlesId
		//	};
		//	_context.Products.Add(product);
		//	await _context.SaveChangesAsync();

		//	return $"新增成功!{product.ProductId}";
		//}

		[HttpPut]
		public async Task<string> PutProduct([FromBody]PetMarketViewModel model)
		{


			Product product = _context.Products.Include(x=>x.ProductType).Where(x => x.ProductId == model.ProductId).FirstOrDefault();

			product.ProductId = model.ProductId;
			product.ProductName = model.ProductName;
			product.ProductPrice = model.ProductPrice;
			product.ProductAmount = model.ProductAmount;
			product.ProductDescription = model.ProductDescription;
			product.ProductState = model.ProductState;
			product.ProductTypeId = model.ProductTypeId;
			product.ProductType.ProductTypeName = model.ProductTypeName;		
			product.ArticlesId = model.ArticlesId;
			product.ProductPics.First().ProductPicImage = model.ProductPicImage;
			product.ProductLaunchedTime = model.ProductLaunchedTime;
			product.ProductSoldTime = model.ProductSoldTime;

			
			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(model.ProductId))
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

		[HttpPut]
		public async Task<string> PutProductState(int id,int state)
		{


			var product = await _context.Products.FindAsync(id);

			product.ProductState=state;

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
				{
					if (product.ProductState!=0||product.ProductState!=1)
					{
						return "修改上架狀態失敗";
					}
				}
					
				else
				{
					throw;
				}
			}

			return "修改上架狀態成功";
		}

		[HttpPost]
		public async Task<string> PostProduct([FromBody] AddProductViewModel model)
		{

			Product product =new Product();

			product.ProductId = model.ProductId;
			product.ProductName = model.ProductName;
			product.ProductPrice = model.ProductPrice;
			product.ProductAmount = model.ProductAmount;
			product.ProductDescription = model.ProductDescription;
			product.ProductState = model.ProductState;
			product.ProductTypeId = model.ProductTypeId;
			product.ArticlesId = model.ArticlesId;
			product.ProductLaunchedTime = model.ProductLaunchedTime;


			_context.Products.Add(product);

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				
			return "新增商品失敗";

			}

			return "新增商品成功";
		}

		[HttpPost]
		public async Task<string> PostProductImage([FromBody] AddProductImageViewModel model)
		{

			AddProductImageViewModel image = new AddProductImageViewModel();

			image.ProductPicId = model.ProductPicId;
			image.ProductPicImage = model.ProductPicImage;


			_context.Entry(image).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{

				return "新增商品失敗";

			}

			return "新增商品成功";
		}
		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
		}
	}
	
}
