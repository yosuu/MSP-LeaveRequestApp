﻿using System.Web;
using System.Web.Mvc;

namespace LeaveRequestApp.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SiteFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
