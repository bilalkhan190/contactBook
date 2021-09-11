using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ContacBookApp.Helper.ApplicationHelper;

namespace ContacBookApp.Controllers
{
    public class BaseController : Controller
    {
        //we will create a request handler for every ajax request.
        public readonly ContactEntities context;
        public BaseController()
        {
            context = new ContactEntities();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requestURL = filterContext.HttpContext.Request.Url.ToString();
            ViewBag.WebsiteURL = GetContentByKey(context, Website_URL);
            if (IsUserLogin())
            {
               
            }
            else
            {
                filterContext.Result = new RedirectResult("account");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}