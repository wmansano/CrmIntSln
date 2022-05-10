using lc.crm;
using lc.crm.api;
using System;
using System.Collections.Generic;

namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        private Account MapToSfOrganization(crm_accounts DbOrganization)
        {
            Account SfOrganization= new Account();

            try
            {
                if (DbOrganization != null)
                {
                    if (!string.IsNullOrWhiteSpace(DbOrganization.account_id)
                            && DbOrganization.account_id != SfOrganization.Id) {
                        SfOrganization.Id = DbOrganization.account_id;
                    }

                    if (DbOrganization.account_guid != null
                            && SfOrganization.lc_account_guid__c != DbOrganization.account_guid.ToString()) {
                        SfOrganization.lc_account_guid__c = DbOrganization.account_guid.ToString();
                    }

                    SfOrganization.lc_crmdb_last_sync__cSpecified = true;
                    SfOrganization.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
                }

            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfOrganization;
        }

        private Contact MapToSfContact(crm_contacts DbContact)
        {
            Contact SfContact = new Contact();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbContact.contact_id)
                        && DbContact.contact_id != SfContact.Id)
                {
                    SfContact.Id = DbContact.contact_id;
                }

                if (DbContact.contact_guid != null 
                        && !string.IsNullOrWhiteSpace(DbContact.contact_guid.ToString()))
                {
                    SfContact.lc_contact_guid__c = DbContact.contact_guid.ToString();
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_type))
                {
                    SfContact.lc_contact_type__c = DbContact.contact_type;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_legacy_id))
                {
                    SfContact.lc_legacy_id__c = DbContact.contact_legacy_id;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_colleague_id))
                {
                    SfContact.lc_colleague_id__c = DbContact.contact_colleague_id;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_first_name))
                {
                    SfContact.FirstName = DbContact.contact_first_name;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_last_name)) {
                    SfContact.LastName = DbContact.contact_last_name;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_middle_name)) {
                    SfContact.lc_middle_name__c = DbContact.contact_middle_name;
                }

                if (DbContact.contact_birthdate != null)
                {
                    SfContact.BirthdateSpecified = true;
                    SfContact.Birthdate = DbContact.contact_birthdate;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_mailing_city)) {
                    SfContact.MailingCity = DbContact.contact_mailing_city;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_mailing_province)) {
                    SfContact.MailingState = DbContact.contact_mailing_province;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_mailing_country)) {
                    SfContact.MailingCountry = DbContact.contact_mailing_country;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_mobile_phone)) {
                    SfContact.Phone = DbContact.contact_mobile_phone;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_primary_academic_program))
                {
                    SfContact.Primary_Academic_Program__c = DbContact.contact_primary_academic_program;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_alternate_email))
                {
                    SfContact.hed__AlternateEmail__c = CleanEmail(DbContact.contact_alternate_email);
                    SfContact.hed__Preferred_Email__c = PreferredEmailTypes.AlternateEmailType;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_college_email) && DbContact.contact_college_email.Contains("lethbridgecollege"))
                {
                    SfContact.hed__UniversityEmail__c = CleanEmail(DbContact.contact_college_email);
                    SfContact.hed__Preferred_Email__c = PreferredEmailTypes.UniversityEmailType;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_work_email))
                {
                    SfContact.hed__WorkEmail__c = CleanEmail(DbContact.contact_work_email);
                    SfContact.hed__Preferred_Email__c = PreferredEmailTypes.WorkEmailType;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_record_type_id)) {
                    SfContact.RecordTypeId = DbContact.contact_record_type_id ?? "0121U000000e7eAQAQ";
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_lead_source)) {
                    SfContact.LeadSource = DbContact.contact_lead_source;
                }

                if (!string.IsNullOrWhiteSpace(DbContact.contact_account_id)) {
                    SfContact.AccountId = DbContact.contact_account_id;
                }

                SfContact.lc_int_rec_alumni__cSpecified = true;
                SfContact.lc_int_rec_alumni__c = DbContact.contact_alumni_type;

                SfContact.lc_int_rec_student__cSpecified = true;
                SfContact.lc_int_rec_student__c = DbContact.contact_student_type;

                SfContact.lc_possible_duplicate__cSpecified = true;
                SfContact.lc_possible_duplicate__c = DbContact.contact_potential_duplicate;

                SfContact.lc_crmdb_last_sync__cSpecified = true;
                SfContact.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);

            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfContact;
        }

        private hed__Course__c MapToSfCourse(crm_courses DbCourse)
        {
            hed__Course__c SfCourse = new hed__Course__c();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbCourse.crm_course_id)
                        && SfCourse.Id != DbCourse.crm_course_id)
                {
                    SfCourse.Id = DbCourse.crm_course_id;
                }

                if (DbCourse.course_guid != null
                        && SfCourse.lc_course_guid__c != DbCourse.course_guid.ToString())
                {
                    SfCourse.lc_course_guid__c = DbCourse.course_guid.ToString();
                }

                if (!string.IsNullOrWhiteSpace(DbCourse.course_name))
                {
                    SfCourse.Name = DbCourse.course_name;
                }

                if (!string.IsNullOrWhiteSpace(DbCourse.course_number))
                {
                    SfCourse.hed__Course_ID__c = DbCourse.course_number;
                }

                if (!string.IsNullOrWhiteSpace(DbCourse.sis_course_id))
                {
                    SfCourse.lc_colleague_course_id__c = DbCourse.sis_course_id;
                }

                if (!string.IsNullOrWhiteSpace(DbCourse.course_department))
                {
                    SfCourse.hed__Account__c = DbCourse.course_department;
                }

                SfCourse.lc_crmdb_last_sync__cSpecified = true;
                SfCourse.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);

            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfCourse;
        }

        private hed__Course_Offering__c MapToSfCourseOffering(crm_course_offerings DbCourseOffering)
        {
            hed__Course_Offering__c SfCourseOffering = new hed__Course_Offering__c();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbCourseOffering.course_offering_id)
                        && SfCourseOffering.Id != DbCourseOffering.course_offering_id)
                {
                    SfCourseOffering.Id = DbCourseOffering.course_offering_id;
                }

                if (DbCourseOffering.course_offering_guid != null
                        && SfCourseOffering.lc_course_offering_guid__c != DbCourseOffering.course_offering_guid.ToString())
                {
                    SfCourseOffering.lc_course_offering_guid__c = DbCourseOffering.course_offering_guid.ToString();
                }

                if (!string.IsNullOrWhiteSpace(DbCourseOffering.sis_course_section_id))
                {
                    SfCourseOffering.hed__Section_ID__c = DbCourseOffering.sis_course_section_id;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseOffering.course_offering_name))
                {
                    SfCourseOffering.Name = DbCourseOffering.course_offering_name;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseOffering.course_offering_course))
                {
                    SfCourseOffering.hed__Course__c = DbCourseOffering.course_offering_course;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseOffering.course_offering_primary_faculty))
                {
                    SfCourseOffering.hed__Faculty__c = DbCourseOffering.course_offering_primary_faculty;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseOffering.course_offering_term))
                {
                    SfCourseOffering.hed__Term__c = DbCourseOffering.course_offering_term;
                }

                if (DbCourseOffering.course_offering_capacity != null)
                {
                    SfCourseOffering.hed__Capacity__cSpecified = true;
                    SfCourseOffering.hed__Capacity__c = (double)DbCourseOffering.course_offering_capacity;
                }

                if (DbCourseOffering.course_offering_start_datetime != null)
                {
                    SfCourseOffering.hed__Start_Date__cSpecified = true;
                    SfCourseOffering.hed__Start_Date__c = DbCourseOffering.course_offering_start_datetime;
                }

                if (DbCourseOffering.course_offering_end_datetime != null)
                {
                    SfCourseOffering.hed__End_Date__cSpecified = true;
                    SfCourseOffering.hed__End_Date__c = DbCourseOffering.course_offering_end_datetime;
                }

                SfCourseOffering.lc_crmdb_last_sync__cSpecified = true;
                SfCourseOffering.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfCourseOffering;
        }

        private hed__Course_Enrollment__c MapToSfCourseConnections(crm_course_connections DbCourseConnection)
        {
            hed__Course_Enrollment__c SfCourseConnection = new hed__Course_Enrollment__c();

            try
            {
                if (DbCourseConnection.course_connection_id != null
                        && SfCourseConnection.Id != DbCourseConnection.course_connection_id)
                {
                    SfCourseConnection.Id = DbCourseConnection.course_connection_id;
                }

                if (DbCourseConnection.course_connection_guid != null
                        && SfCourseConnection.lc_course_connection_guid__c != DbCourseConnection.course_connection_guid.ToString())
                {
                    SfCourseConnection.lc_course_connection_guid__c = DbCourseConnection.course_connection_guid.ToString();
                }

                if (!string.IsNullOrWhiteSpace(DbCourseConnection.course_connection_name)) {
                    SfCourseConnection.lc_course_connection_key__c = DbCourseConnection.course_connection_name;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseConnection.course_connection_program_enrollment_id)) {
                    SfCourseConnection.hed__Program_Enrollment__c = DbCourseConnection.course_connection_program_enrollment_id;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseConnection.course_connection_contact_id))
                {
                    SfCourseConnection.hed__Contact__c = DbCourseConnection.course_connection_contact_id;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseConnection.course_offering_id))
                {
                    SfCourseConnection.hed__Course_Offering__c = DbCourseConnection.course_offering_id;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseConnection.course_connection_program_id))
                {
                    SfCourseConnection.hed__Account__c = DbCourseConnection.course_connection_program_id;
                }

                if (!string.IsNullOrWhiteSpace(DbCourseConnection.course_connection_record_type))
                {
                    SfCourseConnection.RecordTypeId = DbCourseConnection.course_connection_record_type;
                }

                SfCourseConnection.lc_crmdb_last_sync__cSpecified = true;
                SfCourseConnection.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfCourseConnection;
        }

        private hed__Affiliation__c MapToSfAffiliations(crm_affiliations DbAffiliation)
        {
            hed__Affiliation__c SfAffiliation= new hed__Affiliation__c();
            List<string> FieldsToNull = new List<string>();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_id)
                        && SfAffiliation.Id != DbAffiliation.affiliation_id)
                {
                    SfAffiliation.Id = DbAffiliation.affiliation_id;
                }

                if (DbAffiliation.affiliation_guid != null
                        && SfAffiliation.lc_affiliation_guid__c != DbAffiliation.affiliation_guid.ToString())
                {
                    SfAffiliation.lc_affiliation_guid__c = DbAffiliation.affiliation_guid.ToString();
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_organization_id))
                {
                    SfAffiliation.hed__Account__c = DbAffiliation.affiliation_organization_id;
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_contact_id))
                {
                    SfAffiliation.hed__Contact__c = DbAffiliation.affiliation_contact_id;
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_type))
                {
                    SfAffiliation.hed__Affiliation_Type__c = DbAffiliation.affiliation_type;
                }

                if (DbAffiliation.affiliation_end_date != null)
                {
                    SfAffiliation.hed__EndDate__cSpecified = true;
                    SfAffiliation.hed__EndDate__c = GetUTCTime(DbAffiliation.affiliation_end_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfAffiliation.hed__EndDate__c));
                }

                SfAffiliation.hed__Primary__cSpecified = true;
                SfAffiliation.hed__Primary__c = DbAffiliation.affiliation_primary;

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_role))
                {
                    SfAffiliation.hed__Role__c = DbAffiliation.affiliation_role;
                }

                // no date portion to field, UTC converation is not necessary
                if (DbAffiliation.affiliation_start_date != null)
                {
                    SfAffiliation.hed__StartDate__cSpecified = true;
                    SfAffiliation.hed__StartDate__c = DbAffiliation.affiliation_start_date;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfAffiliation.hed__StartDate__c));
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_status))
                {
                    SfAffiliation.hed__Status__c = DbAffiliation.affiliation_status;
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_program_status))
                {
                    SfAffiliation.Program_Status__c = DbAffiliation.affiliation_program_status;
                }

                // UTC datetime conversion unnecessary - this is only the date portion i.e. no time
                if (DbAffiliation.affiliation_program_status_date != null) {
                    SfAffiliation.Program_Status_Date__c = DbAffiliation.affiliation_program_status_date;
                    SfAffiliation.Program_Status_Date__cSpecified = (DbAffiliation.affiliation_program_status_date != null);
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.lc_student_program_id))
                {
                    SfAffiliation.lc_student_program_id__c = DbAffiliation.lc_student_program_id;
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_record_type_id))
                {
                    SfAffiliation.RecordTypeId = DbAffiliation.affiliation_record_type_id;
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_role))
                {
                    SfAffiliation.hed__Role__c = DbAffiliation.affiliation_role;
                }

                if (!string.IsNullOrWhiteSpace(DbAffiliation.affiliation_owner_id))
                {
                    SfAffiliation.OwnerId = DbAffiliation.affiliation_owner_id;
                }

                SfAffiliation.fieldsToNull = FieldsToNull.ToArray();

                SfAffiliation.lc_crmdb_last_sync__cSpecified = true;
                SfAffiliation.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);

            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfAffiliation;
        }

        private lc_application__c MapToSfApplications(crm_applications DbApplication)
        {
            lc_application__c SfApplication = new lc_application__c();
            List<string> FieldsToNull = new List<string>();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbApplication.crm_application_id)
                        && SfApplication.Id != DbApplication.crm_application_id)
                {
                    SfApplication.Id = DbApplication.crm_application_id;
                }

                if (DbApplication.application_guid != null
                        && SfApplication.lc_application_guid__c != DbApplication.application_guid.ToString())
                {
                    SfApplication.lc_application_guid__c = DbApplication.application_guid.ToString();
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_application_guid__c));
                }

                // these should be non-nullable fields
                if (!string.IsNullOrEmpty(DbApplication.appl_owner_id))
                {
                    SfApplication.OwnerId = DbApplication.appl_owner_id ?? Settings.CrmAdmin;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.OwnerId));
                }

                if (!string.IsNullOrWhiteSpace(DbApplication.crm_contact_id))
                {
                    SfApplication.lc_applicant__c = DbApplication.crm_contact_id;
                }
                else {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_applicant__c));
                }

                if (!string.IsNullOrWhiteSpace(DbApplication.sis_application_id))
                {
                    SfApplication.lc_application_id__c = DbApplication.sis_application_id;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_application_id__c));
                }

                // CRM_PROGRAM_ID
                if (!string.IsNullOrWhiteSpace(DbApplication.crm_program_id))
                {
                    SfApplication.lc_academic_program__c = DbApplication.crm_program_id;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_academic_program__c));
                }

                // INTENDED_STUDENT_LOAD
                if (!string.IsNullOrWhiteSpace(DbApplication.intended_student_load))
                {
                    SfApplication.lc_intended_study_load__c = DbApplication.intended_student_load;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_intended_study_load__c));
                }

                // INTENDED_START_TERM
                if (!string.IsNullOrWhiteSpace(DbApplication.intended_start_term))
                {
                    SfApplication.lc_intended_start_term__c = DbApplication.intended_start_term;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_intended_start_term__c));
                }

                // INTENDED_START_YEAR
                if (!string.IsNullOrWhiteSpace(DbApplication.intended_start_year))
                {
                    SfApplication.lc_intended_start_year__c = DbApplication.intended_start_year;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_intended_start_year__c));
                }

                // APPL_LOCATION
                if (!string.IsNullOrWhiteSpace(DbApplication.appl_location))
                {
                    SfApplication.lc_campus_location__c = DbApplication.appl_location;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_campus_location__c));
                }

                // APPL_STAGE
                if (!string.IsNullOrWhiteSpace(DbApplication.appl_stage))
                {
                    SfApplication.lc_appl_stage__c = DbApplication.appl_stage;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_appl_stage__c));
                }

                // APPLICATION_STATUS
                if (!string.IsNullOrWhiteSpace(DbApplication.application_status))
                {
                    SfApplication.lc_application_status__c = DbApplication.application_status;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_application_status__c));
                }

                // APPL_ADMIT_STATUS
                if (!string.IsNullOrWhiteSpace(DbApplication.appl_admit_status))
                {
                    SfApplication.lc_admit_status__c = DbApplication.appl_admit_status;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_admit_status__c));
                }

                // APPL_STATUS_DATE
                if (DbApplication.appl_status_date != null)
                {
                    SfApplication.lc_appl_status_date__cSpecified = true;
                    SfApplication.lc_appl_status_date__c = GetUTCTime(DbApplication.appl_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_appl_status_date__c));
                }

                // ALT_STATUS_DATE
                if (DbApplication.alt_status_date != null)
                {
                    SfApplication.lc_alt_status_date__cSpecified = true;
                    SfApplication.lc_alt_status_date__c = GetUTCTime(DbApplication.alt_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_alt_status_date__c));
                }


                // APP_STATUS_DATE
                if (DbApplication.app_status_date != null)
                {
                    SfApplication.lc_app_status_date__cSpecified = true;
                    SfApplication.lc_app_status_date__c = GetUTCTime(DbApplication.app_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_app_status_date__c));
                }

                // CON_STATUS_DATE
                if (DbApplication.con_status_date != null)
                {
                    SfApplication.lc_con_status_date__cSpecified = true;
                    SfApplication.lc_con_status_date__c = GetUTCTime(DbApplication.con_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_con_status_date__c));
                }

                // DAC_STATUS_DATE
                if (DbApplication.dac_status_date != null)
                {
                    SfApplication.lc_dac_status_date__cSpecified = true;
                    SfApplication.lc_dac_status_date__c = GetUTCTime(DbApplication.dac_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_dac_status_date__c));
                }

                // DTC_STATUS_DATE
                if (DbApplication.dtc_status_date != null)
                {
                    SfApplication.lc_dtc_status_date__cSpecified = true;
                    SfApplication.lc_dtc_status_date__c = GetUTCTime(DbApplication.dtc_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_dtc_status_date__c));
                }

                // FI_STATUS_DATE
                if (DbApplication.fi_status_date != null)
                {
                    SfApplication.lc_fi_status_date__cSpecified = true;
                    SfApplication.lc_fi_status_date__c = GetUTCTime(DbApplication.fi_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_fi_status_date__c));
                }

                // FW_STATUS_DATE
                if (DbApplication.fw_status_date != null)
                {
                    SfApplication.lc_fw_status_date__cSpecified = true;
                    SfApplication.lc_fw_status_date__c = GetUTCTime(DbApplication.fw_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_fw_status_date__c));
                }

                // MS_STATUS_DATE
                if (DbApplication.ms_status_date != null)
                {
                    SfApplication.lc_ms_status_date__cSpecified = true;
                    SfApplication.lc_ms_status_date__c = GetUTCTime(DbApplication.ms_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_ms_status_date__c));
                }

                // NTQ_STATUS_DATE
                if (DbApplication.ntq_status_date != null)
                {
                    SfApplication.lc_ntq_status_date__cSpecified = true;
                    SfApplication.lc_ntq_status_date__c = GetUTCTime(DbApplication.ntq_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_ntq_status_date__c));
                }

                // OFC_STATUS_DATE
                if (DbApplication.ofc_status_date != null)
                {
                    SfApplication.lc_ofc_status_date__cSpecified = true;
                    SfApplication.lc_ofc_status_date__c = GetUTCTime(DbApplication.ofc_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_ofc_status_date__c));
                }

                // OFI_STATUS_DATE
                if (DbApplication.ofi_status_date != null)
                {
                    SfApplication.lc_ofi_status_date__cSpecified = true;
                    SfApplication.lc_ofi_status_date__c = GetUTCTime(DbApplication.ofi_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_ofi_status_date__c));
                }

                // PAR_STATUS_DATE
                if (DbApplication.par_status_date != null)
                {
                    SfApplication.lc_par_status_date__cSpecified = true;
                    SfApplication.lc_par_status_date__c = GetUTCTime(DbApplication.par_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_par_status_date__c));
                }

                // PAS_STATUS_DATE
                if (DbApplication.pas_status_date != null)
                {
                    SfApplication.lc_pas_status_date__cSpecified = true;
                    SfApplication.lc_pas_status_date__c = GetUTCTime(DbApplication.pas_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_pas_status_date__c));
                }

                // PPR_STATUS_DATE
                if (DbApplication.ppr_status_date != null)
                {
                    SfApplication.lc_ppr_status_date__cSpecified = true;
                    SfApplication.lc_ppr_status_date__c = GetUTCTime(DbApplication.ppr_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_ppr_status_date__c));
                }

                // PR_STATUS_DATE
                if (DbApplication.pr_status_date != null)
                {
                    SfApplication.lc_pr_status_date__cSpecified = true;
                    SfApplication.lc_pr_status_date__c = GetUTCTime(DbApplication.pr_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_pr_status_date__c));
                }

                // SC_STATUS_DATE
                if (DbApplication.sc_status_date != null)
                {
                    SfApplication.lc_sc_status_date__cSpecified = true;
                    SfApplication.lc_sc_status_date__c = GetUTCTime(DbApplication.sc_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_sc_status_date__c));
                }

                // UNC_STATUS_DATE
                if (DbApplication.unc_status_date != null)
                {
                    SfApplication.lc_unc_status_date__cSpecified = true;
                    SfApplication.lc_unc_status_date__c = GetUTCTime(DbApplication.unc_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_unc_status_date__c));
                }

                // W_STATUS_DATE
                if (DbApplication.w_status_date != null)
                {
                    SfApplication.lc_w_status_date__cSpecified = true;
                    SfApplication.lc_w_status_date__c = GetUTCTime(DbApplication.w_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_w_status_date__c));
                }

                // WAP_STATUS_DATE
                if (DbApplication.wap_status_date != null)
                {
                    SfApplication.lc_wap_status_date__cSpecified = true;
                    SfApplication.lc_wap_status_date__c = GetUTCTime(DbApplication.wap_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_wap_status_date__c));
                }

                // WTL_STATUS_DATE
                if (DbApplication.wtl_status_date != null)
                {
                    SfApplication.lc_wtl_status_date__cSpecified = true;
                    SfApplication.lc_wtl_status_date__c = GetUTCTime(DbApplication.wtl_status_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_wtl_status_date__c));
                }

                // OFFER_STATUS_DATE
                if (DbApplication.offer_due_date != null)
                {
                    SfApplication.lc_offer_due_date__cSpecified = true;
                    SfApplication.lc_offer_due_date__c = GetUTCTime(DbApplication.offer_due_date);
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfApplication.lc_offer_due_date__c));
                }

                SfApplication.fieldsToNull = FieldsToNull.ToArray();

                SfApplication.lc_crmdb_last_sync__cSpecified = true;
                SfApplication.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfApplication;
        }

        private lc_event_registration__c MapToSfEventRegistrations(crm_event_registrations DbEventRegistration)
        {
            lc_event_registration__c SfEventRegistration = new lc_event_registration__c();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbEventRegistration.event_registration_id)
                    && SfEventRegistration.Id != DbEventRegistration.event_registration_id)
                {
                    SfEventRegistration.Id = DbEventRegistration.event_registration_id;
                }

                if (DbEventRegistration.event_registration_guid != null
                     && SfEventRegistration.lc_event_registration_guid__c != DbEventRegistration.event_registration_guid.ToString())
                {
                    SfEventRegistration.lc_event_registration_guid__c = DbEventRegistration.event_registration_guid.ToString();
                }

                if (string.IsNullOrWhiteSpace(DbEventRegistration.event_registration_id))
                {
                    SfEventRegistration.lc_activity_extender__c = DbEventRegistration.event_registration_activity_extender_id;

                    SfEventRegistration.lc_contact__c = DbEventRegistration.event_registration_contact_id;
                }

                SfEventRegistration.IsDeleted = DbEventRegistration.event_registration_deleted;

                if (DbEventRegistration.event_registration_checkedin != null)
                {
                    SfEventRegistration.lc_checkedin__c = DbEventRegistration.event_registration_checkedin;
                    SfEventRegistration.lc_checkedin__cSpecified = true;
                }

                if (DbEventRegistration.event_registration_attended != null)
                {
                    SfEventRegistration.lc_attended__c = DbEventRegistration.event_registration_attended;
                    SfEventRegistration.lc_attended__cSpecified = true;
                }

                if (DbEventRegistration.event_registration_registered != null)
                {
                    SfEventRegistration.lc_registered__c = DbEventRegistration.event_registration_registered;
                    SfEventRegistration.lc_registered__cSpecified = true;
                }

                SfEventRegistration.lc_crmdb_last_sync__cSpecified = true;
                SfEventRegistration.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfEventRegistration;
        }

        private lc_email_broadcast__c MapToSfEmailBroadcasts(crm_email_broadcasts DbEmailBroadcast)
        {
            lc_email_broadcast__c SfEmailBroadcast = new lc_email_broadcast__c();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbEmailBroadcast.email_broadcast_id)
                        && SfEmailBroadcast.Id != DbEmailBroadcast.email_broadcast_id)
                {
                    SfEmailBroadcast.Id = DbEmailBroadcast.email_broadcast_id;
                }

                if (DbEmailBroadcast.email_broadcast_guid != null
                    && SfEmailBroadcast.lc_email_broadcast_guid__c != DbEmailBroadcast.email_broadcast_guid.ToString())
                {
                    SfEmailBroadcast.lc_email_broadcast_guid__c = DbEmailBroadcast.email_broadcast_guid.ToString();
                }


                if (!string.IsNullOrWhiteSpace(DbEmailBroadcast.email_broadcast_status)) {
                    SfEmailBroadcast.lc_broadcast_status__c = DbEmailBroadcast.email_broadcast_status;
                }
                
                if (DbEmailBroadcast.email_broadcast_sent != null) {
                    SfEmailBroadcast.lc_broadcast_sent__cSpecified = true;
                    SfEmailBroadcast.lc_broadcast_sent__c = GetUTCTime(DbEmailBroadcast.email_broadcast_sent);
                }

                if (DbEmailBroadcast.email_broadcast_send != null) {
                    SfEmailBroadcast.lc_broadcast_send__cSpecified = true;
                    SfEmailBroadcast.lc_broadcast_send__c = DbEmailBroadcast.email_broadcast_send;
                }

                if (DbEmailBroadcast.email_broadcast_messages_sent != null) {
                    SfEmailBroadcast.lc_messages_sent__cSpecified = true;
                    SfEmailBroadcast.lc_messages_sent__c = DbEmailBroadcast.email_broadcast_messages_sent;
                }

                if (!string.IsNullOrWhiteSpace(DbEmailBroadcast.email_campaign_id)) {
                    SfEmailBroadcast.lc_email_campaign__c = DbEmailBroadcast.email_campaign_id;
                }

                if (!string.IsNullOrWhiteSpace(DbEmailBroadcast.email_report_name)) {
                    SfEmailBroadcast.lc_email_report_name__c = DbEmailBroadcast.email_report_name;
                }

                if (!string.IsNullOrWhiteSpace(DbEmailBroadcast.email_template_name)) {
                    SfEmailBroadcast.lc_email_template_name__c = DbEmailBroadcast.email_template_name;
                }

                SfEmailBroadcast.lc_crmdb_last_sync__cSpecified = true;
                SfEmailBroadcast.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfEmailBroadcast;
        }

        private hed__Term__c MapToSfTerms(crm_terms DbTerm)
        {
            hed__Term__c SfTerm = new hed__Term__c();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbTerm.term_id) 
                    && SfTerm.Id != DbTerm.term_id)
                {
                    SfTerm.Id = DbTerm.term_id;
                }

                if (!string.IsNullOrWhiteSpace(DbTerm.term_guid.ToString())
                    && SfTerm.lc_term_guid__c != DbTerm.term_guid.ToString())
                {
                    SfTerm.lc_term_guid__c = DbTerm.term_guid.ToString();
                }

                if (!string.IsNullOrWhiteSpace(DbTerm.term_code))
                {
                    SfTerm.lc_term_code__c = DbTerm.term_code;
                }

                SfTerm.hed__Type__c = "Semester";

                if (!string.IsNullOrWhiteSpace(DbTerm.term_name))
                {
                    SfTerm.Name = DbTerm.term_name;
                }

                if (DbTerm.term_sequence_number != null)
                {
                    SfTerm.hed__Grading_Period_Sequence__cSpecified = true;
                    SfTerm.hed__Grading_Period_Sequence__c = (double)DbTerm.term_sequence_number;

                    SfTerm.Term_Sequence_Number__cSpecified = true;
                    SfTerm.Term_Sequence_Number__c = (double)DbTerm.term_sequence_number;
                }

                if (DbTerm.term_census_date != null)
                {
                    SfTerm.lc_term_census_date__cSpecified = true;
                    SfTerm.lc_term_census_date__c = GetUTCTime(DbTerm.term_census_date);
                }

                if (DbTerm.term_reporting_year != null)
                {
                    SfTerm.Term_Reporting_Year__cSpecified = true;
                    SfTerm.Term_Reporting_Year__c = (double)DbTerm.term_reporting_year;
                }

                if (DbTerm.term_year != null)
                {
                    SfTerm.lc_term_year__cSpecified = true;
                    SfTerm.lc_term_year__c = (double)DbTerm.term_year;
                }

                SfTerm.hed__Account__c = DbTerm.term_account_id ?? Settings.LcAccountId;

                if (DbTerm.term_prereg_start_date != null)
                {
                    SfTerm.lc_prereg_start_date__cSpecified = true;
                    SfTerm.lc_prereg_start_date__c = GetUTCTime(DbTerm.term_prereg_start_date);
                }

                if (DbTerm.term_prereg_end_date != null)
                {
                    SfTerm.lc_prereg_end_date__cSpecified = true;
                    SfTerm.lc_prereg_end_date__c = GetUTCTime(DbTerm.term_prereg_end_date);
                }

                if (DbTerm.term_reg_start_date != null)
                {
                    SfTerm.lc_reg_start_date__cSpecified = true;
                    SfTerm.lc_reg_start_date__c = GetUTCTime(DbTerm.term_reg_start_date);
                }

                if (DbTerm.term_reg_end_date != null)
                {
                    SfTerm.lc_reg_end_date__cSpecified = true;
                    SfTerm.lc_reg_end_date__c = GetUTCTime(DbTerm.term_reg_end_date);
                }

                if (DbTerm.term_start_date != null)
                {
                    SfTerm.hed__Start_Date__cSpecified = true;
                    SfTerm.hed__Start_Date__c = GetUTCTime(DbTerm.term_start_date);
                }

                if (DbTerm.term_end_date != null)
                {
                    SfTerm.hed__End_Date__cSpecified = true;
                    SfTerm.hed__End_Date__c = GetUTCTime(DbTerm.term_end_date);
                }

                if (DbTerm.term_drop_start_date != null)
                {
                    SfTerm.lc_drop_start_date__cSpecified = true;
                    SfTerm.lc_drop_start_date__c = GetUTCTime(DbTerm.term_drop_start_date);
                }

                if (DbTerm.term_drop_end_date != null)
                {
                    SfTerm.lc_drop_end_date__cSpecified = true;
                    SfTerm.lc_drop_end_date__c = GetUTCTime(DbTerm.term_drop_end_date);
                }

                if (DbTerm.term_drop_grad_reqd_date != null)
                {
                    SfTerm.lc_drop_grad_reqd_date__cSpecified = true;
                    SfTerm.lc_drop_grad_reqd_date__c = GetUTCTime(DbTerm.term_drop_grad_reqd_date);
                }

                SfTerm.lc_crmdb_last_sync__cSpecified = true;
                SfTerm.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfTerm;
        }

        private lc_activity_extender__c MapToSfActivityExtenders(crm_activity_extender DbActivityExtender)
        {
            lc_activity_extender__c SfActivityExtender = new lc_activity_extender__c();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbActivityExtender.activity_extender_id)
                        && DbActivityExtender.activity_extender_id != SfActivityExtender.Id)
                {
                        SfActivityExtender.Id = DbActivityExtender.activity_extender_id;
                }

                if (DbActivityExtender.activity_extender_guid != null
                    && SfActivityExtender.lc_activity_extender_guid__c != DbActivityExtender.activity_extender_guid.ToString())
                {
                    SfActivityExtender.lc_activity_extender_guid__c = DbActivityExtender.activity_extender_guid.ToString();
                }

                SfActivityExtender.IsDeleted = DbActivityExtender.activity_extender_deleted;

                SfActivityExtender.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
                SfActivityExtender.lc_crmdb_last_sync__cSpecified = true;
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfActivityExtender;
        }

        private Task MapToSfTasks(crm_tasks DbTask)
        {
            Task SfTask = new Task();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbTask.activity_id) 
                        && SfTask.Id != DbTask.activity_id)
                {
                    SfTask.Id = DbTask.activity_id;
                }

                if (DbTask.activity_guid != null
                    && SfTask.activity_guid__c != DbTask.activity_guid.ToString())
                {
                    SfTask.activity_guid__c = DbTask.activity_guid.ToString();
                }

                SfTask.Id = DbTask.activity_id;

                //sf_task.IsDeleted = db_task.task_deleted;
                //sf_task.LastModifiedById = db_task.task_modified_by ?? Settings.CrmAdmin;

                SfTask.Description = DbTask.task_description;

                //sf_task.RecurrenceStartDateOnlySpecified = true;
                //sf_task.RecurrenceStartDateOnly = GetUTCTime(db_task.task_recurrence_start_datetime);

                //sf_task.RecurrenceEndDateOnlySpecified = true;
                //sf_task.RecurrenceEndDateOnly = GetUTCTime(db_task.task_recurrence_end_datetime);

                SfTask.lc_crmdb_last_sync__cSpecified = true;
                SfTask.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfTask;
        }

        private Event MapToSfEvents(crm_events DbEvent)
        {
            Event SfEvent = new Event();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbEvent.activity_id)
                        && DbEvent.activity_id != SfEvent.Id) 
                {
                    SfEvent.Id = DbEvent.activity_id;
                }

                if (!string.IsNullOrWhiteSpace(DbEvent.event_account_id))
                {
                    SfEvent.AccountId = DbEvent.event_account_id;
                }


                if (!string.IsNullOrWhiteSpace(DbEvent.event_group_event_type))
                {
                    SfEvent.GroupEventType = DbEvent.event_group_event_type;
                }

                //if (DbEvent.event_created_datetime != null)
                //{
                //    SfEvent.CreatedDate = GetUTCTime(DbEvent.event_created_datetime);
                //    SfEvent.CreatedDateSpecified = true;
                //}

                if (DbEvent.activity_guid != null
                    && SfEvent.activity_guid__c != DbEvent.activity_guid.ToString()){
                    SfEvent.activity_guid__c = DbEvent.activity_guid.ToString();
                }

                SfEvent.IsDeleted = DbEvent.event_deleted;

                if (DbEvent.event_system_modstamp != null)
                {
                    SfEvent.SystemModstampSpecified = true;
                    SfEvent.SystemModstamp = GetUTCTime(DbEvent.event_system_modstamp);
                }

                if (!string.IsNullOrWhiteSpace(DbEvent.event_name_id))
                {
                    SfEvent.WhoId = DbEvent.event_name_id;
                }

                if (!string.IsNullOrWhiteSpace(DbEvent.event_related_to_id))
                {
                    SfEvent.WhatId = DbEvent.event_related_to_id;
                }

                if (!string.IsNullOrWhiteSpace(DbEvent.event_location))
                {
                    SfEvent.Location = DbEvent.event_location;
                }

                SfEvent.IsChild = DbEvent.event_is_child;

                SfEvent.IsDeleted = DbEvent.event_deleted;

                if (!string.IsNullOrWhiteSpace(DbEvent.event_description))
                {
                    SfEvent.Description = DbEvent.event_description;
                }

                if (!string.IsNullOrWhiteSpace(DbEvent.event_recurrence_activity_id))
                {
                    SfEvent.RecurrenceActivityId = DbEvent.event_recurrence_activity_id;
                }

                SfEvent.IsArchived = DbEvent.event_archived;

                if (!string.IsNullOrWhiteSpace(DbEvent.event_subject))
                {
                    SfEvent.Subject = DbEvent.event_subject;
                }

                if (!string.IsNullOrWhiteSpace(DbEvent.event_department))
                {
                    SfEvent.Department__c = DbEvent.event_department;
                }

                if (!string.IsNullOrWhiteSpace(DbEvent.event_engagement_type))
                {
                    SfEvent.lc_engagement_type__c = DbEvent.event_engagement_type;
                }

                if (!string.IsNullOrWhiteSpace(DbEvent.event_activity_extender_id))
                {
                    SfEvent.lc_activity_extender__c = DbEvent.event_activity_extender_id;
                }

                if (DbEvent.event_recurrence_start != null)
                {
                    SfEvent.RecurrenceStartDateTimeSpecified = true;
                    SfEvent.RecurrenceStartDateTime = GetUTCTime(DbEvent.event_recurrence_start);
                }

                if (DbEvent.event_recurrence_end != null)
                {
                    SfEvent.RecurrenceEndDateOnlySpecified = true;
                    SfEvent.RecurrenceEndDateOnly = GetUTCTime(DbEvent.event_recurrence_end);
                }

                //SfEvent.LastModifiedById = DbEvent.event_modified_by ?? Settings.CrmAdmin;
                //if (DbEvent.event_modified_datetime != null)
                //{
                //    SfEvent.LastModifiedDateSpecified = true;
                //    SfEvent.LastModifiedDate = GetUTCTime(DbEvent.event_modified_datetime);
                //}

                SfEvent.lc_crmdb_last_sync__cSpecified = true;
                SfEvent.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfEvent;
        }

        private lc_email_campaign__c MapToSfEmailCampaign(crm_email_campaigns DbEmailCampaign)
        {
            lc_email_campaign__c SfEmailCampaign = new lc_email_campaign__c();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbEmailCampaign.email_campaign_id)
                        && DbEmailCampaign.email_campaign_id != SfEmailCampaign.Id) 
                {
                    SfEmailCampaign.Id = DbEmailCampaign.email_campaign_id;
                }              

                if (DbEmailCampaign.email_campaign_guid != null 
                    && !string.IsNullOrWhiteSpace(DbEmailCampaign.email_campaign_guid.ToString()))
                {
                    SfEmailCampaign.lc_email_campaign_guid__c = DbEmailCampaign.email_campaign_guid.ToString();
                }

                SfEmailCampaign.lc_crmdb_last_sync__cSpecified = true;
                SfEmailCampaign.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);

            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfEmailCampaign;
        }

        private lc_inquiry__c MapToSfInquiries(crm_inquiries DbInquiry)
        {
            lc_inquiry__c SfInquiry = new lc_inquiry__c();

            try
            {
                if (DbInquiry != null) {
                    if (!string.IsNullOrWhiteSpace(DbInquiry.inquiry_id)
                            && DbInquiry.inquiry_id != SfInquiry.Id)
                    {
                        SfInquiry.Id = DbInquiry.inquiry_id;
                    }

                    if (!string.IsNullOrWhiteSpace(DbInquiry.inquiry_guid.ToString()))
                    {
                        SfInquiry.lc_inquiry_guid__c = DbInquiry.inquiry_guid.ToString();
                    }

                    if (!string.IsNullOrWhiteSpace(DbInquiry.inq_contact_id))
                    {
                        SfInquiry.lc_contact__c = DbInquiry.inq_contact_id;
                    }

                    SfInquiry.lc_crmdb_last_sync__cSpecified = true;
                    SfInquiry.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfInquiry;
        }

        private EmailTemplate MapToSfEmailTemplate(crm_email_templates DbEmailTemplate)
        {
            EmailTemplate SfEmailTemplate = new EmailTemplate();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbEmailTemplate.email_template_id)
                    && DbEmailTemplate.email_template_id != SfEmailTemplate.Id)
                {
                    SfEmailTemplate.Id = DbEmailTemplate.email_template_id;
                }

                //SfEmailTemplate.LastModifiedDateSpecified = true;
                //SfEmailTemplate.LastModifiedDate = GetUTCTime(DateTime.Now);

            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfEmailTemplate;
        }

        private lc_dynamic_content__c MapToSfDynamicContent(crm_dynamic_content DbDynamicContent)
        {
            lc_dynamic_content__c SfDynamicContent = new lc_dynamic_content__c();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbDynamicContent.crm_dynamic_content_id)
                    && DbDynamicContent.crm_dynamic_content_id != SfDynamicContent.Id)
                {
                    SfDynamicContent.Id = DbDynamicContent.crm_dynamic_content_id;
                }

                if (DbDynamicContent.crm_dynamic_content_guid != null && SfDynamicContent.lc_dynamic_content_guid__c == null)
                {
                    SfDynamicContent.lc_dynamic_content_guid__c = DbDynamicContent.crm_dynamic_content_guid.ToString();
                }

                SfDynamicContent.lc_dynamic_content_html__c = DbDynamicContent.crm_dynamic_content_html;

                SfDynamicContent.lc_crmdb_last_sync__cSpecified = true;
                SfDynamicContent.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);

            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfDynamicContent;
        }

        private lc_inquiry_program__c MapToSfInquiryProgram(crm_inquiry_programs DbInquiryProgram)
        {
            lc_inquiry_program__c SfInquiryProgram = new lc_inquiry_program__c();
            List<string> FieldsToNull = new List<string>();

            try
            {
                if (DbInquiryProgram != null) {

                    if (!string.IsNullOrWhiteSpace(DbInquiryProgram.crm_inq_prog_id)
                            && DbInquiryProgram.crm_inq_prog_id != SfInquiryProgram.Id)
                    {
                        SfInquiryProgram.Id = DbInquiryProgram.crm_inq_prog_id;
                    }

                    if (DbInquiryProgram.crm_inq_prog_guid != null)
                    {
                        SfInquiryProgram.lc_inquiry_program_guid__c = DbInquiryProgram.crm_inq_prog_guid.ToString();
                    }

                    //if (!string.IsNullOrWhiteSpace(DbInquiryProgram.crm_inq_prog_number)) {
                    //    SfInquiryProgram.Name = DbInquiryProgram.crm_inq_prog_number;
                    //}
                    //else
                    //{
                    //    FieldsToNull.Add(GetPropertyName(() => SfInquiryProgram.Name));
                    //}

                    if (!string.IsNullOrWhiteSpace(DbInquiryProgram.crm_inq_prog_inquiry_id))
                    {
                        SfInquiryProgram.lc_inquiry__c = DbInquiryProgram.crm_inq_prog_inquiry_id;
                    }
                    else
                    {
                        FieldsToNull.Add(GetPropertyName(() => SfInquiryProgram.lc_inquiry__c));
                    }

                    if (!string.IsNullOrWhiteSpace(DbInquiryProgram.crm_inq_prog_program_id))
                    {
                        SfInquiryProgram.lc_program__c = DbInquiryProgram.crm_inq_prog_program_id;
                    }
                    else
                    {
                        FieldsToNull.Add(GetPropertyName(() => SfInquiryProgram.lc_program__c));
                    }

                    if (DbInquiryProgram.crm_inq_prog_ack_sent != null)
                    {
                        SfInquiryProgram.lc_inquiry_program_ack_sent__cSpecified = true;
                        SfInquiryProgram.lc_inquiry_program_ack_sent__c = GetUTCTime(DbInquiryProgram.crm_inq_prog_ack_sent);
                    }
                    else
                    {
                        FieldsToNull.Add(GetPropertyName(() => SfInquiryProgram.lc_inquiry_program_ack_sent__c));
                    }

                    SfInquiryProgram.fieldsToNull = FieldsToNull.ToArray();

                    SfInquiryProgram.lc_crmdb_last_sync__cSpecified = true;
                    SfInquiryProgram.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfInquiryProgram;
        }

        private Report MapToSfReports(crm_reports DbReport)
        {
            Report SfReport = new Report();

            try
            {
                if (DbReport != null) {
                    if (!string.IsNullOrEmpty(DbReport.crm_report_id) 
                           && SfReport.Id != DbReport.crm_report_id)
                    {
                        SfReport.Id = DbReport.crm_report_id;
                    }
                }

                //SfReport.LastModifiedDateSpecified = true;
                //SfReport.LastModifiedDate = GetUTCTime(DateTime.Now);

                // custom field is not available on this entity
                //SfReport.lc_crmdb_last_sync__cSpecified = true;
                //SfReport.lc_crmdb_last_sync__c = DateTime.Now;

            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfReport;
        }

        private Account MapToSfAcademicPrograms(crm_accounts DbAcadmicProgram)
        {
            Account SfAcademicProgram = new Account();
            List<string> FieldsToNull = new List<string>();

            try
            {
                if (!string.IsNullOrWhiteSpace(DbAcadmicProgram.account_id)
                        && SfAcademicProgram.Id != DbAcadmicProgram.account_id)
                {
                    SfAcademicProgram.Id = DbAcadmicProgram.account_id;
                }

                if (DbAcadmicProgram != null && SfAcademicProgram.lc_account_guid__c != DbAcadmicProgram.account_guid.ToString())
                {
                    SfAcademicProgram.lc_account_guid__c = DbAcadmicProgram.account_guid.ToString();
                }

                if (!string.IsNullOrWhiteSpace(DbAcadmicProgram.account_name))
                {
                    SfAcademicProgram.Name = DbAcadmicProgram.account_name;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfAcademicProgram.Name));
                }

                if (!string.IsNullOrWhiteSpace(DbAcadmicProgram.account_type))
                {
                    SfAcademicProgram.lc_account_type__c = DbAcadmicProgram.account_type;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfAcademicProgram.lc_account_type__c));
                }

                if (!string.IsNullOrWhiteSpace(AccountRecordTypes.AcademicProgram))
                {
                    SfAcademicProgram.RecordTypeId = AccountRecordTypes.AcademicProgram;
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfAcademicProgram.RecordTypeId));
                }

                if (!string.IsNullOrWhiteSpace(DbAcadmicProgram.account_number))
                {
                    SfAcademicProgram.AccountNumber = DbAcadmicProgram.account_number.Replace("_", ".");
                }
                else
                {
                    FieldsToNull.Add(GetPropertyName(() => SfAcademicProgram.AccountNumber));
                }

                //SfAcademicProgram.CreatedById = Settings.CrmAdmin;
                //SfAcademicProgram.CreatedDate = DbAcadmicProgram.account_created_date;

                SfAcademicProgram.lc_account_active__cSpecified = true;
                SfAcademicProgram.lc_account_active__c = DbAcadmicProgram.account_active;

                SfAcademicProgram.fieldsToNull = FieldsToNull.ToArray();

                SfAcademicProgram.lc_crmdb_last_sync__cSpecified = true;
                SfAcademicProgram.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfAcademicProgram;
        }

        // RoleValues
        private lc_role_values__c MapToSfRoleValues(crm_role_values DbRoleValue)
        {
            lc_role_values__c SfRoleValues = new lc_role_values__c();

            try
            {
                if (SfRoleValues.Id != DbRoleValue.crm_role_value_id)
                {
                    SfRoleValues.Id = DbRoleValue.crm_role_value_id;
                }

                if (SfRoleValues.lc_role_value_guid__c != DbRoleValue.crm_role_value_guid.ToString())
                {
                    SfRoleValues.lc_role_value_guid__c = DbRoleValue.crm_role_value_guid.ToString();
                }

                SfRoleValues.lc_crmdb_last_sync__cSpecified = true;
                SfRoleValues.lc_crmdb_last_sync__c = GetUTCTime(DateTime.Now);
            }
            catch (Exception ex)
            {
                RecordError(GetCurrentMethod(), ex.ToString());
            }

            return SfRoleValues;
        }

        // Program Enrollments

        // International


    }
}
