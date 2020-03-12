using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AffiliateMarketing.Areas.Marchant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AffiliateMarketing.Areas.Marchant.Controllers
{
    [Area("Marchant")]
    [Authorize(Roles = "Marchant")]
    [GetUserDetail]
    public class MarchantBaseController : Controller
    {
    }
}