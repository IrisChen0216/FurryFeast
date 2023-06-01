using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;

namespace FurryFeast.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public NewsController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            
            return View();
        }

    }
}

