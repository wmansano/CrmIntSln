using CrmCore;
using System;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace CrmExtForms.Controllers
{
    public class RCRController : Controller
    {
        private readonly CrmCoreLogic CrmCore = new CrmCoreLogic();

        //[ScriptMethod(UseHttpGet = true)]
        //[WebMethod]
        public ActionResult s1()
        {
            //HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:50343");

            string Source = Server.HtmlEncode(Request["source"]);
            string FirstName = Server.HtmlEncode(Request["FirstName"]);
            string LastName = Server.HtmlEncode(Request["LastName"]);
            string EmailAddress = Server.HtmlEncode(Request["EmailAddress"]);

            CrmCore.Models.ContactLookupDataModel rcrsf = new CrmCore.Models.ContactLookupDataModel();

            if (!string.IsNullOrEmpty(Source))
            {
                rcrsf.Source = (Enumerations.FormSourceTypes)Enum.Parse(typeof(Enumerations.FormSourceTypes), Source);
            }

            if (!string.IsNullOrEmpty(FirstName)) {
                rcrsf.FirstName = FirstName;
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                rcrsf.LastName = LastName;
            }

            if (!string.IsNullOrEmpty(EmailAddress))
            {
                rcrsf.EmailAddress = EmailAddress;
            }

            rcrsf.LookupContact();

            return View(rcrsf);
            
        }

        // GET: Sfad
        public ActionResult sfad()
        {
            //using (CrmCore.CrmCoreLogic ctx = new CrmCore.CrmCoreLogic()) {
            //    var results = ctx.GetTermOptions();
            //}

            RCRDataModel rcir = new RCRDataModel();
            return View(rcir);
        }

        public ActionResult IR(RCRDataModel model = null)
        {

            RCRDataModel RcrIrModel = model ?? new RCRDataModel();

            string EncodedSource = null;
            string EncodedInquiryId = null;

            string EncodedFirstName = null;
            bool EnableFirstName = false;

            string EncodedLastName = null;
            bool EnableLastName = false;

            string EncodedEmailAddress = null;
            bool EnableEmailAddress = false;

            string EncodedStartTerm = null;
            bool EnableStartTerm = false;

            string EncodedProgramInterest = null;
            bool EnableProgramInterest = false;

            string EncodedMailingCity = null;
            bool EnableMailingCity = false;

            string EncodedMailingProv = null;
            bool EnableMailingProv = false;

            string EncodedMobilePhone = null;
            bool EnableMobilePhone = false;

            DateTime? BirthDate = null;
            bool EnableBirthDate = false;

            bool AlreadyApplied = false;
            bool EnableAlreadyApplied = false;

            // for all services as a group
            bool EnableServicesInterest = false;
            bool EnableIndigenous = false;
            bool EnablePriorActivity = false;
            bool saved = false;

            

            if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(Request.QueryString["utm_source"])))
            {
                EncodedSource = Server.HtmlEncode(Request.QueryString["utm_source"]);
            }

            if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(Request.QueryString["inquiry_id"])))
            {
                EncodedInquiryId = Server.HtmlEncode(Request.QueryString["inquiry_id"]);
            }

            switch (EncodedSource)
            {
                case FormSourceTypes.rcrirs1a:
                    EnableMobilePhone = true;
                    EnableBirthDate = true;
                    EnableMailingCity = true;
                    EnableMailingProv = true;
                    EnableProgramInterest = true;
                    EnableStartTerm = true;
                    EnablePriorActivity = true;
                    EnableServicesInterest = true;
                    EnableIndigenous = true;
                    break;
                case FormSourceTypes.rcrirs1b:
                    EnableMailingCity = true;
                    EnableMailingProv = true;
                    EnableStartTerm = true;
                    EnableServicesInterest = true;
                    EnableIndigenous = true;
                    EnablePriorActivity = true;
                    EnableServicesInterest = true;
                    EnableIndigenous = true;
                    break;
                case FormSourceTypes.rcrirs2:
                    EnableMailingCity = true;
                    EnableMailingProv = true;
                    EnableStartTerm = true;
                    EnableServicesInterest = true;
                    EnableIndigenous = true;
                    break;
                case FormSourceTypes.rcrirs1:
                default:
                    EnableFirstName = true;
                    EnableLastName = true;
                    EnableEmailAddress = true;
                    EnableMobilePhone = true;
                    EnableBirthDate = true;
                    EnableMailingCity = true;
                    EnableMailingProv = true;
                    EnableProgramInterest = true;
                    EnableStartTerm = true;
                    EnableAlreadyApplied = true;
                    EnablePriorActivity = true;
                    EnableServicesInterest = true;
                    EnableIndigenous = true;
                    break;
            }

            if (EnableFirstName) { RcrIrModel.EnableFirstName = true; }
            if (EnableLastName) { RcrIrModel.EnableLastName = true; }
            if (EnableEmailAddress) { RcrIrModel.EnableEmailAddress = true; }
            if (EnableMobilePhone) { RcrIrModel.EnableMobilePhone = true; }
            if (EnableBirthDate) { RcrIrModel.EnableBirthDate = true; }
            if (EnableMailingCity) { RcrIrModel.EnableMailingCity = true; }
            if (EnableMailingProv) { RcrIrModel.EnableMailingProv = true; }
            if (EnableProgramInterest) { RcrIrModel.EnableProgramInterest = true; }
            if (EnableStartTerm) { RcrIrModel.EnableStartTerm = true; }
            if (EnableAlreadyApplied) { RcrIrModel.AlreadyApplied = true; }
            if (EnableServicesInterest) { RcrIrModel.EnableServicesInterest = true; }
            if (EnableIndigenous) { RcrIrModel.EnableIndigenous = true; }
            if (EnablePriorActivity) { RcrIrModel.EnablePriorActivity = true; }
            if (EnableServicesInterest) { RcrIrModel.EnableServicesInterest = true; }

            if (RcrIrModel != null && string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.RequiredField))
                                && string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.InputType))
                                && !string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.LastName))) {


                if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.FirstName)))
                {
                    EncodedFirstName = Server.HtmlEncode(RcrIrModel.FirstName);
                }

                if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.LastName)))
                {
                    EncodedLastName = Server.HtmlEncode(RcrIrModel.LastName);
                }

                if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.MobilePhone)))
                {
                    EncodedMobilePhone = Server.HtmlEncode(RcrIrModel.MobilePhone);
                }

                if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.EmailAddress)))
                {
                    EncodedEmailAddress = Server.HtmlEncode(RcrIrModel.EmailAddress);
                }

                if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.StartTerm)))
                {
                    EncodedStartTerm = Server.HtmlEncode(RcrIrModel.StartTerm);
                }

                if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.ProgramInterest)))
                {
                    EncodedProgramInterest = Server.HtmlEncode(RcrIrModel.ProgramInterest);
                }

                if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.MailingCity)))
                {
                    EncodedMailingCity = Server.HtmlEncode(RcrIrModel.MailingCity);
                }

                if (!string.IsNullOrWhiteSpace(Server.HtmlEncode(RcrIrModel.MailingProv)))
                {
                    EncodedMailingProv = Server.HtmlEncode(RcrIrModel.MailingProv);
                }

                if (RcrIrModel.BirthDate != null)
                {
                    BirthDate = RcrIrModel.BirthDate;
                }

                AlreadyApplied = RcrIrModel.AlreadyApplied;

                using (CrmCoreLogic logic = new CrmCoreLogic())
                {
                    saved = logic.SaveShortForm1(EncodedFirstName, EncodedLastName, EncodedEmailAddress, BirthDate, EncodedMobilePhone, EncodedMailingCity, EncodedMailingProv, EncodedStartTerm, EncodedProgramInterest, EncodedSource, EncodedInquiryId);
                }
            }

            if (saved)
            {
                return RedirectToAction("Submitted", "Forms");
            }
            else {
                return View(RcrIrModel);
            }
        }

        public ActionResult test()
        {
            //using (CrmCore.CrmCoreLogic ctx = new CrmCore.CrmCoreLogic()) {
            //    var results = ctx.GetTermOptions();
            //}

            RCRDataModel rcir = new RCRDataModel();
            return View(rcir);
        }

        // GET: Sfad
        public ActionResult RCRCT()
        {
            //using (CrmCore.CrmCoreLogic ctx = new CrmCore.CrmCoreLogic()) {
            //    var results = ctx.GetTermOptions();
            //}

            RCRDataModel rcir = new RCRDataModel();
            return View(rcir);
        }

        // GET: Sfad
        public ActionResult RCRIR()
        {
            //using (CrmCore.CrmCoreLogic ctx = new CrmCore.CrmCoreLogic()) {
            //    var results = ctx.GetTermOptions();
            //}

            RCRDataModel rcir = new RCRDataModel();
            return View(rcir);
        }

        public ActionResult RCRIR1(RCR1DataModel form_data = null   )
        {
            //using (CrmCore.CrmCoreLogic ctx = new CrmCore.CrmCoreLogic()) {
            //    var results = ctx.GetTermOptions();
            //}

            if (form_data == null) {
                form_data = new RCR1DataModel();
            }
            
            return View(form_data);
        }

    }
}