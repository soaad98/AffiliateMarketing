using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AffiliateMarketing.Data;
using AffiliateMarketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AffiliateMarketing.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ApplicationDbContext db;

        public UserController(RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            this.db = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TDIndex(int Draw)
        {
            //db.UserRoles.First().
            var users = db.Users.Select(u => new
            {
                username = u.UserName,
                email = u.Email,
                phone = u.PhoneNumber,
                type = u.UserRoles.First().Role.Name,
                active = u.Active
            }).ToList();

            int totalCount = users.Count();

            var result = new
            {
                draw = Draw,
                recordsTotal = totalCount,
                recordsFiltered = totalCount,
                data = users
            };
            return Json(result);
        }

        //to copy from
        //public ActionResult TDIndex(DataTableParameters parameters)
        //{
        //    var categoriesQuery = db.Services
        //        .Where(x => x.ClientId == UserId)
        //        .Where(x => !x.isDelete);

        //    int totalCount = categoriesQuery.Count();

        //    categoriesQuery = categoriesQuery.Where(c => parameters.Search.Value == null || c.Title.Contains(parameters.Search.Value));

        //    int filterCount = categoriesQuery.Count();

        //    var categories = categoriesQuery.Select(x => new
        //    {
        //        x.Id,
        //        x.Title,
        //        SuperCategory = x.Category.Category2.Title,
        //        SubCategory = x.Category.Title,
        //        x.EndDate,
        //        x.Status,
        //        x.Duration,
        //        x.Price
        //    }).AsEnumerable()
        //    .Select(x => new
        //    {
        //        x.Id,
        //        x.Title,
        //        x.SuperCategory,
        //        x.SubCategory,
        //        EndDate = x.EndDate.ToShortDateString(),
        //        Status = ((SERVICE_STATUS)x.Status).GetDisplayName(),
        //        Duration = ((DURATION_TYPE)x.Duration).GetDisplayName(),
        //        x.Price,
        //    })
        //    .OrderByDescending(x => x.Id)
        //    .Skip(parameters.Start)
        //    .Take(parameters.Length)
        //    .ToList();

        //    var result = new
        //    {
        //        draw = parameters.Draw,
        //        recordsTotal = totalCount,
        //        recordsFiltered = filterCount,
        //        data = categories
        //    };
        //    return Json(result);
        //}


    }
}