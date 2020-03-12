using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AffiliateMarketing.Areas.Publisher
{
    public class GetUserDetailAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            var controller = context.Controller as Controller;
            if (controller != null)
            {
                controller.ViewData["name"] = context.HttpContext.User.Identity.Name;
                controller.ViewData["image"] = "/Panel/Person.jpg";
            }
        }
    }
}