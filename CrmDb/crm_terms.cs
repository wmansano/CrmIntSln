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
    
    public partial class crm_terms
    {
        public System.Guid term_guid { get; set; }
        public string term_id { get; set; }
        public bool term_deleted { get; set; }
        public string term_name { get; set; }
        public Nullable<System.DateTime> term_created_datetime { get; set; }
        public string term_created_by { get; set; }
        public Nullable<System.DateTime> term_modifed_datetime { get; set; }
        public string term_modified_by { get; set; }
        public Nullable<System.DateTime> term_system_modstamp { get; set; }
        public Nullable<System.DateTime> term_last_viewed_datetime { get; set; }
        public Nullable<System.DateTime> term_last_referenced_datetime { get; set; }
        public string term_account_id { get; set; }
        public Nullable<System.DateTime> term_end_date { get; set; }
        public Nullable<System.DateTime> term_start_date { get; set; }
        public Nullable<decimal> term_grade_period_sequence { get; set; }
        public Nullable<decimal> term_instructional_days { get; set; }
        public string term_parent_term_id { get; set; }
        public string term_type { get; set; }
        public string term_code { get; set; }
        public Nullable<decimal> term_reporting_year { get; set; }
        public Nullable<decimal> term_year { get; set; }
        public Nullable<System.DateTime> term_census_date { get; set; }
        public Nullable<System.DateTime> term_reg_start_date { get; set; }
        public Nullable<System.DateTime> term_reg_end_date { get; set; }
        public Nullable<System.DateTime> term_prereg_start_date { get; set; }
        public Nullable<System.DateTime> term_prereg_end_date { get; set; }
        public Nullable<System.DateTime> term_drop_start_date { get; set; }
        public Nullable<System.DateTime> term_drop_end_date { get; set; }
        public Nullable<System.DateTime> term_drop_grad_reqd_date { get; set; }
        public Nullable<decimal> term_sequence_number { get; set; }
        public bool term_mark_delete { get; set; }
        public Nullable<System.DateTime> last_sfsync_datetime { get; set; }
        public Nullable<bool> term_show_web { get; set; }
        public string term_name_alias { get; set; }
    }
}