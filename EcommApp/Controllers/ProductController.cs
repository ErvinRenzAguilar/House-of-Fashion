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
            if (Session["user_id"] != null)
            {
                return View(p);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}