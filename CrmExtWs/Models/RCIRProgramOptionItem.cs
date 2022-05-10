using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CrmCore;

namespace CrmExtForms
{
    public class RCIRProgInterestItem
    {

        public RCIRProgInterestItem()
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
        [Display(Name = "Program Id:")]
        public string ProgramId { get; set; }

        [Required]
        [Display(Name = "Program Interest Name: ")]
        public string ProgramName { get; set; }
    }


}
