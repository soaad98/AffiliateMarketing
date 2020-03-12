using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AffiliateMarketing.Areas.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AffiliateMarketing.Areas.Publisher.Controllers
{
    [Area("Publisher")]
    [Authorize(Roles = "Publisher")]
    [GetUserDetail]
    public class PublisherBaseController : Controller
    {
       
    }
}