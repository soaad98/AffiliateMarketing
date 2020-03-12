using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AffiliateMarketing.Areas.Marchant;
using AffiliateMarketing.Areas.Marchant.Models.ViewModels;
using AffiliateMarketing.Data;
using AffiliateMarketing.Models;
using AffiliateMarketing.Models.CustomExtension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateMarketing.Areas.Marchant.Controllers
{
    public class CampaingnsController : MarchantBaseController
    {
        private readonly ApplicationDbContext _db;

        public CampaingnsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult DTIndex(DataTableParameters parameters)
        {

            var campaignsQuery = _db.campaigns.Where(c => c.UserId == User.GetUserId<string>());

            int totalCount = campaignsQuery.Count();

            campaignsQuery = campaignsQuery.Where(c => parameters.Search == null || parameters.Search.Value == null || c.Title.Contains(parameters.Search.Value));

            int filterCount = campaignsQuery.Count();

            var compaingns = campaignsQuery.Select(u => new
            {
                id = u.Id,
                title = u.Title,
                summary = u.Summary,
                description = u.Description,
                point = u.Point,
                imgurl = u.ImgUrl
            })
            //.OrderByDescending(x => x.)
            .Skip(0)
            .Take(10)
            .ToList();

            var result = new
            {
                draw = 10,
                recordsTotal = totalCount,
                recordsFiltered = filterCount,
                data = compaingns
            };

            return Json(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampaignViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(model.Img.FileName);
                if (model.Img != null && model.Img.Length > 0)
                {
                    using (var stream = new FileStream("wwwroot/CampaignUploads/" + fileName, FileMode.Create))
                    {
                        await model.Img.CopyToAsync(stream);
                    }
                }

                var campaign = new Campaign()
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Summary = model.Summary,
                    Description = model.Description,
                    Point = model.Point,
                    ImgUrl = fileName,
                    UserId = User.GetUserId<string>()
                };

                _db.Add(campaign);
                await _db.SaveChangesAsync();

                return RedirectToAction("index");
            }
            //ModelState.AddModelError("", "Can You Fill All Field ,PLEASE ...you are nice!");
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _db.campaigns.FindAsync(id);

            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

    }
}