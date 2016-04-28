using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LeaveRequestApp.DAL;
using LeaveRequestApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaveRequestApp.Web.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();

        // GET: History
        public ActionResult Index(int page = 0)
        {
            ViewBag.Title = "History";
            if (page == 1)
                return Redirect(Site.CurrentSite.Url + "/history");

            if (page == 0)
                page = 1;

            var manager = new UserManager<Employees>(new UserStore<Employees>(new LeaveContext()));
            var user = manager.FindById(User.Identity.GetUserId());

            bool isUser = Roles.IsUserInRole("User");
            PagedModel<RequestViewModel> result = _uow.RequestRepository.GetRequests(page, 5, isUser, (user.FirstName + " " + user.LastName).Trim());
            return View(result);
        }
    }
}