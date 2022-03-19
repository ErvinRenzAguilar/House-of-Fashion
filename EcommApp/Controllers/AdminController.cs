using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

        // Products CRUD
        public ActionResult ManageProducts()
        {
            if (Session["admin_id"] != null)
            {
                var prod = db.products; //.Include(p => p.CategoryTest)
                return View(prod.ToList());
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
        public ActionResult AddProduct([Bind(Include = "prod_id,prod_name,prod_desc,price,stock,prod_image,product_cat, imgFile")] product prod)
        {
            if (Session["admin_id"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (prod.imgFile != null)
                        {
                            //Added for product image
                            string fileName = Path.GetFileNameWithoutExtension(prod.imgFile.FileName);
                            string extension = Path.GetExtension(prod.imgFile.FileName);
                            fileName = fileName + extension;
                            prod.prod_image = fileName;
                            fileName = Path.Combine(Server.MapPath("~/Media/"), fileName);
                            prod.imgFile.SaveAs(fileName);
                        }

                        db.products.Add(prod);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
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
        public ActionResult EditProduct([Bind(Include = "prod_id,prod_name,prod_desc,price,stock,prod_image,product_cat, imgFile")] product prod)
        {
            if (Session["admin_id"] != null)
            {
                if (ModelState.IsValid)
                {
                    if(prod.imgFile != null)
                    {
                        //Added for product image
                        string fileName = Path.GetFileNameWithoutExtension(prod.imgFile.FileName);
                        string extension = Path.GetExtension(prod.imgFile.FileName);
                        fileName = fileName + extension;
                        prod.prod_image = fileName;
                        fileName = Path.Combine(Server.MapPath("~/Media/"), fileName);
                        prod.imgFile.SaveAs(fileName);
                    }

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

        // End Products CRUD

        //----------------------------------------------------------------------

        // Events CRUD

        public ActionResult ManageEvents()
        {
            if (Session["admin_id"] != null)
            {
                var events = db.events; //.Include(p => p.CategoryTest)
                return View(events.ToList());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        // GET
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
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEvent([Bind(Include = "ev_id,ev_name,start_date,end_date")] @event evt)
        {
            if (Session["admin_id"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.events.Add(evt);
                    db.SaveChanges();
                    return RedirectToAction("ManageEvents");
                }

                return View(evt);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
            
        }

        // GET
        public ActionResult EditEvent(int? id)
        {
            if (Session["admin_id"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                @event evt = db.events.Find(id);
                if (evt == null)
                {
                    return HttpNotFound();
                }

                ViewBag.evt = evt;
                return View(evt);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent([Bind(Include = "ev_id,ev_name,start_date,end_date")] @event evt)
        {
            if (Session["admin_id"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(evt).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ManageEvents");
                }
                return View(evt);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        //GET
        public ActionResult DeleteEvent(int? id)
        {
            if (Session["admin_id"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                @event evt = db.events.Find(id);
                if (evt == null)
                {
                    return HttpNotFound();
                }

                ViewBag.evt = evt;
                return View(evt);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }
        // POST
        [HttpPost]//, ActionName("DeleteEvent")
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(int id)
        {
            if (Session["admin_id"] != null)
            {
                @event evt = db.events.Find(id);
                //find coupons related to event
                List<int> couponsToDelete = db.coupons.Where(c => c.event_id == id).Select(c => c.coup_id).ToList();
                foreach (int coupon in couponsToDelete)
                {
                    DeleteCoupon(coupon);
                }
                //
                db.events.Remove(evt);
                db.SaveChanges();
                return RedirectToAction("ManageEvents");
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        //End Events CRUD

        //-----------------------------------------------------------------------------------------

        // Coupons CRUD
        public ActionResult ManageCoupons()
        {
            if (Session["admin_id"] != null)
            {
                var coupons = db.coupons.Include(p => p.@event);
                return View(coupons.ToList());
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        // GET
        public ActionResult AddCoupon()
        {
            if (Session["admin_id"] != null)
            {
                ViewBag.ev_id = new SelectList(db.events, "ev_id", "ev_name");
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
        public ActionResult AddCoupon([Bind(Include = "coup_id,event_id,disc_pct,coup_code,category")] coupon cpn)
        {
            if (Session["admin_id"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.coupons.Add(cpn);
                    db.SaveChanges();
                    return RedirectToAction("ManageCoupons");
                }

                ViewBag.ev_id = new SelectList(db.events, "ev_id", "ev_name", cpn.event_id);
                return View(cpn);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        public ActionResult EditCoupon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coupon cpn = db.coupons.Find(id);
            if (cpn == null)
            {
                return HttpNotFound();
            }
            ViewBag.ev_id = new SelectList(db.events, "ev_id", "ev_name", cpn.event_id);
            return View(cpn);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCoupon([Bind(Include = "coup_id,event_id,disc_pct,coup_code,category")] coupon cpn)
        {
            if (Session["admin_id"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cpn).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ManageCoupons");
                }
                ViewBag.ev_id = new SelectList(db.events, "ev_id", "ev_name", cpn.event_id);
                return View(cpn);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        // GET
        public ActionResult DeleteCoupon(int? id)
        {
            if (Session["admin_id"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                coupon cpn = db.coupons.Find(id);
                if (cpn == null)
                {
                    return HttpNotFound();
                }

                ViewBag.cpn = cpn;
                return View(cpn);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }
        // POST
        [HttpPost]//, ActionName("DeleteEvent")
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCoupon(int id)
        {
            if (Session["admin_id"] != null)
            {
                coupon cpn = db.coupons.Find(id);
                db.coupons.Remove(cpn);
                db.SaveChanges();
                return RedirectToAction("ManageCoupons");
            }
            else
            {
                return RedirectToAction("AdminLogin", "Account");
            }
        }

        //End Coupons CRUD
    }
}