using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageProducts()
        {
            return View();
        }

        public ActionResult ManageEvents()
        {
            return View();
        }

        public ActionResult ManageCoupons()
        {
            return View();
        }
    }
}