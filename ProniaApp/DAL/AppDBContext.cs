using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using ProniaApp.Models;

namespace ProniaApp.DAL
{
    public class AppDBContext: IdentityDbContext<AppUser>
    {
       public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
       {
       }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Product> Products { get; set; }
       public DbSet<ProductImage> ProductImages { get; set; }
       public DbSet<Slide> Slides { get; set; }


    }

}
