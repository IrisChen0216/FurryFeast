using FurryFeast.Areas.Admin.ViewModels;
using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
        public async Task<object> GetAll() {
            if (_context.StockArticles == null) {
                return NotFound("StockArticles is null.");
            }

            var result = await _context.StockArticles.Select(data => new StockArticleViewModel {
                ArticlesId = data.ArticlesId,
                AarticlesCode = data.AarticlesCode,
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
        public async Task<object> PostData([FromBody] StockArticleViewModel data) {
            if (_context.StockArticles == null) {
                return NotFound("StockArticles is null.");
            }

            // 如果資料重複
            var result = await _context.StockArticles.Where(d => d.AarticlesCode == data.AarticlesCode).FirstOrDefaultAsync();
            if (result != null) {
                return Conflict($"Data duplicate, AarticlesCode: {data.AarticlesCode}");
            }

            result = new StockArticle {
                ArticlesId = data.ArticlesId,
                AarticlesCode = data.AarticlesCode,
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
            return Ok($"Post success, AarticlesCode: {data.AarticlesCode}.");
        }

        // 刪除一筆資料
        [HttpDelete("{code}")]
        public async Task<object> DeleteData(string code) {
            if (_context.StockArticles == null) {
                return NotFound("StockArticles is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockArticles.Where(d => d.AarticlesCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Delete failed, AarticlesCode: {code}");
            }

            _context.StockArticles.Remove(result);
            await _context.SaveChangesAsync();
            return Ok($"Delete success, AarticlesCode: {code}.");
        }

        // 更新一筆資料
        [HttpPatch("{code}")]
        public async Task<object> PatchData(string code, [FromBody] StockArticleViewModel data) {
            if (_context.StockArticles == null) {
                return NotFound("StockArticles is null");
            }

            // 檢查資料是否存在
            var result = await _context.StockArticles.Where(d => d.AarticlesCode == code).FirstOrDefaultAsync();
            if (result == null) {
                return BadRequest($"Patch failed, AarticlesCode: {code}.");
            }

            var patchOneData = await _context.StockArticles.Where(d => d.AarticlesCode == data.AarticlesCode).FirstOrDefaultAsync();

            // 檢查 code 是否存在
            if (result.AarticlesCode != data.AarticlesCode && patchOneData != null) {
                return BadRequest($"Patch duplicate, AarticlesCode: {code}.");
            }

            result.AarticlesCode = data.AarticlesCode;
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
            return Ok($"Patch success, AarticlesCode: {code}.");
        }
    }
}
