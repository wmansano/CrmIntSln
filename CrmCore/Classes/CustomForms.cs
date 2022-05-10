using CrmLcLib;
using lc.crm;
using lc.crm.api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CrmCore.Models;
using static CrmCore.Enumerations;

namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        public bool SaveLandingForm(string uuid, string source, string firstName,string lastName, string emailAddress, string googleClientId)
        {
            bool success = false;
            string ErrorMsg = string.Empty;

            crm_form_submissions form_submission = new crm_form_submissions();

            try
            {
                form_submission.crm_form_guid = Guid.NewGuid();
                form_submission.crm_cookie_uuid = new Guid(uuid);

                if (!string.IsNullOrWhiteSpace(source)) {
                    form_submission.crm_form_source = source;
                }

                if (!string.IsNullOrWhiteSpace(firstName)) {
                    form_submission.crm_form_firstname = firstName;
                }

                if (!string.IsNullOrWhiteSpace(lastName)) {
                    form_submission.crm_form_lastname = lastName;
                }

                if (!string.IsNullOrWhiteSpace(emailAddress)) {
                    form_submission.crm_form_emailaddress = emailAddress;
                }

                if (!string.IsNullOrWhiteSpace(googleClientId)) {
                    form_submission.crm_form_googleclientId = googleClientId;
                }

                form_submission.crm_form_type = FormSubmissionTypes.LandingPage;
                form_submission.crm_form_created_datetime = DateTime.Now;

                db_ctx.crm_form_submissions.Add(form_submission);

                db_ctx.SaveChanges();
            }
            catch (Exception ex)
            {   
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }

        public bool SaveRCRIRSForm(RCRIRSFilterModel Filter)
        {
            bool success = false;
            string ErrorMsg = string.Empty;

            crm_form_submissions form_submission = new crm_form_submissions();

            try
            {
                if (Filter != null)
                {
                    form_submission.crm_form_guid = Guid.NewGuid();

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedSource))
                    {
                        form_submission.crm_form_inquiry_id = Filter.EncodedInquiryId;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedInquiryId))
                    {
                        form_submission.crm_form_inquiry_id = Filter.EncodedInquiryId;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedProgramId))
                    {
                        form_submission.crm_form_program = Filter.EncodedProgramId;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedFirstName))
                    {
                        form_submission.crm_form_firstname = Filter.EncodedFirstName;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedLastName))
                    {
                        form_submission.crm_form_lastname = Filter.EncodedLastName;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.BirthDate.ToString()))
                    {
                        form_submission.crm_form_birthdate = Filter.BirthDate.ToString();
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedEmailAddress))
                    {
                        form_submission.crm_form_emailaddress = Filter.EncodedEmailAddress;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedMobilePhone))
                    {
                        form_submission.crm_form_mobile_phone = Filter.EncodedMobilePhone;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedMailingCity))
                    {
                        form_submission.crm_form_city = Filter.EncodedMailingCity;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter.EncodedMailingProv))
                    {
                        form_submission.crm_form_province = Filter.EncodedMailingProv;
                    }

                    form_submission.crm_form_indigenous = Filter.Indigenous;
                    form_submission.crm_form_sfad_interest = Filter.SfadInterest;
                    form_submission.crm_form_ct_interest = Filter.CtInterest;
                    form_submission.crm_form_athletics_interest = Filter.AthleticInterest;
                    form_submission.crm_form_residence_interest = Filter.ResidenceInterest;
                    form_submission.crm_form_wellness_interest = Filter.WellnessInterest;
                    form_submission.crm_form_accessibility_interest = Filter.AccessibilityInterest;
                    form_submission.crm_form_prior_status = Filter.EncodedPriorStatus;
                    form_submission.crm_form_awards_interest = Filter.AwardsInterest;

                    form_submission.crm_form_type = FormSubmissionTypes.RCRIRS;
                    form_submission.crm_form_created_datetime = DateTime.Now;

                    db_ctx.crm_form_submissions.Add(form_submission);

                    if (db_ctx.SaveChanges() > 0) {
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

        public bool SaveCookieHistory(string uuid, DateTime date, string page, string googleClientId = null)
        {
            bool success = false;
            string ErrorMsg = string.Empty;

            crm_cookie_history cookie_history = new crm_cookie_history();

            try
            {
                cookie_history.crm_cookie_history_uuid = Guid.NewGuid();
                cookie_history.crm_cookie_uuid = new Guid(uuid);
                cookie_history.crm_created_datetime = date;
                cookie_history.crm_page_visited = page;
                cookie_history.crm_googleclientId = googleClientId;

                db_ctx.crm_cookie_history.Add(cookie_history);

                db_ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }

        public bool ProcessFormSubmissions()
        {
            bool Success = false;
            bool Processed = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;

            crm_contacts Contact = null;
            crm_inquiries Inquiry = null;

            try
            {
                MethodName = GetCurrentMethod();
                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                // Read new form submissions
                var DbFormSubmissions = db_ctx.crm_form_submissions.Where(x => x.crm_form_process_skip == false
                                                                           && x.crm_form_ack_sent == null
                                                                           //&& (x.crm_form_type == FormSubmissionTypes.LandingPage
                                                                           //|| x.crm_form_type == FormSubmissionTypes.ShorForm1)
                                                                           )
                                                                    .ToList();

                if (DbFormSubmissions.Any())
                {
                    if (MonitorExecution(MethodName, DbFormSubmissions.Count()))
                    {
                        foreach (crm_form_submissions DbFormSubmission in DbFormSubmissions)
                        {
                            // create an overload for form_submissions
                            Contact = UpsertCrmContact(DbFormSubmission);

                            // If contact exists in salesforce
                            if (Contact != null && !string.IsNullOrWhiteSpace(Contact.contact_id))
                            {
                                Inquiry = UpsertCrmInquiry(Contact);

                                // If Inquiry exists in salesforce
                                if (Inquiry != null && !string.IsNullOrWhiteSpace(Inquiry.inquiry_id))
                                {
                                    // Update Inquiry
                                    if (!string.IsNullOrWhiteSpace(DbFormSubmission.crm_form_source)) {
                                        Inquiry.inq_source = DbFormSubmission.crm_form_source;
                                    }
                                    
                                    //Inquiry.inq_pri_prog_interest = ;  // update program
                                    Inquiry.inq_modified_by = Settings.CrmAdmin;
                                    Inquiry.inq_modified_datetime = DateTime.Now;

                                    // Update Form Submission: Mark form as processed
                                    DbFormSubmission.crm_form_processed_datetime = DateTime.Now;
                                    DbFormSubmission.crm_form_contact_id = Contact.contact_id;

                                    //DbFormSubmission.modified_by = Settings.CrmAdmin;
                                    //DbFormSubmission.modified_datetime = DateTime.Now;

                                    if (DbFormSubmission.crm_form_type == FormSubmissionTypes.LandingPage
                                        || string.IsNullOrWhiteSpace(DbFormSubmission.crm_form_program))
                                    {
                                        // in this case, the acknowledgement was sent
                                        Processed = SendFormAcknowledgements(Contact, Inquiry);
                                    }
                                    else if (DbFormSubmission.crm_form_type == FormSubmissionTypes.RCRIRS
                                            && !string.IsNullOrWhiteSpace(DbFormSubmission.crm_form_program))
                                    {
                                        // in this case, the InquiryProgram was created
                                        // acknowledgement has not been sent yet
                                        // acknowledgement will be sent by broadcast system later
                                        // but we need to mark it as Processed/ack_sent so it doesn't keep trying to send it.
                                        Processed = UpsertCrmInquiryProgram(Inquiry, DbFormSubmission.crm_form_program);
                                    }
                                }
                            }

                            if (!Processed)
                            {
                                DbFormSubmission.crm_form_process_skip = true;
                            }
                            else {
                                DbFormSubmission.crm_form_ack_sent = DateTime.Now;
                            }

                            db_ctx.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return Success;
        }

        private crm_contacts UpsertCrmContact(crm_form_submissions FormSubmission)
        {
            string ErrorMsg = string.Empty;
            string FirstName = string.Empty;
            string LastName = string.Empty;
            string Email = null;
            string Source = string.Empty;
            DateTime? BirthDate = null;
            crm_contacts DbContact = null;
            Dictionary<string, sObject> ContactUpserts = new Dictionary<string, sObject>();

            try
            {
                if (FormSubmission != null)
                {
                    // Parse BirthDate
                    if (!string.IsNullOrWhiteSpace(FormSubmission.crm_form_birthdate))
                    {
                        string StrBirthDate = FormSubmission.crm_form_birthdate.Trim().TrimEnd('\r', '\n');
                        DateTime ParsedDate;
                        if (DateTime.TryParse(StrBirthDate, out ParsedDate))
                        {
                            BirthDate = ParsedDate;
                        }
                    }

                    // Parse First Name
                    if (!string.IsNullOrWhiteSpace(FormSubmission.crm_form_firstname))
                    {
                        FirstName = FormSubmission.crm_form_firstname.Trim().TrimEnd('\r', '\n');
                    }

                    // Parse Last Name
                    if (!string.IsNullOrWhiteSpace(FormSubmission.crm_form_lastname))
                    {
                        LastName = FormSubmission.crm_form_lastname.Trim().TrimEnd('\r', '\n');
                    }

                    // Parse Email
                    if (!string.IsNullOrWhiteSpace(FormSubmission.crm_form_emailaddress))
                    {
                        Email = FormSubmission.crm_form_emailaddress.Trim().TrimEnd('\r', '\n');
                    }

                    // Parse Source
                    if (!string.IsNullOrWhiteSpace(FormSubmission.crm_form_source))
                    {
                        Source = FormSubmission.crm_form_source.Trim().TrimEnd('\r', '\n');
                    }

                    // Minimum required fields
                    if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                    {
                        // Find Contact
                        var DbContacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                    && x.contact_mark_delete == false
                                                    && x.contact_first_name == FirstName
                                                    && x.contact_last_name == LastName
                                                    && (x.contact_birthdate == (BirthDate ?? x.contact_birthdate)
                                                        || (Email != null && Email.Contains("Lethbridgecollege") ?
                                                            x.contact_college_email == Email :
                                                            x.contact_alternate_email == (Email ?? x.contact_alternate_email)))
                                                    ).ToList();

                        if (DbContacts.Any())
                        {
                            // Update contact
                            DbContact = DbContacts.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();
                        }
                        else
                        {
                            // Create new contact
                            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                            {
                                DbContact = new crm_contacts()
                                {
                                    contact_guid = Guid.NewGuid(),
                                    contact_first_name = FirstName,
                                    contact_last_name = LastName,
                                    contact_birthdate = BirthDate,
                                    contact_lead_source = Source,
                                    contact_created_datetime = DateTime.Now,
                                    contact_created_by = Settings.CrmAdmin,
                                };

                                if (!string.IsNullOrWhiteSpace(Email))
                                {
                                    if (Email.Contains("Lethbridgecollege"))
                                    {
                                        DbContact.contact_college_email = Email;
                                        DbContact.contact_preferred_email = PreferredEmailTypes.UniversityEmailType;
                                    }
                                    else {
                                        DbContact.contact_alternate_email = Email;
                                        DbContact.contact_preferred_email = PreferredEmailTypes.AlternateEmailType;
                                    }
                                }

                                db_ctx.crm_contacts.Add(DbContact);
                            }
                        }

                        DbContact.contact_last_modified_datetime = DateTime.Now;
                        DbContact.contact_last_modified_by = Settings.CrmAdmin;

                        if (db_ctx.SaveChanges() > 0) {
                            // Upload contact if it's not in Salesforce
                            if (DbContact != null && string.IsNullOrWhiteSpace(DbContact.contact_id))
                            {
                                Contact SfContact = MapToSfContact(DbContact);

                                ContactUpserts.Add(DbContact.contact_guid.ToString(), SfContact);

                                UpsertRecords(InsertResults.Contact, ContactUpserts);

                                if (string.IsNullOrWhiteSpace(DbContact.contact_id) && !string.IsNullOrWhiteSpace(SfContact.Id)) {
                                    DbContact.contact_id = SfContact.Id;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return DbContact;
        }

        private crm_inquiries UpsertCrmInquiry(crm_contacts Contact)
        {
            string ErrorMsg = string.Empty;
            string TermCode = string.Empty;
            DateTime ExpireDate = DateTime.Now.AddDays(-90);  // Inquiry expires after 90 days
            crm_inquiries DbInquiry = null;
            Dictionary<string, sObject> InquiryUpserts = new Dictionary<string, sObject>();

            try
            {
                if (Contact != null)
                {
                    // Find Inquiry
                    var DbInquiries = db_ctx.crm_inquiries.Where(x => x.inq_deleted == false
                                                                   && x.inquiry_mark_delete == false
                                                                   && x.inq_contact_id == Contact.contact_id
                                                                   && x.inq_created_datetime > ExpireDate
                                                                ).ToList();

                    if (DbInquiries.Any())
                    {
                        // Update Inquiry
                        DbInquiry = DbInquiries.OrderByDescending(x => x.inq_modified_datetime).FirstOrDefault();
                    }
                    else
                    {
                        // Create new Inquiry
                        DbInquiry = new crm_inquiries()
                        {
                            inquiry_guid = Guid.NewGuid(),
                            inq_contact_id = Contact.contact_id,
                            inq_created_datetime = DateTime.Now,
                            inq_created_by = Settings.CrmAdmin,
                        };

                        db_ctx.crm_inquiries.Add(DbInquiry);
                    }

                    DbInquiry.inq_modified_datetime = DateTime.Now;
                    DbInquiry.inq_modified_by = Settings.CrmAdmin;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        if (DbInquiry != null && string.IsNullOrWhiteSpace(DbInquiry.inquiry_id))
                        {
                            lc_inquiry__c SfInquiry = MapToSfInquiries(DbInquiry);

                            InquiryUpserts.Add(DbInquiry.inquiry_guid.ToString(), SfInquiry);

                            UpsertRecords(InsertResults.Inquiry, InquiryUpserts);

                            if (string.IsNullOrWhiteSpace(DbInquiry.inquiry_id) && !string.IsNullOrWhiteSpace(SfInquiry.Id))
                            {
                                DbInquiry.inquiry_id = SfInquiry.Id;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return DbInquiry;
        }

        private bool UpsertCrmInquiryProgram(crm_inquiries Inquiry, string ProgramId)
        {
            string ErrorMsg = string.Empty;
            bool Success = false;
            crm_inquiry_programs DbInquiryProgram = null;

            try
            {
                if (Inquiry != null && !string.IsNullOrWhiteSpace(Inquiry.inquiry_id) && !string.IsNullOrWhiteSpace(ProgramId))
                {
                    // Find Inquiry
                    var DbInquiryPrograms = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_deleted == false
                                                                            && x.crm_inq_prog_mark_delete == false
                                                                            && x.crm_inq_prog_inquiry_id == Inquiry.inquiry_id
                                                                            && x.crm_inq_prog_program_id == ProgramId
                                                                            && x.crm_inq_prog_ack_sent == null
                                                                        ).ToList();

                    if (DbInquiryPrograms.Any())
                    {
                        // Update Inquiry Program
                        DbInquiryProgram = DbInquiryPrograms.OrderByDescending(x => x.crm_inq_prog_last_modified_datetime).FirstOrDefault();
                    }
                    else
                    {
                        // Create new Inquiry Program
                        DbInquiryProgram = new crm_inquiry_programs()
                        {
                            crm_inq_prog_guid = Guid.NewGuid(),
                            crm_inq_prog_inquiry_id = Inquiry.inquiry_id,
                            crm_inq_prog_program_id = ProgramId,
                            crm_inq_prog_created_datetime = DateTime.Now,
                            crm_inq_prog_created_by = Settings.CrmAdmin,
                        };

                        db_ctx.crm_inquiry_programs.Add(DbInquiryProgram);
                    }

                    DbInquiryProgram.crm_inq_prog_last_modified_by = Settings.CrmAdmin;
                    DbInquiryProgram.crm_inq_prog_last_modified_datetime = DateTime.Now;
                    
                    if (db_ctx.SaveChanges() > 0)
                    {
                        // Upload Inquiry Program if it's not in Salesforce
                        if (DbInquiryProgram != null 
                                && (string.IsNullOrWhiteSpace(DbInquiryProgram.crm_inq_prog_id)
                                || DbInquiryProgram.crm_inq_prog_ack_sent == null))
                        {
                            lc_inquiry_program__c SfInquiryProgram = MapToSfInquiryProgram(DbInquiryProgram);

                            Dictionary<string, sObject> InquiryProgramUpserts = new Dictionary<string, sObject>();
                            InquiryProgramUpserts.Add(DbInquiryProgram.crm_inq_prog_guid.ToString(), SfInquiryProgram);

                            UpsertRecords(InsertResults.InquiryProgram, InquiryProgramUpserts);

                            if (string.IsNullOrWhiteSpace(DbInquiryProgram.crm_inq_prog_id) && !string.IsNullOrWhiteSpace(SfInquiryProgram.Id))
                            {
                                DbInquiryProgram.crm_inq_prog_id = SfInquiryProgram.Id;
                            }
                        }

                        Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return Success;
        }

        public bool SendFormAcknowledgements(crm_contacts Contact, crm_inquiries Inquiry)
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool Success = false;
            string MethodName = string.Empty;
            string ErrorMsg = string.Empty;
            string EmailTemplateId = string.Empty;
            List<string> ProgramIdList = new List<string>();
            DateTime StartTime = DateTime.Now;

            try
            {
                if (!string.IsNullOrEmpty(Inquiry.inq_source))
                {
                    switch (Inquiry.inq_source.ToLower())
                    {
                        case FormSourceTypes.mkt20cho:
                            EmailTemplateId = "00X1U000000NXxaUAG";
                            break;
                        case FormSourceTypes.mkt20eng:
                            EmailTemplateId = "00X1U000000NXxGUAW";
                            break;
                        case FormSourceTypes.mkt20exp:
                            EmailTemplateId = "00X1U000000NXxVUAW";
                            break;
                        case FormSourceTypes.mkt20inv:
                            EmailTemplateId = "00X1U000000NXxQUAW";
                            break;
                        case FormSourceTypes.mkt20trn:
                            EmailTemplateId = "00X1U000000NXxBUAW";
                            break;
                        case FormSourceTypes.mkt20wtt:
                            ProgramIdList.Add("0011U00000MSVRdQAP");
                            EmailTemplateId = "00X1U000000NY2zUAG";
                            break;
                        case FormSourceTypes.mkt20hea:
                            ProgramIdList.Add("0011U00000MSVPBQA5");
                            EmailTemplateId = "00X1U000000NY39UAG";
                            break;
                        case FormSourceTypes.mkt20ber:
                            EmailTemplateId = "00X1U000000NXxkUAG";
                            break;
                        case FormSourceTypes.mkt20rtc:
                            EmailTemplateId = "00X1U000000NXxfUAG";
                            break;
                        case FormSourceTypes.rcrirs1:
                            // there is no program, no inquiry ,or program_inquiry
                            // 
                            break;
                        case FormSourceTypes.rcrirs1a:
                            // has inquiry_id
                            break;
                        case FormSourceTypes.rcrirs1b:
                            // has inquiry_id

                            break;
                        case FormSourceTypes.rcrirs2:
                            // has inquiry_id

                            break;
                        case FormSourceTypes.rcrirs3:
                            // has inquiry_id

                            break;
                        default:
                            EmailTemplateId = "00X1U000000NY7BUAW";
                            break;
                    }

                    foreach (string ProgramId in ProgramIdList)
                    {
                        Success = UpsertCrmInquiryProgram(Inquiry, ProgramId);
                    }
                    
                    if (Success || !ProgramIdList.Any())
                    {
                        // Send Acknowledgement email
                        Success = SendAcknowledgementEmail(Contact, Inquiry, EmailTemplateId);

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return Success;
        }

        private bool SendAcknowledgementEmail(crm_contacts Contact, crm_inquiries Inquiry, string EmailTemplateId)
        {
            string ErrorMsg = string.Empty;
            bool Success = false;
            string RecipientEmailAddress = string.Empty;
            Dictionary<string, sObject> InquiryProgramUpserts = new Dictionary<string, sObject>();

            try
            {
                if (!string.IsNullOrWhiteSpace(EmailTemplateId) &&
                    Contact != null && !string.IsNullOrWhiteSpace(Contact.contact_id))
                {
                    var EmailTemplate = db_ctx.crm_email_templates.Where(x => x.email_template_id == EmailTemplateId).FirstOrDefault();

                    string EmailBody = string.Empty;
                    string EmailSubject = string.Empty;

                    if (EmailTemplate != null)
                    {
                        EmailBody = EmailTemplate.email_template_html_value;
                        EmailSubject = EmailTemplate.email_template_subject;

                        if (!string.IsNullOrEmpty(EmailBody))
                        {
                            if (EmailBody.Contains("{{{Recipient.FirstName}}}"))
                            {
                                EmailBody = EmailBody.Replace("{{{Recipient.FirstName}}}", Contact.contact_first_name);
                            }

                            if (EmailBody.Contains("{{{Recipient.Id}}}"))
                            {
                                EmailBody = EmailBody.Replace("{{{Recipient.Id}}}", Contact.contact_id);
                            }

                            if (EmailBody.Contains("{{{Inquiry.Id}}}"))
                            {
                                EmailBody = EmailBody.Replace("{{{Inquiry.Id}}}", Inquiry.inquiry_id);
                            }
                        }
                    }

                    if (Contact.contact_preferred_email == PreferredEmailTypes.UniversityEmailType)
                    {
                        RecipientEmailAddress = Contact.contact_college_email;
                    }
                    else
                    {
                        RecipientEmailAddress = Contact.contact_alternate_email;
                    }

                    if (!string.IsNullOrWhiteSpace(RecipientEmailAddress))
                    {
                        SendEmail sendEmail = new SendEmail()
                        {
                            Body = EmailBody,
                            Title = EmailSubject,
                            To = RecipientEmailAddress,
                            Subtitle = "Lethbridge College",
                            From = "futurestudent@lethbridgecollege.ca",
                        };

                        Success = sendEmail.Send();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            if (!string.IsNullOrWhiteSpace(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return Success;
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
