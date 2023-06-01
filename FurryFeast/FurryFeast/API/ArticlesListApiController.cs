using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API;

[Route("api/articles/[action]")]
[ApiController]
public class ArticlesListController : ControllerBase
{
    private readonly db_a989fb_furryfeastContext _context;

    public ArticlesListController(db_a989fb_furryfeastContext context)
    {
        _context = context;
    }

    public object GetAdmin()
    {
        return _context.Admins
            .Select(x => new
                {
                    x.AdminId,
                    x.AdminAccount
                }
            ).ToList();
    }

    public object GetModel()
    {
        return _context.Articles.Include(x => x.Admin)
            .Select(x => new
                {
                    x.Admin.AdminAccount,
                    x.AdminId,
                    x.ArticleTitle,
                    x.ArticleText,
                    x.ArticleDate,
                    x.ArticleId
                }
            ).ToList();
    }

    [HttpGet("{id}")]
    public object GetArticle(int id)
    {
        return _context.Articles.Include(x => x.Admin)
            .Where(x => x.ArticleId == id)
            .Select(x => new
            {
                x.Admin.AdminAccount,
                x.AdminId,
                x.ArticleTitle,
                x.ArticleText,
                x.ArticleDate,
                x.ArticleId
            });
    }

    [HttpPost]
    public async Task<string> AddArticle([FromBody] ArticleViewModel model)
    {

        Article newArticle = new Article();

        newArticle.AdminId = model.AdminId;
        newArticle.ArticleTitle = model.ArticleTitle;
        newArticle.ArticleText = model.ArticleText;
        newArticle.ArticleDate = model.ArticleDate;

        _context.Articles.Add(newArticle);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

            return "新增文章失敗";

        }

        return "新增文章成功";
    }

    [HttpPost]
    public object UpdateArticle([FromBody] ArticleViewModel model)
    {
        try
        {
            var editedArticle = _context.Articles.FirstOrDefault(x => x.ArticleId == model.ArticleId);
            if (editedArticle == null) return new { success = false, message = "Article not found" };
            editedArticle.ArticleTitle = model.ArticleTitle;
            editedArticle.ArticleText = model.ArticleText;
            editedArticle.ArticleDate = model.ArticleDate;

            _context.Articles.Update(editedArticle);
            _context.SaveChanges();
            return new { success = true, message = "Article updated successfully" };
        }
        catch (Exception e)
        {
            return new { success = false, message = e.Message };
        }

    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteArticle(int id)
    {
        try
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null) _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return "刪除文章成功!";
        }
        catch (Exception)
        {
            return "刪除文章失敗";
        }
    }
}