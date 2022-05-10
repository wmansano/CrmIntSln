using CrmLcLib;
using CrmSfApi;
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
        private bool DownloadSalesforce(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            string SoqlStr = string.Empty;
            Result SingleResult = new Result();
            Result AllResults = new Result();
            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            try
            {
                MethodName = GetCurrentMethod();

                if (MaxRecordCount > 0)
                {

                    // Mixed Centric
                    TotalResults(AllResults, DownloadAccounts(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadContacts(BackLoad, BackLoadPriorDate, MaxRecordCount));

                    // Salesforce Centric
                    TotalResults(AllResults, DownloadReports(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadTasks(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadDynamicContent(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadInquiryPrograms(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadEvents(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadActivityExtenders(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadRoleValues(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadEventRegistrations(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadEmailBroadcasts(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadEmailCampaigns(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadEmailTemplates(BackLoad, BackLoadPriorDate, MaxRecordCount));

                    // Affiliations
                    TotalResults(AllResults, DownloadAffiliations(BackLoad, BackLoadPriorDate, MaxRecordCount));

                    // Colleague Centric
                    TotalResults(AllResults, DownloadTerms(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadAcademicPrograms(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadCourses(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadCourseOfferings(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadCourseConnections(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadInquiries(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    TotalResults(AllResults, DownloadApplications(BackLoad, BackLoadPriorDate, MaxRecordCount));
                    //TotalResults(AllResults, DownloadProgramEnrollments(BackLoad, BackLoadPriorDate, MaxRecordCount));

                    
                }

                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            //RecordTransaction(MethodName, StartTime, AllResults.TotalCount, AllResults.InsertedCount, AllResults.UpdatedCount, ErrorMsg);

            return success;

        }


        #region Salesforce Centric

        private Result DownloadActivityExtenders(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_activity_extender__c> SfActivityExtenders = new List<lc_activity_extender__c>();

            try
            {

                MethodName = LastModified.ActivityExtender;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.ActivityExtender);

                soql = GetSoqlString(SoqlEnums.DownloadActivityExtenders, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfActivityExtenders = api.Query<lc_activity_extender__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfActivityExtenders.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadActivityExtenders, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfActivityExtenders.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfActivityExtenders.AddRange(api.Query<lc_activity_extender__c>(soql));
                        }
                    }
                }

                if (SfActivityExtenders.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfActivityExtenders.Count))
                    {
                        result.TotalCount = SfActivityExtenders.Count;
                        foreach (lc_activity_extender__c SfActivityExtender in SfActivityExtenders)
                        {
                            result = SaveActivityExtender(result, SfActivityExtender);
                        }
                        if (result.UpdatedCount > result.TotalCount)
                        {
                            result.Success = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadEmailCampaigns(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_email_campaign__c> SfEmailCampaigns = new List<lc_email_campaign__c>();

            try
            {

                MethodName = LastModified.EmailCampaign;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.EmailCampaign);
                //DateTime LastRunDate = DateTime.Now.AddDays(-1);

                soql = GetSoqlString(SoqlEnums.DownloadEmailCampaigns, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfEmailCampaigns = api.Query<lc_email_campaign__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfEmailCampaigns.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadEmailCampaigns, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfEmailCampaigns.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfEmailCampaigns.AddRange(api.Query<lc_email_campaign__c>(soql));
                        }
                    }
                }

                if (SfEmailCampaigns.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfEmailCampaigns.Count))
                    {
                        foreach (lc_email_campaign__c SfEmailCampaign in SfEmailCampaigns)
                        {
                            result = SaveEmailCampaign(result, SfEmailCampaign);
                            result.TotalCount++;
                        }
                    }
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadEmailBroadcasts(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_email_broadcast__c> sf_email_broadcasts = new List<lc_email_broadcast__c>();

            try
            {
                MethodName = LastModified.EmailBroadcast;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.EmailBroadcast);
                //DateTime LastRunDate = DateTime.Now.AddDays(-1);
                
                soql = GetSoqlString(SoqlEnums.DownloadEmailBroadcasts, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    sf_email_broadcasts = api.Query<lc_email_broadcast__c>(soql);
                }

                if (BackLoad)
                {
                    if (sf_email_broadcasts.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadEmailCampaigns, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - sf_email_broadcasts.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            sf_email_broadcasts.AddRange(api.Query<lc_email_broadcast__c>(soql));
                        }
                    }
                }

                if (sf_email_broadcasts.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), sf_email_broadcasts.Count))
                    {
                        foreach (lc_email_broadcast__c sf_email_broadcast in sf_email_broadcasts)
                        {
                            result = SaveEmailBroadcast(result, sf_email_broadcast);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadEmailTemplates(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<EmailTemplate> SfEmailTemplates = new List<EmailTemplate>();

            try
            {

                MethodName = LastModified.EmailTemplate;

                //DateTime LastRunDate = DateTime.Now.AddDays(-1);
                DateTime LastRunDate = GetLastRunDateTime(LastModified.EmailTemplate);

                soql = GetSoqlString(SoqlEnums.DownloadEmailTemplates, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfEmailTemplates = api.Query<EmailTemplate>(soql);
                }

                if (BackLoad)
                {
                    if (SfEmailTemplates.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadEmailTemplates, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfEmailTemplates.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfEmailTemplates.AddRange(api.Query<EmailTemplate>(soql));
                        }
                    }
                }

                if (SfEmailTemplates.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfEmailTemplates.Count))
                    {
                        result.TotalCount = SfEmailTemplates.Count;
                        foreach (EmailTemplate SfEmailTemplate in SfEmailTemplates)
                        {
                            result = SaveEmailTemplate(result, SfEmailTemplate);
                        }

                        if (result.UpdatedCount > result.TotalCount) {
                            result.Success = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadEvents(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<Event> SfEvents = new List<Event>();

            try
            {

                MethodName = LastModified.Event;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Event);

                //DateTime LastRunDate = DateTime.Now.AddDays(-30);

                soql = GetSoqlString(SoqlEnums.DownloadEvents, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfEvents = api.Query<Event>(soql);
                }

                if (BackLoad)
                {
                    if (SfEvents.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadEvents, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfEvents.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfEvents.AddRange(api.Query<Event>(soql));
                        }
                    }
                }

                if (SfEvents.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfEvents.Count))
                    {
                        foreach (Event SfEvent in SfEvents)
                        {
                            result = SaveEvent(result, SfEvent);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadEventRegistrations(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_event_registration__c> SfEvent_registrations = new List<lc_event_registration__c>();

            try
            {

                MethodName = LastModified.EventRegistration;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.EventRegistration);

                soql = GetSoqlString(SoqlEnums.DownloadEventRegistrations, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfEvent_registrations = api.Query<lc_event_registration__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfEvent_registrations.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadEventRegistrations, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfEvent_registrations.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfEvent_registrations.AddRange(api.Query<lc_event_registration__c>(soql));
                        }
                    }
                }

                if (SfEvent_registrations.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfEvent_registrations.Count))
                    {
                        foreach (lc_event_registration__c SfEvent_registration in SfEvent_registrations)
                        {
                            result = SaveEventRegistration(result, SfEvent_registration);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadReports(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<Report> SfReports = new List<Report>();

            try
            {

                MethodName = LastModified.Report;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Report);
                //DateTime LastRunDate = DateTime.Now.AddDays(-1);

                soql = GetSoqlString(SoqlEnums.DownloadReports, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfReports = api.Query<Report>(soql);
                }

                if (BackLoad)
                {
                    if (SfReports.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadReports, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfReports.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfReports.AddRange(api.Query<Report>(soql));
                        }
                    }
                }

                if (SfReports.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfReports.Count))
                    {
                        foreach (Report SfReport in SfReports)
                        {
                            result = SaveReport(result, SfReport);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadRoleValues(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_role_values__c> SfRoleValues = new List<lc_role_values__c>();

            try
            {

                MethodName = LastModified.RoleValue;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.RoleValue);

                soql = GetSoqlString(SoqlEnums.DownloadRoleValues, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfRoleValues = api.Query<lc_role_values__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfRoleValues.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadRoleValues, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfRoleValues.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfRoleValues.AddRange(api.Query<lc_role_values__c>(soql));
                        }
                    }
                }

                if (SfRoleValues.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfRoleValues.Count))
                    {
                        foreach (lc_role_values__c SfRoleValue in SfRoleValues)
                        {
                            result = SaveRoleValue(result, SfRoleValue);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadTasks(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<Task> sf_tasks = new List<Task>();

            try
            {

                MethodName = LastModified.Task;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Task);

                soql = GetSoqlString(SoqlEnums.DownloadTasks, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    sf_tasks = api.Query<Task>(soql);
                }

                if (BackLoad)
                {
                    if (sf_tasks.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadTasks, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - sf_tasks.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            sf_tasks.AddRange(api.Query<Task>(soql));
                        }
                    }
                }

                if (sf_tasks.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), sf_tasks.Count))
                    {
                        foreach (Task sf_task in sf_tasks)
                        {
                            result = SaveTask(result, sf_task);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadDynamicContent(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_dynamic_content__c> sf_dynamic_content = new List<lc_dynamic_content__c>();

            try
            {
                MethodName = LastModified.Task;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.DynamicContent);

                soql = GetSoqlString(SoqlEnums.DownloadDynamicContent, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    sf_dynamic_content = api.Query<lc_dynamic_content__c>(soql);
                }

                if (BackLoad)
                {
                    if (sf_dynamic_content.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadDynamicContent, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - sf_dynamic_content.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            sf_dynamic_content.AddRange(api.Query<lc_dynamic_content__c>(soql));
                        }
                    }
                }

                if (sf_dynamic_content.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), sf_dynamic_content.Count))
                    {
                        foreach (lc_dynamic_content__c sf_dc in sf_dynamic_content)
                        {
                            result = SaveDynamicContent(result, sf_dc);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadInquiryPrograms(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_inquiry_program__c> sf_inquiry_programs = new List<lc_inquiry_program__c>();

            try
            {

                MethodName = LastModified.Task;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.InquiryProgram).AddDays(-1);

                soql = GetSoqlString(SoqlEnums.DownloadInquiryPrograms, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    sf_inquiry_programs = api.Query<lc_inquiry_program__c>(soql);
                }

                if (BackLoad)
                {
                    if (sf_inquiry_programs.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadInquiryPrograms, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - sf_inquiry_programs.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            sf_inquiry_programs.AddRange(api.Query<lc_inquiry_program__c>(soql));
                        }
                    }
                }

                if (sf_inquiry_programs.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), sf_inquiry_programs.Count))
                    {
                        foreach (lc_inquiry_program__c sf_ip in sf_inquiry_programs)
                        {
                            result = SaveProgramInquiry(result, sf_ip);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        #endregion Salesforce Centric


        #region Mixed Centric

        private Result DownloadAccounts(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<Account> SfAccounts = new List<Account>();

            try
            {

                MethodName = LastModified.Account;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Account);

                soql = GetSoqlString(SoqlEnums.DownloadAccounts, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfAccounts = api.Query<Account>(soql);
                }

                if (BackLoad)
                {
                    if (SfAccounts.Count < MaxRecordCount)
                    {

                        soql = GetSoqlString(SoqlEnums.DownloadAccounts, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfAccounts.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfAccounts.AddRange(api.Query<Account>(soql));
                        }
                    }
                }

                if (SfAccounts.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfAccounts.Count))
                    {
                        foreach (Account SfAccount in SfAccounts)
                        {
                            if (SfAccount.RecordTypeId == AccountRecordTypes.AcademicProgram)
                            {
                                result = SaveColleagueAccount(result, SfAccount);
                            }
                            else {
                                result = SaveSalesforceAccount(result, SfAccount);
                            }
                            
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadAffiliations(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<hed__Affiliation__c> SfAffiliations = new List<hed__Affiliation__c>();

            try
            {

                MethodName = LastModified.Affiliation;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Affiliation);

                soql = GetSoqlString(SoqlEnums.DownloadAffiliations, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfAffiliations = api.Query<hed__Affiliation__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfAffiliations.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadAffiliations, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfAffiliations.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfAffiliations.AddRange(api.Query<hed__Affiliation__c>(soql));
                        }
                    }
                }

                if (SfAffiliations.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfAffiliations.Count))
                    {
                        foreach (hed__Affiliation__c SfAffiliation in SfAffiliations)
                        {
                            switch (SfAffiliation.RecordTypeId) {
                                case AffiliationRecordTypes.StudentProgramAffiliation:
                                    result = SaveColleagueAffiliation(result, SfAffiliation);
                                    break;
                                case AffiliationRecordTypes.DefaultAffiliation:
                                case AffiliationRecordTypes.CceBusinessAffiliation:
                                case AffiliationRecordTypes.CceCorporateAffiliation:
                                case AffiliationRecordTypes.PlacementAffiliation:
                                case AffiliationRecordTypes.ISAAffiliation:
                                    result = SaveSalesforceAffiliation(result, SfAffiliation);
                                    break;
                                default:
                                    break;
                            }

                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadContacts(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<Contact> SfContacts = new List<Contact>();

            try
            {

                MethodName = LastModified.Contact;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Contact);

                soql = GetSoqlString(SoqlEnums.DownloadContacts, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfContacts = api.Query<Contact>(soql);
                }

                if (BackLoad)
                {
                    if (SfContacts.Count < MaxRecordCount)
                    {

                        soql = GetSoqlString(SoqlEnums.DownloadContacts, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfContacts.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfContacts.AddRange(api.Query<Contact>(soql));
                        }
                    }
                }

                if (SfContacts.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfContacts.Count))
                    {
                        foreach (Contact SfContact in SfContacts)
                        {
                            if (SfContact.RecordTypeId == ContactRecordTypes.DefaultContactRecordType ||
                                SfContact.RecordTypeId == ContactRecordTypes.DuplicateContactRecordType ||
                                SfContact.RecordTypeId == ContactRecordTypes.PrivateContactRecordType)
                            {
                                result = SaveColleagueContact(result, SfContact);
                            }
                            else {
                                result = SaveSalesforceContact(result, SfContact);
                            }
                            
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        #endregion Mixed Centric


        #region Colleague Centric

        private Result DownloadApplications(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_application__c> SfApplications = new List<lc_application__c>();

            try
            {
                MethodName = LastModified.Application;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Application);

                soql = GetSoqlString(SoqlEnums.DownloadApplications, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfApplications = api.Query<lc_application__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfApplications.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadApplications, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfApplications.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfApplications.AddRange(api.Query<lc_application__c>(soql));
                        }
                    }
                }

                if (SfApplications.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfApplications.Count))
                    {
                        foreach (lc_application__c SfApplication in SfApplications)
                        {
                            result = SaveApplication(result, SfApplication);
                            result.TotalCount++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadCourses(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<hed__Course__c> sf_courses = new List<hed__Course__c>();

            try
            {

                MethodName = LastModified.Course;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Course);

                soql = GetSoqlString(SoqlEnums.DownloadCourses, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    sf_courses = api.Query<hed__Course__c>(soql);
                }

                if (BackLoad)
                {
                    if (sf_courses.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadCourses, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - sf_courses.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            sf_courses.AddRange(api.Query<hed__Course__c>(soql));
                        }
                    }
                }

                if (sf_courses.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), sf_courses.Count))
                    {
                        foreach (hed__Course__c sf_course in sf_courses)
                        {
                            result = SaveCourse(result, sf_course);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadCourseConnections(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<hed__Course_Enrollment__c> sf_course_enrollments = new List<hed__Course_Enrollment__c>();

            try
            {

                MethodName = LastModified.CourseConnection;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.CourseConnection);

                soql = GetSoqlString(SoqlEnums.DownloadCourseConnections, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    sf_course_enrollments = api.Query<hed__Course_Enrollment__c>(soql);
                }

                if (BackLoad)
                {
                    if (sf_course_enrollments.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadCourseConnections, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - sf_course_enrollments.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            sf_course_enrollments.AddRange(api.Query<hed__Course_Enrollment__c>(soql));
                        }
                    }
                }

                if (sf_course_enrollments.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), sf_course_enrollments.Count))
                    {
                        foreach (hed__Course_Enrollment__c sf_course_enrollment in sf_course_enrollments)
                        {
                            result = SaveCourseConnection(result, sf_course_enrollment);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadCourseOfferings(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<hed__Course_Offering__c> SfCourseOfferings = new List<hed__Course_Offering__c>();

            try
            {

                MethodName = LastModified.CourseOffering;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.CourseOffering);

                soql = GetSoqlString(SoqlEnums.DownloadCourseOfferings, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfCourseOfferings = api.Query<hed__Course_Offering__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfCourseOfferings.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadCourseOfferings, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfCourseOfferings.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfCourseOfferings.AddRange(api.Query<hed__Course_Offering__c>(soql));
                        }
                    }
                }

                if (SfCourseOfferings.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfCourseOfferings.Count))
                    {
                        foreach (hed__Course_Offering__c SfCourseOffering in SfCourseOfferings)
                        {
                            result = SaveCourseOffering(result, SfCourseOffering);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadInquiries(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_inquiry__c> sf_inquiries = new List<lc_inquiry__c>();

            try
            {

                MethodName = LastModified.Inquiry;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Inquiry);

                soql = GetSoqlString(SoqlEnums.DownloadInquiries, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    sf_inquiries = api.Query<lc_inquiry__c>(soql);
                }

                if (BackLoad)
                {
                    if (sf_inquiries.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadInquiries, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - sf_inquiries.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            sf_inquiries.AddRange(api.Query<lc_inquiry__c>(soql));
                        }
                    }
                }

                if (sf_inquiries.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), sf_inquiries.Count))
                    {
                        foreach (lc_inquiry__c SfInquiry in sf_inquiries)
                        {
                            result = SaveInquiry(result, SfInquiry);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadAcademicPrograms(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<Account> SfPrograms = new List<Account>();

            try
            {
                MethodName = LastModified.Program;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Program);

                soql = GetSoqlString(SoqlEnums.DownloadPrograms, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfPrograms = api.Query<Account>(soql);
                }

                if (BackLoad)
                {
                    if (SfPrograms.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadPrograms, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfPrograms.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfPrograms.AddRange(api.Query<Account>(soql));
                        }
                    }
                }

                if (SfPrograms.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfPrograms.Count))
                    {
                        foreach (Account SfProgram in SfPrograms)
                        {
                            result = SaveAcademicProgram(result, SfProgram);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadProgramEnrollments(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<hed__Program_Enrollment__c> SfProgram_enrollments = new List<hed__Program_Enrollment__c>();

            try
            {

                MethodName = LastModified.ProgramEnrollment;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.ProgramEnrollment);

                soql = GetSoqlString(SoqlEnums.DownloadProgramEnrollments, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfProgram_enrollments = api.Query<hed__Program_Enrollment__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfProgram_enrollments.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadProgramEnrollments, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfProgram_enrollments.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfProgram_enrollments.AddRange(api.Query<hed__Program_Enrollment__c>(soql));
                        }
                    }
                }

                if (SfProgram_enrollments.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfProgram_enrollments.Count))
                    {
                        foreach (hed__Program_Enrollment__c SfProgram_enrollment in SfProgram_enrollments)
                        {
                            result = SaveProgramEnrollment(result, SfProgram_enrollment);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        private Result DownloadTerms(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<hed__Term__c> SfTerms = new List<hed__Term__c>();

            try
            {

                MethodName = LastModified.Term;

                DateTime LastRunDate = GetLastRunDateTime(LastModified.Term);

                soql = GetSoqlString(SoqlEnums.DownloadTerms, DownloadTypes.NewRecordType, MaxRecordCount, LastRunDate);

                using (ApiService api = new ApiService())
                {
                    SfTerms = api.Query<hed__Term__c>(soql);
                }

                if (BackLoad)
                {
                    if (SfTerms.Count < MaxRecordCount)
                    {
                        soql = GetSoqlString(SoqlEnums.DownloadTerms, DownloadTypes.BackLoadModifiedType, (MaxRecordCount - SfTerms.Count), BackLoadPriorDate);

                        using (ApiService api = new ApiService())
                        {
                            SfTerms.AddRange(api.Query<hed__Term__c>(soql));
                        }
                    }
                }

                if (SfTerms.Count > 0)
                {
                    if (MonitorExecution(GetCurrentMethod(), SfTerms.Count))
                    {
                        foreach (hed__Term__c SfTerm in SfTerms)
                        {
                            result = SaveTerm(result, SfTerm);
                            result.TotalCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                result.Success = false;
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result;
        }

        #endregion Colleague Centric

        #region Immediate Salesforce Calls

        private bool DownloadEmailCampaign(string email_campaign_id)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<lc_email_campaign__c> SfEmailCampaigns = new List<lc_email_campaign__c>();

            try
            {
                MethodName = LastModified.EmailTemplate;

                soql = GetSoqlString(SoqlEnums.DownloadEmailCampaigns, DownloadTypes.NewRecordType, 1, null, email_campaign_id);

                using (ApiService api = new ApiService())
                {
                    SfEmailCampaigns = api.Query<lc_email_campaign__c>(soql);
                }

                if (MonitorExecution(GetCurrentMethod(), SfEmailCampaigns.Count))
                {
                    result.TotalCount = SfEmailCampaigns.Count;
                    foreach (lc_email_campaign__c SfEmailCampaign in SfEmailCampaigns)
                    {
                        result = SaveEmailCampaign(result, SfEmailCampaign);
                    }

                    if (result.TotalCount > result.UpdatedCount + result.InsertedCount)
                    {
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result.Success;
        }
        private bool DownloadEmailTemplate(string email_template_id)
        {
            Result result = new Result();
            string soql = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<EmailTemplate> SfEmailTemplates = new List<EmailTemplate>();

            try
            {
                MethodName = LastModified.EmailTemplate;

                soql = GetSoqlString(SoqlEnums.DownloadEmailTemplates, DownloadTypes.NewRecordType, 1, null, email_template_id);

                using (ApiService api = new ApiService())
                {
                    SfEmailTemplates = api.Query<EmailTemplate>(soql);
                }

                if (MonitorExecution(GetCurrentMethod(), SfEmailTemplates.Count))
                {
                    result.TotalCount = SfEmailTemplates.Count;
                    foreach (EmailTemplate SfEmailTemplate in SfEmailTemplates)
                    {
                        result = SaveEmailTemplate(result, SfEmailTemplate);
                    }

                    if (result.TotalCount > result.UpdatedCount + result.InsertedCount)
                    {
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, result.TotalCount, result.InsertedCount, result.UpdatedCount, result.ErrorMsg);

            return result.Success;
        }

        #endregion

        private Result TotalResults(Result total_results, Result individual_results)
        {

            try
            {
                if (individual_results != null)
                {
                    total_results.UpdatedCount += individual_results.UpdatedCount;
                    total_results.InsertedCount += individual_results.InsertedCount;
                    total_results.TotalCount += individual_results.TotalCount;
                }
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }
            return total_results;
        }
    }
}

//private bool GetApplications()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<lc_application__c> SfApplications = new List<lc_application__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.Application;

//            LastRunDate = GetLastRunDateTime(MethodName);

//            soql = GetSoqlString(SoqlEnums.DownloadApplications, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfApplications = api.Query<lc_application__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfApplications.Count))
//        {

//            foreach (lc_application__c SfApplication in SfApplications)
//            {
//                crm_applications DbApplication = new crm_applications();

//                var applications = db_ctx.crm_applications.Where(x => x.sis_application_id == SfApplication.lc_application_id__c);

//                if (applications.Any())
//                {
//                    DbApplication = applications.OrderByDescending(x => x.appl_modfied_date).FirstOrDefault();

//                    if (DbApplication != null)
//                    {

//                        DbApplication.crm_appl_number = SfApplication.Name;

//                        DbApplication.last_sfsync_datetime = DateTime.Now;
//                    }

//                    db_ctx.SaveChanges();

//                    UpdatedCount++;
//                }
//                TotalCount++;
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetContacts()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string csv = "''";
//    string soqlstr = string.Empty;
//    string ErrorMsg = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;
//    List<Record> recipients = new List<Record>();
//    List<Contact> SfContacts = new List<Contact>();
//    List<crm_contacts> DbContacts = new List<crm_contacts>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.Contact;

//            LastRunDate = GetLastRunDateTime(MethodName);

//            soqlstr = GetSoqlString(SoqlEnums.DownloadContacts, LastRunDate, csv);

//            using (ApiService api = new ApiService())
//            {
//                SfContacts = api.QueryAll<Contact>(soqlstr);
//            }

//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfContacts.Count))
//        {
//            foreach (Contact SfContact in SfContacts)
//            {
//                crm_contacts crm_contact = new crm_contacts();

//                var crm_contacts = db_ctx.crm_contacts.Where(x => (x.contact_deleted == false && x.contact_mark_delete == false)
//                                                              && ((!string.IsNullOrEmpty(x.contact_id) && x.contact_id == SfContact.Id)
//                                                              || (!string.IsNullOrEmpty(x.contact_colleague_id) && x.contact_colleague_id == SfContact.lc_colleague_id__c)
//                                                              || ((!string.IsNullOrEmpty(x.contact_last_name) && x.contact_last_name == SfContact.LastName)
//                                                                && (!string.IsNullOrEmpty(x.contact_first_name) && x.contact_first_name == SfContact.FirstName)
//                                                                && (x.contact_birthdate != null && x.contact_birthdate == SfContact.Birthdate)))).ToList();

//                if (crm_contacts.Any())
//                {
//                    // update existing
//                    crm_contact = crm_contacts.OrderByDescending(x => x.contact_last_modified_datetime).OrderByDescending(y => y.contact_id).FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    // insert new
//                    crm_contact.contact_guid = Guid.NewGuid();
//                    crm_contact.contact_created_by = SfContact.CreatedById ?? Settings.CrmAdmin;
//                    crm_contact.contact_created_datetime = GetLocalTime(SfContact.CreatedDate);

//                    db_ctx.crm_contacts.Add(crm_contact);

//                    InsertedCount++;
//                }

//                if (crm_contact.contact_id != SfContact.Id)
//                {
//                    crm_contact.contact_id = SfContact.Id;
//                }

//                crm_contact.contact_deleted = SfContact.IsDeleted ?? false;
//                crm_contact.contact_account_id = SfContact.AccountId;

//                crm_contact.contact_owner_id = SfContact.OwnerId;

//                crm_contact.contact_first_name = SfContact.FirstName;
//                crm_contact.contact_last_name = SfContact.LastName;
//                crm_contact.contact_full_name = SfContact.Name;

//                crm_contact.contact_gender = SfContact.hed__Gender__c;

//                crm_contact.contact_home_phone = SfContact.HomePhone;
//                crm_contact.contact_mobile_phone = SfContact.MobilePhone;

//                if (string.IsNullOrEmpty(SfContact.hed__AlternateEmail__c) || SfContact.hed__AlternateEmail__c.Contains("lethbridge"))
//                {
//                    crm_contact.contact_alternate_email = "";
//                }
//                else
//                {
//                    crm_contact.contact_alternate_email = SfContact.hed__AlternateEmail__c;
//                }

//                crm_contact.contact_preferred_email = SfContact.hed__Preferred_Email__c;

//                crm_contact.contact_primary_address_type = SfContact.hed__Primary_Address_Type__c;
//                crm_contact.contact_mailing_street = SfContact.MailingStreet;
//                crm_contact.contact_mailing_city = SfContact.MailingCity;
//                crm_contact.contact_mailing_province = SfContact.MailingState;
//                crm_contact.contact_mailing_country = SfContact.MailingCountry;
//                crm_contact.contact_mailing_postalcode = SfContact.MailingPostalCode;
//                crm_contact.contact_last_modified_by = SfContact.LastModifiedById ?? Settings.CrmAdmin;

//                if (SfContact.BirthdateSpecified) crm_contact.contact_birthdate = SfContact.Birthdate;

//                if (SfContact.HasOptedOutOfEmailSpecified) crm_contact.contact_email_opt_out = SfContact.HasOptedOutOfEmail ?? false;
//                if (SfContact.IsEmailBouncedSpecified) crm_contact.contact_is_email_bounced = SfContact.IsEmailBounced ?? false;

//                if (SfContact.LastReferencedDateSpecified) crm_contact.contact_last_referenced_datetime = GetLocalTime(SfContact.LastReferencedDate);
//                if (SfContact.LastViewedDateSpecified) crm_contact.contact_last_viewed_datetime = GetLocalTime(SfContact.LastViewedDate);
//                if (SfContact.LastActivityDateSpecified) crm_contact.contact_last_activity_date = GetLocalTime(SfContact.LastActivityDate);

//                if (SfContact.IsEmailBouncedSpecified) crm_contact.contact_is_email_bounced = SfContact.IsEmailBounced ?? false;
//                if (SfContact.EmailBouncedDateSpecified) crm_contact.contact_email_bounced_date = GetLocalTime(SfContact.EmailBouncedDate);
//                if (SfContact.EmailBouncedReason != null) crm_contact.contact_email_bounced_reason = SfContact.EmailBouncedReason;

//                if (SfContact.LastModifiedDateSpecified) crm_contact.contact_last_modified_datetime = GetLocalTime(SfContact.LastModifiedDate);

//                crm_contact.last_sfsync_datetime = DateTime.Now;

//                TotalCount++;

//            }

//            db_ctx.SaveChanges();
//        }

//        success = true;
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetTerms()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;
//    List<hed__Term__c> SfTerms = new List<hed__Term__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.Term;

//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadTerms, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfTerms = api.Query<hed__Term__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfTerms.Count))
//        {

//            foreach (hed__Term__c SfTerm in SfTerms)
//            {
//                crm_terms crm_term = new crm_terms();

//                var crm_terms = db_ctx.crm_terms.Where(x => x.term_id == crm_term.term_id);

//                crm_term.term_id = SfTerm.Id;

//                if (crm_terms.Any())
//                {
//                    // Update
//                    crm_term = crm_terms.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    // Insert
//                    crm_term.term_guid = Guid.NewGuid();
//                    crm_term.term_created_by = SfTerm.CreatedById;
//                    if (SfTerm.CreatedDateSpecified) crm_term.term_created_datetime = GetLocalTime(SfTerm.CreatedDate);

//                    db_ctx.crm_terms.Add(crm_term);
//                    InsertedCount++;
//                }

//                crm_term.term_deleted = SfTerm.IsDeleted ?? false;
//                crm_term.term_name = SfTerm.Name;
//                crm_term.term_modified_by = SfTerm.LastModifiedById;
//                crm_term.term_account_id = SfTerm.hed__Account__c;
//                crm_term.term_parent_term_id = SfTerm.hed__Parent_Term__c;
//                crm_term.term_type = SfTerm.hed__Type__c;
//                crm_term.term_code = SfTerm.term_id__c;

//                if (SfTerm.LastModifiedDateSpecified) crm_term.term_modifed_datetime = GetLocalTime(SfTerm.LastModifiedDate);
//                if (SfTerm.SystemModstampSpecified) crm_term.term_system_modstamp = GetLocalTime(SfTerm.SystemModstamp);
//                if (SfTerm.LastViewedDateSpecified) crm_term.term_last_viewed_datetime = GetLocalTime(SfTerm.LastViewedDate);
//                if (SfTerm.LastReferencedDateSpecified) crm_term.term_last_referenced_datetime = GetLocalTime(SfTerm.LastReferencedDate);
//                if (SfTerm.hed__End_Date__cSpecified) crm_term.term_end_date = GetLocalTime(SfTerm.hed__End_Date__c);
//                if (SfTerm.hed__Start_Date__cSpecified) crm_term.term_start_date = GetLocalTime(SfTerm.hed__Start_Date__c);
//                if (SfTerm.hed__Grading_Period_Sequence__cSpecified) crm_term.term_grade_period_sequence = (Decimal)SfTerm.hed__Grading_Period_Sequence__c;
//                if (SfTerm.hed__Instructional_Days__cSpecified) crm_term.term_instructional_days = (Decimal)SfTerm.hed__Instructional_Days__c;

//                crm_term.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetCourses()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<hed__Course__c> sf_courses = new List<hed__Course__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.Course;

//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadCourses, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                sf_courses = api.Query<hed__Course__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, sf_courses.Count))
//        {

//            foreach (hed__Course__c sf_course in sf_courses)
//            {
//                crm_courses DbCourse = new crm_courses();

//                var DbCourses = db_ctx.crm_courses.Where(x => x.course_deleted == false
//                                                            && x.course_mark_delete == false
//                                                            && x.course_guid.ToString() == sf_course.lc_course_guid__c
//                                                            || (sf_course.Id != null && x.crm_course_id == sf_course.Id)
//                                                            || (x.course_number == sf_course.hed__Course_ID__c
//                                                                && x.course_department == sf_course.hed__Account__c)
//                                                            ).ToList();

//                if (DbCourses.Any())
//                {
//                    // Update
//                    DbCourse = DbCourses.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    // Insert
//                    DbCourse.course_guid = Guid.NewGuid();
//                    DbCourse.course_created_by = sf_course.CreatedById;
//                    if (sf_course.CreatedDateSpecified) DbCourse.course_created_datetime = GetLocalTime(sf_course.CreatedDate);

//                    db_ctx.crm_courses.Add(DbCourse);
//                    InsertedCount++;
//                }

//                if (sf_course.LastModifiedDateSpecified) DbCourse.course_modified_datetime = GetLocalTime(sf_course.LastModifiedDate);

//                DbCourse.crm_course_id = sf_course.Id;
//                DbCourse.course_number = sf_course.hed__Course_ID__c;
//                DbCourse.course_name = sf_course.Name;
//                DbCourse.course_description = sf_course.hed__Description__c;
//                DbCourse.course_desc_extended = sf_course.hed__Extended_Description__c;

//                DbCourse.course_deleted = sf_course.IsDeleted ?? false;

//                if (sf_course.hed__Credit_Hours__c != null)
//                {
//                    DbCourse.course_credit_hours = (decimal)sf_course.hed__Credit_Hours__c;
//                }

//                DbCourse.course_department = sf_course.hed__Account__c;

//                DbCourse.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();

//                TotalCount++;
//            }
//            success = true;
//        }

//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetCourseOfferings()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<hed__Course_Offering__c> SfCourseOfferings = new List<hed__Course_Offering__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.CourseOffering;
//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadCourseOfferings, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfCourseOfferings = api.Query<hed__Course_Offering__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfCourseOfferings.Count))
//        {
//            foreach (hed__Course_Offering__c SfCourseOffering in SfCourseOfferings)
//            {
//                crm_course_offerings DbCourseOffering = new crm_course_offerings();

//                var possible_matches = db_ctx.crm_course_offerings.Where(x => x.sis_course_section_id == SfCourseOffering.hed__Section_ID__c).ToList();

//                if (possible_matches.Any())
//                {
//                    // Update
//                    DbCourseOffering = possible_matches.FirstOrDefault();

//                    DbCourseOffering.course_offering_id = SfCourseOffering.Id;

//                    UpdatedCount++;
//                }
//                else
//                {
//                    // Insert
//                    DbCourseOffering.course_offering_guid = Guid.NewGuid();
//                    DbCourseOffering.course_offering_created_by = SfCourseOffering.CreatedById;
//                    if (SfCourseOffering.CreatedDateSpecified) DbCourseOffering.course_offering_created_datetime = GetLocalTime(SfCourseOffering.CreatedDate);

//                    db_ctx.crm_course_offerings.Add(DbCourseOffering);
//                    InsertedCount++;
//                }

//                DbCourseOffering.course_offering_id = SfCourseOffering.Id;

//                DbCourseOffering.course_offering_capacity = (decimal)SfCourseOffering.hed__Capacity__c;
//                DbCourseOffering.course_offering_course = SfCourseOffering.hed__Course__c;
//                DbCourseOffering.course_offering_name = SfCourseOffering.Name;
//                //DbCourseOffering.sis_course_section_id = SfCourseOffering.hed__Section_ID__c;
//                DbCourseOffering.course_offering_term = SfCourseOffering.hed__Term__c;
//                DbCourseOffering.course_offering_primary_faculty = SfCourseOffering.hed__Faculty__c;

//                DbCourseOffering.course_offering_modified_by = Settings.CrmAdmin;
//                if (SfCourseOffering.LastModifiedDateSpecified) DbCourseOffering.course_offering_modified_datetime = GetLocalTime(SfCourseOffering.LastModifiedDate);

//                DbCourseOffering.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();

//                TotalCount++;
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetCourseConnections()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<hed__Course_Enrollment__c> sf_course_enrollments = new List<hed__Course_Enrollment__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.CourseConnection;
//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadCourseConnections, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                sf_course_enrollments = api.Query<hed__Course_Enrollment__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, sf_course_enrollments.Count))
//        {

//            foreach (hed__Course_Enrollment__c sf_course_enrollment in sf_course_enrollments)
//            {
//                crm_course_connections DbCourseConnection = new crm_course_connections();

//                var DbCourseConnections = db_ctx.crm_course_connections.Where(x => x.course_connection_id == DbCourseConnection.course_connection_id);

//                if (DbCourseConnections.Any())
//                {
//                    // Update
//                    DbCourseConnection = DbCourseConnections.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    // Insert
//                    DbCourseConnection.course_connection_guid = Guid.NewGuid();

//                    DbCourseConnection.course_connection_created_by = sf_course_enrollment.CreatedById;
//                    DbCourseConnection.course_connection_created_datetime = GetLocalTime(sf_course_enrollment.CreatedDate);

//                    db_ctx.crm_course_connections.Add(DbCourseConnection);
//                    InsertedCount++;
//                }

//                DbCourseConnection.course_connection_id = sf_course_enrollment.Id;

//                // DbCourseConnection.course_connection_affiliation = sf_course_enrollment.hed__Affiliation__c;
//                // DbCourseConnection.course_connection_acad_program = sf_course_enrollment.hed__Program_Enrollment__c;
//                // DbCourseConnection.course_connection_name = sf_course_enrollment.Name;
//                // DbCourseConnection.course_offering_id = sf_course_enrollment.hed__Course_Offering__c;
//                // DbCourseConnection.course_credits_attempted = (decimal)sf_course_enrollment.hed__Credits_Attempted__c;
//                // DbCourseConnection.course_credits_earned = (decimal)sf_course_enrollment.hed__Credits_Earned__c;
//                // DbCourseConnection.course_grade = (decimal)sf_course_enrollment.hed__Grade__c;
//                // DbCourseConnection.course_connection_primary = sf_course_enrollment.hed_prim
//                // DbCourseConnection.program_enrollment_id = sf_course_enrollment.hed__Program_Enrollment__c;
//                // DbCourseConnection.course_connection_record_type = sf_course_enrollment.RecordTypeId;
//                // DbCourseConnection.course_connection_status = sf_course_enrollment.


//                if (sf_course_enrollment.LastModifiedDateSpecified)
//                {
//                    DbCourseConnection.course_connection_modified_by = Settings.CrmAdmin;
//                    DbCourseConnection.course_connection_modified_datetime = GetLocalTime(sf_course_enrollment.LastModifiedDate);
//                }

//                DbCourseConnection.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();

//                TotalCount++;
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetPrograms()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<Account> SfAccounts = new List<Account>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.Program;

//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadPrograms, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfAccounts = api.Query<Account>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfAccounts.Count))
//        {
//            foreach (Account SfAccount in SfAccounts)
//            {
//                crm_accounts DbAccount = new crm_accounts();

//                var crm_accounts = db_ctx.crm_accounts.Where(x => x.account_id == DbAccount.account_id
//                                                        && x.account_modifed_datetime >= LastRunDate
//                                                        && x.account_record_type_id == AccountRecordTypes.AcademicProgram);
//                if (crm_accounts.Any())
//                {
//                    DbAccount = crm_accounts.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    DbAccount.account_guid = Guid.NewGuid();
//                    DbAccount.account_created_by = SfAccount.CreatedById;
//                    DbAccount.account_created_date = GetLocalTime(SfAccount.CreatedDate);

//                    db_ctx.crm_accounts.Add(DbAccount);
//                    InsertedCount++;
//                }

//                DbAccount.account_id = SfAccount.Id;
//                DbAccount.account_number = SfAccount.AccountNumber;
//                DbAccount.account_name = SfAccount.Name;

//                DbAccount.account_type = SfAccount.lc_account_type__c;
//                DbAccount.account_record_type_id = SfAccount.RecordTypeId;

//                DbAccount.account_deleted = SfAccount.IsDeleted ?? false;

//                DbAccount.account_modified_by = SfAccount.LastModifiedById;
//                DbAccount.account_modifed_datetime = GetLocalTime(SfAccount.LastModifiedDate);

//                DbAccount.account_last_referenced_date = GetLocalTime(SfAccount.LastReferencedDate);
//                DbAccount.account_last_viewed_date = GetLocalTime(SfAccount.LastViewedDate);

//                DbAccount.account_owner_id = SfAccount.OwnerId;

//                DbAccount.account_parent_account_id = SfAccount.ParentId;
//                DbAccount.account_owner_id = SfAccount.OwnerId;

//                DbAccount.account_phone = SfAccount.Phone;

//                DbAccount.account_system_modstamp = SfAccount.SystemModstamp;

//                DbAccount.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//            }
//            success = true;
//        }

//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetProgramEnrollments()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<hed__Program_Enrollment__c> SfProgram_enrollments = new List<hed__Program_Enrollment__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.ProgramEnrollment;
//            LastRunDate = GetLastRunDateTime(MethodName);
//            //LastRunDate = DateTime.Today.AddYears(-45);
//            soql = GetSoqlString(SoqlEnums.DownloadProgramEnrollments, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfProgram_enrollments = api.Query<hed__Program_Enrollment__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfProgram_enrollments.Count))
//        {
//            foreach (hed__Program_Enrollment__c SfProgram_enrollment in SfProgram_enrollments)
//            {
//                crm_program_enrollments DbProgram_enrollment = new crm_program_enrollments();

//                try
//                {
//                    var possible_matches = db_ctx.crm_program_enrollments.Where(x => (!string.IsNullOrEmpty(x.crm_program_enrollment_id) && x.crm_program_enrollment_id == SfProgram_enrollment.Id)
//                                                                            || (x.crm_program_enrollment_guid.ToString() == SfProgram_enrollment.lc_program_enrollment_guid__c)
//                                                                            || ((!string.IsNullOrEmpty(x.crm_program_enrollment_contact_id) && x.crm_program_enrollment_contact_id == SfProgram_enrollment.hed__Contact__c)
//                                                                                && (!string.IsNullOrEmpty(x.crm_program_enrollment_program_id) && x.crm_program_enrollment_program_id == SfProgram_enrollment.hed__Account__c)
//                                                                                && (x.crm_program_enrollment_start_datetime != null && x.crm_program_enrollment_start_datetime == SfProgram_enrollment.hed__Start_Date__c))
//                                                                        ).ToList();
//                    if (possible_matches.Any())
//                    {
//                        DbProgram_enrollment = possible_matches.OrderByDescending(x => x.crm_program_enrollment_modified_datetime).FirstOrDefault();

//                        if (DbProgram_enrollment != null)
//                        {
//                            DbProgram_enrollment.crm_program_enrollment_id = SfProgram_enrollment.Id;

//                            if (DbProgram_enrollment.crm_program_enrollment_guid.ToString() != SfProgram_enrollment.lc_program_enrollment_guid__c)
//                            {
//                                DbProgram_enrollment.crm_program_enrollment_guid_mismatch = true;
//                            }
//                            else
//                            {
//                                DbProgram_enrollment.crm_program_enrollment_guid_mismatch = false;
//                            }

//                            UpdatedCount++;
//                        }
//                    }
//                    else
//                    {
//                        DbProgram_enrollment.crm_program_enrollment_guid = Guid.NewGuid();

//                        DbProgram_enrollment.crm_program_enrollment_id = SfProgram_enrollment.Id;
//                        DbProgram_enrollment.crm_program_enrollment_contact_id = SfProgram_enrollment.hed__Contact__c;
//                        DbProgram_enrollment.crm_program_enrollment_program_id = SfProgram_enrollment.hed__Account__c;

//                        if (SfProgram_enrollment.hed__Start_Date__cSpecified)
//                        {
//                            DbProgram_enrollment.crm_program_enrollment_start_datetime = GetLocalTime(SfProgram_enrollment.hed__Start_Date__c);
//                        }

//                        if (SfProgram_enrollment.hed__End_Date__cSpecified)
//                        {
//                            DbProgram_enrollment.crm_program_enrollment_end_datetime = GetLocalTime(SfProgram_enrollment.hed__End_Date__c);
//                        }

//                        if (SfProgram_enrollment.CreatedDateSpecified)
//                        {
//                            DbProgram_enrollment.crm_program_enrollment_created_by = SfProgram_enrollment.CreatedById ?? Settings.CrmAdmin;
//                            DbProgram_enrollment.crm_program_enrollment_created_datetime = GetLocalTime(SfProgram_enrollment.CreatedDate);
//                        }

//                        if (SfProgram_enrollment.LastModifiedDateSpecified)
//                        {
//                            DbProgram_enrollment.crm_program_enrollment_modified_by = SfProgram_enrollment.LastModifiedById ?? Settings.CrmAdmin;
//                            DbProgram_enrollment.crm_program_enrollment_modified_datetime = GetLocalTime(SfProgram_enrollment.LastModifiedDate);
//                        }

//                        DbProgram_enrollment.crm_program_enrollment_affiliation = SfProgram_enrollment.hed__Affiliation__c;

//                        db_ctx.crm_program_enrollments.Add(DbProgram_enrollment);

//                        InsertedCount++;
//                    }

//                    DbProgram_enrollment.crm_program_enrollment_key = SfProgram_enrollment.Name;

//                    if (SfProgram_enrollment.IsDeletedSpecified)
//                    {
//                        DbProgram_enrollment.crm_program_enrollment_deleted = SfProgram_enrollment.IsDeleted ?? false;
//                    }

//                    if (SfProgram_enrollment.LastViewedDateSpecified)
//                    {
//                        DbProgram_enrollment.crm_program_enrollment_last_viewed_date = GetLocalTime(SfProgram_enrollment.LastViewedDate);
//                    }

//                    if (SfProgram_enrollment.LastReferencedDateSpecified)
//                    {
//                        DbProgram_enrollment.crm_program_enrollment_last_referenced_date = GetLocalTime(SfProgram_enrollment.LastReferencedDate);
//                    }

//                    DbProgram_enrollment.last_sfsync_datetime = DateTime.Now;

//                    db_ctx.SaveChanges();
//                }
//                catch (Exception ex)
//                {
//                    ErrorMsg += ex.ToString();
//                }

//                TotalCount++;
//            }
//        }

//        success = true;
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg += ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetAffiliations()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = LastModified.Affiliation;
//    DateTime StartTime = DateTime.Now;

//    List<hed__Affiliation__c> SfAffiliations = new List<hed__Affiliation__c>();

//    try
//    {
//        LastRunDate = GetLastRunDateTime(MethodName);

//        soql = GetSoqlString(SoqlEnums.DownloadAffiliations, LastRunDate);

//        using (ApiService api = new ApiService())
//        {
//            SfAffiliations = api.QueryAll<hed__Affiliation__c>(soql);
//        }

//        if (MonitorExecution(MethodName, SfAffiliations.Count))
//        {
//            foreach (hed__Affiliation__c SfAffiliation in SfAffiliations)
//            {
//                success = SaveAffiliation(SfAffiliation);
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg += ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, SfAffiliations.Count, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetInquiries()
//{
//    int TotalCount = 0;
//    int InsertedCount = 0;
//    int UpdatedCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<lc_inquiry__c> sf_inquiries = new List<lc_inquiry__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.Inquiry;

//            LastRunDate = GetLastRunDateTime(MethodName);

//            soql = GetSoqlString(SoqlEnums.DownloadInquiries, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                sf_inquiries = api.QueryAll<lc_inquiry__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, sf_inquiries.Count))
//        {
//            foreach (lc_inquiry__c SfInquiry in sf_inquiries.OrderByDescending(x => x.LastModifiedDate))
//            {
//                crm_inquiries crm_inquiry = new crm_inquiries();

//                var crm_inquiries = db_ctx.crm_inquiries.Where(x => (!string.IsNullOrEmpty(x.inquiry_id) && x.inquiry_id == SfInquiry.Id)
//                                                        || (!string.IsNullOrEmpty(x.inquiry_guid.ToString()) && x.inquiry_guid.ToString() == SfInquiry.lc_inquiry_guid__c));


//                if (crm_inquiries.Any())
//                {
//                    crm_inquiry = crm_inquiries.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    crm_inquiry.inquiry_guid = Guid.NewGuid();
//                    crm_inquiry.inq_created_by = SfInquiry.CreatedById ?? Settings.CrmAdmin;
//                    crm_inquiry.inq_created_datetime = GetLocalTime(SfInquiry.CreatedDate ?? SfInquiry.lc_inquiry_date__c ?? DateTime.Now);

//                    db_ctx.crm_inquiries.Add(crm_inquiry);
//                    InsertedCount++;
//                }
//                TotalCount++;

//                if (!string.IsNullOrEmpty(SfInquiry.Id) && string.IsNullOrEmpty(crm_inquiry.inquiry_id))
//                {
//                    crm_inquiry.inquiry_id = SfInquiry.Id;
//                }

//                crm_inquiry.inquiry_datetime = GetLocalTime(SfInquiry.lc_inquiry_date__c ?? SfInquiry.CreatedDate ?? DateTime.Now);

//                if (!string.IsNullOrEmpty(SfInquiry.Name))
//                {
//                    crm_inquiry.inq_number = SfInquiry.Name;
//                }

//                crm_inquiry.inq_owner_id = SfInquiry.OwnerId;
//                crm_inquiry.inq_deleted = SfInquiry.IsDeleted ?? false;

//                crm_inquiry.inq_contact_id = SfInquiry.lc_contact__c;
//                crm_inquiry.lc_inq_legacy_id = SfInquiry.lc_inquiry_legacy_id__c;

//                crm_inquiry.inq_anticipated_start_term = SfInquiry.lc_anticipated_start_term__c;

//                crm_inquiry.inq_pri_prog_interest = SfInquiry.lc_primary_program__c;
//                crm_inquiry.inq_sec_prog_interest = SfInquiry.lc_secondary_program__c;
//                crm_inquiry.inq_services_interest = SfInquiry.lc_services_interest__c;

//                crm_inquiry.inq_source = SfInquiry.lc_source__c;

//                crm_inquiry.inq_student_type = SfInquiry.lc_student_type__c;

//                crm_inquiry.inq_last_school = SfInquiry.lc_last_school__c;

//                crm_inquiry.inq_campus = SfInquiry.lc_inquiry_campus__c ?? "main";

//                crm_inquiry.inq_agent_flag = SfInquiry.lc_agent__c ?? false;
//                crm_inquiry.inq_agent_prev_flag = SfInquiry.lc_agent_previous_work__c ?? false;
//                crm_inquiry.inq_agency_name = SfInquiry.lc_agency_name__c;

//                if (SfInquiry.LastActivityDate != null)
//                {
//                    crm_inquiry.inq_last_activity_date = GetLocalTime(SfInquiry.LastActivityDate);
//                }

//                if (SfInquiry.LastReferencedDate != null)
//                {
//                    crm_inquiry.inq_last_referenced_datetime = GetLocalTime(SfInquiry.LastReferencedDate);
//                }

//                if (SfInquiry.LastModifiedDate != null)
//                {

//                    crm_inquiry.inq_modified_by = SfInquiry.LastModifiedById ?? Settings.CrmAdmin;
//                    crm_inquiry.inq_modified_datetime = GetLocalTime(SfInquiry.LastModifiedDate);
//                }

//                crm_inquiry.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetEvents()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<Event> SfEvents = new List<Event>();
//    try
//    {
//        try
//        {
//            MethodName = LastModified.Event;

//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadEvents, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfEvents = api.QueryAll<Event>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfEvents.Count))
//        {
//            foreach (Event SfEvent in SfEvents)
//            {
//                try
//                {
//                    crm_events db_crm_event = new crm_events();

//                    var crm_events = db_ctx.crm_events.Where(x => x.event_deleted == false
//                                                                && x.event_created_datetime == db_crm_event.event_created_datetime
//                                                                && x.event_subject == db_crm_event.event_subject
//                                                                && (x.activity_guid == db_crm_event.activity_guid
//                                                                || x.activity_id == db_crm_event.activity_id));


//                    if (crm_events.Any())
//                    {
//                        // Update
//                        db_crm_event = crm_events.FirstOrDefault();
//                        UpdatedCount++;
//                    }
//                    else
//                    {
//                        // Insert
//                        db_crm_event.activity_guid = Guid.NewGuid();
//                        db_crm_event.event_created_by = SfEvent.CreatedById;
//                        if (SfEvent.CreatedDateSpecified) db_crm_event.event_created_datetime = GetLocalTime(SfEvent.CreatedDate);

//                        db_ctx.crm_events.Add(db_crm_event);
//                        InsertedCount++;
//                    }

//                    db_crm_event.activity_id = SfEvent.Id;
//                    db_crm_event.event_name_id = SfEvent.WhoId;
//                    db_crm_event.event_related_to_id = SfEvent.WhatId;

//                    if (SfEvent.StartDateTimeSpecified)
//                    {
//                        db_crm_event.event_start_date = GetLocalTime(SfEvent.StartDateTime);
//                    }

//                    if (SfEvent.StartDateTimeSpecified)
//                    {
//                        db_crm_event.event_start_datetime = GetLocalTime(SfEvent.StartDateTime);
//                    }

//                    if (SfEvent.EndDateTimeSpecified)
//                    {
//                        db_crm_event.event_end_datetime = GetLocalTime(SfEvent.EndDateTime);
//                    }

//                    if (SfEvent.EndDateTimeSpecified)
//                    {
//                        db_crm_event.event_end_date = GetLocalTime(SfEvent.EndDateTime);
//                    }

//                    if (SfEvent.RecurrenceStartDateTimeSpecified)
//                    {
//                        db_crm_event.event_recurrence_start = GetLocalTime(SfEvent.RecurrenceStartDateTime);
//                    }

//                    db_crm_event.event_location = SfEvent.Location;

//                    db_crm_event.event_is_child = SfEvent.IsChild ?? false;

//                    db_crm_event.event_group_event_type = SfEvent.GroupEventType;

//                    db_crm_event.event_deleted = SfEvent.IsDeleted ?? false;
//                    db_crm_event.event_description = SfEvent.Description;

//                    db_crm_event.event_modified_by = SfEvent.LastModifiedById;

//                    if (SfEvent.LastModifiedDateSpecified)
//                    {
//                        db_crm_event.event_modified_datetime = GetLocalTime(SfEvent.LastModifiedDate ?? DateTime.Now);
//                    }

//                    db_crm_event.event_account_id = SfEvent.AccountId;
//                    db_crm_event.event_recurrence_activity_id = SfEvent.RecurrenceActivityId;

//                    db_crm_event.event_archived = SfEvent.IsArchived ?? false;

//                    // Name of the event
//                    db_crm_event.event_subject = SfEvent.Subject;
//                    // Department
//                    db_crm_event.event_department = SfEvent.Department__c;
//                    // Event or Appt.
//                    db_crm_event.event_engagement_type = SfEvent.lc_engagement_type__c;

//                    db_crm_event.event_activity_extender_id = SfEvent.lc_activity_extender__c;

//                    db_crm_event.last_sfsync_datetime = DateTime.Now;

//                    db_ctx.SaveChanges();
//                    TotalCount++;
//                }
//                catch (Exception ex)
//                {
//                    ErrorMsg += ex.ToString();
//                }
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetActivityExtender()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<lc_activity_extender__c> sf_activityExtenders = new List<lc_activity_extender__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.ActivityExtender;

//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadActivityExtender, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                sf_activityExtenders = api.QueryAll<lc_activity_extender__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, sf_activityExtenders.Count))
//        {
//            foreach (lc_activity_extender__c sf_activityExtender in sf_activityExtenders)
//            {
//                try
//                {
//                    crm_activity_extender db_crm_activity_extender = new crm_activity_extender();

//                    var crm_activity_extenders = db_ctx.crm_activity_extender.Where(x => x.activity_extender_id == db_crm_activity_extender.activity_extender_id);

//                    db_crm_activity_extender.activity_extender_id = sf_activityExtender.Id;

//                    if (crm_activity_extenders.Any())
//                    {
//                        // Update
//                        db_crm_activity_extender = crm_activity_extenders.FirstOrDefault();
//                        UpdatedCount++;
//                    }
//                    else
//                    {
//                        // Insert
//                        db_crm_activity_extender.activity_extender_guid = Guid.NewGuid();
//                        db_crm_activity_extender.activity_extender_created_by = sf_activityExtender.CreatedById;
//                        if (sf_activityExtender.CreatedDateSpecified) db_crm_activity_extender.activity_extender_created_datetime = GetLocalTime(sf_activityExtender.CreatedDate);

//                        db_ctx.crm_activity_extender.Add(db_crm_activity_extender);
//                        InsertedCount++;
//                    }

//                    db_crm_activity_extender.activity_extender_deleted = sf_activityExtender.IsDeleted ?? false;

//                    db_crm_activity_extender.activity_extender_modified_by = sf_activityExtender.LastModifiedById;
//                    db_crm_activity_extender.activity_extender_modified_datetime = GetLocalTime(sf_activityExtender.LastModifiedDate ?? DateTime.Now);

//                    db_crm_activity_extender.last_sfsync_datetime = DateTime.Now;

//                    db_ctx.SaveChanges();

//                    TotalCount++;
//                }
//                catch (Exception ex)
//                {
//                    ErrorMsg += ex.ToString();
//                }
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetEventRegistrations()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<lc_event_registration__c> SfEventRegistrations = new List<lc_event_registration__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.EventRegistration;

//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadEventRegistrations, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfEventRegistrations = api.QueryAll<lc_event_registration__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfEventRegistrations.Count))
//        {

//            foreach (lc_event_registration__c SfEventRegistration in SfEventRegistrations)
//            {
//                try
//                {
//                    crm_event_registrations db_crm_event_registration = new crm_event_registrations();

//                    var crm_eventRegistrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_id == db_crm_event_registration.event_registration_id);

//                    db_crm_event_registration.event_registration_id = SfEventRegistration.Id;

//                    if (crm_eventRegistrations.Any())
//                    {
//                        // Update
//                        db_crm_event_registration = crm_eventRegistrations.FirstOrDefault();
//                        UpdatedCount++;
//                    }
//                    else
//                    {
//                        // Insert
//                        db_crm_event_registration.event_registration_guid = Guid.NewGuid();
//                        db_crm_event_registration.event_registration_created_by = SfEventRegistration.CreatedById;
//                        if (SfEventRegistration.CreatedDateSpecified) db_crm_event_registration.event_registration_created_datetime = GetLocalTime(SfEventRegistration.CreatedDate);

//                        db_ctx.crm_event_registrations.Add(db_crm_event_registration);
//                        InsertedCount++;
//                    }

//                    db_crm_event_registration.event_registration_deleted = SfEventRegistration.IsDeleted ?? false;
//                    db_crm_event_registration.event_registration_activity_extender_id = SfEventRegistration.lc_activity_extender__c;
//                    db_crm_event_registration.event_registration_attended = SfEventRegistration.lc_attended__c;
//                    db_crm_event_registration.event_registration_registered = SfEventRegistration.lc_registered__c;
//                    db_crm_event_registration.event_registration_checkedin = SfEventRegistration.lc_checkedin__c;
//                    db_crm_event_registration.event_registration_cancelled = SfEventRegistration.lc_cancelled__c;
//                    db_crm_event_registration.event_registration_contact_id = SfEventRegistration.lc_contact__c;

//                    if (SfEventRegistration.LastModifiedDateSpecified)
//                    {
//                        db_crm_event_registration.event_registration_modified_by = SfEventRegistration.LastModifiedById;
//                        db_crm_event_registration.event_registration_modified_datetime = GetLocalTime(SfEventRegistration.LastModifiedDate ?? DateTime.Now);
//                    }

//                    db_crm_event_registration.last_sfsync_datetime = DateTime.Now;

//                    db_ctx.SaveChanges();
//                    TotalCount++;
//                }
//                catch (Exception ex)
//                {
//                    ErrorMsg += ex.ToString();
//                    RecordError(GetCurrentMethod(), ErrorMsg);
//                }
//            }
//            success = true;
//        }

//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//        RecordError(GetCurrentMethod(), ErrorMsg);
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetEmailCampaigns()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<lc_email_campaign__c> SfEmailCampaigns = new List<lc_email_campaign__c>();
//    try
//    {
//        try
//        {
//            MethodName = LastModified.EmailCampaign;

//            LastRunDate = GetLastRunDateTime(MethodName);

//            soql = GetSoqlString(SoqlEnums.DownloadEmailCampaigns, LastRunDate);


//            using (ApiService api = new ApiService())
//            {
//                SfEmailCampaigns = api.QueryAll<lc_email_campaign__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfEmailCampaigns.Count))
//        {
//            foreach (lc_email_campaign__c SfEmailCampaign in SfEmailCampaigns)
//            {
//                crm_email_campaigns DbEmailCampaign = new crm_email_campaigns();

//                var DbEmailCampaigns = db_ctx.crm_email_campaigns.Where(x => x.email_campaign_id == SfEmailCampaign.Id);

//                DbEmailCampaign.email_campaign_id = SfEmailCampaign.Id;

//                if (DbEmailCampaigns.Any())
//                {
//                    // Update
//                    DbEmailCampaign = DbEmailCampaigns.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    // Insert
//                    DbEmailCampaign.email_campaign_guid = Guid.NewGuid();
//                    DbEmailCampaign.ec_created_by = SfEmailCampaign.CreatedById;
//                    if (SfEmailCampaign.CreatedDateSpecified) DbEmailCampaign.ec_created_datetime = GetLocalTime(SfEmailCampaign.CreatedDate);

//                    db_ctx.crm_email_campaigns.Add(DbEmailCampaign);
//                    InsertedCount++;
//                }

//                DbEmailCampaign.ec_deleted = SfEmailCampaign.IsDeleted ?? false;
//                DbEmailCampaign.ec_active_flag = SfEmailCampaign.lc_active__c ?? false;
//                DbEmailCampaign.ec_recur = SfEmailCampaign.lc_is_reoccurring__c ?? false;

//                DbEmailCampaign.ec_report_id = SfEmailCampaign.lc_report_id__c;
//                DbEmailCampaign.ec_template_id = SfEmailCampaign.lc_email_template_id__c;

//                DbEmailCampaign.ec_send_time = SfEmailCampaign.lc_send_time__c.Value.TimeOfDay;

//                DbEmailCampaign.ec_email_address_type = SfEmailCampaign.lc_recipient_address_type__c;

//                // this custom screen and object do not implement UTC i.e. this is already local time
//                // Mike Paulson is working on this
//                DbEmailCampaign.ec_start_datetime = SfEmailCampaign.lc_start_date__c;

//                if (SfEmailCampaign.lc_end_date__cSpecified)
//                {
//                    DbEmailCampaign.ec_end_datetime = SfEmailCampaign.lc_end_date__c;
//                }
//                else
//                {
//                    DbEmailCampaign.ec_end_datetime = null;
//                }

//                if (SfEmailCampaign.lc_is_reoccurring__cSpecified)
//                {
//                    DbEmailCampaign.ec_recur = SfEmailCampaign.lc_is_reoccurring__c ?? false;
//                }
//                else
//                {
//                    DbEmailCampaign.ec_recur = false;
//                }

//                DbEmailCampaign.ec_recur_days = SfEmailCampaign.lc_recur_days__c;
//                DbEmailCampaign.ec_recur_week_days = SfEmailCampaign.lc_recur_week_days__c;
//                DbEmailCampaign.ec_week_days_only_flag = SfEmailCampaign.lc_week_days_only__c ?? false;
//                DbEmailCampaign.ec_name = SfEmailCampaign.Name;
//                DbEmailCampaign.ec_department = SfEmailCampaign.lc_department__c;
//                DbEmailCampaign.ec_send_now = SfEmailCampaign.lc_send_now__c;
//                DbEmailCampaign.ec_parent_campaign_id = SfEmailCampaign.lc_parent_campaign__c;
//                DbEmailCampaign.ec_from_email_address = SfEmailCampaign.lc_source_email__c;
//                DbEmailCampaign.ec_from_email_address_title = SfEmailCampaign.lc_source_name__c;

//                if (SfEmailCampaign.LastModifiedDateSpecified)
//                {
//                    DbEmailCampaign.ec_modified_by = SfEmailCampaign.LastModifiedById;
//                    DbEmailCampaign.ec_modified_datetime = GetLocalTime(SfEmailCampaign.LastModifiedDate);
//                }

//                DbEmailCampaign.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//                TotalCount++;
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetEmailBroadcasts()
//{
//    int UpdatedCount = 0;
//    int InsertedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<lc_email_broadcast__c> sf_email_broadcasts = new List<lc_email_broadcast__c>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.EmailBroadcast;

//            LastRunDate = GetLastRunDateTime(MethodName);

//            soql = GetSoqlString(SoqlEnums.DownloadEmailBroadcasts, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                sf_email_broadcasts = api.QueryAll<lc_email_broadcast__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, sf_email_broadcasts.Count))
//        {
//            foreach (lc_email_broadcast__c sf_email_broadcast in sf_email_broadcasts)
//            {
//                crm_email_broadcasts DbEmailBroadcast = new crm_email_broadcasts();

//                var DbEmailBroadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_id == sf_email_broadcast.Id);

//                if (DbEmailBroadcasts.Any())
//                {
//                    DbEmailBroadcast = DbEmailBroadcasts.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    DbEmailBroadcast.email_broadcast_guid = Guid.NewGuid();
//                    DbEmailBroadcast.email_broadcast_id = sf_email_broadcast.Id;
//                    DbEmailBroadcast.email_broadcast_created_by = sf_email_broadcast.CreatedById;
//                    if (sf_email_broadcast.CreatedDateSpecified) DbEmailBroadcast.email_broadcast_created_datetime = GetLocalTime(sf_email_broadcast.CreatedDate);

//                    db_ctx.crm_email_broadcasts.Add(DbEmailBroadcast);
//                    InsertedCount++;
//                }

//                if (sf_email_broadcast.LastModifiedDateSpecified)
//                {
//                    DbEmailBroadcast.email_broadcast_modified_by = sf_email_broadcast.LastModifiedById;
//                    DbEmailBroadcast.email_broadcast_modfied_datetime = GetLocalTime(sf_email_broadcast.LastModifiedDate);
//                }

//                if (sf_email_broadcast.lc_broadcast_sent__cSpecified)
//                {
//                    DbEmailBroadcast.email_broadcast_sent = sf_email_broadcast.lc_broadcast_sent__c;
//                }

//                if (sf_email_broadcast.IsDeletedSpecified)
//                {
//                    DbEmailBroadcast.email_broadcast_deleted = sf_email_broadcast.IsDeleted ?? false;
//                }

//                DbEmailBroadcast.email_broadcast_messages_sent = sf_email_broadcast.lc_messages_sent__c;
//                DbEmailBroadcast.email_broadcast_name = sf_email_broadcast.Name;
//                DbEmailBroadcast.email_broadcast_status = sf_email_broadcast.lc_broadcast_status__c;
//                DbEmailBroadcast.email_broadcast_sys_modstamp = GetLocalTime(sf_email_broadcast.SystemModstamp);
//                DbEmailBroadcast.email_campaign_id = sf_email_broadcast.lc_email_campaign__c;

//                DbEmailBroadcast.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//                TotalCount++;
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetEmailTemplates()
//{
//    int UpdatedCount = 0;
//    int InsertedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<EmailTemplate> SfEmailTemplates = new List<EmailTemplate>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.EmailTemplate;

//            LastRunDate = GetLastRunDateTime(MethodName);

//            soql = GetSoqlString(SoqlEnums.DownloadEmailTemplates, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfEmailTemplates = api.QueryAll<EmailTemplate>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfEmailTemplates.Count))
//        {
//            foreach (EmailTemplate SfEmailTemplate in SfEmailTemplates)
//            {
//                crm_email_templates DbEmailTemplate = new crm_email_templates();

//                var DbEmailTemplates = db_ctx.crm_email_templates.Where(x => x.email_template_id == SfEmailTemplate.Id
//                                                                        && x.email_template_id != null);

//                if (DbEmailTemplates.Any())
//                {
//                    DbEmailTemplate = DbEmailTemplates.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    DbEmailTemplate.email_template_guid = Guid.NewGuid();
//                    DbEmailTemplate.email_template_id = SfEmailTemplate.Id;
//                    DbEmailTemplate.email_template_created_by = SfEmailTemplate.CreatedById;
//                    if (SfEmailTemplate.CreatedDateSpecified) DbEmailTemplate.email_template_created_datetime = GetLocalTime(SfEmailTemplate.CreatedDate);

//                    db_ctx.crm_email_templates.Add(DbEmailTemplate);
//                    InsertedCount++;
//                }

//                if (SfEmailTemplate.LastModifiedDateSpecified)
//                {
//                    DbEmailTemplate.email_template_modified_by = SfEmailTemplate.LastModifiedById;
//                    DbEmailTemplate.email_template_modified_datetime = GetLocalTime(SfEmailTemplate.LastModifiedDate);
//                }

//                DbEmailTemplate.email_template_body = SfEmailTemplate.Body;
//                DbEmailTemplate.email_template_brand_template_id = SfEmailTemplate.BrandTemplateId;
//                DbEmailTemplate.email_template_description = SfEmailTemplate.Description;
//                DbEmailTemplate.email_template_dev_name = SfEmailTemplate.DeveloperName;
//                DbEmailTemplate.email_template_encoding = SfEmailTemplate.Encoding;
//                DbEmailTemplate.email_template_folder_id = SfEmailTemplate.FolderId;
//                DbEmailTemplate.email_template_folder_name = SfEmailTemplate.FolderName;
//                DbEmailTemplate.email_template_html_value = SfEmailTemplate.HtmlValue;
//                DbEmailTemplate.email_template_is_active = SfEmailTemplate.IsActive ?? false;
//                DbEmailTemplate.email_template_last_used_datetime = GetLocalTime(SfEmailTemplate.LastUsedDate);
//                DbEmailTemplate.email_template_times_used = SfEmailTemplate.TimesUsed;
//                DbEmailTemplate.email_template_markup = SfEmailTemplate.Markup;
//                DbEmailTemplate.email_template_name = SfEmailTemplate.Name;
//                DbEmailTemplate.email_template_namespace_prefix = SfEmailTemplate.NamespacePrefix;
//                DbEmailTemplate.email_template_owner_id = SfEmailTemplate.OwnerId;
//                DbEmailTemplate.email_template_subject = SfEmailTemplate.Subject;
//                DbEmailTemplate.email_template_system_modstamp = SfEmailTemplate.SystemModstamp;
//                DbEmailTemplate.email_template_style = SfEmailTemplate.TemplateStyle;
//                DbEmailTemplate.email_template_type = SfEmailTemplate.TemplateType;

//                DbEmailTemplate.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//                TotalCount++;

//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetReports()
//{
//    int UpdatedCount = 0;
//    int InsertedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<Report> SfReports = new List<Report>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.Report;

//            LastRunDate = GetLastRunDateTime(MethodName);

//            soql = GetSoqlString(SoqlEnums.DownloadReports, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                SfReports = api.QueryAll<Report>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfReports.Count))
//        {
//            foreach (Report SfReport in SfReports)
//            {
//                crm_reports DbReport = new crm_reports();

//                var DbReports = db_ctx.crm_reports.Where(x => x.crm_report_id == SfReport.Id
//                                                               && x.crm_report_id != null);

//                if (DbReports.Any())
//                {
//                    DbReport = DbReports.OrderByDescending(x => x.crm_report_modified_date).FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    DbReport.report_guid = Guid.NewGuid();
//                    DbReport.crm_report_id = SfReport.Id;

//                    if (SfReport.CreatedDateSpecified)
//                    {
//                        DbReport.crm_report_created_by = SfReport.CreatedById;
//                        DbReport.crm_report_created_date = GetLocalTime(SfReport.CreatedDate);
//                    }

//                    db_ctx.crm_reports.Add(DbReport);
//                    InsertedCount++;
//                }

//                DbReport.crm_report_name = SfReport.Name;
//                DbReport.crm_namespace_prefix = SfReport.NamespacePrefix;
//                DbReport.crm_report_description = SfReport.Description;
//                DbReport.crm_report_folder_name = SfReport.FolderName;
//                DbReport.crm_report_format = SfReport.Format;
//                DbReport.crm_report_owner_id = SfReport.OwnerId;
//                DbReport.crm_report_developer_name = SfReport.DeveloperName;

//                if (SfReport.IsDeletedSpecified)
//                {
//                    DbReport.crm_report_deleted = SfReport.IsDeleted ?? false;
//                }

//                if (SfReport.LastRunDateSpecified)
//                {
//                    DbReport.crm_report_last_run_date = SfReport.LastRunDate;
//                }

//                if (SfReport.SystemModstampSpecified)
//                {
//                    DbReport.crm_report_system_modstamp = SfReport.SystemModstamp;
//                }

//                if (SfReport.LastReferencedDateSpecified)
//                {
//                    DbReport.crm_report_last_referenced_date = SfReport.LastReferencedDate;
//                }

//                if (SfReport.LastViewedDateSpecified)
//                {
//                    DbReport.crm_report_last_viewed_date = SfReport.LastViewedDate;
//                }

//                if (SfReport.LastModifiedDateSpecified)
//                {
//                    DbReport.crm_report_modified_by = SfReport.LastModifiedById;
//                    DbReport.crm_report_modified_date = SfReport.LastModifiedDate;
//                }

//                DbReport.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//                TotalCount++;

//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetRoleValues()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<lc_role_values__c> SfRoleValues = new List<lc_role_values__c>();
//    try
//    {
//        try
//        {
//            MethodName = LastModified.RoleValue;
//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadRoleValues, LastRunDate);


//            using (ApiService api = new ApiService())
//            {
//                SfRoleValues = api.QueryAll<lc_role_values__c>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, SfRoleValues.Count))
//        {
//            foreach (lc_role_values__c SfRoleValue in SfRoleValues)
//            {
//                crm_role_values DbRoleValue = new crm_role_values();

//                var DbRoleValues = db_ctx.crm_role_values.Where(x => x.crm_role_value_id == SfRoleValue.Id);

//                DbRoleValue.crm_role_value_id = SfRoleValue.Id;

//                if (DbRoleValues.Any())
//                {
//                    // Update
//                    DbRoleValue = DbRoleValues.OrderByDescending(x => x.crm_rv_modified_datetime).FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    // Insert
//                    DbRoleValue.crm_role_value_guid = Guid.NewGuid();
//                    DbRoleValue.crm_rv_created_by = SfRoleValue.CreatedById;
//                    if (SfRoleValue.CreatedDateSpecified) DbRoleValue.crm_rv_created_datetime = GetLocalTime(SfRoleValue.CreatedDate);

//                    db_ctx.crm_role_values.Add(DbRoleValue);
//                    InsertedCount++;
//                }

//                DbRoleValue.crm_rv_name = SfRoleValue.Name;
//                DbRoleValue.crm_rv_owner_id = SfRoleValue.OwnerId;
//                DbRoleValue.crm_rv_role_id = SfRoleValue.lc_role_id__c;
//                DbRoleValue.crm_rv_sender_name = SfRoleValue.lc_sender_name__c;
//                DbRoleValue.crm_rv_send_emails = SfRoleValue.lc_send_emails__c;
//                DbRoleValue.crm_rv_default_contact_type = SfRoleValue.lc_default_contact_type__c;

//                if (SfRoleValue.IsDeletedSpecified)
//                {
//                    DbRoleValue.crm_rv_deleted_flag = SfRoleValue.IsDeleted ?? false;
//                }

//                if (SfRoleValue.LastModifiedDateSpecified)
//                {
//                    DbRoleValue.crm_rv_modified_by = SfRoleValue.LastModifiedById;
//                    DbRoleValue.crm_rv_modified_datetime = GetLocalTime(SfRoleValue.LastModifiedDate);
//                }

//                DbRoleValue.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//                TotalCount++;
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

//private bool GetTasks()
//{
//    int InsertedCount = 0;
//    int UpdatedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string soql = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<Task> sf_tasks = new List<Task>();

//    try
//    {
//        try
//        {
//            MethodName = LastModified.Task;

//            LastRunDate = GetLastRunDateTime(MethodName);
//            soql = GetSoqlString(SoqlEnums.DownloadTasks, LastRunDate);

//            using (ApiService api = new ApiService())
//            {
//                sf_tasks = api.QueryAll<Task>(soql);
//            }
//        }
//        catch (Exception ex)
//        {
//            ErrorMsg = ex.ToString();
//        }

//        if (MonitorExecution(MethodName, sf_tasks.Count))
//        {
//            foreach (Task sf_task in sf_tasks)
//            {
//                crm_tasks crm_task = new crm_tasks();

//                var crm_tasks = db_ctx.crm_tasks.Where(x => x.activity_id == crm_task.activity_id);

//                crm_task.activity_id = sf_task.Id;

//                if (crm_tasks.Any())
//                {
//                    // Update
//                    crm_task = crm_tasks.FirstOrDefault();
//                    UpdatedCount++;
//                }
//                else
//                {
//                    // Insert
//                    crm_task.activity_guid = Guid.NewGuid();
//                    crm_task.task_created_by = sf_task.CreatedById;
//                    if (sf_task.CreatedDateSpecified) crm_task.task_created_datetime = GetLocalTime(sf_task.CreatedDate);

//                    db_ctx.crm_tasks.Add(crm_task);
//                    InsertedCount++;
//                }

//                crm_task.task_deleted = sf_task.IsDeleted ?? false;
//                crm_task.task_description = sf_task.Description;
//                crm_task.task_modified_by = sf_task.LastModifiedById;
//                crm_task.task_account_id = sf_task.AccountId;

//                crm_task.task_call_type = sf_task.CallType;
//                crm_task.task_call_object_identfier = sf_task.CallObject;
//                if (sf_task.CallDurationInSecondsSpecified) crm_task.task_call_duration = sf_task.CallDurationInSeconds;

//                crm_task.task_recurrence_activity_id = sf_task.RecurrenceActivityId;
//                crm_task.task_account_id = sf_task.AccountId;
//                crm_task.task_call_type = sf_task.CallType;
//                crm_task.task_owner_id = sf_task.OwnerId;
//                crm_task.task_priority = sf_task.Priority;
//                crm_task.task_call_object_identfier = sf_task.CallObject;
//                crm_task.task_recurrence_activity_id = sf_task.RecurrenceActivityId;
//                crm_task.task_recurrence_instance = sf_task.RecurrenceInstance;
//                crm_task.task_recurrence_month_of_year = sf_task.RecurrenceMonthOfYear;
//                crm_task.task_recurrence_timezone = sf_task.RecurrenceTimeZoneSidKey;
//                crm_task.task_regenerated_type = sf_task.RecurrenceRegeneratedType;
//                crm_task.task_status = sf_task.Status;
//                crm_task.task_subject = sf_task.Subject;
//                crm_task.task_subtype = sf_task.TaskSubtype;
//                crm_task.task_what_id = sf_task.WhatId;
//                crm_task.task_who_id = sf_task.WhoId;
//                crm_task.task_recurrence_type = sf_task.RecurrenceType;

//                if (sf_task.RecurrenceIntervalSpecified) crm_task.task_recurrence_interval = sf_task.RecurrenceInterval;
//                if (sf_task.RecurrenceEndDateOnlySpecified) crm_task.task_recurrence_end_datetime = GetLocalTime(sf_task.RecurrenceEndDateOnly);
//                if (sf_task.RecurrenceStartDateOnlySpecified) crm_task.task_recurrence_start_datetime = GetLocalTime(sf_task.RecurrenceStartDateOnly);
//                if (sf_task.RecurrenceDayOfMonthSpecified) crm_task.task_recurrence_day_of_month = sf_task.RecurrenceDayOfMonth;
//                if (sf_task.IsRecurrenceSpecified) crm_task.task_is_recurrence = sf_task.IsRecurrence ?? false;

//                if (sf_task.LastModifiedDateSpecified) crm_task.task_modified_datetime = GetLocalTime(sf_task.LastModifiedDate);
//                if (sf_task.CompletedDateTimeSpecified) crm_task.task_completed_datetime = GetLocalTime(sf_task.CompletedDateTime);
//                if (sf_task.SystemModstampSpecified) crm_task.task_system_modstamp = GetLocalTime(sf_task.SystemModstamp);

//                if (sf_task.ActivityDateSpecified) crm_task.tast_activity_datetime = GetLocalTime(sf_task.ActivityDate);
//                if (sf_task.CallDurationInSecondsSpecified) crm_task.task_call_duration = sf_task.CallDurationInSeconds;
//                if (sf_task.CompletedDateTimeSpecified) crm_task.task_completed_datetime = GetLocalTime(sf_task.CompletedDateTime);
//                if (sf_task.IsArchivedSpecified) crm_task.task_archived = sf_task.IsArchived ?? false;
//                if (sf_task.IsClosedSpecified) crm_task.task_closed = sf_task.IsClosed ?? false;
//                if (sf_task.IsHighPrioritySpecified) crm_task.task_high_priority = sf_task.IsHighPriority ?? false;
//                if (sf_task.IsReminderSetSpecified) crm_task.task_reminder_set = sf_task.IsReminderSet ?? false;
//                if (sf_task.IsHighPrioritySpecified) crm_task.task_priority = sf_task.Priority;
//                if (sf_task.IsReminderSetSpecified) crm_task.task_reminder_set = sf_task.IsReminderSet ?? false;
//                if (sf_task.RecurrenceDayOfMonthSpecified) crm_task.task_recurrence_day_of_month = sf_task.RecurrenceDayOfMonth;
//                if (sf_task.RecurrenceDayOfWeekMaskSpecified) crm_task.task_recurrence_day_of_week_mask = sf_task.RecurrenceDayOfWeekMask;
//                if (sf_task.RecurrenceIntervalSpecified) crm_task.task_recurrence_interval = sf_task.RecurrenceInterval;
//                if (sf_task.RecurrenceStartDateOnlySpecified) crm_task.task_recurrence_start_datetime = GetLocalTime(sf_task.RecurrenceStartDateOnly);
//                if (sf_task.ReminderDateTimeSpecified) crm_task.task_reminder_datetime = GetLocalTime(sf_task.ReminderDateTime);

//                crm_task.last_sfsync_datetime = DateTime.Now;

//                db_ctx.SaveChanges();
//                TotalCount++;
//            }
//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

