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
    
    public partial class wv_course_connections
    {
        public string course_offering_id { get; set; }
        public string course_connection_contact_id { get; set; }
        public string course_connection_program_enrollment_id { get; set; }
        public string course_connection_program_id { get; set; }
        public string course_connection_acad_program { get; set; }
        public string course_connection_record_type { get; set; }
        public string course_connection_status { get; set; }
        public string course_connection_name { get; set; }
        public Nullable<System.DateTime> contact_last_modified_datetime { get; set; }
        public Nullable<System.DateTime> term_modified_datetime { get; set; }
        public Nullable<System.DateTime> course_offering_modified_datetime { get; set; }
        public Nullable<System.DateTime> sis_stu_course_mod_date { get; set; }
    }
}