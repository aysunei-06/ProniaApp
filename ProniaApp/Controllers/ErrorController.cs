using Microsoft.AspNetCore.Mvc;

namespace ProniaApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
