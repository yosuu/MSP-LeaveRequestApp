using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveRequestApp.DAL;
using LeaveRequestApp.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaveRequestApp.Web.Controllers
{
    [Authorize]
    public class MasterController : Controller
    {
        private LeaveContext context;
        private UnitOfWork _uow = new UnitOfWork();
        private ApplicationUserManager _userManager;

        public MasterController()
        {
            context = new LeaveContext();
        }

        public MasterController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region YEAR
        public ActionResult Year(int page = 0)
        {
            ViewBag.Title = "Year";
            if (page == 1)
                return Redirect(Site.CurrentSite.Url + "/master/year");

            if (page == 0)
                page = 1;

            PagedModel<Years> result = _uow.YearRepository.GetYears(page, 2);
            return View(result);
        }

        public ActionResult EditYear(int? id)
        {
            if (id != null)
            {
                var model = _uow.YearRepository.GetByID(id);
                if (model == null)
                    return Redirect(Site.CurrentSite.Url + "/Error/NotFound");

                ViewBag.Title = String.Format("Edit Year - {0}", model.Year);
                return View(model);
            }
            else
            {
                ViewBag.Title = "New Year";
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditYear(Years model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _uow.YearRepository.AddOrUpdate(model);
                _uow.Save();
                return RedirectToAction("Year");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region STATUS
        public ActionResult Status(int page = 0)
        {
            ViewBag.Title = "Request Status";
            if (page == 1)
                return Redirect(Site.CurrentSite.Url + "/master/requeststatus");

            if (page == 0)
                page = 1;

            PagedModel<RequestStatus> result = _uow.RequestStatusRepository.GetRequestStatus(page, 2);
            return View(result);
        }

        public ActionResult EditRequestStatus(int? id)
        {
            if (id != null)
            {
                var model = _uow.RequestStatusRepository.GetByID(id);
                if (model == null)
                    return Redirect(Site.CurrentSite.Url + "/Error/NotFound");

                ViewBag.Title = String.Format("Edit Request Status - {0}", model.Name);
                return View(model);
            }
            else
            {
                ViewBag.Title = "New Request Status";
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditRequestStatus(RequestStatus model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _uow.RequestStatusRepository.AddOrUpdate(model);
                _uow.Save();
                return RedirectToAction("Status");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region DEPARTMENT
        public ActionResult Department(int page = 0)
        {
            ViewBag.Title = "Department";
            if (page == 1)
                return Redirect(Site.CurrentSite.Url + "/master/department");

            if (page == 0)
                page = 1;

            PagedModel<Departments> result = _uow.DepartmentRepository.GetDepartments(page, 3);
            return View(result);
        }

        public ActionResult EditDepartment(int? id)
        {
            if (id != null)
            {
                var model = _uow.DepartmentRepository.GetByID(id);
                if (model == null)
                    return Redirect(Site.CurrentSite.Url + "/Error/NotFound");

                ViewBag.Title = String.Format("Edit Department - {0}", model.Name);
                return View(model);
            }
            else
            {
                ViewBag.Title = "New Department";
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditDepartment(Departments model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _uow.DepartmentRepository.AddOrUpdate(model);
                _uow.Save();
                return RedirectToAction("Department");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region ROLES
        public ActionResult Roles(int page = 0)
        {
            ViewBag.Title = "Roles";
            if (page == 1)
                return Redirect(Site.CurrentSite.Url + "/master/roles");

            if (page == 0)
                page = 1;

            var q = from r in context.Roles
                    orderby r.Name
                    select new RoleViewModel
                    {
                        ID = r.Id,
                        Name = r.Name
                    };

            var data = q.ToPagedList(page, 2);

            PagedModel<RoleViewModel> model = new PagedModel<RoleViewModel>() { MetaData = new LeaveRequestApp.Models.PagedListMetaData(data.GetMetaData()), Data = data, Total = data.TotalItemCount };
            return View(model);
        }

        public ActionResult EditRole(string id)
        {
            ViewBag.Title = "New Role";
            return View();
        }

        [HttpPost]
        public ActionResult EditRole(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                IdentityRole role = new IdentityRole();
                role.Id = Guid.NewGuid().ToString();
                role.Name = model.Name;

                context.Roles.Add(role);
                context.SaveChanges();
                return RedirectToAction("Roles");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region EMPLOYEE
        public ActionResult Employee(int page = 0)
        {
            ViewBag.Title = "Employee";
            if (page == 1)
                return Redirect(Site.CurrentSite.Url + "/master/employee");

            if (page == 0)
                page = 1;

            var q = from u in context.Users
                    orderby u.FirstName
                    select u;

            var data = q.ToPagedList(page, 2);

            PagedModel<Employees> model = new PagedModel<Employees>() { MetaData = new LeaveRequestApp.Models.PagedListMetaData(data.GetMetaData()), Data = data, Total = data.TotalItemCount };
            return View(model);
        }

        public ActionResult EditEmployee(string id)
        {
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            var model = (from e in context.Users
                         where e.Id == id
                         select e).SingleOrDefault();

            if (model == null)
                return Redirect(Site.CurrentSite.Url + "/Error/NotFound");

            ViewBag.Title = String.Format("Edit Employee - {0}", (model.FirstName + " " + model.LastName).Trim());
            return View(model);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employees model)
        {
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                IdentityResult result = UserManager.Update(model);
                return RedirectToAction("Employee");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        public ActionResult Delete(string id, string module)
        {
            string action = "";
            if (module == "D") // Department
            {
                action = "Department";
                var result = _uow.DepartmentRepository.GetByID(Int32.Parse(id));
                if (result == null)
                {
                    return Redirect(Site.CurrentSite.Url + "/Error/NotFound");
                }

                _uow.DepartmentRepository.Delete(result);
                _uow.Save();
            }
            else if (module == "Y")
            {
                action = "Year";
                var result = _uow.YearRepository.GetByID(Int32.Parse(id));
                if (result == null)
                {
                    return Redirect(Site.CurrentSite.Url + "/Error/NotFound");
                }

                _uow.YearRepository.Delete(result);
                _uow.Save();
            }
            else if (module == "S")
            {
                action = "Status";
                var result = _uow.RequestStatusRepository.GetByID(Int32.Parse(id));
                if (result == null)
                {
                    return Redirect(Site.CurrentSite.Url + "/Error/NotFound");
                }

                _uow.RequestStatusRepository.Delete(result);
                _uow.Save();
            }
            else if (module == "L")
            {
                action = "Roles";
                IdentityRole role = context.Roles.Where(x => x.Id == id).SingleOrDefault();
                if (role == null)
                {
                    return Redirect(Site.CurrentSite.Url + "/Error/NotFound");
                }
                else
                {
                    context.Roles.Remove(role);
                    context.SaveChanges();
                }
            }
            else
                return Redirect(Site.CurrentSite.Url + "/Error/NotFound");

            return RedirectToAction(action);
        }
    }
}