using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AffiliateMarketing.Data;
using AffiliateMarketing.Models;
using AffiliateMarketing.Models.Account.LoginViewModel;
using AffiliateMarketing.Models.Account.RegisterViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateMarketing.Controllers
{
    public class AccountController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ApplicationDbContext db;

        public AccountController(RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            this.db = applicationDbContext;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    if (User.IsInRole("Admin"))
                    {
                        Redirect("/Admin");
                    }
                    else if (User.IsInRole("Marchant"))
                    {
                        Redirect("/Marchant");
                    }
                    else if (User.IsInRole("Publisher"))
                    {
                        Redirect("/Publisher/Homee/Index");
                    }
                }
                //if (result.IsLockedOut)
                //{
                //    return RedirectToAction(nameof(Lockout));
                //}
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            //            if (User.IsInRole("Marchant"))
            //{
            //    Redirect("/Marchant");
            //}
            //else if (User.IsInRole("Publisher"))
            //{

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PubRegister(PubRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pUser = new Publisher()
                {
                    Id = Guid.NewGuid().ToString(),
                    FacebookUrl = model.FacebookUrl,
                    InstagramUrl = model.InstagramUrl,
                    YoutubeUrl = model.YoutubeUrl,
                    TwiterUrl = model.TwiterUrl,
                    OtherUrl = model.OtherUrl,
                };

                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PhoneNumber = model.phone,
                    Address = model.Address,
                    Country = model.Country,
                    website = model.website,
                    zip = model.zip,
                    Publisher = pUser
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _userManager.AddToRoleAsync(user, "Publisher");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    Redirect("/Publisher/Homee/Index");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Register");

        }

        [HttpPost]
        public IActionResult MarRegister(MarRegisterViewModel model)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> ConfigRoles()
        {
            string Msg = "";

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

                var user = new ApplicationUser
                {
                    UserName = "Admin@admin.com",
                    Email = "Admin@admin.com",
                };

                var result = await _userManager.CreateAsync(user, "123");

                Msg += "add Admin role, ";

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    Msg += "add admin user, ";
                }

            }

            if (!await _roleManager.RoleExistsAsync("Marchant"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Marchant"));
                Msg += "add Marchant role, ";
            }

            if (!await _roleManager.RoleExistsAsync("Publisher"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Publisher"));
                Msg += "add Publisher role ";
            }

            return Json(new { msg = Msg + " done." });
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

    }
}