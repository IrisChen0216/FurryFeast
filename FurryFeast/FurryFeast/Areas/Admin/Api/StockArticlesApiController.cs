using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Areas.Admin.Api {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockArticlesApiController : ControllerBase {
        private db_a989fb_furryfeastContext _context;

        public StockArticlesApiController(db_a989fb_furryfeastContext context) {
            _context = context;
        }

        // 查詢所有資料
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            if (_context.StockArticles == null) {
                return NotFound("StockArticles is null.");
            }

            var result = await _context.StockArticles.Select(data => new StockArticleViewModel {
                ArticlesId = data.ArticlesId,
                ArticlesCode = data.ArticlesCode,
                ArticlesIsValid = data.ArticlesIsValid,
                ArticlesDescription = data.ArticlesDescription,
                ArticlesNotes = data.ArticlesNotes,
                ArticlesPrice = data.ArticlesPrice,
                ArticlesQuantity = data.ArticlesQuantity,
                GroupId = data.GroupId,
                MeasureUnitId = data.MeasureUnitId,
                WarehousesId = data.WarehousesId,
                SuppliersId = data.SuppliersId,
                ImagesId = data.ImagesId
            }).ToListAsync();
            return Ok(result);
        }

        // 新增一筆資料
        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] StockArticleViewModel data) {
            if (_context.StockArticles == null) {
                return NotFound("StockArticles is null.");
            }

            // 如果資料重複
            var result = await _context.StockArticles.Where(d => d.ArticlesCode == data.ArticlesCode).FirstOrDefaultAsync();
            if (result != null) {
                return Conflict($"Data duplicate, ArticlesCode: {data.ArticlesCode}");
            }

            result = new StockArticle {
                ArticlesId = data.ArticlesId,
                ArticlesCode = data.ArticlesCode,
                ArticlesIsValid = data.ArticlesIsValid,
                ArticlesDescription = data.ArticlesDescription,
                ArticlesNotes = data.ArticlesNotes,
                ArticlesPrice = data.ArticlesPrice,
                ArticlesQuantity = data.ArticlesQuantity,
                GroupId = data.GroupId,
                MeasureUnitId = data.MeasureUnitId,
                WarehousesId = data.WarehousesId,
                SuppliersId = data.SuppliersId,
                ImagesId = data.ImagesId
            };

            _context.StockArticles.Add(result);
            await _context.SaveChangesAsync();
            return Ok($"Post success, ArticlesCode: {data.ArticlesCode}.");
        }

        // 刪除一筆資料
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteData(string code) {
            if (_context.StockArticles == null) {
                return NotFound("StockArticles is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockArticles.Where(d => d.ArticlesCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Delete failed, ArticlesCode: {code}");
            }

            _context.StockArticles.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Delete success, ArticlesCode: {code}.");
        }

        // 更新一筆資料
        [HttpPatch]
        public async Task<IActionResult> PatchData([FromBody] StockArticleViewModel data) {
            if (_context.StockArticles == null) {
                return NotFound("StockArticles is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockArticles.Where(d => d.ArticlesId == data.ArticlesId).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Patch failed, ArticlesCode: {data.ArticlesCode}.");
            }

            var patchOneData = await _context.StockArticles.Where(d => d.ArticlesCode == data.ArticlesCode).FirstOrDefaultAsync();

            // 檢查 code 是否存在, code 是唯一的字串
            // code 存在但不同筆 = 回傳錯誤, code 重複命名
            // code 存在且為同一筆 = 更新這一筆資料
            if (result.ArticlesCode != data.ArticlesCode && patchOneData != null) {
                return BadRequest($"Patch duplicate, ArticlesCode: {data.ArticlesCode}.");
            }

            result.ArticlesCode = data.ArticlesCode;
            result.ArticlesIsValid = data.ArticlesIsValid;
            result.ArticlesDescription = data.ArticlesDescription;
            result.ArticlesNotes = data.ArticlesNotes;
            result.ArticlesPrice = data.ArticlesPrice;
            result.ArticlesQuantity = data.ArticlesQuantity;
            result.GroupId = data.GroupId;
            result.MeasureUnitId = data.MeasureUnitId;
            result.WarehousesId = data.WarehousesId;
            result.SuppliersId = data.SuppliersId;
            result.ImagesId = data.ImagesId;
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok($"Patch success, ArticlesCode: {data.ArticlesCode}.");
        }
    }
}
