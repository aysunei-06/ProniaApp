using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.DAL;
using ProniaApp.Models;
using ProniaApp.ViewModels;

namespace ProniaApp.Controllers
{
    public class ShopController : Controller
    {
        public readonly AppDBContext _context;
        public ShopController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int? Id)
        {
            if(Id == null || Id <= 0) {
                return RedirectToAction("NotFoundPage", "Error");
            }

            Product product = _context.Products.Include(p=>p.Category).Include(p=>p.ProductImages).FirstOrDefault(p => p.Id == Id);

            if (product == null)
            {
                return RedirectToAction("NotFoundPage", "Error");
            }
            
            DetailVM vm = new DetailVM
            {
                Product = product,
                RelatedProducts = _context.Products.Where(p => p.CategoryId == product.CategoryId).ToList()
            };

            return View(vm);
        }

    }
}
