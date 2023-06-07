using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockWarehousesApiController : ControllerBase {
        private db_a989fb_furryfeastContext _context;

        public StockWarehousesApiController(db_a989fb_furryfeastContext context) {
            _context = context;
        }

        // 查詢所有資料
        [HttpGet]
        public async Task<object> GetAll() {
            if (_context.StockWarehouses == null) {
                return NotFound("StockWarehouses is null.");
            }

            var result = await _context.StockWarehouses.Select(data => new StockWarehouseViewModel {
                WarehousesId = data.WarehousesId,
                WarehousesCode = data.WarehousesCode,
                WarehousesDescription = data.WarehousesDescription,
                WarehousesStreet = data.WarehousesStreet,
                WarehousesZipCode = data.WarehousesZipCode,
                WarehousesCity = data.WarehousesCity,
                WarehousesCountry = data.WarehousesCountry,
                WarehousesNation = data.WarehousesNation,
                WarehouseGroupId = data.WarehouseGroupId
            }).ToListAsync();
            return Ok(result);
        }

        // 新增一筆資料
        [HttpPost]
        public async Task<object> PostData([FromBody] StockWarehouseViewModel data) {
            if (_context.StockWarehouses == null) {
                return NotFound("StockWarehouses is null.");
            }

            // 如果資料重複
            var result = await _context.StockWarehouses.Where(d => d.WarehousesCode == data.WarehousesCode).FirstOrDefaultAsync();
            if (result != null) {
                return Conflict($"Data duplicate, WarehousesCode: {data.WarehousesCode}");
            }

            result = new StockWarehouse {
                WarehousesId = data.WarehousesId,
                WarehousesCode = data.WarehousesCode,
                WarehousesDescription = data.WarehousesDescription,
                WarehousesStreet = data.WarehousesStreet,
                WarehousesZipCode = data.WarehousesZipCode,
                WarehousesCity = data.WarehousesCity,
                WarehousesCountry = data.WarehousesCountry,
                WarehousesNation = data.WarehousesNation,
                WarehouseGroupId = data.WarehouseGroupId
            };

            _context.StockWarehouses.Add(result);
            await _context.SaveChangesAsync();
            return Ok($"Post success, WarehousesCode: {data.WarehousesCode}.");
        }

        // 刪除一筆資料
        [HttpDelete("{code}")]
        public async Task<object> DeleteData(string code) {
            if (_context.StockWarehouses == null) {
                return NotFound("StockWarehouses is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockWarehouses.Where(d => d.WarehousesCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Delete failed, WarehousesCode: {code}");
            }

            _context.StockWarehouses.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Delete success, WarehousesCode: {code}.");
        }

        // 更新一筆資料
        [HttpPatch]
        public async Task<object> PatchData([FromBody] StockWarehouseViewModel data) {
            if (_context.StockWarehouses == null) {
                return NotFound("StockWarehouses is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockWarehouses.Where(d => d.WarehousesId == data.WarehousesId).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Patch failed, WarehousesCode: {data.WarehousesCode}.");
            }

            var patchOneData = await _context.StockWarehouses.Where(d => d.WarehousesCode == data.WarehousesCode).FirstOrDefaultAsync();

			// 檢查 code 是否存在, code 是唯一的字串
			// code 存在但不同筆 = 回傳錯誤, code 重複命名
			// code 存在且為同一筆 = 更新這一筆資料
			if (result.WarehousesCode != data.WarehousesCode && patchOneData != null) {
                return BadRequest($"Patch duplicate, WarehousesCode: {data.WarehousesCode}.");
            }

            result.WarehousesCode = data.WarehousesCode;
            result.WarehousesDescription = data.WarehousesDescription;
            result.WarehousesStreet = data.WarehousesStreet;
            result.WarehousesZipCode = data.WarehousesZipCode;
            result.WarehousesCity = data.WarehousesCity;
            result.WarehousesCountry = data.WarehousesCountry;
            result.WarehousesNation = data.WarehousesNation;
            result.WarehouseGroupId = data.WarehouseGroupId;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok($"Patch success, WarehousesCode: {data.WarehousesCode}.");
        }
    }
}
