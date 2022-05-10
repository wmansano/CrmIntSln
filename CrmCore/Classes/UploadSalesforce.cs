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
        private bool UpsertSalesforce(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {

            bool success = false;
            string MethodName = string.Empty;

            try
            {
                MethodName = GetCurrentMethod();

                UpsertContacts(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertCourses(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertCourseOfferings(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertCourseConnections(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertEventRegistrations(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertApplications(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertEmailBroadcasts(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertTerms(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertInquiries(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertActivityExtenders(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertEmailCampaigns(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertEvents(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertEmailTemplates(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertDynamicContent(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertInquiryPrograms(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertAffiliations(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertAcademicPrograms(BackLoad, BackLoadPriorDate, MaxRecordCount);

                UpsertRoleValues(BackLoad, BackLoadPriorDate, MaxRecordCount);

                // UploadTasks(BackLoad, BackLoadPriorDate, MaxRecordCount);

                // UpsertReports(BackLoad, BackLoadPriorDate, MaxRecordCount);

                // UpsertProgramEnrollments(BackLoad, BackLoadPriorDate, MaxRecordCount);

                // UpsertInternational(BackLoad, BackLoadPriorDate, MaxRecordCount);

                success = true;
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return success;
        }

        // Add UploadOrganizations (Missing)

        private bool UpsertContacts(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int TotalCount = 0;
            int UpdatedCount = 0;
            int InsertedCount = 0;

            bool success = false;
            string MethodName = string.Empty;
            string ErrorMsg = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<crm_contacts> AllContacts = new List<crm_contacts>();
            Dictionary<string, sObject> ContactUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Contact;

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                var NewContacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                                    && x.contact_mark_delete == false
                                                                    && x.contact_last_name != null
                                                                    && x.contact_id == null
                                                                    && (x.contact_record_type_id == ContactRecordTypes.DefaultContactRecordType
                                                                    || x.contact_record_type_id == ContactRecordTypes.DuplicateContactRecordType
                                                                    || x.contact_record_type_id == ContactRecordTypes.PrivateContactRecordType
                                                                    || x.contact_record_type_id == null))
                                                                 .OrderByDescending(x => x.contact_last_modified_datetime)
                                                                 .Take(MaxRecordCount)
                                                                 .ToList();

                AllContacts.AddRange(NewContacts);

                int DifferenceCount = MaxRecordCount - NewContacts.Count;

                if (DifferenceCount > 0)
                {
                    // pull modified records
                    var ChangedContacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                                        && x.contact_mark_delete == false
                                                                        && x.contact_last_name != null
                                                                        && x.contact_id != null
                                                                        && x.contact_last_modified_datetime >= LastRunDate
                                                                        && (x.contact_record_type_id == ContactRecordTypes.DefaultContactRecordType
                                                                        || x.contact_record_type_id == ContactRecordTypes.DuplicateContactRecordType
                                                                        || x.contact_record_type_id == ContactRecordTypes.PrivateContactRecordType))
                                                                    .OrderByDescending(x => x.contact_last_modified_datetime)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                    if (ChangedContacts.Count > 0)
                    {
                        AllContacts.AddRange(ChangedContacts);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - AllContacts.Count);

                    if (DifferenceCount > 0)
                    {
                        var BackloadContacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                                        && x.contact_mark_delete == false
                                                                        && x.contact_colleague_id != null
                                                                        && x.contact_last_name != null
                                                                        && (x.last_sfsync_datetime == null
                                                                            || x.last_sfsync_datetime < BackLoadPriorDate
                                                                           )
                                                                        && (x.contact_record_type_id == ContactRecordTypes.DefaultContactRecordType
                                                                        || x.contact_record_type_id == ContactRecordTypes.DuplicateContactRecordType
                                                                        || x.contact_record_type_id == ContactRecordTypes.PrivateContactRecordType)
                                                                        )
                                                                    .OrderBy(x => x.last_sfsync_datetime)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                        if (BackloadContacts.Count > 0)
                        {
                            AllContacts.AddRange(BackloadContacts);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), AllContacts.Count))
                {
                    foreach (crm_contacts DbContact in AllContacts)
                    {
                        if (!string.IsNullOrWhiteSpace(DbContact.contact_colleague_id)) {

                            // map to salesforce object
                            Contact SfContact = MapToSfContact(DbContact);

                            // count inserts vs. updates
                            if (string.IsNullOrWhiteSpace(DbContact.contact_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                            // prevent duplicate record upload
                            if (!ContactUpserts.Keys.Contains(SfContact.lc_contact_guid__c))
                            {
                                ContactUpserts.Add(DbContact.contact_guid.ToString(), SfContact);
                            }
                        }
                        
                    }
                    // upsert records
                    success = UpsertRecords(InsertResults.Contact, ContactUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool UpsertCourses(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<crm_courses> AllCourses = new List<crm_courses>();
            Dictionary<string, sObject> CourseUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Course;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                var NewCourses = db_ctx.crm_courses.Where(x => x.course_deleted == false
                                                           && (x.course_department != null && x.course_department != "")
                                                           && (x.crm_course_id == null))
                                                        .OrderByDescending(x => x.course_modified_datetime)
                                                        .Take(MaxRecordCount)
                                                        .ToList();
                AllCourses.AddRange(NewCourses);

                int DifferenceCount = MaxRecordCount - NewCourses.Count;

                if (DifferenceCount > 0)
                {
                    var ChangedCourses = db_ctx.crm_courses.Where(x => x.course_deleted == false
                                                               && x.course_mark_delete == false
                                                               && x.crm_course_id != null
                                                               && (x.course_department != null && x.course_department != "")
                                                               && x.course_modified_datetime >= LastRunDate)
                                                            .OrderByDescending(x => x.course_modified_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                    if (ChangedCourses.Count > 0)
                    {
                        AllCourses.AddRange(ChangedCourses);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - AllCourses.Count);
                    if (DifferenceCount > 0)
                    {
                        var BackloadCourses = db_ctx.crm_courses.Where(x => x.course_deleted == false
                                                               && (x.course_department != null && x.course_department != "")
                                                               && (x.last_sfsync_datetime == null
                                                               || x.last_sfsync_datetime < BackLoadPriorDate))
                                                            .OrderBy(x => x.last_sfsync_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                        if (BackloadCourses.Count > 0)
                        {
                            AllCourses.AddRange(BackloadCourses);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), AllCourses.Count))
                {
                    foreach (crm_courses DbCourse in AllCourses)
                    {
                        // map to salesforce object
                        hed__Course__c SfCourse = MapToSfCourse(DbCourse);

                        // count updates vs inserts
                        if (string.IsNullOrWhiteSpace(DbCourse.crm_course_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                        // prevent duplicates
                        if (!CourseUpserts.Keys.Contains(SfCourse.lc_course_guid__c))
                        {
                            CourseUpserts.Add(DbCourse.course_guid.ToString(), SfCourse);
                        }
                    }

                    // upsert records to salesforce
                    success = UpsertRecords(InsertResults.Course, CourseUpserts);   
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool UpsertCourseOfferings(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)   
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<crm_course_offerings> AllCourseOfferings = new List<crm_course_offerings>();
            Dictionary<string, sObject> CourseOfferingUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.CourseOffering;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var NewCourseOfferings = db_ctx.crm_course_offerings.Where(x => x.course_offering_deleted == false
                                                                    && x.course_offering_mark_delete == false
                                                                    && x.course_offering_id == null
                                                                    && x.course_offering_course != null
                                                                    && x.course_offering_course != "")
                                                                .OrderByDescending(x => x.course_offering_modified_datetime)
                                                                .Take(MaxRecordCount)
                                                                .ToList();
                
                AllCourseOfferings.AddRange(NewCourseOfferings);

                int DifferenceCount = MaxRecordCount - AllCourseOfferings.Count;

                if (DifferenceCount > 0)
                {
                    var ChangedCourseOfferings = db_ctx.crm_course_offerings.Where(x => x.course_offering_deleted == false
                                                                    && x.course_offering_mark_delete == false
                                                                    && x.course_offering_id != null
                                                                    && x.course_offering_course != null
                                                                    && x.course_offering_course != ""
                                                                    && x.course_offering_modified_datetime >= LastRunDate)
                                                                    .OrderByDescending(x=>x.course_offering_modified_datetime)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                    if (ChangedCourseOfferings.Count > 0)
                    {
                        AllCourseOfferings.AddRange(ChangedCourseOfferings);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - AllCourseOfferings.Count);
                    if (DifferenceCount > 0)
                    {
                        var ChangedCourseOfferings = db_ctx.crm_course_offerings.Where(x => x.course_offering_deleted == false
                                                                                        && x.course_offering_mark_delete == false
                                                                                        && (x.last_sfsync_datetime == null
                                                                                        || x.last_sfsync_datetime < BackLoadPriorDate))
                                                                                .OrderBy(x => x.last_sfsync_datetime)
                                                                                .Take(DifferenceCount)
                                                                                .ToList();

                        if (ChangedCourseOfferings.Count > 0)
                        {
                            AllCourseOfferings.AddRange(ChangedCourseOfferings);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), AllCourseOfferings.Count))
                {
                    foreach (crm_course_offerings DbCourseOffering in AllCourseOfferings)
                    {
                        // map to salesforce object
                        hed__Course_Offering__c SfCourseOffering = MapToSfCourseOffering(DbCourseOffering);

                        // count updates vs inserts
                        if (!string.IsNullOrWhiteSpace(DbCourseOffering.course_offering_id)) { UpdatedCount++; } else { InsertedCount++; } TotalCount++;
                        
                        // prevent duplicates
                        if (!CourseOfferingUpserts.Keys.Contains(SfCourseOffering.lc_course_offering_guid__c))
                        {
                            CourseOfferingUpserts.Add(DbCourseOffering.course_offering_guid.ToString(), SfCourseOffering);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.CourseOffering, CourseOfferingUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool UpsertCourseConnections(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<crm_course_connections> AllCourseConnections = new List<crm_course_connections>();
            Dictionary<string, sObject> CourseConnectionUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.CourseConnection;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var NewCourseConnections = db_ctx.crm_course_connections.Where(x => x.course_connection_deleted == false
                                                                                && x.course_connection_mark_delete == false
                                                                                && x.course_connection_id == null)
                                                                   .OrderByDescending(x=>x.course_connection_modified_datetime)
                                                                   .Take(MaxRecordCount)
                                                                   .ToList();

                AllCourseConnections.AddRange(NewCourseConnections);

                int DifferenceCount = MaxRecordCount - NewCourseConnections.Count;

                if (DifferenceCount > 0)
                {
                    var ChangedCourseConnections = db_ctx.crm_course_connections.Where(x => x.course_connection_deleted == false
                                                                                        && x.course_connection_mark_delete == false
                                                                                        && x.course_connection_id != null
                                                                                        && (x.course_connection_modified_datetime >= LastRunDate
                                                                                        && x.last_sfsync_datetime >= LastRunDate))
                                                                                .OrderByDescending(x => x.course_connection_modified_datetime)
                                                                                .Take(DifferenceCount)
                                                                                .ToList();

                    if (ChangedCourseConnections.Count > 0)
                    {
                        AllCourseConnections.AddRange(ChangedCourseConnections);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - AllCourseConnections.Count);
                    if (DifferenceCount > 0)
                    {
                        var BackloadCourseConnections = db_ctx.crm_course_connections.Where(x => x.course_connection_deleted == false
                                                                                && x.course_connection_mark_delete == false
                                                                                && x.course_connection_modified_datetime < BackLoadPriorDate)
                                                                        .OrderByDescending(x => x.course_connection_modified_datetime)
                                                                        .Take(DifferenceCount)
                                                                        .ToList();

                        if (BackloadCourseConnections.Count > 0)
                        {
                            AllCourseConnections.AddRange(BackloadCourseConnections);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), AllCourseConnections.Count))
                {

                    foreach (crm_course_connections DbCourseConnection in AllCourseConnections)
                    {
                        // map to salesforce object
                        hed__Course_Enrollment__c SfCourseConnection = MapToSfCourseConnections(DbCourseConnection);

                        // count updates vs inserts
                        if (!string.IsNullOrWhiteSpace(DbCourseConnection.course_offering_id)) { UpdatedCount++; } else { InsertedCount++; } TotalCount++;

                        // prevent duplicates
                        if (!CourseConnectionUpserts.Keys.Contains(SfCourseConnection.lc_course_connection_guid__c))
                        {
                            CourseConnectionUpserts.Add(DbCourseConnection.course_connection_guid.ToString(), SfCourseConnection);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.CourseConnection, CourseConnectionUpserts);

                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool UpsertAffiliations(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<crm_affiliations> AllAffiliations = new List<crm_affiliations>();
            Dictionary<string, sObject> AffiliationUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Affiliation;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                var NewAffiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_mark_delete == false
                                                                && x.affiliation_deleted == false
                                                                && x.affiliation_record_type_id == AffiliationRecordTypes.StudentProgramAffiliation
                                                                && x.affiliation_organization_id != null
                                                                && x.affiliation_id == null)
                                                            .OrderByDescending(x=>x.affiliation_last_modified_datetime)
                                                            .Take(MaxRecordCount)
                                                            .ToList();
                
                AllAffiliations.AddRange(NewAffiliations);

                int DifferenceCount = MaxRecordCount - NewAffiliations.Count;

                if (DifferenceCount > 0)
                {
                    var ChangedAffiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_mark_delete == false
                                                                            && x.affiliation_deleted == false
                                                                            && x.affiliation_id != null
                                                                            && x.affiliation_record_type_id == AffiliationRecordTypes.StudentProgramAffiliation
                                                                            && x.affiliation_organization_id != null
                                                                            && (x.affiliation_last_modified_datetime >= LastRunDate
                                                                            || x.last_sfsync_datetime >= LastRunDate))
                                                                .OrderByDescending(x => x.affiliation_last_modified_datetime)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                    if (ChangedAffiliations.Count > 0)
                    {
                        AllAffiliations.AddRange(ChangedAffiliations);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - AllAffiliations.Count);
                    if (DifferenceCount > 0)
                    {
                        var BackloadAffiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_mark_delete == false
                                                                            && x.affiliation_deleted == false
                                                                            && x.affiliation_record_type_id == AffiliationRecordTypes.StudentProgramAffiliation
                                                                            && x.affiliation_organization_id != null
                                                                            && x.affiliation_last_modified_datetime < BackLoadPriorDate)
                                                                .OrderBy(x => x.affiliation_last_modified_datetime)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                        if (BackloadAffiliations.Count > 0)
                        {
                            AllAffiliations.AddRange(BackloadAffiliations);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), AllAffiliations.Count))
                {
                    AffiliationUpserts = new Dictionary<string, sObject>();
                    foreach (crm_affiliations DbAffiliation in AllAffiliations)
                    {
                        // map to salesforce object
                        hed__Affiliation__c SfAffiliation = MapToSfAffiliations(DbAffiliation);

                        // count inserts vs. updates
                        if (string.IsNullOrWhiteSpace(DbAffiliation.affiliation_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                        // prevent duplicate record upload
                        if (!AffiliationUpserts.Keys.Contains(SfAffiliation.lc_affiliation_guid__c))
                        {
                            AffiliationUpserts.Add(DbAffiliation.affiliation_guid.ToString(), SfAffiliation);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.Affiliation, AffiliationUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool UpsertApplications(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<crm_applications> AllApplications = new List<crm_applications>();
            Dictionary<string, sObject> ApplicationUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Application;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var NewApplications = db_ctx.crm_applications.Where(x => x.application_deleted == false
                                                                && x.appl_mark_delete == false
                                                                && x.crm_application_id == null)
                                                            .OrderByDescending(x=>x.appl_modfied_date)
                                                            .Take(MaxRecordCount)
                                                            .ToList();

                AllApplications.AddRange(NewApplications);

                int DifferenceCount = MaxRecordCount - AllApplications.Count;

                if (DifferenceCount > 0)
                {
                    var ChangedApplications = db_ctx.crm_applications.Where(x => x.application_deleted == false
                                                                            && x.appl_mark_delete == false
                                                                            && x.crm_application_id != null
                                                                            && (x.appl_modfied_date >= LastRunDate
                                                                            || x.last_sfsync_datetime >= LastRunDate))
                                                                    .OrderByDescending(x => x.appl_modfied_date)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                    if (ChangedApplications.Count > 0)
                    {
                        AllApplications.AddRange(ChangedApplications);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - AllApplications.Count);
                    if (DifferenceCount > 0)
                    {
                        var BackloadApplications = db_ctx.crm_applications.Where(x => x.application_deleted == false
                                                                                && x.appl_mark_delete == false
                                                                                && (x.last_sfsync_datetime == null
                                                                                || x.last_sfsync_datetime < BackLoadPriorDate))
                                                                        .OrderBy(x => x.last_sfsync_datetime)
                                                                        .Take(DifferenceCount)
                                                                        .ToList();

                        if (BackloadApplications.Count > 0)
                        {
                            AllApplications.AddRange(BackloadApplications);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), AllApplications.Count))
                {
                    foreach (crm_applications DbApplication in AllApplications)
                    {
                        // map to salesforce object
                        lc_application__c SfApplication = MapToSfApplications(DbApplication);

                        // count inserts vs. updates
                        if (string.IsNullOrWhiteSpace(DbApplication.crm_application_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                        // prevent duplicate record upload
                        if (!ApplicationUpserts.Keys.Contains(SfApplication.lc_application_guid__c))
                        {
                            ApplicationUpserts.Add(DbApplication.application_guid.ToString(), SfApplication);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.Application, ApplicationUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Upsert Event Regisatrations
        /// </summary>
        /// <returns></returns>
        private bool UpsertEventRegistrations(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> EventRegistrationUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.EventRegistration;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var DbEventRegistrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_deleted == false
                                                                            && x.event_registration_mark_delete == false
                                                                            && x.event_registration_id == null)
                                                                    .OrderByDescending(x=>x.event_registration_modified_datetime)
                                                                    .Take(MaxRecordCount)
                                                                    .ToList();

                int DifferenceCount = MaxRecordCount - DbEventRegistrations.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_event_registrations.Where(x => x.event_registration_deleted == false
                                                                                && x.event_registration_mark_delete == false
                                                                                && x.event_registration_id != null
                                                                                && (x.event_registration_modified_datetime >= LastRunDate
                                                                                || x.last_sfsync_datetime >= LastRunDate))
                                                                    .OrderByDescending(x => x.event_registration_modified_datetime)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbEventRegistrations.AddRange(ModifiedRecords);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbEventRegistrations.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_event_registrations.Where(x => x.event_registration_deleted == false
                                                                                && x.event_registration_mark_delete == false
                                                                                && (x.last_sfsync_datetime == null
                                                                                ||x.last_sfsync_datetime < BackLoadPriorDate))
                                                                    .OrderBy(x => x.event_registration_modified_datetime)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbEventRegistrations.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbEventRegistrations.Count))
                {
                    foreach (crm_event_registrations DbEventRegistration in DbEventRegistrations)
                    {
                        // map to salesforce object
                        lc_event_registration__c SfEventRegistration = MapToSfEventRegistrations(DbEventRegistration);

                        // count inserts vs. updates
                        if (string.IsNullOrWhiteSpace(DbEventRegistration.event_registration_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                        // prevent duplicate record upload
                        if (!EventRegistrationUpserts.Keys.Contains(SfEventRegistration.lc_event_registration_guid__c))
                        {
                            EventRegistrationUpserts.Add(DbEventRegistration.event_registration_guid.ToString(), SfEventRegistration);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.EventRegistration, EventRegistrationUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Upsert Email Broadcasts
        /// </summary>
        /// <returns></returns>
        private bool UpsertEmailBroadcasts(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> EmailBroadcastUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.EmailBroadcast;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var DbEmailBroadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
                                                                        && x.email_broadcast_mark_delete == false
                                                                        && x.email_broadcast_id == null
                                                                        && x.email_broadcast_modfied_datetime > LastRunDate)
                                                                    .OrderByDescending(x=>x.email_broadcast_modfied_datetime)
                                                                    .Take(MaxRecordCount)
                                                                    .ToList();

                int DifferenceCount = MaxRecordCount - DbEmailBroadcasts.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
                                                                        && x.email_broadcast_mark_delete == false
                                                                        && x.email_broadcast_id != null
                                                                        && (x.email_broadcast_modfied_datetime >= LastRunDate
                                                                        || x.last_sfsync_datetime >= LastRunDate))
                                                                    .OrderByDescending(x => x.email_broadcast_modfied_datetime)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbEmailBroadcasts.AddRange(ModifiedRecords);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbEmailBroadcasts.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
                                                                        && x.email_broadcast_mark_delete == false
                                                                        && (x.last_sfsync_datetime == null
                                                                        || x.last_sfsync_datetime < BackLoadPriorDate))
                                                                    .OrderBy(x => x.email_broadcast_modfied_datetime)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbEmailBroadcasts.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbEmailBroadcasts.Count))
                {
                    foreach (crm_email_broadcasts DbEmailBroadcast in DbEmailBroadcasts)
                    {
                        // map to salesforce object
                        lc_email_broadcast__c SfEmailBroadcast = MapToSfEmailBroadcasts(DbEmailBroadcast);

                        // count inserts vs. updates
                        if (string.IsNullOrWhiteSpace(DbEmailBroadcast.email_broadcast_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                        // prevent duplicate record upload
                        if (!EmailBroadcastUpserts.Keys.Contains(SfEmailBroadcast.lc_email_broadcast_guid__c))
                        {
                            EmailBroadcastUpserts.Add(DbEmailBroadcast.email_broadcast_guid.ToString(), SfEmailBroadcast);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.EmailBroadcast, EmailBroadcastUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Upsert Terms
        /// </summary>
        /// <returns></returns>
        private bool UpsertTerms(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> TermUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Term;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var DbTerms = db_ctx.crm_terms.Where(x => x.term_deleted == false
                                                                && x.term_mark_delete == false
                                                                && x.term_id == null)
                                                    .OrderByDescending(x => x.term_modifed_datetime)
                                                    .Take(MaxRecordCount)
                                                    .ToList();

                int DifferenceCount = MaxRecordCount - DbTerms.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_terms.Where(x => x.term_deleted == false
                                                                    && x.term_mark_delete == false
                                                                    && x.term_id != null
                                                                    && x.term_modifed_datetime >= LastRunDate)
                                                            .OrderByDescending(x => x.term_modifed_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbTerms.AddRange(ModifiedRecords);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbTerms.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_terms.Where(x => x.term_deleted == false
                                                                    && x.term_mark_delete == false
                                                                    && (x.last_sfsync_datetime == null
                                                                    || x.last_sfsync_datetime < BackLoadPriorDate))
                                                            .OrderBy(x => x.last_sfsync_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbTerms.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbTerms.Count))
                {
                    foreach (crm_terms DbTerm in DbTerms)
                    {
                        // map to salesforce object
                        hed__Term__c SfTerm = MapToSfTerms(DbTerm);

                        // count inserts vs. updates
                        if (string.IsNullOrWhiteSpace(DbTerm.term_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                        // prevent duplicate record upload
                        if (!TermUpserts.Keys.Contains(SfTerm.lc_term_guid__c))
                        {
                            TermUpserts.Add(DbTerm.term_guid.ToString(), SfTerm);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.Term, TermUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Upsert Activity Extenders
        /// </summary>
        /// <returns></returns>
        private bool UpsertActivityExtenders(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> ActivityExtenderUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.ActivityExtender;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var DbActivityExtenders = db_ctx.crm_activity_extender.Where(x => x.activity_extender_deleted == false
                                                                        && x.activity_extender_mark_delete == false
                                                                        && x.activity_extender_id == null)
                                                    .OrderByDescending(x => x.activity_extender_modified_datetime)
                                                    .Take(MaxRecordCount)
                                                    .ToList();

                int DifferenceCount = MaxRecordCount - DbActivityExtenders.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_activity_extender.Where(x => x.activity_extender_deleted == false
                                                                                && x.activity_extender_mark_delete == false
                                                                                && x.activity_extender_id != null
                                                                                && x.activity_extender_modified_datetime >= LastRunDate)
                                                            .OrderByDescending(x => x.activity_extender_modified_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbActivityExtenders.AddRange(ModifiedRecords);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbActivityExtenders.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_activity_extender.Where(x => x.activity_extender_deleted == false
                                                                                && x.activity_extender_mark_delete == false
                                                                                && (x.last_sfsync_datetime == null
                                                                                || x.last_sfsync_datetime < BackLoadPriorDate))
                                                            .OrderBy(x => x.last_sfsync_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbActivityExtenders.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbActivityExtenders.Count))
                {
                    foreach (crm_activity_extender DbActivityExtender in DbActivityExtenders)
                    {
                        // map to salesforce object
                        lc_activity_extender__c SfActivityExtender = MapToSfActivityExtenders(DbActivityExtender);

                        // count inserts vs. updates
                        if (string.IsNullOrWhiteSpace(DbActivityExtender.activity_extender_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                        // prevent duplicate record upload
                        if (!ActivityExtenderUpserts.Keys.Contains(SfActivityExtender.lc_activity_extender_guid__c))
                        {
                            ActivityExtenderUpserts.Add(DbActivityExtender.activity_extender_guid.ToString(), SfActivityExtender);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.ActivityExtender, ActivityExtenderUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Upsert Events
        /// </summary>
        /// <returns></returns>
        private bool UpsertEvents(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> EventUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Event;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var DbEvents = db_ctx.crm_events.Where(x => x.event_deleted == false
                                                               && x.event_mark_delete == false
                                                               && x.activity_id == null)
                                                        .OrderByDescending(x => x.event_modified_datetime)
                                                        .Take(MaxRecordCount)
                                                        .ToList();

                int DifferenceCount = MaxRecordCount - DbEvents.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_events.Where(x => x.event_deleted == false
                                                               && x.event_mark_delete == false
                                                               && x.activity_id != null
                                                               && x.event_modified_datetime >= LastRunDate)
                                                            .OrderByDescending(x => x.event_modified_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbEvents.AddRange(ModifiedRecords);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbEvents.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_events.Where(x => x.event_deleted == false
                                                                && x.event_mark_delete == false
                                                                && x.event_modified_datetime < BackLoadPriorDate)
                                                            .OrderByDescending(x => x.event_modified_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbEvents.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbEvents.Count))
                {
                    foreach (crm_events DbEvent in DbEvents)
                    {
                        // map to salesforce object
                        Event SfEvent = MapToSfEvents(DbEvent);

                        // count inserts vs. updates
                        if (string.IsNullOrWhiteSpace(DbEvent.activity_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                        // prevent duplicate record upload
                        if (!EventUpserts.Keys.Contains(SfEvent.activity_guid__c))
                        {
                            EventUpserts.Add(DbEvent.activity_guid.ToString(), SfEvent);
                        }
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.Event, EventUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Update Email Campaigns
        /// </summary>
        /// <returns></returns>
        private bool UpsertEmailCampaigns(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> EmailCampaignUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.EmailCampaign;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var DbEmailCampaigns = db_ctx.crm_email_campaigns.Where(x => x.ec_deleted == false
                                                                        && x.ec_mark_delete == false
                                                                        && x.email_campaign_id == null)
                                                                .OrderByDescending(x => x.ec_modified_datetime)
                                                        .Take(MaxRecordCount)
                                                        .ToList();

                int DifferenceCount = MaxRecordCount - DbEmailCampaigns.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_email_campaigns.Where(x => x.ec_deleted == false
                                                                        && x.ec_mark_delete == false
                                                                        && x.email_campaign_id != null
                                                                        && x.ec_modified_datetime >= LastRunDate)
                                                                .OrderByDescending(x => x.ec_modified_datetime)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbEmailCampaigns.AddRange(ModifiedRecords);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbEmailCampaigns.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_email_campaigns.Where(x => x.ec_deleted == false
                                                                                    && x.ec_mark_delete == false
                                                                                    && (x.last_sfsync_datetime == null
                                                                                    || x.last_sfsync_datetime < BackLoadPriorDate))
                                                                        .OrderBy(x => x.last_sfsync_datetime)
                                                                        .Take(DifferenceCount)
                                                                        .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbEmailCampaigns.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbEmailCampaigns.Count))
                {
                    foreach (crm_email_campaigns DbEmailCampaign in DbEmailCampaigns)
                    {
                        // map fields
                        lc_email_campaign__c SfEmailCampaign = MapToSfEmailCampaign(DbEmailCampaign);

                        // count inserts vs. updates
                        if (string.IsNullOrEmpty(DbEmailCampaign.email_campaign_id)) { InsertedCount++; } else { UpdatedCount++; }

                        // prevent duplicate record upload
                        if (!EmailCampaignUpserts.Keys.Contains(SfEmailCampaign.lc_email_campaign_guid__c))
                        {
                            EmailCampaignUpserts.Add(SfEmailCampaign.lc_email_campaign_guid__c.ToString(), SfEmailCampaign);
                        }

                        TotalCount++;
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.EmailCampaign, EmailCampaignUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Update Inquiries
        /// </summary>
        /// <returns></returns>
        private bool UpsertInquiries(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> InquiryUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Inquiry;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var DbInquiries = db_ctx.crm_inquiries.Where(x => x.inq_deleted == false
                                                                    && x.inquiry_mark_delete == false
                                                                    && x.inquiry_id == null)
                                                            .OrderByDescending(x => x.inq_modified_datetime)
                                                            .Take(MaxRecordCount)
                                                            .ToList();

                int DifferenceCount = MaxRecordCount - DbInquiries.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_inquiries.Where(x => x.inq_deleted == false
                                                                        && x.inquiry_mark_delete == false
                                                                        && x.inquiry_id != null
                                                                        && x.inq_modified_datetime >= LastRunDate)
                                                                .OrderByDescending(x => x.inq_modified_datetime)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbInquiries.AddRange(ModifiedRecords);
                    }
                }

                // Salesforce centric - no need to BackLoad this object for now
                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbInquiries.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_inquiries.Where(x => x.inq_deleted == false
                                                                        && x.inquiry_mark_delete == false
                                                                        && x.inq_modified_datetime < BackLoadPriorDate)
                                                                .OrderByDescending(x => x.inq_modified_datetime)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbInquiries.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbInquiries.Count))
                {
                    foreach (crm_inquiries DbInquiry in DbInquiries)
                    {
                        lc_inquiry__c SfInquiry = MapToSfInquiries(DbInquiry);

                        // count inserts vs. updates
                        if (string.IsNullOrEmpty(DbInquiry.inquiry_id)) { InsertedCount++; } else { UpdatedCount++; }

                        // prevent duplicate record upload
                        if (!InquiryUpserts.Keys.Contains(SfInquiry.lc_inquiry_guid__c))
                        {
                            InquiryUpserts.Add(DbInquiry.inquiry_guid.ToString(), SfInquiry);
                        }

                        TotalCount++;
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.Inquiry, InquiryUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }
        
        /// <summary>
        /// Update Email Templates (no current need)
        /// </summary>
        /// <returns></returns>
        private bool UpsertEmailTemplates(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> EmailTemplateUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.EmailTemplate;

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var DbEmailTemplates = db_ctx.crm_email_templates.Where(x => x.email_template_deleted == false
                                                                        && x.email_template_mark_delete == false
                                                                        && x.email_template_legacy_id == null
                                                                        && x.email_template_id == null)
                                                                .OrderByDescending(x => x.email_template_modified_datetime).Take(MaxRecordCount)
                                                            .Take(MaxRecordCount)
                                                            .ToList();

                int DifferenceCount = MaxRecordCount - DbEmailTemplates.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_email_templates.Where(x => x.email_template_deleted == false
                                                                        && x.email_template_mark_delete == false
                                                                        && x.email_template_id != null
                                                                        && x.email_template_modified_datetime >= LastRunDate)
                                                                .OrderByDescending(x => x.email_template_modified_datetime)
                                                                .Take(MaxRecordCount)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbEmailTemplates.AddRange(ModifiedRecords);
                    }
                }

                // Salesforce Centric - no need to backfill this object for now
                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbEmailTemplates.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_email_templates.Where(x => x.email_template_deleted == false
                                                                        && x.email_template_mark_delete == false
                                                                        && x.email_template_legacy_id == null
                                                                        && x.email_template_modified_datetime < BackLoadPriorDate)
                                                                .OrderByDescending(x => x.email_template_modified_datetime).Take(MaxRecordCount)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbEmailTemplates.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbEmailTemplates.Count))
                {
                    //do not need to push these back up at the moment
                    foreach (crm_email_templates DbEmailTemplate in DbEmailTemplates)
                    {
                        EmailTemplate SfEmailTemplate = MapToSfEmailTemplate(DbEmailTemplate);

                        // count inserts vs. updates
                        if (string.IsNullOrEmpty(DbEmailTemplate.email_template_id)) { InsertedCount++; } else { UpdatedCount++; }

                        TotalCount++;
                    }
                    // upsert records
                    success = UpsertRecords(InsertResults.EmailTemplate, EmailTemplateUpserts);

                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Update Dynamic Content (no current need)
        /// </summary>
        /// <returns></returns>
        private bool UpsertDynamicContent(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> DynamicContentUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.DynamicContent;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                var DbDynamicContents = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_deleted == false
                                                                        && x.crm_dynamic_content_mark_delete == false
                                                                        && x.crm_dynamic_content_id == null)
                                                            .OrderByDescending(x => x.crm_dynamic_content_last_modified_datetime).Take(MaxRecordCount)
                                                            .Take(MaxRecordCount)
                                                            .ToList();

                int DifferenceCount = MaxRecordCount - DbDynamicContents.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_deleted == false
                                                                        && x.crm_dynamic_content_mark_delete == false
                                                                        && x.crm_dynamic_content_id != null
                                                                        && x.last_sfsync_datetime >= LastRunDate)
                                                                .OrderByDescending(x => x.crm_dynamic_content_last_modified_datetime)
                                                                .Take(MaxRecordCount)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbDynamicContents.AddRange(ModifiedRecords);
                    }
                }

                // Salesforce Centric - no need to backfill this object for now
                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbDynamicContents.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_deleted == false
                                                                        && x.crm_dynamic_content_mark_delete == false
                                                                        && x.crm_dynamic_content_id != null
                                                                        && x.crm_dynamic_content_last_modified_datetime < BackLoadPriorDate)
                                                                .OrderByDescending(x => x.crm_dynamic_content_last_modified_datetime).Take(MaxRecordCount)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbDynamicContents.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbDynamicContents.Count))
                {
                    foreach (crm_dynamic_content DbDynamicContent in DbDynamicContents)
                    {
                        lc_dynamic_content__c SfDynamicContent = MapToSfDynamicContent(DbDynamicContent);

                        // count inserts vs. updates
                        if (string.IsNullOrEmpty(DbDynamicContent.crm_dynamic_content_id)) { InsertedCount++; } else { UpdatedCount++; }

                        // prevent duplicate record upload
                        if (!DynamicContentUpserts.Keys.Contains(SfDynamicContent.lc_dynamic_content_guid__c))
                        {
                            DynamicContentUpserts.Add(DbDynamicContent.crm_dynamic_content_guid.ToString(), SfDynamicContent);
                        }

                        TotalCount++;
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.DynamicContent, DynamicContentUpserts); ;

                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Update Dynamic Content (no current need)
        /// </summary>
        /// <returns></returns>
        private bool UpsertInquiryPrograms(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> InquiryProgramUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.InquiryProgram;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                var DbInquiryPrograms = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_deleted == false
                                                                        && x.crm_inq_prog_mark_delete == false
                                                                        && x.crm_inq_prog_id == null)
                                                            .OrderByDescending(x => x.crm_inq_prog_last_modified_datetime).Take(MaxRecordCount)
                                                            .Take(MaxRecordCount)
                                                            .ToList();

                int DifferenceCount = MaxRecordCount - DbInquiryPrograms.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_deleted == false
                                                                        && x.crm_inq_prog_mark_delete == false
                                                                        && x.crm_inq_prog_id != null
                                                                        && (x.crm_inq_prog_last_modified_datetime >= LastRunDate))
                                                                .OrderByDescending(x => x.crm_inq_prog_last_modified_datetime)
                                                                .Take(MaxRecordCount)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbInquiryPrograms.AddRange(ModifiedRecords);
                    }
                }

                // Salesforce Centric - no need to backfill this object for now
                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbInquiryPrograms.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_deleted == false
                                                                        && x.crm_inq_prog_mark_delete == false
                                                                        && x.crm_inq_prog_id != null
                                                                        && x.crm_inq_prog_last_modified_datetime < BackLoadPriorDate)
                                                                .OrderByDescending(x => x.crm_inq_prog_last_modified_datetime).Take(MaxRecordCount)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbInquiryPrograms.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbInquiryPrograms.Count))
                {
                    foreach (crm_inquiry_programs DbInquiryProgram in DbInquiryPrograms)
                    {
                        lc_inquiry_program__c SfInquiryProgram = MapToSfInquiryProgram(DbInquiryProgram);

                        // count inserts vs. updates
                        if (string.IsNullOrEmpty(DbInquiryProgram.crm_inq_prog_id)) { InsertedCount++; } else { UpdatedCount++; }

                        // prevent duplicate record upload
                        if (!InquiryProgramUpserts.Keys.Contains(SfInquiryProgram.lc_inquiry_program_guid__c))
                        {
                            InquiryProgramUpserts.Add(DbInquiryProgram.crm_inq_prog_guid.ToString(), SfInquiryProgram);
                        }

                            TotalCount++;
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.InquiryProgram, InquiryProgramUpserts); ;

                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Update Email Templates (no current need)
        /// </summary>
        /// <returns></returns>
        private bool UpsertAcademicPrograms(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<crm_accounts> AllAcademicPrograms = new List<crm_accounts>();
            Dictionary<string, sObject> AcademicProgramUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Program;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                var NewAcademicPrograms = db_ctx.crm_accounts.Where(x => x.account_record_type_id == AccountRecordTypes.AcademicProgram
                                                                && x.account_deleted == false
                                                                && x.account_mark_delete == false
                                                                && x.account_id == null)
                                                        .OrderByDescending(x => x.account_modifed_datetime).Take(MaxRecordCount)
                                                        .Take(MaxRecordCount)
                                                        .ToList();

                AllAcademicPrograms.AddRange(NewAcademicPrograms);

                int DifferenceCount = MaxRecordCount - AllAcademicPrograms.Count;

                if (DifferenceCount > 0)
                {
                    var ChangedAcademicPrograms = db_ctx.crm_accounts.Where(x => x.account_record_type_id == AccountRecordTypes.AcademicProgram
                                                                                && x.account_deleted == false
                                                                                && x.account_mark_delete == false
                                                                                && x.account_id != null
                                                                                && x.account_modifed_datetime >= LastRunDate)
                                                                    .OrderByDescending(x => x.account_modifed_datetime)
                                                                    .Take(MaxRecordCount)
                                                                    .Take(DifferenceCount)
                                                                    .ToList();

                    if (ChangedAcademicPrograms.Count > 0)
                    {
                        AllAcademicPrograms.AddRange(ChangedAcademicPrograms);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - AllAcademicPrograms.Count);
                    if (DifferenceCount > 0)
                    {
                        var BackloadAcademicPrograms = db_ctx.crm_accounts.Where(x => x.account_record_type_id == AccountRecordTypes.AcademicProgram
                                                                        && x.account_deleted == false
                                                                        && x.account_mark_delete == false
                                                                        && (x.last_sfsync_datetime == null
                                                                        || x.last_sfsync_datetime < BackLoadPriorDate))
                                                            .OrderBy(x => x.last_sfsync_datetime)
                                                            .Take(MaxRecordCount)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                        if (BackloadAcademicPrograms.Count > 0)
                        {
                            AllAcademicPrograms.AddRange(BackloadAcademicPrograms);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), AllAcademicPrograms.Count))
                {
                    foreach (crm_accounts DbProgram in AllAcademicPrograms)
                    {
                        Account SfProgram = MapToSfAcademicPrograms(DbProgram);

                        if (!string.IsNullOrWhiteSpace(DbProgram.account_id)) { UpdatedCount++; } else { InsertedCount++; }

                        // count inserts vs. updates
                        if (string.IsNullOrEmpty(DbProgram.account_id)) { InsertedCount++; } else { UpdatedCount++; }

                        // prevent duplicate record upload
                        if (!AcademicProgramUpserts.Keys.Contains(SfProgram.lc_account_guid__c))
                        {
                            AcademicProgramUpserts.Add(DbProgram.account_guid.ToString(), SfProgram);
                        }

                        TotalCount++;
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.AcademicProgram, AcademicProgramUpserts);



                    success = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Update Role Values (no current need)
        /// </summary>
        /// <returns></returns>
        private bool UpsertRoleValues(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> RoleValueUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.RoleValue;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                var DbRoleValues = db_ctx.crm_role_values.Where(x => x.crm_rv_deleted_flag == false
                                                                    && x.crm_rv_mark_delete == false
                                                                    && x.crm_role_value_id == null)
                                                            .OrderByDescending(x => x.crm_rv_modified_datetime).Take(MaxRecordCount)
                                                            .Take(MaxRecordCount)
                                                            .ToList();

                int DifferenceCount = MaxRecordCount - DbRoleValues.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_role_values.Where(x => x.crm_rv_deleted_flag == false
                                                                            && x.crm_rv_mark_delete == false
                                                                            && x.crm_role_value_id != null
                                                                            && x.crm_rv_modified_datetime >= LastRunDate)
                                                                .OrderByDescending(x => x.crm_rv_modified_datetime)
                                                                .Take(MaxRecordCount)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbRoleValues.AddRange(ModifiedRecords);
                    }
                }

                // Salesforce Object - no need to backfill for now
                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - DbRoleValues.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_role_values.Where(x => x.crm_rv_deleted_flag == false
                                                                            && x.crm_rv_mark_delete == false
                                                                            && x.crm_rv_modified_datetime < BackLoadPriorDate)
                                                                .OrderByDescending(x => x.crm_rv_modified_datetime).Take(MaxRecordCount)
                                                                .Take(DifferenceCount)
                                                                .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            DbRoleValues.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), DbRoleValues.Count))
                {
                    foreach (crm_role_values DbRoleValue in DbRoleValues)
                    {
                        lc_role_values__c SfRoleValue = MapToSfRoleValues(DbRoleValue);

                        // count inserts vs. updates
                        if (string.IsNullOrEmpty(DbRoleValue.crm_role_value_id)) { InsertedCount++; } else { UpdatedCount++; }

                        // prevent duplicate record upload
                        if (!RoleValueUpserts.Keys.Contains(SfRoleValue.lc_role_value_guid__c))
                        {
                            RoleValueUpserts.Add(DbRoleValue.crm_role_value_guid.ToString(), SfRoleValue);
                        }

                        TotalCount++;
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.RoleValues, RoleValueUpserts);

                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Upsert Tasks
        /// </summary>
        /// <returns></returns>
        private bool UpsertTasks(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            Dictionary<string, sObject> TaskUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Task;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                //var DbTasks = db_ctx.crm_tasks.Where(x => x.task_deleted == false
                //                                    && x.task_mark_delete == false
                //                                    && (x.activity_id == null
                //                                        || (x.task_created_datetime >= LastRunDate
                //                                        || x.task_modified_datetime >= LastRunDate)))
                //                                    .OrderByDescending(x => x.task_modified_datetime)
                //                                    .Take(MaxRecordCount)
                //                                    .ToList();

                //int DifferenceCount = MaxRecordCount - DbTasks.Count;

                //if (DifferenceCount > 0)
                //{
                //    var ModifiedRecords = db_ctx.crm_tasks.Where(x => x.task_deleted == false
                //                                                    && x.task_mark_delete == false
                //                                                    && x.activity_id != null
                //                                                    && x.task_modified_datetime >= LastRunDate)
                //                                            .OrderByDescending(x => x.task_modified_datetime)
                //                                            .Take(DifferenceCount)
                //                                            .ToList();

                //    if (ModifiedRecords.Count > 0)
                //    {
                //        DbTasks.AddRange(ModifiedRecords);
                //    }
                //}

                //if (BackLoad)
                //{
                //    DifferenceCount = (MaxRecordCount - DbTasks.Count);
                //    if (DifferenceCount > 0)
                //    {
                //        var ModifiedRecords = db_ctx.crm_tasks.Where(x => x.task_deleted == false
                //                                                        && x.task_mark_delete == false
                //                                                        && (x.last_sfsync_datetime == null
                //                                                        || x.last_sfsync_datetime < BackLoadPriorDate))
                //                                            .OrderBy(x => x.last_sfsync_datetime)
                //                                            .Take(DifferenceCount)
                //                                            .ToList();

                //        if (ModifiedRecords.Count > 0)
                //        {
                //            DbTasks.AddRange(ModifiedRecords);
                //        }
                //    }
                //}

                //if (MonitorExecution(GetCurrentMethod(), DbTasks.Count))
                //{
                //    foreach (crm_tasks DbTask in DbTasks)
                //    {
                //        // map to salesforce object
                //        Task SfTask = MapToSfTasks(DbTask);

                //        // count inserts vs. updates
                //        if (string.IsNullOrWhiteSpace(DbTask.activity_id)) { InsertedCount++; } else { UpdatedCount++; } TotalCount++;

                //        // prevent duplicate record upload
                //        if (!TaskUpserts.Keys.Contains(SfTask.activity_guid__c))
                //        {
                //            TaskUpserts.Add(DbTask.activity_guid.ToString(), SfTask);
                //        }
                //    }

                //    // upsert records
                //    success = UpsertRecords(InsertResults.Task, TaskUpserts);
                //}
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Update Email Templates (no current need)
        /// </summary>
        /// <returns></returns>
        private bool UpsertReports(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            Dictionary<string, sObject> ReportUpserts = new Dictionary<string, sObject>();

            try
            {
                MethodName = LastModified.Report;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                var DbReports = db_ctx.crm_reports.Where(x => x.crm_report_deleted == false
                                                                && x.crm_report_mark_delete == false
                                                                && x.crm_report_id == null)
                                                        .OrderByDescending(x => x.crm_report_modified_date).Take(MaxRecordCount)
                                                        .Take(MaxRecordCount)
                                                        .ToList();

                int DifferenceCount = MaxRecordCount - DbReports.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_reports.Where(x => x.crm_report_deleted == false
                                                                        && x.crm_report_mark_delete == false
                                                                        && x.crm_report_id != null
                                                                        && x.crm_report_modified_date >= LastRunDate)
                                                            .OrderByDescending(x => x.crm_report_modified_date)
                                                            .Take(MaxRecordCount)
                                                            .Take(DifferenceCount)
                                                            .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        DbReports.AddRange(ModifiedRecords);
                    }
                }

                // Salesforce object - no need to backfill for now
                //if (BackLoad)
                //{
                //    DifferenceCount = (MaxRecordCount - UpsertRecords.Count);
                //    if (DifferenceCount > 0)
                //    {
                //        var ModifiedRecords = db_ctx.crm_reports.Where(x => x.crm_report_deleted == false
                //                                                        && x.crm_report_mark_delete == false
                //                                                        && x.crm_report_modified_date < BackLoadPriorDate)
                //                                            .OrderByDescending(x => x.crm_report_modified_date).Take(MaxRecordCount)
                //                                            .Take(DifferenceCount)
                //                                            .ToList();

                //        if (ModifiedRecords.Count > 0)
                //        {
                //            UpsertRecords.AddRange(ModifiedRecords);
                //        }
                //    }
                //}

                if (MonitorExecution(GetCurrentMethod(), ReportUpserts.Count))
                {
                    foreach (crm_reports DbReport in DbReports)
                    {
                        Report SfReport = MapToSfReports(DbReport);

                        // count inserts vs. updates
                        if (string.IsNullOrEmpty(DbReport.crm_report_id)) { InsertedCount++; } else { UpdatedCount++; }

                        // prevent duplicate record upload
                        //if (!ReportUpserts.Keys.Contains(SfReport.lc_object_guid__c))
                        //{
                        //    ReportUpserts.Add(DbReport.report_guid.ToString(), SfReport);
                        //}

                        TotalCount++;
                    }

                    // upsert records
                    success = UpsertRecords(InsertResults.Report, ReportUpserts);

                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Upsert Program Enrollments
        /// </summary>
        /// <returns></returns>
        private bool UpsertProgramEnrollments(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<sObject> SalesforceInserts = new List<sObject>();
            List<sObject> SalesforceUpdates = new List<sObject>();

            List<Guid> SalesforceInsertKeys = new List<Guid>();

            try
            {
                MethodName = LastModified.ProgramEnrollment;

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                var UpsertRecords = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_deleted == false
                                                                            && x.crm_program_enrollment_mark_delete == false
                                                                            && x.crm_program_enrollment_id == null)
                                                                    .OrderByDescending(x => x.crm_program_enrollment_modified_datetime).Take(MaxRecordCount)
                                                                    .Take(MaxRecordCount)
                                                                    .ToList();

                int DifferenceCount = MaxRecordCount - UpsertRecords.Count;

                if (DifferenceCount > 0)
                {
                    var ModifiedRecords = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_deleted == false
                                                                                    && x.crm_program_enrollment_mark_delete == false
                                                                                    && x.crm_program_enrollment_id != null
                                                                                    && x.crm_program_enrollment_modified_datetime >= LastRunDate)
                                                                            .OrderByDescending(x => x.crm_program_enrollment_modified_datetime).Take(MaxRecordCount)
                                                                            .Take(DifferenceCount)
                                                                            .ToList();

                    if (ModifiedRecords.Count > 0)
                    {
                        UpsertRecords.AddRange(ModifiedRecords);
                    }
                }

                if (BackLoad)
                {
                    DifferenceCount = (MaxRecordCount - UpsertRecords.Count);
                    if (DifferenceCount > 0)
                    {
                        var ModifiedRecords = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_deleted == false
                                                                                    && x.crm_program_enrollment_mark_delete == false
                                                                                    && x.crm_program_enrollment_modified_datetime < BackLoadPriorDate)
                                                                            .OrderByDescending(x => x.crm_program_enrollment_modified_datetime).Take(MaxRecordCount)
                                                                            .Take(DifferenceCount)
                                                                            .ToList();

                        if (ModifiedRecords.Count > 0)
                        {
                            UpsertRecords.AddRange(ModifiedRecords);
                        }
                    }
                }

                if (MonitorExecution(GetCurrentMethod(), UpsertRecords.Count))
                {
                    foreach (crm_program_enrollments DbEnrollment in UpsertRecords)
                    {
                        hed__Program_Enrollment__c SfEnrollment = new hed__Program_Enrollment__c
                        {
                            Id = DbEnrollment.crm_program_enrollment_id
                        };

                        if (string.IsNullOrWhiteSpace(DbEnrollment.crm_program_enrollment_id))
                        {
                            SfEnrollment.lc_program_enrollment_guid__c = Guid.NewGuid().ToString();

                            SalesforceInserts.Add(SfEnrollment);
                            SalesforceInsertKeys.Add(DbEnrollment.crm_program_enrollment_guid);
                            InsertedCount++;
                        }
                        else
                        {
                            SfEnrollment.lc_program_enrollment_guid__c = DbEnrollment.crm_program_enrollment_guid.ToString();

                            SalesforceUpdates.Add(SfEnrollment);
                            UpdatedCount++;
                        }

                        SfEnrollment.hed__Contact__c = DbEnrollment.crm_program_enrollment_contact_id;
                        SfEnrollment.hed__Account__c = DbEnrollment.crm_program_enrollment_program_id;

                        if (DbEnrollment.crm_program_enrollment_start_datetime != null)
                        {
                            SfEnrollment.hed__Start_Date__cSpecified = true;
                            SfEnrollment.hed__Start_Date__c = DbEnrollment.crm_program_enrollment_start_datetime;
                        }

                        SfEnrollment.OwnerId = Settings.CrmAdmin;

                        if (DbEnrollment.crm_program_enrollment_submitted_date != null)
                        {
                            SfEnrollment.hed__Application_Submitted_Date__cSpecified = true;
                            SfEnrollment.hed__Application_Submitted_Date__c = GetUTCTime(DbEnrollment.crm_program_enrollment_submitted_date);
                        }

                        if (DbEnrollment.crm_program_enrollment_end_datetime != null)
                        {
                            SfEnrollment.hed__End_Date__cSpecified = true;
                            SfEnrollment.hed__End_Date__c = GetUTCTime(DbEnrollment.crm_program_enrollment_end_datetime);
                        }

                        SfEnrollment.lc_crmdb_last_sync__c = DateTime.Now;
                        SfEnrollment.lc_crmdb_last_sync__cSpecified = true;

                        TotalCount++;
                    }

                    InsertRecords(SalesforceInserts, SalesforceInsertKeys, InsertResults.ProgramEnrollment, 100);

                    List<SaveResult> results = UpdateRecords(SalesforceUpdates);

                    foreach (SaveResult r in results)
                    {
                        if (!r.success)
                        {
                            foreach (Error error in r.errors)
                            {
                                if (error.statusCode == StatusCode.ENTITY_IS_DELETED)
                                {
                                    if (!string.IsNullOrWhiteSpace(r.id))
                                    {
                                        var program_enrollment = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_id == r.id).FirstOrDefault();
                                        program_enrollment.crm_program_enrollment_deleted = true;
                                        program_enrollment.crm_program_enrollment_mark_delete = false;
                                        db_ctx.SaveChanges();
                                    }
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), error.message);
                                }
                            }
                        }
                    }

                    success = true;

                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        /// <summary>
        /// Backfill International
        /// </summary>
        /// <returns></returns>
        private bool UpsertInternational(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int total = 0;
            int updated = 0;
            int inserted = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<Guid> SalesforceInsertKeys = new List<Guid>();
            List<sObject> SalesforceInserts = new List<sObject>();
            List<sObject> SalesforceUpdates = new List<sObject>();
            Dictionary<Guid, string> insert_results = new Dictionary<Guid, string>();
            List<crm_contacts> DbContacts = new List<crm_contacts>();

            try
            {
                MethodName = GetCurrentMethod();

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                DbContacts = db_ctx.crm_contacts.Where(x => x.contact_international == true).ToList();

                if (MonitorExecution(GetCurrentMethod(), DbContacts.Count))
                {

                    foreach (crm_contacts DbContact in DbContacts)
                    {
                        Contact SfContact = new Contact();

                        if (!string.IsNullOrWhiteSpace(DbContact.contact_id))
                        {
                            SfContact.Id = DbContact.contact_id;

                            SfContact.lc_international_flag__c = DbContact.contact_international;
                            SfContact.lc_international_flag__cSpecified = true;

                            SfContact.lc_crmdb_last_sync__c = DateTime.Now;
                            SfContact.lc_crmdb_last_sync__cSpecified = true;

                            SalesforceUpdates.Add(SfContact);
                            updated++;
                        }

                        total++;
                    }

                    List<SaveResult> results = UpdateRecords(SalesforceUpdates);

                    foreach (SaveResult r in results)
                    {
                        if (!r.success)
                        {
                            foreach (Error error in r.errors)
                            {
                                if (error.statusCode == StatusCode.ENTITY_IS_DELETED)
                                {
                                    if (!string.IsNullOrWhiteSpace(r.id))
                                    {
                                        var contact = db_ctx.crm_contacts.Where(x => x.contact_id == r.id).FirstOrDefault();
                                        contact.contact_deleted = true;
                                        contact.contact_mark_delete = false;
                                        db_ctx.SaveChanges();
                                    }
                                }
                                else
                                {
                                    RecordError(GetCurrentMethod(), error.message);
                                }
                            }
                        }
                    }

                    success = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        public bool TestUpsertInquiryPrograms(string InquiryId, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;

            Dictionary<string, sObject> InquiryProgramUpserts = new Dictionary<string, sObject>();

            try
            {
                if (!string.IsNullOrWhiteSpace(InquiryId))
                {
                    // Get active academic programs
                    var DbAcademicPrograms = db_ctx.crm_accounts.Where(x => x.account_record_type_id == AccountRecordTypes.AcademicProgram
                                                && x.account_deleted == false
                                                && x.account_mark_delete == false
                                                && x.account_active == true
                                                && x.account_show_web == true)
                                        .OrderByDescending(x => x.account_modifed_datetime)
                                        .ToList();
                    foreach (var DbAcademicProgram in DbAcademicPrograms)
                    {
                        // Get Inquiry Programs
                        var DbInquiryPrograms = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_deleted == false
                                                                        && x.crm_inq_prog_mark_delete == false
                                                                        && x.crm_inq_prog_id != null
                                                                        && x.crm_inq_prog_inquiry_id == InquiryId
                                                                        && x.crm_inq_prog_program_id == DbAcademicProgram.account_id
                                                                        && x.crm_inq_prog_last_modified_datetime < BackLoadPriorDate)
                                                                .OrderByDescending(x => x.crm_inq_prog_last_modified_datetime).Take(MaxRecordCount)
                                                                .ToList();
                        if (DbInquiryPrograms != null && DbInquiryPrograms.Any())
                        {
                            var DbInquiryProgram = DbInquiryPrograms.OrderByDescending(x => x.crm_inq_prog_last_modified_datetime).FirstOrDefault();

                            DbInquiryProgram.crm_inq_prog_ack_sent = null;  // Mark it to re-send
                            DbInquiryProgram.crm_inq_prog_last_modified_datetime = DateTime.Now;

                            lc_inquiry_program__c SfInquiryProgram = MapToSfInquiryProgram(DbInquiryProgram);

                            // count inserts vs. updates
                            if (string.IsNullOrEmpty(DbInquiryProgram.crm_inq_prog_id)) { InsertedCount++; } else { UpdatedCount++; }

                            // prevent duplicate record upload
                            if (!InquiryProgramUpserts.Keys.Contains(SfInquiryProgram.lc_inquiry_program_guid__c))
                            {
                                InquiryProgramUpserts.Add(DbInquiryProgram.crm_inq_prog_guid.ToString(), SfInquiryProgram);
                            }

                            TotalCount++;
                        }
                    }

                    db_ctx.SaveChanges();

                    // upsert records
                    success = UpsertRecords(InsertResults.InquiryProgram, InquiryProgramUpserts);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            return success;
        }

    }
}

/// <summary>
/// Backfill ContactScanCodes
/// </summary>
/// <returns></returns>
//private bool UploadContactScanCodes()
//{
//    int total = 0;
//    int updated = 0;
//    int inserted = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    List<Guid> SalesforceInsertKeys = new List<Guid>();
//    List<sObject> SalesforceInserts = new List<sObject>();
//    List<sObject> SalesforceUpdates = new List<sObject>();
//    Dictionary<Guid, string> insert_results = new Dictionary<Guid, string>();
//    List<crm_contacts> DbContacts = new List<crm_contacts>();

//    try
//    {
//        MethodName = GetCurrentMethod();

//        LastRunDate = GetLastRunDateTime(MethodName);

//        DbContacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
//                                           && (
//                                                (x.contact_last_modified_datetime >= LastRunDate
//                                                    || x.contact_created_datetime >= LastRunDate)
//                                                || x.contact_id == null)
//                                              )
//                                             .OrderByDescending(x => x.contact_last_modified_datetime)
//                                             .ToList();

//        if (ProcessRecords(method, DbContacts.Count))
//        {

//            foreach (crm_contacts DbContact in DbContacts)
//            {
//                Contact SfContact = new Contact();

//                if (string.IsNullOrWhiteSpace(DbContact.contact_id))
//                {
//                    SfContact = MapContactDbToSf(DbContact);
//                    SalesforceInserts.Add(SfContact);
//                    SalesforceInsertKeys.Add(DbContact.contact_guid);
//                    inserted++;
//                }
//                else
//                {
//                    SfContact.Id = DbContact.contact_id;
//                    SfContact = MapContactDbToSf(DbContact);
//                    SalesforceUpdates.Add(SfContact);
//                    updated++;
//                }

//                SfContact.lc_crmdb_last_sync__c = DateTime.Now;
//                SfContact.lc_crmdb_last_sync__cSpecified = true;

//                total++;
//            }

//            UpdateRecords(SalesforceUpdates);
//            InsertRecords(SalesforceInserts, SalesforceInsertKeys, InsertResults.Contact);

//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg = ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

//    return success;
//}

//private bool UploadAcademicPrograms(bool BackLoad, DateTime BackLoadPriorDate, int MaxRecordCount = 0)
//{
//    int UpdatedCount = 0;
//    int InsertedCount = 0;
//    int TotalCount = 0;

//    bool success = false;
//    string ErrorMsg = string.Empty;
//    string MethodName = string.Empty;
//    DateTime StartTime = DateTime.Now;

//    Dictionary<string, sObject> AcademicProgramUpserts = new Dictionary<string, sObject>();

//    try
//    {
//        MethodName = LastModified.Account;

//        DateTime? LastRunDate = GetLastRunDateTime(MethodName);

//        var DbAcademicProgramUpserts = db_ctx.crm_accounts.Where(x => x.account_record_type_id != AccountRecordTypes.AcademicProgram
//                                                        && x.account_deleted == false
//                                                        && x.account_mark_delete == false
//                                                        && x.account_id == null)
//                                                .Take(MaxRecordCount)
//                                                .ToList();

//        int DifferenceCount = (MaxRecordCount - DbAcademicProgramUpserts.Count);

//        if (DifferenceCount > 0)
//        {
//            var ModifiedRecords = db_ctx.crm_accounts.Where(x => x.account_record_type_id != AccountRecordTypes.AcademicProgram
//                                                                && x.account_deleted == false
//                                                                && x.account_mark_delete == false
//                                                                && x.account_id != null
//                                                                && x.account_modifed_datetime >= LastRunDate)
//                                                        .OrderByDescending(x => x.account_modifed_datetime)
//                                                        .Take(DifferenceCount)
//                                                        .ToList();

//            if (ModifiedRecords.Count > 0)
//            {
//                DbAcademicProgramUpserts.AddRange(ModifiedRecords);
//            }
//        }

//        if (BackLoad)
//        {
//            DifferenceCount = (MaxRecordCount - DbAcademicProgramUpserts.Count);
//            if (DifferenceCount > 0)
//            {
//                var ModifiedRecords = db_ctx.crm_accounts.Where(x => x.account_record_type_id != AccountRecordTypes.AcademicProgram
//                                                                    && x.account_deleted == false
//                                                                    && x.account_mark_delete == false
//                                                                    && (x.last_sfsync_datetime == null
//                                                                    || x.last_sfsync_datetime < BackLoadPriorDate))
//                                                            .OrderBy(x => x.last_sfsync_datetime)
//                                                            .Take(DifferenceCount)
//                                                            .ToList();

//                if (ModifiedRecords.Count > 0)
//                {
//                    DbAcademicProgramUpserts.AddRange(ModifiedRecords);
//                }
//            }
//        }

//        if (MonitorExecution(GetCurrentMethod(), DbAcademicProgramUpserts.Count))
//        {

//            foreach (crm_accounts DbAcademicProgram in DbAcademicProgramUpserts)
//            {

//                Account SfAcademicProgram = new Account();

//                List<string> FieldsToNull = new List<string>();

//                if (string.IsNullOrWhiteSpace(DbAcademicProgram.account_id))
//                {
//                    InsertedCount++;
//                }
//                else
//                {
//                    UpdatedCount++;
//                }

//                if (DbAcademicProgram.account_id != SfAcademicProgram.Id)
//                {
//                    SfAcademicProgram.Id = DbAcademicProgram.account_id;
//                }

//                SfAcademicProgram.lc_account_guid__c = DbAcademicProgram.account_guid.ToString();



//                SfAcademicProgram.AccountNumber = DbAccount.account_number;

//                SfAcademicProgram.lc_crmdb_last_sync__cSpecified = true;
//                SfAcademicProgram.lc_crmdb_last_sync__c = DateTime.Now;

//                SalesforceUpdates.Add(SfAccount);
//                UpdatedCount++;
//                //}
//                //else
//                //{
//                //    SfAccount.lc_account_guid__c = Guid.NewGuid().ToString();

//                //    SfAccount.Id = DbAccount.account_id;
//                //    SfAccount.AccountNumber = DbAccount.account_number;

//                //    if (DbAccount.account_modifed_datetime != null)
//                //    {
//                //        SfAccount.LastModifiedById = DbAccount.account_modified_by;
//                //        SfAccount.LastModifiedDateSpecified = true;
//                //        SfAccount.LastModifiedDate = GetLocalTime(DbAccount.account_modifed_datetime);
//                //    }

//                //    SfAccount.CreatedDate = DbAccount.account_created_date ?? DateTime.Now;
//                //    SfAccount.CreatedDateSpecified = (DbAccount.account_created_date != null);

//                //    SalesforceInserts.Add(SfAccount);
//                //    SalesforceInsertKeys.Add(DbAccount.account_guid);

//                //    InsertedCount++;
//                //}

//                //if (!string.IsNullOrWhiteSpace(SfAccount.Id)) {
//                //    if (DbAccount.account_guid != null)
//                //    {
//                //        SfAccount.lc_account_guid__c = DbAccount.account_guid.ToString();
//                //    }

//                //    if (DbAccount.account_record_type_id == AccountRecordTypes.UniversityDepartment)
//                //    {
//                //        SfAccount.lc_department_type__c = DbAccount.account_department_type;
//                //        SfAccount.ParentId = Settings.LcAccountId;
//                //    }

//                //    // , AccountNumber
//                //    // SEE ABOVE
//                //    // , AccountSource
//                //    // , AnnualRevenue
//                //    //if (DbAccount.AnnualRevenue != null) { SfAccount.account_annual_revenue = (decimal)DbAccount.AnnualRevenue; }
//                //    // , BillingAddress
//                //    // , BillingCity
//                //    SfAccount.BillingCity = DbAccount.account_billing_city;
//                //    // , BillingCountry
//                //    SfAccount.BillingCountry = DbAccount.account_billing_country;
//                //    // , BillingGeocodeAccuracy
//                //    // , BillingLatitude
//                //    // , BillingLongitude
//                //    // , BillingPostalCode
//                //    SfAccount.BillingPostalCode = DbAccount.account_billing_postalcode;
//                //    // , BillingState
//                //    SfAccount.BillingState = DbAccount.account_billing_province;
//                //    // , BillingStreet
//                //    SfAccount.BillingStreet = DbAccount.account_billing_street;
//                //    // , CreatedById
//                //    // , CreatedDate
//                //    // , Description
//                //    // , Fax
//                //    SfAccount.Fax = DbAccount.account_fax;
//                //    // , hed__Current_Address__c
//                //    SfAccount.hed__Current_Address__c = DbAccount.account_current_address;
//                //    // , hed__Primary_Contact__c
//                //    // , Id
//                //    // , Industry
//                //    SfAccount.Industry = DbAccount.account_industry;
//                //    // , IsDeleted
//                //    //if (DbAccount.account_deleted) { SfAccount.IsDeletedSpecified = DbAccount.account_deleted; }
//                //    // , Jigsaw
//                //    // , JigsawCompanyId
//                //    // , LastActivityDate
//                //    //if (DbAccount.account_last_activity_date != null) {
//                //    //    SfAccount.LastActivityDateSpecified = true;
//                //    //    SfAccount.LastActivityDate = GetUTCTime(DbAccount.account_last_activity_date);
//                //    //}                    
//                //    // , LastModifiedById, LastModifiedDate
//                //    //if (DbAccount.account_modifed_datetime != null)
//                //    //{
//                //    //    SfAccount.LastModifiedById = DbAccount.account_modified_by;
//                //    //    SfAccount.LastModifiedDateSpecified = true;
//                //    //    SfAccount.LastModifiedDate = GetLocalTime(DbAccount.account_modifed_datetime);
//                //    //}
//                //    // , LastReferencedDate
//                //    // , LastViewedDate
//                //    // , lc_account_active__c
//                //    SfAccount.lc_account_active__cSpecified = true;
//                //    SfAccount.lc_account_active__c = DbAccount.account_active;
//                //    // , lc_account_type__c
//                //    SfAccount.lc_account_type__c = DbAccount.account_type;
//                //    // , lc_colleague_account_id__c
//                //    // , MasterRecordId
//                //    // , Name
//                //    SfAccount.Name = DbAccount.account_name;
//                //    // , NumberOfEmployees
//                //    // , OwnerId
//                //    SfAccount.OwnerId = Settings.CrmAdmin;
//                //    // , ParentId
//                //    //SfAccount.ParentId = Settings.LcAccountId;

//                //    // Need logic to separate out account parents //

//                //    // , Phone
//                //    SfAccount.Phone = DbAccount.account_phone;
//                //    // , PhotoUrl
//                //    // , RecordTypeId
//                //    SfAccount.RecordTypeId = DbAccount.account_record_type_id;
//                //    // , ShippingAddress
//                //    // , ShippingCity
//                //    SfAccount.ShippingCity = DbAccount.account_shipping_city;
//                //    // , ShippingCountry
//                //    SfAccount.ShippingCountry = DbAccount.account_shipping_country;
//                //    // , ShippingGeocodeAccuracy
//                //    // , ShippingLatitude
//                //    // , ShippingLongitude
//                //    // , ShippingPostalCode
//                //    SfAccount.ShippingPostalCode = DbAccount.account_shipping_postalcode;
//                //    // , ShippingState
//                //    SfAccount.ShippingState = DbAccount.account_shipping_province;
//                //    // , ShippingStreet
//                //    SfAccount.ShippingStreet = DbAccount.account_shipping_street;
//                //    // , SicDesc
//                //    // , SystemModstamp
//                //    // , Type
//                //    // , Website
//                //    SfAccount.Website = DbAccount.account_website;

//                //    SfAccount.lc_crmdb_last_sync__cSpecified = true;
//                //    SfAccount.lc_crmdb_last_sync__c = DateTime.Now;

//                //    TotalCount++;
//                //}
//            }

//            //InsertRecords(SalesforceInserts, SalesforceInsertKeys, InsertResults.Account);
//            List<SaveResult> results = UpdateRecords(SalesforceUpdates);

//            //error.statusCode == StatusCode.INVALID_CROSS_REFERENCE_KEY ||

//            foreach (SaveResult r in results)
//            {
//                if (!r.success)
//                {
//                    foreach (Error error in r.errors)
//                    {
//                        if (error.statusCode == StatusCode.ENTITY_IS_DELETED)
//                        {
//                            if (!string.IsNullOrWhiteSpace(r.id))
//                            {
//                                var account = db_ctx.crm_accounts.Where(x => x.account_id == r.id).FirstOrDefault();
//                                account.account_deleted = true;
//                                account.account_mark_delete = false;
//                                db_ctx.SaveChanges();
//                            }
//                        }
//                        else
//                        {
//                            RecordError(GetCurrentMethod(), error.message);
//                        }
//                    }
//                }
//            }

//            success = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        ErrorMsg += ex.ToString();
//    }

//    RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

//    return success;
//}

