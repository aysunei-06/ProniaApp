using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaApp.Models;

namespace ProniaApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DAL.AppDBContext>(options =>
            {
                options.UseSqlServer("Server=DESKTOP-N25NMAO\\SQLEXPRESS;Database=ProniaAppDB;Trusted_Connection=True;Encrypt=False");
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt=>
            {
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<DAL.AppDBContext>().AddDefaultTokenProviders();


            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePagesWithReExecute("/Error/NotFoundPage");

            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );


            app.Run();
        }
    }
}
