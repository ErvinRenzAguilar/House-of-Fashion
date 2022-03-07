using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommApp.Models;

namespace EcommApp.Controllers
{
    public class ProductController : Controller
    {
        EcommDBEntities db = new EcommDBEntities();
        public ActionResult Details(int? id)
        {
            product p = db.products.Find(id);

            ViewData["details"] = p;
            if (Session["user_id"] != null)
            {
                if (ViewData["details"] != null)
                    return View();
                else
                    return RedirectToAction("Shop", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}