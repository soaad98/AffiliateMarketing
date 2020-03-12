using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AffiliateMarketing.Areas.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateMarketing.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [GetUserDetail]
    public class AdminBaseController : Controller
    {
      
    }
}