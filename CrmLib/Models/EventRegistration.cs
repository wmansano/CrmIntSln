using System;

namespace CrmLcLib
{
    /// <summary>
    /// The EventRegistrationObj is used to pass information between the Checkin app and the Web Service/Core project
    /// </summary>
    public class EventRegistration    {
        private string _contactIdCardBarcode = string.Empty;

        public string EventId { get; set; }
        public bool EventAttended { get; set; }
        public bool EventRegistered { get; set; }
        public string ContactId { get; set; }
        public string ContactColleagueId { get; set; }
        //public string ContactIdCardBarcode { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public DateTime? ContactBirthDate { get; set; }

        public string ContactIdCardBarcode {
            get { return _contactIdCardBarcode; }
            set { _contactIdCardBarcode = fixBarCodeValue(value); }
        }

        private string fixBarCodeValue(string value) {
            string new_value = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(value) && value.Length == 16)
                {
                    new_value = value.Replace("A", "").Replace("B", "").Substring(0, 14);
                }
                else {
                    new_value = value;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }
            return new_value;
        }

    }
}