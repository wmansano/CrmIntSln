using CrmCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmExtWs.Classes
{
    public class SubmitCookies
    {
        public bool SubmitCookie(cookie cookie) {
            bool success = false;

            try
            {
                string EncodedUUID = HttpContext.Current.Server.HtmlEncode(cookie.uuid);

                bool IsValid = !string.IsNullOrEmpty(EncodedUUID);

                if (IsValid)
                {
                    string EncodedGoogleClientId = "";
                    if (!string.IsNullOrEmpty(cookie.googleClientId)) {
                        EncodedGoogleClientId = HttpContext.Current.Server.HtmlEncode(cookie.googleClientId);
                    }

                    using (CrmCoreLogic logic = new CrmCoreLogic())
                    {
                        foreach (visit v in cookie.visits) {
                            success = logic.SaveCookieHistory(EncodedUUID, v.date, v.page, EncodedGoogleClientId);
                        }
                            
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                success = false;
            }

            return success;
        }
    }

    public class shortform
    {
        public string uuid;
        public string source;
        public string fname;
        public string lname;
        public string email;
        public string googleClientId;
    }
    public class cookie
    {
        public string uuid;
        public string googleClientId;
        public visit[] visits;
    }

    public class visit
    {
        public DateTime date;
        public string page;
    }
}