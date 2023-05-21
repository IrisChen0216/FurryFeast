using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurryFeast.Models;
using FurryFeast.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace FurryFeast.Controllers
{
    public class MembersController : Controller
    {
        private readonly db_a989fb_furryfeastContext _context;

        public MembersController(db_a989fb_furryfeastContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var db_a989fb_furryfeastContext = _context.Members.Include(m => m.Conpon);
            return View(await db_a989fb_furryfeastContext.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Conpon)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["ConponId"] = new SelectList(_context.Conpons, "ConponId", "ConponId");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,MemberName,MemberBirthday,MemberAdress,MemberEmail,MemberPhone,MemberGender,MemberAccount,MemberPassord,ConponId")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConponId"] = new SelectList(_context.Conpons, "ConponId", "ConponId", member.ConponId);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            ViewData["ConponId"] = new SelectList(_context.Conpons, "ConponId", "ConponId", member.ConponId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,MemberName,MemberBirthday,MemberAdress,MemberEmail,MemberPhone,MemberGender,MemberAccount,MemberPassord,ConponId")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConponId"] = new SelectList(_context.Conpons, "ConponId", "ConponId", member.ConponId);
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Conpon)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'db_a989fb_furryfeastContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult MyOrder()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyClass()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> MyConpon()
        {
            return View();
        }

        public IActionResult RegisterIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel list)
        {
            var Member = _context.Members.FirstOrDefault(x => x.MemberAccount == list.MemberAccount);
            if (Member != null)
            {
                ViewBag.Error = "已有帳號存在!";
                return View("Login");
            }
            _context.Members.Add(new Member()
            {
                MemberAccount = list.MemberAccount,
                MemberPassord = list.MemberPassord,
                MemberAdress = list.MemberAdress,
                MemberName = list.MemberName,
                MemberEmail = list.MemberEmail,
                MemberPhone = list.MemberPhone,
                MemberBirthday = list.MemberBirthday,
                MemberGender = list.MemberGender,
                MemberId = list.MemberId
               
            });
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel list)

        {
            var Member = _context.Members.FirstOrDefault(x => x.MemberAccount == list.MemberAccount && x.MemberPassord == list.MemberPassord);

            if (Member == null)
            {
                ViewBag.Error = "帳號密碼錯誤!";
                return View("Login");
            }
            //return Ok(model.MemberAccount + model.MemberPassord);
            var ClaimList=new List<Claim>() {
			new Claim(ClaimTypes.Name, Member.MemberName),
            new Claim("Id",Member.MemberId.ToString())
		};
         

            var ClaimIndentity = new ClaimsIdentity(ClaimList, CookieAuthenticationDefaults.AuthenticationScheme);
            var ClaimPrincipal = new ClaimsPrincipal(ClaimIndentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,ClaimPrincipal);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> UpdateMemberData()
        {
            return View();
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }

	
}
