using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//--
using EcommApp.Models;
//

namespace EcommApp.Controllers
{
    public class AdminController : Controller
    {
        //--
        private EcommDBEntities db = new EcommDBEntities();
        //

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageProducts()
        {
            //-- Commented out since I dont know the admin login details
            if (Session["admin_id"] != null)
            {
                var prod = db.products; //.Include(p => p.CategoryTest)
                return View(prod.ToList());
                //return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        // GET
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

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct([Bind(Include = "prod_id,prod_name,prod_desc,price,stock,prod_image")] product prod)
        {
            if (Session["admin_id"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.products.Add(prod);
                    db.SaveChanges();
                    return RedirectToAction("ManageProducts");
                }
                return View(prod);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        // GET
        public ActionResult EditProduct(int? id)
        {
            if (Session["admin_id"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                product prod = db.products.Find(id);
                if (prod == null)
                {
                    return HttpNotFound();
                }

                ViewBag.product = prod;
                return View(prod);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "prod_id,prod_name,prod_desc,price,stock,prod_image")] product prod)
        {
            if (Session["admin_id"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(prod).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ManageProducts");
                }
                return View(prod);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        // GET
        public ActionResult DeleteProduct(int? id)
        {
            if (Session["admin_id"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                product prod = db.products.Find(id);
                if (prod == null)
                {
                    return HttpNotFound();
                }

                ViewBag.product = prod;
                return View(prod);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }
        // POST
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["admin_id"] != null)
            {
                product prod = db.products.Find(id);
                db.products.Remove(prod);
                db.SaveChanges();
                return RedirectToAction("ManageProducts");
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }
        //
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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