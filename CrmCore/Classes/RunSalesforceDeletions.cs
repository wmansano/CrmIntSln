using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using lc.crm.api;

namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        private bool RunSalesforceDeletion()
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            try
            {
                MethodName = GetCurrentMethod();

                if (DeleteAccounts()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteApplications()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteContacts()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteTerms()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteCourses()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteCourseOfferings()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteCourseConnections()) { UpdatedCount++; }
                TotalCount++;
                if (DeletePrograms()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteAffiliations()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteInquiries()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteTasks()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteEvents()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteActivityExtenders()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteEventRegistrations()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteEmailCampaigns()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteEmailBroadcasts()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteReports()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteRoleValues()) { UpdatedCount++; }
                TotalCount++;
                if (DeleteProgramEnrollments()) { UpdatedCount++; }
                TotalCount++;

                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            //RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteAccounts(string[] delete_accounts = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_accounts == null) {
                    delete_accounts = db_ctx.crm_accounts.Where(x => x.account_deleted == false
                                                                      && x.account_mark_delete == true)
                                                              .Select(y => y.account_id)
                                                              .ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_accounts.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_accounts, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_accounts[i];

                            var records = db_ctx.crm_accounts.Where(x => x.account_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.account_modifed_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.account_deleted = true;
                                    record.account_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges(); 
                            UpdatedCount++;
                        }
                        else {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteApplications(string[] delete_applications = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_applications == null) { 
                delete_applications = db_ctx.crm_applications.Where(x => x.application_deleted == false
                                                                      && x.appl_mark_delete == true)
                                                             .Select(y => y.crm_application_id)
                                                             .ToArray<string>();
                }

                if (MonitorExecution(MethodName, delete_applications.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_applications, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_applications[i];

                            var records = db_ctx.crm_applications.Where(x => x.crm_application_id == id).ToList();

                            if (records.Any())
                            {
                                foreach (lc.crm.crm_applications application in records) {
                                        application.application_deleted = true;
                                        application.appl_mark_delete = false;                                
                                }
                            }

                            db_ctx.SaveChanges();
                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteContacts(string[] delete_contacts = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_contacts == null) {
                    delete_contacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                                  && x.contact_mark_delete == true)
                                                         .Select(y => y.contact_id)
                                                         .ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_contacts.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_contacts, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_contacts[i];

                            var records = db_ctx.crm_contacts.Where(x => x.contact_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.contact_deleted = true;
                                    record.contact_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();
                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteTerms(string[] delete_terms = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_terms == null) { 
                    delete_terms = db_ctx.crm_terms.Where(x => x.term_deleted == false
                                                               && x.term_mark_delete == true)
                                                      .Select(y => y.term_id)
                                                      .ToArray<string>();
                }
                    

                if (MonitorExecution(MethodName, delete_terms.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_terms, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_terms[i];

                            var records = db_ctx.crm_terms.Where(x => x.term_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.term_modifed_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.term_deleted = true;
                                    record.term_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteCourses(string[] delete_courses = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_courses == null) {
                    delete_courses = db_ctx.crm_courses.Where(x => x.course_deleted == false
                                                                 && x.course_mark_delete == true)
                                                        .Select(y => y.crm_course_id)
                                                        .ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_courses.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_courses, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_courses[i];

                            var records = db_ctx.crm_courses.Where(x => x.crm_course_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.course_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.course_deleted = true;
                                    record.course_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();
                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteCourseOfferings(string[] delete_course_offerings = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_course_offerings == null) { 
                    delete_course_offerings = db_ctx.crm_course_offerings.Where(x => x.course_offering_deleted == false
                                                                          && x.course_offering_mark_delete == true)
                                                                 .Select(y => y.course_offering_id)
                                                                 .ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_course_offerings.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_course_offerings, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_course_offerings[i];

                            var records = db_ctx.crm_course_offerings.Where(x => x.course_offering_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.course_offering_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.course_offering_deleted = true;
                                    record.course_offering_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteCourseConnections(string[] delete_course_connections = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_course_connections == null) {
                    delete_course_connections = db_ctx.crm_course_connections.Where(x => x.course_connection_deleted == false
                                                                            && x.course_connection_mark_delete == true)
                                                                   .Select(y => y.course_connection_id)
                                                                   .ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_course_connections.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_course_connections, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_course_connections[i];

                            var records = db_ctx.crm_course_connections.Where(x => x.course_connection_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.course_connection_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.course_connection_deleted = true;
                                    record.course_connection_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeletePrograms(string[] delete_programs = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);
                if (delete_programs == null) {
                    delete_programs = db_ctx.crm_accounts.Where(x => x.account_deleted == false
                                                                             && x.account_type == "lc_academic_program"
                                                                             && x.account_mark_delete == true).Select(y => y.account_id).ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_programs.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_programs, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_programs[i];

                            var records = db_ctx.crm_accounts.Where(x => x.account_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.account_modifed_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.account_deleted = true;
                                    record.account_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteProgramEnrollments(string[] delete_program_enrollments = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);
                if (delete_program_enrollments == null) { 
                    delete_program_enrollments = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_deleted == false
                                                                             && x.crm_program_enrollment_mark_delete == true
                                                                             && x.crm_program_enrollment_id != null)
                                                                    .Select(y => y.crm_program_enrollment_id)
                                                                    .ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_program_enrollments.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_program_enrollments, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_program_enrollments[i];

                            var records = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.crm_program_enrollment_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.crm_program_enrollment_deleted = true;
                                    record.crm_program_enrollment_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteAffiliations(string[] delete_affiliations = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            DateTime? LastRunDate = Settings.GlobalLastRunTime;

            try
            {
                MethodName = GetCurrentMethod();

                LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_affiliations == null) {
                    delete_affiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_deleted == false
                                                                          && x.affiliation_mark_delete == true
                                                                          && x.affiliation_record_type_id == "0121U000000ShBIQA0")
                                                                          //&& x.affiliation_role == "Student"
                                                                          //&& x.affiliation_id != null)
                                                                 .Select(y => y.affiliation_id)
                                                                 .ToArray<string>();
                }

                if (MonitorExecution(MethodName, delete_affiliations.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_affiliations, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any()) {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_affiliations[i];

                            var records = db_ctx.crm_affiliations.Where(x => x.affiliation_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.affiliation_last_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.affiliation_deleted = true;
                                    record.affiliation_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();
                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(),"affiliation_id: " + delete_affiliations[i].ToString() + ", Error: " + results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteInquiries(string[] delete_inquiries = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_inquiries == null) { 
                    delete_inquiries = db_ctx.crm_inquiries.Where(x => x.inq_deleted == false
                                                                              && x.inquiry_mark_delete == true)
                                                                     .Select(y => y.inquiry_id)
                                                                     .ToArray<string>();
                }

                if (delete_inquiries.Any()) {
                    if (MonitorExecution(MethodName, delete_inquiries.Length))
                    {
                        List<DeleteResult> results = DeleteRecords(delete_inquiries, 100);

                        for (int i = 0; i < results.Count(); i++)
                        {
                            success = results[i].success;
                            if (results[i].errors != null && results[i].errors.Any())
                            {
                                ErrorMsg = results[i].errors[0].message.ToString();
                            }

                            if (success || ErrorMsg.Contains("entity is deleted")
                                        || ErrorMsg.Contains("invalid cross reference id"))
                            {
                                string id = delete_inquiries[i];

                                var records = db_ctx.crm_inquiries.Where(x => x.inquiry_id == id).ToList();

                                if (records.Any())
                                {
                                    var record = records.OrderByDescending(x => x.inq_modified_datetime).FirstOrDefault();

                                    if (record != null)
                                    {
                                        record.inq_deleted = true;
                                        record.inquiry_mark_delete = false;
                                    }
                                }

                                db_ctx.SaveChanges();

                                UpdatedCount++;
                            }
                            else
                            {
                                RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteTasks(string[] delete_tasks = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_tasks == null) {
                    delete_tasks = db_ctx.crm_tasks.Where(x => x.task_deleted == false
                                                                       && x.task_mark_delete == true)
                                                              .Select(y => y.activity_id)
                                                              .ToArray<string>();
                
                }

                if (MonitorExecution(MethodName, delete_tasks.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_tasks, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_tasks[i];

                            var records = db_ctx.crm_tasks.Where(x => x.activity_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.task_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.task_deleted = true;
                                    record.task_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteEvents(string[] delete_events = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_events == null) { 
                    delete_events = db_ctx.crm_events.Where(x => x.event_deleted == false
                                                                         && x.event_mark_delete == true)
                                                                .Select(y => y.activity_id)
                                                                .ToArray<string>();                }

                if (MonitorExecution(MethodName, delete_events.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_events, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_events[i];

                            var records = db_ctx.crm_events.Where(x => x.activity_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.event_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.event_deleted = true;
                                    record.event_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteActivityExtenders(string[] delete_activity_extenders = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_activity_extenders == null) { 
                    delete_activity_extenders = db_ctx.crm_activity_extender.Where(x => x.activity_extender_deleted == false
                                                                                           && x.activity_extender_mark_delete == true)
                                                                                       .Select(y => y.activity_extender_id)
                                                                                       .ToArray<string>();
                }

                if (MonitorExecution(MethodName, delete_activity_extenders.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_activity_extenders, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_activity_extenders[i];

                            var records = db_ctx.crm_activity_extender.Where(x => x.activity_extender_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.activity_extender_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.activity_extender_deleted = true;
                                    record.activity_extender_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteEventRegistrations(string[] delete_event_registrations = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_event_registrations == null) { 
                    delete_event_registrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_deleted == false
                                                                                                   && x.event_registration_mark_delete == true)
                                                                                          .Select(y => y.event_registration_id)
                                                                                          .ToArray<string>();
                }

                if (MonitorExecution(MethodName, delete_event_registrations.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_event_registrations, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_event_registrations[i];

                            var records = db_ctx.crm_event_registrations.Where(x => x.event_registration_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.event_registration_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.event_registration_deleted = true;
                                    record.event_registration_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteEmailCampaigns(string[] delete_email_campaigns = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_email_campaigns == null) {
                    delete_email_campaigns = db_ctx.crm_email_campaigns.Where(x => x.ec_deleted == false
                                                                         && x.ec_mark_delete == true)
                                                                .Select(y => y.email_campaign_id)
                                                                .ToArray<string>();
                
                }

                if (delete_email_campaigns.Any()) {
                    if (MonitorExecution(MethodName, delete_email_campaigns.Length))
                    {
                        List<DeleteResult> results = DeleteRecords(delete_email_campaigns, 100);

                        for (int i = 0; i < results.Count(); i++)
                        {
                            success = results[i].success;
                            if (results[i].errors != null && results[i].errors.Any())
                            {
                                ErrorMsg = results[i].errors[0].message.ToString();
                            }

                            if (success || ErrorMsg.Contains("entity is deleted"))
                            {
                                string id = delete_email_campaigns[i];

                                var records = db_ctx.crm_email_campaigns.Where(x => x.email_campaign_id == id).ToList();

                                if (records.Any())
                                {
                                    var record = records.OrderByDescending(x => x.ec_modified_datetime).FirstOrDefault();

                                    if (record != null)
                                    {
                                        record.ec_deleted = true;
                                        record.ec_mark_delete = false;
                                    }
                                }

                                db_ctx.SaveChanges();

                                UpdatedCount++;
                            }
                            else
                            {
                                RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteEmailBroadcasts(string[] delete_email_broadcasts = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_email_broadcasts == null) { 
                    delete_email_broadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
                                                                          && x.email_broadcast_mark_delete == true
                                                                          && x.email_broadcast_id != null
                                                                          && x.email_broadcast_id != string.Empty)
                                                                 .Select(y => y.email_broadcast_id)
                                                                 .ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_email_broadcasts.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_email_broadcasts, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_email_broadcasts[i];

                            var records = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.FirstOrDefault();

                                if (record != null)
                                {
                                    record.email_broadcast_deleted = true;
                                    record.email_broadcast_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteReports(string[] delete_reports = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_reports == null) { 
                    delete_reports = db_ctx.crm_reports.Where(x => x.crm_report_deleted == false
                                                                 && x.crm_report_mark_delete == true)
                                                        .Select(y => y.crm_report_id)
                                                        .ToArray<string>();
                }
                

                if (MonitorExecution(MethodName, delete_reports.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_reports, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_reports[i];

                            var records = db_ctx.crm_reports.Where(x => x.crm_report_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.crm_report_modified_date).FirstOrDefault();

                                if (record != null)
                                {
                                    record.crm_report_deleted = true;
                                    record.crm_report_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool DeleteRoleValues(string[] delete_role_values = null)
        {
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);

                if (delete_role_values == null) { 
                delete_role_values = db_ctx.crm_role_values.Where(x => x.crm_rv_deleted_flag == false
                                                                     && x.crm_rv_mark_delete == true)
                                                            .Select(y => y.crm_role_value_id)
                                                            .ToArray<string>();
                }

                if (MonitorExecution(MethodName, delete_role_values.Length))
                {
                    List<DeleteResult> results = DeleteRecords(delete_role_values, 100);

                    for (int i = 0; i < results.Count(); i++)
                    {
                        success = results[i].success;
                        if (results[i].errors != null && results[i].errors.Any())
                        {
                            ErrorMsg = results[i].errors[0].message.ToString();
                        }

                        if (success || ErrorMsg.Contains("entity is deleted"))
                        {
                            string id = delete_role_values[i];

                            var records = db_ctx.crm_role_values.Where(x => x.crm_role_value_id == id).ToList();

                            if (records.Any())
                            {
                                var record = records.OrderByDescending(x => x.crm_rv_modified_datetime).FirstOrDefault();

                                if (record != null)
                                {
                                    record.crm_rv_deleted_flag = true;
                                    record.crm_rv_mark_delete = false;
                                }
                            }

                            db_ctx.SaveChanges();

                            UpdatedCount++;
                        }
                        else
                        {
                            RecordError(GetCurrentMethod(), results[i].errors[0].message.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, 0, UpdatedCount, ErrorMsg);

            return success;
        }

    }
}
