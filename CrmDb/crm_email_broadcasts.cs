//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace lc.crm
{
    using System;
    using System.Collections.Generic;
    
    public partial class crm_email_broadcasts
    {
        public System.Guid email_broadcast_guid { get; set; }
        public string email_broadcast_id { get; set; }
        public string email_campaign_id { get; set; }
        public string email_broadcast_name { get; set; }
        public string email_broadcast_status { get; set; }
        public Nullable<double> email_broadcast_messages_sent { get; set; }
        public string email_broadcast_email_campaign { get; set; }
        public bool email_broadcast_deleted { get; set; }
        public string email_broadcast_created_by { get; set; }
        public Nullable<System.DateTime> email_broadcast_created_datetime { get; set; }
        public string email_broadcast_modified_by { get; set; }
        public Nullable<System.DateTime> email_broadcast_modfied_datetime { get; set; }
        public Nullable<System.DateTime> email_broadcast_sys_modstamp { get; set; }
        public Nullable<System.DateTime> email_broadcast_sent { get; set; }
        public string email_report_id { get; set; }
        public string email_report_name { get; set; }
        public string email_template_id { get; set; }
        public string email_template_name { get; set; }
        public bool email_broadcast_mark_delete { get; set; }
        public Nullable<System.DateTime> email_broadcast_send { get; set; }
        public Nullable<System.DateTime> last_sfsync_datetime { get; set; }
        public string email_broadcast_department { get; set; }
    }
}
