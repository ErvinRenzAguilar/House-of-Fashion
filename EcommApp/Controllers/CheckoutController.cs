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
            if (item.quantity == 0)
                db.cart_items.Remove(item);

            db.SaveChanges();
            return RedirectToAction("Cart", "Checkout");

        }

        public ActionResult ApplyCoupon()
        {
            if (Session["user_id"] != null)
            {

                return RedirectToAction("Cart", "Checkout");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult ApplyCoupon(coupon coup)
        {
            if (Session["user_id"] != null)
            {
                if (coup.coup_code != null)
                {
                    int user_id = Convert.ToInt32(Session["user_id"]);
                    int cart_id = Convert.ToInt32((from x in db.carts
                                                   where (x.user_id == user_id)
                                                   select x.cart_id).Single());

                    //retrieve coupon code
                    var cpn = db.coupons.SingleOrDefault(c => c.coup_code == coup.coup_code);

                    if (cpn != null)
                    {
                        //store coupon details
                        String cat = cpn.category.ToString();
                        decimal disc_pct = Convert.ToDecimal(cpn.disc_pct);
                        decimal discount = 0;

                        //retrieve associated event
                        var ev = db.events.SingleOrDefault(e => e.ev_id == cpn.event_id);

                        //check if current date is valid for event
                        if (DateTime.Now >= ev.start_date && DateTime.Now <= ev.end_date)
                        {
                            //retrieve list of cart items
                            var cartQuery = from p in db.cart_items
                                            where p.cart_id == cart_id
                                            select p;

                            List<cart_items> items = cartQuery.ToList();
                            for (int i = 0; i < items.Count; i++)
                            {
                                //find specific cart item in products table and check its category
                                cart_items item = items[i];
                                var prod = db.products.Find(item.prod_id);
                                if (string.Equals(prod.product_cat, cat))
                                {
                                    //change cart item price
                                    if (prod.price == item.price)
                                    {
                                        discount = item.price * (disc_pct / 100);
                                        item.price -= discount;
                                    }

                                }
                            }
                            db.SaveChanges();
                        }
                        //if coupon code is invalid for specific event time
                        else
                        {
                            TempData["ErrorMessage"] = "This coupon code is no longer valid.";
                            return RedirectToAction("Cart", "Checkout");
                        }
                    }
                    //if coupon code does not exist
                    else
                    {
                        TempData["ErrorMessage"] = "This coupon code does not exist.";
                        return RedirectToAction("Cart", "Checkout");

                    }
                }
                return RedirectToAction("Cart", "Checkout");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult Payment()
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

                //retrieve cart items in current user's cart
                IEnumerable<cart_items> items = query.ToList();
                ViewData["items"] = items;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult Payment(order placedOrder)
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


                //retrieve cart items in current user's cart
                IEnumerable<cart_items> items = query.ToList();
                decimal total = 0;
                foreach (var item in items)
                {
                    //find product and update stock quantity
                    var prod = db.products.Find(item.prod_id);
                    prod.stock -= item.quantity;

                    //compute grand total
                    decimal subtotal = item.price * item.quantity;
                    total += subtotal;

                    //then remove item from cart
                    db.cart_items.Remove(item);
                }

                //saving order details
                placedOrder.user_id = user_id;
                placedOrder.cart_id = cart_id;
                placedOrder.grand_total = total + 50;
                db.orders.Add(placedOrder);


                db.SaveChanges();
                TempData["ConfirmationMessage"] = "Your order has been placed!";
                return RedirectToAction("Cart", "Checkout");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}