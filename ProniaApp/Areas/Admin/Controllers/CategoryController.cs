using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.DAL;
using ProniaApp.Models;

namespace ProniaApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        public readonly AppDBContext _context;
        public CategoryController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.Include(c => c.Products).ToList();

            return View(categories);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }
            bool result = await _context.Categories.AnyAsync(c => c.Name==category.Name);

            if (result)
            {
                ModelState.AddModelError("Name", "This category already exists");
                return View(category);

            }
            
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();

            Category category = _context.Categories.Include(c => c.Products) .FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }
    }
}
