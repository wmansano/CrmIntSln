using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Net;
using System.Web.Mvc;

namespace CrmCore.Models
{
    public class RCRIRSFilterModel
    {

        #region Private Properties
        private string _EncodedSource = null;
        private string _EncodedInquiryId = null;
        private string _EncodedProgramId = null;
        private bool _EnableIFrame = false;

        private string _EncodedFirstName = null;
        private bool _EnableFirstName = false;

        private string _EncodedLastName = null;
        private bool _EnableLastName = false;

        private string _EncodedEmailAddress = null;
        private bool _EnableEmailAddress = false;

        private string _EncodedStartTerm = null;
        private bool _EnableStartTerm = false;

        private string _EncodedProgramInterest = null;
        private bool _EnableProgramInterest = false;

        private string _EncodedMailingCity = null;
        private bool _EnableMailingCity = false;

        private string _EncodedMailingProv = null;
        private bool _EnableMailingProv = false;

        private string _EncodedMobilePhone = null;
        private bool _EnableMobilePhone = false;

        private DateTime? _BirthDate = null;
        private bool _EnableBirthDate = false;

        private bool _AlreadyApplied = false;
        private bool _EnableAlreadyApplied = false;

        // for all services as a group
        private bool _EnableServicesInterest = false;
        private bool _EnableIndigenous = false;
        private bool _EnablePriorActivity = false;
        private bool _EnableSfadCtInterest = false;
        private bool _Saved = false;

        private bool _Indigenous = false;
        private bool _SfadInterest = false;
        private bool _CtInterest = false;
        private bool _AthleticInterest = false;
        private bool _AccessibilityInterest = false;
        private bool _ResidenceInterest = false;
        private bool _WellnessInterest = false;
        private bool _AwardsInterest = false;
        private string _EncodedPriorStatus = null;

        #endregion

        public static void RCRFilterModel()
        {
            // Constructor
        }

        public void SaveRCRForm(RCRIRSDataModel Model)
        {
            string ErrorMsg = string.Empty;
            try
            {
                if (Model != null && string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(Model.RequiredField))
                        && string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(Model.InputType)))
                {
                    using (CrmCoreLogic logic = new CrmCoreLogic())
                    {
                        _Saved = logic.SaveRCRIRSForm(this);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        #region Public Properties

        public string EncodedSource { 
            get { 
                return _EncodedSource; 
            } 
            set { 
                
                _EncodedSource = value;

                switch (_EncodedSource)
                {
                    case FormSourceTypes.rcrirs1a:
                        _EnableBirthDate = true;
                        _EnableMobilePhone = true;
                        _EnableMailingCity = true;
                        _EnableMailingProv = true;
                        _EnableProgramInterest = true;
                        _EnableStartTerm = true;
                        _EnablePriorActivity = true;
                        _EnableSfadCtInterest = true;
                        _EnableIndigenous = true;
                        break;
                    case FormSourceTypes.rcrirs1b:
                        _EnableMailingCity = true;
                        _EnableMailingProv = true;
                        _EnableStartTerm = true;
                        _EnablePriorActivity = true;
                        _EnableSfadCtInterest = true;
                        _EnableServicesInterest = true;
                        _EnableIndigenous = true;
                        break;
                    case FormSourceTypes.rcrirs2:
                        // Address Line
                        _EnableMailingCity = true;
                        // Country
                        _EnableMailingProv = true;
                        // Postal Code
                        _EnableStartTerm = true;
                        _EnablePriorActivity = true;
                        _EnableServicesInterest = true;
                        _EnableIndigenous = true;
                        break;
                    case FormSourceTypes.rcrirs3:
                        _EnableProgramInterest = true;
                        break;
                    case FormSourceTypes.rcrirs4:
                        _EnableFirstName = true;
                        _EnableLastName = true;
                        _EnableEmailAddress = true;
                        _EnableMobilePhone = true;
                        _EnableBirthDate = true;
                        _EnableMailingCity = true;
                        _EnableMailingProv = true;
                        _EnablePriorActivity = true;
                        _EnableServicesInterest = true;
                        _EnableSfadCtInterest = true;
                        _EnableIndigenous = true;
                        break;
                    case FormSourceTypes.rcrirs1:
                    default:
                        _EnableFirstName = true;
                        _EnableLastName = true;
                        _EnableEmailAddress = true;
                        _EnableMobilePhone = true;
                        _EnableBirthDate = true;
                        _EnableMailingCity = true;
                        _EnableMailingProv = true;
                        _EnableProgramInterest = true;
                        _EnableStartTerm = true;
                        _EnablePriorActivity = true;
                        _EnableServicesInterest = true;
                        _EnableSfadCtInterest = true;
                        _EnableIndigenous = false;
                        break;
                }

            } }
        public string EncodedInquiryId { get { return _EncodedInquiryId; } set { _EncodedInquiryId = value; } }
        public string EncodedProgramId { get { return _EncodedProgramId; } set { _EncodedProgramId = value; } }
        public bool EnableIFrame { get { return _EnableIFrame; } set { _EnableIFrame = value; } }

        public bool EnableFirstName { get { return _EnableFirstName; } set { _EnableFirstName = value; } }
        public string EncodedFirstName { 
            get { 
                return _EncodedFirstName; 
            } 
            set {
                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedFirstName = WebUtility.HtmlEncode(value);
                }
            } 
        }

        public bool EnableLastName { get { return _EnableLastName; } set { _EnableLastName = value; } }
        public string EncodedLastName { 
            get { 
                return _EncodedLastName; 
            } 
            set { 

                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedLastName = WebUtility.HtmlEncode(value);
                }
            } 
        }

        public bool EnableEmailAddress { get { return _EnableEmailAddress; } set { _EnableEmailAddress = value; } }
        public string EncodedEmailAddress { 
            get { 
                return _EncodedEmailAddress; 
            } 
            set { 
                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedEmailAddress = WebUtility.HtmlEncode(value);
                }
            } 
        }

        public bool EnableStartTerm { get { return _EnableStartTerm; } set { _EnableStartTerm = value; } }
        public string EncodedStartTerm { 
            get { 
                return _EncodedStartTerm; 
            } set { 
                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedStartTerm = WebUtility.HtmlEncode(value);
                }
            } 
        }

        public bool EnableProgramInterest { get { return _EnableProgramInterest; } set { _EnableProgramInterest = value; } }
        public string EncodedProgramInterest { 
            get { 
                return _EncodedProgramInterest; 
            } 
            set { 
                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedProgramInterest = WebUtility.HtmlEncode(value);
                }
            } 
        }

        public bool EnableMailingCity { get { return _EnableMailingCity; } set { _EnableMailingCity = value; } }
        public string EncodedMailingCity { 
            get { 
                return _EncodedMailingCity; 
            } 
            set { 
                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedMailingCity = WebUtility.HtmlEncode(value);
                }
            } 
        }
        
        public bool EnableMailingProv { get { return _EnableMailingProv; } set { _EnableMailingProv = value; } }
        public string EncodedMailingProv { 
            get { 
                return _EncodedMailingProv; 
            } 
            set { 
                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedMailingProv = WebUtility.HtmlEncode(value);
                }
            } 
        }

        public bool EnableMobilePhone { get { return _EnableMobilePhone; } set { _EnableMobilePhone = value; } }
        public string EncodedMobilePhone { 
            get { 
                return _EncodedMobilePhone; 
            } 
            set { 

                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedMobilePhone = WebUtility.HtmlEncode(value);
                }
            } 
        }

        public bool EnableBirthDate { get { return _EnableBirthDate; } set { _EnableBirthDate = value; } }
        public DateTime? BirthDate { 
            get { 
                return _BirthDate; 
            } 
            set { 
                if (value != null)
                {
                    _BirthDate = value;
                }
            } 
        }
        
        public bool EnableAlreadyApplied { get { return _EnableAlreadyApplied; } set { _EnableAlreadyApplied = value; } }
        public bool AlreadyApplied { get { return _AlreadyApplied; } set { _AlreadyApplied = value; } }

        // for all services as a group
        public bool EnableServicesInterest { get { return _EnableServicesInterest; } set { _EnableServicesInterest = value; } }
        public bool EnableIndigenous { get { return _EnableIndigenous; } set { _EnableIndigenous = value; } }
        public bool EnablePriorActivity { get { return _EnablePriorActivity; } set { _EnablePriorActivity = value; } }
        public bool EnableSfadCtInterest { get { return _EnableSfadCtInterest; } set { _EnableSfadCtInterest = value; } }
        public bool Saved { get { return _Saved; } set { _Saved = value; } }

        public bool Indigenous { get { return _Indigenous; } set { _Indigenous = value; } }
        public bool SfadInterest { get { return _SfadInterest; } set { _SfadInterest = value; } }
        public bool CtInterest { get { return _CtInterest; } set { _CtInterest = value; } }
        public bool AthleticInterest { get { return _AthleticInterest; } set { _AthleticInterest = value; } }
        public bool AccessibilityInterest { get { return _AccessibilityInterest; } set { _AccessibilityInterest = value; } }
        public bool ResidenceInterest { get { return _ResidenceInterest; } set { _ResidenceInterest = value; } }
        public bool WellnessInterest { get { return _WellnessInterest; } set { _WellnessInterest = value; } }
        public bool AwardsInterest { get { return _AwardsInterest; } set { _AwardsInterest = value; } }
        public string EncodedPriorStatus { 
            get { 
                return _EncodedPriorStatus; 
            } 
            set { 
                if (!string.IsNullOrWhiteSpace(WebUtility.HtmlEncode(value)))
                {
                    _EncodedPriorStatus = WebUtility.HtmlEncode(value);
                }
            } 
        }

        #endregion
    }

    //public class PossibleDateItem
    //{
    //    public int Priority { get; set; }
    //    public DateTime PossibleDate { get; set; }
    //}

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
}
