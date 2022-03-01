using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommApp.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }
    }
}