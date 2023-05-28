using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using FurryFeast.Models;
using System.Linq;
using FurryFeast.Data;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.Controllers
{
    public class ArticleController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly db_a989fb_furryfeastContext _furryFeastContext;

        public ArticleController(db_a989fb_furryfeastContext furryFeastContext)
        {
            _furryFeastContext = furryFeastContext;
        }
        private Article GetArticleById(int id)
        {
            return _furryFeastContext.Articles.FirstOrDefault(a => a.ArticleId == id);
        }
        public IActionResult NewsPost(int
             id)
        {

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

            var article = _furryFeastContext.Articles.Include(data => data.Admin).Where(data=>data.ArticleId==id).FirstOrDefault();

            //if (article == null)
            //{
            //    return NotFound(); 
            //}

            return View(article);
        }

        public IActionResult FAQ()
        {
            return View();                           
        }

        public IActionResult News()
        {
            using (var dbContext = new db_a989fb_furryfeastContext())
            {
                var articles = _furryFeastContext.Articles.Include(data => data.Admin).ToList();
                return View(articles);
            }
        }



        public IActionResult Donates()
        {
            var donate = _furryFeastContext.Donates.FirstOrDefault(); 
            return View(donate);
        }

        public async Task<IActionResult> Shelter(
    int? page,
    string? animal_kind,
    string? animal_Variety,
    string? shelter_name,
    string? animal_sex,
    string? animal_colour,
    string? animal_age)
        {
            var jsonString = await client.GetStringAsync("https://data.coa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL");
            var pets = JsonConvert.DeserializeObject<List<Pet>>(jsonString);

      
            if (!string.IsNullOrEmpty(animal_kind))
            {
                pets = pets.Where(p => p.animal_kind == animal_kind).ToList();
            }
         

            ViewData["AnimalKinds"] = pets.Select(p => p.animal_kind).Distinct().ToList();
            ViewData["AnimalVarieties"] = pets.Select(p => p.animal_Variety).Distinct().ToList();
            ViewData["ShelterNames"] = pets.Select(p => p.shelter_name).Distinct().ToList();
            ViewData["AnimalSexes"] = pets.Select(p => p.animal_sex).Distinct().ToList();
            ViewData["AnimalColours"] = pets.Select(p => p.animal_colour).Distinct().ToList();
            ViewData["AnimalAges"] = pets.Select(p => p.animal_age).Distinct().ToList();
            if (!string.IsNullOrEmpty(shelter_name))
            {
                pets = pets.Where(p => p.shelter_name.ToLower() == shelter_name.ToLower()).ToList();
            }

            var pageSize = 12;
            var totalItemCount = pets.Count;
            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);

            var pageNumber = page ?? 1;
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPageCount));

            var petsPaged = pets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();


            ViewBag.PageNumber = pageNumber;
            ViewBag.PageCount = totalPageCount;

            return View(petsPaged);
        }



        public IActionResult LostForm()
        {
            return View();
        }

        // 聯絡我們
        public IActionResult ContactUs() {
            return View();
        }

        // 連握我們，送出表單
		[HttpPost]
        public IActionResult ContactUs(ContactU contact)
        {
            if (ModelState.IsValid)
            {
                _furryFeastContext.ContactUs.Add(contact);
                _furryFeastContext.SaveChanges();
                return View(); 
            }
            return View(contact); 
        }


        // ArticleController.cs

        public async Task<IActionResult> Lostpets(int? page, string petType, string gender, string breed, string color, DateTime? lostTime, string lostLocation)
        {
            var pageSize = 12;

            var jsonString = await client.GetStringAsync("https://data.coa.gov.tw/Service/OpenData/TransService.aspx?UnitId=IFJomqVzyB0i");
            var pets = JsonConvert.DeserializeObject<List<Lostpets>>(jsonString);

            if (!string.IsNullOrEmpty(petType))
            {
                pets = pets.Where(p => p.PetType == petType).ToList();
            }

            if (!string.IsNullOrEmpty(gender))
            {
                pets = pets.Where(p => p.Gender == gender).ToList();
            }

            if (!string.IsNullOrEmpty(breed))
            {
                pets = pets.Where(p => p.Breed.Contains(breed, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(color))
            {
                pets = pets.Where(p => p.Color.Contains(color, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (lostTime.HasValue)
            {
                pets = pets.Where(p => DateTime.TryParseExact(p.LostTime, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var pLostTime) && pLostTime.Date == lostTime.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(lostLocation))
            {
                pets = pets.Where(p => p.LostLocation.Contains(lostLocation, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // 過濾掉沒有有效圖片的資料
            pets = pets.Where(p => !string.IsNullOrEmpty(p.Picture) && Uri.IsWellFormedUriString(p.Picture, UriKind.Absolute)).ToList();

            var totalItemCount = pets.Count;
            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);

            var pageNumber = page ?? 1;
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPageCount));

            var petsPaged = pets.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewData["PetTypes"] = pets.Select(p => p.PetType).Distinct().ToList();
            ViewData["Breeds"] = pets.Select(p => p.Breed).Distinct().ToList();
            ViewData["Colors"] = pets.Select(p => p.Color).Distinct().ToList();
            ViewData["LostTimes"] = pets.Select(p => p.LostTime).Distinct().ToList();
            ViewData["Genders"] = pets.Select(p => p.Gender).Distinct().ToList();
            ViewData["LostLocations"] = pets.Select(p => p.LostLocation).Distinct().ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageCount = totalPageCount;

            return View(petsPaged);
        }


        [HttpPost]
        public async Task<IActionResult> LoadLostPets(string petType, string gender, string breed, string color, DateTime? lostTime, string lostLocation)
        {
            var jsonString = await client.GetStringAsync("https://data.coa.gov.tw/Service/OpenData/TransService.aspx?UnitId=IFJomqVzyB0i");
            var pets = JsonConvert.DeserializeObject<List<Lostpets>>(jsonString);

            if (!string.IsNullOrEmpty(petType))
            {
                pets = pets.Where(p => p.PetType == petType).ToList();
            }

            if (!string.IsNullOrEmpty(gender))
            {
                pets = pets.Where(p => p.Gender == gender).ToList();
            }

            if (!string.IsNullOrEmpty(breed))
            {
                pets = pets.Where(p => p.Breed.Contains(breed, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(color))
            {
                pets = pets.Where(p => p.Color.Contains(color, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (lostTime.HasValue)
            {
                pets = pets.Where(p => DateTime.TryParseExact(p.LostTime, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var pLostTime) && pLostTime.Date == lostTime.Value.Date).ToList();
            }

            if (!string.IsNullOrEmpty(lostLocation))
            {
                pets = pets.Where(p => p.LostLocation.Contains(lostLocation, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // 過濾掉沒有有效圖片的資料
            pets = pets.Where(p => !string.IsNullOrEmpty(p.Picture) && Uri.IsWellFormedUriString(p.Picture, UriKind.Absolute)).ToList();

            return Json(pets);
        }
    }
}
    
   