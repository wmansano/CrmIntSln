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
    
    public partial class crm_tasks
    {
        public System.Guid activity_guid { get; set; }
        public string activity_id { get; set; }
        public string task_name_id { get; set; }
        public string task_related_to_id { get; set; }
        public string task_subject { get; set; }
        public Nullable<System.DateTime> task_due_date_only { get; set; }
        public string task_status { get; set; }
        public string task_priority { get; set; }
        public bool task_high_priority { get; set; }
        public string task_assigned_to_id { get; set; }
        public string task_description { get; set; }
        public bool task_deleted { get; set; }
        public string task_account_id { get; set; }
        public bool task_closed { get; set; }
        public Nullable<System.DateTime> task_created_datetime { get; set; }
        public string task_created_by { get; set; }
        public Nullable<System.DateTime> task_modified_datetime { get; set; }
        public string task_modified_by { get; set; }
        public Nullable<System.DateTime> task_system_modstamp { get; set; }
        public bool task_archived { get; set; }
        public Nullable<int> task_call_duration { get; set; }
        public string task_call_type { get; set; }
        public string task_call_result { get; set; }
        public string task_call_object_identfier { get; set; }
        public Nullable<System.DateTime> task_reminder_datetime { get; set; }
        public bool task_reminder_set { get; set; }
        public string task_recurrence_activity_id { get; set; }
        public bool task_is_recurrence { get; set; }
        public Nullable<System.DateTime> task_recurrence_start_datetime { get; set; }
        public Nullable<System.DateTime> task_recurrence_end_datetime { get; set; }
        public string task_recurrence_timezone { get; set; }
        public string task_recurrence_type { get; set; }
        public Nullable<int> task_recurrence_interval { get; set; }
        public Nullable<int> task_recurrence_day_of_week_mask { get; set; }
        public Nullable<int> task_recurrence_day_of_month { get; set; }
        public string task_recurrence_instance { get; set; }
        public string task_recurrence_month_of_year { get; set; }
        public string task_repeat_this_task { get; set; }
        public Nullable<System.DateTime> tast_activity_datetime { get; set; }
        public Nullable<System.DateTime> task_completed_datetime { get; set; }
        public string task_owner_id { get; set; }
        public string task_regenerated_type { get; set; }
        public string task_subtype { get; set; }
        public string task_what_id { get; set; }
        public string task_who_id { get; set; }
        public bool task_mark_delete { get; set; }
        public Nullable<System.DateTime> last_sfsync_datetime { get; set; }
    }
}
