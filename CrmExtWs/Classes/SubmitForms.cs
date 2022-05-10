using CrmCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmExtWs.Classes
{
    public class SubmitForms
    {
        public bool SubmitLandingForm(shortform shortform)
        {
            bool success = false;
            bool IsValid = false;

            try
            {
                string EncodedSource = HttpContext.Current.Server.HtmlEncode(shortform.source);

                IsValid = !string.IsNullOrWhiteSpace(EncodedSource);

                if (IsValid) {

                    string EncodedUUID = HttpContext.Current.Server.HtmlEncode(shortform.uuid);
                    string EncodedFirstName = HttpContext.Current.Server.HtmlEncode(shortform.fname);
                    string EncodedLastName = HttpContext.Current.Server.HtmlEncode(shortform.lname);
                    string EncodedEmailAddress = HttpContext.Current.Server.HtmlEncode(shortform.email);
                    string EncodedGoogleClientId = HttpContext.Current.Server.HtmlEncode(shortform.googleClientId);

                    if (IsValid)
                    {
                        IsValid = Utility.IsValidEmail(EncodedEmailAddress);
                    }

                    if (IsValid)
                    {
                        IsValid = !string.IsNullOrEmpty(EncodedFirstName);
                    }

                    if (IsValid)
                    {
                        IsValid = !string.IsNullOrEmpty(EncodedLastName);
                    }

                    if (IsValid)
                    {
                        IsValid = !string.IsNullOrEmpty(EncodedUUID);
                    }

                    if (IsValid)
                    {
                        using (CrmCoreLogic logic = new CrmCoreLogic())
                        {
                            success = logic.SaveLandingForm(EncodedUUID, EncodedSource, EncodedFirstName, EncodedLastName, EncodedEmailAddress, EncodedGoogleClientId);
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
}