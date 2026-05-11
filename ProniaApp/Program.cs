using Microsoft.EntityFrameworkCore;

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

            var app = builder.Build();
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
