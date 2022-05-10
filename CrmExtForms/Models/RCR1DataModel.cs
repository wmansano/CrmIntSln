using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CrmCore;

namespace CrmExtForms
{
    public class RCR1DataModel
    {
        public RCR1DataModel() {
            SetOptions();
        }

        public bool SetOptions()
        {
            bool success = false;
            using CrmCoreLogic logic = new CrmCoreLogic();

            return success;
        }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

    }
}
