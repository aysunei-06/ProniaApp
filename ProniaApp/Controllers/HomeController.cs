using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.DAL;
using ProniaApp.Models;
using ProniaApp.ViewModels;

namespace ProniaApp.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Product> products= _context.Products.Include(p => p.ProductImages).ToList();
            HomeVM homeVM = new HomeVM
            {
                Products = products
            };

            //_context.AddRange(products);
            //_context.SaveChanges();
            return View(homeVM);
        }
    }
}
