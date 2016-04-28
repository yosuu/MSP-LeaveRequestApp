using System;
using System.Web.Mvc;
using LeaveRequestApp.Models;
using System.Configuration;

namespace LeaveRequestApp.Web
{
    public class SiteFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Site.CurrentSite = new Site();
            Site.CurrentSite.Url = ConfigurationManager.AppSettings["CurrentSite"];

            base.OnActionExecuting(filterContext);
        }
    }
}