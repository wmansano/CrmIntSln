using CrmLcLib;
using lc.crm;
using lc.crm.api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static CrmCore.Enumerations;

namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        public Dictionary<string, string> GetEvents(string Token, string department)
        {
            Dictionary<string, string> eventsDictionary = new Dictionary<string, string>();
            string ErrorMsg = string.Empty;
            try
            {
                if (Token == SecurityToken)
                {
                    DateTime Today = DateTime.Today;
                    DateTime NextFewDays = DateTime.Today.AddDays(14);
                    var events = db_ctx.crm_events.Where(x => x.event_deleted == false
                                                                && x.event_mark_delete == false
                                                                && x.activity_id != null
                                                                && x.event_start_datetime >= Today
                                                                && x.event_start_datetime < NextFewDays
                                                                && x.event_department == department
                                                                && x.event_engagement_type == "Event"
                                                                ).Distinct().ToList();
                    if (events.Any())
                    {
                        eventsDictionary = events.Distinct().OrderBy(o => o.event_start_datetime).ToDictionary(t => t.activity_id, t => t.event_subject);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return eventsDictionary;
        }

        public List<string> GetDepartments()
        {
            List<string> depList = new List<string>();
            string ErrorMsg = string.Empty;
            try
            {
                depList = db_ctx.crm_events.Where(x => x.event_deleted == false && !string.IsNullOrWhiteSpace(x.event_department)).Select(s => s.event_department).Distinct().OrderBy(o => o).ToList();  
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return depList;
        }

        public bool SaveEventRegistration(string Token, EventRegistration eventRegistration)
        {
            bool success = false;
            string ErrorMsg = string.Empty;

            crm_scanner_registrations temporary_registration = new crm_scanner_registrations();

            try
            {
                if (Token == SecurityToken) { 
                    var temp_registrations = db_ctx.crm_scanner_registrations.AsEnumerable();

                    if (temp_registrations.Any())
                    {

                        if (!string.IsNullOrWhiteSpace(eventRegistration.ContactColleagueId))
                        {
                            temp_registrations = temp_registrations.Where(x => x.crm_scanner_colleague_id == eventRegistration.ContactColleagueId);
                        }

                        if (!string.IsNullOrWhiteSpace(eventRegistration.ContactIdCardBarcode))
                        {
                            temp_registrations = temp_registrations.Where(x => x.crm_scanner_barcode == eventRegistration.ContactIdCardBarcode);
                        }

                        if (!string.IsNullOrWhiteSpace(eventRegistration.ContactFirstName))
                        {
                            temp_registrations = temp_registrations.Where(x => x.crm_scanner_first_name == eventRegistration.ContactFirstName);
                        }

                        if (!string.IsNullOrWhiteSpace(eventRegistration.ContactLastName))
                        {
                            temp_registrations = temp_registrations.Where(x => x.crm_scanner_last_name == eventRegistration.ContactLastName);
                        }

                        if (!string.IsNullOrWhiteSpace(eventRegistration.EventId))
                        {
                            temp_registrations = temp_registrations.Where(x => x.crm_scanner_event_id == eventRegistration.EventId);
                        }

                        if (eventRegistration.ContactBirthDate != null)
                        {
                            temp_registrations = temp_registrations.Where(x => x.crm_scanner_birth_date == eventRegistration.ContactBirthDate);
                        }

                        if (temp_registrations.Any())
                        {
                            temporary_registration = temp_registrations.OrderByDescending(x => x.crm_scanner_modified_date).FirstOrDefault();
                            success = true;
                        }
                    }

                    if (temporary_registration.crm_scanner_registration_guid.ToString().Contains("00000000-0000-0000-0000-000000000000"))
                    {
                        temporary_registration.crm_scanner_registration_guid = Guid.NewGuid();
                        temporary_registration.crm_scanner_barcode = eventRegistration.ContactIdCardBarcode;
                        temporary_registration.crm_scanner_birth_date = eventRegistration.ContactBirthDate;
                        temporary_registration.crm_scanner_colleague_id = eventRegistration.ContactColleagueId;
                        temporary_registration.crm_scanner_first_name = eventRegistration.ContactFirstName;
                        temporary_registration.crm_scanner_last_name = eventRegistration.ContactLastName;
                        temporary_registration.crm_scanner_event_id = eventRegistration.EventId;
                        //temporary_registration.crm_temp_registration_processed = DateTime.Now;
                        temporary_registration.crm_scanner_modified_date = DateTime.Now;

                        if (!string.IsNullOrWhiteSpace(eventRegistration.ContactIdCardBarcode) && eventRegistration.ContactIdCardBarcode.Length == 7)
                        {
                            temporary_registration.crm_scanner_colleague_id = eventRegistration.ContactIdCardBarcode;
                        }

                        db_ctx.crm_scanner_registrations.Add(temporary_registration);
                        db_ctx.SaveChanges();

                        success = true;
                    }                
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }

        public bool ProcessEventRegistrations()
        {
            bool success = false;
           // bool skip = true;
            bool processed = false;
            string ErrorMsg = string.Empty;
            string contactId = string.Empty;
            //EventStatus status = EventStatus.Attended; // this will need to be passed in from the app (in the future)
            crm_contacts contact = new crm_contacts();
            EventRegistration new_registration_object = new EventRegistration();

            try
            {
                var temp_event_registrations = db_ctx.crm_scanner_registrations.Where(x => x.crm_scanner_registration_processed == null
                                                                                            && x.crm_scanner_registration_skip == false
                                                                                            ).ToList();

                if (temp_event_registrations.Any())
                {
                    foreach (crm_scanner_registrations temp_registration in temp_event_registrations)
                    {
                        contact = UpsertCrmContact(temp_registration);

                        if (contact != null && !string.IsNullOrWhiteSpace(contact.contact_id))
                        {
                            var crm_events = db_ctx.crm_events.Where(x => x.event_deleted == false && x.activity_id == temp_registration.crm_scanner_event_id).ToList();

                            if (crm_events.Any())
                            {
                                var crm_event = crm_events.OrderByDescending(x => x.event_modified_datetime).FirstOrDefault();

                                if (crm_event != null)
                                {
                                    var activity_extenders = db_ctx.crm_activity_extender.Where(x => x.activity_extender_deleted == false
                                                                                                && x.activity_extender_id == crm_event.event_activity_extender_id).ToList();

                                    if (activity_extenders.Any())
                                    {
                                        var activity_extender = activity_extenders.OrderByDescending(x => x.activity_extender_modified_datetime).FirstOrDefault();
                            
                                        if (activity_extender != null)
                                        {
                                            crm_event_registrations event_registration = new crm_event_registrations();

                                            var event_registrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_deleted == false
                                                                                                    && x.event_registration_activity_extender_id == activity_extender.activity_extender_id
                                                                                                    && x.event_registration_contact_id == contact.contact_id).ToList();

                                            if (!event_registrations.Any())
                                            {
                                                event_registration = new crm_event_registrations()
                                                {
                                                    event_registration_guid = Guid.NewGuid(),
                                                    event_registration_activity_extender_id = crm_event.event_activity_extender_id,
                                                    event_registration_contact_id = contact.contact_id,
                                                    event_registration_created_by = Settings.CrmAdmin,
                                                    event_registration_created_datetime = DateTime.Now,
                                                };

                                                db_ctx.crm_event_registrations.Add(event_registration);
                                            }
                                            else
                                            {
                                                event_registration = event_registrations.OrderByDescending(x => x.event_registration_modified_datetime).FirstOrDefault();
                                            }

                                            event_registration.event_registration_deleted = false;
                                            event_registration.event_registration_attended = true; 
                                            temp_registration.crm_scanner_registration_processed = DateTime.Now;
                                            event_registration.event_registration_modified_by = Settings.CrmAdmin;
                                            event_registration.event_registration_modified_datetime = DateTime.Now;

                                            processed = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (!processed) {
                            temp_registration.crm_scanner_registration_skip = true;
                        }

                        db_ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }

        private crm_contacts UpsertCrmContact(crm_scanner_registrations event_registration)
        {
            string ErrorMsg = string.Empty;
            string studentId = string.Empty;
            string contactId = string.Empty;
            string barcode = string.Empty;
            crm_contacts DbContact = new crm_contacts(); ;

            try
            {
                if (event_registration != null)
                {
                    if (!string.IsNullOrWhiteSpace(event_registration.crm_scanner_colleague_id) && !string.IsNullOrWhiteSpace(event_registration.crm_scanner_barcode))
                    {
                        studentId = event_registration.crm_scanner_barcode.Trim().TrimEnd('\r', '\n').PadLeft(7, '0');
                    }
                    if (!string.IsNullOrWhiteSpace(event_registration.crm_scanner_colleague_id))
                    {
                        studentId = event_registration.crm_scanner_colleague_id.Trim().TrimEnd('\r', '\n').PadLeft(7, '0');
                    }
                    if (!string.IsNullOrWhiteSpace(event_registration.crm_scanner_barcode))
                    {
                        barcode = event_registration.crm_scanner_barcode.Trim().TrimEnd('\r', '\n');
                    }
                }

                if (event_registration.crm_scanner_barcode != null)
                {
                    if (barcode.Length == 7)
                    {
                        studentId = barcode.Trim().TrimEnd('\r', '\n').PadLeft(7, '0');
                    }
                    else if (barcode.Length == 18) {
                        contactId = barcode.Trim().TrimEnd('\r', '\n');
                    }
                    else
                    {
                        var barcodes = db_ctx.lcc2_barcodes.Where(x => x.lcc2_barcode_deleted == false && x.lcc2_barcode_number == barcode).ToList();

                        if (barcodes.Any())
                        {
                            var contact_barcode = barcodes.OrderByDescending(x => x.lcc2_barcode_modified_datetime).FirstOrDefault();

                            if (contact_barcode != null)
                            {
                                if (contact_barcode.lcc2_barcode_number != null)
                                {
                                    barcode = contact_barcode.lcc2_barcode_number;
                                }
                                if (contact_barcode.sis_student_id != null)
                                {
                                    studentId = contact_barcode.sis_student_id;
                                }
                            }
                        }
                    }
                }

                var contacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                        && x.contact_mark_delete == false
                                                        && (((!string.IsNullOrWhiteSpace(event_registration.crm_scanner_first_name) && x.contact_first_name == event_registration.crm_scanner_first_name)
                                                            && (!string.IsNullOrWhiteSpace(event_registration.crm_scanner_last_name) && x.contact_last_name == event_registration.crm_scanner_last_name)
                                                            && (event_registration.crm_scanner_birth_date != null && x.contact_birthdate == event_registration.crm_scanner_birth_date))
                                                        || (x.contact_colleague_id == studentId)
                                                        || (x.contact_id == contactId))).ToList();

                if (contacts.Any())
                {
                    DbContact = contacts.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(studentId) 
                            || !string.IsNullOrWhiteSpace(contactId) 
                            || (event_registration.crm_scanner_birth_date != null
                                && !string.IsNullOrWhiteSpace(event_registration.crm_scanner_last_name)))
                    {
                        DbContact.contact_guid = Guid.NewGuid();
                        DbContact.contact_colleague_id = event_registration.crm_scanner_colleague_id;
                        DbContact.contact_first_name = event_registration.crm_scanner_first_name;
                        DbContact.contact_last_name = event_registration.crm_scanner_last_name;
                        DbContact.contact_birthdate = event_registration.crm_scanner_birth_date;

                        db_ctx.crm_contacts.Add(DbContact);
                        db_ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return DbContact;
        }
    }
}

//public string GetCrmContact(EventRegistrationObj eventRegistrationObj)
//{

//    string msg = string.Empty;
//    string contact_id = string.Empty;
//    crm_contacts DbContact = new crm_contacts();

//    try
//    {
//        var contact = LookupContact(eventRegistrationObj);

//        if (contact == null && !string.IsNullOrWhiteSpace(firstName + lastName))
//        {
//            DbContact.contact_guid = Guid.NewGuid();
//            DbContact.contact_deleted = false;
//            DbContact.contact_owner_id = Settings.CrmAdmin;

//            if (barcode != null)
//            {
//                var barcodes = db_ctx.lcc2_barcodes.Where(x => x.lcc2_barcode_deleted == false && x.lcc2_barcode_number == barcode);

//                if (barcodes.Any())
//                {
//                    var contact_barcode = barcodes.OrderByDescending(x => x.lcc2_barcode_modified_datetime).FirstOrDefault();

//                    if (contact_barcode != null)
//                    {
//                        if (contact_barcode.lcc2_barcode_number != null)
//                        {
//                            DbContact.contact_id_card_barcode = contact_barcode.lcc2_barcode_number;
//                        }
//                        if (contact_barcode.sis_student_id != null)
//                        {
//                            DbContact.contact_colleague_id = contact_barcode.sis_student_id.PadLeft(7, '0');
//                        }
//                    }
//                }
//            }

//            if (colleagueId != null)
//            {
//                DbContact.contact_colleague_id = colleagueId.PadLeft(7, '0');
//            }

//            if (birthDate != null)
//            {
//                DbContact.contact_birthdate = birthDate;
//            }

//            if (!string.IsNullOrWhiteSpace(firstName))
//            {
//                DbContact.contact_first_name = firstName.Trim();
//            }

//            if (!string.IsNullOrWhiteSpace(lastName))
//            {
//                DbContact.contact_last_name = lastName.Trim();
//            }

//            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
//            {
//                DbContact.contact_full_name = firstName.Trim() + " " + lastName.Trim();
//            }

//            db_ctx.crm_contacts.Add(DbContact);
//            db_ctx.SaveChanges();
//        }
//        else
//        {
//            DbContact = contact;
//        }
//    }
//    catch (Exception ex)
//    {
//        msg = ex.ToString();
//    }
//    return DbContact.contact_id;
//}

//public string FindColleagueContact(string barcode = null, string colleagueId = null, string firstName = null, string lastName = null, DateTime? birthDate = null)
//{

//    string msg = string.Empty;
//    string contact_id = string.Empty;

//    v_dw_students contact = new v_dw_students();

//    try
//    {
//        var contact = LookupContact(barcode, colleagueId, firstName, lastName, birthDate);

//        if (contact == null && !string.IsNullOrWhiteSpace(firstName + lastName))
//        {
//            DbContact.contact_guid = Guid.NewGuid();
//            DbContact.contact_deleted = false;
//            DbContact.contact_owner_id = Settings.CrmAdmin;

//            if (barcode != null)
//            {
//                var barcodes = db_ctx.lcc2_barcodes.Where(x => x.lcc2_barcode_deleted == false && x.lcc2_barcode_number == barcode);

//                if (barcodes.Any())
//                {
//                    var contact_barcode = barcodes.OrderByDescending(x => x.lcc2_barcode_modified_datetime).FirstOrDefault();

//                    if (contact_barcode != null)
//                    {
//                        if (contact_barcode.lcc2_barcode_number != null)
//                        {
//                            DbContact.contact_id_card_barcode = contact_barcode.lcc2_barcode_number;
//                        }
//                        if (contact_barcode.sis_student_id != null)
//                        {
//                            DbContact.contact_colleague_id = contact_barcode.sis_student_id.PadLeft(7, '0');
//                        }
//                    }
//                }
//            }

//            if (colleagueId != null)
//            {
//                DbContact.contact_colleague_id = colleagueId.PadLeft(7, '0');
//            }

//            if (birthDate != null)
//            {
//                DbContact.contact_birthdate = birthDate;
//            }

//            if (!string.IsNullOrWhiteSpace(firstName))
//            {
//                DbContact.contact_first_name = firstName.Trim();
//            }

//            if (!string.IsNullOrWhiteSpace(lastName))
//            {
//                DbContact.contact_last_name = lastName.Trim();
//            }

//            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
//            {
//                DbContact.contact_full_name = firstName.Trim() + " " + lastName.Trim();
//            }

//            db_ctx.crm_contacts.Add(DbContact);
//            db_ctx.SaveChanges();
//        }
//        else
//        {
//            DbContact = contact;
//        }
//    }
//    catch (Exception ex)
//    {
//        msg = ex.ToString();
//    }
//    return DbContact.contact_id;
//}

//private crm_contacts GetcolleageContact(string barcode = null, string colleagueId = null, string firstName = null, string lastName = null, DateTime? birthDate = null)
//{
//    crm_contacts DbContact = null;
//    string ErrorMsg = string.Empty;

//    try
//    {
//        List<sis> contacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false).ToList();

//        // backfill barcode if available
//        if (barcode != null)
//        {
//            if (barcode.Length == 7)
//            {
//                colleagueId = barcode.PadLeft(7, '0');
//            }
//            else
//            {
//                var barcodes = db_ctx.lcc2_barcodes.Where(x => x.lcc2_barcode_deleted == false && x.lcc2_barcode_number == barcode);

//                if (barcodes.Any())
//                {
//                    var contact_barcode = barcodes.OrderByDescending(x => x.lcc2_barcode_modified_datetime).FirstOrDefault();

//                    if (contact_barcode != null)
//                    {
//                        if (contact_barcode.lcc2_barcode_number != null)
//                        {
//                            barcode = contact_barcode.lcc2_barcode_number;
//                        }
//                        if (contact_barcode.sis_student_id != null)
//                        {
//                            colleagueId = contact_barcode.sis_student_id.PadLeft(7, '0');
//                        }
//                    }
//                }
//            }
//        }

//        if (barcode != null && barcode.Length > 7)
//        {
//            contacts = contacts.Where(x => x.contact_id_card_barcode == barcode).ToList();
//        }

//        if (!string.IsNullOrWhiteSpace(colleagueId))
//        {
//            contacts = contacts.Where(x => x.contact_colleague_id == colleagueId).ToList();
//        }

//        if (!string.IsNullOrWhiteSpace(firstName))
//        {
//            contacts = contacts.Where(x => x.contact_first_name == firstName).ToList();
//        }

//        if (!string.IsNullOrWhiteSpace(lastName))
//        {
//            contacts = contacts.Where(x => x.contact_last_name == lastName).ToList();
//        }

//        if (birthDate != null)
//        {
//            contacts = contacts.Where(x => x.contact_birthdate == birthDate).ToList();
//        }

//        if (contacts.Any())
//        {
//            DbContact = contacts.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();
//        }
//        else
//        {

//        }

//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    return DbContact;
//}

//private crm_contacts LookupContact(string colleague_id, string first_name = null, string last_name = null, string birth_date = null, string barcode = null) {

//    crm_contacts DbContact = new crm_contacts();

//    try
//    {
//        var contacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false && x.contact_to_delete == null);

//        // possible matching scenarios from best match to minimal

//        if (contacts.Any()) {

//            var contact = db_ctx.crm_contacts.Where(x => (x.contact_id_card_barcode != null && x.contact_id_card_barcode == barcode)
//                                                           || (colleague_id != null && x.contact_colleague_id == colleague_id
//                                                                && first_name != null && x.contact_first_name == first_name
//                                                                && last_name != null && x.contact_last_name == last_name
//                                                                && birth_date != null && x.contact)
//                                                            || ))
//                                && x.contact_first_name == first_name
//                                && x.contact_last_name == last_name
//        }




//        // Lookup for Contact Id:

//        // Try using sNumber
//        if (string.IsNullOrWhiteSpace(eventRegistrationObj.ContactId) && !string.IsNullOrWhiteSpace(eventRegistrationObj.ContactColleagueId))
//        {
//            eventRegistrationObj.ContactId = db_ctx.crm_contacts.Where(x => x.contact_colleague_id.Trim().ToUpper() == eventRegistrationObj.ContactColleagueId.Trim().ToUpper())
//                                        .OrderByDescending(o => o.contact_last_modified_datetime).Select(s => s.contact_id).FirstOrDefault();
//        }

//        // Try using Id Card Barcode
//        if (string.IsNullOrWhiteSpace(eventRegistrationObj.ContactId) && !string.IsNullOrWhiteSpace(eventRegistrationObj.ContactIdCardBarcode))
//        {
//            eventRegistrationObj.ContactId = db_ctx.crm_contacts.Where(x => x.contact_id_card_barcode.Trim().ToUpper() == eventRegistrationObj.ContactIdCardBarcode.Trim().ToUpper())
//                                        .OrderByDescending(o => o.contact_last_modified_datetime).Select(s => s.contact_id).FirstOrDefault();
//        }

//        // Try using Name and Birth date
//        if (string.IsNullOrWhiteSpace(eventRegistrationObj.ContactId) && !string.IsNullOrWhiteSpace(eventRegistrationObj.ContactFirstName) &&
//            !string.IsNullOrWhiteSpace(eventRegistrationObj.ContactFirstName) && eventRegistrationObj.ContactBirthDate != null)
//        {
//            eventRegistrationObj.ContactId = db_ctx.crm_contacts.Where(x => (x.contact_first_name ?? string.Empty).Trim().ToUpper() == eventRegistrationObj.ContactFirstName.Trim().ToUpper() &&
//                                                    (x.contact_last_name ?? string.Empty).Trim().ToUpper() == eventRegistrationObj.ContactLastName.Trim().ToUpper() &&
//                                                    x.contact_birthdate == eventRegistrationObj.ContactBirthDate)
//                                        .OrderByDescending(o => o.contact_last_modified_datetime).Select(s => s.contact_id).FirstOrDefault();
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    return eventRegistrationObj.ContactId;
//}
//private bool ValidateEventLookup(string lname, string fname, DateTime birthDate)
//{
//    bool success = false;
//    string ErrorMsg = string.Empty;
//    try
//    {
//        success = db_ctx.crm_contacts.Where(x => x.contact_deleted == false && lname.Trim().ToUpper().Contains(x.contact_last_name.Trim().ToUpper()) && fname.Trim().ToUpper().Contains(x.contact_first_name.Trim().ToUpper()) && x.contact_birthdate == birthDate).Any();
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

//    return success;
//}

//private bool ValidateEventCheckinNumber(string number)
//{
//    bool success = false;
//    string ErrorMsg = string.Empty;
//    try
//    {
//        if (number.ToString().Length == 14)
//        {
//            // we have a barcode
//            success = db_ctx.crm_contacts.Where(x => x.contact_deleted == false && x.contact_id_card_barcode == number).Any();
//        }
//        else if (number.ToString().Length == 7)
//        {
//            // we have an sNumber
//            success = db_ctx.crm_contacts.Where(x => x.contact_deleted == false && x.contact_colleague_id == number).Any();
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

//    return success;
//}

//private List<string> GetTypes(string department)
//{
//    List<string> typeList = new List<string>();
//    string ErrorMsg = string.Empty;
//    try
//    {
//        typeList = db_ctx.crm_events.Where(x => x.event_deleted == false && x.event_department == department).Select(s => s.event_engagement_type).Distinct().OrderBy(o => o).ToList();
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

//    return typeList;
//}

////private bool LookupColleagueContact(EventRegistrationObj eventRegistrationObj)
////{
////    string ErrorMsg = string.Empty;
////    bool success = false;

////    try
////    {
////        // backfill barcode if available
////        if (eventRegistrationObj.ContactIdCardBarcode != null)
////        {
////            if (eventRegistrationObj.ContactIdCardBarcode.Length == 7)
////            {
////                eventRegistrationObj.ContactColleagueId = eventRegistrationObj.ContactIdCardBarcode.PadLeft(7, '0');
////            }
////            else
////            {
////                var barcodes = db_ctx.lcc2_barcodes.Where(x => x.lcc2_barcode_deleted == false && x.lcc2_barcode_number == eventRegistrationObj.ContactIdCardBarcode);

////                if (barcodes.Any())
////                {
////                    var contact_barcode = barcodes.OrderByDescending(x => x.lcc2_barcode_modified_datetime).FirstOrDefault();

////                    if (contact_barcode != null)
////                    {
////                        if (contact_barcode.lcc2_barcode_number != null)
////                        {
////                            eventRegistrationObj.ContactIdCardBarcode = contact_barcode.lcc2_barcode_number;
////                        }
////                        if (contact_barcode.sis_student_id != null)
////                        {
////                            eventRegistrationObj.ContactColleagueId = contact_barcode.sis_student_id.PadLeft(7, '0');
////                        }
////                    }
////                }
////            }
////        }

////        var contacts = db_ctx.wt_core_students.ToList();

////        if (eventRegistrationObj.ContactIdCardBarcode != null && eventRegistrationObj.ContactIdCardBarcode.Length == 7)
////        {
////            contacts = contacts.Where(x => x.sis_student_id == eventRegistrationObj.ContactIdCardBarcode).ToList();
////        }

////        if (!string.IsNullOrWhiteSpace(eventRegistrationObj.ContactColleagueId))
////        {
////            contacts = contacts.Where(x => x.sis_student_id == eventRegistrationObj.ContactColleagueId).ToList();
////        }

////        if (!string.IsNullOrWhiteSpace(eventRegistrationObj.ContactFirstName))
////        {
////            contacts = contacts.Where(x => x.sis_stu_first_name == eventRegistrationObj.ContactFirstName).ToList();
////        }

////        if (!string.IsNullOrWhiteSpace(eventRegistrationObj.ContactLastName))
////        {
////            contacts = contacts.Where(x => x.sis_stu_last_name == eventRegistrationObj.ContactLastName).ToList();
////        }

////        if (eventRegistrationObj.ContactBirthDate != null)
////        {
////            contacts = contacts.Where(x => x.sis_stu_birth_date == eventRegistrationObj.ContactBirthDate).ToList();
////        }

////        if (contacts.Any())
////        {
////            eventRegistrationObj.ContactColleagueId = contacts.OrderByDescending(x => x.modified_date).FirstOrDefault().sis_student_id;
////            eventRegistrationObj.ContactFirstName = contacts.OrderByDescending(x => x.modified_date).FirstOrDefault().sis_stu_first_name;
////            eventRegistrationObj.ContactLastName = contacts.OrderByDescending(x => x.modified_date).FirstOrDefault().sis_stu_last_name;
////            eventRegistrationObj.ContactBirthDate = contacts.OrderByDescending(x => x.modified_date).FirstOrDefault().sis_stu_birth_date;
////            success = true;
////        }

////    }
////    catch (Exception ex)
////    {
////        ErrorMsg = ex.ToString();
////    }

////    return success;
////}

//private bool UpsertSfContact(EventRegistration eventRegistrationObj)
//{

//    bool success = false;
//    Contact SfContact = new Contact();
//    List<sObject> SalesforceInserts = new List<sObject>();
//    List<Guid> SalesforceInsertKeys = new List<Guid>();
//    string ErrorMsg = string.Empty;

//    try
//    {

//        var DbContacts = db_ctx.crm_contacts.Where(x => x.contact_colleague_id == eventRegistrationObj.ContactColleagueId);

//        if (DbContacts.Any())
//        {
//            crm_contacts DbContact = DbContacts.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

//            SfContact = MapContactDbToSf(DbContact);

//            if (SfContact != null)
//            {
//                SalesforceInserts.Add(SfContact);

//                SalesforceInsertKeys.Add(DbContact.contact_guid);

//                if (InsertRecords(SalesforceInserts, SalesforceInsertKeys, InsertResults.Contact))
//                {
//                    eventRegistrationObj.ContactId = SfContact.lc_contact_id__c;
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

//    return success;
//}
