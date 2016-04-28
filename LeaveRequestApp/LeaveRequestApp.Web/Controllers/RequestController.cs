using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveRequestApp.DAL;
using LeaveRequestApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaveRequestApp.Web.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();
        private LeaveContext context = new LeaveContext();

        // GET: Request
        public ActionResult Index(int? id)
        {
            ViewBag.Title = "Edit Request";
            var manager = new UserManager<Employees>(new UserStore<Employees>(new LeaveContext()));

            if (id != null)
            {
                var model = _uow.RequestRepository.GetByID(id);
                if (model == null)
                    return Redirect(Site.CurrentSite.Url + "/Error/NotFound");

                Employees employee = context.Users.Where(x => x.EmployeeID == model.EmployeeID).FirstOrDefault();
                var user = manager.FindById(employee.Id);
                ViewBag.User = (user.FirstName + " " + user.LastName).Trim();

                return View(model);
            }
            else
            {
                var user = manager.FindById(User.Identity.GetUserId());
                ViewBag.User = (user.FirstName + " " + user.LastName).Trim();

                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(Requests model)
        {
            try
            {
                if (model.ID == 0)
                {
                    var manager = new UserManager<Employees>(new UserStore<Employees>(new LeaveContext()));
                    var user = manager.FindById(User.Identity.GetUserId());

                    var qStatus = _uow.RequestStatusRepository.Get(x => x.Name == "Pending").FirstOrDefault();
                    var qYear = _uow.YearRepository.Get(x => x.Year == DateTime.Now.Year).FirstOrDefault();

                    model.EmployeeID = user.EmployeeID;
                    model.RequestStatusID = qStatus.ID; // Pending
                    model.year = qYear.ID;
                    _uow.RequestRepository.AddOrUpdate(model);
                    _uow.Save();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _uow.RequestRepository.AddOrUpdate(model);
                    _uow.Save();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}