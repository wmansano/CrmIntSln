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
    
    public partial class crm_inquiries
    {
        public System.Guid inquiry_guid { get; set; }
        public string inquiry_id { get; set; }
        public string inq_contact_id { get; set; }
        public string inq_owner_id { get; set; }
        public bool inq_deleted { get; set; }
        public string inq_anticipated_start_term { get; set; }
        public string inq_pri_prog_interest { get; set; }
        public string inq_sec_prog_interest { get; set; }
        public string inq_services_interest { get; set; }
        public string inq_campus { get; set; }
        public string inq_last_school { get; set; }
        public string inq_source { get; set; }
        public string inq_student_type { get; set; }
        public Nullable<System.DateTime> inq_email_optin_date { get; set; }
        public bool inq_agent_flag { get; set; }
        public bool inq_agent_prev_flag { get; set; }
        public string inq_agency_name { get; set; }
        public Nullable<bool> inq_agent_rep_prospect { get; set; }
        public Nullable<System.DateTime> inq_created_datetime { get; set; }
        public string inq_created_by { get; set; }
        public Nullable<System.DateTime> inq_modified_datetime { get; set; }
        public string inq_modified_by { get; set; }
        public Nullable<System.DateTime> inq_last_activity_date { get; set; }
        public Nullable<System.DateTime> inq_last_viewed_datetime { get; set; }
        public Nullable<System.DateTime> inq_last_referenced_datetime { get; set; }
        public Nullable<System.DateTime> inq_system_modstamp { get; set; }
        public string lc_inq_legacy_id { get; set; }
        public string inq_contact_legacy_id { get; set; }
        public string inq_anticipated_start_term_code { get; set; }
        public string inq_number { get; set; }
        public Nullable<System.DateTime> inquiry_datetime { get; set; }
        public bool inquiry_mark_delete { get; set; }
        public bool inq_no_communications { get; set; }
        public Nullable<System.DateTime> last_sfsync_datetime { get; set; }
    }
}
