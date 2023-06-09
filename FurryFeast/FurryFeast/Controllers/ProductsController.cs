﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;
using X.PagedList;
using FurryFeast.Helper;
using FurryFeast.ViewModels;
using Microsoft.CodeAnalysis;

namespace FurryFeast.Controllers
{
    public class ProductsController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public ProductsController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: Products //舊版
        public async Task<IActionResult> Index(string type,string searchForm,string file,string sortProducts,int? page)
        {
            IQueryable<Product> products = _context.Products.Where(p => p.ProductState == 1);

			int pageNumber = (page ?? 1);
            int pageSize = 6;          

            ViewBag.NewProducts = sortProducts == "LaunchDate_Desc" ? "LaunchDate_Asc" : "LaunchDate_Desc";
            ViewBag.ProductsPrice = sortProducts == "ProductPrice_Asc" ? "ProductPrice_Desc" : "ProductPrice_Asc";
            
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

        //Vue版
		public async Task<IActionResult> IndexNewOne()
        {
            return View();
        }

		public async Task<IActionResult> Donate()
		{
			return View();
		}
		public async Task<IActionResult> DogProducts()
        {
			IQueryable<Product> products = _context.Products.Where(p => p.ProductState == 1 && p.ProductTypeId==1);
			return View(products);
        }
        public async Task<FileResult> GetPicture(int id)
        {
            Product p = await _context.Products.FindAsync(id);           
            byte[] content = p.ProductPics.First().ProductPicImage;
            return File(content, "image/jpeg");
        }

        public async Task<IActionResult> ProductDetail(int? id)
        {

            var product = await _context.Products
                .Include(p => p.Articles)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            
            return View(product);
        }

        //Vue版
		public async Task<IActionResult> ProductDetailNew(int id)
		{
			ViewBag.productId = id;
			return View();
		}

        //舊版
		public async Task<IActionResult> ProductCart(int? id)
        {
            List<CartItem> cartItems = SessionHelper.GetProductCartSession<List<CartItem>>(HttpContext.Session,"cart");

            if (cartItems != null)
            {
                ViewBag.Total = cartItems.Sum(s=>s.Subtotal);
                
            }
            else { 
                ViewBag.Total = 0; 
            }
          
            return View(cartItems);
        }

		//Vue版
		public async Task<IActionResult> ProductCartNew(int? id)
		{

			return View();
		}


		private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
