using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;
using X.PagedList;

namespace FurryFeast.Controllers
{
    public class ProductsController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public ProductsController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchForm,string file,string sortProducts,int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;

            ViewBag.NewProducts = sortProducts == "LaunchDate_Desc" ? "LaunchDate_Asc" : "LaunchDate_Desc";
            ViewBag.ProductsPrice = sortProducts == "ProductPrice_Asc" ? "ProductPrice_Desc" : "ProductPrice_Asc";

            IQueryable<Product> products = _context.Products.Where(p=>p.ProductState==1 && p.ProductTypeId==1);
            //if (string.IsNullOrEmpty(searchForm))
            //{
            //    searchForm = file;
            //}
            
            if (!string.IsNullOrEmpty(searchForm))
            {
                products = _context.Products.Where(n => n.ProductName.Contains(searchForm));
                return View(products.ToPagedList());
            }
            //ViewBag.filter= searchForm

            //ViewBag.CurrentSort = sortProducts;
            switch (sortProducts)
            {
                case "LaunchDate_Desc":
                    products = _context.Products.Where(p => p.ProductState == 1 && p.ProductTypeId == 1).OrderByDescending(d => d.ProductLaunchedTime);
                    break;
                case "LaunchDate_Asc":
                    products = _context.Products.Where(p => p.ProductState == 1 && p.ProductTypeId == 1).OrderBy(d => d.ProductLaunchedTime);
                    break;
                case "ProductPrice_Desc":
                    products = _context.Products.Where(p => p.ProductState == 1 && p.ProductTypeId == 1).OrderByDescending(p => p.ProductPrice);
                    break;
                case "ProductPrice_Asc":
                    products = _context.Products.Where(p => p.ProductState == 1 && p.ProductTypeId == 1).OrderBy(p => p.ProductPrice);
                    break;
                default:
                    products = _context.Products.Where(d => d.ProductState == 1 && d.ProductTypeId == 1);
                    break;
            }

            
            return View(products.ToPagedList(pageNumber, pageSize));
            //return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Articles)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<FileResult> GetPicture(int id)
        {
            Product p = await _context.Products.FindAsync(id);           
            byte[] content = p.ProductPics.First().ProductPicImage;
            return File(content, "image/jpeg");
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ArticlesId"] = new SelectList(_context.StockArticles, "ArticlesId", "ArticlesId");
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductTypeId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductAmount,ProductDescription,ProductState,ProductLaunchedTime,ProductSoldTime,ProductTypeId,ArticlesId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticlesId"] = new SelectList(_context.StockArticles, "ArticlesId", "ArticlesId", product.ArticlesId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductTypeId", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ArticlesId"] = new SelectList(_context.StockArticles, "ArticlesId", "ArticlesId", product.ArticlesId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductTypeId", product.ProductTypeId);
            return View(product);
        }

        public async Task<IActionResult> ProductDetail(int? id)
        {

            var product = await _context.Products
                .Include(p => p.Articles)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            
            return View(product);
        }

        public async Task<IActionResult> ProductCart(int? id)
        {

            return View();
        }
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductAmount,ProductDescription,ProductState,ProductLaunchedTime,ProductSoldTime,ProductTypeId,ArticlesId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticlesId"] = new SelectList(_context.StockArticles, "ArticlesId", "ArticlesId", product.ArticlesId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductTypeId", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Articles)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
