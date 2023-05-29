using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockSuppliersApiController : ControllerBase {
        private db_a989fb_furryfeastContext _context;

        public StockSuppliersApiController(db_a989fb_furryfeastContext context) {
            _context = context;
        }

        // 查詢所有資料
        [HttpGet]
        public async Task<object> GetAll() {
            if (_context.StockSuppliers == null) {
                return NotFound("StockSuppliers is null.");
            }

            var result = await _context.StockSuppliers.Select(data => new StockSupplierViewModel {
                SuppliersId = data.SuppliersId,
                SuppliersCode = data.SuppliersCode,
                SuppliersDescription = data.SuppliersDescription,
                SuppliersStreet = data.SuppliersStreet,
                SuppliersZipCode = data.SuppliersZipCode,
                SuppliersCity = data.SuppliersCity,
                SuppliersCountry = data.SuppliersCountry,
                SuppliersNation = data.SuppliersNation,
                SuppliersPhone = data.SuppliersPhone,
                SuppliersFax = data.SuppliersFax,
                SuppliersEmail = data.SuppliersEmail,
                SuppliersUrl = data.SuppliersUrl,
                SupplierGroupId = data.SupplierGroupId
            }).ToListAsync();
            return Ok(result);
        }

        // 新增一筆資料
        [HttpPost]
        public async Task<object> PostData([FromBody] StockSupplierViewModel data) {
            if (_context.StockSuppliers == null) {
                return NotFound("StockSuppliers is null.");
            }

            // 如果資料重複
            var result = await _context.StockSuppliers.Where(d => d.SuppliersCode == data.SuppliersCode).FirstOrDefaultAsync();
            if (result != null) {
                return Conflict($"Data duplicate, SuppliersCode: {data.SuppliersCode}");
            }

            result = new StockSupplier {
                SuppliersId = data.SuppliersId,
                SuppliersCode = data.SuppliersCode,
                SuppliersDescription = data.SuppliersDescription,
                SuppliersStreet = data.SuppliersStreet,
                SuppliersZipCode = data.SuppliersZipCode,
                SuppliersCity = data.SuppliersCity,
                SuppliersCountry = data.SuppliersCountry,
                SuppliersNation = data.SuppliersNation,
                SuppliersPhone = data.SuppliersPhone,
                SuppliersFax = data.SuppliersFax,
                SuppliersEmail = data.SuppliersEmail,
                SuppliersUrl = data.SuppliersUrl,
                SupplierGroupId = data.SupplierGroupId
            };

            _context.StockSuppliers?.Add(result);
            await _context.SaveChangesAsync();
            return Ok($"Post success, SuppliersCode: {data.SuppliersCode}.");
        }

        // 刪除一筆資料
        [HttpDelete("{code}")]
        public async Task<object> DeleteData(string code) {
            if (_context.StockSuppliers == null) {
                return NotFound("StockSuppliers is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockSuppliers.Where(d => d.SuppliersCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Delete failed, SuppliersCode: {code}");
            }

            _context.StockSuppliers.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Delete success, SuppliersCode: {code}.");
        }

        // 更新一筆資料
        [HttpPatch("{code}")]
        public async Task<object> PatchData(string code, [FromBody] StockSupplierViewModel data) {
            if (_context.StockSuppliers == null) {
                return NotFound("StockSuppliers is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockSuppliers.Where(d => d.SuppliersCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Patch failed, SuppliersCode: {code}.");
            }

            var patchOneData = await _context.StockSuppliers.Where(d => d.SuppliersCode == data.SuppliersCode).FirstOrDefaultAsync();

            // 檢查 code 是否存在
            if (result.SuppliersCode != data.SuppliersCode && patchOneData != null) {
                return BadRequest($"Patch duplicate, SuppliersCode: {code}.");
            }

            result.SuppliersCode = data.SuppliersCode;
            result.SuppliersDescription = data.SuppliersDescription;
            result.SuppliersStreet = data.SuppliersStreet;
            result.SuppliersZipCode = data.SuppliersZipCode;
            result.SuppliersCity = data.SuppliersCity;
            result.SuppliersCountry = data.SuppliersCountry;
            result.SuppliersNation = data.SuppliersNation;
            result.SuppliersPhone = data.SuppliersPhone;
            result.SuppliersFax = data.SuppliersFax;
            result.SuppliersEmail = data.SuppliersEmail;
            result.SuppliersUrl = data.SuppliersUrl;
            result.SupplierGroupId = data.SupplierGroupId;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok($"Patch success, SuppliersCode: {code}.");
        }
    }
}
