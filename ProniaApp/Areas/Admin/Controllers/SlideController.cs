
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.Areas.Admin.ViewModels.Create;
using ProniaApp.Areas.Admin.ViewModels.Update;
using ProniaApp.DAL;
using ProniaApp.Models;
using ProniaApp.Utilities.Enums;
using ProniaApp.Utilities.Extensions;

namespace ProniaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        public readonly AppDBContext _context;
        public readonly IWebHostEnvironment _env;
        public SlideController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = _context.Slides.ToList();
            return View(slides);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateVM vm)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!vm.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type must be image");
                return View();
            }

            if (!vm.Photo.ValidateSize(2, FileSize.Mb))
            {
                ModelState.AddModelError("Photo", "File size must be max 200KB");
                return View();
            }

            Slide slide = new Slide
            {
                Name = vm.Name,
                Description = vm.Description,
                Order = vm.Order,
                Discount = vm.Discount,
                ImageUrl = await vm.Photo.SaveFileAsync(_env.WebRootPath, "assets", "images", "website-images"),
                IsDeleted = false,
                CreatedAt = DateTime.Now,
            };


            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id <= 0 || id == null) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (existed == null) return NotFound();

            existed.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");

            //birinci sekili silmek lazim
            //ikinci slide i silmek lazim


            _context.Slides.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (existed == null) return NotFound();


            return View(existed);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);


            UpdateVM vm = new UpdateVM
            {
                Name = existed.Name,
                Description = existed.Description,
                Order = existed.Order,
                Discount = existed.Discount,
                ImageUrl = existed.ImageUrl
            };
    

            if (existed == null) return NotFound();


            return View(vm);
             

        }

        [HttpPost]

        public async Task<IActionResult> Update(int? id, UpdateVM updatevm)
        {
            if (id == null || id <= 0) return BadRequest();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(updatevm);
            }

            if (updatevm.Photo != null) {

                if (!updatevm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }

                if (!updatevm.Photo.ValidateSize(2, FileSize.Mb))
                {
                    ModelState.AddModelError("Photo", "File size must be max 200KB");
                    return View();
                }


               slide.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");

               slide.ImageUrl=await updatevm.Photo.SaveFileAsync(_env.WebRootPath, "assets", "images", "website-images");

            }


            slide.Name = updatevm.Name;
            slide.Order = updatevm.Order;
            slide.Description = updatevm.Description;
            slide.Discount = updatevm.Discount;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
