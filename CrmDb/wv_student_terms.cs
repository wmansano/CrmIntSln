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
    
    public partial class wv_student_terms
    {
        public string sis_sttr_id { get; set; }
        public string sis_student_id { get; set; }
        public string sis_term_id { get; set; }
        public string sis_sttr_type { get; set; }
        public string sis_sttr_intent_id { get; set; }
        public Nullable<System.DateTime> sis_sttr_reg_date { get; set; }
        public Nullable<System.DateTime> sis_sttr_prereg_date { get; set; }
        public string sis_sttr_printed_comments { get; set; }
        public int sis_sttr_cred_limit_waive_flag { get; set; }
        public int sis_sttr_rehab_dept_client_flag { get; set; }
        public int sis_sttr_tech_prep_flag { get; set; }
        public string sis_stls_course_sec { get; set; }
        public string stls_schedule { get; set; }
        public string stls_student_acad_cred { get; set; }
        public Nullable<decimal> stcc_term_gpa { get; set; }
        public int stcc_term_gpa_flag { get; set; }
        public Nullable<decimal> stcc_trgr_cum_gpa { get; set; }
        public int stcc_trgr_cum_gpa_flag { get; set; }
        public Nullable<System.DateTime> stcc_cc_created_date { get; set; }
        public Nullable<System.DateTime> stcc_modified_date { get; set; }
        public Nullable<System.DateTime> sttr_created_date { get; set; }
        public Nullable<System.DateTime> sttr_modified_date { get; set; }
        public Nullable<long> RN { get; set; }
    }
}