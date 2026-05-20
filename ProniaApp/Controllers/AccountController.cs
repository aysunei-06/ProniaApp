using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProniaApp.Models;
using ProniaApp.ViewModels.Account;

namespace ProniaApp.Controllers
{
    public class AccountController : Controller
    {

        public readonly UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);

            }

            AppUser appuser = new AppUser
            {
                Name = vm.FirstName,
                Surname = vm.LastName,
                UserName = vm.UserName,
                Email = vm.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appuser, vm.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            await _signInManager.SignInAsync(appuser, false);

            return RedirectToAction(nameof(HomeController.Index),"Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }    
}
