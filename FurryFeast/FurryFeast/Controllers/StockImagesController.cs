using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;

namespace FurryFeast.Controllers {
	public class StockImagesController : Controller {
		private readonly db_a989fb_furryfeastContext _context;

		public StockImagesController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// GET: StockImages
		public async Task<IActionResult> Index() {
			return _context.StockImages != null ?
						View(await _context.StockImages.ToListAsync()) :
						Problem("Entity set 'db_a989fb_furryfeastContext.StockImages'  is null.");
		}

		// GET: StockImages/Details/5
		public async Task<IActionResult> Details(int? id) {
			if (id == null || _context.StockImages == null) {
				return NotFound();
			}

			var stockImage = await _context.StockImages
				.FirstOrDefaultAsync(m => m.ImagesId == id);
			if (stockImage == null) {
				return NotFound();
			}

			return View(stockImage);
		}

		// GET: StockImages/Create
		public IActionResult Create() {
			return View();
		}

		// POST: StockImages/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ImagesId,ImagesCode,ImagesDescription,ImagesFileCrc,ImagesBitmapFile")] StockImage stockImage) {
			if (ModelState.IsValid) {
				_context.Add(stockImage);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(stockImage);
		}

		// GET: StockImages/Edit/5
		public async Task<IActionResult> Edit(int? id) {
			if (id == null || _context.StockImages == null) {
				return NotFound();
			}

			var stockImage = await _context.StockImages.FindAsync(id);
			if (stockImage == null) {
				return NotFound();
			}
			return View(stockImage);
		}

		// POST: StockImages/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 2048000)]
        public async Task<IActionResult> Edit(int id, [Bind("ImagesId,ImagesCode,ImagesDescription,ImagesFileCrc,ImagesBitmapFile")] StockImage stockImage) {
			if (id != stockImage.ImagesId) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {

					setPicture(stockImage);

					_context.Update(stockImage);
					await _context.SaveChangesAsync();
				} catch (DbUpdateConcurrencyException) {
					if (!StockImageExists(stockImage.ImagesId)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(stockImage);
		}

		// GET: StockImages/Delete/5
		public async Task<IActionResult> Delete(int? id) {
			if (id == null || _context.StockImages == null) {
				return NotFound();
			}

			var stockImage = await _context.StockImages
				.FirstOrDefaultAsync(m => m.ImagesId == id);
			if (stockImage == null) {
				return NotFound();
			}

			return View(stockImage);
		}

		// POST: StockImages/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id) {
			if (_context.StockImages == null) {
				return Problem("Entity set 'db_a989fb_furryfeastContext.StockImages'  is null.");
			}
			var stockImage = await _context.StockImages.FindAsync(id);
			if (stockImage != null) {
				_context.StockImages.Remove(stockImage);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool StockImageExists(int id) {
			return (_context.StockImages?.Any(e => e.ImagesId == id)).GetValueOrDefault();
		}

		public async Task<FileResult> GetPicuture(int id) {
			StockImage StockImage = await _context.StockImages.FindAsync(id);

			// Category? = 有資料才讀取 .Picture, 否則不動作
			byte[] content = StockImage?.ImagesBitmapFile;
			return File(content, "imgae/jpeg");
		}

		private void setPicture(StockImage stockImage) {
			if (Request.Form.Files["ImagesBitmapFile"] != null) {
				byte[] data = null;
				using (BinaryReader br = new BinaryReader(Request.Form.Files["ImagesBitmapFile"].OpenReadStream())) {
					data = br.ReadBytes((int)Request.Form.Files["ImagesBitmapFile"].Length);
					stockImage.ImagesBitmapFile = data;
				}
			} else {

			}
		}
	}
}
