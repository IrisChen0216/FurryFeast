using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockGroupsApiController : ControllerBase {
        private db_a989fb_furryfeastContext _context;

        public StockGroupsApiController(db_a989fb_furryfeastContext context) {
            _context = context;
        }

        // 查詢所有資料
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            if (_context.StockGroups == null) {
                return NotFound("StockGroups is null.");
            }

            var result = await _context.StockGroups.Select(data => new StockGroupViewModel {
                GroupsId = data.GroupsId,
                GroupsCode = data.GroupsCode,
                GroupsDescription = data.GgroupsDescription,
                GroupsNotes = data.GroupsNotes
            }).ToListAsync();
            return Ok(result);
        }

        // 新增一筆資料
        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] StockGroupViewModel data) {
            if (_context.StockGroups == null) {
                return NotFound("StockGroups is null.");
            }

            // 如果資料重複
            var result = await _context.StockGroups.Where(d => d.GroupsCode == data.GroupsCode).FirstOrDefaultAsync();
            if (result != null) {
                return Conflict($"Data duplicate, GroupsCode: {data.GroupsCode}");
            }

            result = new StockGroup {
                GroupsId = data.GroupsId,
                GroupsCode = data.GroupsCode,
                GgroupsDescription = data.GroupsDescription,
                GroupsNotes = data.GroupsNotes
            };

            _context.StockGroups.Add(result);
            await _context.SaveChangesAsync();
            return Ok($"Post success, GroupsCode: {data.GroupsCode}.");
        }

        // 刪除一筆資料
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteData(string code) {
            if (_context.StockGroups == null) {
                return NotFound("StockGroups is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockGroups.Where(d => d.GroupsCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Delete failed, GroupsCode: {code}");
            }

            _context.StockGroups.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Delete success, GroupsCode: {code}.");
        }

        // 更新一筆資料
        [HttpPatch]
        public async Task<IActionResult> PatchData([FromBody] StockGroupViewModel data) {
            if (_context.StockGroups == null) {
                return NotFound("StockGroups is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockGroups.Where(d => d.GroupsId == data.GroupsId).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Patch failed, GroupsCode: {data.GroupsCode}.");
            }

            var patchOneData = await _context.StockGroups.Where(d => d.GroupsCode == data.GroupsCode).FirstOrDefaultAsync();

			// 檢查 code 是否存在, code 是唯一的字串
			// code 存在但不同筆 = 回傳錯誤, code 重複命名
			// code 存在且為同一筆 = 更新這一筆資料
			if (result.GroupsCode != data.GroupsCode && patchOneData != null) {
                return BadRequest($"Patch duplicate, GroupsCode: {data.GroupsCode}.");
            }

            result.GroupsCode = data.GroupsCode;
            result.GgroupsDescription = data.GroupsDescription;
            result.GroupsNotes = data.GroupsNotes;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok($"Patch success, GroupsCode: {data.GroupsCode}.");
        }
    }
}
