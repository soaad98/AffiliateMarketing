using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateMarketing.Areas.Publisher.Controllers
{
    [Area("Publisher")]
    [Authorize(Roles = "Publisher")]
    public class HomeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}