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
    
    public partial class crm_email_campaigns
    {
        public System.Guid email_campaign_guid { get; set; }
        public string email_campaign_id { get; set; }
        public bool ec_deleted { get; set; }
        public string ec_report_id { get; set; }
        public string ec_template_id { get; set; }
        public string ec_created_by { get; set; }
        public Nullable<System.DateTime> ec_created_datetime { get; set; }
        public string ec_modified_by { get; set; }
        public Nullable<System.DateTime> ec_modified_datetime { get; set; }
        public Nullable<System.DateTime> ec_system_modstamp { get; set; }
        public Nullable<System.DateTime> ec_end_datetime { get; set; }
        public Nullable<System.DateTime> ec_start_datetime { get; set; }
        public string ec_parent_campaign_id { get; set; }
        public Nullable<double> ec_recur_days { get; set; }
        public Nullable<System.TimeSpan> ec_send_time { get; set; }
        public bool ec_week_days_only_flag { get; set; }
        public string ec_name { get; set; }
        public string ec_department { get; set; }
        public Nullable<bool> ec_send_now { get; set; }
        public bool ec_active_flag { get; set; }
        public string ec_from_email_address_title { get; set; }
        public string ec_from_email_address { get; set; }
        public string ec_email_address_type { get; set; }
        public bool ec_mark_delete { get; set; }
        public string ec_recur_week_days { get; set; }
        public bool ec_recur { get; set; }
        public Nullable<System.DateTime> last_sfsync_datetime { get; set; }
        public Nullable<bool> ec_allow_repeat_broadcasts { get; set; }
        public bool ec_allow_ongoing_delivery { get; set; }
    }
}
