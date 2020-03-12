using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateMarketing.Areas.Publisher.Controllers
{
    public class HomeController : PublisherBaseController
    {
        public  IActionResult Index()
        {
            return View();
        }
    }
}