using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmDb
{
    using System;
    using System.Collections.Generic;

    public partial class crm_email_campaigns
    {
        private bool P_ready = false;
        
        public crm_email_campaigns() {

        }

        public bool CheckReadiness()
        {
            bool success = false;

            try
            {

            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return success;
        }

        public List<string> GetReportRecipients(string report_id)
        {
            List<string> _recipients = new List<string>();

            try
            {

            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _recipients;
        }

        public string GetEmailTemplate(string template_id)
        {
            string _htmlTemplate = string.Empty;

            try
            {

            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _htmlTemplate;
        }

        public Dictionary<string, string> GetEmailMergeData(string html)
        {
            Dictionary<string, string> _mergefields = new Dictionary<string, string>();

            try
            {
                // parse html template and return merge fields
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }

            return _mergefields;
        }

        public bool Ready
        {
            get => P_ready;
            set => P_ready = value;
        }
    }
}