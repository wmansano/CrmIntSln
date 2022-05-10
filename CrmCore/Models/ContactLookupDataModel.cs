using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Web.Mvc;

namespace CrmCore.Models
{
    public class ContactLookupDataModel
    {
        //private readonly string dbcs = ConfigurationManager.ConnectionStrings["crmdb_entities"].ConnectionString;

        public ContactLookupDataModel() {

        }

        public bool SetOptions()
        {
            bool success = false;
            using CrmCoreLogic logic = new CrmCoreLogic();
            TermOptions = logic.GetFutureTermOptions();
            EventOptions = logic.GetSfadEventOptions();
            return success;
        }

        public bool LookupContact() {
            bool success = false;

            using CrmCoreLogic logic = new CrmCoreLogic();
            logic.ContactLookup(this);


            return success;
        }

        [Required]
        //[Display(Source = 0)]
        public Enumerations.FormSourceTypes Source { get; set; }

        [Required]
        [Display(Name = "ContactId")]
        public string ContactId { get; set; }

        [Required]
        [Display(Name = "sNumber")]
        public string sNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Highschool Name")]
        public string HighSchoolName { get; set; }

        [Required]
        [Display(Name = "Current Grade")]
        public string CurrentGrade { get; set; }

        [Required]
        [Display(Name = "Current Student")]
        public bool CurrentStudent { get; set; }

        [Required]
        [Display(Name = "Mailing Address")]
        public string MailingAddress { get; set; }

        [Required]
        [Display(Name = "Mailing City")]
        public string MailingCity { get; set; }

        [Required]
        [Display(Name = "Mailing Province")]
        public string MailingProv { get; set; }

        [Required]
        [Display(Name = "Mailing Country")]
        public string MailiingCountry { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Mailing Zip")]
        public string MailingZip { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Already Applied")]
        public bool AlreadyApplied { get; set; }

        [Required]
        [Display(Name = "Start Term")]
        public string StartTerm { get; set; }

        [Required]
        [Display(Name = "Anticipated Start Year")]
        public string AnticipatedStartYear { get; set; }

        [Required]
        [Display(Name = "Anticipated Start Term")]
        public string AnticipatedStartTerm { get; set; }

        [Required]
        [Display(Name = "Current Status")]
        public string CurrentStatus { get; set; }

        [Required]
        [Display(Name = "High School Grade")]
        public string HighschoolGrade { get; set; }

        [Required]
        [Display(Name = "Please send me information on: ")]
        public string ServicesInterest { get; set; }

        [Display(Name = "Are you an Indigenous Person? ")]
        [Range(typeof(bool), "true", "false", ErrorMessage = "This is a required field")]
        public bool Indigenous { get; set; }

        [Display(Name = "Indigenous Status? ")]
        public string IndigenousStatus { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public string EventDate { get; set; }

        [Required]
        [Display(Name = "Preferred Date")]
        [DataType(DataType.Date)]
        public string PreferredDate { get; set; }

        [Required]
        [Display(Name = "Emergency Contact Person")]
        public string EmergencyContactName { get; set; }

        [Required]
        [Display(Name = "Emergency Contact Number")]
        public string EmergencyContactNumber { get; set; }

        [Required]
        [Display(Name = "Special Accomodations")]
        public string SpecialAccomodations { get; set; }

        [Display(Name = "Are you interested in a campus tour? ")]
        [Range(typeof(bool), "true", "false", ErrorMessage = "This is a required field")]
        public bool CampusTour { get; set; }

        [Display(Name = "Would you like to be a student for a day (includes campus tour)? ")]
        [Range(typeof(bool), "true", "false", ErrorMessage = "This is a required field")]
        public bool SFAD { get; set; }

        public List<RCIREventItem> Events { get; set; }

        public List<RCIRProgInterestItem> ProgramInterests { get; set; }

        public List<SelectListItem> TermOptions { get; set; }

        public List<SelectListItem> EventOptions { get; set; }
    }

    public class PossibleDateItem
    {
        public int Priority { get; set; }
        public DateTime PossibleDate { get; set; }
    }
}
