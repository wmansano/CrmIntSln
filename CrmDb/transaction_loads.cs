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
    
    public partial class transaction_loads
    {
        public System.Guid transaction_load_guid { get; set; }
        public string transaction_load_name { get; set; }
        public Nullable<System.DateTime> transaction_load_start_datetime { get; set; }
        public string transaction_load_start_record { get; set; }
        public string transaction_load_end_record { get; set; }
        public Nullable<System.DateTime> transaction_load_end_datetime { get; set; }
        public string transaction_load_batch_size { get; set; }
        public Nullable<double> transaction_load_run_time { get; set; }
        public System.DateTime transaction_load_created_datetime { get; set; }
        public System.DateTime transaction_load_modified_datetime { get; set; }
    }
}
