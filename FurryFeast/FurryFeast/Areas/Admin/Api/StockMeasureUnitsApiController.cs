using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class StockMeasureUnitsApiController : ControllerBase {
		private db_a989fb_furryfeastContext _context;

		public StockMeasureUnitsApiController(db_a989fb_furryfeastContext context) {
			_context = context;
		}

		// 查詢所有資料
		[HttpGet]
		public async Task<IActionResult> GetAll() {
			if (_context.StockMeasureUnits == null) {
				return NotFound("StockMeasureUnits is null.");
			}

			var result = await _context.StockMeasureUnits.Select(data => new StockMeasureUnitsViewModel {
				MeasureUnitsId = data.MeasureUnitsId,
				MeasureUnitsCode = data.MeasureUnitsCode,
				MeasureUnitsDescription = data.MeasureUnitsDescription,
			}).ToListAsync();
			return Ok(result);
		}

		// 新增一筆資料
		[HttpPost]
		public async Task<IActionResult> PostData([FromBody] StockMeasureUnitsViewModel data) {
			if (_context.StockMeasureUnits == null) {
				return NotFound("StockMeasureUnits is null.");
			}

			// 如果資料重複
			var result = await _context.StockMeasureUnits.Where(d => d.MeasureUnitsCode == data.MeasureUnitsCode).FirstOrDefaultAsync();
			if (result != null) {
				return Conflict($"Data duplicate, MeasureUnitsCode: {data.MeasureUnitsCode}");
			}

			result = new StockMeasureUnit {
				MeasureUnitsId = data.MeasureUnitsId,
				MeasureUnitsCode = data.MeasureUnitsCode,
				MeasureUnitsDescription = data.MeasureUnitsDescription
			};

			_context.StockMeasureUnits.Add(result);
			await _context.SaveChangesAsync();
			return Ok($"Post success, MeasureUnitsCode: {data.MeasureUnitsCode}.");
		}

		// 刪除一筆資料
		[HttpDelete("{code}")]
		public async Task<IActionResult> DeleteData(string code) {
			if (_context.StockMeasureUnits == null) {
				return NotFound("StockMeasureUnits is null");
			}

			// 檢查資料是否存在
			var result = await _context.StockMeasureUnits.Where(d => d.MeasureUnitsCode == code).FirstOrDefaultAsync();
			if (result == null) {
				return BadRequest($"Delete failed, MeasureUnitsCode: {code}");
			}

			// 刪除時的FK錯誤
			try {
				_context.StockMeasureUnits.Remove(result);
				await _context.SaveChangesAsync();
				return Ok($"Delete success, MeasureUnitsCode: {code}.");
			} catch (DbUpdateException ex) {
				return BadRequest(ex.InnerException!.Message);
			}
		}

		// 更新一筆資料
		[HttpPatch]
		public async Task<IActionResult> PatchData([FromBody] StockMeasureUnitsViewModel data) {
			if (_context.StockMeasureUnits == null) {
				return NotFound("StockMeasureUnits is null");
			}

			// 檢查資料是否存在
			var result = await _context.StockMeasureUnits.Where(d => d.MeasureUnitsId == data.MeasureUnitsId).FirstOrDefaultAsync();
			if (result == null) {
				return BadRequest($"Patch failed, MeasureUnitsCode: {data.MeasureUnitsCode}.");
			}

			var patchOneData = await _context.StockMeasureUnits.Where(d => d.MeasureUnitsCode == data.MeasureUnitsCode).FirstOrDefaultAsync();

			// 檢查 code 是否存在, code 是唯一的字串
			// code 存在但不同筆 = 回傳錯誤, code 重複命名
			// code 存在且為同一筆 = 更新這一筆資料
			if (result.MeasureUnitsCode != data.MeasureUnitsCode && patchOneData != null) {
				return BadRequest($"Patch duplicate, MeasureUnitsCode: {data.MeasureUnitsCode}.");
			}

			result.MeasureUnitsCode = data.MeasureUnitsCode;
			result.MeasureUnitsDescription = data.MeasureUnitsDescription;
			_context.Entry(result).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return Ok($"Patch success, MeasureUnitsCode: {data.MeasureUnitsCode}.");
		}
	}
}
