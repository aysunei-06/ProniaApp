using Microsoft.EntityFrameworkCore;
using ProniaApp.Models;

namespace ProniaApp.DAL
{
    public class AppDBContext: DbContext
    {
       public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
       {
       }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Product> Products { get; set; }
       public DbSet<ProductImage> ProductImages { get; set; }

    }

}
