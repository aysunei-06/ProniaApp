
using Microsoft.AspNetCore.Mvc;
using ProniaApp.DAL;

namespace ProniaApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public readonly AppDBContext _context;
        public HomeController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
