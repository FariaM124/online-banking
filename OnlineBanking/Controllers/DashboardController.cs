using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "RegisterLogin");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AdminIndex()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "RegisterLogin");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "RegisterLogin");
        }

    }
}