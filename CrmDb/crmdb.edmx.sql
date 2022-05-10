
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/31/2020 19:27:09
-- Generated from EDMX file: C:\Projects\CrmIntSln\CrmDb\crmdb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [crmdb_prod];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[crm_accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_accounts];
GO
IF OBJECT_ID(N'[dbo].[crm_activities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_activities];
GO
IF OBJECT_ID(N'[dbo].[crm_activity_extender]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_activity_extender];
GO
IF OBJECT_ID(N'[dbo].[crm_affiliations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_affiliations];
GO
IF OBJECT_ID(N'[dbo].[crm_applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_applications];
GO
IF OBJECT_ID(N'[dbo].[crm_assets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_assets];
GO
IF OBJECT_ID(N'[dbo].[crm_campaign_members]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_campaign_members];
GO
IF OBJECT_ID(N'[dbo].[crm_campaigns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_campaigns];
GO
IF OBJECT_ID(N'[dbo].[crm_cases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_cases];
GO
IF OBJECT_ID(N'[dbo].[crm_contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_contacts];
GO
IF OBJECT_ID(N'[dbo].[crm_cookie_history]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_cookie_history];
GO
IF OBJECT_ID(N'[dbo].[crm_course_connections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_course_connections];
GO
IF OBJECT_ID(N'[dbo].[crm_course_offerings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_course_offerings];
GO
IF OBJECT_ID(N'[dbo].[crm_courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_courses];
GO
IF OBJECT_ID(N'[dbo].[crm_dynamic_content]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_dynamic_content];
GO
IF OBJECT_ID(N'[dbo].[crm_email_broadcasts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_email_broadcasts];
GO
IF OBJECT_ID(N'[dbo].[crm_email_campaigns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_email_campaigns];
GO
IF OBJECT_ID(N'[dbo].[crm_email_messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_email_messages];
GO
IF OBJECT_ID(N'[dbo].[crm_email_templates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_email_templates];
GO
IF OBJECT_ID(N'[dbo].[crm_event_registrations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_event_registrations];
GO
IF OBJECT_ID(N'[dbo].[crm_events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_events];
GO
IF OBJECT_ID(N'[dbo].[crm_form_submissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_form_submissions];
GO
IF OBJECT_ID(N'[dbo].[crm_inquiries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_inquiries];
GO
IF OBJECT_ID(N'[dbo].[crm_inquiry_programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_inquiry_programs];
GO
IF OBJECT_ID(N'[dbo].[crm_leads]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_leads];
GO
IF OBJECT_ID(N'[dbo].[crm_po_course_codes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_po_course_codes];
GO
IF OBJECT_ID(N'[dbo].[crm_program_enrollments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_program_enrollments];
GO
IF OBJECT_ID(N'[dbo].[crm_reports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_reports];
GO
IF OBJECT_ID(N'[dbo].[crm_role_values]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_role_values];
GO
IF OBJECT_ID(N'[dbo].[crm_scanner_registrations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_scanner_registrations];
GO
IF OBJECT_ID(N'[dbo].[crm_settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_settings];
GO
IF OBJECT_ID(N'[dbo].[crm_tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_tasks];
GO
IF OBJECT_ID(N'[dbo].[crm_terms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crm_terms];
GO
IF OBJECT_ID(N'[dbo].[dynamic_content]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dynamic_content];
GO
IF OBJECT_ID(N'[dbo].[lcc2_barcodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[lcc2_barcodes];
GO
IF OBJECT_ID(N'[dbo].[merge_fields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[merge_fields];
GO
IF OBJECT_ID(N'[dbo].[soql_queries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[soql_queries];
GO
IF OBJECT_ID(N'[dbo].[transaction_errors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[transaction_errors];
GO
IF OBJECT_ID(N'[dbo].[transaction_loads]', 'U') IS NOT NULL
    DROP TABLE [dbo].[transaction_loads];
GO
IF OBJECT_ID(N'[dbo].[transaction_logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[transaction_logs];
GO
IF OBJECT_ID(N'[dbo].[wt_addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_addresses];
GO
IF OBJECT_ID(N'[dbo].[wt_applicants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_applicants];
GO
IF OBJECT_ID(N'[dbo].[wt_application_statuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_application_statuses];
GO
IF OBJECT_ID(N'[dbo].[wt_applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_applications];
GO
IF OBJECT_ID(N'[dbo].[wt_core_students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_core_students];
GO
IF OBJECT_ID(N'[dbo].[wt_course_departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_course_departments];
GO
IF OBJECT_ID(N'[dbo].[wt_course_sections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_course_sections];
GO
IF OBJECT_ID(N'[dbo].[wt_courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_courses];
GO
IF OBJECT_ID(N'[dbo].[wt_departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_departments];
GO
IF OBJECT_ID(N'[dbo].[wt_divisions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_divisions];
GO
IF OBJECT_ID(N'[dbo].[wt_institutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_institutions];
GO
IF OBJECT_ID(N'[dbo].[wt_off_email_restore]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_off_email_restore];
GO
IF OBJECT_ID(N'[dbo].[wt_program_enrollments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_program_enrollments];
GO
IF OBJECT_ID(N'[dbo].[wt_programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_programs];
GO
IF OBJECT_ID(N'[dbo].[wt_student_course_sections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_student_course_sections];
GO
IF OBJECT_ID(N'[dbo].[wt_student_courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_student_courses];
GO
IF OBJECT_ID(N'[dbo].[wt_student_credentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_student_credentials];
GO
IF OBJECT_ID(N'[dbo].[wt_student_programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_student_programs];
GO
IF OBJECT_ID(N'[dbo].[wt_student_terms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_student_terms];
GO
IF OBJECT_ID(N'[dbo].[wt_terms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[wt_terms];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'crm_accounts'
CREATE TABLE [dbo].[crm_accounts] (
    [account_guid] uniqueidentifier  NOT NULL,
    [account_id] varchar(18)  NULL,
    [account_deleted] bit  NOT NULL,
    [account_master_record_id] varchar(18)  NULL,
    [account_name] varchar(255)  NULL,
    [account_type] varchar(40)  NULL,
    [account_record_type_id] varchar(18)  NULL,
    [lc_account_active] bit  NOT NULL,
    [account_parent_account_id] varchar(18)  NULL,
    [account_billing_address] varchar(255)  NULL,
    [account_billing_street] varchar(255)  NULL,
    [account_billing_city] varchar(40)  NULL,
    [account_billing_province] varchar(80)  NULL,
    [account_billing_postalcode] varchar(20)  NULL,
    [account_billing_country] varchar(80)  NULL,
    [account_shipping_address] varchar(255)  NULL,
    [account_shipping_street] varchar(255)  NULL,
    [account_shipping_city] varchar(40)  NULL,
    [account_shipping_province] varchar(80)  NULL,
    [account_shipping_postalcode] varchar(20)  NULL,
    [account_shipping_country] varchar(80)  NULL,
    [account_phone] varchar(40)  NULL,
    [account_fax] varchar(40)  NULL,
    [account_number] varchar(40)  NULL,
    [account_website] varchar(255)  NULL,
    [account_industry] varchar(40)  NULL,
    [account_annual_revenue] decimal(18,2)  NULL,
    [account_owner_id] varchar(18)  NULL,
    [account_created_date] datetime  NULL,
    [account_created_by] varchar(18)  NULL,
    [account_modifed_datetime] datetime  NULL,
    [account_modified_by] varchar(18)  NULL,
    [account_system_modstamp] datetime  NULL,
    [account_last_activity_date] datetime  NULL,
    [account_last_viewed_date] datetime  NULL,
    [account_last_referenced_date] datetime  NULL,
    [account_source] varchar(40)  NULL,
    [account_one2one] varchar(18)  NULL,
    [account_current_address] varchar(18)  NULL,
    [account_active] bit  NOT NULL,
    [account_department_type] varchar(1)  NULL,
    [account_colleague_id] varchar(20)  NULL,
    [account_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [account_show_web] bit  NULL,
    [account_name_alias] varchar(80)  NULL
);
GO

-- Creating table 'crm_activities'
CREATE TABLE [dbo].[crm_activities] (
    [activity_guid] uniqueidentifier  NULL,
    [activity_id] varchar(18)  NOT NULL,
    [activity_legacy_id] varchar(18)  NULL,
    [activity_name_id] varchar(18)  NULL,
    [activity_legacy_name_id] varchar(18)  NULL,
    [activity_related_to] varchar(18)  NULL,
    [activity_legacy_rel_to_id] varchar(18)  NULL,
    [activity_subject] varchar(255)  NULL,
    [activity_due_date] datetime  NULL,
    [activity_status] varchar(40)  NULL,
    [activity_priority] varchar(40)  NULL,
    [activity_priority_high] bit  NOT NULL,
    [activity_assigned_to] varchar(18)  NULL,
    [activity_legacy_assigned_to] varchar(18)  NULL,
    [activity_description] varchar(max)  NULL,
    [activity_deleted] bit  NOT NULL,
    [activity_school_id] varchar(18)  NULL,
    [activity_legacy_school] varchar(18)  NULL,
    [activity_closed] bit  NOT NULL,
    [activity_created_date] datetime  NULL,
    [activity_created_by] varchar(18)  NULL,
    [activity_mod_date] varchar(18)  NULL,
    [activity_mod_by] varchar(18)  NULL,
    [activity_sys_timestamp] datetime  NULL,
    [activity_archived] bit  NOT NULL,
    [activity_call_duration] int  NULL,
    [activity_call_type] varchar(40)  NULL,
    [activity_call_result] varchar(255)  NULL,
    [activity_call_obj_id] varchar(255)  NULL,
    [activity_reminder_date] datetime  NULL,
    [activity_reminder_set] bit  NOT NULL,
    [activity_recurrence_id] varchar(18)  NULL,
    [activity_create_recur] bit  NOT NULL,
    [activity_recurrence_start] datetime  NULL,
    [activity_recurrence_end] datetime  NULL,
    [activity_recurrence_time_zone] varchar(40)  NULL,
    [activity_recurrence_type] varchar(40)  NULL,
    [activity_recurrence_int] int  NULL,
    [activity_recurrence_day_of_week] int  NULL,
    [activity_recurrence_day_of_month] int  NULL,
    [activity_recurrence_instance] varchar(40)  NULL,
    [activity_recurrence_month_of_year] varchar(40)  NULL,
    [activity_repeat_task] varchar(40)  NULL,
    [activity_contactid_cl] varchar(20)  NULL,
    [activity_oldwhatid_cl] varchar(20)  NULL,
    [activity_testtype_cl] varchar(20)  NULL,
    [activity_cancelled] bit  NOT NULL,
    [activity_copy_events] bit  NOT NULL,
    [activity_org_event_id] varchar(18)  NULL,
    [activity_prospect_score] float  NULL,
    [activity_data_quality_desc] varchar(13)  NULL,
    [activity_data_quality_score] float  NULL,
    [activity_pri_adv_rec] varchar(18)  NULL
);
GO

-- Creating table 'crm_activity_extender'
CREATE TABLE [dbo].[crm_activity_extender] (
    [activity_extender_guid] uniqueidentifier  NOT NULL,
    [activity_extender_id] varchar(18)  NULL,
    [activity_extender_deleted] bit  NULL,
    [activity_extender_created_by] varchar(18)  NULL,
    [activity_extender_created_datetime] datetime  NULL,
    [activity_extender_modified_by] varchar(18)  NULL,
    [activity_extender_modified_datetime] datetime  NULL,
    [activity_extender_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_affiliations'
CREATE TABLE [dbo].[crm_affiliations] (
    [affiliation_guid] uniqueidentifier  NOT NULL,
    [affiliation_id] varchar(18)  NULL,
    [affiliation_owner_id] varchar(18)  NULL,
    [affiliation_deleted] bit  NOT NULL,
    [affiliation_key] varchar(18)  NULL,
    [affiliation_created_by] varchar(18)  NULL,
    [affiliation_system_modstamp] datetime  NULL,
    [affiliation_last_modified_by] varchar(18)  NULL,
    [affiliation_last_viewed_datetime] datetime  NULL,
    [affiliation_last_referenced_datetime] datetime  NULL,
    [affiliation_organization_id] varchar(18)  NULL,
    [lc_student_program_id] varchar(25)  NULL,
    [affiliation_type] varchar(1300)  NULL,
    [affiliation_description] varchar(max)  NULL,
    [affiliation_contact_id] varchar(18)  NULL,
    [affiliation_created_datetime] datetime  NULL,
    [affiliation_last_modified_datetime] datetime  NULL,
    [affiliation_primary] bit  NOT NULL,
    [affiliation_role] varchar(255)  NULL,
    [affiliation_status] varchar(255)  NULL,
    [affiliation_start_date] datetime  NULL,
    [affiliation_end_date] datetime  NULL,
    [affiliation_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [affiliation_guid_mismatch] bit  NOT NULL,
    [lc_temp_use_flag] bit  NULL,
    [affiliation_program_status] varchar(1)  NULL,
    [affiliation_program_status_date] datetime  NULL,
    [affiliation_record_type_id] varchar(18)  NULL
);
GO

-- Creating table 'crm_applications'
CREATE TABLE [dbo].[crm_applications] (
    [application_guid] uniqueidentifier  NOT NULL,
    [crm_application_id] varchar(18)  NULL,
    [crm_appl_number] varchar(20)  NULL,
    [crm_contact_id] varchar(18)  NULL,
    [sis_student_id] varchar(10)  NULL,
    [sis_application_id] varchar(6)  NULL,
    [appl_stage] varchar(40)  NULL,
    [application_status] varchar(40)  NULL,
    [appl_status_date] datetime  NULL,
    [intended_start_term] varchar(18)  NULL,
    [intended_start_year] varchar(4)  NULL,
    [intended_student_load] varchar(4)  NULL,
    [appl_location] varchar(4)  NULL,
    [alt_status_date] datetime  NULL,
    [app_status_date] datetime  NULL,
    [con_status_date] datetime  NULL,
    [dtc_status_date] datetime  NULL,
    [dac_status_date] datetime  NULL,
    [fi_status_date] datetime  NULL,
    [fw_status_date] datetime  NULL,
    [ms_status_date] datetime  NULL,
    [ntq_status_date] datetime  NULL,
    [ofc_status_date] datetime  NULL,
    [offer_due_date] datetime  NULL,
    [ofi_status_date] datetime  NULL,
    [par_status_date] datetime  NULL,
    [ppr_status_date] datetime  NULL,
    [pas_status_date] datetime  NULL,
    [w_status_date] datetime  NULL,
    [pr_status_date] datetime  NULL,
    [sc_status_date] datetime  NULL,
    [unc_status_date] datetime  NULL,
    [wtl_status_date] datetime  NULL,
    [wap_status_date] datetime  NULL,
    [appl_created_by] varchar(18)  NULL,
    [appl_created_date] datetime  NULL,
    [appl_modified_by] varchar(18)  NULL,
    [appl_modfied_date] datetime  NULL,
    [appl_affiliation] varchar(18)  NULL,
    [crm_program_id] varchar(18)  NULL,
    [appl_owner_id] varchar(18)  NULL,
    [application_deleted] bit  NOT NULL,
    [appl_admit_status] varchar(5)  NULL,
    [appl_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_assets'
CREATE TABLE [dbo].[crm_assets] (
    [asset_guid] uniqueidentifier  NOT NULL,
    [asset_id] varchar(18)  NULL
);
GO

-- Creating table 'crm_campaign_members'
CREATE TABLE [dbo].[crm_campaign_members] (
    [campaign_member_guid] uniqueidentifier  NOT NULL,
    [campaign_member_id] varchar(18)  NULL,
    [campaign_member_deleted] bit  NOT NULL,
    [campaign_id] varchar(18)  NULL,
    [campaign_member_lead_id] varchar(18)  NULL,
    [campaign_member_contact_id] varchar(18)  NULL,
    [campaign_member_status] varchar(40)  NULL,
    [campaign_member_responded] bit  NOT NULL,
    [campaign_member_created_datetime] datetime  NULL,
    [campaign_member_created_by] varchar(18)  NULL,
    [campaign_member_modified_datetime] datetime  NULL,
    [campaign_member_modified_by] varchar(18)  NULL,
    [campaign_member_system_modstamp] datetime  NULL,
    [campaign_member_first_responded_date] datetime  NULL
);
GO

-- Creating table 'crm_campaigns'
CREATE TABLE [dbo].[crm_campaigns] (
    [campaign_guid] uniqueidentifier  NOT NULL,
    [campaign_id] varchar(18)  NULL,
    [campaign_deleted] bit  NOT NULL,
    [campaign_name] varchar(80)  NULL,
    [campaign_parent_id] varchar(18)  NULL,
    [campaign_type] varchar(40)  NULL,
    [campaign_status] varchar(40)  NULL,
    [campaign_start_date] datetime  NULL,
    [campaign_end_date] datetime  NULL,
    [campaign_num_sent] float  NULL,
    [campaign_active] bit  NOT NULL,
    [campaign_description] varchar(max)  NULL,
    [campaign_leads_count] int  NULL,
    [campaign_leads_converted] int  NULL,
    [campaign_contacts_count] int  NULL,
    [campaign_responses_count] int  NULL,
    [campaign_opportunities_count] int  NULL,
    [campaign_owner_id] varchar(18)  NULL,
    [campaign_created_datetime] datetime  NULL,
    [campaign_created_by] varchar(18)  NULL,
    [campaign_modified_datetime] datetime  NULL,
    [campaign_modifed_by] varchar(18)  NULL,
    [campaign_system_modstamp] datetime  NULL,
    [campaign_last_activity_datetime] datetime  NULL,
    [campaign_last_viewed_datetime] datetime  NULL,
    [campaign_last_referenced_datetime] datetime  NULL,
    [campaign_record_type_id] varchar(18)  NULL,
    [campaign_mark_delete] bit  NOT NULL,
    [campaign_allow_repeat_broadcasts] bit  NULL
);
GO

-- Creating table 'crm_cases'
CREATE TABLE [dbo].[crm_cases] (
    [case_guid] uniqueidentifier  NOT NULL,
    [case_id] varchar(18)  NULL
);
GO

-- Creating table 'crm_contacts'
CREATE TABLE [dbo].[crm_contacts] (
    [contact_guid] uniqueidentifier  NOT NULL,
    [contact_id] varchar(18)  NULL,
    [contact_legacy_id] varchar(18)  NULL,
    [contact_colleague_id] varchar(7)  NULL,
    [contact_deleted] bit  NOT NULL,
    [contact_master_record_id] varchar(18)  NULL,
    [contact_account_id] varchar(18)  NULL,
    [contact_first_name] varchar(40)  NULL,
    [contact_middle_name] varchar(80)  NULL,
    [contact_last_name] varchar(80)  NULL,
    [contact_full_name] varchar(121)  NULL,
    [contact_other_street] varchar(255)  NULL,
    [contact_other_city] varchar(40)  NULL,
    [contact_other_province] varchar(80)  NULL,
    [contact_other_postal_code] varchar(20)  NULL,
    [contact_other_country] varchar(80)  NULL,
    [contact_mailing_street] varchar(255)  NULL,
    [contact_mailing_city] varchar(40)  NULL,
    [contact_mailing_province] varchar(80)  NULL,
    [contact_mailing_postalcode] varchar(20)  NULL,
    [contact_mailing_country] varchar(80)  NULL,
    [contact_business_phone] varchar(40)  NULL,
    [contact_business_fax] varchar(40)  NULL,
    [contact_mobile_phone] varchar(40)  NULL,
    [contact_home_phone] varchar(40)  NULL,
    [contact_other_phone] varchar(40)  NULL,
    [contact_asst_phone] varchar(40)  NULL,
    [contact_reports_to] varchar(18)  NULL,
    [contact_email] varchar(80)  NULL,
    [contact_title] varchar(128)  NULL,
    [contact_department] varchar(80)  NULL,
    [contact_assistant_name] varchar(40)  NULL,
    [contact_lead_source] varchar(40)  NULL,
    [contact_birthdate] datetime  NULL,
    [contact_description] varchar(max)  NULL,
    [contact_owner_id] varchar(18)  NULL,
    [contact_created_datetime] datetime  NULL,
    [contact_created_by] varchar(18)  NULL,
    [contact_last_modified_datetime] datetime  NULL,
    [contact_last_modified_by] varchar(18)  NULL,
    [contact_last_activity_date] datetime  NULL,
    [contact_last_stay_in_touch_request_date] datetime  NULL,
    [contact_last_stay_in_touch_save_date] datetime  NULL,
    [contact_last_viewed_datetime] datetime  NULL,
    [contact_last_referenced_datetime] datetime  NULL,
    [contact_email_bounced_reason] varchar(255)  NULL,
    [contact_email_bounced_date] datetime  NULL,
    [contact_is_email_bounced] bit  NOT NULL,
    [contact_primary_academic_program] varchar(18)  NULL,
    [contact_primary_department] varchar(18)  NULL,
    [contact_primary_educational_institution] varchar(18)  NULL,
    [contact_primary_sports_organization] varchar(18)  NULL,
    [contact_alternate_email] varchar(80)  NULL,
    [contact_citizenship] varchar(255)  NULL,
    [contact_country_of_origin] varchar(255)  NULL,
    [contact_current_address] varchar(18)  NULL,
    [contact_deceased] bit  NOT NULL,
    [contact_do_not_contact] bit  NOT NULL,
    [contact_ethnicity] varchar(255)  NULL,
    [contact_financial_aid_applicant] bit  NOT NULL,
    [contact_gender] varchar(255)  NULL,
    [contact_preferred_phone] varchar(50)  NULL,
    [contact_preferred_email] varchar(255)  NULL,
    [contact_primary_address_type] varchar(255)  NULL,
    [contact_primary_household] varchar(18)  NULL,
    [contact_primary_business_organization] varchar(18)  NULL,
    [contact_religion] varchar(255)  NULL,
    [contact_secondary_address_type] varchar(255)  NULL,
    [contact_college_email] varchar(255)  NULL,
    [contact_work_email] varchar(255)  NULL,
    [contact_work_phone] varchar(40)  NULL,
    [contact_work_address] varchar(255)  NULL,
    [contact_do_not_auto_update] bit  NOT NULL,
    [contact_primary_student_organization] varchar(18)  NULL,
    [contact_potential_duplicate] bit  NOT NULL,
    [contact_invalid_email_flag] bit  NOT NULL,
    [contact_id_card_barcode] varchar(14)  NULL,
    [contact_type] varchar(max)  NULL,
    [contact_international] bit  NOT NULL,
    [contact_stenso19flreg_flag] bit  NOT NULL,
    [contact_mark_delete] bit  NOT NULL,
    [contact_barcode] varbinary(7)  NULL,
    [contact_barcode_uploaded] datetime  NULL,
    [contact_qrcode_uploaded] datetime  NULL,
    [contact_email_opt_out] bit  NOT NULL,
    [contact_sis_modified_datetime] datetime  NULL,
    [contact_alumni_type] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [contact_applicant_type] bit  NULL,
    [contact_prospect_type] bit  NULL,
    [contact_student_type] bit  NULL,
    [contact_id_barcode_uploaded] datetime  NULL,
    [contact_record_type_id] varchar(18)  NULL
);
GO

-- Creating table 'crm_course_connections'
CREATE TABLE [dbo].[crm_course_connections] (
    [course_connection_guid] uniqueidentifier  NOT NULL,
    [course_connection_id] varchar(18)  NULL,
    [course_connection_name] varchar(80)  NULL,
    [course_connection_contact_id] varchar(18)  NULL,
    [course_connection_program_id] varchar(18)  NULL,
    [course_connection_program_enrollment_id] varchar(18)  NULL,
    [course_offering_id] varchar(18)  NULL,
    [course_connection_affiliation_id] varchar(18)  NULL,
    [course_credits_attempted] decimal(3,3)  NULL,
    [course_credits_earned] decimal(3,3)  NULL,
    [course_grade] decimal(4,2)  NULL,
    [course_connection_primary] bit  NOT NULL,
    [course_connection_record_type] varchar(18)  NULL,
    [course_connection_status] varchar(40)  NULL,
    [course_connection_created_by] varchar(18)  NULL,
    [course_connection_created_datetime] datetime  NULL,
    [course_connection_modified_by] varchar(18)  NULL,
    [course_connection_modified_datetime] datetime  NULL,
    [course_connection_deleted] bit  NOT NULL,
    [contact_last_modified_datetime] datetime  NULL,
    [term_modified_datetime] datetime  NULL,
    [course_offering_modified_datetime] datetime  NULL,
    [sis_stu_course_mod_date] datetime  NULL,
    [course_connection_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_course_offerings'
CREATE TABLE [dbo].[crm_course_offerings] (
    [course_offering_guid] uniqueidentifier  NOT NULL,
    [course_offering_id] varchar(18)  NULL,
    [sis_course_section_id] varchar(19)  NULL,
    [course_section_code] varchar(30)  NULL,
    [course_offering_capacity] decimal(18,0)  NULL,
    [course_offering_course] varchar(18)  NULL,
    [course_offering_name] varchar(80)  NULL,
    [course_offering_start_datetime] datetime  NULL,
    [course_offering_end_datetime] datetime  NULL,
    [course_offering_facility] varchar(18)  NULL,
    [course_offering_primary_faculty] varchar(18)  NULL,
    [course_offering_modified_by] varchar(18)  NULL,
    [course_offering_modified_datetime] datetime  NULL,
    [course_offering_created_datetime] datetime  NULL,
    [course_offering_created_by] varchar(18)  NULL,
    [course_offering_term] varchar(18)  NULL,
    [course_offering_time_block] varchar(18)  NULL,
    [course_offering_deleted] bit  NOT NULL,
    [course_offering_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_courses'
CREATE TABLE [dbo].[crm_courses] (
    [course_guid] uniqueidentifier  NOT NULL,
    [crm_course_id] varchar(18)  NULL,
    [course_number] varchar(255)  NULL,
    [course_name] varchar(80)  NULL,
    [sis_course_id] varchar(10)  NULL,
    [course_created_by] varchar(18)  NULL,
    [course_created_datetime] datetime  NULL,
    [course_modified_by] varchar(18)  NULL,
    [course_modified_datetime] datetime  NULL,
    [course_credit_hours] decimal(3,3)  NULL,
    [course_department] varchar(18)  NULL,
    [course_description] varchar(255)  NULL,
    [course_desc_extended] varchar(max)  NULL,
    [course_deleted] bit  NOT NULL,
    [course_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_email_broadcasts'
CREATE TABLE [dbo].[crm_email_broadcasts] (
    [email_broadcast_guid] uniqueidentifier  NOT NULL,
    [email_broadcast_id] varchar(18)  NULL,
    [email_campaign_id] varchar(18)  NULL,
    [email_broadcast_name] varchar(80)  NULL,
    [email_broadcast_status] varchar(40)  NULL,
    [email_broadcast_messages_sent] float  NULL,
    [email_broadcast_email_campaign] varchar(18)  NULL,
    [email_broadcast_deleted] bit  NOT NULL,
    [email_broadcast_created_by] varchar(18)  NULL,
    [email_broadcast_created_datetime] datetime  NULL,
    [email_broadcast_modified_by] varchar(18)  NULL,
    [email_broadcast_modfied_datetime] datetime  NULL,
    [email_broadcast_sys_modstamp] datetime  NULL,
    [email_broadcast_sent] datetime  NULL,
    [email_report_id] varchar(18)  NULL,
    [email_report_name] varchar(255)  NULL,
    [email_template_id] varchar(18)  NULL,
    [email_template_name] varchar(255)  NULL,
    [email_broadcast_mark_delete] bit  NOT NULL,
    [email_broadcast_send] datetime  NULL,
    [last_sfsync_datetime] datetime  NULL,
    [email_broadcast_department] varchar(255)  NULL
);
GO

-- Creating table 'crm_email_campaigns'
CREATE TABLE [dbo].[crm_email_campaigns] (
    [email_campaign_guid] uniqueidentifier  NOT NULL,
    [email_campaign_id] varchar(18)  NULL,
    [ec_deleted] bit  NOT NULL,
    [ec_report_id] varchar(18)  NULL,
    [ec_template_id] varchar(18)  NULL,
    [ec_created_by] varchar(18)  NULL,
    [ec_created_datetime] datetime  NULL,
    [ec_modified_by] varchar(18)  NULL,
    [ec_modified_datetime] datetime  NULL,
    [ec_system_modstamp] datetime  NULL,
    [ec_end_datetime] datetime  NULL,
    [ec_start_datetime] datetime  NULL,
    [ec_parent_campaign_id] varchar(18)  NULL,
    [ec_recur_days] float  NULL,
    [ec_send_time] time  NULL,
    [ec_week_days_only_flag] bit  NOT NULL,
    [ec_name] varchar(80)  NULL,
    [ec_department] varchar(80)  NULL,
    [ec_send_now] bit  NULL,
    [ec_active_flag] bit  NOT NULL,
    [ec_from_email_address_title] varchar(255)  NULL,
    [ec_from_email_address] varchar(255)  NULL,
    [ec_email_address_type] varchar(40)  NULL,
    [ec_mark_delete] bit  NOT NULL,
    [ec_recur_week_days] varchar(255)  NULL,
    [ec_recur] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [ec_allow_repeat_broadcasts] bit  NULL,
    [ec_allow_ongoing_delivery] bit  NOT NULL
);
GO

-- Creating table 'crm_email_messages'
CREATE TABLE [dbo].[crm_email_messages] (
    [email_message_guid] uniqueidentifier  NOT NULL,
    [email_message_id] varchar(18)  NULL,
    [email_message_case_id] varchar(18)  NULL,
    [email_message_activity_id] varchar(18)  NULL,
    [email_message_contact_id] varchar(18)  NULL,
    [email_message_account_id] varchar(18)  NULL,
    [email_message_created_by] varchar(18)  NULL,
    [email_message_created_datetime] datetime  NULL,
    [email_message_modified_by] varchar(18)  NULL,
    [email_message_modified_datetime] datetime  NULL,
    [email_message_system_mod_stamp] datetime  NULL,
    [email_message_text_body] varchar(max)  NULL,
    [email_message_html_body] varchar(max)  NULL,
    [email_message_headers] varchar(max)  NULL,
    [email_message_subject] varchar(3000)  NULL,
    [email_message_from_name] varchar(1000)  NULL,
    [email_message_from_address] varchar(1000)  NULL,
    [email_message_to_address] varchar(4000)  NULL,
    [email_message_cc_address] varchar(4000)  NULL,
    [email_message_bcc_address] varchar(4000)  NULL,
    [email_message_is_incoming] bit  NOT NULL,
    [email_message_has_attachment] bit  NOT NULL,
    [email_message_status] varchar(40)  NULL,
    [email_message_datetime] datetime  NULL,
    [email_message_deleted] bit  NOT NULL,
    [email_message_reply_to_msg_id] varchar(18)  NULL,
    [email_message_template_guid] uniqueidentifier  NULL,
    [email_message_sent_datetime] datetime  NULL,
    [email_message_sent] bit  NOT NULL,
    [email_message_error] varchar(4000)  NULL,
    [email_message_report_id] varchar(18)  NULL,
    [email_message_template_id] varchar(18)  NULL,
    [email_message_secondary_id] varchar(18)  NULL,
    [email_message_campaign_id] varchar(18)  NULL,
    [email_message_broadcast_id] varchar(18)  NULL,
    [email_message_broadcast_guid] uniqueidentifier  NULL
);
GO

-- Creating table 'crm_email_templates'
CREATE TABLE [dbo].[crm_email_templates] (
    [email_template_guid] uniqueidentifier  NOT NULL,
    [email_template_id] varchar(18)  NULL,
    [email_template_legacy_id] varchar(18)  NULL,
    [email_template_name] varchar(80)  NULL,
    [email_template_dev_name] varchar(80)  NULL,
    [email_template_namespace_prefix] varchar(15)  NULL,
    [email_template_owner_id] varchar(18)  NULL,
    [email_template_folder_id] varchar(18)  NULL,
    [email_template_folder_name] varchar(80)  NULL,
    [email_template_brand_template_id] varchar(18)  NULL,
    [email_template_style] varchar(40)  NULL,
    [email_template_is_active] bit  NOT NULL,
    [email_template_type] varchar(40)  NULL,
    [email_template_encoding] varchar(40)  NULL,
    [email_template_description] varchar(4000)  NULL,
    [email_template_subject] varchar(255)  NULL,
    [email_template_html_value] varchar(max)  NULL,
    [email_template_body] varchar(max)  NULL,
    [email_template_times_used] int  NULL,
    [email_template_last_used_datetime] datetime  NULL,
    [email_template_created_datetime] datetime  NULL,
    [email_template_created_by] varchar(18)  NULL,
    [email_template_modified_datetime] datetime  NULL,
    [email_template_modified_by] varchar(18)  NULL,
    [email_template_system_modstamp] datetime  NULL,
    [email_template_api_version] float  NULL,
    [email_template_markup] varchar(max)  NULL,
    [email_template_deleted] bit  NOT NULL,
    [email_template_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_event_registrations'
CREATE TABLE [dbo].[crm_event_registrations] (
    [event_registration_guid] uniqueidentifier  NOT NULL,
    [event_registration_id] varchar(18)  NULL,
    [event_registration_activity_extender_id] varchar(18)  NULL,
    [event_registration_contact_id] varchar(18)  NULL,
    [event_registration_attended] bit  NULL,
    [event_registration_registered] bit  NULL,
    [event_registration_deleted] bit  NULL,
    [event_registration_created_by] varchar(18)  NULL,
    [event_registration_created_datetime] datetime  NULL,
    [event_registration_modified_by] varchar(18)  NULL,
    [event_registration_modified_datetime] datetime  NULL,
    [event_registration_checkedin] bit  NULL,
    [event_registration_cancelled] bit  NULL,
    [event_registration_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [event_registration_confirmation_datetime] datetime  NULL
);
GO

-- Creating table 'crm_events'
CREATE TABLE [dbo].[crm_events] (
    [activity_guid] uniqueidentifier  NOT NULL,
    [activity_id] varchar(18)  NULL,
    [event_name_id] varchar(18)  NULL,
    [event_related_to_id] varchar(18)  NULL,
    [event_subject] varchar(255)  NULL,
    [event_location] varchar(255)  NULL,
    [event_all_day] bit  NOT NULL,
    [event_due_datetime] datetime  NULL,
    [event_due_date_only] datetime  NULL,
    [event_duration] int  NULL,
    [event_start_datetime] datetime  NULL,
    [event_end_datetime] datetime  NULL,
    [event_description] varchar(max)  NULL,
    [event_account_id] varchar(18)  NULL,
    [event_assigned_to_id] varchar(18)  NULL,
    [event_private] bit  NOT NULL,
    [event_show_time_as] varchar(40)  NULL,
    [event_deleted] bit  NOT NULL,
    [event_is_child] bit  NOT NULL,
    [event_is_group_event] bit  NOT NULL,
    [event_group_event_type] varchar(40)  NULL,
    [event_created_datetime] datetime  NULL,
    [event_created_by] varchar(18)  NULL,
    [event_modified_datetime] datetime  NULL,
    [event_modified_by] varchar(18)  NULL,
    [event_system_modstamp] datetime  NULL,
    [event_archived] bit  NOT NULL,
    [event_recurrence_activity_id] varchar(18)  NULL,
    [event_is_recurring] bit  NOT NULL,
    [event_recurrence_start] datetime  NULL,
    [event_recurrence_end] datetime  NULL,
    [event_recurrence_timezone] varchar(40)  NULL,
    [event_recurrence_type] varchar(40)  NULL,
    [event_recurrence_interval] int  NULL,
    [event_recurrence_day_of_week_mask] int  NULL,
    [event_recurrence_day_of_month] int  NULL,
    [event_recurrence_instance] varchar(40)  NULL,
    [event_recurrence_month_of_year] varchar(40)  NULL,
    [event_reminder_datetime] datetime  NULL,
    [event_reminder_set] bit  NOT NULL,
    [event_department] varchar(40)  NULL,
    [event_engagement_type] varchar(40)  NULL,
    [event_activity_extender_id] varchar(18)  NULL,
    [event_start_date] datetime  NULL,
    [event_end_date] datetime  NULL,
    [event_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [event_reminder_dates] varchar(255)  NULL,
    [event_reminder_email_address] varchar(255)  NULL,
    [event_reminder_send_flag] bit  NULL,
    [event_confirmation_send_flag] bit  NULL,
    [event_details_field] varchar(2000)  NULL
);
GO

-- Creating table 'crm_inquiries'
CREATE TABLE [dbo].[crm_inquiries] (
    [inquiry_guid] uniqueidentifier  NOT NULL,
    [inquiry_id] varchar(18)  NULL,
    [inq_contact_id] varchar(18)  NULL,
    [inq_owner_id] varchar(18)  NULL,
    [inq_deleted] bit  NOT NULL,
    [inq_anticipated_start_term] varchar(18)  NULL,
    [inq_pri_prog_interest] varchar(18)  NULL,
    [inq_sec_prog_interest] varchar(18)  NULL,
    [inq_services_interest] varchar(5000)  NULL,
    [inq_campus] varchar(255)  NULL,
    [inq_last_school] varchar(255)  NULL,
    [inq_source] varchar(40)  NULL,
    [inq_student_type] varchar(1000)  NULL,
    [inq_email_optin_date] datetime  NULL,
    [inq_agent_flag] bit  NOT NULL,
    [inq_agent_prev_flag] bit  NOT NULL,
    [inq_agency_name] varchar(255)  NULL,
    [inq_agent_rep_prospect] bit  NULL,
    [inq_created_datetime] datetime  NULL,
    [inq_created_by] varchar(18)  NULL,
    [inq_modified_datetime] datetime  NULL,
    [inq_modified_by] varchar(18)  NULL,
    [inq_last_activity_date] datetime  NULL,
    [inq_last_viewed_datetime] datetime  NULL,
    [inq_last_referenced_datetime] datetime  NULL,
    [inq_system_modstamp] datetime  NULL,
    [lc_inq_legacy_id] varchar(18)  NULL,
    [inq_contact_legacy_id] varchar(18)  NULL,
    [inq_anticipated_start_term_code] varchar(18)  NULL,
    [inq_number] varchar(15)  NULL,
    [inquiry_datetime] datetime  NULL,
    [inquiry_mark_delete] bit  NOT NULL,
    [inq_no_communications] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_leads'
CREATE TABLE [dbo].[crm_leads] (
    [lead_guid] uniqueidentifier  NOT NULL,
    [lead_id] varchar(18)  NULL,
    [lead_contact_id] varchar(18)  NULL,
    [srm_colleague_id] varchar(7)  NULL,
    [srm_contact_id] varchar(18)  NULL,
    [lead_deleted] bit  NOT NULL,
    [lead_master_record_id] varchar(18)  NULL,
    [lead_last_name] varchar(80)  NULL,
    [lead_first_name] varchar(40)  NULL,
    [lead_full_name] varchar(121)  NULL,
    [lead_title] varchar(128)  NULL,
    [lead_company] varchar(255)  NULL,
    [lead_street] varchar(255)  NULL,
    [lead_city] varchar(40)  NULL,
    [lead_province] varchar(80)  NULL,
    [lead_postal_code] varchar(20)  NULL,
    [lead_country] varchar(80)  NULL,
    [lead_phone] varchar(40)  NULL,
    [lead_email] varchar(80)  NULL,
    [lead_description] varchar(max)  NULL,
    [lead_source] varchar(40)  NULL,
    [lead_status] varchar(40)  NULL,
    [lead_owner_id] varchar(18)  NULL,
    [lead_converted] int  NULL,
    [lead_converted_datetime] datetime  NULL,
    [lead_unread_by_owner] bit  NULL,
    [lead_created_datetime] datetime  NULL,
    [lead_created_by] varchar(18)  NULL,
    [lead_last_modified_datetime] datetime  NULL,
    [lead_last_modified_by] varchar(18)  NULL,
    [lead_last_activity_datetime] datetime  NULL,
    [lead_last_viewed_datetime] datetime  NULL,
    [lead_last_referenced_datetime] datetime  NULL,
    [lead_email_bounced_reason] varchar(255)  NULL,
    [lead_email_bounced_datetime] datetime  NULL
);
GO

-- Creating table 'crm_po_course_codes'
CREATE TABLE [dbo].[crm_po_course_codes] (
    [po_course_code] varchar(20)  NOT NULL,
    [po_course_created_by] varchar(10)  NULL,
    [po_course_created_datetime] datetime  NULL,
    [po_course_modifed_by] varchar(10)  NULL,
    [po_course_modified_datetime] datetime  NULL
);
GO

-- Creating table 'crm_program_enrollments'
CREATE TABLE [dbo].[crm_program_enrollments] (
    [crm_program_enrollment_guid] uniqueidentifier  NOT NULL,
    [crm_program_enrollment_id] varchar(18)  NULL,
    [crm_program_enrollment_contact_id] varchar(18)  NULL,
    [crm_program_enrollment_program_id] varchar(18)  NULL,
    [crm_program_enrollment_affiliation] varchar(18)  NULL,
    [crm_program_enrollment_program_plan] varchar(18)  NULL,
    [crm_program_enrollment_order] int  NULL,
    [crm_program_enrollment_start_datetime] datetime  NULL,
    [crm_program_enrollment_end_datetime] datetime  NULL,
    [crm_program_enrollment_eligible_to_enroll] bit  NOT NULL,
    [crm_program_enrollment_status] varchar(1)  NULL,
    [crm_program_enrollment_submitted_date] datetime  NULL,
    [crm_program_enrollment_gpa] decimal(18,3)  NULL,
    [crm_program_enrollment_class_standing] varchar(40)  NULL,
    [crm_program_enrollment_credits_attempted] decimal(18,3)  NULL,
    [crm_program_enrollment_credits_earned] decimal(18,3)  NULL,
    [crm_program_enrollment_graduation_year] decimal(18,0)  NULL,
    [crm_program_enrollment_last_referenced_date] datetime  NULL,
    [crm_program_enrollment_last_viewed_date] datetime  NULL,
    [crm_program_enrollment_owner_id] varchar(18)  NULL,
    [crm_program_enrollment_created_by] varchar(18)  NULL,
    [crm_program_enrollment_created_datetime] datetime  NULL,
    [crm_program_enrollment_modified_by] varchar(18)  NULL,
    [crm_program_enrollment_modified_datetime] datetime  NULL,
    [crm_program_enrollment_deleted] bit  NOT NULL,
    [crm_program_enrollment_mark_delete] bit  NOT NULL,
    [crm_program_enrollment_guid_mismatch] bit  NOT NULL,
    [crm_program_enrollment_id_mismatch] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [crm_program_enrollment_key] varchar(10)  NULL
);
GO

-- Creating table 'crm_reports'
CREATE TABLE [dbo].[crm_reports] (
    [report_guid] uniqueidentifier  NOT NULL,
    [crm_report_id] varchar(18)  NULL,
    [crm_report_name] varchar(80)  NULL,
    [crm_report_developer_name] varchar(80)  NULL,
    [crm_report_description] varchar(255)  NULL,
    [crm_report_folder_name] varchar(80)  NULL,
    [crm_report_format] varchar(80)  NULL,
    [crm_report_last_run_date] datetime  NULL,
    [crm_report_deleted] bit  NOT NULL,
    [crm_namespace_prefix] varchar(80)  NULL,
    [crm_report_owner_id] varchar(18)  NULL,
    [crm_report_system_modstamp] datetime  NULL,
    [crm_report_last_referenced_date] datetime  NULL,
    [crm_report_last_viewed_date] datetime  NULL,
    [crm_report_modified_by] varchar(18)  NULL,
    [crm_report_modified_date] datetime  NULL,
    [crm_report_created_by] varchar(18)  NULL,
    [crm_report_created_date] datetime  NULL,
    [crm_report_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_role_values'
CREATE TABLE [dbo].[crm_role_values] (
    [crm_role_value_guid] uniqueidentifier  NOT NULL,
    [crm_role_value_id] varchar(18)  NULL,
    [crm_rv_role_id] varchar(18)  NULL,
    [crm_rv_name] varchar(80)  NULL,
    [crm_rv_sender_name] varchar(2000)  NULL,
    [crm_rv_send_emails] varchar(2000)  NULL,
    [crm_rv_default_contact_type] varchar(40)  NULL,
    [crm_rv_owner_id] varchar(18)  NULL,
    [crm_rv_modified_by] varchar(18)  NULL,
    [crm_rv_modified_datetime] datetime  NULL,
    [crm_rv_created_by] varchar(18)  NULL,
    [crm_rv_created_datetime] datetime  NULL,
    [crm_rv_deleted_flag] bit  NOT NULL,
    [crm_rv_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_scanner_registrations'
CREATE TABLE [dbo].[crm_scanner_registrations] (
    [crm_scanner_registration_guid] uniqueidentifier  NOT NULL,
    [crm_scanner_barcode] varchar(18)  NULL,
    [crm_scanner_colleague_id] varchar(7)  NULL,
    [crm_scanner_first_name] varchar(50)  NULL,
    [crm_scanner_last_name] varchar(50)  NULL,
    [crm_scanner_birth_date] datetime  NULL,
    [crm_scanner_event_id] varchar(18)  NULL,
    [crm_scanner_registration_processed] datetime  NULL,
    [crm_scanner_modified_date] datetime  NULL,
    [crm_scanner_registration_skip] bit  NOT NULL
);
GO

-- Creating table 'crm_tasks'
CREATE TABLE [dbo].[crm_tasks] (
    [activity_guid] uniqueidentifier  NOT NULL,
    [activity_id] varchar(18)  NULL,
    [task_name_id] varchar(18)  NULL,
    [task_related_to_id] varchar(18)  NULL,
    [task_subject] varchar(255)  NULL,
    [task_due_date_only] datetime  NULL,
    [task_status] varchar(80)  NULL,
    [task_priority] varchar(40)  NULL,
    [task_high_priority] bit  NOT NULL,
    [task_assigned_to_id] varchar(18)  NULL,
    [task_description] varchar(max)  NULL,
    [task_deleted] bit  NOT NULL,
    [task_account_id] varchar(18)  NULL,
    [task_closed] bit  NOT NULL,
    [task_created_datetime] datetime  NULL,
    [task_created_by] varchar(18)  NULL,
    [task_modified_datetime] datetime  NULL,
    [task_modified_by] varchar(18)  NULL,
    [task_system_modstamp] datetime  NULL,
    [task_archived] bit  NOT NULL,
    [task_call_duration] int  NULL,
    [task_call_type] varchar(18)  NULL,
    [task_call_result] varchar(255)  NULL,
    [task_call_object_identfier] varchar(255)  NULL,
    [task_reminder_datetime] datetime  NULL,
    [task_reminder_set] bit  NOT NULL,
    [task_recurrence_activity_id] varchar(18)  NULL,
    [task_is_recurrence] bit  NOT NULL,
    [task_recurrence_start_datetime] datetime  NULL,
    [task_recurrence_end_datetime] datetime  NULL,
    [task_recurrence_timezone] varchar(40)  NULL,
    [task_recurrence_type] varchar(40)  NULL,
    [task_recurrence_interval] int  NULL,
    [task_recurrence_day_of_week_mask] int  NULL,
    [task_recurrence_day_of_month] int  NULL,
    [task_recurrence_instance] varchar(40)  NULL,
    [task_recurrence_month_of_year] varchar(40)  NULL,
    [task_repeat_this_task] varchar(40)  NULL,
    [tast_activity_datetime] datetime  NULL,
    [task_completed_datetime] datetime  NULL,
    [task_owner_id] varchar(18)  NULL,
    [task_regenerated_type] varchar(80)  NULL,
    [task_subtype] varchar(40)  NULL,
    [task_what_id] varchar(18)  NULL,
    [task_who_id] varchar(18)  NULL,
    [task_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL
);
GO

-- Creating table 'crm_terms'
CREATE TABLE [dbo].[crm_terms] (
    [term_guid] uniqueidentifier  NOT NULL,
    [term_id] varchar(18)  NULL,
    [term_deleted] bit  NOT NULL,
    [term_name] varchar(80)  NULL,
    [term_created_datetime] datetime  NULL,
    [term_created_by] varchar(18)  NULL,
    [term_modifed_datetime] datetime  NULL,
    [term_modified_by] varchar(18)  NULL,
    [term_system_modstamp] datetime  NULL,
    [term_last_viewed_datetime] datetime  NULL,
    [term_last_referenced_datetime] datetime  NULL,
    [term_account_id] varchar(18)  NULL,
    [term_end_date] datetime  NULL,
    [term_start_date] datetime  NULL,
    [term_grade_period_sequence] decimal(18,2)  NULL,
    [term_instructional_days] decimal(16,2)  NULL,
    [term_parent_term_id] varchar(18)  NULL,
    [term_type] varchar(255)  NULL,
    [term_code] varchar(4)  NULL,
    [term_reporting_year] decimal(18,0)  NULL,
    [term_year] decimal(18,0)  NULL,
    [term_census_date] datetime  NULL,
    [term_reg_start_date] datetime  NULL,
    [term_reg_end_date] datetime  NULL,
    [term_prereg_start_date] datetime  NULL,
    [term_prereg_end_date] datetime  NULL,
    [term_drop_start_date] datetime  NULL,
    [term_drop_end_date] datetime  NULL,
    [term_drop_grad_reqd_date] datetime  NULL,
    [term_sequence_number] decimal(18,0)  NULL,
    [term_mark_delete] bit  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [term_show_web] bit  NULL,
    [term_name_alias] varchar(80)  NULL
);
GO

-- Creating table 'dynamic_content'
CREATE TABLE [dbo].[dynamic_content] (
    [dynamic_content_id] varchar(18)  NOT NULL,
    [dynamic_content_label] varchar(50)  NULL,
    [dynamic_content_name] varchar(80)  NULL,
    [dynamic_content_matching_id] varchar(18)  NULL,
    [dynamic_content_deleted] bit  NOT NULL,
    [dynamic_content_owner_id] varchar(18)  NULL,
    [dynamic_content_available] bit  NOT NULL,
    [dynamic_content_html] varchar(max)  NULL,
    [dynamic_content_text] varchar(max)  NULL,
    [dynamic_content_created_by] varchar(18)  NULL,
    [dynamic_content_created_datetime] datetime  NULL,
    [dynamic_content_modified_by] varchar(18)  NULL,
    [dynamic_content_modified_datetime] datetime  NULL,
    [dynamic_system_modstamp] datetime  NULL
);
GO

-- Creating table 'lcc2_barcodes'
CREATE TABLE [dbo].[lcc2_barcodes] (
    [sis_student_id] varchar(7)  NOT NULL,
    [lcc2_barcode_number] varchar(14)  NOT NULL,
    [lcc2_barcode_deleted] bit  NOT NULL,
    [lcc2_barcode_modified_datetime] datetime  NOT NULL
);
GO

-- Creating table 'merge_fields'
CREATE TABLE [dbo].[merge_fields] (
    [merge_field_id] uniqueidentifier  NOT NULL,
    [email_template_id] uniqueidentifier  NOT NULL,
    [merge_field] varchar(80)  NOT NULL
);
GO

-- Creating table 'soql_queries'
CREATE TABLE [dbo].[soql_queries] (
    [soql_query_id] uniqueidentifier  NOT NULL,
    [soql_query_data_type] int  NULL,
    [soql_query_description] varchar(60)  NULL,
    [soql_query_string] nvarchar(max)  NULL,
    [soql_query_created_by] varchar(18)  NULL,
    [soql_query_created_datetime] datetime  NULL,
    [soql_query_modified_by] varchar(18)  NULL,
    [soql_query_modified_datetime] datetime  NULL
);
GO

-- Creating table 'transaction_errors'
CREATE TABLE [dbo].[transaction_errors] (
    [transaction_error_id] uniqueidentifier  NOT NULL,
    [transaction_error_datetime] datetime  NOT NULL,
    [transaction_error_message] varchar(max)  NULL,
    [transaction_type] varchar(80)  NULL
);
GO

-- Creating table 'transaction_loads'
CREATE TABLE [dbo].[transaction_loads] (
    [transaction_load_guid] uniqueidentifier  NOT NULL,
    [transaction_load_name] varchar(60)  NOT NULL,
    [transaction_load_start_datetime] datetime  NULL,
    [transaction_load_start_record] varchar(18)  NULL,
    [transaction_load_end_record] varchar(18)  NULL,
    [transaction_load_end_datetime] datetime  NULL,
    [transaction_load_batch_size] varchar(6)  NULL,
    [transaction_load_run_time] float  NULL,
    [transaction_load_created_datetime] datetime  NOT NULL,
    [transaction_load_modified_datetime] datetime  NOT NULL
);
GO

-- Creating table 'transaction_logs'
CREATE TABLE [dbo].[transaction_logs] (
    [transaction_id] uniqueidentifier  NOT NULL,
    [transaction_name] varchar(50)  NULL,
    [records_inserted] int  NULL,
    [records_updated] int  NULL,
    [records_total] int  NULL,
    [last_executed] datetime  NULL,
    [run_time] float  NULL,
    [transaction_notes] varchar(max)  NULL,
    [transaction_successful] bit  NULL
);
GO

-- Creating table 'wt_addresses'
CREATE TABLE [dbo].[wt_addresses] (
    [sis_address_id] varchar(10)  NOT NULL,
    [sis_address_lines] varchar(1000)  NULL,
    [sis_address_city] varchar(30)  NULL,
    [sis_address_state] varchar(30)  NULL,
    [sis_address_country] varchar(8)  NULL,
    [sis_address_zip] varchar(10)  NULL,
    [sis_address_created_datetime] datetime  NULL,
    [sis_address_modified_datetime] datetime  NULL
);
GO

-- Creating table 'wt_applicants'
CREATE TABLE [dbo].[wt_applicants] (
    [sis_applicant_id] varchar(7)  NOT NULL,
    [sis_applicant_housing_flag] bit  NOT NULL,
    [sis_applicant_comments] varchar(5000)  NULL,
    [sis_applicant_acad_level] varchar(3)  NULL,
    [sis_applicant_eps_code] varchar(6)  NULL,
    [sis_applicant_created_date] datetime  NULL,
    [sis_applicant_modified_date] datetime  NULL
);
GO

-- Creating table 'wt_application_statuses'
CREATE TABLE [dbo].[wt_application_statuses] (
    [sis_application_id] varchar(6)  NOT NULL,
    [sis_appl_status] varchar(3)  NULL,
    [sis_appl_status_datetime] datetime  NULL,
    [sis_appl_pos] int  NULL
);
GO

-- Creating table 'wt_applications'
CREATE TABLE [dbo].[wt_applications] (
    [sis_application_id] varchar(10)  NOT NULL,
    [sis_student_id] varchar(10)  NULL,
    [sis_appl_program] varchar(20)  NULL,
    [sis_appl_location] varchar(5)  NULL,
    [sis_appl_start_year] int  NULL,
    [sis_appl_start_term] varchar(6)  NULL,
    [sis_appl_stu_load_intent] varchar(10)  NULL,
    [sis_appl_offer_due_date] datetime  NULL,
    [sis_alt_status_datetime] datetime  NULL,
    [sis_app_status_datetime] datetime  NULL,
    [sis_con_status_datetime] datetime  NULL,
    [sis_dtc_status_datetime] datetime  NULL,
    [sis_dac_status_datetime] datetime  NULL,
    [sis_fi_status_datetime] datetime  NULL,
    [sis_fw_status_datetime] datetime  NULL,
    [sis_ms_status_datetime] datetime  NULL,
    [sis_ntq_status_datetime] datetime  NULL,
    [sis_ofi_status_datetime] datetime  NULL,
    [sis_ofc_status_datetime] datetime  NULL,
    [sis_pas_status_datetime] datetime  NULL,
    [sis_par_status_datetime] datetime  NULL,
    [sis_ppr_status_datetime] datetime  NULL,
    [sis_w_status_datetime] datetime  NULL,
    [sis_pr_status_datetime] datetime  NULL,
    [sis_sc_status_datetime] datetime  NULL,
    [sis_unc_status_datetime] datetime  NULL,
    [sis_wtl_status_datetime] datetime  NULL,
    [sis_wap_status_datetime] datetime  NULL,
    [sis_appl_parent_edu_level] varchar(10)  NULL,
    [sis_appl_admit_status] varchar(5)  NULL,
    [sis_appl_status] varchar(5)  NULL,
    [sis_appl_status_date] datetime  NULL,
    [sis_appl_stage] varchar(11)  NULL,
    [sis_appl_mark_delete] bit  NOT NULL,
    [sis_appl_date] datetime  NULL,
    [sis_appl_modified_date] datetime  NULL
);
GO

-- Creating table 'wt_core_students'
CREATE TABLE [dbo].[wt_core_students] (
    [sis_student_id] varchar(10)  NOT NULL,
    [sis_stu_first_name] varchar(30)  NULL,
    [sis_stu_last_name] varchar(60)  NULL,
    [sis_stu_middle_name] varchar(30)  NULL,
    [sis_stu_hist_last_name] varchar(60)  NULL,
    [sis_stu_birth_date] datetime  NULL,
    [sis_stu_gender] varchar(1)  NULL,
    [sis_stu_pref_email] varchar(50)  NULL,
    [sis_stu_off_email] varchar(50)  NULL,
    [sis_stu_address] varchar(500)  NULL,
    [sis_stu_city] varchar(60)  NULL,
    [sis_stu_state] varchar(30)  NULL,
    [sis_stu_country] varchar(32)  NULL,
    [sis_stu_zip] varchar(10)  NULL,
    [sis_stu_home_phone] varchar(40)  NULL,
    [sis_stu_per_phone] varchar(40)  NULL,
    [sis_stu_marital_status] varchar(32)  NULL,
    [sis_stu_native_lang] varchar(32)  NULL,
    [sis_stu_alien_status] varchar(32)  NULL,
    [sis_stu_international_flag] bit  NOT NULL,
    [sis_stu_program] varchar(13)  NULL,
    [sis_stu_prog_start_year] int  NULL,
    [sis_stu_prog_start_term] varchar(7)  NULL,
    [sis_stu_location] varchar(12)  NULL,
    [sis_stu_restriction_flag] bit  NOT NULL,
    [sis_stu_duplicate_flag] bit  NOT NULL,
    [sis_stu_privacy_flag] bit  NOT NULL,
    [sis_stu_deleted_flag] bit  NOT NULL,
    [sis_stu_aboriginal] varchar(30)  NULL,
    [sis_stu_app_status] varchar(4)  NULL,
    [crm_student_stage] varchar(30)  NULL,
    [sis_stu_alumni_type] bit  NOT NULL,
    [created_by] varchar(7)  NULL,
    [created_date] datetime  NULL,
    [modified_by] varchar(7)  NULL,
    [modified_date] datetime  NULL,
    [sis_stu_mark_delete] bit  NOT NULL,
    [sis_stu_student_type] bit  NOT NULL,
    [sis_stu_applicant_type] bit  NOT NULL
);
GO

-- Creating table 'wt_course_departments'
CREATE TABLE [dbo].[wt_course_departments] (
    [sis_course_id] varchar(11)  NOT NULL,
    [sis_course_department] varchar(6)  NOT NULL,
    [sis_course_department_modified_datetime] datetime  NULL,
    [sis_course_department_created_datetime] datetime  NULL,
    [sis_course_department_deleted] bit  NOT NULL
);
GO

-- Creating table 'wt_course_sections'
CREATE TABLE [dbo].[wt_course_sections] (
    [sis_course_section_id] varchar(19)  NOT NULL,
    [sis_cs_sec_short_title] varchar(30)  NULL,
    [sis_cs_sec_sched_type] varchar(5)  NULL,
    [sis_cs_sec_location] varchar(5)  NULL,
    [sis_cs_sec_min_enroll] decimal(5,0)  NULL,
    [sis_cs_sec_capacity] decimal(5,0)  NULL,
    [sis_cs_sec_subject] varchar(7)  NULL,
    [sis_cs_sec_min_cred] decimal(9,5)  NULL,
    [sis_cs_sec_reg_retake_policy] varchar(5)  NULL,
    [sis_cs_sec_only_pass_nopass_flag] bit  NOT NULL,
    [sis_cs_sec_term] varchar(7)  NULL,
    [sis_cs_sec_course_no] varchar(7)  NULL,
    [sis_cs_sec_no] varchar(5)  NULL,
    [sis_cs_sec_billing_cred] decimal(9,5)  NULL,
    [sis_cs_sec_course] varchar(10)  NULL,
    [sis_cs_sec_petition_reqd_flag] bit  NOT NULL,
    [sis_cs_sec_acad_level] varchar(5)  NULL,
    [sis_cs_sec_cred_type] varchar(5)  NULL,
    [sis_cs_sec_allow_pass_flag] bit  NOT NULL,
    [sis_cs_sec_allow_audit_flag] bit  NOT NULL,
    [sis_cs_sec_grade_scheme] varchar(5)  NULL,
    [sis_cs_sec_start_date] datetime  NULL,
    [sis_cs_sec_end_date] datetime  NULL,
    [sis_cs_sec_comments] varchar(2000)  NULL,
    [sis_cs_sec_topic_code] varchar(5)  NULL,
    [sis_cs_sec_billing_period_type] varchar(10)  NULL,
    [sis_cs_sec_gl_no] varchar(30)  NULL,
    [sis_cs_sec_synonym] varchar(11)  NULL,
    [sis_cs_sec_alow_waitlist_flag] bit  NOT NULL,
    [sis_cs_sec_billing_method] varchar(10)  NULL,
    [sis_cs_sec_drop_reg_ref_pol] varchar(10)  NULL,
    [sis_cs_sec_over_reg_start_date] datetime  NULL,
    [sis_cs_sec_ovr_reg_end_date] datetime  NULL,
    [sis_cs_sec_ovr_add_start_date] datetime  NULL,
    [sis_cs_sec_ovr_add_end_date] datetime  NULL,
    [sis_cs_sec_ovr_prereq_end_date] datetime  NULL,
    [sis_cs_sec_ovr_prereg_end_date] datetime  NULL,
    [sis_cs_sec_ovr_drop_end_date] datetime  NULL,
    [sis_cs_sec_transfer_status] varchar(10)  NULL,
    [sis_cs_sec_purpose] varchar(10)  NULL,
    [sis_cs_sec_faculty_consent_flag] bit  NOT NULL,
    [sis_cs_sec_printed_comments] varchar(2000)  NULL,
    [sis_cs_sec_ovr_drop_gr_reqd_date] datetime  NULL,
    [sis_cs_sec_name] varchar(21)  NULL,
    [sis_cs_sec_ovr_drop_start_date] datetime  NULL,
    [sis_cs_sec_no_weeks] decimal(3,0)  NULL,
    [sis_cs_sec_discount_max_amt] decimal(10,2)  NULL,
    [sis_cs_sec_cip_flag] bit  NOT NULL,
    [sis_cs_sec_time_bill_flag] bit  NOT NULL,
    [sis_cs_sec_user1] varchar(20)  NULL,
    [sis_cs_sec_r2_event] varchar(20)  NULL,
    [sis_cs_sec_special_property_flag] bit  NOT NULL,
    [sis_cs_sec_first_meeting_date] datetime  NULL,
    [sis_cs_sec_last_meeting_date] datetime  NULL,
    [sis_cs_sec_meeting_info] varchar(180)  NULL,
    [sis_cs_sec_faculty_info] varchar(45)  NULL,
    [sis_cs_created_date] datetime  NULL,
    [sis_cs_modified_datetime] datetime  NULL,
    [sis_cs_sec_override_crs_reqs_flag] bit  NOT NULL
);
GO

-- Creating table 'wt_courses'
CREATE TABLE [dbo].[wt_courses] (
    [sis_course_id] varchar(10)  NOT NULL,
    [sis_course_title] varchar(1996)  NULL,
    [sis_course_cred_type] varchar(5)  NULL,
    [sis_course_name] varchar(15)  NULL,
    [sis_course_desc] varchar(max)  NULL,
    [sis_course_short_title] varchar(30)  NULL,
    [sis_course_subject] varchar(7)  NULL,
    [sis_course_min_cred] decimal(9,5)  NULL,
    [sis_course_max_cred] decimal(9,5)  NULL,
    [sis_course_prereqs] varchar(20)  NULL,
    [sis_course_no] varchar(7)  NULL,
    [sis_course_cip] varchar(9)  NULL,
    [sis_course_retake_policy] varchar(5)  NULL,
    [sis_course_start_date] datetime  NULL,
    [sis_course_end_date] datetime  NULL,
    [sis_course_acad_level] varchar(5)  NULL,
    [sis_course_grade_scheme] varchar(5)  NULL,
    [sis_course_sched_type] varchar(5)  NULL,
    [sis_course_billing_period_type] varchar(10)  NULL,
    [sis_course_drop_reg_ref_pol] varchar(10)  NULL,
    [sis_course_billing_method] varchar(10)  NULL,
    [sis_course_comments] varchar(max)  NULL,
    [sis_course_billing_cred] decimal(9,5)  NULL,
    [sis_course_no_weeks] decimal(3,0)  NULL,
    [sis_course_transfer_status] varchar(10)  NULL,
    [sis_course_count_retake_cred_flag] varchar(1)  NULL,
    [sis_course_created_by] varchar(20)  NULL,
    [sis_course_created_date] datetime  NULL,
    [sis_course_modified_by] varchar(20)  NULL,
    [sis_course_allow_audit_flag] varchar(1)  NULL,
    [sis_course_allow_waitlist_flag] varchar(1)  NULL,
    [sis_course_topic_code] varchar(5)  NULL,
    [sis_course_capacity] decimal(5,0)  NULL,
    [sis_course_min_enrol] decimal(5,0)  NULL,
    [sis_course_subject_idx] varchar(15)  NULL,
    [sis_waitlist_mult_sect_flag] varchar(3)  NULL,
    [sis_course_prtl_cc_update_datetime] datetime  NULL,
    [sis_course_modified_datetime] datetime  NULL,
    [sis_course_deleted] bit  NULL,
    [update] bit  NULL
);
GO

-- Creating table 'wt_departments'
CREATE TABLE [dbo].[wt_departments] (
    [sis_department_id] varchar(5)  NOT NULL,
    [sis_department_modified_datetime] datetime  NULL,
    [sis_department_created_datetime] datetime  NULL,
    [sis_department_desc] varchar(30)  NULL,
    [sis_department_division_code] varchar(5)  NULL,
    [sis_department_active_code] varchar(1)  NULL,
    [sis_department_created_by] varchar(20)  NULL,
    [sis_department_modified_by] varchar(20)  NULL,
    [sis_department_head_id] varchar(10)  NULL,
    [sis_department_type] varchar(10)  NULL,
    [sis_department_deleted] bit  NOT NULL,
    [sis_department_mark_delete] bit  NOT NULL
);
GO

-- Creating table 'wt_divisions'
CREATE TABLE [dbo].[wt_divisions] (
    [sis_division_id] varchar(5)  NOT NULL,
    [sis_division_desc] varchar(35)  NULL,
    [sis_division_head] varchar(10)  NULL,
    [sis_division_school] varchar(5)  NULL,
    [sis_division_created_date] datetime  NULL,
    [sis_division_created_by] varchar(20)  NULL,
    [sis_division_modified_by] varchar(20)  NULL,
    [sis_division_type] varchar(10)  NULL,
    [sis_division_department_code] varchar(5)  NULL,
    [sis_division_location] varchar(5)  NULL,
    [sis_division_modified_datetime] datetime  NULL,
    [sis_division_deleted] bit  NULL,
    [update] bit  NULL
);
GO

-- Creating table 'wt_institutions'
CREATE TABLE [dbo].[wt_institutions] (
    [sis_institution_id] varchar(10)  NOT NULL,
    [crm_account_id] varchar(18)  NULL,
    [sis_inst_type] varchar(10)  NULL,
    [sis_inst_name] varchar(255)  NULL,
    [sis_inst_address] varchar(255)  NULL,
    [sis_inst_city] varchar(50)  NULL,
    [sis_inst_state] varchar(50)  NULL,
    [sis_inst_zip] varchar(15)  NULL,
    [sis_inst_phone] varchar(20)  NULL,
    [sis_inst_deleted] bit  NOT NULL,
    [sis_inst_mod_date] datetime  NULL,
    [update] bit  NULL
);
GO

-- Creating table 'wt_off_email_restore'
CREATE TABLE [dbo].[wt_off_email_restore] (
    [crm_contact_id] varchar(18)  NOT NULL,
    [crm_off_email_address] varchar(255)  NULL
);
GO

-- Creating table 'wt_program_enrollments'
CREATE TABLE [dbo].[wt_program_enrollments] (
    [crm_program_enrollment_student_id] varchar(7)  NOT NULL,
    [crm_program_enrollment_program_id] varchar(32)  NOT NULL,
    [crm_program_enrollment_start_date] datetime  NOT NULL,
    [crm_program_enrollment_end_date] datetime  NULL,
    [crm_program_enrollment_admit_status] varchar(5)  NULL,
    [crm_program_enrollment_location] varchar(5)  NULL,
    [crm_program_enrollment_catalog] varchar(5)  NULL,
    [crm_program_enrollment_department] varchar(5)  NULL,
    [crm_program_enrollment_division] varchar(5)  NULL,
    [crm_program_enrollment_school] varchar(5)  NULL,
    [crm_program_enrollment_comments] varchar(2000)  NULL,
    [crm_program_enrollment_eval_include_code] varchar(10)  NULL,
    [crm_program_enrollment_printed_comments] varchar(2000)  NULL,
    [crm_program_enrollment_eval_date] datetime  NULL,
    [crm_program_enrollment_eval_status] varchar(10)  NULL,
    [crm_program_enrollment_combined_cred] decimal(9,5)  NULL,
    [crm_program_enrollment_combined_gpa] decimal(8,5)  NULL,
    [crm_program_enrollment_inst_cred] decimal(9,5)  NULL,
    [crm_program_enrollment_inst_gpa] decimal(8,5)  NULL,
    [crm_program_enrollment_acad_cred_end_date] datetime  NULL,
    [crm_program_enrollment_ant_cmpl_date] datetime  NULL,
    [crm_program_enrollment_created_date] datetime  NULL,
    [crm_program_enrollment_modified_date] datetime  NULL,
    [crm_program_enrollment_order] smallint  NULL,
    [crm_program_enrollment_mark_delete] bit  NOT NULL
);
GO

-- Creating table 'wt_programs'
CREATE TABLE [dbo].[wt_programs] (
    [sis_program_id] varchar(20)  NOT NULL,
    [sis_program_desc] varchar(max)  NULL,
    [sis_program_title] varchar(60)  NULL,
    [sis_program_acad_level] varchar(5)  NULL,
    [sis_program_active_flag] bit  NULL,
    [sis_program_stu_select_flag] bit  NOT NULL,
    [sis_program_start_date] datetime  NULL,
    [sis_program_end_date] datetime  NULL,
    [sis_program_comments] varchar(max)  NULL,
    [sis_program_created_by] varchar(20)  NULL,
    [sis_program_created_date] datetime  NULL,
    [sis_program_modified_by] varchar(20)  NULL,
    [sis_program_allow_grad_flag] bit  NOT NULL,
    [sis_program_grade_scheme] varchar(5)  NULL,
    [sis_program_transcript_grouping] varchar(5)  NULL,
    [sis_program_create_application_flag] bit  NOT NULL,
    [sis_program_admit_capacity] decimal(6,0)  NULL,
    [sis_program_catalogs] varchar(5)  NULL,
    [sis_program_ccds] varchar(5)  NULL,
    [sis_program_locations] varchar(5)  NULL,
    [sis_program_departments] varchar(5)  NULL,
    [sis_program_gov_codes] varchar(15)  NULL,
    [sis_program_modified_datetime] datetime  NULL,
    [sis_program_deleted] bit  NOT NULL,
    [sis_program_active] bit  NOT NULL,
    [sis_program_mark_delete] bit  NOT NULL
);
GO

-- Creating table 'wt_student_course_sections'
CREATE TABLE [dbo].[wt_student_course_sections] (
    [sis_scs_id] varchar(10)  NOT NULL,
    [sis_cs_id] varchar(19)  NULL,
    [sis_stc_id] varchar(10)  NULL,
    [sis_student_id] varchar(10)  NULL,
    [sis_scs_reg_method] varchar(10)  NULL,
    [sis_scs_location] varchar(5)  NULL,
    [sis_scs_ar_posted_flag] bit  NOT NULL,
    [sis_scs_reg_time_period] varchar(10)  NULL,
    [sis_scs_created_date] datetime  NULL,
    [sis_scs_modified_date] datetime  NULL,
    [sis_scs_mark_delete] bit  NOT NULL
);
GO

-- Creating table 'wt_student_courses'
CREATE TABLE [dbo].[wt_student_courses] (
    [sis_stu_course_id] varchar(10)  NOT NULL,
    [sis_student_id] varchar(10)  NOT NULL,
    [sis_stu_course_term] varchar(7)  NOT NULL,
    [sis_stu_course_name] varchar(80)  NULL,
    [sis_stu_course_title] varchar(30)  NULL,
    [sis_stu_course_subject] varchar(7)  NULL,
    [sis_stu_course_number] varchar(7)  NULL,
    [sis_stu_course_section] varchar(5)  NULL,
    [sis_stu_course_campus] varchar(5)  NULL,
    [sis_stu_course_mod_date] datetime  NULL,
    [sis_stu_course_deleted] bit  NOT NULL,
    [sis_stu_course_mark_delete] bit  NULL
);
GO

-- Creating table 'wt_student_credentials'
CREATE TABLE [dbo].[wt_student_credentials] (
    [sis_student_id] varchar(7)  NOT NULL,
    [crm_program_id] varchar(18)  NOT NULL,
    [sis_student_cred_date] datetime  NULL,
    [sis_student_cred_type] varchar(5)  NULL,
    [sis_student_cred_created_datetime] datetime  NULL,
    [sis_student_cred_modified_datetime] datetime  NULL,
    [sis_student_cred_deleted] bit  NOT NULL,
    [sis_student_cred_mark_delete] bit  NOT NULL
);
GO

-- Creating table 'wt_student_programs'
CREATE TABLE [dbo].[wt_student_programs] (
    [sis_stu_program_id] varchar(25)  NOT NULL,
    [sis_student_id] varchar(7)  NOT NULL,
    [sis_stu_program_code] varchar(100)  NOT NULL,
    [sis_stu_program_location] varchar(10)  NULL,
    [sis_stu_program_admit_status] varchar(5)  NULL,
    [sis_stu_program_status] varchar(1)  NULL,
    [sis_stu_program_status_date] datetime  NULL,
    [sis_stu_program_start_date] datetime  NULL,
    [sis_stu_program_end_date] datetime  NULL,
    [sis_stu_program_created_date] datetime  NULL,
    [sis_stu_program_modified_date] datetime  NULL,
    [sis_stu_program_mark_delete] bit  NOT NULL,
    [sis_stu_program_deleted] bit  NOT NULL
);
GO

-- Creating table 'wt_student_terms'
CREATE TABLE [dbo].[wt_student_terms] (
    [sis_sttr_id] varchar(24)  NOT NULL,
    [sis_student_id] varchar(10)  NOT NULL,
    [sis_term_id] varchar(4)  NOT NULL,
    [sis_sttr_type] varchar(4)  NOT NULL,
    [sis_term_modified_date] datetime  NULL,
    [sis_stu_term_deleted] bit  NOT NULL,
    [sis_stu_term_mark_delete] bit  NOT NULL
);
GO

-- Creating table 'wt_terms'
CREATE TABLE [dbo].[wt_terms] (
    [sis_term_id] varchar(4)  NOT NULL,
    [sis_term_name] varchar(40)  NULL,
    [sis_term_desc] varchar(40)  NULL,
    [sis_reporting_year] varchar(4)  NULL,
    [sis_term_year] varchar(4)  NULL,
    [sis_term_prereg_start_date] datetime  NULL,
    [sis_term_prereg_end_date] datetime  NULL,
    [sis_term_reg_start_date] datetime  NULL,
    [sis_term_start_date] datetime  NULL,
    [sis_term_reg_end_date] datetime  NULL,
    [sis_term_end_date] datetime  NULL,
    [sis_term_reporting_term] varchar(4)  NULL,
    [sis_term_add_start_date] datetime  NULL,
    [sis_term_add_end_date] datetime  NULL,
    [sis_term_drop_grad_reqd_date] datetime  NULL,
    [sis_term_drop_start_date] datetime  NULL,
    [sis_term_drop_end_date] datetime  NULL,
    [sis_term_sequence_no] smallint  NULL,
    [sis_term_created_by] varchar(7)  NULL,
    [sis_term_created_date] datetime  NULL,
    [sis_term_modified_by] varchar(7)  NULL,
    [sis_term_modified_date] datetime  NULL,
    [sis_term_bill_end_date] datetime  NULL,
    [sis_term_use_in_best_fit_calc] bit  NOT NULL,
    [sis_term_census_dates] datetime  NULL,
    [sis_term_deleted] bit  NOT NULL,
    [sis_term_mark_delete] bit  NOT NULL
);
GO

-- Creating table 'sv_core_student_duplicates'
CREATE TABLE [dbo].[sv_core_student_duplicates] (
    [sis_student_id] varchar(10)  NOT NULL
);
GO

-- Creating table 'wv_applicants'
CREATE TABLE [dbo].[wv_applicants] (
    [sis_application_id] varchar(10)  NULL,
    [sis_student_id] varchar(10)  NULL,
    [sis_app_program] varchar(8000)  NULL,
    [sis_app_location] varchar(5)  NULL,
    [sis_app_start_term] varchar(7)  NULL,
    [sis_app_start_year] varchar(4)  NULL,
    [sis_app_start_session] varchar(6)  NOT NULL,
    [sis_app_stu_load_intent] varchar(10)  NULL,
    [sis_app_admit_status] varchar(5)  NULL,
    [sis_app_status] varchar(5)  NULL,
    [sis_app_status_date] datetime  NULL,
    [sis_app_stage] varchar(11)  NOT NULL,
    [sis_app_offer_due_date] datetime  NULL,
    [sis_app_parent_edu_level] varchar(10)  NULL,
    [sis_app_mod_date] datetime  NULL
);
GO

-- Creating table 'wv_application_statuses'
CREATE TABLE [dbo].[wv_application_statuses] (
    [sis_application_id] varchar(10)  NOT NULL,
    [sis_appl_status] varchar(5)  NULL,
    [sis_appl_status_datetime] datetime  NULL
);
GO

-- Creating table 'wv_applications'
CREATE TABLE [dbo].[wv_applications] (
    [sis_application_id] varchar(10)  NOT NULL,
    [sis_student_id] varchar(10)  NULL,
    [sis_appl_program] varchar(8000)  NULL,
    [sis_appl_location] varchar(5)  NOT NULL,
    [sis_appl_start_term] varchar(7)  NULL,
    [sis_appl_start_year] varchar(4)  NULL,
    [sis_appl_start_session] varchar(6)  NOT NULL,
    [sis_appl_stu_load_intent] varchar(10)  NULL,
    [sis_appl_fa_intent_flag] int  NOT NULL,
    [sis_appl_active_stu_program_flag] int  NOT NULL,
    [sis_appl_admit_status] varchar(5)  NULL,
    [sis_appl_status] varchar(5)  NULL,
    [sis_appl_status_date] datetime  NULL,
    [sis_appl_stage] varchar(11)  NOT NULL,
    [sis_appl_offer_due_date] datetime  NULL,
    [sis_appl_parent_edu_level] varchar(10)  NULL,
    [sis_appl_modified_date] datetime  NULL,
    [sis_appl_date] datetime  NULL
);
GO

-- Creating table 'wv_colleague_persons'
CREATE TABLE [dbo].[wv_colleague_persons] (
    [ID] varchar(10)  NOT NULL,
    [LAST_NAME] varchar(57)  NULL,
    [FIRST_NAME] varchar(30)  NULL,
    [MIDDLE_NAME] varchar(30)  NULL,
    [BIRTH_DATE] datetime  NULL,
    [PREFERRED_NAME] varchar(300)  NULL,
    [PERSON_CHANGE_DATE] datetime  NULL,
    [PERSON_ADD_DATE] datetime  NULL,
    [BIRTH_NAME_LAST] varchar(57)  NULL,
    [BIRTH_NAME_FIRST] varchar(30)  NULL,
    [BIRTH_NAME_MIDDLE] varchar(30)  NULL,
    [PERSON_CORP_INDICATOR] varchar(1)  NULL,
    [PRIVACY_FLAG] varchar(10)  NULL,
    [PERSONAL_PRONOUN] varchar(10)  NULL,
    [PERSON_CHOSEN_FIRST_NAME] varchar(30)  NULL,
    [PERSON_CHOSEN_LAST_NAME] varchar(57)  NULL,
    [PERSON_CHOSEN_MIDDLE_NAME] varchar(30)  NULL
);
GO

-- Creating table 'wv_core_student_personal'
CREATE TABLE [dbo].[wv_core_student_personal] (
    [sis_student_id] varchar(10)  NULL,
    [sis_stu_first_name] varchar(200)  NULL,
    [sis_stu_last_name] varchar(200)  NULL,
    [sis_stu_middle_name] varchar(200)  NULL,
    [sis_stu_hist_last_name] varchar(200)  NULL,
    [sis_stu_birth_date] datetime  NULL,
    [sis_stu_gender] varchar(1)  NULL,
    [sis_stu_pref_email] varchar(50)  NULL,
    [sis_stu_marital_status] varchar(32)  NULL,
    [sis_stu_native_lang] varchar(32)  NULL,
    [sis_stu_alien_status] varchar(32)  NULL,
    [sis_stu_international_flag] int  NOT NULL,
    [sis_stu_duplicate_flag] int  NOT NULL,
    [sis_stu_privacy_flag] int  NOT NULL,
    [sis_stu_aboriginal] varchar(30)  NULL,
    [sis_stu_mod_date] datetime  NULL,
    [sis_stu_mod_time] datetime  NULL,
    [sis_stu_add_date] datetime  NULL,
    [sis_stu_add_time] datetime  NULL
);
GO

-- Creating table 'wv_core_student_restrictions'
CREATE TABLE [dbo].[wv_core_student_restrictions] (
    [sis_student_id] varchar(10)  NOT NULL,
    [sis_stu_restrict_mod_date] datetime  NULL
);
GO

-- Creating table 'wv_core_student_statuses'
CREATE TABLE [dbo].[wv_core_student_statuses] (
    [sis_student_id] varchar(10)  NOT NULL,
    [sis_stu_stage] varchar(9)  NOT NULL,
    [sis_app_status] varchar(5)  NOT NULL,
    [modified_date] datetime  NULL
);
GO

-- Creating table 'wv_core_student_terms'
CREATE TABLE [dbo].[wv_core_student_terms] (
    [sis_student_id] varchar(10)  NOT NULL,
    [sis_term_id] varchar(4)  NOT NULL,
    [sis_term_modified_date] datetime  NULL
);
GO

-- Creating table 'wv_course_connections'
CREATE TABLE [dbo].[wv_course_connections] (
    [course_offering_id] varchar(18)  NULL,
    [course_connection_contact_id] varchar(18)  NULL,
    [course_connection_program_enrollment_id] varchar(18)  NULL,
    [course_connection_program_id] varchar(18)  NULL,
    [course_connection_acad_program] varchar(40)  NULL,
    [course_connection_record_type] varchar(18)  NOT NULL,
    [course_connection_status] varchar(7)  NOT NULL,
    [course_connection_name] varchar(8000)  NULL,
    [contact_last_modified_datetime] datetime  NULL,
    [term_modified_datetime] datetime  NULL,
    [course_offering_modified_datetime] datetime  NULL,
    [sis_stu_course_mod_date] datetime  NULL
);
GO

-- Creating table 'wv_course_departments'
CREATE TABLE [dbo].[wv_course_departments] (
    [sis_course_id] varchar(10)  NOT NULL,
    [sis_course_department] varchar(5)  NULL,
    [RN] bigint  NULL
);
GO

-- Creating table 'wv_course_sections'
CREATE TABLE [dbo].[wv_course_sections] (
    [sis_course_section_id] varchar(19)  NOT NULL,
    [sis_cs_sec_short_title] varchar(30)  NULL,
    [sis_cs_sec_sched_type] varchar(5)  NULL,
    [sis_cs_sec_location] varchar(5)  NULL,
    [sis_cs_sec_min_enroll] decimal(5,0)  NULL,
    [sis_cs_sec_capacity] decimal(5,0)  NULL,
    [sis_cs_sec_subject] varchar(7)  NULL,
    [sis_cs_sec_min_cred] decimal(9,5)  NULL,
    [sis_cs_sec_reg_retake_policy] varchar(5)  NULL,
    [sis_cs_sec_only_pass_nopass_flag] int  NOT NULL,
    [sis_cs_sec_term] varchar(7)  NULL,
    [sis_cs_sec_course_no] varchar(7)  NULL,
    [sis_cs_sec_no] varchar(5)  NULL,
    [sis_cs_sec_billing_cred] decimal(9,5)  NULL,
    [sis_cs_sec_course] varchar(10)  NULL,
    [sis_cs_sec_petition_reqd_flag] int  NOT NULL,
    [sis_cs_sec_acad_level] varchar(5)  NULL,
    [sis_cs_sec_cred_type] varchar(5)  NULL,
    [sis_cs_sec_allow_pass_flag] int  NOT NULL,
    [sis_cs_sec_allow_audit_flag] int  NOT NULL,
    [sis_cs_sec_grade_scheme] varchar(5)  NULL,
    [sis_cs_sec_start_date] datetime  NULL,
    [sis_cs_sec_end_date] datetime  NULL,
    [sis_cs_sec_comments] varchar(max)  NULL,
    [sis_cs_sec_topic_code] varchar(5)  NULL,
    [sis_cs_sec_billing_period_type] varchar(10)  NULL,
    [sis_cs_sec_gl_no] varchar(30)  NULL,
    [sis_cs_sec_synonym] varchar(11)  NULL,
    [sis_cs_sec_alow_waitlist_flag] int  NOT NULL,
    [sis_cs_sec_billing_method] varchar(10)  NULL,
    [sis_cs_sec_drop_reg_ref_pol] varchar(10)  NULL,
    [sis_cs_sec_over_reg_start_date] datetime  NULL,
    [sis_cs_sec_ovr_reg_end_date] datetime  NULL,
    [sis_cs_sec_ovr_add_start_date] datetime  NULL,
    [sis_cs_sec_ovr_add_end_date] datetime  NULL,
    [sis_cs_sec_ovr_prereq_end_date] datetime  NULL,
    [sis_cs_sec_ovr_prereg_end_date] datetime  NULL,
    [sis_cs_sec_ovr_drop_end_date] datetime  NULL,
    [sis_cs_sec_transfer_status] varchar(10)  NULL,
    [sis_cs_sec_purpose] varchar(10)  NULL,
    [sis_cs_sec_faculty_consent_flag] int  NOT NULL,
    [sis_cs_sec_printed_comments] varchar(max)  NULL,
    [sis_cs_sec_ovr_drop_gr_reqd_date] datetime  NULL,
    [sis_cs_sec_name] varchar(21)  NULL,
    [sis_cs_sec_ovr_drop_start_date] datetime  NULL,
    [sis_cs_sec_no_weeks] decimal(3,0)  NULL,
    [sis_cs_sec_discount_max_amt] decimal(10,2)  NULL,
    [sis_cs_sec_cip_flag] int  NOT NULL,
    [sis_cs_sec_time_bill_flag] int  NOT NULL,
    [sis_cs_sec_user1] varchar(20)  NULL,
    [sis_cs_sec_r2_event] varchar(20)  NULL,
    [sis_cs_sec_special_property_flag] int  NOT NULL,
    [sis_cs_sec_first_meeting_date] datetime  NULL,
    [sis_cs_sec_last_meeting_date] datetime  NULL,
    [sis_cs_sec_meeting_info] varchar(180)  NULL,
    [sis_cs_sec_faculty_info] varchar(45)  NULL,
    [sis_cs_created_date] datetime  NULL,
    [sis_cs_modified_datetime] datetime  NULL,
    [sis_cs_sec_override_crs_reqs_flag] int  NOT NULL
);
GO

-- Creating table 'wv_courses'
CREATE TABLE [dbo].[wv_courses] (
    [sis_course_id] varchar(10)  NOT NULL,
    [sis_course_title] varchar(1996)  NULL,
    [sis_course_cred_type] varchar(5)  NULL,
    [sis_course_name] varchar(15)  NULL,
    [sis_course_desc] varchar(max)  NULL,
    [sis_course_short_title] varchar(30)  NULL,
    [sis_course_subject] varchar(7)  NULL,
    [sis_course_min_cred] decimal(9,5)  NULL,
    [sis_course_max_cred] decimal(9,5)  NULL,
    [sis_course_prereqs] varchar(20)  NULL,
    [sis_course_no] varchar(7)  NULL,
    [sis_course_cip] varchar(9)  NULL,
    [sis_course_retake_policy] varchar(5)  NULL,
    [sis_course_start_date] datetime  NULL,
    [sis_course_end_date] datetime  NULL,
    [sis_course_acad_level] varchar(5)  NULL,
    [sis_course_grade_scheme] varchar(5)  NULL,
    [sis_course_sched_type] varchar(5)  NULL,
    [sis_course_billing_period_type] varchar(10)  NULL,
    [sis_course_drop_reg_ref_pol] varchar(10)  NULL,
    [sis_course_billing_method] varchar(10)  NULL,
    [sis_course_comments] varchar(max)  NULL,
    [sis_course_billing_cred] decimal(9,5)  NULL,
    [sis_course_no_weeks] decimal(3,0)  NULL,
    [sis_course_transfer_status] varchar(10)  NULL,
    [sis_course_count_retake_cred_flag] int  NOT NULL,
    [sis_course_allow_audit_flag] int  NOT NULL,
    [sis_course_allow_waitlist_flag] int  NOT NULL,
    [sis_course_topic_code] varchar(5)  NULL,
    [sis_course_capacity] decimal(5,0)  NULL,
    [sis_course_min_enrol] decimal(5,0)  NULL,
    [sis_course_subject_idx] varchar(15)  NULL,
    [sis_waitlist_mult_sect_flag] int  NOT NULL,
    [sis_course_prtl_cc_update_datetime] datetime  NULL,
    [sis_course_created_date] datetime  NULL,
    [sis_course_modified_datetime] datetime  NULL
);
GO

-- Creating table 'wv_divisions'
CREATE TABLE [dbo].[wv_divisions] (
    [sis_division_id] varchar(5)  NOT NULL,
    [sis_division_desc] varchar(35)  NULL,
    [sis_division_head] varchar(10)  NULL,
    [sis_division_school] varchar(5)  NULL,
    [sis_division_created_date] datetime  NULL,
    [sis_division_created_by] varchar(20)  NULL,
    [sis_division_modified_by] varchar(20)  NULL,
    [sis_division_type] varchar(10)  NULL,
    [sis_division_department_code] varchar(5)  NULL,
    [sis_division_location] varchar(5)  NOT NULL,
    [sis_division_modified_datetime] datetime  NULL
);
GO

-- Creating table 'wv_institutions'
CREATE TABLE [dbo].[wv_institutions] (
    [sis_institution_id] varchar(10)  NOT NULL,
    [sis_inst_name] varchar(200)  NULL,
    [sis_inst_type] varchar(10)  NULL,
    [sis_inst_mod_date] datetime  NULL
);
GO

-- Creating table 'wv_name_history'
CREATE TABLE [dbo].[wv_name_history] (
    [sis_student_id] varchar(10)  NOT NULL,
    [sis_former_last_name] varchar(57)  NULL,
    [sis_former_first_name] varchar(57)  NULL,
    [sis_former_middle_name] varchar(30)  NULL
);
GO

-- Creating table 'wv_persons'
CREATE TABLE [dbo].[wv_persons] (
    [sis_person_id] varchar(10)  NOT NULL,
    [sis_person_last_name] varchar(57)  NULL,
    [sis_person_source] varchar(10)  NULL,
    [sis_person_first_name] varchar(30)  NULL,
    [sis_person_middle_name] varchar(30)  NULL,
    [sis_person_prefix] varchar(25)  NULL,
    [sis_person_preferred_address] varchar(10)  NULL,
    [sis_joint_person] varchar(10)  NULL,
    [sis_person_ssn] varchar(12)  NULL,
    [sis_person_status] varchar(10)  NULL,
    [sis_person_gender] varchar(1)  NULL,
    [sis_person_marital_status] varchar(10)  NULL,
    [sis_person_birth_date] datetime  NULL,
    [sis_person_preferred_name] varchar(300)  NULL,
    [sis_person_ethnic] varchar(5)  NULL,
    [sis_person_native_language] varchar(10)  NULL,
    [sis_person_birth_name_last] varchar(57)  NULL,
    [sis_person_birth_name_first] varchar(30)  NULL,
    [sis_person_birth_name_middle] varchar(30)  NULL,
    [sis_person_pref_residence] varchar(10)  NULL,
    [sis_person_bus_address] varchar(10)  NULL,
    [sis_person_country_entry_date] datetime  NULL,
    [sis_person_corp_flag] int  NOT NULL,
    [sis_person_residence_country] varchar(8)  NULL,
    [sis_person_origin_date] datetime  NULL,
    [sis_person_origin_code] varchar(10)  NULL,
    [sis_person_visa_type] varchar(20)  NULL,
    [sis_person_citizenship] varchar(8)  NULL,
    [sis_emer_contact_name] varchar(30)  NULL,
    [sis_emer_contact_phone] varchar(20)  NULL,
    [sis_person_directory_flag] int  NOT NULL,
    [sis_person_privacy_flag] int  NOT NULL,
    [sis_person_primary_language] varchar(20)  NULL,
    [sis_person_gender_identity] varchar(10)  NULL,
    [sis_person_pronoun] varchar(10)  NULL,
    [sis_person_chosen_first_name] varchar(30)  NULL,
    [sis_person_chosen_last_name] varchar(57)  NULL,
    [sis_person_chosen_middle_name] varchar(30)  NULL,
    [sis_person_jati_id] varchar(50)  NULL,
    [sis_person_sexual_orientation] varchar(10)  NULL,
    [sis_person_created_datetime] datetime  NULL,
    [sis_person_modified_datetime] datetime  NULL
);
GO

-- Creating table 'wv_program_enrollments'
CREATE TABLE [dbo].[wv_program_enrollments] (
    [sis_student_id] varchar(7)  NULL,
    [sis_student_program] varchar(32)  NULL,
    [sis_student_program_start_date] datetime  NULL,
    [sis_student_program_end_date] datetime  NULL,
    [sis_student_program_admit_status] varchar(5)  NOT NULL,
    [sis_student_program_location] varchar(5)  NOT NULL,
    [sis_student_program_catalog] varchar(5)  NULL,
    [sis_student_program_department] varchar(5)  NULL,
    [sis_student_program_division] varchar(5)  NULL,
    [sis_student_program_school] varchar(5)  NULL,
    [sis_student_program_comments] varchar(max)  NULL,
    [sis_student_program_eval_include_code] varchar(10)  NULL,
    [sis_student_program_printed_comments] varchar(max)  NULL,
    [sis_student_program_eval_date] datetime  NULL,
    [sis_student_program_eval_status] varchar(10)  NULL,
    [sis_student_program_combined_cred] decimal(9,5)  NULL,
    [sis_student_program_combined_gpa] decimal(8,5)  NULL,
    [sis_student_program_inst_cred] decimal(9,5)  NULL,
    [sis_student_program_inst_gpa] decimal(8,5)  NULL,
    [sis_student_program_acad_cred_end_date] datetime  NULL,
    [sis_student_program_ant_cmpl_date] datetime  NULL,
    [sis_student_program_created_date] datetime  NULL,
    [sis_student_program_modified_date] datetime  NULL,
    [unique] bigint  NULL,
    [sis_student_program_order] bigint  NULL
);
GO

-- Creating table 'wv_programs'
CREATE TABLE [dbo].[wv_programs] (
    [sis_program_id] varchar(20)  NOT NULL,
    [sis_program_desc] varchar(max)  NULL,
    [sis_program_title] varchar(60)  NULL,
    [sis_program_acad_level] varchar(5)  NULL,
    [sis_program_stu_select_flag] int  NOT NULL,
    [sis_program_active_flag] int  NOT NULL,
    [sis_program_start_date] datetime  NULL,
    [sis_program_end_date] datetime  NULL,
    [sis_program_comments] varchar(max)  NULL,
    [sis_program_allow_grad_flag] int  NOT NULL,
    [sis_program_grade_scheme] varchar(5)  NULL,
    [sis_program_transcript_grouping] varchar(5)  NULL,
    [sis_program_create_application_flag] int  NOT NULL,
    [sis_program_admit_capacity] decimal(6,0)  NULL,
    [sis_program_catalogs] varchar(5)  NULL,
    [sis_program_ccds] varchar(5)  NULL,
    [sis_program_locations] varchar(5)  NULL,
    [sis_program_departments] varchar(5)  NULL,
    [sis_program_gov_codes] varchar(15)  NULL,
    [sis_program_created_date] datetime  NULL,
    [sis_program_modified_datetime] datetime  NULL
);
GO

-- Creating table 'wv_schools'
CREATE TABLE [dbo].[wv_schools] (
    [sis_school_id] varchar(5)  NOT NULL,
    [sis_school_desc] varchar(30)  NULL,
    [sis_school_head_id] varchar(10)  NULL,
    [sis_school_created_by] varchar(20)  NULL,
    [sis_school_created_date] datetime  NULL,
    [sis_school_modified_by] varchar(20)  NULL,
    [sis_school_modified_date] datetime  NULL,
    [sis_school_location_code] varchar(5)  NOT NULL,
    [sis_division_code] varchar(5)  NULL
);
GO

-- Creating table 'wv_student_applications'
CREATE TABLE [dbo].[wv_student_applications] (
    [sis_application_id] varchar(10)  NOT NULL,
    [crm_contact_id] varchar(18)  NULL,
    [sis_student_id] varchar(10)  NULL,
    [crm_program_id] varchar(18)  NULL,
    [sis_appl_location] varchar(5)  NULL,
    [sis_appl_start_year] int  NULL,
    [sis_appl_start_term] varchar(18)  NULL,
    [sis_appl_stu_load_intent] varchar(10)  NULL,
    [sis_appl_offer_due_date] datetime  NULL,
    [sis_alt_status_datetime] datetime  NULL,
    [sis_app_status_datetime] datetime  NULL,
    [sis_con_status_datetime] datetime  NULL,
    [sis_dtc_status_datetime] datetime  NULL,
    [sis_dac_status_datetime] datetime  NULL,
    [sis_fi_status_datetime] datetime  NULL,
    [sis_fw_status_datetime] datetime  NULL,
    [sis_ms_status_datetime] datetime  NULL,
    [sis_ntq_status_datetime] datetime  NULL,
    [sis_ofi_status_datetime] datetime  NULL,
    [sis_ofc_status_datetime] datetime  NULL,
    [sis_pas_status_datetime] datetime  NULL,
    [sis_par_status_datetime] datetime  NULL,
    [sis_ppr_status_datetime] datetime  NULL,
    [sis_w_status_datetime] datetime  NULL,
    [sis_pr_status_datetime] datetime  NULL,
    [sis_sc_status_datetime] datetime  NULL,
    [sis_unc_status_datetime] datetime  NULL,
    [sis_wtl_status_datetime] datetime  NULL,
    [sis_wap_status_datetime] datetime  NULL,
    [sis_appl_parent_edu_level] varchar(10)  NULL,
    [sis_appl_admit_status] varchar(5)  NULL,
    [sis_appl_status] varchar(5)  NULL,
    [sis_appl_status_date] datetime  NULL,
    [sis_appl_stage] varchar(11)  NULL,
    [sis_appl_mark_delete] bit  NOT NULL,
    [sis_appl_date] datetime  NULL,
    [sis_appl_modified_date] datetime  NULL,
    [RN] bigint  NULL
);
GO

-- Creating table 'wv_student_course_sections'
CREATE TABLE [dbo].[wv_student_course_sections] (
    [sis_scs_id] varchar(10)  NOT NULL,
    [sis_cs_id] varchar(19)  NULL,
    [sis_stc_id] varchar(10)  NULL,
    [sis_student_id] varchar(10)  NOT NULL,
    [sis_scs_reg_method] varchar(10)  NULL,
    [sis_scs_location] varchar(5)  NULL,
    [sis_scs_ar_posted_flag] bit  NULL,
    [sis_scs_reg_time_period] varchar(10)  NULL,
    [sis_scs_created_date] datetime  NULL,
    [sis_scs_modified_date] datetime  NULL
);
GO

-- Creating table 'wv_student_programs'
CREATE TABLE [dbo].[wv_student_programs] (
    [sis_stu_program_id] varchar(8000)  NULL,
    [sis_student_id] varchar(7)  NULL,
    [sis_stu_program_code] varchar(32)  NULL,
    [sis_stu_program_location] varchar(5)  NOT NULL,
    [sis_stu_program_admit_status] varchar(5)  NOT NULL,
    [sis_stu_program_created_date] datetime  NULL,
    [sis_stu_program_modified_date] datetime  NULL,
    [sis_stu_program_status] varchar(10)  NULL,
    [sis_stu_program_status_date] datetime  NULL,
    [sis_stu_program_start_date] datetime  NULL,
    [sis_stu_program_end_date] datetime  NULL,
    [RowNum] bigint  NULL
);
GO

-- Creating table 'wv_student_terms'
CREATE TABLE [dbo].[wv_student_terms] (
    [sis_sttr_id] varchar(24)  NOT NULL,
    [sis_student_id] varchar(8)  NULL,
    [sis_term_id] varchar(4)  NULL,
    [sis_sttr_type] varchar(24)  NULL,
    [sis_sttr_intent_id] varchar(10)  NULL,
    [sis_sttr_reg_date] datetime  NULL,
    [sis_sttr_prereg_date] datetime  NULL,
    [sis_sttr_printed_comments] varchar(max)  NULL,
    [sis_sttr_cred_limit_waive_flag] int  NOT NULL,
    [sis_sttr_rehab_dept_client_flag] int  NOT NULL,
    [sis_sttr_tech_prep_flag] int  NOT NULL,
    [sis_stls_course_sec] varchar(10)  NULL,
    [stls_schedule] varchar(10)  NULL,
    [stls_student_acad_cred] varchar(10)  NULL,
    [stcc_term_gpa] decimal(7,5)  NULL,
    [stcc_term_gpa_flag] int  NOT NULL,
    [stcc_trgr_cum_gpa] decimal(7,5)  NULL,
    [stcc_trgr_cum_gpa_flag] int  NOT NULL,
    [stcc_cc_created_date] datetime  NULL,
    [stcc_modified_date] datetime  NULL,
    [sttr_created_date] datetime  NULL,
    [sttr_modified_date] datetime  NULL,
    [RN] bigint  NULL
);
GO

-- Creating table 'wv_terms'
CREATE TABLE [dbo].[wv_terms] (
    [sis_term_id] varchar(7)  NOT NULL,
    [sis_term_name] varchar(30)  NULL,
    [sis_term_desc] varchar(30)  NULL,
    [sis_reporting_year] decimal(4,0)  NULL,
    [sis_term_year] varchar(4)  NULL,
    [sis_term_prereg_start_date] datetime  NULL,
    [sis_term_prereg_end_date] datetime  NULL,
    [sis_term_reg_start_date] datetime  NULL,
    [sis_term_reg_end_date] datetime  NULL,
    [sis_term_start_date] datetime  NULL,
    [sis_term_end_date] datetime  NULL,
    [sis_term_reporting_term] varchar(7)  NULL,
    [sis_term_add_start_date] datetime  NULL,
    [sis_term_add_end_date] datetime  NULL,
    [sis_term_drop_grad_reqd_date] datetime  NULL,
    [sis_term_drop_start_date] datetime  NULL,
    [sis_term_drop_end_date] datetime  NULL,
    [sis_term_sequence_no] decimal(3,0)  NULL,
    [sis_term_created_by] varchar(8000)  NULL,
    [sis_term_created_date] datetime  NULL,
    [sis_term_modified_by] varchar(8000)  NULL,
    [sis_term_modified_date] datetime  NULL,
    [sis_term_bill_end_date] datetime  NULL,
    [sis_term_use_in_best_fit_calc] int  NOT NULL,
    [sis_term_census_dates] datetime  NULL
);
GO

-- Creating table 'crm_form_submissions'
CREATE TABLE [dbo].[crm_form_submissions] (
    [crm_form_guid] uniqueidentifier  NOT NULL,
    [crm_cookie_uuid] uniqueidentifier  NULL,
    [crm_form_source] varchar(80)  NULL,
    [crm_form_firstname] varchar(200)  NULL,
    [crm_form_lastname] varchar(200)  NULL,
    [crm_form_emailaddress] varchar(250)  NULL,
    [crm_form_birthdate] varchar(50)  NULL,
    [crm_form_created_datetime] datetime  NULL,
    [crm_form_googleclientId] varchar(255)  NULL,
    [crm_form_processed_datetime] datetime  NULL,
    [crm_form_process_skip] bit  NOT NULL,
    [crm_form_contact_id] varchar(18)  NULL,
    [crm_form_ack_sent] datetime  NULL,
    [crm_form_mobile_phone] varchar(10)  NULL,
    [crm_form_city] varchar(50)  NULL,
    [crm_form_province] varchar(80)  NULL,
    [crm_form_term] varchar(18)  NULL,
    [crm_form_program] varchar(18)  NULL,
    [crm_form_type] varchar(20)  NULL,
    [crm_form_inquiry_id] varchar(18)  NULL,
    [crm_form_indigenous] bit  NULL,
    [crm_form_prior_status] varchar(80)  NULL,
    [crm_form_sfad_interest] bit  NULL,
    [crm_form_ct_interest] bit  NULL,
    [crm_form_athletics_interest] bit  NULL,
    [crm_form_accessibility_interest] bit  NULL,
    [crm_form_wellness_interest] bit  NULL,
    [crm_form_awards_interest] bit  NULL,
    [crm_form_residence_interest] bit  NULL
);
GO

-- Creating table 'crm_dynamic_content'
CREATE TABLE [dbo].[crm_dynamic_content] (
    [crm_dynamic_content_guid] uniqueidentifier  NOT NULL,
    [crm_dynamic_content_id] varchar(18)  NULL,
    [crm_dynamic_content_label] varchar(255)  NULL,
    [crm_dynamic_content_number] varchar(80)  NULL,
    [crm_dynamic_content_name] varchar(255)  NULL,
    [crm_dynamic_content_available] bit  NOT NULL,
    [crm_dynamic_content_html] varchar(max)  NULL,
    [crm_dynamic_content_text] varchar(max)  NULL,
    [crm_dynamic_content_last_modified_datetime] datetime  NULL,
    [crm_dynamic_content_recordtype_id] varchar(18)  NULL,
    [crm_dynamic_content_created_datetime] datetime  NULL,
    [crm_dynamic_content_mark_delete] bit  NOT NULL,
    [crm_dynamic_content_deleted] bit  NOT NULL,
    [crm_dynamic_content_last_modified_by] varchar(18)  NULL,
    [crm_dynamic_content_created_by] varchar(18)  NULL,
    [last_sfsync_datetime] datetime  NULL,
    [crm_dynamic_content_matching_text] varchar(255)  NULL
);
GO

-- Creating table 'crm_cookie_history'
CREATE TABLE [dbo].[crm_cookie_history] (
    [crm_cookie_history_uuid] uniqueidentifier  NOT NULL,
    [crm_cookie_uuid] uniqueidentifier  NULL,
    [crm_page_visited] varchar(255)  NULL,
    [crm_created_datetime] datetime  NULL,
    [crm_googleclientId] varchar(255)  NULL
);
GO

-- Creating table 'crm_settings'
CREATE TABLE [dbo].[crm_settings] (
    [crm_setting_guid] uniqueidentifier  NOT NULL,
    [crm_setting_name] varchar(255)  NOT NULL,
    [crm_setting_number] int  NULL,
    [crm_setting_type] int  NULL,
    [crm_setting_description] varchar(500)  NULL,
    [crm_setting_bool] bit  NOT NULL,
    [crm_setting_string] varchar(255)  NULL,
    [crm_setting_int] int  NULL,
    [crm_setting_datetime] datetime  NULL
);
GO

-- Creating table 'crm_inquiry_programs'
CREATE TABLE [dbo].[crm_inquiry_programs] (
    [crm_inq_prog_guid] uniqueidentifier  NOT NULL,
    [crm_inq_prog_id] varchar(18)  NULL,
    [crm_inq_prog_number] varchar(20)  NULL,
    [crm_inq_prog_inquiry_id] varchar(18)  NULL,
    [crm_inq_prog_program_id] varchar(18)  NULL,
    [crm_inq_prog_created_by] varchar(18)  NULL,
    [crm_inq_prog_created_datetime] datetime  NOT NULL,
    [last_sfsync_datetime] datetime  NULL,
    [crm_inq_prog_deleted] bit  NOT NULL,
    [crm_inq_prog_mark_delete] bit  NOT NULL,
    [crm_inq_prog_last_modified_by] varchar(18)  NULL,
    [crm_inq_prog_last_modified_datetime] datetime  NOT NULL,
    [crm_inq_prog_ack_sent] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [account_guid] in table 'crm_accounts'
ALTER TABLE [dbo].[crm_accounts]
ADD CONSTRAINT [PK_crm_accounts]
    PRIMARY KEY CLUSTERED ([account_guid] ASC);
GO

-- Creating primary key on [activity_id] in table 'crm_activities'
ALTER TABLE [dbo].[crm_activities]
ADD CONSTRAINT [PK_crm_activities]
    PRIMARY KEY CLUSTERED ([activity_id] ASC);
GO

-- Creating primary key on [activity_extender_guid] in table 'crm_activity_extender'
ALTER TABLE [dbo].[crm_activity_extender]
ADD CONSTRAINT [PK_crm_activity_extender]
    PRIMARY KEY CLUSTERED ([activity_extender_guid] ASC);
GO

-- Creating primary key on [affiliation_guid] in table 'crm_affiliations'
ALTER TABLE [dbo].[crm_affiliations]
ADD CONSTRAINT [PK_crm_affiliations]
    PRIMARY KEY CLUSTERED ([affiliation_guid] ASC);
GO

-- Creating primary key on [application_guid] in table 'crm_applications'
ALTER TABLE [dbo].[crm_applications]
ADD CONSTRAINT [PK_crm_applications]
    PRIMARY KEY CLUSTERED ([application_guid] ASC);
GO

-- Creating primary key on [asset_guid] in table 'crm_assets'
ALTER TABLE [dbo].[crm_assets]
ADD CONSTRAINT [PK_crm_assets]
    PRIMARY KEY CLUSTERED ([asset_guid] ASC);
GO

-- Creating primary key on [campaign_member_guid] in table 'crm_campaign_members'
ALTER TABLE [dbo].[crm_campaign_members]
ADD CONSTRAINT [PK_crm_campaign_members]
    PRIMARY KEY CLUSTERED ([campaign_member_guid] ASC);
GO

-- Creating primary key on [campaign_guid] in table 'crm_campaigns'
ALTER TABLE [dbo].[crm_campaigns]
ADD CONSTRAINT [PK_crm_campaigns]
    PRIMARY KEY CLUSTERED ([campaign_guid] ASC);
GO

-- Creating primary key on [case_guid] in table 'crm_cases'
ALTER TABLE [dbo].[crm_cases]
ADD CONSTRAINT [PK_crm_cases]
    PRIMARY KEY CLUSTERED ([case_guid] ASC);
GO

-- Creating primary key on [contact_guid] in table 'crm_contacts'
ALTER TABLE [dbo].[crm_contacts]
ADD CONSTRAINT [PK_crm_contacts]
    PRIMARY KEY CLUSTERED ([contact_guid] ASC);
GO

-- Creating primary key on [course_connection_guid] in table 'crm_course_connections'
ALTER TABLE [dbo].[crm_course_connections]
ADD CONSTRAINT [PK_crm_course_connections]
    PRIMARY KEY CLUSTERED ([course_connection_guid] ASC);
GO

-- Creating primary key on [course_offering_guid] in table 'crm_course_offerings'
ALTER TABLE [dbo].[crm_course_offerings]
ADD CONSTRAINT [PK_crm_course_offerings]
    PRIMARY KEY CLUSTERED ([course_offering_guid] ASC);
GO

-- Creating primary key on [course_guid] in table 'crm_courses'
ALTER TABLE [dbo].[crm_courses]
ADD CONSTRAINT [PK_crm_courses]
    PRIMARY KEY CLUSTERED ([course_guid] ASC);
GO

-- Creating primary key on [email_broadcast_guid] in table 'crm_email_broadcasts'
ALTER TABLE [dbo].[crm_email_broadcasts]
ADD CONSTRAINT [PK_crm_email_broadcasts]
    PRIMARY KEY CLUSTERED ([email_broadcast_guid] ASC);
GO

-- Creating primary key on [email_campaign_guid] in table 'crm_email_campaigns'
ALTER TABLE [dbo].[crm_email_campaigns]
ADD CONSTRAINT [PK_crm_email_campaigns]
    PRIMARY KEY CLUSTERED ([email_campaign_guid] ASC);
GO

-- Creating primary key on [email_message_guid] in table 'crm_email_messages'
ALTER TABLE [dbo].[crm_email_messages]
ADD CONSTRAINT [PK_crm_email_messages]
    PRIMARY KEY CLUSTERED ([email_message_guid] ASC);
GO

-- Creating primary key on [email_template_guid] in table 'crm_email_templates'
ALTER TABLE [dbo].[crm_email_templates]
ADD CONSTRAINT [PK_crm_email_templates]
    PRIMARY KEY CLUSTERED ([email_template_guid] ASC);
GO

-- Creating primary key on [event_registration_guid] in table 'crm_event_registrations'
ALTER TABLE [dbo].[crm_event_registrations]
ADD CONSTRAINT [PK_crm_event_registrations]
    PRIMARY KEY CLUSTERED ([event_registration_guid] ASC);
GO

-- Creating primary key on [activity_guid] in table 'crm_events'
ALTER TABLE [dbo].[crm_events]
ADD CONSTRAINT [PK_crm_events]
    PRIMARY KEY CLUSTERED ([activity_guid] ASC);
GO

-- Creating primary key on [inquiry_guid] in table 'crm_inquiries'
ALTER TABLE [dbo].[crm_inquiries]
ADD CONSTRAINT [PK_crm_inquiries]
    PRIMARY KEY CLUSTERED ([inquiry_guid] ASC);
GO

-- Creating primary key on [lead_guid] in table 'crm_leads'
ALTER TABLE [dbo].[crm_leads]
ADD CONSTRAINT [PK_crm_leads]
    PRIMARY KEY CLUSTERED ([lead_guid] ASC);
GO

-- Creating primary key on [po_course_code] in table 'crm_po_course_codes'
ALTER TABLE [dbo].[crm_po_course_codes]
ADD CONSTRAINT [PK_crm_po_course_codes]
    PRIMARY KEY CLUSTERED ([po_course_code] ASC);
GO

-- Creating primary key on [crm_program_enrollment_guid] in table 'crm_program_enrollments'
ALTER TABLE [dbo].[crm_program_enrollments]
ADD CONSTRAINT [PK_crm_program_enrollments]
    PRIMARY KEY CLUSTERED ([crm_program_enrollment_guid] ASC);
GO

-- Creating primary key on [report_guid] in table 'crm_reports'
ALTER TABLE [dbo].[crm_reports]
ADD CONSTRAINT [PK_crm_reports]
    PRIMARY KEY CLUSTERED ([report_guid] ASC);
GO

-- Creating primary key on [crm_role_value_guid] in table 'crm_role_values'
ALTER TABLE [dbo].[crm_role_values]
ADD CONSTRAINT [PK_crm_role_values]
    PRIMARY KEY CLUSTERED ([crm_role_value_guid] ASC);
GO

-- Creating primary key on [crm_scanner_registration_guid] in table 'crm_scanner_registrations'
ALTER TABLE [dbo].[crm_scanner_registrations]
ADD CONSTRAINT [PK_crm_scanner_registrations]
    PRIMARY KEY CLUSTERED ([crm_scanner_registration_guid] ASC);
GO

-- Creating primary key on [activity_guid] in table 'crm_tasks'
ALTER TABLE [dbo].[crm_tasks]
ADD CONSTRAINT [PK_crm_tasks]
    PRIMARY KEY CLUSTERED ([activity_guid] ASC);
GO

-- Creating primary key on [term_guid] in table 'crm_terms'
ALTER TABLE [dbo].[crm_terms]
ADD CONSTRAINT [PK_crm_terms]
    PRIMARY KEY CLUSTERED ([term_guid] ASC);
GO

-- Creating primary key on [dynamic_content_id] in table 'dynamic_content'
ALTER TABLE [dbo].[dynamic_content]
ADD CONSTRAINT [PK_dynamic_content]
    PRIMARY KEY CLUSTERED ([dynamic_content_id] ASC);
GO

-- Creating primary key on [sis_student_id], [lcc2_barcode_number] in table 'lcc2_barcodes'
ALTER TABLE [dbo].[lcc2_barcodes]
ADD CONSTRAINT [PK_lcc2_barcodes]
    PRIMARY KEY CLUSTERED ([sis_student_id], [lcc2_barcode_number] ASC);
GO

-- Creating primary key on [merge_field_id] in table 'merge_fields'
ALTER TABLE [dbo].[merge_fields]
ADD CONSTRAINT [PK_merge_fields]
    PRIMARY KEY CLUSTERED ([merge_field_id] ASC);
GO

-- Creating primary key on [soql_query_id] in table 'soql_queries'
ALTER TABLE [dbo].[soql_queries]
ADD CONSTRAINT [PK_soql_queries]
    PRIMARY KEY CLUSTERED ([soql_query_id] ASC);
GO

-- Creating primary key on [transaction_error_id] in table 'transaction_errors'
ALTER TABLE [dbo].[transaction_errors]
ADD CONSTRAINT [PK_transaction_errors]
    PRIMARY KEY CLUSTERED ([transaction_error_id] ASC);
GO

-- Creating primary key on [transaction_load_guid] in table 'transaction_loads'
ALTER TABLE [dbo].[transaction_loads]
ADD CONSTRAINT [PK_transaction_loads]
    PRIMARY KEY CLUSTERED ([transaction_load_guid] ASC);
GO

-- Creating primary key on [transaction_id] in table 'transaction_logs'
ALTER TABLE [dbo].[transaction_logs]
ADD CONSTRAINT [PK_transaction_logs]
    PRIMARY KEY CLUSTERED ([transaction_id] ASC);
GO

-- Creating primary key on [sis_address_id] in table 'wt_addresses'
ALTER TABLE [dbo].[wt_addresses]
ADD CONSTRAINT [PK_wt_addresses]
    PRIMARY KEY CLUSTERED ([sis_address_id] ASC);
GO

-- Creating primary key on [sis_applicant_id] in table 'wt_applicants'
ALTER TABLE [dbo].[wt_applicants]
ADD CONSTRAINT [PK_wt_applicants]
    PRIMARY KEY CLUSTERED ([sis_applicant_id] ASC);
GO

-- Creating primary key on [sis_application_id] in table 'wt_application_statuses'
ALTER TABLE [dbo].[wt_application_statuses]
ADD CONSTRAINT [PK_wt_application_statuses]
    PRIMARY KEY CLUSTERED ([sis_application_id] ASC);
GO

-- Creating primary key on [sis_application_id] in table 'wt_applications'
ALTER TABLE [dbo].[wt_applications]
ADD CONSTRAINT [PK_wt_applications]
    PRIMARY KEY CLUSTERED ([sis_application_id] ASC);
GO

-- Creating primary key on [sis_student_id] in table 'wt_core_students'
ALTER TABLE [dbo].[wt_core_students]
ADD CONSTRAINT [PK_wt_core_students]
    PRIMARY KEY CLUSTERED ([sis_student_id] ASC);
GO

-- Creating primary key on [sis_course_id], [sis_course_department] in table 'wt_course_departments'
ALTER TABLE [dbo].[wt_course_departments]
ADD CONSTRAINT [PK_wt_course_departments]
    PRIMARY KEY CLUSTERED ([sis_course_id], [sis_course_department] ASC);
GO

-- Creating primary key on [sis_course_section_id] in table 'wt_course_sections'
ALTER TABLE [dbo].[wt_course_sections]
ADD CONSTRAINT [PK_wt_course_sections]
    PRIMARY KEY CLUSTERED ([sis_course_section_id] ASC);
GO

-- Creating primary key on [sis_course_id] in table 'wt_courses'
ALTER TABLE [dbo].[wt_courses]
ADD CONSTRAINT [PK_wt_courses]
    PRIMARY KEY CLUSTERED ([sis_course_id] ASC);
GO

-- Creating primary key on [sis_department_id] in table 'wt_departments'
ALTER TABLE [dbo].[wt_departments]
ADD CONSTRAINT [PK_wt_departments]
    PRIMARY KEY CLUSTERED ([sis_department_id] ASC);
GO

-- Creating primary key on [sis_division_id] in table 'wt_divisions'
ALTER TABLE [dbo].[wt_divisions]
ADD CONSTRAINT [PK_wt_divisions]
    PRIMARY KEY CLUSTERED ([sis_division_id] ASC);
GO

-- Creating primary key on [sis_institution_id] in table 'wt_institutions'
ALTER TABLE [dbo].[wt_institutions]
ADD CONSTRAINT [PK_wt_institutions]
    PRIMARY KEY CLUSTERED ([sis_institution_id] ASC);
GO

-- Creating primary key on [crm_contact_id] in table 'wt_off_email_restore'
ALTER TABLE [dbo].[wt_off_email_restore]
ADD CONSTRAINT [PK_wt_off_email_restore]
    PRIMARY KEY CLUSTERED ([crm_contact_id] ASC);
GO

-- Creating primary key on [crm_program_enrollment_student_id], [crm_program_enrollment_program_id], [crm_program_enrollment_start_date] in table 'wt_program_enrollments'
ALTER TABLE [dbo].[wt_program_enrollments]
ADD CONSTRAINT [PK_wt_program_enrollments]
    PRIMARY KEY CLUSTERED ([crm_program_enrollment_student_id], [crm_program_enrollment_program_id], [crm_program_enrollment_start_date] ASC);
GO

-- Creating primary key on [sis_program_id] in table 'wt_programs'
ALTER TABLE [dbo].[wt_programs]
ADD CONSTRAINT [PK_wt_programs]
    PRIMARY KEY CLUSTERED ([sis_program_id] ASC);
GO

-- Creating primary key on [sis_scs_id] in table 'wt_student_course_sections'
ALTER TABLE [dbo].[wt_student_course_sections]
ADD CONSTRAINT [PK_wt_student_course_sections]
    PRIMARY KEY CLUSTERED ([sis_scs_id] ASC);
GO

-- Creating primary key on [sis_stu_course_id], [sis_student_id], [sis_stu_course_term] in table 'wt_student_courses'
ALTER TABLE [dbo].[wt_student_courses]
ADD CONSTRAINT [PK_wt_student_courses]
    PRIMARY KEY CLUSTERED ([sis_stu_course_id], [sis_student_id], [sis_stu_course_term] ASC);
GO

-- Creating primary key on [sis_student_id], [crm_program_id] in table 'wt_student_credentials'
ALTER TABLE [dbo].[wt_student_credentials]
ADD CONSTRAINT [PK_wt_student_credentials]
    PRIMARY KEY CLUSTERED ([sis_student_id], [crm_program_id] ASC);
GO

-- Creating primary key on [sis_stu_program_id] in table 'wt_student_programs'
ALTER TABLE [dbo].[wt_student_programs]
ADD CONSTRAINT [PK_wt_student_programs]
    PRIMARY KEY CLUSTERED ([sis_stu_program_id] ASC);
GO

-- Creating primary key on [sis_sttr_id] in table 'wt_student_terms'
ALTER TABLE [dbo].[wt_student_terms]
ADD CONSTRAINT [PK_wt_student_terms]
    PRIMARY KEY CLUSTERED ([sis_sttr_id] ASC);
GO

-- Creating primary key on [sis_term_id] in table 'wt_terms'
ALTER TABLE [dbo].[wt_terms]
ADD CONSTRAINT [PK_wt_terms]
    PRIMARY KEY CLUSTERED ([sis_term_id] ASC);
GO

-- Creating primary key on [sis_student_id] in table 'sv_core_student_duplicates'
ALTER TABLE [dbo].[sv_core_student_duplicates]
ADD CONSTRAINT [PK_sv_core_student_duplicates]
    PRIMARY KEY CLUSTERED ([sis_student_id] ASC);
GO

-- Creating primary key on [sis_app_start_session], [sis_app_stage] in table 'wv_applicants'
ALTER TABLE [dbo].[wv_applicants]
ADD CONSTRAINT [PK_wv_applicants]
    PRIMARY KEY CLUSTERED ([sis_app_start_session], [sis_app_stage] ASC);
GO

-- Creating primary key on [sis_application_id] in table 'wv_application_statuses'
ALTER TABLE [dbo].[wv_application_statuses]
ADD CONSTRAINT [PK_wv_application_statuses]
    PRIMARY KEY CLUSTERED ([sis_application_id] ASC);
GO

-- Creating primary key on [sis_application_id], [sis_appl_location], [sis_appl_start_session], [sis_appl_fa_intent_flag], [sis_appl_active_stu_program_flag], [sis_appl_stage] in table 'wv_applications'
ALTER TABLE [dbo].[wv_applications]
ADD CONSTRAINT [PK_wv_applications]
    PRIMARY KEY CLUSTERED ([sis_application_id], [sis_appl_location], [sis_appl_start_session], [sis_appl_fa_intent_flag], [sis_appl_active_stu_program_flag], [sis_appl_stage] ASC);
GO

-- Creating primary key on [ID] in table 'wv_colleague_persons'
ALTER TABLE [dbo].[wv_colleague_persons]
ADD CONSTRAINT [PK_wv_colleague_persons]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [sis_stu_international_flag], [sis_stu_duplicate_flag], [sis_stu_privacy_flag] in table 'wv_core_student_personal'
ALTER TABLE [dbo].[wv_core_student_personal]
ADD CONSTRAINT [PK_wv_core_student_personal]
    PRIMARY KEY CLUSTERED ([sis_stu_international_flag], [sis_stu_duplicate_flag], [sis_stu_privacy_flag] ASC);
GO

-- Creating primary key on [sis_student_id] in table 'wv_core_student_restrictions'
ALTER TABLE [dbo].[wv_core_student_restrictions]
ADD CONSTRAINT [PK_wv_core_student_restrictions]
    PRIMARY KEY CLUSTERED ([sis_student_id] ASC);
GO

-- Creating primary key on [sis_student_id], [sis_stu_stage], [sis_app_status] in table 'wv_core_student_statuses'
ALTER TABLE [dbo].[wv_core_student_statuses]
ADD CONSTRAINT [PK_wv_core_student_statuses]
    PRIMARY KEY CLUSTERED ([sis_student_id], [sis_stu_stage], [sis_app_status] ASC);
GO

-- Creating primary key on [sis_student_id], [sis_term_id] in table 'wv_core_student_terms'
ALTER TABLE [dbo].[wv_core_student_terms]
ADD CONSTRAINT [PK_wv_core_student_terms]
    PRIMARY KEY CLUSTERED ([sis_student_id], [sis_term_id] ASC);
GO

-- Creating primary key on [course_connection_record_type], [course_connection_status] in table 'wv_course_connections'
ALTER TABLE [dbo].[wv_course_connections]
ADD CONSTRAINT [PK_wv_course_connections]
    PRIMARY KEY CLUSTERED ([course_connection_record_type], [course_connection_status] ASC);
GO

-- Creating primary key on [sis_course_id] in table 'wv_course_departments'
ALTER TABLE [dbo].[wv_course_departments]
ADD CONSTRAINT [PK_wv_course_departments]
    PRIMARY KEY CLUSTERED ([sis_course_id] ASC);
GO

-- Creating primary key on [sis_course_section_id], [sis_cs_sec_only_pass_nopass_flag], [sis_cs_sec_petition_reqd_flag], [sis_cs_sec_allow_pass_flag], [sis_cs_sec_allow_audit_flag], [sis_cs_sec_alow_waitlist_flag], [sis_cs_sec_faculty_consent_flag], [sis_cs_sec_cip_flag], [sis_cs_sec_time_bill_flag], [sis_cs_sec_special_property_flag], [sis_cs_sec_override_crs_reqs_flag] in table 'wv_course_sections'
ALTER TABLE [dbo].[wv_course_sections]
ADD CONSTRAINT [PK_wv_course_sections]
    PRIMARY KEY CLUSTERED ([sis_course_section_id], [sis_cs_sec_only_pass_nopass_flag], [sis_cs_sec_petition_reqd_flag], [sis_cs_sec_allow_pass_flag], [sis_cs_sec_allow_audit_flag], [sis_cs_sec_alow_waitlist_flag], [sis_cs_sec_faculty_consent_flag], [sis_cs_sec_cip_flag], [sis_cs_sec_time_bill_flag], [sis_cs_sec_special_property_flag], [sis_cs_sec_override_crs_reqs_flag] ASC);
GO

-- Creating primary key on [sis_course_id], [sis_course_count_retake_cred_flag], [sis_course_allow_audit_flag], [sis_course_allow_waitlist_flag], [sis_waitlist_mult_sect_flag] in table 'wv_courses'
ALTER TABLE [dbo].[wv_courses]
ADD CONSTRAINT [PK_wv_courses]
    PRIMARY KEY CLUSTERED ([sis_course_id], [sis_course_count_retake_cred_flag], [sis_course_allow_audit_flag], [sis_course_allow_waitlist_flag], [sis_waitlist_mult_sect_flag] ASC);
GO

-- Creating primary key on [sis_division_id], [sis_division_location] in table 'wv_divisions'
ALTER TABLE [dbo].[wv_divisions]
ADD CONSTRAINT [PK_wv_divisions]
    PRIMARY KEY CLUSTERED ([sis_division_id], [sis_division_location] ASC);
GO

-- Creating primary key on [sis_institution_id] in table 'wv_institutions'
ALTER TABLE [dbo].[wv_institutions]
ADD CONSTRAINT [PK_wv_institutions]
    PRIMARY KEY CLUSTERED ([sis_institution_id] ASC);
GO

-- Creating primary key on [sis_student_id] in table 'wv_name_history'
ALTER TABLE [dbo].[wv_name_history]
ADD CONSTRAINT [PK_wv_name_history]
    PRIMARY KEY CLUSTERED ([sis_student_id] ASC);
GO

-- Creating primary key on [sis_person_id], [sis_person_corp_flag], [sis_person_directory_flag], [sis_person_privacy_flag] in table 'wv_persons'
ALTER TABLE [dbo].[wv_persons]
ADD CONSTRAINT [PK_wv_persons]
    PRIMARY KEY CLUSTERED ([sis_person_id], [sis_person_corp_flag], [sis_person_directory_flag], [sis_person_privacy_flag] ASC);
GO

-- Creating primary key on [sis_student_program_admit_status], [sis_student_program_location] in table 'wv_program_enrollments'
ALTER TABLE [dbo].[wv_program_enrollments]
ADD CONSTRAINT [PK_wv_program_enrollments]
    PRIMARY KEY CLUSTERED ([sis_student_program_admit_status], [sis_student_program_location] ASC);
GO

-- Creating primary key on [sis_program_id], [sis_program_stu_select_flag], [sis_program_active_flag], [sis_program_allow_grad_flag], [sis_program_create_application_flag] in table 'wv_programs'
ALTER TABLE [dbo].[wv_programs]
ADD CONSTRAINT [PK_wv_programs]
    PRIMARY KEY CLUSTERED ([sis_program_id], [sis_program_stu_select_flag], [sis_program_active_flag], [sis_program_allow_grad_flag], [sis_program_create_application_flag] ASC);
GO

-- Creating primary key on [sis_school_id], [sis_school_location_code] in table 'wv_schools'
ALTER TABLE [dbo].[wv_schools]
ADD CONSTRAINT [PK_wv_schools]
    PRIMARY KEY CLUSTERED ([sis_school_id], [sis_school_location_code] ASC);
GO

-- Creating primary key on [sis_application_id], [sis_appl_mark_delete] in table 'wv_student_applications'
ALTER TABLE [dbo].[wv_student_applications]
ADD CONSTRAINT [PK_wv_student_applications]
    PRIMARY KEY CLUSTERED ([sis_application_id], [sis_appl_mark_delete] ASC);
GO

-- Creating primary key on [sis_scs_id], [sis_student_id] in table 'wv_student_course_sections'
ALTER TABLE [dbo].[wv_student_course_sections]
ADD CONSTRAINT [PK_wv_student_course_sections]
    PRIMARY KEY CLUSTERED ([sis_scs_id], [sis_student_id] ASC);
GO

-- Creating primary key on [sis_stu_program_location], [sis_stu_program_admit_status] in table 'wv_student_programs'
ALTER TABLE [dbo].[wv_student_programs]
ADD CONSTRAINT [PK_wv_student_programs]
    PRIMARY KEY CLUSTERED ([sis_stu_program_location], [sis_stu_program_admit_status] ASC);
GO

-- Creating primary key on [sis_sttr_id], [sis_sttr_cred_limit_waive_flag], [sis_sttr_rehab_dept_client_flag], [sis_sttr_tech_prep_flag], [stcc_term_gpa_flag], [stcc_trgr_cum_gpa_flag] in table 'wv_student_terms'
ALTER TABLE [dbo].[wv_student_terms]
ADD CONSTRAINT [PK_wv_student_terms]
    PRIMARY KEY CLUSTERED ([sis_sttr_id], [sis_sttr_cred_limit_waive_flag], [sis_sttr_rehab_dept_client_flag], [sis_sttr_tech_prep_flag], [stcc_term_gpa_flag], [stcc_trgr_cum_gpa_flag] ASC);
GO

-- Creating primary key on [sis_term_id], [sis_term_use_in_best_fit_calc] in table 'wv_terms'
ALTER TABLE [dbo].[wv_terms]
ADD CONSTRAINT [PK_wv_terms]
    PRIMARY KEY CLUSTERED ([sis_term_id], [sis_term_use_in_best_fit_calc] ASC);
GO

-- Creating primary key on [crm_form_guid] in table 'crm_form_submissions'
ALTER TABLE [dbo].[crm_form_submissions]
ADD CONSTRAINT [PK_crm_form_submissions]
    PRIMARY KEY CLUSTERED ([crm_form_guid] ASC);
GO

-- Creating primary key on [crm_dynamic_content_guid] in table 'crm_dynamic_content'
ALTER TABLE [dbo].[crm_dynamic_content]
ADD CONSTRAINT [PK_crm_dynamic_content]
    PRIMARY KEY CLUSTERED ([crm_dynamic_content_guid] ASC);
GO

-- Creating primary key on [crm_cookie_history_uuid] in table 'crm_cookie_history'
ALTER TABLE [dbo].[crm_cookie_history]
ADD CONSTRAINT [PK_crm_cookie_history]
    PRIMARY KEY CLUSTERED ([crm_cookie_history_uuid] ASC);
GO

-- Creating primary key on [crm_setting_guid] in table 'crm_settings'
ALTER TABLE [dbo].[crm_settings]
ADD CONSTRAINT [PK_crm_settings]
    PRIMARY KEY CLUSTERED ([crm_setting_guid] ASC);
GO

-- Creating primary key on [crm_inq_prog_guid] in table 'crm_inquiry_programs'
ALTER TABLE [dbo].[crm_inquiry_programs]
ADD CONSTRAINT [PK_crm_inquiry_programs]
    PRIMARY KEY CLUSTERED ([crm_inq_prog_guid] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------