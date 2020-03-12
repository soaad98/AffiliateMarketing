using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateMarketing.Areas.Marchant.Controllers
{
    public class HomeController : MarchantBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}