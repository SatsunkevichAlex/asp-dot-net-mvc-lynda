using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _userManager = userManager;
            _signinManager = signInManager;
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            IdentityUser newUser = new IdentityUser
            {
                Email = registration.EmailAddress,
                UserName = registration.EmailAddress
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, registration.Password);

            if (!result.Succeeded)
            {
                foreach (string error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }

                return View();
            }

            return RedirectToAction("Login");
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signinManager.PasswordSignInAsync(
                login.EmailAddress, login.Password,
                login.RememberMe, false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Login error!");
                return View();
            }

            if (String.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signinManager.SignOutAsync();

            if (String.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }
    }
}