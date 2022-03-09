using EcommApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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


        //public ActionResult AddToCart(string prodId, string prod_name, int qty, double price)
        //{
        //    System.Console.WriteLine("Running");

        //    var user_id = Session["user_id"];
        //    var cart_id = db.Database.SqlQuery<int>("select cart_id from carts where user_id = @user_id", new SqlParameter("user_id", user_id));
        //    System.Console.WriteLine(user_id+" "+cart_id);
        //    db.Database.ExecuteSqlCommand("insert into dbo.cart_items(cart_id, prod_id, item_name, quantity, price ) values(@cart_id, 1, @prod_name, @qty, @prod_price)",
        //        new SqlParameter("cart_id", cart_id),
        //        new SqlParameter("prod_id", prodId),
        //        new SqlParameter("prod_name", prod_name),
        //        new SqlParameter("qty", qty),
        //        new SqlParameter("prod_price", price));


        //    return RedirectToAction("Shop", "Home");
        //}
        public ActionResult AddToCart(string prodId, string prodname, int qty, double price)
        {
           
            int user_id = Convert.ToInt32(Session["user_id"]);
            
            int cart_id = Convert.ToInt32((from x in db.carts
                                           where (x.user_id == user_id)
                                           select x.cart_id).Single());

            int prod_id = Convert.ToInt16(prodId);
            int quantity = Convert.ToInt16(qty);


            cart_items items = new cart_items();
            items.cart_id = cart_id;
            items.prod_id = prod_id;
            items.item_name = prodname;
            items.price = Convert.ToDecimal(price);
            items.quantity = qty;
            int check = IsExistingCheck(prod_id);
            if (check == -1)
            {
                db.cart_items.Add(items);
            }
            else
            {
                items.quantity += 1;
                db.Entry(items).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Cart", "Checkout");

        }
        private int IsExistingCheck(int? id)
        {
            int user_id = Convert.ToInt32(Session["user_id"]);
            int cart_id = Convert.ToInt32((from x in db.carts
                                           where (x.user_id == user_id)
                                           select x.cart_id).Single());
            var query = from p in db.cart_items
                        where p.cart_id == cart_id
                        select p;

            List<cart_items> items = query.ToList();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].prod_id == id) 
                    return i;
            }
            return -1;
        }
    }
}