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

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ApplicationDbContext db;

        public AccountController(RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            this.db = applicationDbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Panel()
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return Redirect("/Admin");
            }
            else if (await _userManager.IsInRoleAsync(user, "Marchant"))
            {
                return Redirect("/Marchant");
            }
            else if (await _userManager.IsInRoleAsync(user, "Publisher"))
            {
                return Redirect("/Publisher");
            }

            return RedirectToAction("AccessDenied");
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
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user.Active)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {

                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return Redirect("/Admin");
                        }
                        else if (await _userManager.IsInRoleAsync(user, "Publisher"))
                        {
                            return Redirect("/Publisher");
                        }
                        else if (await _userManager.IsInRoleAsync(user, "Marchant"))
                        {
                            return Redirect("/Marchant");
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
                else
                {
                    ModelState.AddModelError(string.Empty, "هدا المستخدم غير فعال, يرجى التواصل مع الادارة");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        public IActionResult PubRegister()
        {
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
                    Active = true,
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

                    return Redirect("/Publisher");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        [HttpGet]
        public IActionResult MarRegister()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> MarRegister(MarRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mUser = new Marchant()
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = model.CompanyName,
                    ContactName = model.ContactName

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
                    Active = true,
                    Marchant = mUser
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _userManager.AddToRoleAsync(user, "Marchant");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Redirect("/Marchant");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }


        [HttpGet]
        public IActionResult AccessDenied()
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
                await _roleManager.CreateAsync(new ApplicationRole("Admin"));

                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@admin.com",
                    Active = true,
                };

                var result = await _userManager.CreateAsync(user, "P@ss123");

                Msg += "add Admin role, ";

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    Msg += "add admin user, ";
                }
            }

            if (!await _roleManager.RoleExistsAsync("Marchant"))
            {
                await _roleManager.CreateAsync(new ApplicationRole("Marchant"));
                Msg += "add Marchant role, ";

                var mUser = new Marchant()
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "aaa",
                    ContactName = "aaa"

                };

                var user = new ApplicationUser
                {
                    UserName = "Marchant",
                    Email = "Marchant@Marchant.com",
                    PhoneNumber = "123",
                    zip = 123,
                    Active = true,
                    Marchant = mUser
                };

                var result = await _userManager.CreateAsync(user, "P@ss123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Marchant");
                    Msg += "add Marchant user, ";
                }

            }

            if (!await _roleManager.RoleExistsAsync("Publisher"))
            {
                await _roleManager.CreateAsync(new ApplicationRole("Publisher"));
                Msg += "add Publisher role ";

                var pUser = new Publisher()
                {
                    Id = Guid.NewGuid().ToString(),
                };

                var user = new ApplicationUser
                {
                    UserName = "publisher",
                    Email = "publisher@publisher.com",
                    PhoneNumber = "1234",
                    zip = 123,
                    Active = true,
                    Publisher = pUser
                };

                var result = await _userManager.CreateAsync(user, "P@ss123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Publisher");
                    Msg += "add Publisher user, ";
                }

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