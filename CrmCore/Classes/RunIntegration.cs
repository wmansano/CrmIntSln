using lc.crm;
using lc.crm.api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {

        private bool SyncColleagueAffiliations()
        {
            int InsertedCount = 0;
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            try
            {
                MethodName = GetCurrentMethod();


                if (SyncProspectAffiliations()) { UpdatedCount++; }
                TotalCount++;

                if (SyncApplicantAffiliations()) { UpdatedCount++; }
                TotalCount++;

                if (SyncStudentAffiliations()) { UpdatedCount++; }
                TotalCount++;

                if (SyncAlumniAffiliations()) { UpdatedCount++; }
                TotalCount++;

                if (SyncCompletedAffiliations()) { UpdatedCount++; }
                TotalCount++;

                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        private bool SyncProspectAffiliations()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<crm_inquiries> crm_Inquiries = new List<crm_inquiries>();
            string affiliationRole = "Prospect";
            string affiliationType = "Academic Program";

            try
            {
                MethodName = GetCurrentMethod();

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                crm_Inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_datetime >= LastRunDate
                                                            || x.inq_modified_datetime >= LastRunDate).ToList();
                if (crm_Inquiries.Any()) {
                    if (MonitorExecution(MethodName, crm_Inquiries.Count))
                    {
                        foreach (crm_inquiries inq in crm_Inquiries)
                        {
                            var contacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                                    && x.contact_mark_delete == false
                                                                    && x.contact_id == inq.inq_contact_id).ToList();

                            if (contacts.Any()) {
                                var contact = contacts.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();

                                if (contact != null) {
                                    // need to evaluate
                                    //if (string.IsNullOrEmpty(contact.contact_colleague_id))
                                    //{
                                    //    contact.contact_prospect_type = true;
                                    //}
                                    //else {
                                    //    contact.contact_prospect_type = false;
                                    //}

                                    contact.contact_prospect_type = true;

                                    List<string> programs = new List<string>();

                                    if (!string.IsNullOrEmpty(inq.inq_pri_prog_interest))
                                    {
                                        programs.Add(inq.inq_pri_prog_interest);
                                    }

                                    //if (!string.IsNullOrWhiteSpace(inq.inq_sec_prog_interest)
                                    //     && inq.inq_sec_prog_interest != inq.inq_pri_prog_interest)
                                    //{
                                    //    programs.Add(inq.inq_sec_prog_interest);
                                    //}

                                    foreach (string program in programs)
                                    {
                                        crm_affiliations DbAffiliation = new crm_affiliations();
                                        DateTime StartDate = GetLocalTime(inq.inquiry_datetime ?? inq.inq_created_datetime);
                                        //string lc_student_program_id = contact.contact_colleague_id + 

                                        //var ProgramNumber = db_ctx.crm_accounts.Where(x => x.account_deleted == false
                                        //                                              && x.account_mark_delete == false
                                        //                                              && x.account_id == program)
                                        //                                       .Select(y => y.account_number)
                                        //                                       .FirstOrDefault();

                                        var affiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_deleted == false
                                                                                          && x.affiliation_mark_delete == false
                                                                                          && x.affiliation_record_type_id == AffiliationRecordTypes.StudentProgramAffiliation
                                                                                          && (x.affiliation_contact_id == inq.inq_contact_id
                                                                                                && x.affiliation_organization_id == program)
                                                                                          && x.affiliation_role == affiliationRole
                                                                                          && x.affiliation_type == affiliationType).ToList();

                                        if (!affiliations.Any())
                                        {
                                            DbAffiliation.affiliation_guid = Guid.NewGuid();
                                            DbAffiliation.affiliation_contact_id = inq.inq_contact_id;
                                            DbAffiliation.affiliation_role = affiliationRole;
                                            DbAffiliation.affiliation_type = affiliationType;
                                            DbAffiliation.affiliation_organization_id = program;
                                            DbAffiliation.affiliation_owner_id = Settings.CrmAdmin;
                                            DbAffiliation.affiliation_created_by = Settings.CrmAdmin;
                                            DbAffiliation.affiliation_start_date = StartDate;
                                            DbAffiliation.affiliation_created_datetime = inq.inquiry_datetime ?? inq.inq_created_datetime;
                                            DbAffiliation.affiliation_system_modstamp = inq.inquiry_datetime ?? inq.inq_created_datetime;
                                            DbAffiliation.affiliation_last_viewed_datetime = inq.inquiry_datetime ?? inq.inq_created_datetime;

                                            DbAffiliation.affiliation_last_modified_by = Settings.CrmAdmin;
                                            DbAffiliation.affiliation_last_modified_datetime = DateTime.Now;

                                            DbAffiliation.last_sfsync_datetime = DateTime.Now;

                                            db_ctx.crm_affiliations.Add(DbAffiliation);

                                            db_ctx.SaveChanges();

                                            inserted++;
                                        }

                                        total++;
                                    }
                                }
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

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool SyncApplicantAffiliations()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            string prospectRole = "Prospect";
            string applicantRole = "Applicant";
            string studentRoles = string.Empty;
            string affiliatedProgram = "0011U00000MSVQ0QAP"; // Open Studies is default
            string programAccountRecordTypeId = "0121U000001MGdWQAW";
            string affiliationType = "Academic Program";

            try
            {
                MethodName = GetCurrentMethod();

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                var dw_applications = db_ctx.wt_applications.Where(x => x.sis_appl_mark_delete == false
                                                              && (((x.sis_appl_modified_date != null) 
                                                              && x.sis_appl_modified_date >= LastRunDate)
                                                                || ((x.sis_appl_date != null) && x.sis_appl_date >= LastRunDate)))
                                                        .OrderBy(y => y.sis_appl_date)
                                                        .ToList();

                if (MonitorExecution(MethodName, dw_applications.Count))
                {
                    foreach (wt_applications dw_application in dw_applications)
                    {
                        crm_affiliations applicant_affiliation = new crm_affiliations();

                        DateTime? EndDate = null;
                        DateTime StartDate = dw_application.sis_appl_status_date ?? dw_application.sis_appl_date ?? DateTime.Now;

                        if (dw_application.sis_appl_stage == "Complete" || dw_application.sis_appl_stage == "Stopped")
                        {
                            EndDate = dw_application.sis_appl_status_date;
                        }

                        var contact = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                                  && x.contact_mark_delete == false
                                                                  && x.contact_colleague_id == dw_application.sis_student_id)
                                                         .OrderByDescending(y => y.contact_last_modified_datetime)
                                                         .FirstOrDefault();

                        if (contact != null)
                        {
                            var academic_programs = db_ctx.crm_accounts.Where(x => x.account_deleted == false
                                                                                && x.account_mark_delete == false
                                                                                && x.account_record_type_id == programAccountRecordTypeId
                                                                                && x.account_number == dw_application.sis_appl_program).ToList();
                            if (academic_programs.Any())
                            {
                                var academic_program = academic_programs.OrderByDescending(x => x.account_modifed_datetime).FirstOrDefault();

                                if (academic_program != null)
                                {
                                    affiliatedProgram = academic_program.account_id;
                                }
                            }

                            var applicant_affiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_deleted == false
                                                                                         && x.affiliation_mark_delete == false
                                                                                         && x.affiliation_role == applicantRole
                                                                                         && x.affiliation_contact_id == contact.contact_id
                                                                                         //&& x.affiliation_start_date == StartDate
                                                                                         && x.affiliation_organization_id == affiliatedProgram)
                                                                                  .OrderByDescending(y => y.affiliation_created_datetime)
                                                                                  .ToList();
                            if (applicant_affiliations.Any())
                            {
                                applicant_affiliation = applicant_affiliations.OrderByDescending(x => x.affiliation_last_modified_datetime).FirstOrDefault();
                                updated++;
                            }
                            else
                            {
                                applicant_affiliation.affiliation_guid = Guid.NewGuid();
                                applicant_affiliation.affiliation_contact_id = contact.contact_id;
                                applicant_affiliation.affiliation_owner_id = Settings.CrmAdmin;
                                applicant_affiliation.affiliation_role = applicantRole;
                                applicant_affiliation.affiliation_type = affiliationType;
                                applicant_affiliation.affiliation_organization_id = affiliatedProgram;
                                applicant_affiliation.affiliation_created_by = Settings.CrmAdmin;
                                applicant_affiliation.affiliation_start_date = StartDate;
                                applicant_affiliation.affiliation_created_datetime = dw_application.sis_appl_date;
                                applicant_affiliation.affiliation_system_modstamp = dw_application.sis_appl_date;
                                applicant_affiliation.affiliation_last_viewed_datetime = dw_application.sis_appl_modified_date;

                                db_ctx.crm_affiliations.Add(applicant_affiliation);
                                inserted++;
                            }

                            // close any previous prospect inquiries
                            // was instructed by Recruitment to close all previous prospect affiliations ...not just for the specific program
                            var prospect_affiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_deleted == false
                                                                                && x.affiliation_mark_delete == false
                                                                                && x.affiliation_role == prospectRole
                                                                                && x.affiliation_type == affiliationType
                                                                                && x.affiliation_contact_id == contact.contact_id
                                                                                //&& x.affiliation_organization_id == dw_application.sis_appl_program
                                                                                && (x.affiliation_end_date == null
                                                                                    && x.affiliation_start_date <= dw_application.sis_appl_date)).ToList();
                            if (prospect_affiliations.Any())
                            {
                                foreach (crm_affiliations prospect_affiliation in prospect_affiliations)
                                {
                                    prospect_affiliation.affiliation_end_date = dw_application.sis_appl_date;
                                }
                            }

                            if (EndDate != null)
                            {
                                contact.contact_applicant_type = false;
                                applicant_affiliation.affiliation_primary = false;
                                applicant_affiliation.affiliation_end_date = EndDate;
                                applicant_affiliation.affiliation_status = "Former";
                            }
                            else
                            {
                                contact.contact_applicant_type = true;
                                contact.contact_prospect_type = false;
                                contact.contact_primary_academic_program = affiliatedProgram;
                                applicant_affiliation.affiliation_primary = true;
                                applicant_affiliation.affiliation_status = "Current";
                            }

                            applicant_affiliation.affiliation_last_modified_by = Settings.CrmAdmin;
                            applicant_affiliation.affiliation_last_modified_datetime = DateTime.Now;

                            db_ctx.SaveChanges();

                            total++;
                        }
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool SyncStudentAffiliations()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            string prospectRole = "Prospect";
            string applicantRole = "Applicant";
            string studentRole = "Student";
            string affiliationType = "Academic Program";
            string defaultProgram = "0011U00000MSVQ0QAP";
            string studentRoles = string.Empty;
            string affiliatedProgram = string.Empty;
            List<wt_student_programs> wt_student_programs = new List<wt_student_programs>();

            try
            {
                MethodName = GetCurrentMethod();

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                wt_student_programs = db_ctx.wt_student_programs.Where(x => x.sis_stu_program_mark_delete == false
                                                                         && ((x.sis_stu_program_modified_date != null)
                                                                            && x.sis_stu_program_modified_date >= LastRunDate))
                                                                .OrderBy(y => y.sis_stu_program_modified_date).ToList();

                if (MonitorExecution(MethodName, wt_student_programs.Count))
                {
                    foreach (wt_student_programs wt_student_program in wt_student_programs)
                    {
                        crm_affiliations student_affiliation = new crm_affiliations();
                        DateTime StartDate = wt_student_program.sis_stu_program_start_date ?? DateTime.Now;
                        DateTime? EndDate = wt_student_program.sis_stu_program_end_date;
                        affiliatedProgram = db_ctx.crm_accounts.Where(x=>x.account_number == wt_student_program.sis_stu_program_code 
                                                                        && x.account_active == true)
                                                                .OrderByDescending(y=>y.account_modifed_datetime)
                                                                .FirstOrDefault().account_id ?? defaultProgram;

                        if (!string.IsNullOrEmpty(affiliatedProgram)) {
                            var contacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                                      && x.contact_mark_delete == false
                                                                      && x.contact_colleague_id == wt_student_program.sis_student_id).ToList();
                            if (contacts.Any())
                            {

                                var contact = contacts.OrderByDescending(y => y.contact_last_modified_datetime).FirstOrDefault();

                                if (contact != null)
                                {
                                    var student_affiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_deleted == false
                                                                                                 && x.affiliation_mark_delete == false
                                                                                                 && (x.affiliation_role == studentRole)
                                                                                                 && x.affiliation_contact_id == contact.contact_id
                                                                                                 //&& x.affiliation_start_date == StartDate
                                                                                                 && x.affiliation_organization_id == affiliatedProgram)
                                                                                          .OrderByDescending(y => y.affiliation_created_datetime)
                                                                                          .ToList();


                                    if (student_affiliations.Any())
                                    {
                                        student_affiliation = student_affiliations.OrderByDescending(x => x.affiliation_last_modified_datetime).FirstOrDefault();

                                        if (string.IsNullOrEmpty(student_affiliation.affiliation_role) && student_affiliation.affiliation_type == affiliationType)
                                        {
                                            student_affiliation.affiliation_role = studentRole;
                                        }

                                        updated++;
                                    }
                                    else
                                    {
                                        student_affiliation.affiliation_guid = Guid.NewGuid();
                                        student_affiliation.affiliation_contact_id = contact.contact_id;
                                        student_affiliation.affiliation_owner_id = Settings.CrmAdmin;
                                        student_affiliation.affiliation_role = studentRole;
                                        student_affiliation.affiliation_type = affiliationType;
                                        student_affiliation.affiliation_organization_id = affiliatedProgram;
                                        student_affiliation.affiliation_created_by = Settings.CrmAdmin;
                                        student_affiliation.affiliation_start_date = StartDate;
                                        student_affiliation.affiliation_created_datetime = wt_student_program.sis_stu_program_modified_date;
                                        student_affiliation.affiliation_system_modstamp = wt_student_program.sis_stu_program_modified_date;
                                        student_affiliation.affiliation_last_viewed_datetime = wt_student_program.sis_stu_program_modified_date;

                                        db_ctx.crm_affiliations.Add(student_affiliation);

                                        inserted++;
                                    }

                                    // close unclosed prospect affiliations
                                    var prospect_affiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_deleted == false
                                                                                        && x.affiliation_mark_delete == false
                                                                                        && x.affiliation_contact_id == contact.contact_id
                                                                                        && x.affiliation_role == prospectRole
                                                                                        && x.affiliation_type == affiliationType
                                                                                        && (x.affiliation_end_date == null
                                                                                            && x.affiliation_start_date <= StartDate)).ToList();
                                    if (prospect_affiliations.Any())
                                    {
                                        foreach (crm_affiliations prospect_affiliation in prospect_affiliations)
                                        {
                                            prospect_affiliation.affiliation_end_date = StartDate;
                                        }
                                    }

                                    // close unclosed applicant affiliations
                                    var applicant_affiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_deleted == false
                                                                                        && x.affiliation_mark_delete == false
                                                                                        && x.affiliation_role == applicantRole
                                                                                        && x.affiliation_type == affiliationType
                                                                                        && x.affiliation_contact_id == contact.contact_id
                                                                                        && (x.affiliation_end_date == null
                                                                                            && x.affiliation_start_date <= StartDate)).ToList();
                                    if (applicant_affiliations.Any())
                                    {
                                        foreach (crm_affiliations applicant_affiliation in applicant_affiliations)
                                        {
                                            applicant_affiliation.affiliation_end_date = StartDate;
                                        }
                                    }

                                    if (EndDate != null && EndDate < DateTime.Now)
                                    {
                                        contact.contact_student_type = false;
                                        student_affiliation.affiliation_primary = false;
                                        student_affiliation.affiliation_end_date = EndDate;
                                        student_affiliation.affiliation_status = "Former";
                                    }
                                    else
                                    {
                                        contact.contact_student_type = true;
                                        contact.contact_prospect_type = false;
                                        contact.contact_applicant_type = false;
                                        student_affiliation.affiliation_primary = true;
                                        contact.contact_primary_academic_program = affiliatedProgram;
                                        student_affiliation.affiliation_status = "Current";
                                    }

                                    student_affiliation.affiliation_last_modified_by = Settings.CrmAdmin;
                                    student_affiliation.affiliation_last_modified_datetime = DateTime.Now;

                                    db_ctx.SaveChanges();

                                    total++;
                                }
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

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool SyncAlumniAffiliations()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            string affiliationRole = "Student";
            string studentRoles = string.Empty;

            List<wt_student_credentials> wt_student_credentials = new List<wt_student_credentials>();

            try
            {
                MethodName = GetCurrentMethod();

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                wt_student_credentials = db_ctx.wt_student_credentials.Where(x => x.sis_student_cred_deleted == false
                                                                               && x.sis_student_cred_mark_delete == false
                                                                             && ((x.sis_student_cred_modified_datetime != null)
                                                                                 && x.sis_student_cred_modified_datetime >= LastRunDate)).ToList();

                if (MonitorExecution(MethodName, wt_student_credentials.Count))
                {
                    foreach (wt_student_credentials wt_student_credential in wt_student_credentials)
                    {
                        crm_affiliations DbAffiliation = new crm_affiliations();

                        var contact = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                                  && x.contact_mark_delete == false
                                                                  && x.contact_colleague_id == wt_student_credential.sis_student_id)
                                                         .OrderByDescending(y => y.contact_last_modified_datetime)
                                                         .FirstOrDefault();
                        if (contact != null)
                        {
                            var applicant_affiliations = db_ctx.crm_affiliations.Where(x => x.affiliation_deleted == false
                                                                                         && x.affiliation_mark_delete == false
                                                                                         && x.affiliation_role == affiliationRole
                                                                                         && x.affiliation_contact_id == contact.contact_id
                                                                                         && x.affiliation_organization_id == wt_student_credential.crm_program_id
                                                                                         && x.affiliation_start_date == wt_student_credential.sis_student_cred_created_datetime)
                                                                                  .OrderByDescending(y => y.affiliation_created_datetime)
                                                                                  .ToList();


                            if (applicant_affiliations.Any())
                            {
                                DbAffiliation = applicant_affiliations.OrderByDescending(x => x.affiliation_last_modified_datetime).FirstOrDefault();
                                updated++;
                            }
                            else
                            {
                                DbAffiliation.affiliation_guid = Guid.NewGuid();
                                DbAffiliation.affiliation_contact_id = contact.contact_id;
                                DbAffiliation.affiliation_owner_id = Settings.CrmAdmin;
                                DbAffiliation.affiliation_role = affiliationRole;
                                DbAffiliation.affiliation_type = "Academic Program";
                                DbAffiliation.affiliation_organization_id = wt_student_credential.crm_program_id;
                                DbAffiliation.affiliation_created_by = Settings.CrmAdmin;
                                DbAffiliation.affiliation_start_date = wt_student_credential.sis_student_cred_created_datetime;
                                DbAffiliation.affiliation_created_datetime = DbAffiliation.affiliation_created_datetime;
                                DbAffiliation.affiliation_system_modstamp = DbAffiliation.affiliation_created_datetime;
                                DbAffiliation.affiliation_last_viewed_datetime = DbAffiliation.affiliation_last_modified_datetime;

                                db_ctx.crm_affiliations.Add(DbAffiliation);
                                inserted++;
                            }

                            //if (EndDate != null && EndDate < DateTime.Now)
                            //{
                            //    DbAffiliation.affiliation_primary = false;
                            //    DbAffiliation.affiliation_end_date = EndDate;
                            //    DbAffiliation.affiliation_status = "Former";
                            //}
                            //else
                            //{
                            //    DbAffiliation.affiliation_primary = true;
                            //    contact.contact_primary_academic_program = wt_student_program.crm_program_id;
                            //}

                            DbAffiliation.affiliation_last_modified_by = Settings.CrmAdmin;
                            DbAffiliation.affiliation_last_modified_datetime = DateTime.Now;

                            db_ctx.SaveChanges();

                            total++;
                        }
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool SyncCompletedAffiliations()
        {

            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            try
            {
                MethodName = GetCurrentMethod();

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                success = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, total, inserted, updated, ErrorMsg);

            return success;
        }

        private bool SyncCourseOfferingNames()
        {
            int UpdatedCount = 0;
            int InsertedCount = 0;
            int TotalCount = 0;

            bool success = false;
            bool problem = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<crm_course_offerings> crm_course_offerings = new List<crm_course_offerings>();

            List<sObject> SalesforceInserts = new List<sObject>();
            List<sObject> SalesforceUpdates = new List<sObject>();

            try
            {
                MethodName = GetCurrentMethod();

                DateTime LastRunDate = GetLastRunDateTime(MethodName);

                crm_course_offerings = db_ctx.crm_course_offerings.Where(x => x.course_offering_deleted == false
                                                                    && (x.course_offering_id != null
                                                                    || (x.course_offering_created_datetime >= LastRunDate
                                                                    || x.course_offering_modified_datetime >= LastRunDate)
                                                                    )
                                                                    ).ToList();

                if (MonitorExecution(MethodName, crm_course_offerings.Count))
                {
                    foreach (crm_course_offerings DbCourseOffering in crm_course_offerings)
                    {
                        hed__Course_Offering__c SfCourseOffering = new hed__Course_Offering__c
                        {
                            Id = DbCourseOffering.course_offering_id
                        };

                        if (!string.IsNullOrEmpty(DbCourseOffering.course_offering_id))
                        {
                            if (DbCourseOffering.course_offering_name.Count(x => x == '-') == 3)
                            {
                                SfCourseOffering.Name = DbCourseOffering.course_offering_name;
                            }
                            else
                            {
                                problem = true;
                            }

                            if (DbCourseOffering.course_section_code.Count(x => x == '-') == 2)
                            {
                                SfCourseOffering.lc_course_section_code__c = DbCourseOffering.course_section_code;
                            }
                            else
                            {
                                problem = true;
                            }

                            if (!problem)
                            {
                                SalesforceUpdates.Add(SfCourseOffering);
                                UpdatedCount++;
                            }
                        }

                        TotalCount++;
                    }

                    UpdateRecords(SalesforceUpdates);

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

        private bool SyncBroadcasts()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            int broadcast_duration = 90;
            int broadcast_interval = 0;

            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<string> emailAddresses = new List<string>();

            try
            {
                MethodName = GetCurrentMethod();

                DateTime LastRunDate = GetLastRunDateTime(MethodName); 

                // get all open email campaigns
                // TODO: get latest WSDL and check all fields e.g. 'A' active?
                var email_campaigns = db_ctx.crm_email_campaigns.Where(x => x.ec_deleted == false
                                                                            && x.ec_active_flag == true
                                                                            && x.ec_start_datetime != null
                                                                            && (x.ec_end_datetime == null
                                                                                || x.ec_end_datetime < DateTime.Today
                                                                                || x.ec_modified_datetime >= LastRunDate)
                                                                                ).ToList();

                if (MonitorExecution(MethodName, email_campaigns.Count))
                {
                    foreach (crm_email_campaigns email_campaign in email_campaigns)
                    {
                        if (email_campaign.ec_recur_days != null)
                        {
                            broadcast_interval = (int)email_campaign.ec_recur_days;

                            if ((email_campaign.ec_start_datetime != null && email_campaign.ec_end_datetime != null)
                                    && (email_campaign.ec_end_datetime > email_campaign.ec_start_datetime))
                            {
                                broadcast_duration = (email_campaign.ec_end_datetime.Value - email_campaign.ec_start_datetime.Value).Days;
                            }
                        }

                        List<string> updated_broadcast_guids = new List<string>();
                        for (int i = 0; i <= broadcast_duration; i += broadcast_interval)
                        {
                            crm_email_broadcasts email_broadcast = new crm_email_broadcasts();

                            // we are not worrying about the time yet.
                            DateTime iterated_date = ((DateTime)email_campaign.ec_start_datetime).AddDays(i).Date;

                            var broadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
                                                                                    && x.email_campaign_id == email_campaign.email_campaign_id
                                                                                    && x.email_broadcast_sent == iterated_date
                                                                                    && x.email_broadcast_email_campaign != null
                                                                                    && (x.email_broadcast_status == null
                                                                                        || x.email_broadcast_status == "Pending")
                                                                                    && (x.email_broadcast_sent == null
                                                                                        || x.email_broadcast_sent >= DateTime.Now)).ToList();
                            // Create or update broadcasts
                            if (broadcasts.Any())
                            {
                                // update broadcasts
                                email_broadcast = broadcasts.OrderByDescending(x => x.email_broadcast_modfied_datetime).FirstOrDefault();
                                updated++;
                            }
                            else
                            {
                                // create broadcasts
                                email_broadcast.email_broadcast_guid = Guid.NewGuid();
                                email_broadcast.email_broadcast_created_by = Settings.CrmAdmin;
                                email_broadcast.email_broadcast_created_datetime = DateTime.Now;
                                email_broadcast.email_campaign_id = email_campaign.email_campaign_id;
                                email_broadcast.email_broadcast_email_campaign = email_campaign.email_campaign_id;

                                email_broadcast.email_broadcast_sent = email_campaign.ec_start_datetime.Value.AddDays(i);

                                db_ctx.crm_email_broadcasts.Add(email_broadcast);
                                inserted++;
                            }

                            email_broadcast.email_broadcast_status = "Pending";
                            //email_broadcast.email_broadcast_name = email_campaign.ec_name + " (" + email_campaign.ec_start_datetime.ToString() + ")";
                            email_broadcast.email_report_id = email_campaign.ec_report_id;

                            var report_names = db_ctx.crm_reports.Where(x => x.crm_report_id == email_campaign.ec_report_id).ToList();
                            if (report_names.Any())
                            {
                                var report_name = report_names.OrderByDescending(x => x.crm_report_modified_date).FirstOrDefault();

                                email_broadcast.email_report_name = report_name.crm_report_name ?? "Unspecified";
                            }

                            email_broadcast.email_template_id = email_campaign.ec_template_id;

                            var templates_names = db_ctx.crm_email_templates.Where(x => x.email_template_id == email_campaign.ec_template_id).ToList();
                            if (templates_names.Any())
                            {
                                var template_name = templates_names.OrderByDescending(x => x.email_template_modified_datetime).FirstOrDefault();
                                email_broadcast.email_template_name = template_name.email_template_name ?? "Unspecified";
                            }

                            // need to delete all broadcasts not included in this updated batch
                            updated_broadcast_guids.Add(email_broadcast.email_broadcast_guid.ToString());

                            var broadcasts_to_clear = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_deleted == false
                                                                                            && x.email_campaign_id == email_campaign.email_campaign_id
                                                                                            && !updated_broadcast_guids.Contains(x.email_broadcast_guid.ToString())).ToList();

                            foreach (crm_email_broadcasts todelete in broadcasts_to_clear)
                            {
                                todelete.email_broadcast_mark_delete = true;
                                todelete.email_broadcast_modified_by = Settings.CrmAdmin;
                                todelete.email_broadcast_modfied_datetime = DateTime.Now;
                            }

                            db_ctx.SaveChanges();

                            total++;
                        }
                    }
                }

                success = true;
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
