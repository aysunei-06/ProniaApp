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
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            bool result = await _context.Categories.AnyAsync(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower());

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
            if (id == null || id <= 0) return BadRequest();

            Category category = _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }


        public IActionResult Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Category existed = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (existed == null) return NotFound();

            return View(existed);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id == null || id <= 0) return BadRequest();

            Category existed = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (existed == null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View(existed);
            }

            bool result = await _context.Categories.AnyAsync(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower() && c.Id != id);

            if (result)
            {
                ModelState.AddModelError("Name", "This category already exists");
                return View(existed);
            }

            existed.Name = category.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Category existed = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (existed == null) return NotFound();

            _context.Categories.Remove(existed);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



    }
}