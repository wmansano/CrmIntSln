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
    
    public partial class crm_applications
    {
        public System.Guid application_guid { get; set; }
        public string crm_application_id { get; set; }
        public string crm_appl_number { get; set; }
        public string crm_contact_id { get; set; }
        public string sis_student_id { get; set; }
        public string sis_application_id { get; set; }
        public string appl_stage { get; set; }
        public string application_status { get; set; }
        public Nullable<System.DateTime> appl_status_date { get; set; }
        public string intended_start_term { get; set; }
        public string intended_start_year { get; set; }
        public string intended_student_load { get; set; }
        public string appl_location { get; set; }
        public Nullable<System.DateTime> alt_status_date { get; set; }
        public Nullable<System.DateTime> app_status_date { get; set; }
        public Nullable<System.DateTime> con_status_date { get; set; }
        public Nullable<System.DateTime> dtc_status_date { get; set; }
        public Nullable<System.DateTime> dac_status_date { get; set; }
        public Nullable<System.DateTime> fi_status_date { get; set; }
        public Nullable<System.DateTime> fw_status_date { get; set; }
        public Nullable<System.DateTime> ms_status_date { get; set; }
        public Nullable<System.DateTime> ntq_status_date { get; set; }
        public Nullable<System.DateTime> ofc_status_date { get; set; }
        public Nullable<System.DateTime> offer_due_date { get; set; }
        public Nullable<System.DateTime> ofi_status_date { get; set; }
        public Nullable<System.DateTime> par_status_date { get; set; }
        public Nullable<System.DateTime> ppr_status_date { get; set; }
        public Nullable<System.DateTime> pas_status_date { get; set; }
        public Nullable<System.DateTime> w_status_date { get; set; }
        public Nullable<System.DateTime> pr_status_date { get; set; }
        public Nullable<System.DateTime> sc_status_date { get; set; }
        public Nullable<System.DateTime> unc_status_date { get; set; }
        public Nullable<System.DateTime> wtl_status_date { get; set; }
        public Nullable<System.DateTime> wap_status_date { get; set; }
        public string appl_created_by { get; set; }
        public Nullable<System.DateTime> appl_created_date { get; set; }
        public string appl_modified_by { get; set; }
        public Nullable<System.DateTime> appl_modfied_date { get; set; }
        public string appl_affiliation { get; set; }
        public string crm_program_id { get; set; }
        public string appl_owner_id { get; set; }
        public bool application_deleted { get; set; }
        public string appl_admit_status { get; set; }
        public bool appl_mark_delete { get; set; }
        public Nullable<System.DateTime> last_sfsync_datetime { get; set; }
    }
}