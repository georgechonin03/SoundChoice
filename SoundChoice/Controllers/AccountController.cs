using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoundChoice.Models;
using SoundChoice.Models.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SoundChoice.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser>signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Register post method. Redirects the user to the home page if the process is successful.
        /// </summary>
        /// <returns>The ViewResult object on the basis of the model parameter.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email=model.Email,
                    //Name = model.Name
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            return View(model);
        }
        /// <summary>
        /// Signs out the user of their account.
        /// </summary>
        /// <returns>Redirected to the login page.</returns>
        [HttpPost]
        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        /// <summary>
        /// Login post method. Redirects users to the home page if the process is successful.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The ViewResult object on the basis of the model parameter.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt!");
            }
            return View(model);
        }
    }
}
