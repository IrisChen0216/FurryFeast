using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StockImagesApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockImagesApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<IActionResult> GetAll() {
			if (_context.StockImages == null) {
				return NotFound("StockImages is null.");
			}

			var result = await _context.StockImages.Select(data => new StockImageViewModel {
				ImagesId = data.ImagesId,
				ImagesCode = data.ImagesCode,
				ImagesDescription = data.ImagesDescription,
				ImagesFileCrc = data.ImagesFileCrc,
				ImagesBitmapFile = data.ImagesBitmapFile
			}).ToListAsync();
			return Ok(result);
		}

		// 查詢所有資料 但沒有圖片
		[HttpGet]
		public async Task<IActionResult> GetAllWithoutBitmap() {
			if (_context.StockImages == null) {
				return NotFound("StockImages is null.");
			}

			var result = await _context.StockImages.Select(data => new StockImageWithoutBitmapViewModel {
				ImagesId = data.ImagesId,
				ImagesCode = data.ImagesCode,
				ImagesDescription = data.ImagesDescription,
				ImagesFileCrc = data.ImagesFileCrc
			}).ToListAsync();
			return Ok(result);
		}

		// 新增一筆資料
		[HttpPost]
		public async Task<IActionResult> PostData([FromBody] StockImageViewModel data) {
			if (_context.StockImages == null) {
				return NotFound("StockImages is null.");
			}

			// 如果資料重複
			var result = await _context.StockImages.Where(d => d.ImagesCode == data.ImagesCode).FirstOrDefaultAsync();
			if (result != null) {
				return Conflict($"Data duplicate, ImagesCode: {data.ImagesCode}");
			}

			result = new StockImage {
				ImagesId = data.ImagesId,
				ImagesCode = data.ImagesCode,
				ImagesDescription = data.ImagesDescription,
				ImagesFileCrc = data.ImagesFileCrc,
				ImagesBitmapFile = data.ImagesBitmapFile
			};

			_context.StockImages.Add(result);
			await _context.SaveChangesAsync();
			return Ok($"Post success, ImagesCode: {data.ImagesCode}.");
		}

		// 刪除一筆資料
		[HttpDelete("{code}")]
		public async Task<IActionResult> DeleteData(string code) {
			if (_context.StockImages == null) {
				return NotFound("StockImages is null");
			}

			// 檢查資料是否存在
			var result = await _context.StockImages.Where(d => d.ImagesCode == code).FirstOrDefaultAsync();
			if (result == null) {
				return BadRequest($"Delete failed, ImagesCode: {code}");
			}

			try {
				_context.StockImages.Remove(result);
				await _context.SaveChangesAsync();
				return Ok($"Delete success, ImagesCode: {code}.");
			} catch (DbUpdateException ex) {
				return BadRequest(ex.InnerException!.Message);
			}
		}

		// 更新一筆資料
		[HttpPatch]
		public async Task<IActionResult> PatchData([FromBody] StockImageViewModel data) {
			if (_context.StockImages == null) {
				return NotFound("StockImages is null");
			}

			// 檢查資料是否存在
			var result = await _context.StockImages.Where(d => d.ImagesId == data.ImagesId).FirstOrDefaultAsync();
			if (result == null) {
				return BadRequest($"Patch failed, ImagesCode: {data.ImagesCode}.");
			}

			var patchOneData = await _context.StockImages.Where(d => d.ImagesCode == data.ImagesCode).FirstOrDefaultAsync();

			// 檢查 code 是否存在, code 是唯一的字串
			// code 存在但不同筆 = 回傳錯誤, code 重複命名
			// code 存在且為同一筆 = 更新這一筆資料
			if (result.ImagesCode != data.ImagesCode && patchOneData != null) {
				return BadRequest($"Patch duplicate, ImagesCode: {data.ImagesCode}.");
			}

			result.ImagesCode = data.ImagesCode;
			result.ImagesDescription = data.ImagesDescription;
			result.ImagesFileCrc = data.ImagesFileCrc;
			result.ImagesBitmapFile = data.ImagesBitmapFile;
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return Ok($"Patch success, ImagesCode: {data.ImagesCode}.");
		}
	}
}
