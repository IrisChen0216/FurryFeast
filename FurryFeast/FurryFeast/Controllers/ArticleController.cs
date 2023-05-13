using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using FurryFeast.Models;  // 新添加的行

namespace FurryFeast.Controllers
{
    public class ArticleController : Controller
    {
        private static readonly HttpClient client = new HttpClient();


        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Donates()
        {
            return View();
        }
        public IActionResult Shelter()
        {
            return View();
        }
        public IActionResult LostForm()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        public async Task<IActionResult> Lostpets()
        {
            var jsonString = await client.GetStringAsync("https://data.coa.gov.tw/Service/OpenData/TransService.aspx?UnitId=IFJomqVzyB0i");
            var pets = JsonConvert.DeserializeObject<List<Lostpets>>(jsonString);
            // 將每一種可能的寵物種類、品種、毛色、失蹤時間、性別和遺失地點添加到 ViewData
            ViewData["PetTypes"] = pets.Select(p => p.PetType).Distinct().ToList();
            ViewData["Breeds"] = pets.Select(p => p.Breed).Distinct().ToList();
            ViewData["Colors"] = pets.Select(p => p.Color).Distinct().ToList();
            ViewData["LostTimes"] = pets.Select(p => p.LostTime).Distinct().ToList();
            ViewData["Genders"] = pets.Select(p => p.Gender).Distinct().ToList();
            ViewData["LostLocations"] = pets.Select(p => p.LostLocation).Distinct().ToList();

            return View(pets);
        }

    }
}

