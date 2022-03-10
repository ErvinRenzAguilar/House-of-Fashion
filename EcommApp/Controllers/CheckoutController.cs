using EcommApp.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace EcommApp.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout

        EcommDBEntities db = new EcommDBEntities();
        public ActionResult Cart()
        {
            if (Session["user_id"] != null)
            {
                int user_id = Convert.ToInt32(Session["user_id"]);
                int cart_id = Convert.ToInt32((from x in db.carts
                                               where (x.user_id == user_id)
                                               select x.cart_id).Single());

                var query = from p in db.cart_items
                            where p.cart_id == cart_id
                            select p;

                IEnumerable<cart_items> items = query.ToList();
                ViewData["items"] = items;
                return View();
               
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Delete(int id)
        {
            if (Session["user_id"] != null)
            {
                int user_id = Convert.ToInt32(Session["user_id"]);
                int cart_id = Convert.ToInt32((from x in db.carts
                                               where (x.user_id == user_id)
                                               select x.cart_id).Single());
                var query = (from p in db.cart_items
                            where p.cart_id == cart_id
                            && p.prod_id == id
                            select p).SingleOrDefault();

                cart_items item = query;
                db.cart_items.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Cart", "Checkout");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Plus(int? prod_id)
        {

            int user_id = Convert.ToInt32(Session["user_id"]);

            int cart_id = Convert.ToInt32((from x in db.carts
                                           where (x.user_id == user_id)
                                           select x.cart_id).Single());
            
            
            var query = (from p in db.cart_items
                             where p.cart_id == cart_id
                             && p.prod_id == prod_id
                             select p).SingleOrDefault();
                cart_items item = query;
                item.quantity++;
           

            db.SaveChanges();
            return RedirectToAction("Cart", "Checkout");

        }
        public ActionResult Minus(int? prod_id)
        {

            int user_id = Convert.ToInt32(Session["user_id"]);

            int cart_id = Convert.ToInt32((from x in db.carts
                                           where (x.user_id == user_id)
                                           select x.cart_id).Single());


            var query = (from p in db.cart_items
                         where p.cart_id == cart_id
                         && p.prod_id == prod_id
                         select p).SingleOrDefault();
            cart_items item = query;
           
            item.quantity--;
            if(item.quantity == 0)
              db.cart_items.Remove(item);
            
            db.SaveChanges();
            return RedirectToAction("Cart", "Checkout");

        }
        public ActionResult Payment()
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