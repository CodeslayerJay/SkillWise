using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillWise.Models;
using SkillWise.Models.Services;
using SkillWise.Models.ViewModels;

namespace SkillWise.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IUserService _userService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        
        // Get: /SignUp
        public IActionResult SignUp()
        {
            // User is already logged in
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        // Post: SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var user = _userService.BuildUser(viewModel);

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);

                    return RedirectToAction("Index", "Dashboard");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(viewModel);
            }
                        
            return View();

        }

        // Get: SignIn
        public ActionResult SignIn()
        {
            // User is already logged in
            if( _signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        // Post: SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, isPersistent: false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }


        // Post: SignOut
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }

        // Get: Details
        [Authorize]
        public async Task<IActionResult> Details()
        {
            var model = await _signInManager.UserManager.GetUserAsync(User);
            return View(model);
        }
    }
}