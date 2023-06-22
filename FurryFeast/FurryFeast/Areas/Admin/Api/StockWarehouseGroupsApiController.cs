using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockWarehouseGroupsApiController : ControllerBase {
        private db_a989fb_furryfeastContext _context;

        public StockWarehouseGroupsApiController(db_a989fb_furryfeastContext context) {
            _context = context;
        }

        // 查詢所有資料
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            if (_context.StockWarehouseGroups == null) {
                return NotFound("StockWarehouseGroups is null.");
            }

            var result = await _context.StockWarehouseGroups.Select(data => new StockWarehouseGroupViewModel {
                WarehouseGroupsId = data.WarehouseGroupsId,
                WarehouseGroupsCode = data.WarehouseGroupsCode,
                WarehouseGroupsDescription = data.WarehouseGroupsDescription
            }).ToListAsync();
            return Ok(result);
        }

        // 新增一筆資料
        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] StockWarehouseGroupViewModel data) {
            if (_context.StockWarehouseGroups == null) {
                return NotFound("StockWarehouseGroups is null.");
            }

            // 如果資料重複
            var result = await _context.StockWarehouseGroups.Where(d => d.WarehouseGroupsCode == data.WarehouseGroupsCode).FirstOrDefaultAsync();
            if (result != null) {
                return Conflict($"Data duplicate, WarehouseGroupsCode: {data.WarehouseGroupsCode}");
            }

            result = new StockWarehouseGroup {
                WarehouseGroupsId = data.WarehouseGroupsId,
                WarehouseGroupsCode = data.WarehouseGroupsCode,
                WarehouseGroupsDescription = data.WarehouseGroupsDescription
            };

            _context.StockWarehouseGroups.Add(result);
            await _context.SaveChangesAsync();
            return Ok($"Post success, WarehouseGroupsCode: {data.WarehouseGroupsCode}.");
        }

        // 刪除一筆資料
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteData(string code) {
            if (_context.StockWarehouseGroups == null) {
                return NotFound("StockWarehouseGroups is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockWarehouseGroups.Where(d => d.WarehouseGroupsCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Delete failed, WarehouseGroupsCode: {code}");
            }

            _context.StockWarehouseGroups.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Delete success, WarehouseGroupsCode: {code}.");
        }

        // 更新一筆資料
        [HttpPatch]
        public async Task<IActionResult> PatchData([FromBody] StockWarehouseGroupViewModel data) {
            if (_context.StockWarehouseGroups == null) {
                return NotFound("StockWarehouseGroups is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockWarehouseGroups.Where(d => d.WarehouseGroupsId == data.WarehouseGroupsId).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Patch failed, WarehouseGroupsCode: {data.WarehouseGroupsCode}.");
            }

            var patchOneData = await _context.StockWarehouseGroups.Where(d => d.WarehouseGroupsCode == data.WarehouseGroupsCode).FirstOrDefaultAsync();

			// 檢查 code 是否存在, code 是唯一的字串
			// code 存在但不同筆 = 回傳錯誤, code 重複命名
			// code 存在且為同一筆 = 更新這一筆資料
			if (result.WarehouseGroupsCode != data.WarehouseGroupsCode && patchOneData != null) {
                return BadRequest($"Patch duplicate, WarehouseGroupsCode: {data.WarehouseGroupsCode}.");
            }

            result.WarehouseGroupsCode = data.WarehouseGroupsCode;
            result.WarehouseGroupsDescription = data.WarehouseGroupsDescription;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok($"Patch success, WarehouseGroupsCode: {data.WarehouseGroupsCode}.");
        }
    }
}
