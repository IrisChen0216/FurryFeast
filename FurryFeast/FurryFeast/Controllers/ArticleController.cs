using Microsoft.AspNetCore.Mvc;
using FurryFeast.Models;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Controllers {
	public class ArticleController : Controller {
		private static readonly HttpClient client = new HttpClient();
		private readonly db_a989fb_furryfeastContext _furryFeastContext;

		public ArticleController(db_a989fb_furryfeastContext furryFeastContext) {
			_furryFeastContext = furryFeastContext;
		}
		private Article GetArticleById(int id) {
			return _furryFeastContext.Articles.FirstOrDefault(a => a.ArticleId == id);
		}
		public IActionResult NewsPost(int
			 id) {

			ViewBag.TitleOne = _furryFeastContext.Articles.Where(p => p.ArticleDate != null)
									.OrderByDescending(p => p.ArticleDate)
									.Take(1)
									.FirstOrDefault().ArticleTitle;

			ViewBag.TitleTwo = _furryFeastContext.Articles.Where(p => p.ArticleDate != null)
									.OrderByDescending(p => p.ArticleDate)
									.Skip(1).Take(1).SingleOrDefault().ArticleTitle;

			ViewBag.TitleThree = _furryFeastContext.Articles.Where(p => p.ArticleDate != null)
								   .OrderByDescending(p => p.ArticleDate)
								   .Skip(2).Take(1).SingleOrDefault().ArticleTitle;

			var article = _furryFeastContext.Articles.Include(data => data.Admin).Where(data => data.ArticleId == id).FirstOrDefault();

			//if (article == null)
			//{
			//    return NotFound(); 
			//}

			return View(article);
		}

		public IActionResult FAQ() {
			return View();
		}

		public IActionResult LostFrom()
		{
			return View();
		}
		public IActionResult News() {
			using (var dbContext = new db_a989fb_furryfeastContext()) {
				var articles = _furryFeastContext.Articles.Include(data => data.Admin).ToList();
				return View(articles);
			}
		}



		public IActionResult Donates() {
			var donate = _furryFeastContext.Donates.FirstOrDefault();
			return View(donate);
		}

		public async Task<IActionResult> Shelter() {
			return View();
		}




		// 聯絡我們
		public IActionResult ContactUs() {
			ViewBag.Message = 3;
			return View();
		}

        // 聯絡我們，送出表單
        [HttpPost]
        public IActionResult ContactUs(ContactU contact)
        {
            if (ModelState.IsValid)
            {
                _furryFeastContext.ContactUs.Add(contact);
                _furryFeastContext.SaveChanges();

                //TempData["SuccessMessage"] = "表單送出成功";
                ViewBag.Message = 1;
                return View();
            }

            //TempData["ErrorMessage"] = "表單送出失敗";
            ViewBag.Message = 0;
            return View();
        }
        public IActionResult ContactUsSuccess()
        {
            ViewBag.Message = TempData["SuccessMessage"] as string;
            return View();
        }

        public IActionResult ContactUsFailure()
        {
            ViewBag.Message = TempData["ErrorMessage"] as string;
            return View();
        }


        public IActionResult LostPets()
        {
            return View();
        }






    }
}

