using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CrmCore;

namespace CrmExtForms
{
    public class RCIREventItem
    {

        public RCIREventItem()
        {
            SetOptions();
        }

        public bool SetOptions()
        {
            bool success = false;
            //using CrmCoreLogic logic = new CrmCoreLogic();

            return success;
        }

        [Required]
        [Display(Name = "EventId")]
        public string EventId { get; set; }

        [Required]
        [Display(Name = "Event Name: ")]
        public string EventName { get; set; }
    }

}
