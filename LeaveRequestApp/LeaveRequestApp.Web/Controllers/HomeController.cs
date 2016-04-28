using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveRequestApp.DAL;
using LeaveRequestApp.Models;

namespace LeaveRequestApp.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();

        public ActionResult Index(int page = 0)
        {
            ViewBag.Title = "Dashboard";
            if (page == 1)
                return Redirect(Site.CurrentSite.Url);

            if (page == 0)
                page = 1;

            PagedModel<Requests> result = _uow.RequestRepository.GetTaskList(page, 5);
            return View(result);
        }

        public ActionResult Approve(int id)
        {
            var model = _uow.RequestRepository.GetByID(id);
            if (model == null)
                return Redirect(Site.CurrentSite.Url + "/Error/NotFound");

            model.RequestStatusID = 1;
            _uow.RequestRepository.Update(model);
            _uow.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Reject(int id)
        {
            var model = _uow.RequestRepository.GetByID(id);
            if (model == null)
                return Redirect(Site.CurrentSite.Url + "/Error/NotFound");

            model.RequestStatusID = 11;
            _uow.RequestRepository.Update(model);
            _uow.Save();
            return RedirectToAction("Index");
        }
    }
}