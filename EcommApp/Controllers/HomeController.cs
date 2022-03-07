using EcommApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommApp.Controllers
{
    public class HomeController : Controller
    {

        EcommDBEntities db = new EcommDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shop()
        {
            var query = from p in db.products
                        orderby p.prod_name
                        select p;

            IEnumerable<product> products = query.ToList();

            ViewData["prodlist"] = products;

            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Shirt()
        {
            var query = from p in db.products
                        where p.product_cat == "top"
                        select p;

            IEnumerable<product> products = query.ToList();

            ViewData["prodlist"] = products;

            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }



        }

        public ActionResult Dresses()
        {

            var query = from p in db.products
                        where p.product_cat == "dress"
                        select p;

            IEnumerable<product> products = query.ToList();

            ViewData["prodlist"] = products;
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }



        }
      
        public ActionResult Jeans()
        {

            var query = from p in db.products
                        where p.product_cat == "jeans"
                        select p;

            IEnumerable<product> products = query.ToList();

            ViewData["prodlist"] = products;
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }



        }

        public ActionResult Shorts()
        {
            var query = from p in db.products
                        where p.product_cat == "shorts"
                        select p;

            IEnumerable<product> products = query.ToList();

            ViewData["prodlist"] = products;
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult Swim()
        {
            var query = from p in db.products
                        where p.product_cat == "swim"
                        select p;

            IEnumerable<product> products = query.ToList();

            ViewData["prodlist"] = products;

            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }

        public ActionResult AboutUs()
        {
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


    }
}