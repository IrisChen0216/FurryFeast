using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;

namespace FurryFeast.Controllers
{
    public class StockArticlesController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public StockArticlesController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: StockArticles
        public async Task<IActionResult> Index()
        {
            var db_a989fb_furryfeastContext = _context.StockArticles.Include(s => s.Group).Include(s => s.Images).Include(s => s.MeasureUnit).Include(s => s.Suppliers).Include(s => s.Warehouses);
            return View(await db_a989fb_furryfeastContext.ToListAsync());
        }

        // GET: StockArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockArticles == null)
            {
                return NotFound();
            }

            var stockArticle = await _context.StockArticles
                .Include(s => s.Group)
                .Include(s => s.Images)
                .Include(s => s.MeasureUnit)
                .Include(s => s.Suppliers)
                .Include(s => s.Warehouses)
                .FirstOrDefaultAsync(m => m.ArticlesId == id);
            if (stockArticle == null)
            {
                return NotFound();
            }

            return View(stockArticle);
        }

        // GET: StockArticles/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.StockGroups, "GroupsId", "GroupsId");
            ViewData["ImagesId"] = new SelectList(_context.StockImages, "ImagesId", "ImagesId");
            ViewData["MeasureUnitId"] = new SelectList(_context.StockMeasureUnits, "MeasureUnitsId", "MeasureUnitsId");
            ViewData["SuppliersId"] = new SelectList(_context.StockSuppliers, "SuppliersId", "SuppliersId");
            ViewData["WarehousesId"] = new SelectList(_context.StockWarehouses, "WarehousesId", "WarehousesId");
            return View();
        }

        // POST: StockArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticlesId,AarticlesCode,ArticlesIsValid,ArticlesDescription,ArticlesNotes,ArticlesPrice,ArticlesQuantity,GroupId,MeasureUnitId,WarehousesId,SuppliersId,ImagesId")] StockArticle stockArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.StockGroups, "GroupsId", "GroupsId", stockArticle.GroupId);
            ViewData["ImagesId"] = new SelectList(_context.StockImages, "ImagesId", "ImagesId", stockArticle.ImagesId);
            ViewData["MeasureUnitId"] = new SelectList(_context.StockMeasureUnits, "MeasureUnitsId", "MeasureUnitsId", stockArticle.MeasureUnitId);
            ViewData["SuppliersId"] = new SelectList(_context.StockSuppliers, "SuppliersId", "SuppliersId", stockArticle.SuppliersId);
            ViewData["WarehousesId"] = new SelectList(_context.StockWarehouses, "WarehousesId", "WarehousesId", stockArticle.WarehousesId);
            return View(stockArticle);
        }

        // GET: StockArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockArticles == null)
            {
                return NotFound();
            }

            var stockArticle = await _context.StockArticles.FindAsync(id);
            if (stockArticle == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.StockGroups, "GroupsId", "GroupsId", stockArticle.GroupId);
            ViewData["ImagesId"] = new SelectList(_context.StockImages, "ImagesId", "ImagesId", stockArticle.ImagesId);
            ViewData["MeasureUnitId"] = new SelectList(_context.StockMeasureUnits, "MeasureUnitsId", "MeasureUnitsId", stockArticle.MeasureUnitId);
            ViewData["SuppliersId"] = new SelectList(_context.StockSuppliers, "SuppliersId", "SuppliersId", stockArticle.SuppliersId);
            ViewData["WarehousesId"] = new SelectList(_context.StockWarehouses, "WarehousesId", "WarehousesId", stockArticle.WarehousesId);
            return View(stockArticle);
        }

        // POST: StockArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticlesId,AarticlesCode,ArticlesIsValid,ArticlesDescription,ArticlesNotes,ArticlesPrice,ArticlesQuantity,GroupId,MeasureUnitId,WarehousesId,SuppliersId,ImagesId")] StockArticle stockArticle)
        {
            if (id != stockArticle.ArticlesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockArticleExists(stockArticle.ArticlesId))
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
            ViewData["GroupId"] = new SelectList(_context.StockGroups, "GroupsId", "GroupsId", stockArticle.GroupId);
            ViewData["ImagesId"] = new SelectList(_context.StockImages, "ImagesId", "ImagesId", stockArticle.ImagesId);
            ViewData["MeasureUnitId"] = new SelectList(_context.StockMeasureUnits, "MeasureUnitsId", "MeasureUnitsId", stockArticle.MeasureUnitId);
            ViewData["SuppliersId"] = new SelectList(_context.StockSuppliers, "SuppliersId", "SuppliersId", stockArticle.SuppliersId);
            ViewData["WarehousesId"] = new SelectList(_context.StockWarehouses, "WarehousesId", "WarehousesId", stockArticle.WarehousesId);
            return View(stockArticle);
        }

        // GET: StockArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockArticles == null)
            {
                return NotFound();
            }

            var stockArticle = await _context.StockArticles
                .Include(s => s.Group)
                .Include(s => s.Images)
                .Include(s => s.MeasureUnit)
                .Include(s => s.Suppliers)
                .Include(s => s.Warehouses)
                .FirstOrDefaultAsync(m => m.ArticlesId == id);
            if (stockArticle == null)
            {
                return NotFound();
            }

            return View(stockArticle);
        }

        // POST: StockArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockArticles == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.StockArticles'  is null.");
            }
            var stockArticle = await _context.StockArticles.FindAsync(id);
            if (stockArticle != null)
            {
                _context.StockArticles.Remove(stockArticle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockArticleExists(int id)
        {
          return (_context.StockArticles?.Any(e => e.ArticlesId == id)).GetValueOrDefault();
        }
    }
}
