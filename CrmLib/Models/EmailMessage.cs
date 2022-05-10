using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CrmLcLib
{
    //public class EmailMessage : IDisposable
    //{

    //    //AmazonWebApi.AmazonSES amazonSES = new AmazonWebApi.AmazonSES();

    //    //private bool P_IsInitialized = false;
    //    //private string P_Source = string.Empty;
    //    ////private List<Destinations> _ToDestination = null;
    //    //private List<string> P_ToDestination = null;
    //    //private List<string> P_CcDestination = null;
    //    //private List<string> P_BccDestination = null;
    //    //private Dictionary<string, string> _DefaultTemplateData = null;
    //    ////private string _TemplateName = string.Empty;
    //    //private string P_Subject = string.Empty;
    //    //private string P_HtmlBody = string.Empty;
    //    //private string P_TextBody = string.Empty;
    //    //private string P_Response = string.Empty;

    //    public EmailMessage()
    //    {
    //        //P_IsInitialized = true;
    //    }

    //    //public bool SendSingleEmail()
    //    //{
    //    //    bool success = false;
    //    //    try
    //    //    {
    //    //        if (_IsInitialized)
    //    //        {
    //    //            // Check if object is ready to be sent
    //    //            if (string.IsNullOrEmpty(_Source)) { throw new ArgumentException("Attribute cannot be null/empty", "source email address"); }
    //    //            if (!(_ToDestination != null || _ToDestination.Count > 0)) { throw new ArgumentException("Attribute cannot be null/empty", "destination email address"); }
    //    //            if (string.IsNullOrEmpty(_Subject)) { throw new ArgumentException("Attribute cannot be null/empty", "subject"); }
    //    //            if (string.IsNullOrEmpty(_HtmlBody)) { throw new ArgumentException("Attribute cannot be null/empty", "html email body"); }
    //    //            if (string.IsNullOrEmpty(_TextBody)) { throw new ArgumentException("Attribute cannot be null/empty", "text email body"); }

    //    //            // Call Amazon Send Email Web Api
    //    //            //success = amazonSES.SendSingleEmailAmazonSES(this);
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        string msg = ex.ToString();
    //    //        //using (LcCrmLogic lcCrmLogic = new LcCrmLogic())
    //    //        //{
    //    //        //    lcCrmLogic.SaveException(Structs.Project.LcCrmCore, Structs.Class.LcCrmLibrary, "SendEmailObj.Send", "Error", ex.ToString());
    //    //        //}
    //    //    }

    //    //    return success;
    //    //}

    //    //public bool SendBulkEmail()
    //    //{
    //    //    bool success = false;
    //    //    try
    //    //    {
    //    //        if (_IsInitialized)
    //    //        {
    //    //            // Check if object is ready to be sent
    //    //            if (string.IsNullOrEmpty(_Source)) { throw new ArgumentException("Attribute cannot be null/empty", "source email address"); }
    //    //            if (!(_ToDestination != null || _ToDestination.Count > 0)) { throw new ArgumentException("Attribute cannot be null/empty", "destination email address"); }
    //    //            //if (string.IsNullOrEmpty(_TemplateName)) { throw new ArgumentException("Attribute cannot be null/empty", "template name"); }
    //    //            if (string.IsNullOrEmpty(_Subject)) { throw new ArgumentException("Attribute cannot be null/empty", "subject"); }
    //    //            if (string.IsNullOrEmpty(_HtmlBody)) { throw new ArgumentException("Attribute cannot be null/empty", "html email body"); }
    //    //            if (string.IsNullOrEmpty(_TextBody)) { throw new ArgumentException("Attribute cannot be null/empty", "text email body"); }

    //    //            // Call Amazon Send Email Web Api
    //    //            //success = amazonSES.SendBulkEmailAmazonSES(this);  // Send Bulk Email
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        string msg = ex.ToString();
    //    //        //using (LcCrmLogic lcCrmLogic = new LcCrmLogic())
    //    //        //{
    //    //        //    lcCrmLogic.SaveException(Structs.Project.LcCrmCore, Structs.Class.LcCrmLibrary, "SendEmailObj.Send", "Error", ex.ToString());
    //    //        //}
    //    //    }

    //    //    return success;
    //    //}

    //    //public string Source { get { return P_Source; } set { P_Source = value; } }
    //    //public List<string> ToDestination { get { return P_ToDestination; } set { P_ToDestination = value; } }
    //    //public List<string> CcDestination { get { return P_CcDestination; } set { P_CcDestination = value; } }
    //    //public List<string> BccDestination { get { return P_BccDestination; } set { P_BccDestination = value; } }
    //    //public Dictionary<string, string> DefaultTemplateData { get { return _DefaultTemplateData; } set { _DefaultTemplateData = value; } }
    //    ////public string TemplateName { get { return _TemplateName; } set { _TemplateName = value; } }
    //    //public string Subject { get { return P_Subject; } set { P_Subject = value; } }
    //    //public string HtmlBody { get { return P_HtmlBody; } set { P_HtmlBody = value; } }
    //    //public string TextBody { get { return P_TextBody; } set { P_TextBody = value; } }

    //    public void Dispose()
    //    {
    //        //P_IsInitialized = false;
    //        this.Dispose();
    //    }
    //}
}
