using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LeaveRequestApp.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Register",
                url: "account/register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "Login",
                url: "account/login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "LogOff",
                url: "account/LogOff",
                defaults: new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                name: "History",
                url: "history",
                defaults: new { controller = "History", action = "Index" }
            );

            routes.MapRoute(
                name: "Request",
                url: "request/{id}",
                defaults: new { controller = "Request", action = "Index", id = UrlParameter.Optional }
            );

            #region Year
            routes.MapRoute(
                name: "Year",
                url: "master/year",
                defaults: new { controller = "Master", action = "Year" }
            );

            routes.MapRoute(
                name: "Edit Year",
                url: "master/edityear/{id}",
                defaults: new { controller = "Master", action = "EditYear", id = UrlParameter.Optional }
            );
            #endregion

            #region Status
            routes.MapRoute(
                name: "Status",
                url: "master/requeststatus",
                defaults: new { controller = "Master", action = "Status" }
            );

            routes.MapRoute(
                name: "Edit Status",
                url: "master/editrequeststatus/{id}",
                defaults: new { controller = "Master", action = "EditRequestStatus", id = UrlParameter.Optional }
            );
            #endregion

            #region Department
            routes.MapRoute(
                name: "Department",
                url: "master/department",
                defaults: new { controller = "Master", action = "Department" }
            );

            routes.MapRoute(
                name: "Edit Department",
                url: "master/editdepartment/{id}",
                defaults: new { controller = "Master", action = "EditDepartment", id = UrlParameter.Optional }
            );
            #endregion

            #region Roles
            routes.MapRoute(
                name: "Roles",
                url: "master/roles",
                defaults: new { controller = "Master", action = "Roles" }
            );

            routes.MapRoute(
                name: "Edit Role",
                url: "master/editrole/{id}",
                defaults: new { controller = "Master", action = "EditRole", id = UrlParameter.Optional }
            );
            #endregion

            #region Employee
            routes.MapRoute(
                name: "Employee",
                url: "master/employee",
                defaults: new { controller = "Master", action = "Employee" }
            );

            routes.MapRoute(
                name: "Edit Employee",
                url: "master/editemployee/{id}",
                defaults: new { controller = "Master", action = "EditEmployee" }
            );
            #endregion

            routes.MapRoute(
                name: "Delete",
                url: "master/delete/{module}/{id}",
                defaults: new { controller = "Master", action = "Delete" }
            );

            routes.MapRoute(
                name: "Approve",
                url: "home/approve/{id}",
                defaults: new { controller = "Home", action = "Approve" }
            );

            routes.MapRoute(
                name: "Reject",
                url: "home/reject/{id}",
                defaults: new { controller = "Home", action = "Reject" }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Error",
                url: "{*url}",
                defaults: new { controller = "Error", action = "NotFound" }
            );

        }
    }
}
