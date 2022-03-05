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
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult AddProduct()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult EditProduct()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult DeleteProduct()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult ManageEvents()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult AddEvent()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult EditEvent()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult DeleteEvent()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult ManageCoupons()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult AddCoupon()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult EditCoupon()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult DeleteCoupon()
        {
            if (Session["admin_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

    }
}