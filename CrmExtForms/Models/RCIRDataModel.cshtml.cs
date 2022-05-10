using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CrmCore;

namespace CrmExtForms
{
    public class RCRDataModel
    {
        private readonly CrmCoreLogic CrmCore = new CrmCoreLogic();

        public RCRDataModel() {
            SetOptions();
        }

        public bool SetOptions()
        {
            bool success = false;
            using CrmCoreLogic logic = new CrmCoreLogic();
            TermOptions = logic.GetFutureTermOptions();
            ProgramOptions = logic.GetProgramOptions();
            // EventOptions = logic.GetSfadEventOptions();
            return success;
        }

        public bool LookupContact() {
            bool success = false;
            
            


            return success;
        }

        //[Required]
        ////[Display(Source = 0)]
        //public Enumerations.FormSourceTypes Source { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "sNumber")]
        public string sNumber { get; set; }
        public bool EnableSnumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public bool EnableFirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public bool EnableLastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        public bool EnableMiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        public bool EnableBirthDate { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Mailing City")]
        public string MailingCity { get; set; }
        public bool EnableMailingCity { get; set; }

        [Required]
        [Display(Name = "Mailing Province")]
        public string MailingProv { get; set; }
        public bool EnableMailingProv { get; set; }


        [Required]
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "10 digit phone number only!")]
        public string MobilePhone { get; set; }
        public bool EnableMobilePhone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string EmailAddress { get; set; }
        public bool EnableEmailAddress { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "When are you planning to attend? ")]
        public string StartTerm { get; set; }
        public bool EnableStartTerm { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "What program are you interested in? ")]
        public string ProgramInterest { get; set; }
        public bool EnableProgramInterest { get; set; }

        public bool EnablePriorActivity { get; set; }

        [Display(Name = "What describes you best?")]
        public string PriorActivity { get; set; }

        [Display(Name = "Please send me additional information on:")]
        public bool EnableServicesInterest { get; set; }

        [Required]
        [Display(Name = "Student for a Day")]
        public bool SfadInterest { get; set; }

        [Required]
        [Display(Name = "Campus Tour")]
        public bool CtInterest { get; set; }

        [Display(Name = "Are you an indigenous person? ")]
        public bool Indigenous { get; set; }
        public bool EnableIndigenous { get; set; }

        [Display(Name = "Athletics")]
        public bool AthleticsInterest { get; set; }

        [Display(Name = "Residence")]
        public bool ResidencyInterest { get; set; }

        [Display(Name = "Accessibility Services")]
        public bool AccessibilityInterest { get; set; }

        [Display(Name = "Wellness Services")]
        public bool WellnessInterest { get; set; }

        [Display(Name = "Financial Awards & Student Aid")]
        public bool AwardsInterest { get; set; }

        [Required]
        [Display(Name = "Already Applied")]
        public bool AlreadyApplied { get; set; }

        [DataType(DataType.Text)]
        public string RequiredField { get; set; }
        public bool EnableRequiredField { get; set; }

        [DataType(DataType.Text)]
        public string InputType { get; set; }
        public bool EnableInputType { get; set; }


        //[Required]
        //[Display(Name = "Anticipated Start Term")]
        //public string AnticipatedStartTerm { get; set; }

        //[Required]
        //[Display(Name = "Highschool Name")]
        //public string HighSchoolName { get; set; }

        //[Required]
        //[Display(Name = "Current Grade")]
        //public string CurrentGrade { get; set; }

        //[Required]
        //[Display(Name = "Current Student")]
        //public bool CurrentStudent { get; set; }

        //[Required]
        //[Display(Name = "Mailing Address")]
        //public string MailingAddress { get; set; }

        //[Required]
        //[Display(Name = "Mailing Country")]
        //public string MailiingCountry { get; set; }

        //[Required]
        //[DataType(DataType.PostalCode)]
        //[Display(Name = "Mailing Zip")]
        //public string MailingZip { get; set; }

        //[Required]
        //[DataType(DataType.PhoneNumber)]
        //[Display(Name = "Home Phone")]
        //public string HomePhone { get; set; }

        //[Required]
        //[Display(Name = "Verify Email Address")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Email Address does not match!")]
        //public string VerifyEmailAddress { get; set; }

        //[Required]
        //[Display(Name = "Anticipated Start Year")]
        //public string AnticipatedStartYear { get; set; }

        //[Required]
        //[Display(Name = "Current Status")]
        //public string CurrentStatus { get; set; }

        //[Required]
        //[Display(Name = "High School Grade")]
        //public string HighschoolGrade { get; set; }

        //[Required]
        //[Display(Name = "Please send me information on: ")]
        //public string ServicesInterest { get; set; }

        //[Display(Name = "Are you an Indigenous Person? ")]
        //[Range(typeof(bool), "true", "false", ErrorMessage = "This is a required field")]
        //public bool Indigenous { get; set; }

        //[Display(Name = "Indigenous Status? ")]
        //public string IndigenousStatus { get; set; }

        //[Required]
        //[Display(Name = "Event Date")]
        //[DataType(DataType.Date)]
        //public string EventDate { get; set; }

        //[Required]
        //[Display(Name = "Preferred Date")]
        //[DataType(DataType.Date)]
        //public string PreferredDate { get; set; }

        //[Required]
        //[Display(Name = "Emergency Contact Person")]
        //public string EmergencyContactName { get; set; }

        //[Required]
        //[Display(Name = "Emergency Contact Number")]
        //public string EmergencyContactNumber { get; set; }

        //[Required]
        //[Display(Name = "Special Accomodations")]
        //public string SpecialAccomodations { get; set; }

        //[Display(Name = "Are you interested in a campus tour? ")]
        //[Range(typeof(bool), "true", "false", ErrorMessage = "This is a required field")]
        //public bool CampusTour { get; set; }

        //[Display(Name = "Would you like to be a student for a day (includes campus tour)? ")]
        //[Range(typeof(bool), "true", "false", ErrorMessage = "This is a required field")]
        //public bool SFAD { get; set; }


        //public List<RCIREventItem> Events { get; set; }

        public IEnumerable<SelectListItem> ProgramOptions { get; set; }

        public IEnumerable<SelectListItem> TermOptions { get; set; }

        //public List<SelectListItem> EventOptions { get; set; }
    }

    //public class PossibleDateItem
    //{
    //    public int Priority { get; set; }
    //    public DateTime PossibleDate { get; set; }
    //}
}
