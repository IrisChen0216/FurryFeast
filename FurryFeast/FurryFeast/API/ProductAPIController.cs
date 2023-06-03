using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API;

[Route("api/products/[action]")]
[ApiController]
public class ProductApiController : ControllerBase
{
    private readonly db_a989fb_furryfeastContext _context;

    public ProductApiController(db_a989fb_furryfeastContext context)
    {
        _context = context;
    }

    public object AllProducts()
    {
        return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType).Where(x => x.ProductState == 1)
            .Select(x => new
            {
                product = new
                {
                    productId = x.ProductId,
                    productName = x.ProductName,
                    productDescription = x.ProductDescription,
                    productPrice = x.ProductPrice,
                    productAmount = x.ProductAmount,
                    productPicId = x.ProductPicId,
                    productLauchedTime = x.ProductLaunchedTime
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
        return _context.Products.Include(x => x.ProductPics).Include(x => x.ProductType)
            .Where(x => x.ProductState == 1 && x.ProductTypeId == type).Select(x => new
            {
                product = new
                {
                    productId = x.ProductId,
                    productName = x.ProductName,
                    productDescription = x.ProductDescription,
                    productPrice = x.ProductPrice,
                    productAmount = x.ProductAmount,
                    productPicId = x.ProductPicId,
                    productLauchedTime = x.ProductLaunchedTime
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
                backEndPics = x.ProductPics.Select(p => p.ProductPicImage),
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
                backEndPics = x.ProductPics.Select(p => p.ProductPicImage),
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

    [HttpGet("{id}")]
    public async Task<PetMarketViewModel> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        var model = new PetMarketViewModel
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
            ProductPicImage = product.ProductPics.First().ProductPicImage,
            ProductTypeName = product.ProductType.ProductTypeName,
            ArticlesId = product.ArticlesId
        };

        return model;
    }

    [HttpPut]
    public async Task<string> PutProduct([FromBody] PetMarketViewModel model)
    {
       

        try
        {
            var product = _context.Products.Include(x => x.ProductType)
                .FirstOrDefault(x => x.ProductId == model.ProductId);

            if (product == null)
            {
                return "修改商品失敗";
            }

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

            await _context.SaveChangesAsync();
            return "修改商品成功";
        }
        catch (Exception)
        {
            return "修改商品失敗";
        }
    }

    [HttpPut]
    public async Task<string> PutProductState(int id, int state)
    {
        try
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return "修改上架狀態失敗";
            product.ProductState = state;
            await _context.SaveChangesAsync();
            return "修改上架狀態成功";
        }
        catch (Exception)
        {
            return "修改上架狀態失敗";
        }
    }

    [HttpPost]
    public async Task<string> PostProduct([FromBody] AddProductViewModel model)
    {
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
            return "新增商品成功";
        }
        catch (Exception)
        {
            return "新增商品失敗";
        }
    }

    /// <summary>
    /// 有問題的程式碼
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> PostProductImage([FromBody] AddProductImageViewModel model)
    {
        var image = new AddProductImageViewModel
        {
            ProductPicId = model.ProductPicId,
            ProductPicImage = model.ProductPicImage
        };
        //todo 不能這樣寫
        _context.Entry(image).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return "新增圖片失敗";
        }

        return "新增圖片成功";
    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteProduct(int id)
    {
        try
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return "刪除商品失敗";
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return "刪除商品成功!";
        }
        catch (Exception)
        {
            return "刪除商品失敗";
        }
    }
}