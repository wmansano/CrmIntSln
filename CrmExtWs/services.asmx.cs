using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using CrmExtWs.Classes;
using Newtonsoft.Json;

namespace CrmExtWs
{
    /// <summary>
    /// Summary description for services
    /// </summary>
    [WebService(Namespace = "https://www.lethbridgecollege.ca")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class services : WebService
    {

        [WebMethod]
        [DosAttackModule]
        public void submitcookie(string cookie)
        {

            try
            {
                if (!string.IsNullOrEmpty(cookie)) {
                    var submission = JsonConvert.DeserializeObject<cookie>(cookie);

                    if (submission != null) {
                        SubmitCookies SubmitCookie = new SubmitCookies();
                        SubmitCookie.SubmitCookie(submission);
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        [DosAttackModule]
        public void submitform(string shortform)
        {
            try
            {
                if (!string.IsNullOrEmpty(shortform)) {
                    var submission = JsonConvert.DeserializeObject<shortform>(shortform);

                    if (submission != null) {
                        SubmitForms SubmitForm = new SubmitForms();

                        SubmitForm.SubmitLandingForm(submission);
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }


        //[WebMethod]
        //[DosAttackModule]
        //public void submitshortform(string source, string fname, string lname, string email, string uuid)
        //{
        //    try
        //    {
        //        SubmitForms SubmitForm = new SubmitForms();

        //        string EncodedSource = Server.HtmlEncode(source);
        //        string EncodedFirstName = Server.HtmlEncode(fname);
        //        string EncodedLastName = Server.HtmlEncode(lname);
        //        string EncodedEmailAddress = Server.HtmlEncode(email);
        //        string EncodedUUID = Server.HtmlEncode(uuid);

        //        bool IsValid = int.TryParse(EncodedSource, out int n);

        //        if (IsValid)
        //        {
        //            IsValid = Utility.IsValidEmail(EncodedEmailAddress);
        //        }

        //        if (IsValid)
        //        {
        //            IsValid = !string.IsNullOrEmpty(EncodedFirstName);
        //        }

        //        if (IsValid)
        //        {
        //            IsValid = !string.IsNullOrEmpty(EncodedLastName);
        //        }

        //        if (IsValid)
        //        {
        //            IsValid = !string.IsNullOrEmpty(EncodedUUID);
        //        }

        //        if (IsValid)
        //        {
        //            SubmitForm.ShortForm(EncodedSource, EncodedFirstName, EncodedLastName, EncodedEmailAddress, EncodedUUID);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _ = ex.ToString();
        //    }
        //}
    }
}
