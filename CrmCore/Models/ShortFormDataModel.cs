using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Web.Mvc;

namespace CrmCore.Models
{
    public class ShortFormDataModel : IDisposable
    {
        //private readonly string dbcs = ConfigurationManager.ConnectionStrings["crmdb_entities"].ConnectionString;
        private CrmCoreLogic Logic = new CrmCoreLogic();

        public ShortFormDataModel() {

        }

        public bool LookupContact() {
            bool success = false;

            //using CrmCoreLogic logic = new CrmCoreLogic();
            //logic.ContactLookup(this);


            return success;
        }

        [Required]
        //[Display(Source = 0)]
        public Enumerations.FormSourceTypes Source { get; set; }

        [Required]
        [Display(Name = "Guid")]
        public Guid Guid { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Cookie")]
        public cookie Cookie { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class cookie
    {
        public string uuid;
        public visit[] visits;
    }

    public class visit
    {
        public DateTime date;
        public string page;
    }
}
