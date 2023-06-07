using FurryFeast.Data;
using FurryFeast.Models;
using FurryFeast.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Text.Json.Serialization;

namespace FurryFeast {
	public class Program {
		public static void Main(string[] args) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the DI container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

			// db_a989fb_furryfeastContext
			var FurryFeastDbConnectionString = builder.Configuration.GetConnectionString("FurryFeastDb") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<db_a989fb_furryfeastContext>(options =>
				options.UseLazyLoadingProxies().UseSqlServer(FurryFeastDbConnectionString));

			// 避免 EF 生成 JSON 參考循環錯誤
			// System.Text.Json.JsonException: A possible object cycle was detected
			//builder.Services.AddControllers().AddJsonOptions(x =>
			//	x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
				AddCookie(opt => {
					opt.LoginPath = "/Members/Index";
					opt.AccessDeniedPath = "/Home/AcessDenied";
					opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
				})
				.AddFacebook(facebookOptions =>
				{
					facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
					facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
					//facebookOptions.Events.TicketReceived
					facebookOptions.Events.OnCreatingTicket = (x) =>
					{
						return Task.CompletedTask;
					};
				})
				.AddGoogle(googleOptions =>
				{
					googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
					googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
				});
			builder.Services.AddTransient<EncryptService>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddRazorPages();

			builder.Services.AddControllersWithViews();

			
			builder.Services.AddHttpContextAccessor();

			builder.Services.AddDistributedMemoryCache();

			//加入購物車要用的Session
			builder.Services.AddSession(option => {
				option.Cookie.Name = ".FurryFeastCart.Session";
				option.IdleTimeout = TimeSpan.FromDays(365);
				option.Cookie.IsEssential = true;
				option.Cookie.HttpOnly = true;
				option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseMigrationsEndPoint();
			} else {
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseSession();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "areas",
				  pattern: "{area:exists}/{controller=Home}/{action=SignIn}/{id?}");
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}