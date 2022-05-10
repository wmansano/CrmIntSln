using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrmExtForms.Controllers
{
    public class FormsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Submitted()
        {
            ViewBag.Title = "Thank you!";
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}