using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockSuppliersGroupsApiController : ControllerBase {
        private db_a989fb_furryfeastContext _context;

        public StockSuppliersGroupsApiController(db_a989fb_furryfeastContext context) {
            _context = context;
        }

        // 查詢所有資料
        [HttpGet]
        public async Task<object> GetAll() {
            if (_context.StockSuppliersGroups == null) {
                return NotFound("StockSuppliersGroups is null.");
            }

            var result = await _context.StockSuppliersGroups.Select(data => new StockSuppliersGroupViewModel {
                SuppliersGroupsId = data.SuppliersGroupsId,
                SuppliersGroupsCode = data.SuppliersGroupsCode,
                SuppliersGroupsDescription = data.SuppliersGroupsDescription
            }).ToListAsync();
            return Ok(result);
        }

        // 新增一筆資料
        [HttpPost]
        public async Task<object> PostData([FromBody] StockSuppliersGroupViewModel data) {
            if (_context.StockSuppliersGroups == null) {
                return NotFound("StockSuppliersGroups is null.");
            }

            // 如果資料重複
            var result = await _context.StockSuppliersGroups.Where(d => d.SuppliersGroupsCode == data.SuppliersGroupsCode).FirstOrDefaultAsync();
            if (result != null) {
                return Conflict($"Data duplicate, SuppliersGroupsCode: {data.SuppliersGroupsCode}");
            }

            result = new StockSuppliersGroup {
                SuppliersGroupsId = data.SuppliersGroupsId,
                SuppliersGroupsCode = data.SuppliersGroupsCode,
                SuppliersGroupsDescription = data.SuppliersGroupsDescription
            };

            _context.StockSuppliersGroups.Add(result);
            await _context.SaveChangesAsync();
            return Ok($"Post success, SuppliersGroupsCode: {data.SuppliersGroupsCode}.");
        }

        // 刪除一筆資料
        [HttpDelete("{code}")]
        public async Task<object> DeleteData(string code) {
            if (_context.StockSuppliersGroups == null) {
                return NotFound("StockSuppliersGroups is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockSuppliersGroups.Where(d => d.SuppliersGroupsCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Delete failed, SuppliersGroupsCode: {code}");
            }

            _context.StockSuppliersGroups.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Delete success, SuppliersGroupsCode: {code}.");
        }

        // 更新一筆資料
        [HttpPatch]
        public async Task<object> PatchData([FromBody] StockSuppliersGroupViewModel data) {
            if (_context.StockSuppliersGroups == null) {
                return NotFound("StockSuppliersGroups is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockSuppliersGroups.Where(d => d.SuppliersGroupsId == data.SuppliersGroupsId).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Patch failed, SuppliersGroupsCode: {data.SuppliersGroupsCode}.");
            }

            var patchOneData = await _context.StockSuppliersGroups.Where(d => d.SuppliersGroupsCode == data.SuppliersGroupsCode).FirstOrDefaultAsync();

			// 檢查 code 是否存在, code 是唯一的字串
			// code 存在但不同筆 = 回傳錯誤, code 重複命名
			// code 存在且為同一筆 = 更新這一筆資料
			if (result.SuppliersGroupsCode != data.SuppliersGroupsCode && patchOneData != null) {
                return BadRequest($"Patch duplicate, SuppliersGroupsCode: {data.SuppliersGroupsCode}.");
            }

            result.SuppliersGroupsCode = data.SuppliersGroupsCode;
            result.SuppliersGroupsDescription = data.SuppliersGroupsDescription;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok($"Patch success, SuppliersGroupsCode: {data.SuppliersGroupsCode}.");
        }
    }
}
