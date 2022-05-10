using CrmCore;
using CrmCore.Models;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;

namespace CrmExtForms.Controllers
{
    public class RCRController : Controller
    {
        private readonly CrmCoreLogic CrmCore = new CrmCoreLogic();

        public ActionResult IR(RCRIRSDataModel Model = null)
        {

            RCRIRSFilterModel Filter = new RCRIRSFilterModel();

            if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(Request.QueryString[RequestParameters.UtmSource])))
            {
                Filter.EncodedSource = WebUtility.HtmlEncode(Request.QueryString[RequestParameters.UtmSource]);
            }

            if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(Request.QueryString[RequestParameters.InquiryId])))
            {
                Filter.EncodedInquiryId = WebUtility.HtmlEncode(Request.QueryString[RequestParameters.InquiryId]);
            }

            if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(Request.QueryString[RequestParameters.ProgramId])))
            {
                Filter.EncodedProgramId = WebUtility.HtmlEncode(Request.QueryString[RequestParameters.ProgramId]);

                if (string.IsNullOrWhiteSpace(Filter.EncodedSource)) { Filter.EncodedSource = FormSourceTypes.rcrirs4; }
            }

            if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(Request.QueryString[RequestParameters.EnableIframe])))
            {
                Filter.EnableIFrame = WebUtility.HtmlEncode(Request.QueryString[RequestParameters.EnableIframe]).Trim().ToLower() == "1" ||
                                WebUtility.HtmlEncode(Request.QueryString[RequestParameters.EnableIframe]).Trim().ToLower() == "true";
            }

            Filter.SaveRCRForm(Model);

            if (Filter.Saved)
            {
                return RedirectToAction("Submitted", "Forms");
            }
            else {
                return View(Model);
            }
        }
    }
}