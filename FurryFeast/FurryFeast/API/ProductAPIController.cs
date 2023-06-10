using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FurryFeast.API
{
	[Route("api/products/[action]")]
	[ApiController]


	public class ProductAPIController : ControllerBase
	{
		private readonly db_a989fb_furryfeastContext _context;
		public ProductAPIController(db_a989fb_furryfeastContext context)
		{
			_context = context;
		}

		//寵物商城商品資料
		public object AllProducts()
		{
			return _context.Products.Where(x => x.ProductState == 1).Include(x => x.ProductPics).Include(x => x.ProductType).Select(x => new
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

		//捐贈頁面商品資料
		public object DonateProducts()
		{
			return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Where(x => x.ProductState == 1 && x.ProductTypeId==4).Select(x => new
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

		public object ProductsType(int type)
		{
			return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Where(x => x.ProductState == 1 && x.ProductTypeId == type).Select(x => new
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

		/// <summary>
		/// 給後臺用的商品
		/// </summary>
		/// <returns></returns>
		public object BackendProducts()
		{
			return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Include(x => x.Articles)
				.Select(x => new
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
						productSoldTime = x.ProductSoldTime,
						productState = x.ProductState,
						productArticleId = x.ArticlesId
					},
					//backEndPics = x.ProductPics.Select(p => p.ProductPicImage),
					backEndType = x.ProductType.ProductTypeName,
					backEndArticle = new
					{
						ArticleId = x.ArticlesId,
						ArticleName = x.Articles.ArticlesDescription
					}
				});
		}

		public object BackendProductsType(int type)
		{
			return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Include(x => x.Articles)
				.Where(x => x.ProductTypeId == type).Select(x => new
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
						productSoldTime = x.ProductSoldTime,
						productState = x.ProductState,
						productArticleId = x.ArticlesId
					},
					//backEndPics = x.ProductPics.Select(p => p.ProductPicImage),
					backEndType = x.ProductType.ProductTypeName,
					backEndArticle = new
					{
						ArticleId = x.ArticlesId,
						ArticleName = x.Articles.ArticlesDescription
					}
				});
		}

		public object BackendProductsState(int state)
		{
			return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Include(x => x.Articles)
				.Where(x => x.ProductState == state).Select(x => new
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
						productSoldTime = x.ProductSoldTime,
						productState = x.ProductState,
						productArticleId = x.ArticlesId
					},
					backEndPics = x.ProductPics.Select(p => p.ProductPicImage),
					backEndType = x.ProductType.ProductTypeName,
					backEndArticle = new
					{
						ArticleId = x.ArticlesId,
						ArticleName = x.Articles.ArticlesDescription
					}
				});
		}

		public object BackendArticle()
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

	

		[HttpGet("{id}")] //後台商品詳細資訊
		public async Task<PetMarketBackendViewModel> GetProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			var productPic = _context.ProductPics.Where(p => p.ProductId == id).ToList();
			List<string> Pic = new List<string>();
			List<int> PicID = new List<int>();

			foreach (var pic in productPic)
			{
				string Base64Pic = Convert.ToBase64String(pic.ProductPicImage);
				Pic.Add(Base64Pic);
				PicID.Add(pic.ProductPicId);
			}

			PetMarketBackendViewModel model = new PetMarketBackendViewModel
			{

				ProductId = product.ProductId,
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				ProductPrice = product.ProductPrice,
				ProductAmount = product.ProductAmount,
				ProductTypeId = product.ProductTypeId,
				ProductLaunchedTime = product.ProductLaunchedTime,
				ProductSoldTime = product.ProductSoldTime,
				ProductState = product.ProductState,
				ProductPicImage = Pic,
				ProductPicId=PicID,
				ProductTypeName = product.ProductType.ProductTypeName,
				ArticlesId = product.ArticlesId
			};

			return model;
		}

		[HttpGet("{id}")] //前台商品詳細資訊
		public async Task<ProductDetailViewModel> ProductDetail(int id)
		{
			var product = await _context.Products.FindAsync(id);
			var productPic = _context.ProductPics.Where(p => p.ProductId == id).ToList();
			List<string> Pic = new List<string>();

			foreach (var pic in productPic)
			{
				string Base64Pic = Convert.ToBase64String(pic.ProductPicImage);
				Pic.Add(Base64Pic);
			}

			ProductDetailViewModel model = new ProductDetailViewModel
			{
				ProductId = product.ProductId,
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				ProductPrice = product.ProductPrice,
				ProductAmount = product.ProductAmount,
				ProductTypeId = product.ProductTypeId,
				ProductPicImage = Pic,
				ProductTypeName = product.ProductType.ProductTypeName,
			};

			return model;
		}


		[HttpPut]
		public async Task<object> PutProduct([FromBody] PetMarketViewModel model)
		{

			try
			{
				Product product = _context.Products.Include(x => x.ProductType).Where(x => x.ProductId == model.ProductId).FirstOrDefault();

				product.ProductId = model.ProductId;
				product.ProductName = model.ProductName;
				product.ProductPrice = model.ProductPrice;
				product.ProductAmount = model.ProductAmount;
				product.ProductDescription = model.ProductDescription;
				product.ProductState = model.ProductState;
				product.ProductTypeId = model.ProductTypeId;
				product.ProductType.ProductTypeName = model.ProductTypeName;
				product.ArticlesId = model.ArticlesId;
				//product.ProductPics.ProductPicImage = model.ProductPicImage;
				product.ProductLaunchedTime = model.ProductLaunchedTime;
				product.ProductSoldTime = model.ProductSoldTime;


				_context.Entry(product).State = EntityState.Modified;

				await _context.SaveChangesAsync();
				return Ok("修改商品成功");
			}
			catch (Exception)
			{
				return NotFound("修改商品失敗");
			}
		}

		[HttpPut]
		public async Task<object> PutProductState(int id, int state)
		{


			var product = await _context.Products.FindAsync(id);

			if (state == 0)
			{
				product.ProductSoldTime = DateTime.Now;
			}
			product.ProductState = state;

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
				{
					if (product.ProductState != 0 || product.ProductState != 1)
					{
						return NotFound("修改上架狀態失敗");
					}
				}

				else
				{
					throw;
				}
			}

			return Ok("修改上架狀態成功");
		}

		[HttpPost]
		public async Task<object> PostProduct([FromBody] AddProductViewModel model)
		{
			TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
			try
			{
				_context.Products.Add(new Product
				{
					ProductId = model.ProductId,
					ProductName = model.ProductName,
					ProductPrice = model.ProductPrice,
					ProductAmount = model.ProductAmount,
					ProductDescription = model.ProductDescription,
					ProductState = model.ProductState,
					ProductTypeId = model.ProductTypeId,
					ArticlesId = model.ArticlesId,
					ProductLaunchedTime = model.ProductLaunchedTime
				});
				await _context.SaveChangesAsync();
				return Ok("success");
			}
			catch (Exception)
			{
				return NotFound("新增商品失敗");
			}
		}



		[HttpPost]
		public async Task<object> PostProductImage([FromForm] List<IFormFile> ProductPicImage, [FromForm] int ProductId)
		{
			//List<ProductPic> productPicImages = new List<ProductPic>();

			foreach (var pic in ProductPicImage)
			{
				if (pic != null)
				{

					byte[] data = null;
					using (BinaryReader br = new BinaryReader(pic.OpenReadStream()))
					{

						data = br.ReadBytes((int)pic.Length);
						ProductPic image = new ProductPic();
						image.ProductPicImage = data;
						image.ProductId = ProductId;
						_context.ProductPics.Add(image);
					}

				};

			};

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return NotFound("新增圖片失敗");
			}

			return Ok("OK");
		}

		
		[HttpPost]
		public async Task<object> PutProductImage([FromForm] List<IFormFile> ProductPicImage, [FromForm] List<int> ProductPicId)
		{
			//List<ProductPic> productPicImages = new List<ProductPic>();

			for (int i=0;i<ProductPicImage.Count;i++)
			{
				var pic = ProductPicImage[i];
				var picId = ProductPicId[i];

				if (pic != null)
				{

					byte[] data = null;
					using (BinaryReader br = new BinaryReader(pic.OpenReadStream()))
					{

						data = br.ReadBytes((int)pic.Length);
						var image =await _context.ProductPics.FindAsync(picId);
						if (image != null)
						{
							image.ProductPicImage = data;
						}
						
					}

				};

			};

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Ok("修改圖片失敗");
			}

			return NotFound("修改圖片成功");
		}

		[HttpDelete("{id}")]
		public async Task<object> DeleteProduct(int id)
		{
			try
			{
				var product = await _context.Products.FindAsync(id);
				if (product != null)
				{
					_context.Products.Remove(product);
				}				
				await _context.SaveChangesAsync();
				return Ok("success");
			}
			catch (Exception)
			{
				return NotFound("刪除商品失敗");
			}
		}

		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
		}
	}
}

