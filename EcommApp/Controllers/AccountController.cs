using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommApp.Models;

namespace EcommApp.Controllers
{
    public class AccountController : Controller
    {
        EcommDBEntities db = new EcommDBEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View(db.users.ToList());

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(user userAccount)
        {
            if (db.users.Any(x => x.email == userAccount.email))
            {
                ViewBag.Message = "This account already exists.";
                return View();
            }
            else
            {
                db.users.Add(userAccount);
                try
                {
                    db.SaveChanges();
                    ViewBag.Message = userAccount.first_name + " " + userAccount.last_name + " has successfully registered!";
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
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user userAccount)
        {
            var user = db.users.SingleOrDefault(u => u.email == userAccount.email && u.password == userAccount.password);
            if (user != null)
            {
                Session["user_id"] = user.user_id.ToString();
                Session["email"] = user.email.ToString();
                return RedirectToAction("Shop", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong!");
            }

            return View();
        }

        public ActionResult Delete(int? user_id)
        {

            var user = db.users.Find(user_id);
            if (user != null)
            {
                db.users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Logout()
        {
            Session.Remove("user_id");
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
    }
}       