using CrmCore.Models;
using lc.crm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using static CrmCore.Enumerations;

namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        private string GetSfRecordTypeById(string id)
        {

            string recordType = SfRecordTypes.Unrecognized;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;

            try
            {
                MethodName = GetCurrentMethod();

                if (!string.IsNullOrEmpty(id))
                {
                    if (id.Length == 18)
                    {

                        switch (id.Substring(0, 3))
                        {
                            case "001":
                                recordType = SfRecordTypes.Account;
                                break;
                            case "a03":
                                recordType = SfRecordTypes.CourseConnection;
                                break;
                            case "a01":
                                recordType = SfRecordTypes.Affiliation;
                                break;
                            case "a08":
                                recordType = SfRecordTypes.ProgramEnrollment;
                                break;
                            case "a0V":
                                recordType = SfRecordTypes.EventRegistration;
                                break;
                            case "a0O":
                                recordType = SfRecordTypes.EmailBroadcast;
                                break;
                            case "a0N":
                                recordType = SfRecordTypes.EmailCampaign;
                                break;
                            case "a0U":
                                recordType = SfRecordTypes.ActivityExtender;
                                break;
                            case "00U":
                                recordType = SfRecordTypes.Event;
                                break;
                            case "00T":
                                recordType = SfRecordTypes.Task;
                                break;
                            case "a0J":
                                recordType = SfRecordTypes.Inquiry;
                                break;
                            case "003":
                                recordType = SfRecordTypes.Contact;
                                break;
                            case "a04":
                                recordType = SfRecordTypes.CourseOffering;
                                break;
                            case "a05":
                                recordType = SfRecordTypes.Course;
                                break;
                            case "a0W":
                                recordType = SfRecordTypes.Application;
                                break;
                            case "a0o":
                                recordType = SfRecordTypes.InquiryProgram;
                                break;
                            default:
                                recordType = SfRecordTypes.Unrecognized;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(MethodName, ErrorMsg);

            return recordType;
        }

        private crm_contacts GetContactByStudentId(string StudentId)
        {
            crm_contacts _Contact = new crm_contacts();
            string ErrorMsg = string.Empty;

            try
            {
                var retval = db_ctx.crm_contacts.Where(x => x.contact_colleague_id == StudentId
                                                && x.contact_deleted == false)
                                            .OrderByDescending(y => y.contact_last_modified_datetime);

                if (retval.Any())
                {
                    if (retval != null)
                    {
                        _Contact = retval.FirstOrDefault();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return _Contact;
        }

        private string GetProgId(string code)
        {
            string result = "";
            string ErrorMsg = string.Empty;

            try
            {
                var results = db_ctx.crm_accounts.Where(x => x.account_record_type_id == "0121U000001MGdWQAW"
                                                     && x.account_number == code).ToList();

                if (result.Any())
                {
                    result = results.OrderByDescending(x => x.account_modifed_datetime).FirstOrDefault().account_id;
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return result;
        }

        private string GetDeptId(string code)
        {
            string result = "";
            string ErrorMsg = string.Empty;

            try
            {
                var dept_codes = db_ctx.wt_course_departments.Where(x => x.sis_course_id == code).ToList();

                if (dept_codes.Any())
                {
                    string dept_code = dept_codes.FirstOrDefault().sis_course_department;
                    if (!string.IsNullOrEmpty(dept_code))
                    {
                        var r = db_ctx.crm_accounts.Where(x => x.account_record_type_id == "0121U000001MGdVQAW"
                                                     && x.account_number == dept_code).ToList();

                        if (r.Any())
                        {
                            result = r.FirstOrDefault().account_id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return result;
        }

        private string GetTermId(string code)
        {
            string retVal = string.Empty;
            string ErrorMsg = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    retVal = db_ctx.crm_terms.Where(x => x.term_code == code
                                                                  && x.term_deleted == false)
                                                         .FirstOrDefault().term_id;
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return retVal;
        }

        private string GetCourseId(string code)
        {
            string retVal = string.Empty;
            string ErrorMsg = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var possible_matches = db_ctx.crm_courses.Where(x => x.sis_course_id.Trim() == code.Trim()
                                                           //&& x.course_deleted != false
                                                           ).ToList();

                    if (possible_matches.Any())
                    {

                        var result = possible_matches.OrderByDescending(x => x.course_modified_datetime).FirstOrDefault();

                        if (result != null)
                        {
                            retVal = result.crm_course_id;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return retVal;
        }

        private string GetSoqlString(SoqlEnums soqlEnum, DownloadTypes downloadTypes, int limit, DateTime? lastRunDate, string csv = null)
        {
            string soqlStr = string.Empty;
            string ErrorMsg = string.Empty;
            string lastRunString = string.Empty;
            string targetDate = "lc_crmdb_last_sync__c";

            try
            {
                soqlStr = db_ctx.soql_queries.Where(x => x.soql_query_data_type == (int)soqlEnum).OrderByDescending(o => o.soql_query_modified_datetime).Select(s => s.soql_query_string).FirstOrDefault();

                if (soqlStr.ToUpper().Contains("WHERE"))
                {
                    if (string.IsNullOrEmpty(csv)) { 
                        soqlStr += " AND";
                    }
                }
                else
                {
                    soqlStr += " WHERE";
                }

                if (lastRunDate != null) {
                    lastRunString = GetUTCTime(lastRunDate).ToString("s") + "Z";
                }

                if (soqlEnum == SoqlEnums.DownloadReports || soqlEnum == SoqlEnums.DownloadEmailTemplates)
                {
                    targetDate = "LastModifiedDate";
                }

                if (downloadTypes == DownloadTypes.LatestModifiedType || downloadTypes == DownloadTypes.NewRecordType)
                {
                    soqlStr += " (CreatedDate > " + lastRunString + "  OR LastModifiedDate > " + lastRunString + ")";
                }
                else if (downloadTypes == DownloadTypes.BackLoadModifiedType) {
                    soqlStr += " (" + targetDate + " < " + lastRunString + " OR " + targetDate + " = null)";
                }

                if (!string.IsNullOrEmpty(csv))
                {
                    soqlStr = soqlStr.Replace(":csv", csv);
                }

                if (limit > 0) { 
                    soqlStr += " ORDER BY " + targetDate + " ASC LIMIT " + limit.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return soqlStr;
        }

        public List<SelectListItem> GetTermOptions()
        {
            string ErrorMsg = string.Empty;
            List<SelectListItem> future_terms = new List<SelectListItem>();

            try
            {
                var terms = db_ctx.crm_terms;

                future_terms = terms.Select(t => new SelectListItem
                {
                    Text = t.term_name,
                    Value = t.term_id
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return future_terms;
        }

        public List<SelectListItem> GetFutureTermOptions()
        {
            string ErrorMsg = string.Empty;
            List<SelectListItem> future_terms = new List<SelectListItem>();
            List<string> UniqueIds = new List<string>();
            List<string> Semesters = new List<string>() { "WN", "S2", "SM", "S1", "FL" };

            try
            {
                var terms = db_ctx.crm_terms.Where(x => x.term_code != null
                                                    && x.term_deleted == false
                                                    && x.term_mark_delete == false
                                                    //&& x.term_show_web == true
                                                    )
                                            .Distinct()
                                            .OrderBy(a => a.term_code.Substring(0, 2))
                                            .ThenBy(o => o.term_code.Substring(2, 2).Replace("WN", "A")
                                                                                    .Replace("S2", "B")
                                                                                    .Replace("SM", "C")
                                                                                    .Replace("S1", "D")
                                                                                    .Replace("FL", "E"));

                foreach (crm_terms term in terms) {
                    if (!UniqueIds.Contains(term.term_id)) {
                        int future_year = Convert.ToInt32(term.term_code.Substring(0, 2));
                        int current_year = Convert.ToInt32(DateTime.Now.ToString("yy"));
                        string semester = term.term_code.Substring(2, 2);
                        if (future_year >= current_year && future_year < (current_year + 3) && Semesters.Contains(semester))
                        {
                            string term_name = "20" + future_year + " ";
                            switch (semester) {
                                case "WN":
                                    term_name += "Winter (Jan-Apr)";
                                    break;
                                case "S2":
                                    term_name += "Spring Short (May-Jun)";
                                    break;
                                case "SM":
                                    term_name += "Spring Long (May-Aug)";
                                    break;
                                case "S1":
                                    term_name += "Summer Short (Jul-Aug)";
                                    break;
                                case "FL":
                                    term_name += "Fall (Sep-Dec)";
                                    break;
                            }

                            future_terms.Add(new SelectListItem() { Text = term_name, Value = term.term_id });
                            UniqueIds.Add(term.term_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return future_terms;
        }

        public List<SelectListItem> GetProgramOptions()
        {
            string ErrorMsg = string.Empty;
            List<SelectListItem> programs = new List<SelectListItem>();


            try
            {
                var progs = db_ctx.crm_accounts.Where(x => x.account_deleted == false
                                                            && x.account_mark_delete == false
                                                            && x.account_record_type_id == AccountRecordTypes.AcademicProgram
                                                            && x.account_active == true
                                                            && x.lc_account_active == true
                                                            //&& x.account_show_web == true
                                                            )
                                            .Distinct()
                                            .OrderBy(a => a.account_name);

                foreach (crm_accounts program in progs)
                {
                    programs.Add(new SelectListItem() { Text = program.account_name_alias, Value = program.account_id });
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return programs;
        }

        public List<SelectListItem> GetSfadEventOptions()
        {
            string ErrorMsg = string.Empty;
            List<SelectListItem> future_terms = new List<SelectListItem>();


            try
            {
                
                var fixedOrder = new List<string> { "WN", "FL", "S1", "S2", "SM" };
                var terms = db_ctx.crm_terms.Where(x => x.term_code != null).OrderBy(a => a.term_code.Substring(0, 2));

                List<string> semesters = new List<string>() { "WN", "FL", "SM" };
                foreach (crm_terms term in terms)
                {
                    int future_year = Convert.ToInt32(term.term_code.Substring(0, 2));
                    int current_year = Convert.ToInt32(DateTime.Now.ToString("yy"));
                    string semester = term.term_code.Substring(2, 2);
                    if (future_year > current_year && future_year < (current_year + 3) && semesters.Contains(semester))
                    {
                        future_terms.Add(new SelectListItem() { Text = term.term_name, Value = term.term_id });
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            if (!string.IsNullOrEmpty(ErrorMsg)) RecordError(GetCurrentMethod(), ErrorMsg);

            return future_terms;
        }

        public ContactLookupDataModel ContactLookup(ContactLookupDataModel lookupData)
        {
            string ErrorMsg = string.Empty;
            string studentId = string.Empty;
            string contactId = string.Empty;
            string barcode = string.Empty;
            crm_contacts DbContact = new crm_contacts(); ;

            try
            {

                var crm_contacts = db_ctx.crm_contacts.Where(x => x.contact_deleted == false
                                                        && x.contact_mark_delete == false);

                if (!string.IsNullOrEmpty(lookupData.FirstName)) {
                    crm_contacts = crm_contacts.Where(x => x.contact_first_name.Contains(lookupData.FirstName));
                }

                if (!string.IsNullOrEmpty(lookupData.LastName))
                {
                    crm_contacts = crm_contacts.Where(x => x.contact_last_name.Contains(lookupData.LastName));
                }

                if (!string.IsNullOrEmpty(lookupData.EmailAddress))
                {
                    crm_contacts = crm_contacts.Where(x => x.contact_college_email.Contains(lookupData.EmailAddress)
                                                       || x.contact_alternate_email.Contains(lookupData.EmailAddress)
                                                       || x.contact_work_email.Contains(lookupData.EmailAddress));
                }

                if (crm_contacts.Any())
                {
                    DbContact = crm_contacts.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();

                    if (DbContact != null) {
                        lookupData.ContactId = DbContact.contact_id;
                        lookupData.sNumber = DbContact.contact_colleague_id;
                        lookupData.FirstName = DbContact.contact_first_name;
                        lookupData.LastName = DbContact.contact_last_name;
                        lookupData.BirthDate = DbContact.contact_birthdate ?? DateTime.Today;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(studentId))
                    {
                        DbContact.contact_guid = Guid.NewGuid();
                        DbContact.contact_first_name = lookupData.FirstName;
                        DbContact.contact_last_name = lookupData.LastName;
                        DbContact.contact_alternate_email = lookupData.EmailAddress;

                        db_ctx.crm_contacts.Add(DbContact);
                        db_ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            return lookupData;
        }
    }
}
