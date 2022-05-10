using lc.crm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        private bool RunColleagueSyncronization()
        {

            bool success = false;
            string MethodName = string.Empty;

            try
            {
                MethodName = GetCurrentMethod();

                //SyncColleagueStudents();

                //SyncColleagueTerms();

                //SyncColleagueDepartments();

                //SyncColleaguePrograms();

                //SyncColleagueCourses();

                //SyncColleagueCourseOfferings();

                //SyncColleagueCourseConnections();

                //SyncColleagueAffiliations();

                //SyncColleagueApplications();

                success = true;
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return success;

        }

        private bool SyncColleagueStudents()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<string> possible_duplicates = new List<string>();
            List<wt_core_students> dw_students = new List<wt_core_students>();

            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName); 

                dw_students = db_ctx.wt_core_students.Where(x => x.sis_stu_deleted_flag == false
                                                           && x.sis_stu_mark_delete == false
                                                           && x.modified_date >= LastRunDate).ToList();

                if (MonitorExecution(MethodName, dw_students.Count))
                {
                    foreach (wt_core_students wt_student in dw_students)
                    {

                        crm_contacts DbContact = new crm_contacts();

                        var matches = db_ctx.crm_contacts.Where(x => (x.contact_colleague_id == wt_student.sis_student_id
                                                          || (x.contact_first_name == wt_student.sis_stu_first_name
                                                             && x.contact_last_name == wt_student.sis_stu_last_name
                                                             && x.contact_birthdate == wt_student.sis_stu_birth_date)
                                                          || (x.contact_first_name == wt_student.sis_stu_first_name
                                                             && x.contact_last_name == wt_student.sis_stu_hist_last_name
                                                             && x.contact_birthdate == wt_student.sis_stu_birth_date))).ToList();
                        if (matches.Any())
                        {
                            DbContact = matches.OrderByDescending(o => o.contact_last_modified_datetime).FirstOrDefault();

                            if (matches.Count() > 1)
                            {
                                var id = matches.OrderBy(x => x.contact_last_modified_datetime).LastOrDefault();
                                //matches.OrderBy(x => x.contact_last_modified_datetime).LastOrDefault().contact_potential_duplicate = true;
                                if (id != null)
                                {
                                    if (!string.IsNullOrEmpty(id.ToString()))
                                    {
                                        possible_duplicates.Add(id.contact_id.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            DbContact.contact_guid = Guid.NewGuid();
                            DbContact.contact_created_by = Settings.CrmAdmin;
                            db_ctx.crm_contacts.Add(DbContact);
                            inserted++;
                        }

                        DbContact.contact_deleted = wt_student.sis_stu_deleted_flag;

                        DbContact.contact_owner_id = Settings.CrmAdmin;
                        DbContact.contact_colleague_id = wt_student.sis_student_id;
                        DbContact.contact_deleted = wt_student.sis_stu_deleted_flag;

                        if (!string.IsNullOrEmpty(wt_student.sis_stu_pref_email))
                        {
                            if (wt_student.sis_stu_pref_email.Contains("lethbridgecollege"))
                            {
                                DbContact.contact_college_email = wt_student.sis_stu_pref_email;
                            }
                            else
                            {
                                DbContact.contact_alternate_email = wt_student.sis_stu_off_email;
                            }
                        }

                        if (!string.IsNullOrEmpty(wt_student.sis_stu_off_email))
                        {
                            if (wt_student.sis_stu_off_email.Contains("lethbridgecollege"))
                            {
                                if (string.IsNullOrEmpty(DbContact.contact_college_email)) DbContact.contact_college_email = wt_student.sis_stu_off_email;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(DbContact.contact_alternate_email)) DbContact.contact_alternate_email = wt_student.sis_stu_off_email;
                            }
                        }

                        if (wt_student.sis_student_id != null)
                        {
                            if (!string.IsNullOrEmpty(wt_student.sis_stu_pref_email))
                            {
                                DbContact.contact_preferred_email = "University";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(wt_student.sis_stu_off_email))
                            {
                                DbContact.contact_preferred_email = "Alternate";
                            }
                        }

                        DbContact.contact_first_name = wt_student.sis_stu_first_name;
                        DbContact.contact_last_name = wt_student.sis_stu_last_name;
                        DbContact.contact_full_name = wt_student.sis_stu_first_name + " " + wt_student.sis_stu_last_name;
                        DbContact.contact_birthdate = wt_student.sis_stu_birth_date;

                        DbContact.contact_mobile_phone = wt_student.sis_stu_per_phone;
                        DbContact.contact_home_phone = wt_student.sis_stu_home_phone;

                        DbContact.contact_mailing_street = wt_student.sis_stu_address;
                        DbContact.contact_mailing_postalcode = wt_student.sis_stu_zip;
                        DbContact.contact_mailing_province = wt_student.sis_stu_state;
                        DbContact.contact_mailing_country = wt_student.sis_stu_country;

                        if (wt_student.modified_date != null)
                        {
                            DbContact.contact_last_modified_by = Settings.CrmAdmin;
                            DbContact.contact_last_modified_datetime = wt_student.modified_date;
                        }

                        if (wt_student.created_date != null)
                        {
                            DbContact.contact_created_by = Settings.CrmAdmin;
                            DbContact.contact_created_datetime = wt_student.created_date;
                        }

                        DbContact.contact_last_modified_datetime = DateTime.Now;

                        db_ctx.SaveChanges();

                        total++;
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

        private bool SyncColleagueTerms()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string studentRoles = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<wt_terms> wt_terms = new List<wt_terms>();

            try
            {
                MethodName = GetCurrentMethod();
                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName); 

                wt_terms = db_ctx.wt_terms.Where(x => x.sis_term_deleted == false
                                                            && (x.sis_term_created_date >= LastRunDate
                                                             || x.sis_term_modified_date >= LastRunDate)).ToList();

                if (MonitorExecution(MethodName, wt_terms.Count))
                {

                    foreach (wt_terms dw_term in wt_terms.OrderByDescending(x => x.sis_term_modified_date).ToList())
                    {
                        crm_terms DbTerm = new crm_terms();

                        var matches = db_ctx.crm_terms.Where(x => x.term_code == dw_term.sis_term_id);

                        if (matches.Any())
                        {
                            DbTerm = matches.OrderByDescending(x => x.term_modifed_datetime).FirstOrDefault();
                            updated++;
                        }
                        else
                        {
                            DbTerm.term_guid = Guid.NewGuid();
                            DbTerm.term_created_by = Settings.CrmAdmin;
                            DbTerm.term_created_datetime = DateTime.Now;

                            db_ctx.crm_terms.Add(DbTerm);
                            inserted++;
                        }

                        DbTerm.term_name = dw_term.sis_term_name;
                        DbTerm.term_start_date = dw_term.sis_term_start_date;
                        DbTerm.term_end_date = dw_term.sis_term_end_date;

                        DbTerm.term_type = "Semester";

                        DbTerm.term_prereg_start_date = dw_term.sis_term_prereg_start_date;
                        DbTerm.term_prereg_end_date = dw_term.sis_term_prereg_end_date;

                        DbTerm.term_reg_start_date = dw_term.sis_term_reg_start_date;
                        DbTerm.term_reg_end_date = dw_term.sis_term_reg_end_date;

                        DbTerm.term_drop_start_date = dw_term.sis_term_drop_start_date;
                        DbTerm.term_drop_end_date = dw_term.sis_term_start_date;

                        DbTerm.term_drop_grad_reqd_date = dw_term.sis_term_drop_grad_reqd_date;

                        if (!string.IsNullOrEmpty(dw_term.sis_reporting_year))
                        {
                            DbTerm.term_reporting_year = Convert.ToDecimal(dw_term.sis_reporting_year);
                        }

                        if (!string.IsNullOrEmpty(dw_term.sis_term_year))
                        {
                            DbTerm.term_year = Convert.ToDecimal(dw_term.sis_term_year);
                        }

                        if (dw_term.sis_term_sequence_no != null)
                        {
                            DbTerm.term_sequence_number = dw_term.sis_term_sequence_no;
                            DbTerm.term_grade_period_sequence = dw_term.sis_term_sequence_no;
                        }

                        DbTerm.term_modified_by = Settings.CrmAdmin;
                        DbTerm.term_modifed_datetime = DateTime.Now;

                        db_ctx.SaveChanges();

                        total++;
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

        private bool SyncColleagueDepartments()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string studentRoles = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<wt_departments> wt_departments = new List<wt_departments>();

            try
            {
                MethodName = GetCurrentMethod();
                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName); 

                wt_departments = db_ctx.wt_departments.Where(x => x.sis_department_deleted == false
                                                                   && (x.sis_department_created_datetime >= LastRunDate
                                                                   || x.sis_department_modified_datetime >= LastRunDate)
                                                                   //&& x.sis_department_active_code == "A"
                                                                   //&& x.sis_department_type == "A"
                                                                   ).Distinct().ToList();

                if (MonitorExecution(MethodName, wt_departments.Count()))
                {

                    foreach (wt_departments dw_department in wt_departments.OrderByDescending(x => x.sis_department_modified_datetime).ToList())
                    {
                        crm_accounts db_department = new crm_accounts();

                        var matches = db_ctx.crm_accounts.Where(x => x.account_record_type_id == AccountRecordTypes.UniversityDepartment
                                                                  && x.account_number == dw_department.sis_department_id).ToList();

                        if (matches.Any())
                        {
                            db_department = matches.OrderByDescending(x => x.account_modifed_datetime).FirstOrDefault();
                            updated++;
                        }
                        else
                        {
                            db_department.account_guid = Guid.NewGuid();
                            db_department.account_number = dw_department.sis_department_id;

                            db_department.account_type = AccountTypes.CollegeDepartment;
                            db_department.account_record_type_id = AccountRecordTypes.UniversityDepartment;

                            db_department.account_created_by = Settings.CrmAdmin;
                            db_department.account_created_date = dw_department.sis_department_created_datetime ?? DateTime.Now;

                            db_ctx.crm_accounts.Add(db_department);
                            inserted++;
                        }


                        db_department.account_parent_account_id = Settings.LcAccountId;
                        db_department.account_name = dw_department.sis_department_desc;

                        db_department.account_active = (dw_department.sis_department_active_code == "A");
                        db_department.account_department_type = dw_department.sis_department_type;

                        db_department.account_modified_by = Settings.CrmAdmin;
                        db_department.account_modifed_datetime = dw_department.sis_department_modified_datetime;

                        db_ctx.SaveChanges();

                        total++;
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

        private bool SyncColleaguePrograms()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string studentRoles = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<wt_programs> wt_programs = new List<wt_programs>();
            try
            {
                MethodName = GetCurrentMethod();
                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName); 

                wt_programs = db_ctx.wt_programs.Where(x => x.sis_program_deleted == false
                                                            && x.sis_program_mark_delete == false
                                                            && (x.sis_program_created_date >= LastRunDate
                                                            || x.sis_program_modified_datetime >= LastRunDate)
                                                            ).ToList();

                if (MonitorExecution(MethodName, wt_programs.Count))
                {

                    foreach (wt_programs dw_program in wt_programs.OrderByDescending(x => x.sis_program_modified_datetime).ToList())
                    {
                        crm_accounts DbProgram = new crm_accounts();

                        var matches = db_ctx.crm_accounts.Where(x => x.account_record_type_id == AccountRecordTypes.AcademicProgram
                                                                  && x.account_deleted == false
                                                                  && x.account_number != null
                                                                  && x.account_number == dw_program.sis_program_id
                                                                  ).ToList();

                        if (matches.Any())
                        {
                            DbProgram = matches.OrderByDescending(x => x.account_modifed_datetime).FirstOrDefault();
                            updated++;
                        }
                        else
                        {
                            DbProgram.account_guid = Guid.NewGuid();

                            DbProgram.account_number = dw_program.sis_program_id;
                            DbProgram.account_name = dw_program.sis_program_title;

                            DbProgram.account_created_by = Settings.CrmAdmin;
                            DbProgram.account_created_date = dw_program.sis_program_created_date;

                            db_ctx.crm_accounts.Add(DbProgram);
                            inserted++;
                        }

                        DbProgram.account_active = (dw_program.sis_program_end_date == null);

                        DbProgram.account_parent_account_id = Settings.LcAccountId;

                        DbProgram.account_type = AccountTypes.AcademicProgram;
                        DbProgram.account_record_type_id = AccountRecordTypes.AcademicProgram;

                        DbProgram.account_modified_by = Settings.CrmAdmin;
                        DbProgram.account_modifed_datetime = dw_program.sis_program_modified_datetime;

                        db_ctx.SaveChanges();

                        total++;
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

        private bool SyncColleagueCourses()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string studentRoles = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<wt_courses> wt_courses = new List<wt_courses>();


            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName); 

                wt_courses = db_ctx.wt_courses.Where(x => x.sis_course_deleted == false
                                                          && (x.sis_course_created_date >= LastRunDate
                                                          || x.sis_course_modified_datetime >= LastRunDate)).ToList();

                if (MonitorExecution(MethodName, wt_courses.Count))
                {

                    foreach (wt_courses dw_course in wt_courses.OrderByDescending(x => x.sis_course_modified_datetime).ToList())
                    {
                        crm_courses DbCourse = new crm_courses();

                        var matches = db_ctx.crm_courses.Where(x => x.sis_course_id == dw_course.sis_course_id);

                        if (matches.Any())
                        {
                            DbCourse = matches.OrderByDescending(x => x.course_modified_datetime).FirstOrDefault();
                            updated++;
                        }
                        else
                        {
                            // crmdb key
                            DbCourse.course_guid = Guid.NewGuid();

                            // colleague key
                            DbCourse.sis_course_id = dw_course.sis_course_id;

                            DbCourse.course_created_by = Settings.CrmAdmin;
                            DbCourse.course_created_datetime = DateTime.Now;

                            db_ctx.crm_courses.Add(DbCourse);
                            inserted++;
                        }

                        DbCourse.course_name = dw_course.sis_course_title;
                        DbCourse.course_number = dw_course.sis_course_name;

                        DbCourse.course_department = GetDeptId(dw_course.sis_course_id);

                        DbCourse.course_modified_by = Settings.CrmAdmin;
                        DbCourse.course_modified_datetime = dw_course.sis_course_modified_datetime ?? DateTime.Now;

                        db_ctx.SaveChanges();

                        total++;
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

        private bool SyncColleagueCourseOfferings()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string studentRoles = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            List<wt_course_sections> wt_course_sections = new List<wt_course_sections>();

            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName); 

                wt_course_sections = db_ctx.wt_course_sections.Where(x => x.sis_cs_created_date >= LastRunDate
                                                                  || x.sis_cs_created_date >= LastRunDate
                                                                  || x.sis_cs_modified_datetime >= LastRunDate).ToList();

                if (MonitorExecution(MethodName, wt_course_sections.Count))
                {
                    foreach (wt_course_sections dw_course_offering in wt_course_sections.OrderByDescending(x => x.sis_cs_modified_datetime).ToList())
                    {
                        crm_course_offerings DbCourseOffering = new crm_course_offerings();

                        var matches = db_ctx.crm_course_offerings.Where(x => x.sis_course_section_id == dw_course_offering.sis_course_section_id).ToList();

                        if (matches.Any())
                        {
                            DbCourseOffering = matches.OrderByDescending(x => x.course_offering_modified_datetime).FirstOrDefault();
                            updated++;
                        }
                        else
                        {
                            DbCourseOffering.course_offering_guid = Guid.NewGuid();

                            DbCourseOffering.sis_course_section_id = dw_course_offering.sis_course_section_id;

                            DbCourseOffering.course_offering_created_by = Settings.CrmAdmin;
                            DbCourseOffering.course_offering_created_datetime = DateTime.Now;

                            db_ctx.crm_course_offerings.Add(DbCourseOffering);
                            inserted++;
                        }

                        DbCourseOffering.course_offering_term = GetTermId(dw_course_offering.sis_cs_sec_term);

                        DbCourseOffering.course_offering_start_datetime = dw_course_offering.sis_cs_sec_start_date;
                        DbCourseOffering.course_offering_end_datetime = dw_course_offering.sis_cs_sec_end_date;

                        DbCourseOffering.course_offering_course = GetCourseId(dw_course_offering.sis_cs_sec_course);

                        DbCourseOffering.course_offering_name = dw_course_offering.sis_cs_sec_name;
                        DbCourseOffering.course_offering_capacity = dw_course_offering.sis_cs_sec_capacity;

                        DbCourseOffering.course_offering_modified_by = Settings.CrmAdmin;
                        DbCourseOffering.course_offering_modified_datetime = dw_course_offering.sis_cs_modified_datetime ?? DateTime.Now;

                        db_ctx.SaveChanges();

                        total++;
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

        private bool SyncColleagueCourseConnections()
        {
            int inserted = 0;
            int updated = 0;
            int total = 0;

            bool success = false;
            string ErrorMsg = string.Empty;
            string studentRoles = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;

            List<wt_courses> wt_courses = new List<wt_courses>();

            try
            {
                MethodName = GetCurrentMethod();

                DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName); 

                wt_courses = db_ctx.wt_courses.Where(x => x.sis_course_deleted == false
                                                                    && (x.sis_course_created_date >= LastRunDate
                                                                   || x.sis_course_modified_datetime >= LastRunDate)).ToList();

                if (MonitorExecution(MethodName, wt_courses.Count))
                {
                    foreach (wt_courses dw_course in wt_courses.OrderByDescending(x => x.sis_course_modified_datetime).ToList())
                    {
                        crm_courses DbCourse = new crm_courses();

                        var matches = db_ctx.crm_courses.Where(x => x.crm_course_id == dw_course.sis_course_id);

                        if (matches.Any())
                        {
                            DbCourse = matches.OrderByDescending(x => x.course_modified_datetime).FirstOrDefault();
                        }
                        else
                        {
                            DbCourse.course_guid = Guid.NewGuid();
                            DbCourse.course_created_by = Settings.CrmAdmin;
                            DbCourse.course_created_datetime = DateTime.Now;

                            db_ctx.crm_courses.Add(DbCourse);
                        }


                        DbCourse.course_modified_by = Settings.CrmAdmin;
                        DbCourse.course_modified_datetime = DateTime.Now;

                        db_ctx.SaveChanges();

                        updated++; total++;
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

        //private bool SyncColleagueApplications()
        //{
        //    int inserted = 0;
        //    int updated = 0;
        //    int total = 0;

        //    bool success = false;
        //    string ErrorMsg = string.Empty;
        //    string studentRoles = string.Empty;
        //    string MethodName = string.Empty;
        //    DateTime StartTime = DateTime.Now;


        //    List<v_dw_student_applications> v_dw_student_applications = new List<v_dw_student_applications>();

        //    try
        //    {
        //        MethodName = GetCurrentMethod();

        //        DateTime? LastRunDate = LastRunDate = GetLastRunDateTime(MethodName);  // 

        //        v_dw_student_applications = db_ctx.v_dw_student_applications.Where(x => x.sis_appl_mark_delete == false
        //                                                            && (x.sis_application_id == null
        //                                                                || (x.sis_appl_modified_date >= LastRunDate
        //                                                                || x.sis_appl_date >= LastRunDate)
        //                                                                )).ToList();

        //        if (MonitorExecution(MethodName, v_dw_student_applications.Count))
        //        {
        //            foreach (v_dw_student_applications dw_application in v_dw_student_applications)
        //            {
        //                crm_applications DbApplication = new crm_applications();

        //                var matches = db_ctx.crm_applications.Where(x => x.application_deleted == false
        //                                                            && x.sis_application_id == dw_application.sis_application_id);

        //                if (matches.Any())
        //                {
        //                    DbApplication = matches.OrderByDescending(x => x.appl_modfied_date).FirstOrDefault();
        //                    updated++;
        //                }
        //                else
        //                {
        //                    DbApplication.sis_application_id = dw_application.sis_application_id;
        //                    DbApplication.sis_student_id = dw_application.sis_student_id;
        //                    DbApplication.crm_contact_id = dw_application.crm_contact_id;

        //                    DbApplication.application_guid = Guid.NewGuid();
        //                    DbApplication.appl_created_by = Settings.CrmAdmin;
        //                    DbApplication.appl_created_date = DateTime.Now;
        //                    DbApplication.appl_location = dw_application.sis_appl_location;
        //                    DbApplication.appl_owner_id = Settings.CrmAdmin;

        //                    db_ctx.crm_applications.Add(DbApplication);
        //                    inserted++;
        //                }

        //                DbApplication.crm_program_id = dw_application.crm_program_id;

        //                DbApplication.intended_start_term = dw_application.sis_appl_start_term;

        //                if (dw_application.sis_appl_start_year != null)
        //                {
        //                    DbApplication.intended_start_year = dw_application.sis_appl_start_year.ToString();
        //                }

        //                DbApplication.appl_stage = dw_application.sis_appl_stage;
        //                DbApplication.intended_student_load = dw_application.sis_appl_stu_load_intent;

        //                DbApplication.application_status = dw_application.sis_appl_status;
        //                DbApplication.appl_status_date = dw_application.sis_appl_status_date;
        //                DbApplication.app_status_date = dw_application.sis_app_status_datetime;
        //                DbApplication.alt_status_date = dw_application.sis_alt_status_datetime;
        //                DbApplication.con_status_date = dw_application.sis_con_status_datetime;
        //                DbApplication.dac_status_date = dw_application.sis_dac_status_datetime;
        //                DbApplication.dtc_status_date = dw_application.sis_dtc_status_datetime;
        //                DbApplication.fi_status_date = dw_application.sis_fi_status_datetime;
        //                DbApplication.fw_status_date = dw_application.sis_fw_status_datetime;
        //                DbApplication.ms_status_date = dw_application.sis_ms_status_datetime;
        //                DbApplication.ntq_status_date = dw_application.sis_ntq_status_datetime;
        //                DbApplication.ofc_status_date = dw_application.sis_ofc_status_datetime;
        //                DbApplication.offer_due_date = dw_application.sis_appl_offer_due_date;
        //                DbApplication.ofi_status_date = dw_application.sis_ofi_status_datetime;
        //                DbApplication.par_status_date = dw_application.sis_par_status_datetime;
        //                DbApplication.pas_status_date = dw_application.sis_pas_status_datetime;
        //                DbApplication.ppr_status_date = dw_application.sis_ppr_status_datetime;
        //                DbApplication.pr_status_date = dw_application.sis_pr_status_datetime;
        //                DbApplication.sc_status_date = dw_application.sis_sc_status_datetime;
        //                DbApplication.unc_status_date = dw_application.sis_unc_status_datetime;
        //                DbApplication.wap_status_date = dw_application.sis_wap_status_datetime;
        //                DbApplication.wtl_status_date = dw_application.sis_wtl_status_datetime;
        //                DbApplication.w_status_date = dw_application.sis_w_status_datetime;

        //                DbApplication.appl_modified_by = Settings.CrmAdmin;
        //                DbApplication.appl_modfied_date = DateTime.Now;

        //                db_ctx.SaveChanges();

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

    }
}
