using CrmLcLib;
using lc.crm;
using lc.crm.api;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;

namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        private bool CreateEmailBroadcasts()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string MethodName = string.Empty;
            string ErrorMsg = string.Empty;
            string report_id = string.Empty;
            string report_name = string.Empty;
            string template_id = string.Empty;
            string template_name = string.Empty;
            DateTime StartTime = DateTime.Now;
            DateTime Today = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            DateTime StartDate = DateTime.Today;
            DateTime Yesterday = DateTime.Today.AddDays(-1);
            DateTime Tomorrow = DateTime.Today.AddDays(1);
            TimeSpan TimeOfDay = DateTime.Now.TimeOfDay;
            DateTime? TimeToSend = DateTime.Now;

            crm_email_broadcasts crm_email_broadcast = new crm_email_broadcasts();
            List<crm_email_broadcasts> crm_email_broadcasts = new List<crm_email_broadcasts>();
            List<crm_email_broadcasts> updated_email_broadcasts = new List<crm_email_broadcasts>();
            List<crm_email_broadcasts> new_email_broadcasts = new List<crm_email_broadcasts>();
            List<crm_email_broadcasts> abandoned_broadcasts = new List<crm_email_broadcasts>();
            List<crm_email_campaigns> crm_email_campaigns = new List<crm_email_campaigns>();
            List<Guid> valid_broadcast_guids = new List<Guid>();
            Dictionary<string, sObject> BroadcastUpserts = new Dictionary<string, sObject>();

            try
            {

                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = GetLastRunDateTime(MethodName);

                crm_email_campaigns = db_ctx.crm_email_campaigns.Where(x => (x.ec_deleted == false
                                                                    && x.ec_mark_delete == false
                                                                    && x.ec_active_flag == true
                                                                    && ((x.ec_send_time <= TimeOfDay
                                                                    && ((x.ec_recur == false && x.ec_start_datetime > Yesterday && x.ec_start_datetime < Tomorrow)
                                                                    || (x.ec_recur == true
                                                                        && (x.ec_start_datetime <= Today
                                                                        && (x.ec_end_datetime == null || x.ec_end_datetime >= Today))))))
                                                                    || (x.ec_allow_ongoing_delivery == true
                                                                        && x.ec_recur == true
                                                                        && x.ec_allow_repeat_broadcasts == true)))
                                                                .OrderByDescending(x => x.ec_modified_datetime).ToList();

                foreach (crm_email_campaigns crm_email_campaign in crm_email_campaigns)
                {
                    bool AllowRepeatCampaignBroadcast = crm_email_campaign.ec_allow_repeat_broadcasts ?? false;
                    bool AllowOngoingCampaignBroadcasts = crm_email_campaign.ec_allow_ongoing_delivery;

                    report_id = crm_email_campaign.ec_report_id;
                    template_id = crm_email_campaign.ec_template_id;

                    if (crm_email_campaign.ec_start_datetime.Value.Date > DateTime.Today)
                    {
                        StartDate = crm_email_campaign.ec_start_datetime ?? DateTime.Today;
                    }

                    if (AllowOngoingCampaignBroadcasts) {
                        StartDate = DateTime.Now;
                    }

                    String Days = crm_email_campaign.ec_recur_week_days ?? DateTime.Today.ToString("dddd", CurrentDateFormat);

                    List<string> DaysOfWeek = Days.Split(';').ToList<string>();

                    var day = StartDate.Date;
                    string DayOfWeek = day.ToString("dddd", CurrentDateFormat);

                    if (!string.IsNullOrEmpty(DayOfWeek) && DaysOfWeek.Contains(DayOfWeek))
                    {
                        string[] BroadCastStatuses = { BroadcastStatus.Scheduled, BroadcastStatus.Completed, BroadcastStatus.Created };
                        string[] ScheduledBroadCastStatuses = { BroadcastStatus.Scheduled, BroadcastStatus.Created };
                        crm_email_broadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_send.Value.Year <= day.Year
                                                                                && x.email_broadcast_send.Value.Month <= day.Month
                                                                                && x.email_broadcast_send.Value.Day <= day.Day
                                                                                && x.email_broadcast_deleted == false
                                                                                && x.email_broadcast_mark_delete == false
                                                                                && x.email_campaign_id == crm_email_campaign.email_campaign_id
                                                                                && BroadCastStatuses.Contains(x.email_broadcast_status))
                                                                          .OrderByDescending(x => x.email_broadcast_modfied_datetime).ToList();

                        crm_email_broadcast = null;
                        bool NewBroadcast = false;

                        bool AllowReschedule = (AllowRepeatCampaignBroadcast || AllowOngoingCampaignBroadcasts);
                        bool AlreadyScheduled = crm_email_broadcasts.Any(x => ScheduledBroadCastStatuses.Contains(x.email_broadcast_status));
                        bool ExistsTodayBroadcasts = crm_email_broadcasts.Any(x => x.email_broadcast_send.Value.Year == day.Year
                                                                        && x.email_broadcast_send.Value.Month == day.Month
                                                                        && x.email_broadcast_send.Value.Day == day.Day);

                        // If no broadcast found today and there is no past scheduled broadcast => create one
                        if (!ExistsTodayBroadcasts && !AlreadyScheduled)
                        {
                            NewBroadcast = true;
                        } else
                        {
                            // If Scheduled/Created broadcast found => use it and don't create another one
                            if (AlreadyScheduled)
                            {
                                crm_email_broadcast = crm_email_broadcasts.Where(x => ScheduledBroadCastStatuses.Contains(x.email_broadcast_status)).OrderByDescending(x => x.email_broadcast_modfied_datetime).FirstOrDefault();
                            } else
                            {
                                // If no Scheduled/Created broadcast found, but allow repeat/ongoing => create one
                                if (AllowReschedule)
                                {
                                    NewBroadcast = true;
                                }
                            }
                        }

                        if (NewBroadcast)
                        {
                            crm_email_broadcast = new crm_email_broadcasts()
                            {
                                email_broadcast_guid = Guid.NewGuid(),
                                email_broadcast_status = BroadcastStatus.Created,
                                email_broadcast_created_by = Settings.CrmAdmin,
                                email_broadcast_created_datetime = DateTime.Now,
                            };

                            db_ctx.crm_email_broadcasts.Add(crm_email_broadcast);

                            inserted++;
                        }

                        if (crm_email_broadcast != null)
                        {
                            // update scheduled broadcasts (skip completed etc)
                            if (ScheduledBroadCastStatuses.Contains(crm_email_broadcast.email_broadcast_status))
                            {
                                if (!AllowOngoingCampaignBroadcasts)
                                {
                                    crm_email_broadcast.email_broadcast_send = new DateTime(day.Year, day.Month, day.Day, crm_email_campaign.ec_send_time.Value.Hours, crm_email_campaign.ec_send_time.Value.Minutes, crm_email_campaign.ec_send_time.Value.Seconds);
                                }
                                else {
                                    crm_email_broadcast.email_broadcast_send = StartDate;
                                }

                                crm_email_broadcast.email_campaign_id = crm_email_campaign.email_campaign_id;
                                crm_email_broadcast.email_broadcast_email_campaign = crm_email_campaign.email_campaign_id;

                                crm_email_broadcast.email_report_id = report_id;
                                crm_email_broadcast.email_report_name = db_ctx.crm_reports.Where(x => x.crm_report_id == crm_email_campaign.ec_report_id
                                                                                                && x.crm_report_deleted == false
                                                                                                && x.crm_report_mark_delete == false)
                                                                                        .OrderByDescending(w => w.crm_report_modified_date)
                                                                                        .Select(y => y.crm_report_name)
                                                                                        .FirstOrDefault() ?? string.Empty;

                                crm_email_broadcast.email_template_id = template_id;
                                crm_email_broadcast.email_template_name = db_ctx.crm_email_templates.Where(x => x.email_template_id == crm_email_campaign.ec_template_id
                                                                                                                    && x.email_template_deleted == false
                                                                                                                    && x.email_template_mark_delete == false)
                                                                                                    .OrderByDescending(w => w.email_template_modified_datetime)
                                                                                                    .Select(y => y.email_template_name)
                                                                                                    .FirstOrDefault() ?? string.Empty;

                                crm_email_broadcast.email_broadcast_modified_by = Settings.CrmAdmin;
                                crm_email_broadcast.email_broadcast_modfied_datetime = DateTime.Now;

                                crm_email_broadcast.email_broadcast_sys_modstamp = DateTime.Now;
                            }

                            //if (string.IsNullOrWhiteSpace(crm_email_broadcast.email_broadcast_id))
                            //{
                            //    lc_email_broadcast__c SfEmailBroadcast = MapToSfEmailBroadcasts(crm_email_broadcast);

                            //    BroadcastUpserts.Add(crm_email_broadcast.email_broadcast_guid.ToString(), SfEmailBroadcast);

                            //    UpsertRecords(Enumerations.InsertResults.EmailBroadcast, BroadcastUpserts);

                            //    if (string.IsNullOrWhiteSpace(crm_email_broadcast.email_broadcast_id) && !string.IsNullOrWhiteSpace(SfEmailBroadcast.Id))
                            //    {
                            //        crm_email_broadcast.email_broadcast_id = SfEmailBroadcast.Id;
                            //    }
                            //}
                            
                            valid_broadcast_guids.Add(crm_email_broadcast.email_broadcast_guid);
                        }
                    }
                }

                abandoned_broadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
                                                                            && x.email_broadcast_mark_delete == false
                                                                            && x.email_broadcast_status == BroadcastStatus.Created
                                                                            && x.email_broadcast_status == BroadcastStatus.Scheduled
                                                                            && !valid_broadcast_guids.Contains(x.email_broadcast_guid))
                                                                    .ToList();
                if (abandoned_broadcasts.Any())
                {
                    foreach (crm_email_broadcasts broadcast in abandoned_broadcasts)
                    {
                        broadcast.email_broadcast_mark_delete = true;
                        broadcast.email_broadcast_status = BroadcastStatus.Cancelled;
                        broadcast.email_broadcast_modfied_datetime = DateTime.Now;
                    }
                }

                db_ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool ScheduleEmailMessages()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;
            int msgcnt = 0;

            bool success = false;
            bool AllowDuplicateRecipients = false;
            string secondaryId = string.Empty;
            string ErrorMsg = string.Empty;
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<DateTime> DateList = new List<DateTime>();
            List<string> SendToAddressTypes = new List<string>();
            List<Record> EmailRecipients = new List<Record>();
            crm_email_templates crm_email_template = new crm_email_templates();
            crm_email_campaigns crm_email_campaign = new crm_email_campaigns();
            crm_reports crm_report = new crm_reports();
            List<crm_email_broadcasts> crm_broadcasts_to_delete = new List<crm_email_broadcasts>();
            List<crm_email_templates> crm_email_templates = new List<crm_email_templates>();
            List<crm_email_campaigns> crm_email_campaigns = new List<crm_email_campaigns>();
            List<crm_email_broadcasts> crm_email_broadcasts = new List<crm_email_broadcasts>();

            // Salesforce Reporting API settings maintained in Web.Config
            string clientId = WebConfigurationManager.AppSettings["SFClientId"];
            string clientSecret = WebConfigurationManager.AppSettings["SFClientSecret"];
            string username = WebConfigurationManager.AppSettings["SFClientUsername"];
            string password = WebConfigurationManager.AppSettings["SFClientPassword"];
            string analyticApiUrl = WebConfigurationManager.AppSettings["SFReportingApiUrl"];

            try
            {
                MethodName = GetCurrentMethod();

                crm_email_broadcasts = db_ctx.crm_email_broadcasts.Where(x => (x.email_broadcast_deleted == false
                                                                            && x.email_broadcast_mark_delete == false
                                                                            && x.email_broadcast_status == "Scheduled"
                                                                            || x.email_broadcast_status == "Created")
                                                                            && x.email_broadcast_send <= DateTime.Now
                                                                            && x.email_broadcast_id != null
                                                                            )
                                                                    .OrderByDescending(x => x.email_broadcast_modfied_datetime).ToList();
                
                foreach (crm_email_broadcasts broadcast in crm_email_broadcasts)
                {
                    SendToAddressTypes = new List<string>();
                    crm_email_template = new crm_email_templates();
                    EmailRecipients = new List<Record>();

                    try
                    {
                        crm_email_campaigns = db_ctx.crm_email_campaigns.Where(x => x.ec_deleted == false
                                                                                && x.ec_mark_delete == false
                                                                                && x.ec_active_flag == true
                                                                                && x.email_campaign_id == broadcast.email_campaign_id).ToList();

                        if (crm_email_campaigns.Any())
                        {
                            crm_email_campaign = crm_email_campaigns.OrderByDescending(x => x.ec_modified_datetime).FirstOrDefault();

                            if (crm_email_campaign != null)
                            {
                                AllowDuplicateRecipients = crm_email_campaign.ec_allow_ongoing_delivery;

                                crm_email_templates = db_ctx.crm_email_templates.Where(x => x.email_template_deleted == false
                                                                                                && x.email_template_id == crm_email_campaign.ec_template_id)
                                                                                    .ToList();

                                if (crm_email_templates.Any())
                                {
                                    crm_email_template = crm_email_templates.OrderByDescending(x => x.email_template_modified_datetime).FirstOrDefault();

                                    if (crm_email_template != null)
                                    {
                                        var reports = db_ctx.crm_reports.Where(x => x.crm_report_deleted == false
                                                                                    && x.crm_report_mark_delete == false
                                                                                    && x.crm_report_id == crm_email_campaign.ec_report_id);
                                        if (reports.Any())
                                        {
                                            crm_report = reports.OrderByDescending(x => x.crm_report_modified_date).FirstOrDefault();

                                            if (crm_report != null)
                                            {
                                                var client = new SalesforceClient();
                                                var authFlow = new Security.UsernamePasswordAuthenticationFlow(clientId, clientSecret, username, password);

                                                client.Authenticate(authFlow);

                                                var reportRecords = client.GetMergeObjectIdsByReportId<List<object>>(analyticApiUrl, crm_report.crm_report_id);

                                                if (reportRecords.Any())
                                                {
                                                    foreach (JObject record in reportRecords)
                                                    {
                                                        // for special cases where InquiryProgramId is not available (yet)
                                                        bool SkipEmailRecipient = false;

                                                        Record EmailRecipient = new Record();

                                                        var contactId = record["dataCells"][0]["value"];

                                                        if (contactId != null)
                                                        {
                                                            string contact_id = contactId.ToString();

                                                            if (!string.IsNullOrEmpty(contact_id))
                                                            {
                                                                if (AllowDuplicateRecipients || !EmailRecipients.Any(x => x.ContactId == contact_id))
                                                                {
                                                                    if (GetSfRecordTypeById(contact_id) == SfRecordTypes.Contact)
                                                                    {
                                                                        EmailRecipient = db_ctx.crm_contacts.Where(x => x.contact_id == contact_id
                                                                                                                && x.contact_deleted == false
                                                                                                                && x.contact_mark_delete == false
                                                                                                                && x.contact_invalid_email_flag == false
                                                                                                                && x.contact_email_opt_out == false
                                                                                                                && x.contact_is_email_bounced == false)
                                                                                                            .Select(y => new Record()
                                                                                                            {
                                                                                                                PreferredEmail = y.contact_email,
                                                                                                                AlternativeEmail = y.contact_alternate_email,
                                                                                                                CollegeEmail = y.contact_college_email,
                                                                                                                FirstName = y.contact_first_name,
                                                                                                                ContactId = y.contact_id,
                                                                                                                StudentId = y.contact_colleague_id,
                                                                                                                AccountId = y.contact_account_id
                                                                                                            })
                                                                                                            .Distinct().FirstOrDefault();
                                                                    }

                                                                    if (EmailRecipient != null)
                                                                    {
                                                                        // grab the relevant Inquiry Id (If Exists!)
                                                                        if (record["dataCells"].Count() > 1)
                                                                        {
                                                                            var InquiryId = record["dataCells"][1]["value"];

                                                                            if (InquiryId != null)
                                                                            {
                                                                                string inquiry_id = InquiryId.ToString();

                                                                                if (!string.IsNullOrWhiteSpace(inquiry_id))
                                                                                {
                                                                                    if (GetSfRecordTypeById(inquiry_id) == SfRecordTypes.Inquiry)
                                                                                    {
                                                                                        var inquiry = db_ctx.crm_inquiries.Where(x => x.inquiry_mark_delete == false
                                                                                                                                                    && x.inq_deleted == false
                                                                                                                                                    && x.inquiry_id == inquiry_id)
                                                                                                                                            .OrderByDescending(y => y.inq_modified_datetime)
                                                                                                                                            .FirstOrDefault();
                                                                                        if (inquiry != null)
                                                                                        {
                                                                                            secondaryId = inquiry.inquiry_id;
                                                                                            EmailRecipient.InquiryId = inquiry.inquiry_id;
                                                                                            EmailRecipient.NoCommunication = inquiry.inq_no_communications;
                                                                                            if (inquiry.inq_pri_prog_interest != null)
                                                                                            {
                                                                                                EmailRecipient.ProgramInterestId1 = inquiry.inq_pri_prog_interest;
                                                                                            }
                                                                                            if (inquiry.inq_sec_prog_interest != null)
                                                                                            {
                                                                                                EmailRecipient.ProgramInterestId2 = inquiry.inq_sec_prog_interest;
                                                                                            }
                                                                                            if (inquiry.inq_services_interest != null)
                                                                                            {
                                                                                                EmailRecipient.ServicesInterest = inquiry.inq_services_interest;
                                                                                            }

                                                                                            // grab the relevant Inquiry Id (If Exists!)
                                                                                            if (record["dataCells"].Count() > 2)
                                                                                            {
                                                                                                var InquiryProgramId = record["dataCells"][2]["value"];

                                                                                                if (InquiryProgramId != null)
                                                                                                {
                                                                                                    string inquiry_program_id = InquiryProgramId.ToString();

                                                                                                    if (!string.IsNullOrWhiteSpace(inquiry_program_id))
                                                                                                    {
                                                                                                        if (GetSfRecordTypeById(inquiry_program_id) == SfRecordTypes.InquiryProgram)
                                                                                                        {
                                                                                                            // if program inquiry record, mark acknowledgement sent
                                                                                                            if (!string.IsNullOrWhiteSpace(inquiry_program_id))
                                                                                                            {
                                                                                                                var inquiry_program = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_deleted == false
                                                                                                                                                                    && x.crm_inq_prog_mark_delete == false
                                                                                                                                                                    && x.crm_inq_prog_id == inquiry_program_id)
                                                                                                                                                                .OrderByDescending(y => y.crm_inq_prog_last_modified_datetime)
                                                                                                                                                                .FirstOrDefault();
                                                                                                                if (inquiry_program != null)
                                                                                                                {
                                                                                                                    EmailRecipient.ProgramId = inquiry_program.crm_inq_prog_program_id;
                                                                                                                    EmailRecipient.InquiryProgramId = inquiry_program.crm_inq_prog_id;
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    SkipEmailRecipient = true;
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            // skip recipient
                                                                            // just in cases where we do not have a InquiryProgramId
                                                                            if (!SkipEmailRecipient)
                                                                            {
                                                                                EmailRecipients.Add(EmailRecipient);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                    broadcast.email_template_name = crm_email_template.email_template_name;

                                                    if (crm_email_campaign.ec_email_address_type == PickListValues.BothEmailType)
                                                    {
                                                        SendToAddressTypes.Add(PickListValues.CollegeEmailType);
                                                        SendToAddressTypes.Add(PickListValues.AlternateEmailType);
                                                    }
                                                    else
                                                    {
                                                        SendToAddressTypes.Add(crm_email_campaign.ec_email_address_type);
                                                    }

                                                    foreach (string SendToAddressType in SendToAddressTypes)
                                                    {
                                                        List<crm_email_messages> messages_to_send = new List<crm_email_messages>();

                                                        if (MonitorExecution(crm_email_campaign.ec_name, EmailRecipients.Count()))
                                                        {
                                                            foreach (Record EmailRecipient in EmailRecipients)
                                                            {
                                                                if (EmailRecipient != null)
                                                                {
                                                                    string recipient_email_address = string.Empty;

                                                                    switch (SendToAddressType)
                                                                    {
                                                                        case "Preferred":
                                                                            recipient_email_address = EmailRecipient.PreferredEmail;
                                                                            break;
                                                                        case "Alternate":
                                                                            recipient_email_address = EmailRecipient.AlternativeEmail;
                                                                            break;
                                                                        case "College":
                                                                            recipient_email_address = EmailRecipient.CollegeEmail;
                                                                            break;
                                                                        default:
                                                                            break;
                                                                    }

                                                                    bool MessageQueued = false;
                                                                    if (AllowDuplicateRecipients)
                                                                    {
                                                                        MessageQueued = false;
                                                                    }
                                                                    else { 
                                                                        MessageQueued = db_ctx.crm_email_messages.Where(x => x.email_message_campaign_id == crm_email_campaign.email_campaign_id
                                                                                                                                && x.email_message_broadcast_id == broadcast.email_broadcast_id
                                                                                                                                && x.email_message_contact_id == EmailRecipient.ContactId
                                                                                                                                && x.email_message_to_address == recipient_email_address).Any();
                                                                    }

                                                                    if (!MessageQueued) {
                                                                        try
                                                                        {
                                                                            crm_email_messages crm_email_message = new crm_email_messages()
                                                                            {
                                                                                email_message_guid = Guid.NewGuid(),
                                                                                email_message_status = "Scheduled",
                                                                                email_message_secondary_id = secondaryId,
                                                                                email_message_contact_id = EmailRecipient.ContactId,
                                                                                email_message_account_id = EmailRecipient.AccountId,
                                                                                email_message_campaign_id = crm_email_campaign.email_campaign_id,
                                                                                email_message_broadcast_id = broadcast.email_broadcast_id,
                                                                                email_message_report_id = crm_report.crm_report_id,
                                                                                email_message_template_id = crm_email_template.email_template_id,
                                                                                email_message_subject = crm_email_template.email_template_subject,
                                                                                email_message_to_address = recipient_email_address,
                                                                                email_message_from_address = crm_email_campaign.ec_from_email_address,
                                                                                email_message_from_name = crm_email_campaign.ec_from_email_address_title,
                                                                                email_message_bcc_address = Settings.CrmEmailAddress,
                                                                                email_message_is_incoming = false,
                                                                                email_message_datetime = DateTime.Now,
                                                                                email_message_created_by = Settings.CrmAdmin,
                                                                                email_message_created_datetime = DateTime.Now,
                                                                                email_message_modified_by = Settings.CrmAdmin,
                                                                                email_message_modified_datetime = DateTime.Now,
                                                                                email_message_html_body = MergeEmailBody(crm_email_template.email_template_html_value, EmailRecipient, true),
                                                                            };

                                                                            db_ctx.crm_email_messages.Add(crm_email_message);


                                                                            // if program inquiry record, mark acknowledgement sent
                                                                            if (!string.IsNullOrWhiteSpace(EmailRecipient.InquiryProgramId)) {
                                                                                crm_inquiry_programs inquiry_program = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_deleted == false
                                                                                                                                                                && x.crm_inq_prog_mark_delete == false
                                                                                                                                                                && x.crm_inq_prog_id == EmailRecipient.InquiryProgramId)
                                                                                                                                                            .OrderByDescending(y => y.crm_inq_prog_last_modified_datetime)
                                                                                                                                                            .FirstOrDefault();
                                                                                if (inquiry_program != null)
                                                                                {
                                                                                    inquiry_program.crm_inq_prog_ack_sent = DateTime.Now;
                                                                                    inquiry_program.crm_inq_prog_last_modified_datetime = DateTime.Now;

                                                                                    db_ctx.SaveChanges();
                                                                                }
                                                                            }

                                                                            msgcnt++;

                                                                            if (string.IsNullOrEmpty(crm_email_message.email_message_to_address))
                                                                            {
                                                                                crm_email_message.email_message_status = "Failed";
                                                                            }

                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            ErrorMsg += ex.ToString();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        
                        if (EmailRecipients.Any())
                        {
                            broadcast.email_broadcast_status = "Scheduled";
                            
                        }
                        else {
                            broadcast.email_broadcast_status = "Completed";
                            broadcast.email_broadcast_sent = DateTime.Now;
                            broadcast.email_broadcast_messages_sent = msgcnt;
                        }

                        if (db_ctx.SaveChanges() > 0)
                        {
                            success = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool SendEmailMessages()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;
            int cnt = 0;
            bool success = false;

            bool SaveBroadcast = false;
            string PreviousBroadcastId = string.Empty;
            string CurrentBroadcastId = string.Empty;
            string NextBroadcastId = string.Empty;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            crm_email_broadcasts crm_email_broadcast = new crm_email_broadcasts();
            List<crm_email_broadcasts> crm_email_broadcasts = new List<crm_email_broadcasts>();

            try
            {
                MethodName = GetCurrentMethod();

                List<crm_email_messages> crm_email_messages = db_ctx.crm_email_messages.Where(x => x.email_message_status == "Scheduled"
                                                                                             && x.email_message_deleted == false
                                                                                             && x.email_message_broadcast_id != null)
                                                                                       .OrderByDescending(x => x.email_message_broadcast_id).ToList();

                if (MonitorExecution(MethodName, crm_email_messages.Count))
                {
                    for (int i = 0; i < crm_email_messages.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(crm_email_messages[i].email_message_broadcast_id))
                        {
                            CurrentBroadcastId = crm_email_messages[i].email_message_broadcast_id;

                            if (i + 1 < crm_email_messages.Count) { NextBroadcastId = crm_email_messages[i + 1].email_message_broadcast_id; }
                            else { NextBroadcastId = string.Empty; }

                            crm_email_broadcast = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_id == CurrentBroadcastId)
                                                                            .OrderByDescending(x => x.email_broadcast_modfied_datetime)
                                                                            .FirstOrDefault();

                            if (crm_email_messages[i].email_message_broadcast_id == crm_email_broadcast.email_broadcast_id)
                            {

                                SendEmail sendEmail = new SendEmail()
                                {
                                    Body = crm_email_messages[i].email_message_html_body.Trim(),
                                    Title = crm_email_messages[i].email_message_subject.Trim(),
                                    To = crm_email_messages[i].email_message_to_address.Trim(),
                                    From = crm_email_messages[i].email_message_from_address.Trim(),
                                };

                                success = sendEmail.Send();


                                if (success && string.IsNullOrEmpty(sendEmail.ErrorMsg))
                                {
                                    cnt++;

                                    crm_tasks crm_task = new crm_tasks()
                                    {
                                        activity_guid = Guid.NewGuid(),
                                        task_account_id = crm_email_messages[i].email_message_account_id,
                                        task_who_id = crm_email_messages[i].email_message_contact_id,
                                        task_assigned_to_id = crm_email_messages[i].email_message_contact_id,
                                        task_what_id = crm_email_messages[i].email_message_broadcast_id,
                                        task_subject = crm_email_messages[i].email_message_subject,
                                        task_name_id = crm_email_messages[i].email_message_id,
                                        task_related_to_id = crm_email_messages[i].email_message_broadcast_id,
                                        task_subtype = "BroadcastEmail",
                                        task_priority = "Normal",
                                        task_status = "Completed",
                                        task_created_by = Settings.CrmAdmin,
                                        task_created_datetime = DateTime.Now,
                                        task_modified_by = Settings.CrmAdmin,
                                        task_modified_datetime = DateTime.Now,
                                        task_description = sendEmail.Body,
                                    };

                                    db_ctx.crm_tasks.Add(crm_task);

                                    crm_email_messages[i].email_message_status = "Completed";
                                    crm_email_messages[i].email_message_datetime = DateTime.Now;
                                    crm_email_messages[i].email_message_sent_datetime = DateTime.Now;

                                    if (db_ctx.SaveChanges() > 0) {
                                        success = true;
                                    }

                                }
                                else
                                {
                                    ErrorMsg += " Failed to send: " + sendEmail.ErrorMsg;
                                    success = false;
                                }
                            }
                        }

                        if (crm_email_messages.Count <= 1)
                        {
                            SaveBroadcast = true;
                        }
                        else
                        {
                            SaveBroadcast = CurrentBroadcastId != NextBroadcastId;
                        }

                        if (SaveBroadcast) {
                            crm_email_broadcast.email_broadcast_messages_sent = cnt;
                            crm_email_broadcast.email_broadcast_sent = DateTime.Now;
                            crm_email_broadcast.email_broadcast_status = "Completed";
                            crm_email_broadcast.email_broadcast_modfied_datetime = DateTime.Now;
                            cnt = 0;

                            if (db_ctx.SaveChanges() > 0)
                            {
                                success = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private string MergeEmailBody(string emailBody, Record record, bool html)
        {
            string ErrorMsg = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(emailBody))
                {

                    emailBody = CleanDynamicCodes(emailBody);

                    // dynamic programs merge
                    if (emailBody.Contains("&lt;&lt;&lt;DC_RCR_IR_IL_PRG&gt;&gt;&gt;"))
                    {
                        string merge_content = GetProgramDynamicContent(record.ProgramId);

                        emailBody = emailBody.Replace("&lt;&lt;&lt;DC_RCR_IR_IL_PRG&gt;&gt;&gt;", merge_content);

                        emailBody = CleanDynamicCodes(emailBody);
                    }

                    // dynamic program footer merge
                    if (emailBody.Contains("&lt;&lt;&lt;DC_RCR_IR_IL_DYNAMIC&gt;&gt;&gt;"))
                    {
                        string merge_content = GetNewDynamicContent("DC_RCR_IR_IL_DYNAMIC");

                        emailBody = emailBody.Replace("&lt;&lt;&lt;DC_RCR_IR_IL_DYNAMIC&gt;&gt;&gt;", merge_content);

                        emailBody = CleanDynamicCodes(emailBody);
                    }

                    // dynamic services merge
                    if (emailBody.Contains("&lt;&lt;&lt;DC_RCR_SVC&gt;&gt;&gt;"))
                    {
                        string merge_content = string.Empty;
                        if (!string.IsNullOrEmpty(record.ServicesInterest))
                        {
                            foreach (string ServiceInterestCode in record.ServicesInterest.Split(';').Distinct().ToList())
                            {
                                merge_content += GetServicesDynamicContent(record.ServicesInterest);
                                merge_content += Environment.NewLine;
                            }
                        }

                        emailBody = emailBody.Replace("&lt;&lt;&lt;DC_RCR_SVC&gt;&gt;&gt;", merge_content);

                        emailBody = CleanDynamicCodes(emailBody);
                    }

                    // general field merges
                    if (emailBody.Contains("{{{Recipient.Id}}}"))
                    {
                        emailBody = emailBody.Replace("{{{Recipient.Id}}}", record.ContactId);
                    }
                    if (emailBody.Contains("{{{Recipient.CaseSafeId__c}}}"))
                    {
                        emailBody = emailBody.Replace("{{{Recipient.CaseSafeId__c}}}", record.ContactId);
                    }
                    if (emailBody.Contains("{{{Recipient.CaseSafeID__c}}}"))
                    {
                        emailBody = emailBody.Replace("{{{Recipient.CaseSafeID__c}}}", record.ContactId);
                    }
                    if (emailBody.Contains("{{{Recipient.FirstName}}}"))
                    {
                        emailBody = emailBody.Replace("{{{Recipient.FirstName}}}", record.FirstName);
                    }
                    if (emailBody.Contains("{{{Recipient.lc_colleague_id__c}}}"))
                    {
                        emailBody = emailBody.Replace("{{{Recipient.lc_colleague_id__c}}}", record.StudentId);
                    }
                    if (emailBody.Contains("{{{Inquiry.Id}}}"))
                    {
                        emailBody = emailBody.Replace("{{{Inquiry.Id}}}", record.InquiryId);
                    }

                    // event specific merges
                    if (emailBody.Contains("{{{lc_event_registration__c.lc_event_name__c}}}"))
                    {
                        emailBody = emailBody.Replace("{{{lc_event_registration__c.lc_event_name__c}}}", record.EventName);
                    }
                    if (emailBody.Contains("{{{lc_event_registration__c.lc_event_date_time__c}}}"))
                    {
                        emailBody = emailBody.Replace("{{{lc_event_registration__c.lc_event_date_time__c}}}", record.EventDate);
                    }
                    if (emailBody.Contains("{{{lc_event_registration__c.lc_event_location__c}}}"))
                    {
                        emailBody = emailBody.Replace("{{{lc_event_registration__c.lc_event_location__c}}}", record.EventLocation);
                    }
                    if (emailBody.Contains("{{{Recipient.primary_program_interest}}}"))
                    {
                        emailBody = emailBody.Replace("{{{Recipient.primary_program_interest}}}", record.ProgramInterestId1);
                    }

                    // old program interest 1 merge
                    if (emailBody.Contains("&lt;&lt;&lt;DM_RCR_PROG&gt;&gt;&gt;"))
                    {
                        string merge_content = string.Empty;

                        if (!string.IsNullOrEmpty(record.ProgramInterestId1))
                        {
                            merge_content += GetPrimaryProgramName(record.ProgramInterestId1);
                        }

                        emailBody = emailBody.Replace("&lt;&lt;&lt;DM_RCR_PROG&gt;&gt;&gt;", merge_content);

                        emailBody = CleanDynamicCodes(emailBody);
                    }

                    // old program interest 2 merge
                    if (emailBody.Contains("&lt;&lt;&lt;DM_RCIR_1&gt;&gt;&gt;"))
                    {
                        string merge_content = GetDynamicContent(record.ProgramInterestId1, html);

                        if (record.ProgramInterestId1 != record.ProgramInterestId2)
                        {
                            merge_content += Environment.NewLine;
                            merge_content += GetDynamicContent(record.ProgramInterestId2, html);
                        }

                        emailBody = emailBody.Replace("&lt;&lt;&lt;DM_RCIR_1&gt;&gt;&gt;", merge_content);

                        emailBody = CleanDynamicCodes(emailBody);
                    }

                    // old services interest merge
                    if (emailBody.Contains("&lt;&lt;&lt;DM_RCIR_3&gt;&gt;&gt;"))
                    {
                        string merge_content = string.Empty;
                        if (!string.IsNullOrEmpty(record.ServicesInterest))
                        {
                            foreach (string service_interest_id in record.ServicesInterest.Split(';').Distinct().ToList())
                            {
                                merge_content += GetDynamicContent(service_interest_id, html);
                                merge_content += Environment.NewLine;
                            }
                        }

                        emailBody = emailBody.Replace("&lt;&lt;&lt;DM_RCIR_3&gt;&gt;&gt;", merge_content);

                        emailBody = CleanDynamicCodes(emailBody);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) {
                RecordError(GetCurrentMethod(), ErrorMsg);
            }

            return emailBody;
        }

        private string CleanDynamicCodes(string body) {

            try
            {
                body = body.Replace("&lt;&lt;&lt; ", "&lt;&lt;&lt;");
                body = body.Replace("&lt;&lt;&lt;&nbsp;", "&lt;&lt;&lt;");
                body = body.Replace(" &gt;&gt;&gt;", "&gt;&gt;&gt;");
                body = body.Replace("&nbsp;&gt;&gt;&gt;", "&gt;&gt;&gt;");
            }
            catch
            {
                body = string.Empty;
            }

            return body;
        }

        private string CleanEmail(string email)
        {
            string retval = string.Empty;
            try
            {
                email = email.Replace(",", ".");
                email = email.Replace("..",".");
                email = email.Replace("!", "");
                email = email.Replace("#", "");

                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email && email.Contains("."))
                {
                    retval = email;
                }
            }
            catch
            {
                retval = string.Empty;
            }

            return retval;
        }

        private bool SendScheduledBroadcastNotifications(List<crm_email_broadcasts> broadcasts)
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string MethodName = string.Empty;
            string ErrorMsg = string.Empty;
            DateTime StartTime = DateTime.Now;
            DateTime Today = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            DateTime StartDate = DateTime.Today;
            DateTime Yesterday = DateTime.Today.AddDays(-1);
            DateTime Tomorrow = DateTime.Today.AddDays(1);
            TimeSpan TimeOfDay = DateTime.Now.TimeOfDay;

            crm_email_broadcasts crm_email_broadcast = new crm_email_broadcasts();
            List<crm_email_campaigns> crm_email_campaigns = new List<crm_email_campaigns>();
            List<Guid> ValidBroadcastGuids = new List<Guid>();

            try
            {

                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool SendDeliveredBroadcastNotifications()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string MethodName = string.Empty;
            string ErrorMsg = string.Empty;
            DateTime StartTime = DateTime.Now;
            DateTime Today = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            DateTime StartDate = DateTime.Today;
            DateTime Yesterday = DateTime.Today.AddDays(-1);
            DateTime Tomorrow = DateTime.Today.AddDays(1);
            TimeSpan TimeOfDay = DateTime.Now.TimeOfDay;

            crm_email_broadcasts crm_email_broadcast = new crm_email_broadcasts();
            List<crm_email_campaigns> crm_email_campaigns = new List<crm_email_campaigns>();
            List<Guid> ValidBroadcastGuids = new List<Guid>();

            try
            {

                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                crm_email_campaigns = db_ctx.crm_email_campaigns.Where(x => (x.ec_deleted == false
                                                                    && x.ec_mark_delete == false
                                                                    && x.ec_active_flag == true
                                                                    && x.ec_send_time <= TimeOfDay
                                                                    && ((x.ec_recur == false && x.ec_start_datetime > Yesterday && x.ec_start_datetime < Tomorrow)
                                                                    || (x.ec_recur == true
                                                                        && (x.ec_start_datetime <= Today
                                                                        && (x.ec_end_datetime == null || x.ec_end_datetime >= Today)))))
                                                                    ).OrderByDescending(x => x.ec_modified_datetime).ToList();

                if (MonitorExecution(MethodName, crm_email_campaigns.Count))
                {

                    foreach (crm_email_campaigns crm_email_campaign in crm_email_campaigns)
                    {

                        crm_email_broadcast = new crm_email_broadcasts();

                        if (crm_email_campaign.ec_start_datetime.Value.Date > DateTime.Today)
                        {
                            StartDate = crm_email_campaign.ec_start_datetime ?? DateTime.Today;
                        }

                        String Days = crm_email_campaign.ec_recur_week_days ?? DateTime.Today.ToString("dddd", CurrentDateFormat);

                        List<string> DaysOfWeek = Days.Split(';').ToList<string>();

                        var day = StartDate.Date;
                        string DayOfWeek = day.ToString("dddd", CurrentDateFormat);

                        if (!string.IsNullOrEmpty(DayOfWeek) && DaysOfWeek.Contains(DayOfWeek))
                        {
                            crm_email_broadcast = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_send.Value.Year == day.Year
                                                                                    && x.email_broadcast_send.Value.Month == day.Month
                                                                                    && x.email_broadcast_send.Value.Day == day.Day
                                                                                    && x.email_broadcast_deleted == false
                                                                                    && x.email_broadcast_mark_delete == false
                                                                                    && x.email_campaign_id == crm_email_campaign.email_campaign_id)
                                                                            .OrderByDescending(x => x.email_broadcast_modfied_datetime)
                                                                            .FirstOrDefault();

                            if (crm_email_broadcast == null || crm_email_broadcast.email_broadcast_created_datetime == null)
                            {
                                DateTime TimeToSend = new DateTime(day.Year, day.Month, day.Day, crm_email_campaign.ec_send_time.Value.Hours, crm_email_campaign.ec_send_time.Value.Minutes, crm_email_campaign.ec_send_time.Value.Seconds);

                                if (crm_email_campaign.ec_report_id != null && crm_email_campaign != null)
                                {

                                    var report_name = db_ctx.crm_reports.Where(x => x.crm_report_id == crm_email_campaign.ec_report_id
                                                                                && x.crm_report_deleted == false
                                                                                && x.crm_report_mark_delete == false)
                                                                        .OrderByDescending(w => w.crm_report_modified_date)
                                                                        .Select(y => y.crm_report_name)
                                                                        .FirstOrDefault() ?? string.Empty;
                                    var template_name = db_ctx.crm_email_templates.Where(x => x.email_template_id == crm_email_campaign.ec_template_id
                                                                                        && x.email_template_deleted == false
                                                                                        && x.email_template_mark_delete == false)
                                                                        .OrderByDescending(w => w.email_template_modified_datetime)
                                                                        .Select(y => y.email_template_name)
                                                                        .FirstOrDefault() ?? string.Empty;

                                    crm_email_broadcast = new crm_email_broadcasts()
                                    {
                                        email_broadcast_guid = Guid.NewGuid(),
                                        email_broadcast_status = "Scheduled",
                                        email_broadcast_send = TimeToSend,
                                        email_campaign_id = crm_email_campaign.email_campaign_id,
                                        email_broadcast_email_campaign = crm_email_campaign.email_campaign_id,
                                        email_report_id = crm_email_campaign.ec_report_id,
                                        email_report_name = report_name,
                                        email_template_id = crm_email_campaign.ec_template_id,
                                        email_template_name = template_name,
                                        email_broadcast_modified_by = Settings.CrmAdmin,
                                        email_broadcast_modfied_datetime = DateTime.Now,
                                        email_broadcast_created_by = Settings.CrmAdmin,
                                        email_broadcast_created_datetime = DateTime.Now,
                                        email_broadcast_sys_modstamp = DateTime.Now,
                                    };

                                    db_ctx.crm_email_broadcasts.Add(crm_email_broadcast);
                                    inserted++;
                                }
                            }

                            if (crm_email_broadcast != null)
                            {
                                ValidBroadcastGuids.Add(crm_email_broadcast.email_broadcast_guid);
                            }
                            // }
                        }
                    }


                    List<crm_email_broadcasts> AbandondedBroadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
                                                                                                            && x.email_broadcast_mark_delete == false
                                                                                                            && x.email_broadcast_status == "Scheduled"
                                                                                                            && !ValidBroadcastGuids.Contains(x.email_broadcast_guid))
                                                                                                    .ToList();
                    if (AbandondedBroadcasts.Any())
                    {
                        foreach (crm_email_broadcasts broadcast in AbandondedBroadcasts)
                        {
                            broadcast.email_broadcast_mark_delete = true;
                            broadcast.email_broadcast_status = "Cancelled";
                            broadcast.email_broadcast_modfied_datetime = DateTime.Now;
                        }
                    }

                    db_ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool QueueEventRegistrationConfirmations() {

            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string MethodName = string.Empty;
            string ErrorMsg = string.Empty;
            DateTime StartTime = DateTime.Now;

            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                var event_registrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_confirmation_datetime == null
                                                                                 && x.event_registration_activity_extender_id == "a0U1U000002hZQcUAM"
                                                                                 && x.event_registration_created_datetime > DateTime.Today);

                if (MonitorExecution(MethodName, event_registrations.Count()))
                {

                    var previous_event_extender_id = string.Empty;
                    crm_events crm_event = new crm_events();
                    foreach (crm_event_registrations registration in event_registrations.OrderBy(x=>x.event_registration_activity_extender_id)) {

                        var current_event_extender_id = registration.event_registration_activity_extender_id;

                        if (current_event_extender_id != previous_event_extender_id) {
                            crm_event = db_ctx.crm_events.Where(x => x.event_activity_extender_id == current_event_extender_id)
                                                        .OrderByDescending(y => y.event_modified_datetime)
                                                        .FirstOrDefault();
                        }

                        if (crm_event.event_confirmation_send_flag ?? false) {

                            if (crm_event.event_start_datetime >= StartTime) {

                                var contact_id = registration.event_registration_contact_id;
                                var reminder_subject = "Registration Confirmation";
                                var event_from_name = crm_event.event_department ?? "Lethbridge College";
                                var event_from_address = crm_event.event_reminder_email_address ?? Settings.CrmEmailAddress;
                                var reminder_template_id = "00X1U000000ZhdoUAC";
                                var event_name = crm_event.event_description;
                                var event_datetime = crm_event.event_start_date + ", " + crm_event.event_start_datetime + " - " + crm_event.event_end_datetime;
                                var event_location = crm_event.event_location;

                                if (!string.IsNullOrEmpty(contact_id))
                                {

                                    var EmailRecipient = db_ctx.crm_contacts.Where(x => x.contact_id == contact_id
                                                                                    && x.contact_deleted == false
                                                                                    && x.contact_mark_delete == false
                                                                                    && x.contact_invalid_email_flag == false
                                                                                    && x.contact_email_opt_out == false
                                                                                    && x.contact_is_email_bounced == false)
                                                                                .Select(y => new Record()
                                                                                {
                                                                                    PreferredEmail = y.contact_email,
                                                                                    AlternativeEmail = y.contact_alternate_email,
                                                                                    CollegeEmail = y.contact_college_email,
                                                                                    FirstName = y.contact_first_name,
                                                                                    ContactId = y.contact_id,
                                                                                    StudentId = y.contact_colleague_id,
                                                                                    AccountId = y.contact_account_id
                                                                                })
                                                                                .Distinct().FirstOrDefault();

                                    var recipient_email_address = string.Empty;
                                    if (!string.IsNullOrEmpty(EmailRecipient.CollegeEmail))
                                    {
                                        recipient_email_address = EmailRecipient.CollegeEmail;
                                    }
                                    else if (!string.IsNullOrEmpty(EmailRecipient.AlternativeEmail))
                                    {
                                        recipient_email_address = EmailRecipient.AlternativeEmail;
                                    }
                                    else {
                                        recipient_email_address = Settings.CrmAdmin;
                                    }

                                    if (!string.IsNullOrEmpty(recipient_email_address))
                                    {

                                        var email_template = db_ctx.crm_email_templates.Where(x => x.email_template_deleted == false
                                                                                                && x.email_template_mark_delete == false
                                                                                                && x.email_template_id == reminder_template_id)
                                                                                        .OrderByDescending(x => x.email_template_modified_datetime)
                                                                                        .FirstOrDefault();

                                        if (email_template != null)
                                        {

                                            if (!string.IsNullOrEmpty(email_template.email_template_html_value))
                                            {

                                                bool AlreadyQueued = db_ctx.crm_email_messages.Where(x => x.email_message_broadcast_id == registration.event_registration_id
                                                                                                                            && x.email_message_contact_id == EmailRecipient.ContactId
                                                                                                                            && x.email_message_to_address == recipient_email_address).Any();
                                                if (!AlreadyQueued)
                                                {
                                                    try
                                                    {
                                                        crm_email_messages crm_email_message = new crm_email_messages()
                                                        {
                                                            email_message_guid = Guid.NewGuid(),
                                                            email_message_status = "Scheduled",
                                                            email_message_contact_id = contact_id,
                                                            email_message_account_id = EmailRecipient.AccountId,
                                                            email_message_broadcast_id = registration.event_registration_id,
                                                            email_message_template_id = email_template.email_template_id,
                                                            email_message_subject = reminder_subject,
                                                            email_message_to_address = recipient_email_address,
                                                            email_message_from_address = event_from_address,
                                                            email_message_from_name = event_from_name,
                                                            email_message_bcc_address = Settings.CrmEmailAddress,
                                                            email_message_is_incoming = false,
                                                            email_message_datetime = DateTime.Now,
                                                            email_message_created_by = Settings.CrmAdmin,
                                                            email_message_created_datetime = DateTime.Now,
                                                            email_message_modified_by = Settings.CrmAdmin,
                                                            email_message_modified_datetime = DateTime.Now,
                                                            email_message_html_body = MergeEmailBody(email_template.email_template_html_value, EmailRecipient, true),
                                                            email_message_text_body = MergeEmailBody(email_template.email_template_body, EmailRecipient, false),
                                                        };

                                                        db_ctx.crm_email_messages.Add(crm_email_message);

                                                        success = true;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ErrorMsg += ex.ToString();
                                                    }
                                                }
                                                if (success)
                                                {
                                                    registration.event_registration_confirmation_datetime = DateTime.Now;

                                                    if (db_ctx.SaveChanges() > 0)
                                                    {
                                                        success = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

    }
}

//private bool SendEmailMessages(List<crm_email_messages> emails)
//{
//    bool success = false;
//    try
//    {
//        foreach (crm_email_messages email in emails)
//        {

//            // prepare task record
//            crm_tasks crm_task = new crm_tasks()
//            {
//                activity_guid = Guid.NewGuid(),
//                task_account_id = email.email_message_account_id,
//                task_who_id = email.email_message_contact_id,
//                task_subject = email.email_message_subject,
//                task_subtype = "BroadcastEmail",
//                task_created_by = Settings.CrmAdmin,
//                task_created_datetime = DateTime.Now,
//                task_modified_by = Settings.CrmAdmin,
//                task_modified_datetime = DateTime.Now,
//            };

//            // prepare email
//            SendEmail sendEmail = new SendEmail()
//            {
//                Body = email.email_message_html_body,
//                Title = email.email_message_subject,
//                To = email.email_message_to_address,
//                From = email.email_message_from_address,
//                Bcc = email.email_message_bcc_address,
//            };

//            try
//            {
//                sendEmail.Send();

//                if (!string.IsNullOrEmpty(sendEmail.ErrorMsg))
//                {
//                    email.email_message_status = "Failed";
//                    RecordError("EmailFailed", sendEmail.ErrorMsg);
//                    email.email_message_error = sendEmail.ErrorMsg;
//                }
//                else
//                {
//                    email.email_message_status = "Successful";
//                    crm_task.task_status = "Completed";
//                    email.email_message_datetime = DateTime.Now;
//                }
//            }
//            catch (Exception ex)
//            {
//                RecordError("EmailFailed", ex.ToString());
//                email.email_message_status = "Failed";
//                email.email_message_error = ex.ToString();
//            }

//            try
//            {
//                db_ctx.crm_tasks.Add(crm_task);
//                db_ctx.crm_email_messages.Add(email);

//                db_ctx.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                RecordError("EmailFailed", ex.ToString());
//            }

//        }
//        success = true;
//    }
//    catch (Exception ex)
//    {
//        RecordError("EmailFailed", ex.ToString());
//    }
//    return success;
//}


//private bool RunOldCommunications()
//{
//    bool success = false;
//    string MethodName = string.Empty;

//    try
//    {
//        MethodName = GetCurrentMethod();

//        ScheduleEmailBroadcasts();
//        SendEmailBroadcasts();

//        success = true;
//    }
//    catch (Exception ex)
//    {
//        RecordError(MethodName, ex.ToString());
//    }

//    return success;

//}


//private bool ScheduleEmailBroadcasts()
//{
//    int inserted = 0;
//    int updated = 0;
//    int total = 0;

//    bool success = false;
//    string MethodName = string.Empty;
//    string ErrorMsg = string.Empty;
//    string report_id = string.Empty;
//    string report_name = string.Empty;
//    string template_id = string.Empty;
//    string template_name = string.Empty;
//    DateTime StartTime = DateTime.Now;
//    DateTime Today = DateTime.Today;
//    DateTime EndDate = DateTime.Today;
//    DateTime StartDate = DateTime.Today;
//    DateTime Yesterday = DateTime.Today.AddDays(-1);
//    DateTime Tomorrow = DateTime.Today.AddDays(1);
//    TimeSpan TimeOfDay = DateTime.Now.TimeOfDay;
//    DateTime? TimeToSend = DateTime.Now;

//    crm_email_broadcasts crm_email_broadcast = new crm_email_broadcasts();
//    List<crm_email_broadcasts> crm_email_broadcasts = new List<crm_email_broadcasts>();
//    List<crm_email_broadcasts> new_email_broadcasts = new List<crm_email_broadcasts>();
//    List<crm_email_broadcasts> abandoned_broadcasts = new List<crm_email_broadcasts>();
//    List<crm_email_campaigns> crm_email_campaigns = new List<crm_email_campaigns>();
//    List<Guid> valid_broadcast_guids = new List<Guid>();

//    try
//    {

//        MethodName = GetCurrentMethod();

//        DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

//        crm_email_campaigns = db_ctx.crm_email_campaigns.Where(x => (x.ec_deleted == false
//                                                            && x.ec_mark_delete == false
//                                                            && x.ec_active_flag == true
//                                                            && x.ec_send_time <= TimeOfDay
//                                                            && ((x.ec_recur == false && x.ec_start_datetime > Yesterday && x.ec_start_datetime < Tomorrow)
//                                                            || (x.ec_recur == true
//                                                                && (x.ec_start_datetime <= Today
//                                                                && (x.ec_end_datetime == null || x.ec_end_datetime >= Today)))))
//                                                            ).OrderByDescending(x => x.ec_modified_datetime).ToList();


//        foreach (crm_email_campaigns crm_email_campaign in crm_email_campaigns)
//        {

//            report_id = crm_email_campaign.ec_report_id;
//            template_id = crm_email_campaign.ec_template_id;

//            crm_email_broadcast = new crm_email_broadcasts();

//            if (crm_email_campaign.ec_start_datetime.Value.Date > DateTime.Today)
//            {
//                StartDate = crm_email_campaign.ec_start_datetime ?? DateTime.Today;
//            }

//            String Days = crm_email_campaign.ec_recur_week_days ?? DateTime.Today.ToString("dddd", CurrentDateFormat);

//            List<string> DaysOfWeek = Days.Split(';').ToList<string>();

//            var day = StartDate.Date;
//            string DayOfWeek = day.ToString("dddd", CurrentDateFormat);

//            if (!string.IsNullOrEmpty(DayOfWeek) && DaysOfWeek.Contains(DayOfWeek))
//            {
//                crm_email_broadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_send.Value.Year == day.Year
//                                                                        && x.email_broadcast_send.Value.Month == day.Month
//                                                                        && x.email_broadcast_send.Value.Day == day.Day
//                                                                        && x.email_broadcast_deleted == false
//                                                                        && x.email_broadcast_mark_delete == false
//                                                                        && x.email_campaign_id == crm_email_campaign.email_campaign_id)
//                                                                .OrderByDescending(x => x.email_broadcast_modfied_datetime).ToList();

//                if (!crm_email_broadcasts.Any())
//                {
//                    crm_email_broadcast = new crm_email_broadcasts()
//                    {
//                        email_broadcast_guid = Guid.NewGuid(),
//                        email_broadcast_status = "Scheduled",
//                        email_broadcast_created_by = Settings.CrmAdmin,
//                        email_broadcast_created_datetime = DateTime.Now,
//                    };

//                    db_ctx.crm_email_broadcasts.Add(crm_email_broadcast);

//                    inserted++;
//                }
//                else
//                {
//                    crm_email_broadcast = crm_email_broadcasts.FirstOrDefault();
//                }

//                // update scheduled broadcasts (skip completed etc)
//                if (crm_email_broadcast.email_broadcast_status == "Scheduled")
//                {

//                    crm_email_broadcast.email_broadcast_send = new DateTime(day.Year, day.Month, day.Day, crm_email_campaign.ec_send_time.Value.Hours, crm_email_campaign.ec_send_time.Value.Minutes, crm_email_campaign.ec_send_time.Value.Seconds);

//                    crm_email_broadcast.email_campaign_id = crm_email_campaign.email_campaign_id;
//                    crm_email_broadcast.email_broadcast_email_campaign = crm_email_campaign.email_campaign_id;

//                    crm_email_broadcast.email_report_id = report_id;
//                    crm_email_broadcast.email_report_name = db_ctx.crm_reports.Where(x => x.crm_report_id == crm_email_campaign.ec_report_id
//                                                                                    && x.crm_report_deleted == false
//                                                                                    && x.crm_report_mark_delete == false)
//                                                                            .OrderByDescending(w => w.crm_report_modified_date)
//                                                                            .Select(y => y.crm_report_name)
//                                                                            .FirstOrDefault() ?? string.Empty;

//                    crm_email_broadcast.email_template_id = template_id;
//                    crm_email_broadcast.email_template_name = db_ctx.crm_email_templates.Where(x => x.email_template_id == crm_email_campaign.ec_template_id
//                                                                                                        && x.email_template_deleted == false
//                                                                                                        && x.email_template_mark_delete == false)
//                                                                                        .OrderByDescending(w => w.email_template_modified_datetime)
//                                                                                        .Select(y => y.email_template_name)
//                                                                                        .FirstOrDefault() ?? string.Empty;

//                    crm_email_broadcast.email_broadcast_modified_by = Settings.CrmAdmin;
//                    crm_email_broadcast.email_broadcast_modfied_datetime = DateTime.Now;

//                    crm_email_broadcast.email_broadcast_sys_modstamp = DateTime.Now;
//                }

//                valid_broadcast_guids.Add(crm_email_broadcast.email_broadcast_guid);
//            }
//        }

//        abandoned_broadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
//                                                                    && x.email_broadcast_mark_delete == false
//                                                                    && x.email_broadcast_status == "Scheduled"
//                                                                    && !valid_broadcast_guids.Contains(x.email_broadcast_guid))
//                                                            .ToList();
//        if (abandoned_broadcasts.Any())
//        {
//            foreach (crm_email_broadcasts broadcast in abandoned_broadcasts)
//            {
//                broadcast.email_broadcast_mark_delete = true;
//                broadcast.email_broadcast_status = "Cancelled";
//                broadcast.email_broadcast_modfied_datetime = DateTime.Now;
//            }
//        }

//        db_ctx.SaveChanges();

//        success = true;
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

//    return success;
//}



//private bool SendEmailBroadcasts()
//{
//    int inserted = 0;
//    int updated = 0;
//    int total = 0;

//    bool success = false;
//    string secondaryId = string.Empty;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;
//    List<DateTime> DateList = new List<DateTime>();
//    List<string> SendToAddressTypes = new List<string>();
//    List<Record> EmailRecipients = new List<Record>();
//    crm_email_templates crm_email_template = new crm_email_templates();
//    crm_email_campaigns crm_email_campaign = new crm_email_campaigns();
//    crm_reports crm_report = new crm_reports();
//    List<crm_email_broadcasts> crm_broadcasts_to_delete = new List<crm_email_broadcasts>();
//    List<crm_email_templates> crm_email_templates = new List<crm_email_templates>();
//    List<crm_email_campaigns> crm_email_campaigns = new List<crm_email_campaigns>();

//    string clientId = WebConfigurationManager.AppSettings["SFClientId"];
//    string clientSecret = WebConfigurationManager.AppSettings["SFClientSecret"];
//    string username = WebConfigurationManager.AppSettings["SFClientUsername"];
//    string password = WebConfigurationManager.AppSettings["SFClientPassword"];
//    string analyticApiUrl = WebConfigurationManager.AppSettings["SFReportingApiUrl"];

//    try
//    {
//        MethodName = GetCurrentMethod();

//        List<crm_email_broadcasts> crm_email_broadcasts = db_ctx.crm_email_broadcasts.Where(x => (x.email_broadcast_status == "Scheduled"
//                                                                                        || x.email_broadcast_status == "Created")
//                                                                                        && x.email_broadcast_send <= DateTime.Now
//                                                                                        && x.email_broadcast_deleted == false
//                                                                                        && x.email_broadcast_mark_delete == false)
//                                                                                .OrderByDescending(x => x.email_broadcast_modfied_datetime).ToList();
//        if (MonitorExecution(MethodName, crm_email_broadcasts.Count))
//        {
//            foreach (crm_email_broadcasts broadcast in crm_email_broadcasts)
//            {
//                SendToAddressTypes = new List<string>();
//                crm_email_template = new crm_email_templates();
//                EmailRecipients = new List<Record>();
//                int MessagesSentCnt = 0;

//                try
//                {
//                    crm_email_campaigns = db_ctx.crm_email_campaigns.Where(x => x.ec_deleted == false
//                                                                          && x.ec_mark_delete == false
//                                                                          && x.ec_active_flag == true
//                                                                          && x.email_campaign_id == broadcast.email_campaign_id).ToList();

//                    if (crm_email_campaigns.Any())
//                    {
//                        crm_email_campaign = crm_email_campaigns.OrderByDescending(x => x.ec_modified_datetime).FirstOrDefault();

//                        if (crm_email_campaign != null)
//                        {
//                            crm_email_templates = db_ctx.crm_email_templates.Where(x => x.email_template_deleted == false
//                                                                                         && x.email_template_id == crm_email_campaign.ec_template_id)
//                                                                                .ToList();

//                            if (crm_email_templates.Any())
//                            {
//                                crm_email_template = crm_email_templates.OrderByDescending(x => x.email_template_modified_datetime).FirstOrDefault();

//                                if (crm_email_template != null)
//                                {

//                                    var reports = db_ctx.crm_reports.Where(x => x.crm_report_deleted == false
//                                                                             && x.crm_report_mark_delete == false
//                                                                             && x.crm_report_id == crm_email_campaign.ec_report_id);
//                                    if (reports.Any())
//                                    {
//                                        crm_report = reports.OrderByDescending(x => x.crm_report_modified_date).FirstOrDefault();

//                                        if (crm_report != null)
//                                        {
//                                            var client = new SalesforceClient();
//                                            var authFlow = new Security.UsernamePasswordAuthenticationFlow(clientId, clientSecret, username, password);

//                                            client.Authenticate(authFlow);

//                                            var reportRecords = client.GetMergeObjectIdsByReportId<List<object>>(analyticApiUrl, crm_report.crm_report_id);

//                                            if (reportRecords.Any())
//                                            {
//                                                foreach (JObject record in reportRecords)
//                                                {
//                                                    Record EmailRecipient = new Record();

//                                                    var contactId = record["dataCells"][0]["value"];

//                                                    if (contactId != null)
//                                                    {

//                                                        string contact_id = contactId.ToString();

//                                                        if (!string.IsNullOrEmpty(contact_id))
//                                                        {

//                                                            if (!EmailRecipients.Any(x => x.ContactId == contact_id))
//                                                            {
//                                                                if (GetSfRecordTypeById(contact_id) == SfRecordTypes.Contact)
//                                                                {
//                                                                    EmailRecipient = db_ctx.crm_contacts.Where(x => x.contact_id == contact_id
//                                                                                                          && x.contact_deleted == false
//                                                                                                          && x.contact_mark_delete == false
//                                                                                                          && x.contact_invalid_email_flag == false
//                                                                                                          && x.contact_email_opt_out == false
//                                                                                                          && x.contact_is_email_bounced == false)
//                                                                                                        .Select(y => new Record()
//                                                                                                        {
//                                                                                                            PreferredEmail = y.contact_email,
//                                                                                                            AlternativeEmail = y.contact_alternate_email,
//                                                                                                            CollegeEmail = y.contact_college_email,
//                                                                                                            FirstName = y.contact_first_name,
//                                                                                                            ContactId = y.contact_id,
//                                                                                                            StudentId = y.contact_colleague_id,
//                                                                                                            AccountId = y.contact_account_id
//                                                                                                        })
//                                                                                                        .Distinct().FirstOrDefault();
//                                                                }

//                                                                if (EmailRecipient != null)
//                                                                {
//                                                                    if (record["dataCells"].Count() > 1)
//                                                                    {
//                                                                        var InquiryId = record["dataCells"][1]["value"];

//                                                                        if (InquiryId != null)
//                                                                        {
//                                                                            string inquiry_id = InquiryId.ToString();

//                                                                            if (!string.IsNullOrEmpty(inquiry_id))
//                                                                            {
//                                                                                if (GetSfRecordTypeById(inquiry_id) == SfRecordTypes.Inquiry)
//                                                                                {

//                                                                                    crm_inquiries inquiry = db_ctx.crm_inquiries.Where(x => x.inquiry_mark_delete == false
//                                                                                                                                                && x.inq_deleted == false
//                                                                                                                                                && x.inquiry_id == inquiry_id)
//                                                                                                                                        .OrderByDescending(y => y.inq_modified_datetime)
//                                                                                                                                        .FirstOrDefault();
//                                                                                    if (inquiry != null)
//                                                                                    {
//                                                                                        secondaryId = inquiry.inquiry_id;
//                                                                                        EmailRecipient.InquiryId = inquiry.inquiry_id;
//                                                                                        EmailRecipient.NoCommunication = inquiry.inq_no_communications;
//                                                                                        if (inquiry.inq_pri_prog_interest != null)
//                                                                                        {
//                                                                                            EmailRecipient.ProgramInterestId1 = inquiry.inq_pri_prog_interest;
//                                                                                        }
//                                                                                        if (inquiry.inq_sec_prog_interest != null)
//                                                                                        {
//                                                                                            EmailRecipient.ProgramInterestId2 = inquiry.inq_sec_prog_interest;
//                                                                                        }
//                                                                                        if (inquiry.inq_services_interest != null)
//                                                                                        {
//                                                                                            EmailRecipient.ServicesInterest = inquiry.inq_services_interest;
//                                                                                        }
//                                                                                    }
//                                                                                }
//                                                                            }
//                                                                        }
//                                                                    }

//                                                                    EmailRecipients.Add(EmailRecipient);
//                                                                }
//                                                            }
//                                                        }
//                                                    }
//                                                }

//                                                broadcast.email_template_name = crm_email_template.email_template_name;

//                                                if (crm_email_campaign.ec_email_address_type == PickListValues.BothEmailType)
//                                                {
//                                                    SendToAddressTypes.Add(PickListValues.CollegeEmailType);
//                                                    SendToAddressTypes.Add(PickListValues.AlternateEmailType);
//                                                }
//                                                else
//                                                {
//                                                    SendToAddressTypes.Add(crm_email_campaign.ec_email_address_type);
//                                                }

//                                                foreach (string SendToAddressType in SendToAddressTypes)
//                                                {
//                                                    List<crm_email_messages> messages_to_send = new List<crm_email_messages>();

//                                                    if (MonitorExecution(crm_email_campaign.ec_name, EmailRecipients.Count()))
//                                                    {
//                                                        foreach (Record EmailRecipient in EmailRecipients)
//                                                        {
//                                                            if (EmailRecipient != null)
//                                                            {
//                                                                crm_email_messages crm_email_message = new crm_email_messages();

//                                                                if (!string.IsNullOrEmpty(secondaryId))
//                                                                {
//                                                                    crm_email_message.email_message_secondary_id = secondaryId;
//                                                                }

//                                                                switch (SendToAddressType)
//                                                                {
//                                                                    case "Preferred":
//                                                                        crm_email_message.email_message_to_address = EmailRecipient.PreferredEmail;
//                                                                        break;
//                                                                    case "Alternate":
//                                                                        crm_email_message.email_message_to_address = EmailRecipient.AlternativeEmail;
//                                                                        break;
//                                                                    case "College":
//                                                                        crm_email_message.email_message_to_address = EmailRecipient.CollegeEmail;
//                                                                        break;
//                                                                    default:
//                                                                        break;
//                                                                }

//                                                                bool previouslySent = db_ctx.crm_email_messages.Where(x => x.email_message_campaign_id == crm_email_campaign.email_campaign_id
//                                                                                                                            && x.email_message_broadcast_guid == broadcast.email_broadcast_guid
//                                                                                                                            && x.email_message_contact_id == EmailRecipient.ContactId
//                                                                                                                            && x.email_message_to_address == crm_email_message.email_message_to_address).Any();
//                                                                if (!previouslySent)
//                                                                {

//                                                                    crm_email_message.email_message_guid = Guid.NewGuid();
//                                                                    crm_email_message.email_message_contact_id = EmailRecipient.ContactId;
//                                                                    crm_email_message.email_message_account_id = EmailRecipient.AccountId;
//                                                                    crm_email_message.email_message_campaign_id = crm_email_campaign.email_campaign_id;
//                                                                    crm_email_message.email_message_broadcast_id = broadcast.email_broadcast_id;
//                                                                    crm_email_message.email_message_report_id = crm_report.crm_report_id;
//                                                                    crm_email_message.email_message_template_id = crm_email_template.email_template_id;
//                                                                    crm_email_message.email_message_subject = crm_email_template.email_template_subject;
//                                                                    crm_email_message.email_message_from_address = crm_email_campaign.ec_from_email_address;
//                                                                    crm_email_message.email_message_from_name = crm_email_campaign.ec_from_email_address_title;
//                                                                    crm_email_message.email_message_bcc_address = Settings.CrmEmailAddress;
//                                                                    crm_email_message.email_message_is_incoming = false;
//                                                                    crm_email_message.email_message_datetime = DateTime.Now;
//                                                                    crm_email_message.email_message_created_by = Settings.CrmAdmin;
//                                                                    crm_email_message.email_message_created_datetime = DateTime.Now;
//                                                                    crm_email_message.email_message_modified_by = Settings.CrmAdmin;
//                                                                    crm_email_message.email_message_modified_datetime = DateTime.Now;
//                                                                    crm_email_message.email_message_html_body = MergeEmailBody(crm_email_template.email_template_html_value, EmailRecipient, true);
//                                                                    crm_email_message.email_message_text_body = MergeEmailBody(crm_email_template.email_template_body, EmailRecipient, false);


//                                                                    if ((SendToAddressType == "Alternate"
//                                                                            && (!string.IsNullOrEmpty(crm_email_message.email_message_to_address)
//                                                                                && !crm_email_message.email_message_to_address.Contains("lethbridge")))
//                                                                        || (SendToAddressType == "College"
//                                                                            && (!string.IsNullOrEmpty(crm_email_message.email_message_to_address)
//                                                                                && crm_email_message.email_message_to_address.Contains("lethbridge")))
//                                                                        || (SendToAddressType == "Preferred"))
//                                                                    {
//                                                                        crm_tasks crm_task = new crm_tasks()
//                                                                        {
//                                                                            activity_guid = Guid.NewGuid(),
//                                                                            task_account_id = crm_email_message.email_message_account_id,
//                                                                            task_who_id = crm_email_message.email_message_contact_id,
//                                                                            task_assigned_to_id = crm_email_message.email_message_contact_id,
//                                                                            task_what_id = broadcast.email_broadcast_id,
//                                                                            task_subject = crm_email_message.email_message_subject,
//                                                                            task_name_id = crm_email_message.email_message_id,
//                                                                            task_related_to_id = broadcast.email_broadcast_id,
//                                                                            task_subtype = "BroadcastEmail",
//                                                                            task_priority = "Normal",
//                                                                            task_created_by = Settings.CrmAdmin,
//                                                                            task_created_datetime = DateTime.Now,
//                                                                            task_modified_by = Settings.CrmAdmin,
//                                                                            task_modified_datetime = DateTime.Now,
//                                                                        };

//                                                                        SendEmail sendEmail = new SendEmail()
//                                                                        {
//                                                                            Body = crm_email_message.email_message_html_body,
//                                                                            Title = crm_email_message.email_message_subject,
//                                                                            To = crm_email_message.email_message_to_address,
//                                                                            From = crm_email_message.email_message_from_address,
//                                                                        };

//                                                                        try
//                                                                        {
//                                                                            crm_task.task_description = sendEmail.Body;
//                                                                            if (sendEmail.Send() && string.IsNullOrEmpty(sendEmail.ErrorMsg))
//                                                                            {
//                                                                                MessagesSentCnt++;
//                                                                                crm_task.task_status = "Completed";
//                                                                                crm_email_message.email_message_status = "Successful";
//                                                                                crm_email_message.email_message_datetime = DateTime.Now;
//                                                                                crm_email_message.email_message_sent_datetime = DateTime.Now;
//                                                                            }
//                                                                            else
//                                                                            {
//                                                                                crm_task.task_status = "Failed";
//                                                                                crm_email_message.email_message_status = "Failed";
//                                                                                crm_email_message.email_message_error = sendEmail.ErrorMsg;
//                                                                                ErrorMsg += sendEmail.ErrorMsg;
//                                                                            }
//                                                                        }
//                                                                        catch (Exception ex)
//                                                                        {
//                                                                            crm_email_message.email_message_status = "Failed";
//                                                                            crm_email_message.email_message_error = ex.ToString();
//                                                                            ErrorMsg += ex.ToString();
//                                                                        }

//                                                                        try
//                                                                        {
//                                                                            db_ctx.crm_tasks.Add(crm_task);
//                                                                            db_ctx.crm_email_messages.Add(crm_email_message);
//                                                                        }
//                                                                        catch (Exception ex)
//                                                                        {
//                                                                            ErrorMsg += ex.ToString();
//                                                                        }
//                                                                    }
//                                                                }
//                                                            }
//                                                        }
//                                                    }
//                                                }

//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }

//                    if (!EmailRecipients.Any() || MessagesSentCnt > 0)
//                    {
//                        broadcast.email_broadcast_messages_sent = MessagesSentCnt;
//                        broadcast.email_broadcast_sent = DateTime.Now;
//                        broadcast.email_broadcast_status = "Completed";
//                    }

//                    //if (db_ctx.SaveChanges() > 0) {
//                    success = true;
//                    //}
//                }
//                catch (Exception ex)
//                {
//                    ErrorMsg = ex.ToString();
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

//    return success;
//}
