using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaApp.Models;
using ProniaApp.Utilities.Enums;
using ProniaApp.ViewModels.Account;

namespace ProniaApp.Controllers
{
    public class AccountController : Controller
    {

        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        public readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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



            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == vm.UserNameOrEmail || u.Email == vm.UserNameOrEmail);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username or email is incorrect");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, vm.Password, vm.IsPersistent, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "This account has been locked out due to too many failed attempts.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Password is incorrect");
                }
                return View(vm);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach(UserRoles role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
