using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrmCore;

namespace CrmIntUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrmCoreLogic CrmCore = new CrmCoreLogic();
        private readonly string sfcs = ConfigurationManager.ConnectionStrings["SFNAPI"].ConnectionString;
        private readonly string dbcs = ConfigurationManager.ConnectionStrings["crmdb_entities"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RunIntegration()
        {
            //CrmCore.RunCrmIntegration(CrmCore.VerifyEnvironment(sfcs, dbcs));
            CrmCore.RunIntegration();
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}