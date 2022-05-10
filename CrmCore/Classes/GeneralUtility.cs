using CrmLcLib;
using CrmSfApi;
using IronBarCode;
using lc.crm;
using lc.crm.api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web.Configuration;
using static CrmCore.Enumerations;


namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        private bool ProcessUpsertResults(InsertResults type, Dictionary<string, SaveResult> save_results)
        {
            bool success = false;
            string ErrorMsg = string.Empty;

            try
            {
                foreach (KeyValuePair<string, SaveResult> result in save_results) {
                    Guid current_guid = new Guid(result.Key);
                    ErrorMsg = string.Empty;

                    switch (type)
                    {
                        case InsertResults.Contact:
                            crm_contacts contact = db_ctx.crm_contacts.Where(x => x.contact_guid == current_guid
                                                                                  && x.contact_deleted == false)
                                                                         .OrderByDescending(y => y.contact_created_datetime)
                                                                         .FirstOrDefault();
                            if (contact != null)
                            {
                                if (result.Value.success)
                                {
                                    contact.contact_id = result.Value.id;
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var contact_delete = db_ctx.crm_contacts.Where(x => x.contact_id == result.Value.id).FirstOrDefault();
                                                    contact_delete.contact_deleted = true;
                                                    contact_delete.contact_mark_delete = false;
                                                    contact_delete.contact_last_modified_by = Settings.CrmAdmin;
                                                    contact_delete.contact_last_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }

                            break;
                        case InsertResults.Term:
                            crm_terms term = db_ctx.crm_terms.Where(x => x.term_guid == current_guid
                                                                            && x.term_deleted == false)
                                                                    .OrderByDescending(y => y.term_created_datetime)
                                                                    .FirstOrDefault();
                            if (term != null)
                            {
                                if (result.Value.success)
                                {
                                    if (!string.IsNullOrWhiteSpace(result.Value.id) && term.term_id != result.Value.id)
                                    {
                                        term.term_id = result.Value.id;
                                        term.term_modifed_datetime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var term_delete = db_ctx.crm_terms.Where(x => x.term_id == result.Value.id).FirstOrDefault();
                                                    term_delete.term_deleted = true;
                                                    term_delete.term_mark_delete = false;
                                                    term_delete.term_modifed_datetime = DateTime.Now;
                                                }
                                                break;
                                            case StatusCode.DUPLICATE_VALUE:

                                                List<hed__Term__c> SfTerms = new List<hed__Term__c>();

                                                if (term != null && !string.IsNullOrWhiteSpace(term.term_code))
                                                {

                                                    var soql = GetSoqlString(SoqlEnums.GetTermBySisTermCode, DownloadTypes.SpecificRecordType, 1, DateTime.Now, term.term_code);

                                                    using (ApiService api = new ApiService())
                                                    {
                                                        SfTerms = api.Query<hed__Term__c>(soql);
                                                    }

                                                    if (SfTerms.Any())
                                                    {

                                                        bool FirstRecord = true;
                                                        foreach (hed__Term__c SfTerm in SfTerms)
                                                        {
                                                            if (FirstRecord)
                                                            {
                                                                FirstRecord = false;

                                                                if (!string.IsNullOrWhiteSpace(SfTerm.Id))
                                                                {
                                                                    term.term_id = SfTerm.Id;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                term.term_mark_delete = true;
                                                            }

                                                            term.term_modified_by = Settings.CrmAdmin;
                                                            term.term_modifed_datetime = DateTime.Now;
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.Affiliation:
                            crm_affiliations affiliation = db_ctx.crm_affiliations.Where(x => x.affiliation_guid == current_guid
                                                                                          && x.affiliation_deleted == false)
                                                                                 .OrderByDescending(y => y.affiliation_created_datetime)
                                                                                 .FirstOrDefault();
                            if (affiliation != null)
                            {
                                if (result.Value.success)
                                {
                                    if (!string.IsNullOrWhiteSpace(result.Value.id) && affiliation.affiliation_id != result.Value.id) {
                                        affiliation.affiliation_id = result.Value.id;
                                        affiliation.affiliation_last_modified_datetime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                affiliation.affiliation_deleted = true;
                                                affiliation.affiliation_mark_delete = false;
                                                break;
                                            case StatusCode.DUPLICATE_VALUE:

                                                List<hed__Affiliation__c> SfAffiliations = new List<hed__Affiliation__c>();

                                                if (affiliation != null && !string.IsNullOrWhiteSpace(affiliation.lc_student_program_id)) {

                                                    var soql = GetSoqlString(SoqlEnums.GetAffiliationByStudentProgram, DownloadTypes.SpecificRecordType, 1, DateTime.Now, affiliation.lc_student_program_id);

                                                    using (ApiService api = new ApiService())
                                                    {
                                                        SfAffiliations = api.Query<hed__Affiliation__c>(soql);
                                                    }

                                                    if (SfAffiliations.Any()) {
                                                        
                                                        bool FirstRecord = true;
                                                        foreach (hed__Affiliation__c SfAffiliation in SfAffiliations)
                                                        {
                                                            if (FirstRecord)
                                                            {
                                                                FirstRecord = false;
                                                                var aff_id = SfAffiliations.OrderByDescending(x => x.LastModifiedDate).Select(y=>y.Id).FirstOrDefault();

                                                                if (!string.IsNullOrWhiteSpace(aff_id)) {
                                                                    affiliation.affiliation_id = aff_id;
                                                                    affiliation.affiliation_last_modified_datetime = DateTime.Now;
                                                                }
                                                            }
                                                            else {
                                                                // create a potential duplicate flag?
                                                                affiliation.affiliation_guid_mismatch = true;
                                                                affiliation.affiliation_last_modified_datetime = DateTime.Now;
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + "affiliation_guid: " + affiliation.affiliation_guid.ToString() + "; affiliation.affiliation_id: " + affiliation.affiliation_id +  "; Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                                }
                            break;
                        case InsertResults.Inquiry:
                            crm_inquiries inquiry = db_ctx.crm_inquiries.Where(x => x.inquiry_guid == current_guid
                                                                               && x.inq_deleted == false)
                                                                           .OrderByDescending(y => y.inq_created_datetime)
                                                                           .FirstOrDefault();
                            if (inquiry != null)
                            {
                                if (result.Value.success)
                                {
                                    if (inquiry.inquiry_id != result.Value.id) { 
                                        inquiry.inquiry_id = result.Value.id;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var inquiry_delete = db_ctx.crm_inquiries.Where(x => x.inquiry_id == result.Value.id).FirstOrDefault();
                                                    inquiry_delete.inq_deleted = true;
                                                    inquiry_delete.inquiry_mark_delete = false;
                                                    inquiry_delete.inq_modified_by = Settings.CrmAdmin;
                                                    inquiry_delete.inq_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.InquiryProgram:
                            crm_inquiry_programs inquiry_program = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_guid == current_guid
                                                                                                        && x.crm_inq_prog_deleted == false)
                                                                                               .OrderByDescending(y => y.crm_inq_prog_created_datetime)
                                                                                               .FirstOrDefault();
                            if (inquiry_program != null)
                            {
                                if (result.Value.success)
                                {
                                    if (inquiry_program.crm_inq_prog_id != result.Value.id)
                                    {
                                        inquiry_program.crm_inq_prog_id = result.Value.id;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var inquiry_program_delete = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_id == result.Value.id).FirstOrDefault();
                                                    inquiry_program_delete.crm_inq_prog_deleted = true;
                                                    inquiry_program_delete.crm_inq_prog_mark_delete = false;
                                                    inquiry_program_delete.crm_inq_prog_last_modified_by = Settings.CrmAdmin;
                                                    inquiry_program_delete.crm_inq_prog_last_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.Task:
                            crm_tasks task = db_ctx.crm_tasks.Where(x => x.activity_guid == current_guid
                                                                            && x.task_deleted == false)
                                                                   .OrderByDescending(y => y.task_created_datetime)
                                                                   .FirstOrDefault();
                            if (task != null)
                            {
                                if (result.Value.success)
                                {
                                    task.activity_id = result.Value.id;
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var task_delete = db_ctx.crm_tasks.Where(x => x.activity_id == result.Value.id).FirstOrDefault();
                                                    task_delete.task_deleted = true;
                                                    task_delete.task_mark_delete = false;
                                                    task_delete.task_modified_by = Settings.CrmAdmin;
                                                    task_delete.task_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.EmailCampaign:
                            crm_campaigns campaign = db_ctx.crm_campaigns.Where(x => x.campaign_guid == current_guid
                                                                                     && x.campaign_deleted == false)
                                                                            .OrderByDescending(y => y.campaign_created_datetime)
                                                                            .FirstOrDefault();
                            if (campaign != null)
                            {
                                if (result.Value.success)
                                {
                                    campaign.campaign_id = result.Value.id;
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var email_campaign_delete = db_ctx.crm_email_campaigns.Where(x => x.email_campaign_id == result.Value.id).FirstOrDefault();
                                                    email_campaign_delete.ec_deleted = true;
                                                    email_campaign_delete.ec_mark_delete = false;
                                                    email_campaign_delete.ec_modified_by = Settings.CrmAdmin;
                                                    email_campaign_delete.ec_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.EmailBroadcast:
                            crm_email_broadcasts broadcast = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_guid == current_guid
                                                                                                          && x.email_broadcast_deleted == false)
                                                                                                 .OrderByDescending(y => y.email_broadcast_created_datetime)
                                                                                                 .FirstOrDefault();
                            if (broadcast != null)
                            {
                                if (result.Value.success)
                                {
                                    if (!string.IsNullOrWhiteSpace(result.Value.id) && broadcast.email_broadcast_id != result.Value.id)
                                    {
                                        broadcast.email_broadcast_id = result.Value.id;
                                        broadcast.email_broadcast_modfied_datetime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var email_broadcast_delete = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_id == result.Value.id).FirstOrDefault();
                                                    email_broadcast_delete.email_broadcast_deleted = true;
                                                    email_broadcast_delete.email_broadcast_mark_delete = false;
                                                    email_broadcast_delete.email_broadcast_modfied_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.EmailTemplate:
                            crm_email_templates template = db_ctx.crm_email_templates.Where(x => x.email_template_guid == current_guid
                                                                                                       && x.email_template_deleted == false)
                                                                                              .OrderByDescending(y => y.email_template_created_datetime)
                                                                                              .FirstOrDefault();
                            if (template != null)
                            {
                                if (result.Value.success)
                                {
                                    template.email_template_id = result.Value.id;
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var email_template_delete = db_ctx.crm_email_templates.Where(x => x.email_template_id == result.Value.id).FirstOrDefault();
                                                    email_template_delete.email_template_deleted = true;
                                                    email_template_delete.email_template_mark_delete = false;
                                                    email_template_delete.email_template_modified_by = Settings.CrmAdmin;
                                                    email_template_delete.email_template_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.Event:
                            crm_events events = db_ctx.crm_events.Where(x => x.activity_guid == current_guid
                                                                                && x.event_deleted == false)
                                                                            .OrderByDescending(y => y.event_created_datetime)
                                                                            .FirstOrDefault();
                            if (events != null)
                            {
                                if (result.Value.success)
                                {
                                    if (!string.IsNullOrWhiteSpace(result.Value.id) && events.activity_id != result.Value.id)
                                    {
                                        events.activity_id = result.Value.id;
                                        events.event_modified_datetime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var crm_event_delete = db_ctx.crm_events.Where(x => x.activity_id == result.Value.id).FirstOrDefault();
                                                    crm_event_delete.event_deleted = true;
                                                    crm_event_delete.event_mark_delete = false;
                                                    crm_event_delete.event_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.ActivityExtender:
                            crm_activity_extender activity_extender = db_ctx.crm_activity_extender.Where(x => x.activity_extender_guid == current_guid
                                                                                                                    && x.activity_extender_deleted == false)
                                                                                                            .OrderByDescending(y => y.activity_extender_created_datetime)
                                                                                                            .FirstOrDefault();
                            if (activity_extender != null)
                            {
                                if (result.Value.success)
                                {
                                    if (!string.IsNullOrWhiteSpace(result.Value.id) && activity_extender.activity_extender_id != result.Value.id)
                                    {
                                        activity_extender.activity_extender_id = result.Value.id;
                                        activity_extender.activity_extender_modified_datetime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var activity_extender_delete = db_ctx.crm_activity_extender.Where(x => x.activity_extender_id == result.Value.id).FirstOrDefault();
                                                    activity_extender_delete.activity_extender_deleted = true;
                                                    activity_extender_delete.activity_extender_mark_delete = false;
                                                    activity_extender_delete.activity_extender_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.EventRegistration:
                            crm_event_registrations event_registrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_guid == current_guid
                                                                                                                        && x.event_registration_deleted == false)
                                                                                                                .OrderByDescending(y => y.event_registration_created_datetime)
                                                                                                                .FirstOrDefault();
                            if (event_registrations != null)
                            {
                                if (result.Value.success)
                                {
                                    if (!string.IsNullOrWhiteSpace(result.Value.id) && event_registrations.event_registration_id != result.Value.id)
                                    {
                                        event_registrations.event_registration_id = result.Value.id;
                                        event_registrations.event_registration_modified_datetime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var event_registration_delete = db_ctx.crm_event_registrations.Where(x => x.event_registration_id == result.Value.id).FirstOrDefault();
                                                    event_registration_delete.event_registration_deleted = true;
                                                    event_registration_delete.event_registration_mark_delete = false;
                                                    event_registration_delete.event_registration_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.Application:
                            crm_applications application = db_ctx.crm_applications.Where(x => x.application_guid == current_guid
                                                                                            && x.application_deleted == false)
                                                                                    .OrderByDescending(y => y.appl_created_date)
                                                                                    .FirstOrDefault();
                            if (application != null)
                            {
                                if (result.Value.success)
                                {
                                    if (!string.IsNullOrWhiteSpace(result.Value.id) && application.crm_application_id != result.Value.id)
                                    {
                                        application.crm_application_id = result.Value.id;
                                        application.appl_modfied_date = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var application_delete = db_ctx.crm_applications.Where(x => x.crm_application_id == result.Value.id).FirstOrDefault();
                                                    application_delete.application_deleted = true;
                                                    application_delete.appl_mark_delete = false;
                                                    application_delete.appl_modfied_date = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.ProgramEnrollment:
                            crm_program_enrollments program_enrollment = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_guid == current_guid
                                                                                                                        && x.crm_program_enrollment_deleted == false)
                                                                                                                .OrderByDescending(y => y.crm_program_enrollment_created_datetime)
                                                                                                                .FirstOrDefault();
                            if (program_enrollment != null)
                            {
                                if (result.Value.success)
                                {
                                    program_enrollment.crm_program_enrollment_id = result.Value.id;
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var program_enrollment_delete = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_id == result.Value.id).FirstOrDefault();
                                                    program_enrollment_delete.crm_program_enrollment_deleted = true;
                                                    program_enrollment_delete.crm_program_enrollment_mark_delete = false;
                                                    program_enrollment_delete.crm_program_enrollment_modified_by = Settings.CrmAdmin;
                                                    program_enrollment_delete.crm_program_enrollment_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.AcademicProgram:
                        case InsertResults.Account:
                            crm_accounts account = db_ctx.crm_accounts.Where(x => x.account_guid == current_guid
                                                                                        && x.account_deleted == false)
                                                                                .OrderByDescending(y => y.account_created_date)
                                                                                .FirstOrDefault();
                            if (account != null)
                            {
                                if (result.Value.success)
                                {
                                    account.account_id = result.Value.id;
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var account_delete = db_ctx.crm_accounts.Where(x => x.account_id == result.Value.id).FirstOrDefault();
                                                    account_delete.account_deleted = true;
                                                    account_delete.account_mark_delete = false;
                                                    account_delete.account_modified_by = Settings.CrmAdmin;
                                                    account_delete.account_modifed_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.Course:
                            crm_courses course = db_ctx.crm_courses.Where(x => x.course_guid == current_guid
                                                                                     && x.course_deleted == false)
                                                                            .OrderByDescending(y => y.course_modified_datetime)
                                                                            .FirstOrDefault();
                            if (course != null)
                            {
                                if (result.Value.success)
                                {
                                    if (course.crm_course_id != result.Value.id) {
                                        course.crm_course_id = result.Value.id;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var course_delete = db_ctx.crm_courses.Where(x => x.crm_course_id == result.Value.id).FirstOrDefault();
                                                    course_delete.course_deleted = true;
                                                    course_delete.course_mark_delete = false;
                                                    course_delete.course_modified_by = Settings.CrmAdmin;
                                                    course_delete.course_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            case StatusCode.DUPLICATE_VALUE:

                                                List<hed__Course__c> SfCourses = new List<hed__Course__c>();

                                                if (course != null && !string.IsNullOrWhiteSpace(course.sis_course_id))
                                                {

                                                    var soql = GetSoqlString(SoqlEnums.GetCourseBySisCourseId, DownloadTypes.SpecificRecordType, 1, DateTime.Now, course.sis_course_id);

                                                    using (ApiService api = new ApiService())
                                                    {
                                                        SfCourses = api.Query<hed__Course__c>(soql);
                                                    }

                                                    if (SfCourses.Any())
                                                    {

                                                        bool FirstRecord = true;
                                                        foreach (hed__Course__c SfCourse in SfCourses)
                                                        {
                                                            if (FirstRecord)
                                                            {
                                                                FirstRecord = false;

                                                                if (!string.IsNullOrWhiteSpace(SfCourse.Id))
                                                                {
                                                                    course.crm_course_id = SfCourse.Id;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                course.course_mark_delete = true;
                                                            }

                                                            course.course_modified_by = Settings.CrmAdmin;
                                                            course.course_modified_datetime = DateTime.Now;
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.CourseOffering:
                            crm_course_offerings course_offering = db_ctx.crm_course_offerings.Where(x => x.course_offering_guid == current_guid
                                                                                                                && x.course_offering_deleted == false)
                                                                                                        .OrderByDescending(y => y.course_offering_modified_datetime)
                                                                                                        .FirstOrDefault();
                            if (course_offering != null)
                            {
                                if (result.Value.success)
                                {
                                    course_offering.course_offering_id = result.Value.id;
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var course_offering_delete = db_ctx.crm_course_offerings.Where(x => x.course_offering_id == result.Value.id).FirstOrDefault();
                                                    course_offering_delete.course_offering_deleted = true;
                                                    course_offering_delete.course_offering_mark_delete = false;
                                                    course_offering_delete.course_offering_modified_by = Settings.CrmAdmin;
                                                    course_offering_delete.course_offering_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case InsertResults.CourseConnection:
                            crm_course_connections course_connection = db_ctx.crm_course_connections.Where(x => x.course_connection_guid == current_guid
                                                                                                                        && x.course_connection_deleted == false)
                                                                                                            .OrderByDescending(y => y.course_connection_modified_datetime)
                                                                                                            .FirstOrDefault();
                            if (course_connection != null)
                            {
                                if (result.Value.success)
                                {
                                    if (!string.IsNullOrWhiteSpace(result.Value.id) && course_connection.course_connection_id != result.Value.id)
                                    {
                                        course_connection.course_connection_id = result.Value.id;
                                        course_connection.course_connection_modified_datetime = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    foreach (Error error in result.Value.errors)
                                    {
                                        switch (error.statusCode)
                                        {
                                            case StatusCode.ENTITY_IS_DELETED:
                                                if (!string.IsNullOrWhiteSpace(result.Value.id))
                                                {
                                                    var course_connection_delete = db_ctx.crm_course_connections.Where(x => x.course_connection_id == result.Value.id).FirstOrDefault();
                                                    course_connection_delete.course_connection_deleted = true;
                                                    course_connection_delete.course_connection_mark_delete = false;
                                                    course_connection_delete.course_connection_modified_by = Settings.CrmAdmin;
                                                    course_connection_delete.course_connection_modified_datetime = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                ErrorMsg += "Type: " + type.ToString() + ", Key: " + result.Key + ", Uncaptured Error.  StatusCode: " + error.statusCode + ". Message: " + error.message;
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    if (!string.IsNullOrEmpty(ErrorMsg)) {
                        RecordError(GetCurrentMethod(), ErrorMsg);
                        ErrorMsg = string.Empty;
                    } 

                    db_ctx.SaveChanges();
                    success = true;
                    
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += "Type: " + type.ToString() + ", Error Message: " + ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }

        private void SaveInsertResults(List<Guid> keys, SaveResult[] insert_results, InsertResults object_name)
        {
            string ErrorMsg = string.Empty;
            try
            {

                for (int b = 0; b < insert_results.Length; b++)
                {
                    Guid current_guid = keys[b];

                    switch (object_name)
                    {
                        case InsertResults.Contact:
                            crm_contacts contact = db_ctx.crm_contacts.Where(x => x.contact_guid == current_guid
                                                                                  && x.contact_deleted == false)
                                                                         .OrderByDescending(y => y.contact_created_datetime)
                                                                         .FirstOrDefault();
                            if (contact != null)
                            {
                                if (insert_results[b].success)
                                {
                                    contact.contact_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }

                            break;
                        case InsertResults.Term:
                            crm_terms term = db_ctx.crm_terms.Where(x => x.term_guid == current_guid
                                                                            && x.term_deleted == false)
                                                                    .OrderByDescending(y => y.term_created_datetime)
                                                                    .FirstOrDefault();
                            if (term != null)
                            {
                                if (insert_results[b].success)
                                {
                                    term.term_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.Affiliation:
                            crm_affiliations affiliation = db_ctx.crm_affiliations.Where(x => x.affiliation_guid == current_guid
                                                                                          && x.affiliation_deleted == false)
                                                                                 .OrderByDescending(y => y.affiliation_created_datetime)
                                                                                 .FirstOrDefault();
                            if (affiliation != null)
                            {
                                if (insert_results[b].success)
                                {
                                    affiliation.affiliation_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);

                                    //foreach(Error in insert_results[b].errors)
                                }
                            }
                            break;
                        case InsertResults.Inquiry:
                            crm_inquiries inquiry = db_ctx.crm_inquiries.Where(x => x.inquiry_guid == current_guid
                                                                               && x.inq_deleted == false)
                                                                           .OrderByDescending(y => y.inq_created_datetime)
                                                                           .FirstOrDefault();
                            if (inquiry != null)
                            {
                                if (insert_results[b].success)
                                {
                                    inquiry.inquiry_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.Task:
                            crm_tasks task = db_ctx.crm_tasks.Where(x => x.activity_guid == current_guid
                                                                            && x.task_deleted == false)
                                                                   .OrderByDescending(y => y.task_created_datetime)
                                                                   .FirstOrDefault();
                            if (task != null)
                            {
                                if (insert_results[b].success)
                                {
                                    task.activity_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.EmailCampaign:
                            crm_campaigns campaign = db_ctx.crm_campaigns.Where(x => x.campaign_guid == current_guid
                                                                                     && x.campaign_deleted == false)
                                                                            .OrderByDescending(y => y.campaign_created_datetime)
                                                                            .FirstOrDefault();
                            if (campaign != null)
                            {
                                if (insert_results[b].success)
                                {
                                    campaign.campaign_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.EmailBroadcast:
                            crm_email_broadcasts broadcast = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_guid == current_guid
                                                                                                          && x.email_broadcast_deleted == false)
                                                                                                 .OrderByDescending(y => y.email_broadcast_created_datetime)
                                                                                                 .FirstOrDefault();
                            if (broadcast != null)
                            {
                                if (insert_results[b].success)
                                {
                                    broadcast.email_broadcast_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.EmailTemplate:
                            crm_email_templates template = db_ctx.crm_email_templates.Where(x => x.email_template_guid == current_guid
                                                                                                       && x.email_template_deleted == false)
                                                                                              .OrderByDescending(y => y.email_template_created_datetime)
                                                                                              .FirstOrDefault();
                            if (template != null)
                            {
                                if (insert_results[b].success)
                                {
                                    template.email_template_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.Event:
                            crm_events events = db_ctx.crm_events.Where(x => x.activity_guid == current_guid
                                                                                && x.event_deleted == false)
                                                                            .OrderByDescending(y => y.event_created_datetime)
                                                                            .FirstOrDefault();
                            if (events != null)
                            {
                                if (insert_results[b].success)
                                {
                                    events.activity_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.ActivityExtender:
                            crm_activity_extender activity_extender = db_ctx.crm_activity_extender.Where(x => x.activity_extender_guid == current_guid
                                                                                                                    && x.activity_extender_deleted == false)
                                                                                                            .OrderByDescending(y => y.activity_extender_created_datetime)
                                                                                                            .FirstOrDefault();
                            if (activity_extender != null)
                            {
                                if (insert_results[b].success)
                                {
                                    activity_extender.activity_extender_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.EventRegistration:
                            crm_event_registrations event_registrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_guid == current_guid
                                                                                                                        && x.event_registration_deleted == false)
                                                                                                                .OrderByDescending(y => y.event_registration_created_datetime)
                                                                                                                .FirstOrDefault();
                            if (event_registrations != null)
                            {
                                if (insert_results[b].success)
                                {
                                    event_registrations.event_registration_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.Application:
                            crm_applications application = db_ctx.crm_applications.Where(x => x.application_guid == current_guid
                                                                                                    && x.application_deleted == false)
                                                                                            .OrderByDescending(y => y.appl_created_date)
                                                                                            .FirstOrDefault();
                            if (application != null)
                            {
                                if (insert_results[b].success)
                                {
                                    application.crm_application_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.ProgramEnrollment:
                            crm_program_enrollments program_enrollment = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_guid == current_guid
                                                                                                                        && x.crm_program_enrollment_deleted == false)
                                                                                                                .OrderByDescending(y => y.crm_program_enrollment_created_datetime)
                                                                                                                .FirstOrDefault();
                            if (program_enrollment != null)
                            {
                                if (insert_results[b].success)
                                {
                                    program_enrollment.crm_program_enrollment_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.Account:
                            crm_accounts account = db_ctx.crm_accounts.Where(x => x.account_guid == current_guid
                                                                                        && x.account_deleted == false)
                                                                                .OrderByDescending(y => y.account_created_date)
                                                                                .FirstOrDefault();
                            if (account != null)
                            {
                                if (insert_results[b].success)
                                {
                                    account.account_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.Course:
                            crm_courses course = db_ctx.crm_courses.Where(x => x.course_guid == current_guid
                                                                                     && x.course_deleted == false)
                                                                            .OrderByDescending(y => y.course_modified_datetime)
                                                                            .FirstOrDefault();
                            if (course != null)
                            {
                                if (insert_results[b].success)
                                {
                                    course.crm_course_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.CourseOffering:
                            crm_course_offerings course_offering = db_ctx.crm_course_offerings.Where(x => x.course_offering_guid == current_guid
                                                                                                                && x.course_offering_deleted == false)
                                                                                                        .OrderByDescending(y => y.course_offering_modified_datetime)
                                                                                                        .FirstOrDefault();
                            if (course_offering != null)
                            {
                                if (insert_results[b].success)
                                {
                                    course_offering.course_offering_id = insert_results[b].id;
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        case InsertResults.CourseConnection:
                            crm_course_connections course_connection = db_ctx.crm_course_connections.Where(x => x.course_connection_guid == current_guid
                                                                                                                        && x.course_connection_deleted == false)
                                                                                                            .OrderByDescending(y => y.course_connection_modified_datetime)
                                                                                                            .FirstOrDefault();
                            if (course_connection != null)
                            {
                                if (insert_results[b].success)
                                {
                                    if (course_connection.course_connection_id != insert_results[b].id) {
                                        course_connection.course_connection_id = insert_results[b].id;
                                    }
                                    
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), insert_results[b].errors[0].message);
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    db_ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ", Error Message: " + ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);
        }

        private List<DeleteResult> DeleteRecords(string[] ids, Int32 batchSize = 100)
        {
            List<DeleteResult> results = new List<DeleteResult>();

            try
            {
                if (ids.Any())
                {
                    for (int i = 0; i < ids.Length; i += batchSize)
                    {
                        var items = ids.Skip(i).Take(batchSize).ToArray();

                        using (ApiService api = new ApiService())
                        {
                            results.AddRange(api.Delete(items).ToList());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return results;
        }

        private bool UpsertRecords(InsertResults type, Dictionary<string, sObject> upserts, Int32 batchSize = 200)
        {
            bool success = false;
            Dictionary<string, SaveResult> SaveResults = new Dictionary<string, SaveResult>();
            string MethodName = GetCurrentMethod();
            try
            {
                if (upserts.Any())
                {

                    if (MonitorExecution(MethodName + type.ToString(),upserts.Count)) {
                        for (int i = 0; i < upserts.Count; i += batchSize)
                        {
                            var items = upserts.Skip(i).Take(batchSize).ToDictionary(p => p.Key, p => p.Value);

                            using (ApiService api = new ApiService())
                            {
                                SaveResults = api.Upsert(items);
                            }

                            ProcessUpsertResults(type, SaveResults);
                        }
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return success;
        }

        private List<SaveResult> UpdateRecords(List<sObject> updates, Int32 batchSize = 10)
        {
            List<SaveResult> results = new List<SaveResult>();

            try
            {
                if (updates.Any())
                {
                    for (int i = 0; i < updates.Count; i += batchSize)
                    {
                        var items = updates.Skip(i).Take(batchSize);

                        using (ApiService api = new ApiService())
                        {
                            results.AddRange(api.Update(items.ToArray()).ToList());
                        }

                        for (int b = 0; b < results.Count; b += 1)
                        {
                            if (results[b] != null)
                            {
                                if (!results[b].success)
                                {
                                    for (int c = 0; c < results[b].errors.Length; c += 1)
                                    {
                                        if (!string.IsNullOrEmpty(updates[b].Id)) {
                                            results[b].id = updates[b].Id;
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
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return results;
        }

        private bool InsertRecords(List<sObject> inserts, List<Guid> guids, InsertResults type, Int32 batchSize = 100)
        {
            bool success = false;
            SaveResult[] saveResults;

            try
            {
                if (inserts.Any())
                {
                    for (int i = 0; i < inserts.Count; i += batchSize)
                    {
                        var items = inserts.Skip(i).Take(batchSize).ToArray();
                        var keys = guids.Skip(i).Take(batchSize).ToList();

                        using (ApiService api = new ApiService())
                        {
                            saveResults = api.Insert(items);
                        }

                        SaveInsertResults(keys, saveResults, type);
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }
            return success;
        }

        private string GetDynamicContent(string matching_id, bool html)
        {
            string dynamic_content = string.Empty;
            string ErrorMsg = string.Empty;
            try
            {
                if (html)
                {
                    dynamic_content = db_ctx.dynamic_content.Where(x => x.dynamic_content_matching_id == matching_id).Select(y => y.dynamic_content_html).FirstOrDefault();
                }
                else
                {
                    dynamic_content = db_ctx.dynamic_content.Where(x => x.dynamic_content_matching_id == matching_id).Select(y => y.dynamic_content_text).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return dynamic_content;
        }

        private string GetNewDynamicContent(string matching_text)
        {
            string dynamic_content = string.Empty;
            string ErrorMsg = string.Empty;
            try
            {
                    dynamic_content = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_matching_text == matching_text)
                                                                .Select(y => y.crm_dynamic_content_html)
                                                                .FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return dynamic_content;
        }

        private string GetProgramDynamicContent(string program_id)
        {
            string ParentContent = string.Empty;
            string ChildContent = string.Empty;
            string ParentReplacementText = string.Empty;
            string ReturnContent = string.Empty;
            string ErrorMsg = string.Empty;
            try
            {

                if (!string.IsNullOrWhiteSpace(program_id)) {
                    string ProgCode = db_ctx.crm_accounts.Where(x => x.account_deleted == false
                                                            && x.account_mark_delete == false
                                                            && x.account_record_type_id == AccountRecordTypes.AcademicProgram
                                                            && x.account_active == true
                                                            && x.account_id == program_id)
                                                       .OrderByDescending(y => y.account_modifed_datetime)
                                                       .Select(z => z.account_number)
                                                       .FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(ProgCode)) {

                        // grab parent EXTN merge first
                        if (ProgCode.Contains("EXTN"))
                        {
                            ParentReplacementText = "&lt;&lt;&lt;DC_RCR_IL_EXTN_DYNAMIC&gt;&gt;&gt;";
                            var content = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_matching_text == "EXTN")
                                                                         .Select(y => y.crm_dynamic_content_html)
                                                                         .FirstOrDefault();

                            if (!string.IsNullOrWhiteSpace(content)) {
                                ParentContent = CleanDynamicCodes(content.Trim());
                            }
                        }


                        // grab parent BUS merge first
                        if (ProgCode.Contains("BUS"))
                        {
                            ParentReplacementText = "&lt;&lt;&lt;DC_RCR_IR_IL_BUS_DYNAMIC&gt;&gt;&gt;";
                            var content = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_matching_text == "BUS")
                                                                         .Select(y => y.crm_dynamic_content_html)
                                                                         .FirstOrDefault();

                            if (!string.IsNullOrWhiteSpace(content)) {
                                ParentContent = CleanDynamicCodes(content.Trim());
                            }
                        }

                        var DynamicRecord = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_matching_text.Contains(ProgCode)).FirstOrDefault();

                        if (DynamicRecord != null && !string.IsNullOrEmpty(DynamicRecord.crm_dynamic_content_html))
                        {
                            ChildContent = DynamicRecord.crm_dynamic_content_html.Trim();
                        }

                        if (!string.IsNullOrWhiteSpace(ParentContent))
                        {
                            ReturnContent = ParentContent.Replace(ParentReplacementText, ChildContent) ?? ChildContent;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(ChildContent)) {
                                ReturnContent = ChildContent;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(ReturnContent.Trim())) {
                            ReturnContent += "<p style='color: white; '>" + ProgCode ?? program_id ?? "NotFound" + "</p>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return ReturnContent;
        }

        private string GetServicesDynamicContent(string Services)
        {
            string ParentContent = string.Empty;
            string ChildContent = string.Empty;
            string ReturnContent = string.Empty;
            string ErrorMsg = string.Empty;
            try
            {
                ParentContent = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_matching_text == "DC_RCR_IR_SVC")
                                                        .Select(y => y.crm_dynamic_content_html)
                                                        .FirstOrDefault().Trim();

                foreach (string ServiceInterestCode in Services.Split(';').Distinct().ToList())
                {
                    ChildContent += GetNewDynamicContent(ServiceInterestCode);
                    ChildContent += Environment.NewLine;
                }

                ReturnContent = ParentContent.Replace("&lt;&lt;&lt; DC_RCR_IR_SVC_DYNAMIC &gt;&gt;&gt;", ChildContent);

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return ReturnContent;
        }

        private string GetPrimaryProgramName(string account_id)
        {
            string program_name = string.Empty;
            string ErrorMsg = string.Empty;
            try
            {
                program_name = db_ctx.crm_accounts.Where(x => x.account_id == account_id).FirstOrDefault().account_name;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return program_name;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool GenerateContactIdBarCodes(int? batchSize = null)
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            bool assembly_licensed = false;
            int batch_size = batchSize ?? 100;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            DateTime since_date = DateTime.Today.AddMonths(-2);


            string local_temp_path = Path.GetTempPath();
            string remote_path = "ftp://lethbridgecollege.ca/externalapps/crm/";
            string LicenseKey = WebConfigurationManager.AppSettings["IronBarcode.LicenseKey"];

            try
            {
                assembly_licensed = IronBarCode.License.IsValidLicense(LicenseKey);

                if (assembly_licensed) {

                    DateTime? LastRunDate = GetLastRunDateTime(MethodName);

                    var contacts = db_ctx.crm_contacts.Where(x => x.contact_id != null
                                                               && x.contact_deleted == false
                                                               && x.contact_mark_delete == false
                                                               && (x.contact_id_barcode_uploaded == null))
                                                      .ToList();

                    if (contacts.Any())
                    {
                        total = contacts.Count();

                        for (int i = 0; i < total; i += batch_size)
                        {
                            var batch = contacts.OrderBy(x => x.contact_last_modified_datetime).Skip(i).Take(batch_size).ToList();

                            foreach (crm_contacts c in batch)
                            {
                                string contact_id = c.contact_id;

                                string local_file_name = contact_id + ".jpg";
                                string local_full_path = local_temp_path + local_file_name;
                                string remote_full_path = remote_path + local_file_name;

                                try
                                {
                                    var barCode = IronBarCode.BarcodeWriter.CreateBarcode(contact_id, BarcodeWriterEncoding.Code128);
                                    barCode.SetMargins(5, 5, 5, 5);
                                    //barCode.AddAnnotationTextBelowBarcode(contact_id);
                                    barCode.ResizeTo(240, 60);

                                    barCode.SaveAsJpeg(local_full_path);

                                    FileInfo fileInf = new FileInfo(local_full_path);

                                    if (UploadBarcode(local_file_name, "barcode"))
                                    {
                                        c.contact_id_barcode_uploaded = DateTime.Now;
                                        fileInf.Delete();
                                        inserted++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorMsg += ex.ToString();
                                }
                            }

                            db_ctx.SaveChanges();
                        }
                    }
                    success = true;
                }

                MethodName = GetCurrentMethod();
                
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool UploadBarcode(string filename, string folder)
        {
            string ErrorMsg = string.Empty;
            bool success = false;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // TODO: move much of this to settings and add to LC_Utility
            string ftpUsername = WebConfigurationManager.AppSettings["FtpUsername"];
            string ftpPassword = WebConfigurationManager.AppSettings["FtpPassword"];

            string local_temp_path = Path.GetTempPath();

            string local_full_path = local_temp_path + filename;
            string remote_path = "lethbridgecollege.ca/" + folder + "/";
            //string remote_full_path = remote_path + filename;

            FileInfo fileInf = new FileInfo(local_full_path);
            string uri = "ftp://" + remote_path + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            //reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            try
            {
                // Notify the server about the size of the uploaded file
                reqFTP.ContentLength = fileInf.Length;

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                FileStream fs = fileInf.OpenRead();

                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();

                success = true;
            }
            catch (WebException e)
            {
                ErrorMsg = ((FtpWebResponse)e.Response).StatusDescription;
            }

            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                RecordError(GetCurrentMethod(), ErrorMsg);
            }

            return success;
        }

        public bool VerifyEnvironment(string sfcs, string dbcs)
        {
            bool success = false;
            string ErrorMsg = string.Empty;
            try
            {
                using (EntityConnection conn = new EntityConnection(dbcs))
                {

                    string catalog = conn.StoreConnection.Database;

                    DbConnectionStringBuilder sfConnectionStringBuilder = new DbConnectionStringBuilder
                    {
                        ConnectionString = sfcs
                    };
                    string username = (string)sfConnectionStringBuilder["Username"];

                    switch (username)
                    {
                        case "crmadmin@lethbridgecollege.ca":
                            success = (catalog == "crmdb_prod");
                            break;
                        case "crmadmin@lethbridgecollege.ca.test":
                            success = (catalog == "crmdb_test");
                            break;
                        case "crmadmin@lethbridgecollege.ca.dev":
                            success = (catalog == "crmdb_dev");
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) 
            {
                RecordError(GetCurrentMethod(), ErrorMsg);
            }

            return success;
        }

        private bool RecordTransaction(string methodName, DateTime startTime, int recordsTouched, int recordsInserted, int recordsUpdated, string error)
        {
            bool success = true;
            string transactionMessage = "Transaction Successful";
            transaction_logs log = new transaction_logs();

            try
            {

                if (string.IsNullOrEmpty(error)) {
                    log.transaction_id = Guid.NewGuid();
                    log.transaction_name = methodName;
                    log.last_executed = DateTime.Now;
                    log.transaction_notes = transactionMessage;
                    log.records_total = recordsTouched;
                    log.records_updated = recordsUpdated;
                    log.records_inserted = recordsInserted;
                    log.transaction_successful = success;
                    log.run_time = Math.Round(((TimeSpan)(DateTime.Now - startTime)).TotalSeconds, 2);

                    db_ctx.transaction_logs.Add(log);
                    db_ctx.SaveChanges();
                }

                success = true;
            }
            catch (Exception ex) { error = "Error 1 of 2: " + error + ", Error 2 of 2: " + ex.ToString(); }

            if (!success || !string.IsNullOrEmpty(error)) {
                RecordError(methodName, error);
            }

            return success;
        }

        private bool RecordError(string type, string ErrorMsg)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrEmpty(type) || !string.IsNullOrEmpty(ErrorMsg))
                {
                    transaction_errors error = new transaction_errors
                    {
                        transaction_error_id = Guid.NewGuid(),
                        transaction_type = type,
                        transaction_error_datetime = DateTime.Now,
                        transaction_error_message = ErrorMsg
                    };

                    db_ctx.transaction_errors.Add(error);
                    db_ctx.SaveChanges();

                    //SendEmail sendEmail = new SendEmail()
                    //{
                    //    Body = "<html><head><title>A CRM Integration Error has Occurred</title></head><body><table><tr><td>",
                    //    Title = "A CRM Integration Error has Occurred",
                    //    To = "patrick.dudley@lethbridgecollege.ca",
                    //    From = "patrick.dudley@lethbridgecollege.ca",
                    //};

                    //sendEmail.Body += "<br /><br />";
                    //sendEmail.Body += "<label>transaction_error_datetime: " + error.transaction_error_datetime + "</label><br />";
                    //sendEmail.Body += "<label>transaction_error_id: " + error.transaction_error_id + "</label><br />";
                    //sendEmail.Body += "<label>transaction_error_message: " + error.transaction_error_message + "</label><br />";
                    //sendEmail.Body += "<br /><br />";
                    //sendEmail.Body += "</td></tr></table></body></html>";

                    //sendEmail.Send();

                    //success = true;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        private DateTime GetLastRunDateTime(string methodName)
        {
            string ErrorMsg = string.Empty;
            DateTime LastRunTime = Settings.GlobalLastRunTime;

            try
            {
                if (!string.IsNullOrEmpty(methodName))
                {

                    if (Settings.EnableGlobalLastRun)
                    {
                        LastRunTime = Settings.GlobalLastRunTime;
                    }
                    else {
                        LastRunTime = db_ctx.transaction_logs.Where(x => x.transaction_name == methodName
                                                                      && x.transaction_successful == true)
                                                             .OrderByDescending(x => x.last_executed)
                                                             .Select(y => y.last_executed)
                                                             .FirstOrDefault() ?? LastRunTime;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }


            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(methodName, ErrorMsg);

            return LastRunTime;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            string ErrorMsg = string.Empty;
            string methodName = string.Empty;

            try
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                methodName = sf.GetMethod().Name;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return methodName;
        }

        private DateTime GetLocalTime(DateTime? dt)
        {
            DateTime return_datetime = DateTime.Now;
            string ErrorMsg = string.Empty;
            try
            {
                return_datetime = (dt ?? DateTime.Now).ToLocalTime();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return return_datetime;
        }

        private DateTime GetUTCTime(DateTime? dt)
        {
            DateTime return_datetime = DateTime.Now;
            string ErrorMsg = string.Empty;
            try
            {
                return_datetime = (dt ?? DateTime.Now).ToUniversalTime();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return return_datetime;
        }

        private string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;
            return me.Member.Name;
        }

        private bool SaveInsertResult(Guid key, string value, InsertResults object_name)
        {
            string ErrorMsg = string.Empty;
            bool success = false;

            try
            {
                switch (object_name)
                {
                    case InsertResults.Contact:
                        crm_contacts c = db_ctx.crm_contacts.Where(x => x.contact_guid == key && x.contact_deleted == false).OrderByDescending(y => y.contact_created_datetime).FirstOrDefault();
                        c.contact_id = value;
                        break;
                    case InsertResults.Term:
                        crm_terms t = db_ctx.crm_terms.Where(x => x.term_guid == key && x.term_deleted == false).OrderByDescending(y => y.term_created_datetime).FirstOrDefault();
                        t.term_id = value;
                        break;
                    case InsertResults.Affiliation:
                        crm_affiliations a = db_ctx.crm_affiliations.Where(x => x.affiliation_guid == key && x.affiliation_deleted == false).OrderByDescending(y => y.affiliation_created_datetime).FirstOrDefault();
                        a.affiliation_id = value;
                        break;
                    case InsertResults.Inquiry:
                        crm_inquiries i = db_ctx.crm_inquiries.Where(x => x.inquiry_guid == key && x.inq_deleted == false).OrderByDescending(y => y.inq_created_datetime).FirstOrDefault();
                        i.inquiry_id = value;
                        break;
                    case InsertResults.Task:
                        crm_tasks s = db_ctx.crm_tasks.Where(x => x.activity_guid == key && x.task_deleted == false).OrderByDescending(y => y.task_created_datetime).FirstOrDefault();
                        s.activity_id = value;
                        break;
                    case InsertResults.EmailCampaign:
                        crm_campaigns ca = db_ctx.crm_campaigns.Where(x => x.campaign_guid == key && x.campaign_deleted == false).OrderByDescending(y => y.campaign_created_datetime).FirstOrDefault();
                        ca.campaign_id = value;
                        break;
                    case InsertResults.EmailBroadcast:
                        crm_email_broadcasts b = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_guid == key && x.email_broadcast_deleted == false).OrderByDescending(y => y.email_broadcast_created_datetime).FirstOrDefault();
                        b.email_broadcast_id = value;
                        break;
                    case InsertResults.EmailTemplate:
                        crm_email_templates te = db_ctx.crm_email_templates.Where(x => x.email_template_guid == key && x.email_template_deleted == false).OrderByDescending(y => y.email_template_created_datetime).FirstOrDefault();
                        te.email_template_id = value;
                        break;
                    case InsertResults.Event:
                        crm_events e = db_ctx.crm_events.Where(x => x.activity_guid == key && x.event_deleted == false).OrderByDescending(y => y.event_created_datetime).FirstOrDefault();
                        e.activity_id = value;
                        break;
                    case InsertResults.ActivityExtender:
                        crm_activity_extender ae = db_ctx.crm_activity_extender.Where(x => x.activity_extender_guid == key && x.activity_extender_deleted == false).OrderByDescending(y => y.activity_extender_created_datetime).FirstOrDefault();
                        ae.activity_extender_id = value;
                        break;
                    case InsertResults.EventRegistration:
                        crm_event_registrations er = db_ctx.crm_event_registrations.Where(x => x.event_registration_guid == key && x.event_registration_deleted == false).OrderByDescending(y => y.event_registration_created_datetime).FirstOrDefault();
                        er.event_registration_id = value;
                        break;
                    case InsertResults.Application:
                        crm_applications app = db_ctx.crm_applications.Where(x => x.application_guid == key).OrderByDescending(y => y.appl_created_date).FirstOrDefault();
                        app.crm_application_id = value;
                        break;
                    case InsertResults.ProgramEnrollment:
                        crm_program_enrollments pe = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_guid == key).OrderByDescending(y => y.crm_program_enrollment_created_datetime).FirstOrDefault();
                        pe.crm_program_enrollment_id = value;
                        break;
                    case InsertResults.Account:
                        crm_accounts acc = db_ctx.crm_accounts.Where(x => x.account_guid == key && x.account_deleted == false).OrderByDescending(y => y.account_created_date).FirstOrDefault();
                        acc.account_id = value;
                        break;
                    case InsertResults.Course:
                        crm_courses cr = db_ctx.crm_courses.Where(x => x.course_guid == key && x.course_deleted == false).OrderByDescending(y => y.course_modified_datetime).FirstOrDefault();
                        cr.crm_course_id = value;
                        break;
                    case InsertResults.CourseOffering:
                        crm_course_offerings co = db_ctx.crm_course_offerings.Where(x => x.course_offering_guid == key && x.course_offering_deleted == false).OrderByDescending(y => y.course_offering_modified_datetime).FirstOrDefault();
                        co.course_offering_id = value;
                        break;
                    default:
                        break;
                }
                db_ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }


            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }

        private List<Record> GetReportRecipients(string reportId)
        {
            List<Record> recipients = new List<Record>();

            try
            {
                var client = new CrmCore.SalesforceClient();

                string clientId = WebConfigurationManager.AppSettings["SFClientId"];
                string clientSecret = WebConfigurationManager.AppSettings["SFClientSecret"];
                string username = WebConfigurationManager.AppSettings["SFClientUsername"];
                string password = WebConfigurationManager.AppSettings["SFClientPassword"];
                string analyticApiUrl = WebConfigurationManager.AppSettings["SFReportingApiUrl"];

                var authFlow = new CrmCore.Security.UsernamePasswordAuthenticationFlow(clientId, clientSecret, username, password);

                client.Authenticate(authFlow);

                var reportContactIds = client.GetContactIdsByReportId<List<string>>(analyticApiUrl, reportId, client.GetContactIdIndex<int>(analyticApiUrl, reportId));

                recipients = db_ctx.crm_contacts.Where(x => x.contact_email_opt_out == false && x.contact_is_email_bounced == false && reportContactIds.Contains(x.contact_id)).Select(y => new Record() { PreferredEmail = y.contact_email, AlternativeEmail = y.contact_alternate_email, CollegeEmail = y.contact_college_email, FirstName = y.contact_first_name, ContactId = y.contact_id, StudentId = y.contact_colleague_id, AccountId = y.contact_account_id }).ToList<Record>();

            }
            catch (Exception ex)
            {
                string errmsg = ex.ToString();
            }

            return recipients;
        }

        private TimeSpan GetUtcTimeSpan(TimeSpan ts)
        {
            string ErrorMsg = string.Empty;
            TimeSpan retVal = new TimeSpan();

            try
            {
                DateTime dt = new DateTime(ts.Ticks).AddDays(1);
                DateTime dtUtc = dt.ToUniversalTime();
                retVal = dtUtc.TimeOfDay;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return retVal;
        }

        private TimeSpan GetLocalTimeSpan(TimeSpan tsUtc)
        {
            string ErrorMsg = string.Empty;
            TimeSpan retVal = new TimeSpan();

            try
            {
                DateTime dtUtc = new DateTime(tsUtc.Ticks).AddDays(1);
                DateTime dt = dtUtc.ToLocalTime();
                retVal = dt.TimeOfDay;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return retVal;
        }

        private bool ReverseTest(bool testbool) {
            return !testbool;
        }

        private bool UpsertRecords(Dictionary<string, IEnumerable> dic)
        {
            // unused input parameter was giving errors
            // Guid[] keys (removed)
            bool success = false;
            string ErrorMsg = string.Empty;
            SaveResult[] result;
            //Dictionary<Guid, string> insert_results = new Dictionary<Guid, string>();

            try
            {
                if (dic.Any())
                {
                    using (ApiService api = new ApiService())
                    {
                        foreach (KeyValuePair<string, IEnumerable> item in dic)
                        {

                            sObject[] array = item.Value.Cast<sObject>().ToArray();

                            if (item.Key == "update")
                            {
                                result = api.Update(array);
                            }
                            else
                            {
                                //insert_results = api.Insert(array, keys);
                            }
                        }
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }

        private void AddCblValue(ref string cbl, string val)
        {
            string retval = string.Empty;
            string ErrorMsg = string.Empty;
            List<string> possible_values = new List<string>
            {
                "Prospect",
                "Applicant",
                "Student",
                "Alumni",
                "Constituent"
            };

            try
            {
                List<string> cblist = new List<string>();
                if (!string.IsNullOrEmpty(cbl))
                {
                    cblist = cbl.Split(';').ToList();
                    //cblist.RemoveAll(x => !possible_values.Contains(x));
                }

                if (!cblist.Contains(val)) { cblist.Add(val); }

                cbl = String.Join(";", cblist.Select(x => x.ToString()).ToArray());
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        private void RemoveCblValue(ref string cbl, string val)
        {
            string retval = string.Empty;
            string ErrorMsg = string.Empty;

            try
            {
                List<string> cblist = new List<string>();
                if (!string.IsNullOrEmpty(cbl))
                {
                    cblist = cbl.Split(';').ToList();
                }

                if (cblist.Contains(val))
                {
                    cblist.Remove(val);
                }

                cbl = String.Join(";", cblist.Select(x => x.ToString()).ToArray());
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        private bool UpdateContactTimestamps()
        {
            int InsertedCount = 0;
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string soqlstr = string.Empty;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<Contact> SfContacts = new List<Contact>();
            List<Contact> process_contacts = new List<Contact>();
            List<string> str_contacts = new List<string>();
            List<crm_contacts> DbContacts = new List<crm_contacts>();

            try
            {
                MethodName = GetCurrentMethod();
                //soqlstr = GetSoqlString(Enumerations.SoqlEnums.DownloadContacts);
                using (ApiService api = new ApiService())
                {
                    SfContacts = api.QueryAll<Contact>(soqlstr);
                }

                foreach (Contact SfContact in SfContacts)
                {
                    DateTime mod_date = GetLocalTime(SfContact.LastModifiedDate);

                    var DbContact = db_ctx.crm_contacts.Where(x => x.contact_id == SfContact.Id
                                                                && x.contact_last_modified_datetime != mod_date).OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

                    if (DbContact != null)
                    {
                        DbContact.contact_last_modified_datetime = GetLocalTime(SfContact.LastModifiedDate);
                        UpdatedCount++;
                    }

                    TotalCount++;
                }

                db_ctx.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool FixDynamicContentData()
        {
            bool success = false;
            string ErrorMsg = string.Empty;
            try
            {
                List<dynamic_content> dcs = new List<dynamic_content>();
                List<crm_accounts> accounts = new List<crm_accounts>();

                dcs = db_ctx.dynamic_content.Where(x => x.dynamic_content_label == "PROGRAMS").ToList();

                accounts = db_ctx.crm_accounts.ToList();

                foreach (dynamic_content dc in dcs)
                {

                    //var test = accounts.Where(x => x.account_number == dc.dynamic_content_name).FirstOrDefault();

                    dc.dynamic_content_matching_id = accounts.Where(x => x.account_number == dc.dynamic_content_name).FirstOrDefault().account_id;

                    //foreach (account account in accounts) {
                    //    if (account.account_number == dc.dynamic_content_name) {
                    //        string hitme = "here";
                    //    }
                    //}

                    //if (test != null)
                    //{
                    //    if (!string.IsNullOrEmpty(test.account_id))
                    //    {
                    //        dc.dynamic_content_matching_id = test.account_id;
                    //    }
                    //}
                }

                db_ctx.SaveChanges();

                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }

        private bool FixInquiryContactIds()
        {
            bool success = false;
            string ErrorMsg = string.Empty;

            List<crm_inquiries> inquiries = new List<crm_inquiries>();
            List<crm_contacts> contacts = new List<crm_contacts>();

            try
            {

                inquiries = db_ctx.crm_inquiries.Where(x => x.inq_contact_id == null
                                                        && x.inq_deleted == false
                                                        && x.inq_contact_legacy_id != null).ToList();

                //contacts = db_ctx.contacts.Where(y=> inquiries.)

                foreach (crm_inquiries i in inquiries)
                {
                    i.inq_contact_id = contacts.Where(z => z.contact_legacy_id == i.inq_contact_legacy_id).FirstOrDefault().contact_id;

                    if (!string.IsNullOrEmpty(i.inq_contact_id))
                    {
                        i.inq_modified_datetime = DateTime.Now;

                        db_ctx.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return success;
        }
    }
}



//private List<string> GetFtpFileList(string folder)
//{
//    string ErrorMsg = string.Empty;
//    List<string> files = new List<string>();

//    // TODO: move much of this to settings and add to LC_Utility
//    string ftpUsername = "patrick_dudley";
//    string ftpPassword = "pk59p92XHt";

//    string remote_path = "ftp://lethbridgecollege.ca/" + folder + "/";

//    try
//    {
//        //Create FTP request
//        FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(remote_path);

//        request.Method = WebRequestMethods.Ftp.ListDirectory;
//        request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
//        //reqFTP.KeepAlive = false;
//        request.UseBinary = true;

//        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
//        Stream responseStream = response.GetResponseStream();
//        StreamReader reader = new StreamReader(responseStream);



//        while (!reader.EndOfStream)
//        {
//            files.Add(reader.ReadLine());
//        }

//        //Clean-up
//        reader.Close();
//        //responseStream.Close();
//        response.Close();
//    }
//    catch (WebException e)
//    {
//        ErrorMsg = ((FtpWebResponse)e.Response).StatusDescription;
//    }

//    RecordError(GetCurrentMethod(), ErrorMsg);

//    return files;
//}

//private bool GenerateBarCodes(string studentId = null, int? batchSize = null)
//{
//    int inserted = 0;
//    int updated = 0;
//    int total = 0;

//    bool success = false;
//    int batch_size = batchSize ?? 10;
//    string ErrorMsg = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;


//    string local_temp_path = Path.GetTempPath();
//    string remote_path = "ftp://lethbridgecollege.ca/externalapps/crm/";

//    try
//    {
//        MethodName = GetCurrentMethod();
//        DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

//        //List<string> uploaded_codes = GetFtpFileList("barcode");

//        //var contacts = db_ctx.crm_contacts.Where(x => x.contact_colleague_id != null
//        //                                          && !uploaded_codes.Contains(x.contact_colleague_id));

//        var contacts = db_ctx.crm_contacts.Where(x => x.contact_colleague_id != null);

//        if (!string.IsNullOrEmpty(studentId))
//        {
//            contacts = contacts.Where(x => x.contact_colleague_id == studentId);
//        }
//        else
//        {
//            contacts = contacts.Where(x => x.contact_barcode_uploaded == null);
//        }

//        if (contacts.Any())
//        {

//            total = contacts.Count();

//            for (int i = 0; i < total; i += batch_size)
//            {
//                var batch = contacts.OrderBy(x => x.contact_last_modified_datetime).Skip(i).Take(batch_size);

//                foreach (crm_contacts c in batch)
//                {
//                    string student_id = c.contact_colleague_id;

//                    string local_file_name = student_id + ".jpg";
//                    string local_full_path = local_temp_path + local_file_name;
//                    string remote_full_path = remote_path + local_file_name;

//                    try
//                    {
//                        var barCode = IronBarCode.BarcodeWriter.CreateBarcode(student_id, BarcodeWriterEncoding.Code128);
//                        barCode.SetMargins(10, 50, 10, 50);
//                        //barCode.AddAnnotationTextBelowBarcode(student_id);
//                        barCode.ResizeTo(200, 50);

//                        barCode.SaveAsJpeg(local_full_path);

//                        FileInfo fileInf = new FileInfo(local_full_path);

//                        if (Upload(local_file_name, "barcode"))
//                        {
//                            fileInf.Delete();
//                            inserted++;
//                        }

//                        c.contact_barcode_uploaded = DateTime.Now;
//                    }
//                    catch (Exception ex)
//                    {
//                        ErrorMsg += ex.ToString();
//                    }
//                }
//                db_ctx.SaveChanges();
//            }
//        }

//        success = true;
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg += ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

//    return success;
//}

