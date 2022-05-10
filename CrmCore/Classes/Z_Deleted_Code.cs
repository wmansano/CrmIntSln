using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lc.crm.Logic
{
    class Z_Deleted_Code
    {

        //DownloadContacts(false);
        // UploadContacts();
        // foreach (string dep in GetDepartments()) {
        //    foreach (KeyValuePair<string, string> dic in GetEvents(dep)) {
        //        string eventName = dic.Key + dic.Value;
        //    }

        //}

        // EventRegistrationObj obj = new EventRegistrationObj()
        // {
        //     //ContactFirstName = "Devin",
        //     //ContactLastName = "Coslovi",
        //     ContactIdCardBarcode = "0055804",
        //     //ContactBirthDate = Convert.ToDateTime("2019-07-15"),
        //     EventId = "00U1U000007Z7bDUAS",
        //     EventAttended = true,
        // };

        // SaveEventRegistration(obj);

        // Dictionary<string, EventStatus> events = new Dictionary<string, EventStatus>();
        // events.Add("00U1U000007ZtjeUAC", EventStatus.Checkedin); // NSO:: "00U1U000007ZtjeUAC"
        // events.Add("00U1U000007ZttaUAC", EventStatus.Checkedin); // LIFE 1: "00U1U000007ZttaUAC"
        // events.Add("00U1U000007ZttfUAC", EventStatus.Checkedin); // LIFE 2: "00U1U000007ZttfUAC"
        // events.Add("00U1U000007ZttLUAS", EventStatus.Checkedin); // TECH 1: "00U1U000007ZttLUAS"
        // events.Add("00U1U000007ZttGUAS", EventStatus.Checkedin); // TECH 2: "00U1U000007ZttGUAS"
        // events.Add("00U1U000007ZttVUAS", EventStatus.Checkedin); // WELL 2: "00U1U000007ZttVUAS"
        // events.Add("00U1U000007ZttHUAS", EventStatus.Checkedin); // WELL 2: "00U1U000007ZttHUAS"
        // events.Add("00U1U000009ctwHUAQ", EventStatus.Checkedin); // ISE : "00U1U000009ctwHUAQ"
        // foreach (KeyValuePair<string,EventStatus> keyValuePair in events) {
        //     ProcessEventRegistrations(keyValuePair.Key, keyValuePair.Value);
        // }

        // UploadActivityExtenders();
        // UploadEventRegistrations();
        // DownloadEventRegistrations();


        //public bool SetRCIR1Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-1);
        //    DateTime maxDelayDateTime = DateTime.Today.AddDays(-5);
        //    string dcp1 = string.Empty;
        //    string dcp2 = string.Empty;
        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        var previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject == "List Email: Programs at Lethbridge College"
        //                                                                && x.task_subtype == "ListEmail"
        //                                                                && x.task_status == "Completed")
        //                                                           || (x.task_what_id == "a0N1U000003za3SUAQ"
        //                                                                && x.task_subtype == "BroadcastEmail"
        //                                                                && x.task_status == "Completed")).Select(y => y.task_who_id).Distinct().ToList<string>();

        //        var send_to_inquiries = db_ctx.crm_inquiries.Where(x => (x.inq_pri_prog_interest != null || x.inq_sec_prog_interest != null)
        //                                                            && x.inquiry_datetime > maxDelayDateTime
        //                                                            && x.inquiry_datetime <= delayDate
        //                                                            && x.inq_deleted == false
        //                                                            && !Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                            && !Settings.EslProgIds.Contains(x.inq_sec_prog_interest)
        //                                                            && x.inq_contact_id != null
        //                                                            && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                var possible_matches = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                    && x.contact_deleted == false
        //                                                                    && x.contact_colleague_id != "0525280"
        //                                                                    && x.contact_is_email_bounced == false
        //                                                                    && x.contact_invalid_email_flag == false).ToList();

        //                if (possible_matches.Any())
        //                {
        //                    var contact = possible_matches.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        dcp1 = db_ctx.dynamic_content.Where(x => x.dynamic_content_matching_id == i.inq_pri_prog_interest).Select(y => y.dynamic_content_html).FirstOrDefault();

        //                        if (i.inq_pri_prog_interest != i.inq_sec_prog_interest)
        //                        {
        //                            dcp2 = db_ctx.dynamic_content.Where(x => x.dynamic_content_matching_id == i.inq_sec_prog_interest).Select(y => y.dynamic_content_html).FirstOrDefault();
        //                        }

        //                        if (!string.IsNullOrEmpty(dcp1))
        //                        {
        //                            contact.contact_prog_interest_flag = true;
        //                            contact.contact_prog_interest_content = dcp1;
        //                            contact.contact_prog_interest_content += Environment.NewLine;
        //                        }

        //                        if (!string.IsNullOrEmpty(dcp2))
        //                        {
        //                            contact.contact_prog_interest_content += dcp2;
        //                            contact.contact_prog_interest_content += Environment.NewLine;
        //                        }

        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                    }
        //                }
        //            }
        //        }

        //        var contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_prog_interest_flag == true
        //                                                            && !recipients.Contains(x.contact_id)).ToList();
        //        if (contacts_to_clear.Any())
        //        {
        //            foreach (crm_contacts c in contacts_to_clear)
        //            {
        //                c.contact_prog_interest_flag = false;
        //                c.contact_prog_interest_content = " ";

        //                c.contact_last_modified_by = Settings.CrmAdmin;
        //                c.contact_last_modified_datetime = DateTime.Now;
        //            }
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        ///// <summary>
        ///// Programs
        ///// </summary>
        ///// <returns></returns>
        //public bool SetRCIR2Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-10);
        //    DateTime old_sys_LastRunDatetime = DateTime.ParseExact("2019-04-14", "yyyy-MM-dd", CultureInfo.InvariantCulture);

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        var previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject.Contains("Visit Lethbridge College")
        //                                                                    && x.task_subtype == "ListEmail"
        //                                                                    && x.task_status == "Completed")
        //                                                             || (x.task_what_id == "a0N1U000003Yq2OUAS"
        //                                                                    && x.task_subtype == "BroadcastEmail"
        //                                                                    && x.task_status == "Completed")).Select(y => y.task_who_id).Distinct().ToList<string>();

        //        var send_to_inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_datetime >= old_sys_LastRunDatetime
        //                                                                && x.inquiry_datetime <= delayDate
        //                                                                && x.inq_deleted == false
        //                                                                && x.inq_contact_id != null
        //                                                            && !Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                            && !Settings.EslProgIds.Contains(x.inq_sec_prog_interest)
        //                                                                && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {

        //                var possible_matches = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                && x.contact_deleted == false
        //                                                                && x.contact_colleague_id != "0525280"
        //                                                                && x.contact_is_email_bounced == false
        //                                                                && x.contact_invalid_email_flag == false).ToList();
        //                if (possible_matches.Any())
        //                {

        //                    var contact = possible_matches.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        contact.contact_visit_campus_flag = true;
        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                    }
        //                }
        //            }
        //        }

        //        var contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_visit_campus_flag == true
        //                                                           && !recipients.Contains(x.contact_id)).ToList();

        //        if (contacts_to_clear.Any())
        //        {
        //            foreach (crm_contacts c in contacts_to_clear)
        //            {
        //                c.contact_visit_campus_flag = false;
        //                c.contact_last_modified_by = Settings.CrmAdmin;
        //                c.contact_last_modified_datetime = DateTime.Now;
        //            }
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        ///// <summary>
        ///// Services
        ///// </summary>
        ///// <returns></returns>
        //public bool SetRCIR3Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    string services = string.Empty;
        //    DateTime delayDate = DateTime.Today.AddDays(-20);
        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        var previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject == "List Email: Services for Success"
        //                                                                    && x.task_subtype == "ListEmail"
        //                                                                    && x.task_status == "Completed")
        //                                                             || (x.task_what_id == "a0N1U000003Yq2dUAC"
        //                                                                    && x.task_subtype == "BroadcastEmail"
        //                                                                    && x.task_status == "Completed")).Select(y => y.task_who_id).ToList<string>();

        //        var send_to_inquiries = db_ctx.crm_inquiries.Where(x => x.inq_services_interest != null
        //                                                            && x.inquiry_datetime > Settings.MigrationDateTime
        //                                                            && x.inquiry_datetime <= delayDate
        //                                                            && x.inq_deleted == false
        //                                                            && x.inq_contact_id != null
        //                                                            && !Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                            && !Settings.EslProgIds.Contains(x.inq_sec_prog_interest)
        //                                                            && !previously_sent.Contains(x.inq_contact_id)
        //                                                            ).ToList();



        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                var possible_matches = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                    && x.contact_deleted == false
        //                                                                    && x.contact_colleague_id != "0525280"
        //                                                                    && x.contact_is_email_bounced == false
        //                                                                    && x.contact_invalid_email_flag == false
        //                                                                    ).ToList();
        //                if (possible_matches.Any())
        //                {

        //                    var contact = possible_matches.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);
        //                        contact.contact_srvs_interest_content = " ";

        //                        if (!string.IsNullOrEmpty(i.inq_services_interest))
        //                        {
        //                            contact.contact_srvs_interest_flag = true;

        //                            foreach (string o in i.inq_services_interest.Split(';').Distinct().ToList())
        //                            {
        //                                string svc = db_ctx.dynamic_content.Where(x => o.Trim().ToLower().Contains(x.dynamic_content_matching_id.ToLower()))
        //                                                                    .Select(y => y.dynamic_content_html).FirstOrDefault();
        //                                if (!string.IsNullOrEmpty(svc))
        //                                {
        //                                    contact.contact_srvs_interest_content += svc;
        //                                    contact.contact_srvs_interest_content += Environment.NewLine;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            contact.contact_srvs_interest_flag = false;
        //                        }

        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;

        //                        db_ctx.SaveChanges();
        //                    }
        //                }
        //            }
        //        }


        //        var contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_srvs_interest_flag == true
        //                                                            && !recipients.Contains(x.contact_id)).ToList();
        //        if (contacts_to_clear.Any())
        //        {
        //            foreach (crm_contacts c in contacts_to_clear)
        //            {
        //                c.contact_srvs_interest_flag = false;
        //                c.contact_srvs_interest_content = " ";

        //                c.contact_last_modified_by = Settings.CrmAdmin;
        //                c.contact_last_modified_datetime = DateTime.Now;
        //            }
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        ///// <summary>
        ///// Programs
        ///// </summary>
        ///// <returns></returns>
        //public bool SetRCIR4Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-30);

        //    // system migration date - never remove
        //    DateTime old_sys_LastRunDatetime = DateTime.ParseExact("2019-03-25", "yyyy-MM-dd", CultureInfo.InvariantCulture);

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        var previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject.Contains("List Email: Ready to Apply")
        //                                                                    && x.task_subtype == "ListEmail"
        //                                                                    && x.task_status == "Completed")
        //                                                                || (x.task_what_id == "a0N1U000003Yq2iUAC"
        //                                                                    && x.task_subtype == "BroadcastEmail"
        //                                                                    && x.task_status == "Completed")).Select(y => y.task_who_id).ToList<string>();

        //        var send_to_inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_datetime > old_sys_LastRunDatetime
        //                                                                                && x.inquiry_datetime <= delayDate
        //                                                                                && x.inq_deleted == false
        //                                                                                && x.inq_contact_id != null
        //                                                            && !Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                            && !Settings.EslProgIds.Contains(x.inq_sec_prog_interest)
        //                                                                                && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                var possible_matches = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                        && x.contact_deleted == false
        //                                                                        && x.contact_colleague_id != "0525280"
        //                                                                        && x.contact_is_email_bounced == false
        //                                                                        && x.contact_invalid_email_flag == false
        //                                                                        ).ToList();
        //                if (possible_matches.Any())
        //                {
        //                    crm_contacts contact = possible_matches.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        contact.contact_how_to_apply_flag = true;
        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                    }
        //                }
        //            }
        //        }

        //        var contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_how_to_apply_flag == true
        //                                                    && !recipients.Contains(x.contact_id)).ToList();
        //        if (contacts_to_clear.Any())
        //        {
        //            foreach (crm_contacts contact in contacts_to_clear)
        //            {
        //                contact.contact_how_to_apply_flag = false;
        //                contact.contact_last_modified_by = Settings.CrmAdmin;
        //                contact.contact_last_modified_datetime = DateTime.Now;
        //            }
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        //public bool SetRCIRESL1Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-1);
        //    DateTime LastRunDatetime = DateTime.ParseExact("2019-04-24 16:29", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        var previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject == "List Email: Learn English at Lethbridge College"
        //                                                            && x.task_subtype == "ListEmail"
        //                                                            && x.task_status == "Completed")
        //                                                        || (x.task_what_id == "a0N1U000003YxM2UAK"
        //                                                            && x.task_subtype == "BroadcastEmail"
        //                                                            && x.task_status == "Completed")).Select(y => y.task_who_id).Distinct().ToList<string>();

        //        var send_to_inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_datetime >= LastRunDatetime
        //                                                                && x.inquiry_datetime <= delayDate
        //                                                                && x.inq_deleted == false
        //                                                                && x.inq_contact_id != null
        //                                                                && (Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                                    || Settings.EslProgIds.Contains(x.inq_sec_prog_interest))
        //                                                                && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                var possible_matches = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                        && x.contact_deleted == false
        //                                                                        && x.contact_colleague_id != "0525280"
        //                                                                        && x.contact_is_email_bounced == false
        //                                                                        && x.contact_invalid_email_flag == false).ToList();
        //                if (possible_matches.Any())
        //                {

        //                    var contact = possible_matches.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        contact.contact_rciresl1_flag = true;
        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                    }
        //                }
        //            }
        //        }


        //        var contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_rciresl1_flag == true
        //                                                             && !recipients.Contains(x.contact_id)).ToList();

        //        if (contacts_to_clear.Any())
        //        {
        //            foreach (crm_contacts c in contacts_to_clear)
        //            {
        //                c.contact_rciresl1_flag = false;
        //                c.contact_last_modified_by = Settings.CrmAdmin;
        //                c.contact_last_modified_datetime = DateTime.Now;
        //            }
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        //public bool SetRCIRESL2Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-10);
        //    DateTime LastRunDatetime = DateTime.ParseExact("2019-04-25 09:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        List<string> previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject == "List Email: Our Learning Environment"
        //                                                                    && x.task_subtype == "ListEmail"
        //                                                                    && x.task_status == "Completed")
        //                                                                || (x.task_what_id == "a0N1U000003YwpBUAS"
        //                                                                    && x.task_subtype == "BroadcastEmail"
        //                                                                    && x.task_status == "Completed")).Select(y => y.task_who_id).Distinct().ToList();

        //        List<crm_inquiries> send_to_inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_datetime >= LastRunDatetime
        //                                                                                && x.inquiry_datetime <= delayDate
        //                                                                                && x.inq_deleted == false
        //                                                                                && x.inq_contact_id != null
        //                                                                                && (Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                                                    || Settings.EslProgIds.Contains(x.inq_sec_prog_interest))
        //                                                                                && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        List<crm_contacts> send_to_contacts = new List<crm_contacts>();

        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                send_to_contacts = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                            && x.contact_deleted == false
        //                                                                            && x.contact_colleague_id != "0525280"
        //                                                                            && x.contact_is_email_bounced == false
        //                                                                            && x.contact_invalid_email_flag == false).ToList();

        //                if (send_to_contacts.Any())
        //                {
        //                    crm_contacts contact = send_to_contacts.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        contact.contact_rciresl2_flag = true;
        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                    }
        //                }
        //            }
        //        }

        //        List<crm_contacts> contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_rciresl2_flag == true
        //                                                                                     && !recipients.Contains(x.contact_id)).ToList();

        //        foreach (crm_contacts c in contacts_to_clear)
        //        {
        //            c.contact_rciresl2_flag = false;
        //            c.contact_last_modified_by = Settings.CrmAdmin;
        //            c.contact_last_modified_datetime = DateTime.Now;
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        //public bool SetRCIRESL3Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-20);
        //    DateTime LastRunDatetime = DateTime.ParseExact("2019-04-04 09:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        List<string> previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject == "List Email: Living in Lethbridge"
        //                                                                        && x.task_subtype == "ListEmail"
        //                                                                        && x.task_status == "Completed")
        //                                                                   || (x.task_what_id == "a0N1U000003YxHwUAK"
        //                                                                        && x.task_subtype == "BroadcastEmail"
        //                                                                        && x.task_status == "Completed")).Select(y => y.task_who_id).Distinct().ToList();

        //        List<crm_inquiries> send_to_inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_datetime >= LastRunDatetime
        //                                                                                && x.inquiry_datetime <= delayDate
        //                                                                                && x.inq_deleted == false
        //                                                                                && x.inq_contact_id != null
        //                                                                                && (Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                                                    || Settings.EslProgIds.Contains(x.inq_sec_prog_interest))
        //                                                                                && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        List<crm_contacts> send_to_contacts = new List<crm_contacts>();

        //        if (send_to_contacts.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                send_to_contacts = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                           && x.contact_deleted == false
        //                                                                           && x.contact_colleague_id != "0525280"
        //                                                                           && x.contact_is_email_bounced == false
        //                                                                           && x.contact_invalid_email_flag == false).ToList();
        //                if (send_to_contacts.Any())
        //                {
        //                    crm_contacts contact = send_to_contacts.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        contact.contact_rciresl3_flag = true;
        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                    }
        //                }
        //            }
        //        }

        //        List<crm_contacts> contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_rciresl3_flag == true
        //                                                                                    && !recipients.Contains(x.contact_id)).ToList();

        //        foreach (crm_contacts c in contacts_to_clear)
        //        {
        //            c.contact_rciresl3_flag = false;
        //            c.contact_last_modified_by = Settings.CrmAdmin;
        //            c.contact_last_modified_datetime = DateTime.Now;
        //        }
        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        //public bool SetRCIRESL4Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-30);
        //    DateTime LastRunDatetime = DateTime.ParseExact("2019-04-04 09:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        List<string> previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject == "Ready to Apply"
        //                                                                        && x.task_subtype == "ListEmail"
        //                                                                        && x.task_status == "Completed")
        //                                                                    || (x.task_what_id == "a0N1U000003YxM5UAK"
        //                                                                        && x.task_subtype == "BroadcastEmail"
        //                                                                        && x.task_status == "Completed")).Select(y => y.task_who_id).Distinct().ToList();

        //        List<crm_inquiries> send_to_inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_datetime >= LastRunDatetime
        //                                                                                && x.inquiry_datetime <= delayDate
        //                                                                                && x.inq_deleted == false
        //                                                                                && x.inq_contact_id != null
        //                                                                                && (Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                                                    || Settings.EslProgIds.Contains(x.inq_sec_prog_interest))
        //                                                                                && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        List<crm_contacts> send_to_contacts = new List<crm_contacts>();

        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                send_to_contacts = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                   && x.contact_deleted == false
        //                                                                   && x.contact_colleague_id != "0525280"
        //                                                                   && x.contact_is_email_bounced == false
        //                                                                   && x.contact_invalid_email_flag == false).ToList();
        //                if (send_to_contacts.Any())
        //                {
        //                    crm_contacts contact = send_to_contacts.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        contact.contact_rciresl4_flag = true;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                    }
        //                }
        //            }
        //        }

        //        List<crm_contacts> contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_rciresl4_flag == true
        //                                                                                    && !recipients.Contains(x.contact_id)).ToList();

        //        foreach (crm_contacts c in contacts_to_clear)
        //        {
        //            c.contact_rciresl4_flag = false;
        //            c.contact_last_modified_by = Settings.CrmAdmin;
        //            c.contact_last_modified_datetime = DateTime.Now;
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        //public bool SetRCIRESL5Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-40);
        //    DateTime LastRunDatetime = DateTime.ParseExact("2019-04-04 09:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        List<string> previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject == "List Email: Applying to Lethbridge College"
        //                                                                        && x.task_subtype == "ListEmail"
        //                                                                        && x.task_status == "Completed")
        //                                                                   || (x.task_what_id == "a0N1U000003YwpCUAS"
        //                                                                        && x.task_subtype == "BroadcastEmail"
        //                                                                        && x.task_status == "Completed")).Select(y => y.task_who_id).Distinct().ToList();

        //        List<crm_inquiries> send_to_inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_datetime >= LastRunDatetime
        //                                                                                && x.inquiry_datetime <= delayDate
        //                                                                                && x.inq_deleted == false
        //                                                                                && x.inq_contact_id != null
        //                                                                                && (Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                                                    || Settings.EslProgIds.Contains(x.inq_sec_prog_interest))
        //                                                                                && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        List<crm_contacts> send_to_contacts = new List<crm_contacts>();

        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                send_to_contacts = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                           && x.contact_deleted == false
        //                                                                           && x.contact_colleague_id != "0525280"
        //                                                                           && x.contact_is_email_bounced == false
        //                                                                           && x.contact_invalid_email_flag == false).ToList();
        //                if (send_to_contacts.Any())
        //                {
        //                    crm_contacts contact = send_to_contacts.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        contact.contact_rciresl5_flag = true;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                    }
        //                }
        //            }
        //        }

        //        List<crm_contacts> contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_rciresl5_flag == true
        //                                                                                    && !recipients.Contains(x.contact_id)).ToList();

        //        foreach (crm_contacts c in contacts_to_clear)
        //        {
        //            c.contact_rciresl5_flag = false;
        //            c.contact_last_modified_by = Settings.CrmAdmin;
        //            c.contact_last_modified_datetime = DateTime.Now;
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        //public bool SetRCIRESL6Flags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-3);
        //    DateTime maxDelayDateTime = DateTime.Today.AddDays(-8);
        //    DateTime lastRunDatetime = DateTime.ParseExact("2019-04-04 09:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        //    string dcp1 = string.Empty;
        //    string dcp2 = string.Empty;

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        List<string> previously_sent = db_ctx.crm_tasks.Where(x => (x.task_subject == "List Email: Additional Programs at Lethbridge College"
        //                                                                        && x.task_subtype == "ListEmail"
        //                                                                        && x.task_status == "Completed")
        //                                                                    || (x.task_what_id == "a0N1U000003YxWIUA0"
        //                                                                        && x.task_subtype == "BroadcastEmail"
        //                                                                        && x.task_status == "Completed")).Select(y => y.task_who_id).Distinct().ToList();

        //        List<crm_inquiries> send_to_inquiries = db_ctx.crm_inquiries.Where(x => (x.inq_pri_prog_interest != null || x.inq_sec_prog_interest != null)
        //                                                                        && x.inquiry_datetime > maxDelayDateTime
        //                                                                        && x.inquiry_datetime <= delayDate
        //                                                                        && x.inq_deleted == false
        //                                                                        && (Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                                            || Settings.EslProgIds.Contains(x.inq_sec_prog_interest))
        //                                                                        && x.inq_contact_id != null
        //                                                                        && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        List<crm_contacts> send_to_contacts = new List<crm_contacts>();

        //        if (send_to_inquiries.Any() && Continue(method, send_to_inquiries.Count))
        //        {
        //            foreach (crm_inquiries i in send_to_inquiries)
        //            {
        //                send_to_contacts = db_ctx.crm_contacts.Where(x => x.contact_id == i.inq_contact_id
        //                                                                        && x.contact_deleted == false
        //                                                                        && x.contact_colleague_id != "0525280"
        //                                                                        && x.contact_is_email_bounced == false
        //                                                                        && x.contact_invalid_email_flag == false).ToList();

        //                if (send_to_contacts.Any())
        //                {
        //                    crm_contacts contact = send_to_contacts.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        if (Settings.EslProgIds.Contains(i.inq_pri_prog_interest) && !Settings.EslProgIds.Contains(i.inq_sec_prog_interest))
        //                        {
        //                            i.inq_pri_prog_interest = i.inq_sec_prog_interest;
        //                        }

        //                        if (!string.IsNullOrEmpty(i.inq_pri_prog_interest) && !Settings.EslProgIds.Contains(i.inq_pri_prog_interest))
        //                        {

        //                            dcp1 = db_ctx.dynamic_content.Where(x => x.dynamic_content_matching_id == i.inq_pri_prog_interest).Select(y => y.dynamic_content_html).FirstOrDefault();
        //                            if (!string.IsNullOrEmpty(dcp1))
        //                            {
        //                                contact.contact_rciresl6_flag = true;
        //                                contact.contact_prog_interest_content = dcp1;
        //                                contact.contact_prog_interest_content += Environment.NewLine;
        //                            }

        //                            contact.contact_last_modified_datetime = DateTime.Now;
        //                            contact.contact_last_modified_by = Settings.CrmAdmin;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        List<crm_contacts> contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_rciresl6_flag == true
        //                                                                                    && !recipients.Contains(x.contact_id)).ToList();

        //        foreach (crm_contacts c in contacts_to_clear)
        //        {
        //            c.contact_rciresl6_flag = false;
        //            c.contact_prog_interest_content = " ";

        //            c.contact_last_modified_by = Settings.CrmAdmin;
        //            c.contact_last_modified_datetime = DateTime.Now;
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        //public bool SetSTENSO19FLREGFlags()
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;

        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        var temp_contact = db_ctx.crm_contacts.Where(x => x.contact_id == "0031U00000LO9J3QAL").FirstOrDefault();

        //        var temp_task = db_ctx.crm_tasks.Where(x => x.task_who_id == "0031U00000LO9J3QAL").FirstOrDefault();

        //        List<string> previously_sent = db_ctx.crm_tasks.Where(x => x.task_subject.Contains("New Student Orientation on Tuesday September 3")
        //                                                                //&& x.task_subtype == "ListEmail"
        //                                                                //&& x.task_status == "Completed"
        //                                                                ).Select(y => y.task_who_id).Distinct().ToList();

        //        List<crm_event_registrations> send_to_registrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_deleted == false
        //                                                                                                                    && x.event_registration_contact_id != null
        //                                                                                                                    && x.event_registration_activity_extender_id == "a0U1U000001frBzUAI"
        //                                                                                                                    && !previously_sent.Contains(x.event_registration_contact_id)).Distinct().ToList();

        //        List<crm_contacts> send_to_contacts = new List<crm_contacts>();

        //        if (send_to_registrations.Any() && Continue(method, send_to_registrations.Count))
        //        {
        //            foreach (crm_event_registrations i in send_to_registrations)
        //            {
        //                send_to_contacts = db_ctx.crm_contacts.Where(x => x.contact_id == i.event_registration_contact_id
        //                                                                           && x.contact_deleted == false
        //                                                                           && x.contact_colleague_id != "0525280"
        //                                                                           && x.contact_is_email_bounced == false
        //                                                                           && x.contact_invalid_email_flag == false).ToList();
        //                if (send_to_contacts.Any())
        //                {
        //                    crm_contacts contact = send_to_contacts.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

        //                    if (contact != null)
        //                    {
        //                        recipients.Add(contact.contact_id);

        //                        contact.contact_stenso19flreg_flag = true;
        //                        contact.contact_last_modified_datetime = DateTime.Now;
        //                        contact.contact_last_modified_by = Settings.CrmAdmin;
        //                    }
        //                }
        //            }
        //        }

        //        List<crm_contacts> contacts_to_clear = db_ctx.crm_contacts.Where(x => x.contact_stenso19flreg_flag == true
        //                                                                                    && !recipients.Contains(x.contact_id)
        //                                                                                    ).ToList();

        //        foreach (crm_contacts c in contacts_to_clear)
        //        {
        //            c.contact_stenso19flreg_flag = false;
        //            c.contact_last_modified_by = Settings.CrmAdmin;
        //            c.contact_last_modified_datetime = DateTime.Now;
        //        }

        //        db_ctx.SaveChanges();

        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}

        //public bool SyncInternationalFlag()
        //{
        //    int inserted = 0;
        //    int updated = 0;
        //    int total = 0;

        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime StartTime = DateTime.Now;
        //    DateTime LastRunDate = DateTime.Now;
        //    List<wv_persons> dw_students = new List<wv_persons>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        LastRunDate = GetLastRunDateTime(MethodName);

        //        var students = db_ctx.wv_persons.Where(x => x.sis_stu_international_flag == "1").ToList();

        //        if (students.Any() && Continue(method, students.Count))
        //        {
        //            foreach (wv_persons dw_student in students)
        //            {

        //                crm_contacts DbContact = new crm_contacts();

        //                var matches = db_ctx.crm_contacts.Where(x => (x.contact_colleague_id == dw_student.sis_student_id
        //                                                  || (x.contact_first_name == dw_student.sis_stu_first_name
        //                                                     && x.contact_last_name == dw_student.sis_stu_last_name
        //                                                     && x.contact_birthdate == dw_student.sis_stu_birth_date)
        //                                                  || (x.contact_first_name == dw_student.sis_stu_first_name
        //                                                     && x.contact_last_name == dw_student.sis_stu_hist_last_name
        //                                                     && x.contact_birthdate == dw_student.sis_stu_birth_date)));
        //                if (matches.Any())
        //                {
        //                    DbContact = matches.OrderByDescending(o => o.contact_last_modified_datetime).FirstOrDefault();


        //                    if (!string.IsNullOrEmpty(dw_student.sis_stu_international_flag))
        //                    {
        //                        DbContact.contact_potential_duplicate = (matches.Count() > 1);
        //                        DbContact.contact_deleted = dw_student.sis_stu_deleted_flag;
        //                        DbContact.contact_international = (dw_student.sis_stu_international_flag == "1");
        //                        DbContact.contact_last_modified_datetime = DateTime.Now;
        //                        db_ctx.SaveChanges();

        //                        updated++;
        //                    }
        //                }

        //                total++;
        //            }
        //        }
        //        success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

        //    return success;
        //}

        //public List<string[]> GetEventRegistrations(string content = null) {
        //    List<string[]> evRegList = new List<string[]>();
        //    string ErrorMsg = string.Empty;
        //    try {
        //        var query = (from er in db_ctx.crm_event_registrations
        //                     join e in db_ctx.crm_events on er.event_registration_activity_extender_id equals e.event_activity_extender_id
        //                     join c in db_ctx.crm_contacts on er.event_registration_contact_id equals c.contact_id
        //                     where er.event_registration_deleted == false
        //                     select e.activity_id + ";" + er.event_registration_attended + ";" + er.event_registration_registered + ";" + c.contact_id + ";" + c.contact_colleague_id + ";" + c.contact_id_card_barcode + ";" + c.contact_first_name + ";" + c.contact_last_name + ";" + c.contact_birthdate.ToString()
        //                     ).ToList();

        //        if (!string.IsNullOrEmpty(content)) {
        //            query = query.Where(x => x.Contains(content)).ToList();
        //        }

        //        foreach (var item in query) {
        //            evRegList.Add(item.Split(';'));
        //        }
        //    } catch (Exception ex) {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return evRegList;
        //}

        //public bool ValidateEventRegistration(EventRegistrationObj eventRegistrationObj)
        //{
        //    bool success = false;
        //    string ErrorMsg = string.Empty;

        //    try
        //    {
        //        // Lookup for Contact Id if it is empty
        //        if (string.IsNullOrEmpty(eventRegistrationObj.ContactId)) { eventRegistrationObj.ContactId = LookupContactId(eventRegistrationObj); }

        //        // Validate Event Registration
        //        if (!string.IsNullOrEmpty(eventRegistrationObj.EventId) && !string.IsNullOrEmpty(eventRegistrationObj.ContactId))
        //        {
        //            var eventRegQuery = (from er in db_ctx.crm_event_registrations
        //                                 join e in db_ctx.crm_events on er.event_registration_activity_extender_id equals e.event_activity_extender_id
        //                                 where er.event_registration_deleted == false
        //                                 && er.event_registration_contact_id == eventRegistrationObj.ContactId
        //                                 && e.activity_id == eventRegistrationObj.EventId
        //                                 select er
        //                         ).OrderByDescending(o => o.event_registration_modified_datetime).ToList();

        //            success = eventRegQuery.Any();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

        //    return success;
        //}



        //public bool SendEmailCampaigns()
        //{
        //    int inserted = 0;
        //    int updated = 0;
        //    int total = 0;

        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string soql = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime StartTime = DateTime.Now;
        //    DateTime LastRunDate = DateTime.Now;

        //    try
        //    {
        //        MethodName = GetCurrentMethod();
        //        LastRunDate = GetLastRunDateTime(MethodName);

        //        var email_campaigns = db_ctx.crm_email_campaigns.Where(x => x.ec_deleted == false
        //                                                            && x.ec_active_flag == true
        //                                                            && x.ec_modified_datetime >= LastRunDate
        //                                                            && (x.ec_end_datetime >= DateTime.Now
        //                                                            || x.ec_end_datetime == null));

        //        if (email_campaigns.Any())
        //        {

        //            foreach (crm_email_campaigns email_campaign in email_campaigns)
        //            {

        //                var broadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
        //                                                                    && x.email_campaign_id == email_campaign.email_campaign_id);

        //                if (broadcasts.Any())
        //                {


        //                }
        //            }
        //        }


        //        var broadcasts_to_send = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
        //                                                                    && x.email_broadcast_status == "Pending"
        //                                                                    && x.email_broadcast_sent == DateTime.Today).ToList();

        //        foreach (crm_email_broadcasts broadcast in broadcasts_to_send)
        //        {

        //            var email_campaign = db_ctx.crm_email_campaigns.Where(x => x.email_campaign_id == broadcast.email_campaign_id).OrderByDescending(x => x.ec_modified_datetime).FirstOrDefault();

        //            var email_template = db_ctx.crm_email_templates.Where(x => x.email_template_deleted == false
        //                                                                && x.email_template_id == email_campaign.ec_template_id)
        //                                                        .OrderByDescending(x => x.email_template_modified_datetime)
        //                                                        .FirstOrDefault();

        //            List<crm_email_messages> messages_to_send = new List<crm_email_messages>();

        //            List<Record> recipients = GetReportRecipients(email_campaign.ec_report_id);

        //            foreach (Record record in recipients)
        //            {

        //                crm_email_messages email = new crm_email_messages
        //                {
        //                    email_message_to_address = record.Email,
        //                    email_message_from_address = email_campaign.ec_from_email_address,
        //                    email_message_from_name = email_campaign.ec_from_email_address_title,
        //                    email_message_bcc_address = Settings.CrmEmailAddress,
        //                    email_message_is_incoming = false,
        //                    email_message_datetime = DateTime.Now,
        //                    email_message_subject = email_template.email_template_subject,

        //                    email_message_html_body = MergeEmailBody(email_template.email_template_html_value, record),
        //                    email_message_text_body = MergeEmailBody(email_template.email_template_body, record)
        //                };

        //                messages_to_send.Add(email);
        //            }

        //            SendEmailMessages(messages_to_send);

        //            if (success)
        //            {
        //                foreach (crm_email_messages message in messages_to_send)
        //                {
        //                    db_ctx.crm_email_messages.Add(message);
        //                }

        //                broadcast.email_broadcast_sent = DateTime.Now;
        //                broadcast.email_broadcast_status = "Success";

        //                db_ctx.SaveChanges();

        //                updated++;
        //            }
        //            else
        //            {
        //                broadcast.email_broadcast_status = "Failed to send broadcast";
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



        //public bool DuplicateCommunication(string contactId, string campaignId) {
        //    bool duplicate = false;
        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime now = DateTime.Now;
        //    DateTime delayDate = DateTime.Today.AddDays(-1);
        //    DateTime maxDelayDateTime = DateTime.Today.AddDays(-5);
        //    string dcp1 = string.Empty;
        //    string dcp2 = string.Empty;
        //    List<string> recipients = new List<string>();

        //    try
        //    {
        //        var send_to_inquiries = db_ctx.crm_inquiries.Where(x => (x.inq_pri_prog_interest != null || x.inq_sec_prog_interest != null)
        //                                                            && x.inquiry_datetime > maxDelayDateTime
        //                                                            && x.inquiry_datetime <= delayDate
        //                                                            && x.inq_deleted == false
        //                                                            && !Settings.EslProgIds.Contains(x.inq_pri_prog_interest)
        //                                                            && !Settings.EslProgIds.Contains(x.inq_sec_prog_interest)
        //                                                            && x.inq_contact_id != null
        //                                                            && !previously_sent.Contains(x.inq_contact_id)).ToList();

        //        var contact = possible_matches.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();

        //        if (contact != null)
        //        {
        //            recipients.Add(contact.contact_id);

        //            dcp1 = db_ctx.dynamic_content.Where(x => x.dynamic_content_matching_id == i.inq_pri_prog_interest).Select(y => y.dynamic_content_html).FirstOrDefault();

        //            if (i.inq_pri_prog_interest != i.inq_sec_prog_interest)
        //            {
        //                dcp2 = db_ctx.dynamic_content.Where(x => x.dynamic_content_matching_id == i.inq_sec_prog_interest).Select(y => y.dynamic_content_html).FirstOrDefault();
        //            }

        //            if (!string.IsNullOrEmpty(dcp1))
        //            {
        //                contact.contact_prog_interest_flag = true;
        //                contact.contact_prog_interest_content = dcp1;
        //                contact.contact_prog_interest_content += Environment.NewLine;
        //            }

        //            if (!string.IsNullOrEmpty(dcp2))
        //            {
        //                contact.contact_prog_interest_content += dcp2;
        //                contact.contact_prog_interest_content += Environment.NewLine;
        //            }

        //            contact.contact_last_modified_datetime = DateTime.Now;
        //            contact.contact_last_modified_by = Settings.CrmAdmin;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return duplicate;
        //}

        //public bool SendEmailCampaigns(string campaignId = null)
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
        //    crm_reports crm_report = new crm_reports();
        //    crm_email_broadcasts crm_email_broadcast = new crm_email_broadcasts();
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
        //        LastRunDate = GetLastRunDateTime(MethodName); 

        //        if (!string.IsNullOrEmpty(campaignId))
        //        {
        //            crm_email_campaigns = db_ctx.crm_email_campaigns.Where(x => (campaignId != null && x.email_campaign_id == campaignId))
        //                                                        .OrderByDescending(x => x.ec_modified_datetime).ToList();
        //        }
        //        else
        //        {
        //            crm_email_campaigns = db_ctx.crm_email_campaigns.Where(x => (x.ec_deleted == false
        //                                                                && x.ec_mark_delete == false
        //                                                                && x.ec_active_flag == true
        //                                                                && (x.ec_start_datetime <= DateTime.Now
        //                                                                    && (x.ec_end_datetime == null || x.ec_end_datetime >= DateTime.Today)))
        //                                                               ).OrderByDescending(x => x.ec_modified_datetime).ToList();
        //        }

        //        if (ProcessRecords(method, crm_email_campaigns.Count))
        //        {

        //            foreach (crm_email_campaigns crm_email_campaign in crm_email_campaigns)
        //            {

        //                // reset addresstypes
        //                SendToAddressTypes = new List<string>();
        //                crm_email_broadcast = new crm_email_broadcasts();
        //                crm_email_template = new crm_email_templates();
        //                EmailRecipients = new List<Record>();
        //                int MessagesSentCnt = 0;


        //                if (crm_email_campaign != null)
        //                {

        //                    List<string> DaysOfWeek = new List<string>();
        //                    DateTime EndDate = crm_email_campaign.ec_end_datetime ?? DateTime.Today.AddDays(14);
        //                    DateTime StartDate = crm_email_campaign.ec_start_datetime ?? DateTime.Today;
        //                    if (StartDate < DateTime.Today) { StartDate = DateTime.Today; }
        //                    String Days = crm_email_campaign.ec_recur_week_days ?? DateTime.Today.ToString("dddd", CurrentDateFormat);
        //                    DaysOfWeek = Days.Split(';').ToList<string>();

        //                    // make sure the report actually exists
        //                    var reports = db_ctx.crm_reports.Where(x => x.crm_report_deleted == false
        //                                                           && x.crm_report_mark_delete == false
        //                                                           && x.crm_report_id == crm_email_campaign.ec_report_id);

        //                    if (reports.Any())
        //                    {
        //                        crm_report = reports.OrderByDescending(x => x.crm_report_modified_date).FirstOrDefault();

        //                        if (crm_report != null)
        //                        {

        //                            for (var day = StartDate.Date; day.Date <= EndDate.Date; day = day.AddDays(1))
        //                            {

        //                                crm_email_broadcast = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_sent.Value.Year == day.Year
        //                                                                                          && x.email_broadcast_sent.Value.Month == day.Month
        //                                                                                          && x.email_broadcast_sent.Value.Day == day.Day
        //                                                                                          && x.email_broadcast_deleted == false
        //                                                                                          && x.email_broadcast_mark_delete == false
        //                                                                                          && x.email_campaign_id == crm_email_campaign.email_campaign_id)
        //                                                                                    .OrderByDescending(x => x.email_broadcast_modfied_datetime)
        //                                                                                    .FirstOrDefault();

        //                                if (DaysOfWeek.Contains(day.ToString("dddd", CurrentDateFormat)))
        //                                {
        //                                    if (crm_email_broadcast == null || crm_email_broadcast.email_broadcast_created_datetime == null)
        //                                    {
        //                                        crm_email_broadcast = new crm_email_broadcasts()
        //                                        {
        //                                            email_broadcast_guid = Guid.NewGuid(),
        //                                            email_broadcast_name = crm_email_campaign.ec_name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss", CurrentDateFormat),
        //                                            email_broadcast_status = "Scheduled",
        //                                            email_campaign_id = crm_email_campaign.email_campaign_id,
        //                                            email_broadcast_email_campaign = crm_email_campaign.email_campaign_id,
        //                                            email_report_id = crm_email_campaign.ec_report_id,
        //                                            email_report_name = crm_report.crm_report_name,
        //                                            email_template_id = crm_email_campaign.ec_template_id,
        //                                            email_broadcast_sent = day,
        //                                            email_broadcast_modified_by = Settings.CrmAdmin,
        //                                            email_broadcast_modfied_datetime = DateTime.Now,
        //                                            email_broadcast_created_by = Settings.CrmAdmin,
        //                                            email_broadcast_created_datetime = DateTime.Now,
        //                                            email_broadcast_sys_modstamp = DateTime.Now,
        //                                        };

        //                                        db_ctx.crm_email_broadcasts.Add(crm_email_broadcast);
        //                                        db_ctx.SaveChanges();
        //                                    }

        //                                    string dayOfWeek = DateTime.Today.ToString("dddd", CurrentDateFormat);
        //                                    if (day.Date == DateTime.Today && DaysOfWeek.Contains(dayOfWeek))
        //                                    {
        //                                        if (crm_email_broadcast.email_broadcast_status != "Success")
        //                                        {
        //                                            try
        //                                            {
        //                                                var client = new CrmSfApi.SalesforceClient();
        //                                                var authFlow = new CrmSfApi.Security.UsernamePasswordAuthenticationFlow(clientId, clientSecret, username, password);

        //                                                client.Authenticate(authFlow);

        //                                                var reportRecords = client.GetMergeObjectIdsByReportId<List<object>>(analyticApiUrl, crm_email_campaign.ec_report_id);

        //                                                crm_email_templates = db_ctx.crm_email_templates.Where(x => x.email_template_deleted == false
        //                                                                                                    && x.email_template_id == crm_email_campaign.ec_template_id).ToList();

        //                                                if (crm_email_templates.Any())
        //                                                {
        //                                                    crm_email_template = crm_email_templates.OrderByDescending(x => x.email_template_modified_datetime)
        //                                                                                            .FirstOrDefault();
        //                                                }

        //                                                foreach (JObject record in reportRecords)
        //                                                {
        //                                                    Record EmailRecipient = new Record();

        //                                                    // would be more scalable to iterate columns, find correct object and populate local instance
        //                                                    var contact_id = record["dataCells"][0]["value"].ToString();
        //                                                    if (contact_id != null && GetSfRecordTypeById(contact_id) == SfRecordTypes.Contact)
        //                                                    {

        //                                                        EmailRecipient = db_ctx.crm_contacts.Where(x => x.contact_email_opt_out == false
        //                                                                                                   && x.contact_is_email_bounced == false
        //                                                                                                   && x.contact_id == contact_id)
        //                                                                                            .Select(y => new Record()
        //                                                                                            {
        //                                                                                                PreferredEmail = y.contact_email
        //                                                                                                                    ,
        //                                                                                                AlternativeEmail = y.contact_alternate_email
        //                                                                                                                    ,
        //                                                                                                CollegeEmail = y.contact_college_email
        //                                                                                                                    ,
        //                                                                                                FirstName = y.contact_first_name
        //                                                                                                                    ,
        //                                                                                                ContactId = y.contact_id
        //                                                                                                                    ,
        //                                                                                                StudentId = y.contact_colleague_id
        //                                                                                                                    ,
        //                                                                                                AccountId = y.contact_account_id
        //                                                                                            })
        //                                                                                                                    .Distinct().FirstOrDefault();
        //                                                    }

        //                                                    if (crm_email_campaign.ec_name.Contains("RCRIR1")
        //                                                        || crm_email_campaign.ec_name.Contains("RCRIR3")
        //                                                        || crm_email_campaign.ec_name.Contains("RCIRESL6"))
        //                                                    {
        //                                                        var inquiry_id = record["dataCells"][1]["value"].ToString();
        //                                                        if (!string.IsNullOrEmpty(inquiry_id) && GetSfRecordTypeById(inquiry_id) == SfRecordTypes.Inquiry)
        //                                                        {

        //                                                            crm_inquiries inquiry = db_ctx.crm_inquiries.Where(x => x.inquiry_mark_delete == false
        //                                                                                                                        && x.inq_deleted == false
        //                                                                                                                        && x.inquiry_id == inquiry_id)
        //                                                                                                                .OrderByDescending(y => y.inq_modified_datetime)
        //                                                                                                                .FirstOrDefault();

        //                                                            if (inquiry != null)
        //                                                            {
        //                                                                secondaryId = inquiry.inquiry_id;
        //                                                                EmailRecipient.InquiryId = inquiry.inquiry_id;
        //                                                                EmailRecipient.NoCommunication = inquiry.inq_no_communications;

        //                                                                if (inquiry.inq_pri_prog_interest != null)
        //                                                                {
        //                                                                    EmailRecipient.ProgramInterestId1 = inquiry.inq_pri_prog_interest;
        //                                                                }
        //                                                                if (inquiry.inq_sec_prog_interest != null)
        //                                                                {
        //                                                                    EmailRecipient.ProgramInterestId2 = inquiry.inq_sec_prog_interest;
        //                                                                }
        //                                                                if (inquiry.inq_services_interest != null)
        //                                                                {
        //                                                                    EmailRecipient.ServicesInterest = inquiry.inq_services_interest;
        //                                                                }
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                    EmailRecipients.Add(EmailRecipient);
        //                                                }
        //                                            }
        //                                            catch (Exception ex)
        //                                            {
        //                                                ErrorMsg += ex.ToString();
        //                                            }

        //                                            if (crm_email_template != null)
        //                                            {
        //                                                crm_email_broadcast.email_template_name = crm_email_template.email_template_name;

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

        //                                                    int recipient_count = EmailRecipients.Count();
        //                                                    foreach (Record EmailRecipient in EmailRecipients.Where(x => x.NoCommunication != true))
        //                                                    {
        //                                                        if (EmailRecipient != null)
        //                                                        {
        //                                                            crm_email_messages crm_email_message = new crm_email_messages();

        //                                                            if (!string.IsNullOrEmpty(secondaryId))
        //                                                            {
        //                                                                crm_email_message.email_message_secondary_id = secondaryId;
        //                                                            }

        //                                                            switch (SendToAddressType)
        //                                                            {
        //                                                                case "Preferred":
        //                                                                    crm_email_message.email_message_to_address = EmailRecipient.PreferredEmail;
        //                                                                    break;
        //                                                                case "Alternate":
        //                                                                    crm_email_message.email_message_to_address = EmailRecipient.AlternativeEmail;
        //                                                                    break;
        //                                                                case "College":
        //                                                                    crm_email_message.email_message_to_address = EmailRecipient.CollegeEmail;
        //                                                                    break;
        //                                                                default:
        //                                                                    break;
        //                                                            }

        //                                                            bool previouslySent = db_ctx.crm_email_messages.Where(x => x.email_message_campaign_id == crm_email_campaign.email_campaign_id
        //                                                                                                                       && x.email_message_broadcast_guid == crm_email_broadcast.email_broadcast_guid
        //                                                                                                                       && x.email_message_contact_id == EmailRecipient.ContactId
        //                                                                                                                       && x.email_message_to_address == crm_email_message.email_message_to_address).Any();
        //                                                            if (!previouslySent)
        //                                                            {

        //                                                                crm_email_message.email_message_guid = Guid.NewGuid();
        //                                                                crm_email_message.email_message_contact_id = EmailRecipient.ContactId;
        //                                                                crm_email_message.email_message_account_id = EmailRecipient.AccountId;
        //                                                                crm_email_message.email_message_campaign_id = crm_email_campaign.email_campaign_id;
        //                                                                crm_email_message.email_message_broadcast_id = crm_email_broadcast.email_broadcast_id;
        //                                                                crm_email_message.email_message_subject = crm_email_template.email_template_subject;
        //                                                                crm_email_message.email_message_from_address = crm_email_campaign.ec_from_email_address;
        //                                                                crm_email_message.email_message_from_name = crm_email_campaign.ec_from_email_address_title;
        //                                                                crm_email_message.email_message_bcc_address = Settings.CrmEmailAddress;
        //                                                                crm_email_message.email_message_is_incoming = false;
        //                                                                crm_email_message.email_message_datetime = DateTime.Now;
        //                                                                crm_email_message.email_message_created_by = Settings.CrmAdmin;
        //                                                                crm_email_message.email_message_created_datetime = DateTime.Now;
        //                                                                crm_email_message.email_message_modified_by = Settings.CrmAdmin;
        //                                                                crm_email_message.email_message_modified_datetime = DateTime.Now;
        //                                                                crm_email_message.email_message_html_body = MergeEmailBody(crm_email_template.email_template_html_value, EmailRecipient, true);
        //                                                                crm_email_message.email_message_text_body = MergeEmailBody(crm_email_template.email_template_body, EmailRecipient, false);


        //                                                                if ((SendToAddressType == "Alternate"
        //                                                                        && (!string.IsNullOrEmpty(crm_email_message.email_message_to_address)
        //                                                                            && !crm_email_message.email_message_to_address.Contains("lethbridge")))
        //                                                                    || (SendToAddressType == "College"
        //                                                                        && (!string.IsNullOrEmpty(crm_email_message.email_message_to_address)
        //                                                                            && crm_email_message.email_message_to_address.Contains("lethbridge"))))
        //                                                                {
        //                                                                    crm_tasks crm_task = new crm_tasks()
        //                                                                    {
        //                                                                        activity_guid = Guid.NewGuid(),
        //                                                                        task_account_id = crm_email_message.email_message_account_id,
        //                                                                        task_who_id = crm_email_message.email_message_contact_id,
        //                                                                        task_assigned_to_id = crm_email_message.email_message_contact_id,
        //                                                                        task_what_id = crm_email_broadcast.email_broadcast_id,
        //                                                                        task_subject = crm_email_message.email_message_subject,
        //                                                                        task_name_id = crm_email_message.email_message_id,
        //                                                                        task_related_to_id = crm_email_broadcast.email_broadcast_id,
        //                                                                        task_subtype = "BroadcastEmail",
        //                                                                        task_priority = "Normal",
        //                                                                        task_created_by = Settings.CrmAdmin,
        //                                                                        task_created_datetime = DateTime.Now,
        //                                                                        task_modified_by = Settings.CrmAdmin,
        //                                                                        task_modified_datetime = DateTime.Now,
        //                                                                    };

        //                                                                    SendEmail sendEmail = new SendEmail()
        //                                                                    {
        //                                                                        Body = crm_email_message.email_message_html_body,
        //                                                                        Title = crm_email_message.email_message_subject,
        //                                                                        To = crm_email_message.email_message_to_address,
        //                                                                        From = crm_email_message.email_message_from_address,
        //                                                                    };

        //                                                                    try
        //                                                                    {
        //                                                                        crm_task.task_description = sendEmail.Body;
        //                                                                        if (sendEmail.Send() && string.IsNullOrEmpty(sendEmail.ErrorMsg))
        //                                                                        {
        //                                                                            recipient_count--;
        //                                                                            MessagesSentCnt++;
        //                                                                            crm_task.task_status = "Completed";
        //                                                                            crm_email_message.email_message_status = "Successful";
        //                                                                            crm_email_message.email_message_datetime = DateTime.Now;
        //                                                                        }
        //                                                                        else
        //                                                                        {
        //                                                                            crm_task.task_status = "Failed";
        //                                                                            crm_email_message.email_message_status = "Failed";
        //                                                                            crm_email_message.email_message_error = sendEmail.ErrorMsg;
        //                                                                            ErrorMsg += sendEmail.ErrorMsg;
        //                                                                        }
        //                                                                    }
        //                                                                    catch (Exception ex)
        //                                                                    {
        //                                                                        crm_email_message.email_message_status = "Failed";
        //                                                                        crm_email_message.email_message_error = ex.ToString();
        //                                                                        ErrorMsg += ex.ToString();
        //                                                                    }

        //                                                                    try
        //                                                                    {
        //                                                                        db_ctx.crm_tasks.Add(crm_task);
        //                                                                        db_ctx.crm_email_messages.Add(crm_email_message);
        //                                                                    }
        //                                                                    catch (Exception ex)
        //                                                                    {
        //                                                                        ErrorMsg += ex.ToString();
        //                                                                    }
        //                                                                }

        //                                                                db_ctx.SaveChanges();
        //                                                                success = true;
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                ErrorMsg += "Email Template was not found!";
        //                                            }

        //                                            try
        //                                            {
        //                                                crm_email_broadcast.email_broadcast_status = "Success";
        //                                                crm_email_broadcast.email_broadcast_sent = DateTime.Now;
        //                                                crm_email_broadcast.email_broadcast_messages_sent = MessagesSentCnt;

        //                                                db_ctx.SaveChanges();
        //                                            }
        //                                            catch (Exception ex)
        //                                            {
        //                                                ErrorMsg += ex.ToString();
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            ErrorMsg += "Email Broadcast was not found or already sent today!";
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    // if a previous broadcast was scheduled on this day, DELETE IT
        //                                    //if (crm_email_broadcast != null) { crm_email_broadcast.email_broadcast_mark_delete = true; }

        //                                    db_ctx.SaveChanges();
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            ErrorMsg += "Email Report was not found!";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ErrorMsg += "Email Report was not found!";
        //                    }
        //                }
        //                else
        //                {
        //                    ErrorMsg += "Email Campaign was not found!";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ErrorMsg += "Email Campaign was not found!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsg = ex.ToString();
        //    }

        //    RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

        //    return success;
        //}

    }
}
