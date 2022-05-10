using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmDb
{
    public partial class crm_email_messages
    {
        private bool P_Is_initialized = false;

        private System.Guid P_email_message_guid = Guid.NewGuid();

        private string P_DefaultTemplate = null;
        private string P_ReplacementTemplate = null;

        private string P_Response = string.Empty;

        private List<string> P_ToList = null;
        private List<string> P_CcList = null;
        private List<string> P_BccList = null;

        //private string _TemplateName = string.Empty;
        private string P_Subject = string.Empty;
        private string P_HtmlBody = string.Empty;
        private string P_TextBody = string.Empty;

        public crm_email_messages()
        {
            P_Is_initialized = false;
        }

        public bool is_initialized { get { return P_Is_initialized; } set { P_Is_initialized = value; } }
        public string Response { get { return P_Response; } set { P_Response = value; } }

        public List<string> ToList { get { return P_ToList; } set { P_ToList = value; } }
        public List<string> CcList { get { return P_CcList; } set { P_CcList = value; } }
        public List<string> BccList { get { return P_BccList; } set { P_BccList = value; } }
        
        //public string TemplateName { get { return _TemplateName; } set { _TemplateName = value; } }

        public string DefaultTemplate { get { return P_DefaultTemplate; } set { P_DefaultTemplate = value; } }

        public string ReplacementTemplate { get { return P_ReplacementTemplate; } set { P_ReplacementTemplate = value; } }

        public void Dispose()
        {
            P_Is_initialized = false;
            this.Dispose();
        }

        //public void Dispose()
        //{
        //    P_is_initialized = false;
        //    this.Dispose();
        //}
    }
}
