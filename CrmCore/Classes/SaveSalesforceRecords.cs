using CrmCore.Models;
using CrmLcLib;
using lc.crm;
using lc.crm.api;
using System;
using System.Data;
using System.Linq;

namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        #region Salesforce Centric

        private Result SaveSalesforceAccount(Result result, Account SfAccount)
        {
            try
            {
                if (SfAccount.RecordTypeId != AccountRecordTypes.AcademicProgram) {
                    crm_accounts crm_account = new crm_accounts();

                    var matches = db_ctx.crm_accounts.Where(x => x.account_id == SfAccount.Id).ToList();

                    if (matches.Any())
                    {
                        crm_account = matches.OrderByDescending(x => x.account_modifed_datetime).FirstOrDefault();
                        result.UpdatedCount++;

                    }
                    else
                    {
                        crm_account.account_guid = Guid.NewGuid();

                        crm_account.account_id = SfAccount.Id;

                        crm_account.account_created_by = SfAccount.CreatedById;
                        if (SfAccount.CreatedDateSpecified) crm_account.account_created_date = GetLocalTime(SfAccount.CreatedDate);

                        db_ctx.crm_accounts.Add(crm_account);
                        result.InsertedCount++;
                    }

                    if (!string.IsNullOrEmpty(crm_account.account_id)) {
                        // , Id
                        if (!string.IsNullOrWhiteSpace(SfAccount.Id))
                        {
                            crm_account.account_id = SfAccount.Id;
                        }

                        // , AccountNumber
                        if (!string.IsNullOrWhiteSpace(SfAccount.AccountNumber)) {
                            crm_account.account_number = SfAccount.AccountNumber;
                        }

                        // , AccountSource
                        // , AnnualRevenue
                        //if (SfAccount.AnnualRevenue != null) { crm_account.account_annual_revenue = (decimal)SfAccount.AnnualRevenue; }
                        // , BillingAddress
                        // , BillingCity
                        if (!string.IsNullOrWhiteSpace(SfAccount.BillingCity)) {
                            crm_account.account_billing_city = SfAccount.BillingCity;
                        }

                        // , BillingCountry
                        if (!string.IsNullOrWhiteSpace(SfAccount.BillingCountry)) {
                            crm_account.account_billing_country = SfAccount.BillingCountry;
                        }

                        // , BillingGeocodeAccuracy
                        // , BillingLatitude
                        // , BillingLongitude
                        // , BillingPostalCode
                        if (!string.IsNullOrWhiteSpace(SfAccount.BillingPostalCode)) {
                            crm_account.account_billing_postalcode = SfAccount.BillingPostalCode;
                        }

                        // , BillingState
                        if (!string.IsNullOrWhiteSpace(SfAccount.BillingState)) {
                            crm_account.account_billing_province = SfAccount.BillingState;
                        }

                        // , BillingStreet
                        if (!string.IsNullOrWhiteSpace(SfAccount.BillingStreet)) {
                            crm_account.account_billing_street = SfAccount.BillingStreet;
                        }

                        // , CreatedById
                        // , CreatedDate
                        // , Description

                        // , Fax
                        if (!string.IsNullOrWhiteSpace(SfAccount.Fax)) {
                            crm_account.account_fax = SfAccount.Fax;
                        }

                        // , hed__Current_Address__c
                        if (!string.IsNullOrWhiteSpace(SfAccount.hed__Current_Address__c)) {
                            crm_account.account_current_address = SfAccount.hed__Current_Address__c;
                        }

                        // , hed__Primary_Contact__c

                        // , Industry
                        if (!string.IsNullOrWhiteSpace(SfAccount.Industry)) {
                            crm_account.account_industry = SfAccount.Industry;
                        }
                        
                        // , IsDeleted
                        if (SfAccount.IsDeletedSpecified) { 
                            crm_account.account_mark_delete = SfAccount.IsDeleted ?? false;
                            crm_account.account_modifed_datetime = DateTime.Now;
                        }
                        // , Jigsaw
                        // , JigsawCompanyId
                        // , LastActivityDate
                        if (SfAccount.LastActivityDateSpecified) { 
                            crm_account.account_last_activity_date = GetLocalTime(SfAccount.LastActivityDate); 
                        }
                        // , LastModifiedById, LastModifiedDate
                        if (SfAccount.LastModifiedDateSpecified)
                        {
                            crm_account.account_modified_by = SfAccount.LastModifiedById;
                            crm_account.account_modifed_datetime = GetLocalTime(SfAccount.LastModifiedDate);
                        }
                        // , LastReferencedDate

                        if (SfAccount.LastReferencedDateSpecified) { 
                            crm_account.account_last_referenced_date = GetLocalTime(SfAccount.LastReferencedDate); 
                        }
                        // , LastViewedDate
                        if (SfAccount.LastViewedDateSpecified) { 
                            crm_account.account_last_viewed_date = GetLocalTime(SfAccount.LastViewedDate); 
                        }
                        // , lc_account_active__c
                        if (SfAccount.lc_account_active__cSpecified) { 
                            crm_account.account_active = SfAccount.lc_account_active__c ?? true; 
                        }
                        // , lc_account_type__c
                        if (!string.IsNullOrWhiteSpace(SfAccount.lc_account_type__c)) {
                            crm_account.account_type = SfAccount.lc_account_type__c;
                        }

                        // , lc_colleague_account_id__c
                        // , MasterRecordId
                        // , Name
                        if (!string.IsNullOrWhiteSpace(SfAccount.Name)) {
                            crm_account.account_name = SfAccount.Name;
                        }

                        // , NumberOfEmployees
                        // , OwnerId
                        if (!string.IsNullOrWhiteSpace(SfAccount.OwnerId)) {
                            crm_account.account_owner_id = SfAccount.OwnerId ?? Settings.CrmAdmin;
                        }

                        // , ParentId
                        if (!string.IsNullOrWhiteSpace(SfAccount.ParentId)) {
                            crm_account.account_parent_account_id = SfAccount.ParentId ?? Settings.LcAccountId;
                        }

                        // , Phone
                        if (!string.IsNullOrWhiteSpace(SfAccount.Phone)) {
                            crm_account.account_phone = SfAccount.Phone;
                        }

                        // , PhotoUrl
                        // , RecordTypeId
                        if (!string.IsNullOrWhiteSpace(SfAccount.RecordTypeId)) {
                            crm_account.account_record_type_id = SfAccount.RecordTypeId;
                        }

                        // , ShippingAddress
                        // , ShippingCity
                        if (!string.IsNullOrWhiteSpace(SfAccount.ShippingCity)) {
                            crm_account.account_shipping_city = SfAccount.ShippingCity;
                        }
                        
                        // , ShippingCountry
                        if (!string.IsNullOrWhiteSpace(SfAccount.ShippingCountry)) {
                            crm_account.account_shipping_country = SfAccount.ShippingCountry;
                        }
                        
                        // , ShippingGeocodeAccuracy
                        // , ShippingLatitude
                        // , ShippingLongitude
                        // , ShippingPostalCode
                        if (!string.IsNullOrWhiteSpace(SfAccount.ShippingPostalCode)) {
                            crm_account.account_shipping_postalcode = SfAccount.ShippingPostalCode;
                        }
                        
                        // , ShippingState
                        if (!string.IsNullOrWhiteSpace(SfAccount.ShippingState)) {
                            crm_account.account_shipping_province = SfAccount.ShippingState;
                        }
                        
                        // , ShippingStreet
                        if (!string.IsNullOrWhiteSpace(SfAccount.ShippingStreet)) {
                            crm_account.account_shipping_street = SfAccount.ShippingStreet;
                        }
                        
                        // , SicDesc
                        // , SystemModstamp
                        // , Type
                        // , Website
                        if (!string.IsNullOrWhiteSpace(SfAccount.Website)) {
                            crm_account.account_website = SfAccount.Website;
                        }
                        

                        crm_account.last_sfsync_datetime = DateTime.Now;

                        if (db_ctx.SaveChanges() > 0)
                        {
                            result.Success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveActivityExtender(Result result, lc_activity_extender__c SfActivityExtender)
        {
            try
            {
                crm_activity_extender DbActivityExtender = new crm_activity_extender();

                var crm_activity_extenders = db_ctx.crm_activity_extender.Where(x => x.activity_extender_id == SfActivityExtender.Id);

                if (crm_activity_extenders.Any())
                {
                    DbActivityExtender = crm_activity_extenders.OrderByDescending(x=>x.activity_extender_modified_datetime).FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbActivityExtender.activity_extender_guid = Guid.NewGuid();

                    DbActivityExtender.activity_extender_id = SfActivityExtender.Id;

                    DbActivityExtender.activity_extender_created_by = SfActivityExtender.CreatedById;
                    if (SfActivityExtender.CreatedDateSpecified) DbActivityExtender.activity_extender_created_datetime = GetLocalTime(SfActivityExtender.CreatedDate);

                    db_ctx.crm_activity_extender.Add(DbActivityExtender);
                    result.InsertedCount++;
                }

                if (!string.IsNullOrEmpty(DbActivityExtender.activity_extender_id)) {
                    DbActivityExtender.activity_extender_deleted = SfActivityExtender.IsDeleted ?? false;

                    DbActivityExtender.activity_extender_modified_by = SfActivityExtender.LastModifiedById;
                    DbActivityExtender.activity_extender_modified_datetime = GetLocalTime(SfActivityExtender.LastModifiedDate ?? DateTime.Now);

                    DbActivityExtender.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveSalesforceAffiliation(Result result, hed__Affiliation__c SfAffiliation)
        {
            try
            {

                crm_affiliations crm_affiliation = new crm_affiliations();

                var crm_Affiliations = db_ctx.crm_affiliations.Where(x => (x.affiliation_id == SfAffiliation.Id
                                                                    && x.affiliation_record_type_id != AffiliationRecordTypes.StudentProgramAffiliation))
                                                              .ToList();

                if (crm_Affiliations.Any())
                {
                    crm_affiliation = crm_Affiliations.OrderByDescending(x => x.affiliation_last_modified_datetime).FirstOrDefault();

                    if (crm_affiliation != null)
                    {
                        if (crm_affiliation.affiliation_id != SfAffiliation.Id) {
                            crm_affiliation.affiliation_id = SfAffiliation.Id;
                        }
                        
                        if (crm_affiliation.affiliation_guid.ToString() != SfAffiliation.lc_affiliation_guid__c)
                        {
                            crm_affiliation.affiliation_guid_mismatch = true;
                        }
                    }

                    result.UpdatedCount++;
                }
                else
                {
                    crm_affiliation.affiliation_guid = Guid.NewGuid();

                    crm_affiliation.affiliation_id = SfAffiliation.Id;

                    if (SfAffiliation.CreatedDateSpecified)
                    {
                        crm_affiliation.affiliation_created_by = SfAffiliation.CreatedById;
                        crm_affiliation.affiliation_created_datetime = GetLocalTime(SfAffiliation.CreatedDate);
                    }

                    db_ctx.crm_affiliations.Add(crm_affiliation);

                    result.InsertedCount++;
                }

                if (!string.IsNullOrEmpty(crm_affiliation.affiliation_id)) {

                    if (crm_affiliation.affiliation_contact_id == null && SfAffiliation.hed__Contact__c != null)
                    {
                        crm_affiliation.affiliation_contact_id = SfAffiliation.hed__Contact__c;
                    }

                    if (crm_affiliation.affiliation_organization_id == null && SfAffiliation.hed__Account__c != null)
                    {
                        crm_affiliation.affiliation_organization_id = SfAffiliation.hed__Account__c;
                    }

                    if (crm_affiliation.lc_student_program_id == null && SfAffiliation.lc_student_program_id__c != null)
                    {
                        crm_affiliation.lc_student_program_id = SfAffiliation.lc_student_program_id__c;
                    }

                    crm_affiliation.affiliation_record_type_id = SfAffiliation.RecordTypeId;

                    crm_affiliation.affiliation_owner_id = SfAffiliation.OwnerId;

                    crm_affiliation.affiliation_deleted = SfAffiliation.IsDeleted ?? false;

                    crm_affiliation.affiliation_key = SfAffiliation.Name;
                    crm_affiliation.affiliation_role = SfAffiliation.hed__Role__c;
                    crm_affiliation.affiliation_status = SfAffiliation.hed__Status__c;
                    crm_affiliation.affiliation_type = SfAffiliation.hed__Affiliation_Type__c;

                    if (SfAffiliation.hed__EndDate__cSpecified)
                    {
                        crm_affiliation.affiliation_end_date = GetLocalTime(SfAffiliation.hed__EndDate__c);
                    }
                    else
                    {
                        crm_affiliation.affiliation_end_date = null;
                    }

                    if (SfAffiliation.hed__StartDate__cSpecified)
                    {
                        crm_affiliation.affiliation_start_date = GetLocalTime(SfAffiliation.hed__StartDate__c);
                    }
                    else
                    {
                        crm_affiliation.affiliation_start_date = null;
                    }

                    crm_affiliation.affiliation_primary = SfAffiliation.hed__Primary__c ?? false;

                    if (SfAffiliation.SystemModstampSpecified)
                    {
                        crm_affiliation.affiliation_system_modstamp = GetLocalTime(SfAffiliation.SystemModstamp);
                    }

                    if (SfAffiliation.LastViewedDateSpecified)
                    {
                        crm_affiliation.affiliation_last_viewed_datetime = GetLocalTime(SfAffiliation.LastViewedDate);
                    }

                    if (SfAffiliation.LastReferencedDateSpecified)
                    {
                        crm_affiliation.affiliation_last_referenced_datetime = GetLocalTime(SfAffiliation.LastReferencedDate);
                    }

                    if (SfAffiliation.LastModifiedDateSpecified)
                    {
                        crm_affiliation.affiliation_last_modified_by = SfAffiliation.LastModifiedById;
                        crm_affiliation.affiliation_last_modified_datetime = GetLocalTime(SfAffiliation.LastModifiedDate);
                    }

                    crm_affiliation.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveSalesforceContact(Result result, Contact SfContact)
        {
            try
            {
                if (SfContact.RecordTypeId != ContactRecordTypes.DefaultContactRecordType &&
                    SfContact.RecordTypeId != ContactRecordTypes.DuplicateContactRecordType &&
                    SfContact.RecordTypeId != ContactRecordTypes.PrivateContactRecordType) {

                    crm_contacts crm_contact = new crm_contacts();

                    var crm_contacts = db_ctx.crm_contacts.Where(x => x.contact_id == SfContact.Id).ToList();

                    if (crm_contacts.Any())
                    {
                        crm_contact = crm_contacts.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();
                        result.UpdatedCount++;
                    }
                    else
                    {
                        crm_contact.contact_guid = Guid.NewGuid();

                        crm_contact.contact_created_by = SfContact.CreatedById ?? Settings.CrmAdmin;
                        crm_contact.contact_created_datetime = GetLocalTime(SfContact.CreatedDate);

                        db_ctx.crm_contacts.Add(crm_contact);
                        result.InsertedCount++;
                    }

                    if (string.IsNullOrEmpty(crm_contact.contact_id)) {
                        crm_contact.contact_id = SfContact.Id;
                    }

                    if (!string.IsNullOrEmpty(crm_contact.contact_id))
                    {
                        crm_contact.contact_record_type_id = SfContact.RecordTypeId;

                        crm_contact.contact_deleted = SfContact.IsDeleted ?? false;
                        crm_contact.contact_account_id = SfContact.AccountId;

                        crm_contact.contact_owner_id = SfContact.OwnerId;

                        crm_contact.contact_first_name = SfContact.FirstName;
                        crm_contact.contact_last_name = SfContact.LastName;
                        crm_contact.contact_full_name = SfContact.Name;

                        crm_contact.contact_gender = SfContact.hed__Gender__c;

                        crm_contact.contact_home_phone = SfContact.HomePhone;
                        crm_contact.contact_mobile_phone = SfContact.MobilePhone;

                        if (!string.IsNullOrEmpty(SfContact.hed__AlternateEmail__c) && !SfContact.hed__AlternateEmail__c.Contains("lethbridge"))
                        {
                            crm_contact.contact_alternate_email = SfContact.hed__AlternateEmail__c;
                        }

                        if (!string.IsNullOrEmpty(SfContact.hed__WorkEmail__c))
                        {
                            crm_contact.contact_work_email = SfContact.hed__WorkEmail__c;
                        }

                        crm_contact.contact_preferred_email = SfContact.hed__Preferred_Email__c;

                        crm_contact.contact_primary_address_type = SfContact.hed__Primary_Address_Type__c;
                        crm_contact.contact_mailing_street = SfContact.MailingStreet;
                        crm_contact.contact_mailing_city = SfContact.MailingCity;
                        crm_contact.contact_mailing_province = SfContact.MailingState;
                        crm_contact.contact_mailing_country = SfContact.MailingCountry;
                        crm_contact.contact_mailing_postalcode = SfContact.MailingPostalCode;
                        crm_contact.contact_last_modified_by = SfContact.LastModifiedById ?? Settings.CrmAdmin;

                        if (SfContact.BirthdateSpecified) crm_contact.contact_birthdate = SfContact.Birthdate;

                        if (SfContact.HasOptedOutOfEmailSpecified) crm_contact.contact_email_opt_out = SfContact.HasOptedOutOfEmail ?? false;
                        if (SfContact.IsEmailBouncedSpecified) crm_contact.contact_is_email_bounced = SfContact.IsEmailBounced ?? false;

                        if (SfContact.LastReferencedDateSpecified) crm_contact.contact_last_referenced_datetime = GetLocalTime(SfContact.LastReferencedDate);
                        if (SfContact.LastViewedDateSpecified) crm_contact.contact_last_viewed_datetime = GetLocalTime(SfContact.LastViewedDate);
                        if (SfContact.LastActivityDateSpecified) crm_contact.contact_last_activity_date = GetLocalTime(SfContact.LastActivityDate);

                        if (SfContact.IsEmailBouncedSpecified) crm_contact.contact_is_email_bounced = SfContact.IsEmailBounced ?? false;
                        if (SfContact.EmailBouncedDateSpecified) crm_contact.contact_email_bounced_date = GetLocalTime(SfContact.EmailBouncedDate);
                        if (SfContact.EmailBouncedReason != null) crm_contact.contact_email_bounced_reason = SfContact.EmailBouncedReason;

                        if (SfContact.LastModifiedDateSpecified) crm_contact.contact_last_modified_datetime = GetLocalTime(SfContact.LastModifiedDate);

                        crm_contact.last_sfsync_datetime = DateTime.Now;

                        if (db_ctx.SaveChanges() > 0)
                        {
                            result.Success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveEmailCampaign(Result result, lc_email_campaign__c SfEmailCampaign)
        {
            try
            {
                crm_email_campaigns DbEmailCampaign = new crm_email_campaigns();

                var DbEmailCampaigns = db_ctx.crm_email_campaigns.Where(x => x.email_campaign_id == SfEmailCampaign.Id);

                if (DbEmailCampaigns.Any())
                {
                    DbEmailCampaign = DbEmailCampaigns.FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbEmailCampaign.email_campaign_guid = Guid.NewGuid();

                    DbEmailCampaign.email_campaign_id = SfEmailCampaign.Id;

                    DbEmailCampaign.ec_created_by = SfEmailCampaign.CreatedById;
                    if (SfEmailCampaign.CreatedDateSpecified) DbEmailCampaign.ec_created_datetime = GetLocalTime(SfEmailCampaign.CreatedDate);

                    db_ctx.crm_email_campaigns.Add(DbEmailCampaign);
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(DbEmailCampaign.email_campaign_id)) {
        
                    DbEmailCampaign.ec_deleted = SfEmailCampaign.IsDeleted ?? false;
                    DbEmailCampaign.ec_active_flag = SfEmailCampaign.lc_active__c ?? false;
                    DbEmailCampaign.ec_recur = SfEmailCampaign.lc_is_reoccurring__c ?? false;

                    DbEmailCampaign.ec_report_id = SfEmailCampaign.lc_report_id__c;
                    DbEmailCampaign.ec_template_id = SfEmailCampaign.lc_email_template_id__c;

                    DbEmailCampaign.ec_send_time = SfEmailCampaign.lc_send_time__c.Value.TimeOfDay;

                    DbEmailCampaign.ec_email_address_type = SfEmailCampaign.lc_recipient_address_type__c;

                    DbEmailCampaign.ec_allow_repeat_broadcasts = SfEmailCampaign.lc_allow_repeat_broadcasts__c ?? false;

                    DbEmailCampaign.ec_allow_ongoing_delivery = SfEmailCampaign.lc_email_campaign_ongoing_delivery__c ?? false;

                    // this custom screen and object do not implement UTC i.e. this is already local time
                    // Mike Paulson is working on this
                    DbEmailCampaign.ec_start_datetime = GetLocalTime(SfEmailCampaign.lc_start_date__c);

                    if (SfEmailCampaign.lc_end_date__cSpecified)
                    {
                        DbEmailCampaign.ec_end_datetime = GetLocalTime(SfEmailCampaign.lc_end_date__c);
                    }
                    else
                    {
                        DbEmailCampaign.ec_end_datetime = null;
                    }

                    if (SfEmailCampaign.lc_is_reoccurring__cSpecified)
                    {
                        DbEmailCampaign.ec_recur = SfEmailCampaign.lc_is_reoccurring__c ?? false;
                    }
                    else
                    {
                        DbEmailCampaign.ec_recur = false;
                    }

                    DbEmailCampaign.ec_recur_days = SfEmailCampaign.lc_recur_days__c;
                    DbEmailCampaign.ec_recur_week_days = SfEmailCampaign.lc_recur_week_days__c;
                    DbEmailCampaign.ec_week_days_only_flag = SfEmailCampaign.lc_week_days_only__c ?? false;
                    DbEmailCampaign.ec_name = SfEmailCampaign.Name;
                    DbEmailCampaign.ec_department = SfEmailCampaign.lc_department__c;
                    DbEmailCampaign.ec_send_now = SfEmailCampaign.lc_send_now__c;
                    DbEmailCampaign.ec_parent_campaign_id = SfEmailCampaign.lc_parent_campaign__c;
                    DbEmailCampaign.ec_from_email_address = SfEmailCampaign.lc_source_email__c;
                    DbEmailCampaign.ec_from_email_address_title = SfEmailCampaign.lc_source_name__c;

                    if (SfEmailCampaign.LastModifiedDateSpecified)
                    {
                        DbEmailCampaign.ec_modified_by = SfEmailCampaign.LastModifiedById;
                        DbEmailCampaign.ec_modified_datetime = GetLocalTime(SfEmailCampaign.LastModifiedDate);
                    }

                    DbEmailCampaign.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveEmailBroadcast(Result result, lc_email_broadcast__c sf_email_broadcast)
        {
            try
            {
                crm_email_broadcasts DbEmailBroadcast = new crm_email_broadcasts();

                var DbEmailBroadcasts = db_ctx.crm_email_broadcasts.Where(x => x.email_broadcast_id == sf_email_broadcast.Id);

                if (DbEmailBroadcasts.Any())
                {
                    DbEmailBroadcast = DbEmailBroadcasts.OrderByDescending(x=>x.email_broadcast_modfied_datetime).FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbEmailBroadcast.email_broadcast_guid = Guid.NewGuid();

                    DbEmailBroadcast.email_broadcast_id = sf_email_broadcast.Id;

                    DbEmailBroadcast.email_broadcast_created_by = sf_email_broadcast.CreatedById;
                    if (sf_email_broadcast.CreatedDateSpecified) DbEmailBroadcast.email_broadcast_created_datetime = GetLocalTime(sf_email_broadcast.CreatedDate);

                    db_ctx.crm_email_broadcasts.Add(DbEmailBroadcast);
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(DbEmailBroadcast.email_broadcast_id)) {
                    if (sf_email_broadcast.LastModifiedDateSpecified)
                    {
                        DbEmailBroadcast.email_broadcast_modified_by = sf_email_broadcast.LastModifiedById;
                        DbEmailBroadcast.email_broadcast_modfied_datetime = GetLocalTime(sf_email_broadcast.LastModifiedDate);
                    }

                    if (sf_email_broadcast.lc_broadcast_sent__cSpecified)
                    {
                        DbEmailBroadcast.email_broadcast_sent = sf_email_broadcast.lc_broadcast_sent__c;
                    }

                    if (sf_email_broadcast.IsDeletedSpecified)
                    {
                        DbEmailBroadcast.email_broadcast_deleted = sf_email_broadcast.IsDeleted ?? false;
                    }

                    DbEmailBroadcast.email_broadcast_messages_sent = sf_email_broadcast.lc_messages_sent__c;
                    DbEmailBroadcast.email_broadcast_name = sf_email_broadcast.Name;
                    DbEmailBroadcast.email_broadcast_status = sf_email_broadcast.lc_broadcast_status__c;
                    DbEmailBroadcast.email_broadcast_sys_modstamp = GetLocalTime(sf_email_broadcast.SystemModstamp);
                    DbEmailBroadcast.email_campaign_id = sf_email_broadcast.lc_email_campaign__c;

                    DbEmailBroadcast.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveEmailTemplate(Result result, EmailTemplate SfEmailTemplate)
        {
            try
            {
                crm_email_templates DbEmailTemplate = new crm_email_templates();

                var DbEmailTemplates = db_ctx.crm_email_templates.Where(x => x.email_template_id == SfEmailTemplate.Id
                                                                        && x.email_template_id != null);

                if (DbEmailTemplates.Any())
                {
                    DbEmailTemplate = DbEmailTemplates.FirstOrDefault();

                    result.UpdatedCount++;
                }
                else
                {
                    DbEmailTemplate.email_template_guid = Guid.NewGuid();

                    DbEmailTemplate.email_template_id = SfEmailTemplate.Id;

                    DbEmailTemplate.email_template_created_by = SfEmailTemplate.CreatedById;
                    if (SfEmailTemplate.CreatedDateSpecified) DbEmailTemplate.email_template_created_datetime = GetLocalTime(SfEmailTemplate.CreatedDate);

                    db_ctx.crm_email_templates.Add(DbEmailTemplate);

                    result.InsertedCount++;
                }

                if (!string.IsNullOrEmpty(DbEmailTemplate.email_template_id)) {

                    if (SfEmailTemplate.LastModifiedDateSpecified)
                    {
                        DbEmailTemplate.email_template_modified_by = SfEmailTemplate.LastModifiedById;
                        DbEmailTemplate.email_template_modified_datetime = GetLocalTime(SfEmailTemplate.LastModifiedDate);
                    }

                    DbEmailTemplate.email_template_body = SfEmailTemplate.Body;
                    DbEmailTemplate.email_template_brand_template_id = SfEmailTemplate.BrandTemplateId;
                    DbEmailTemplate.email_template_description = SfEmailTemplate.Description;
                    DbEmailTemplate.email_template_dev_name = SfEmailTemplate.DeveloperName;
                    DbEmailTemplate.email_template_encoding = SfEmailTemplate.Encoding;
                    DbEmailTemplate.email_template_folder_id = SfEmailTemplate.FolderId;
                    DbEmailTemplate.email_template_folder_name = SfEmailTemplate.FolderName;
                    DbEmailTemplate.email_template_html_value = SfEmailTemplate.HtmlValue;
                    DbEmailTemplate.email_template_is_active = SfEmailTemplate.IsActive ?? false;
                    DbEmailTemplate.email_template_last_used_datetime = GetLocalTime(SfEmailTemplate.LastUsedDate);
                    DbEmailTemplate.email_template_times_used = SfEmailTemplate.TimesUsed;
                    DbEmailTemplate.email_template_markup = SfEmailTemplate.Markup;
                    DbEmailTemplate.email_template_name = SfEmailTemplate.Name;
                    DbEmailTemplate.email_template_namespace_prefix = SfEmailTemplate.NamespacePrefix;
                    DbEmailTemplate.email_template_owner_id = SfEmailTemplate.OwnerId;
                    DbEmailTemplate.email_template_subject = SfEmailTemplate.Subject;
                    DbEmailTemplate.email_template_system_modstamp = SfEmailTemplate.SystemModstamp;
                    DbEmailTemplate.email_template_style = SfEmailTemplate.TemplateStyle;
                    DbEmailTemplate.email_template_type = SfEmailTemplate.TemplateType;

                    DbEmailTemplate.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveEvent(Result result, Event SfEvent)
        {
            try
            {
                crm_events db_crm_event = new crm_events();

                var crm_events = db_ctx.crm_events.Where(x => x.activity_id == SfEvent.Id).ToList();

                if (crm_events.Any())
                {
                    db_crm_event = crm_events.OrderByDescending(x=>x.event_modified_datetime).FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    db_crm_event.activity_guid = Guid.NewGuid();

                    db_crm_event.activity_id = SfEvent.Id;

                    db_crm_event.event_created_by = SfEvent.CreatedById;
                    if (SfEvent.CreatedDateSpecified) db_crm_event.event_created_datetime = GetLocalTime(SfEvent.CreatedDate);

                    db_ctx.crm_events.Add(db_crm_event);
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(db_crm_event.activity_id)) {
                    db_crm_event.event_name_id = SfEvent.WhoId;
                    db_crm_event.event_related_to_id = SfEvent.WhatId;

                    if (SfEvent.StartDateTimeSpecified)
                    {
                        db_crm_event.event_start_date = GetLocalTime(SfEvent.StartDateTime);
                    }

                    if (SfEvent.StartDateTimeSpecified)
                    {
                        db_crm_event.event_start_datetime = GetLocalTime(SfEvent.StartDateTime);
                    }

                    if (SfEvent.EndDateTimeSpecified)
                    {
                        db_crm_event.event_end_datetime = GetLocalTime(SfEvent.EndDateTime);
                    }

                    if (SfEvent.EndDateTimeSpecified)
                    {
                        db_crm_event.event_end_date = GetLocalTime(SfEvent.EndDateTime);
                    }

                    if (SfEvent.RecurrenceStartDateTimeSpecified)
                    {
                        db_crm_event.event_recurrence_start = GetLocalTime(SfEvent.RecurrenceStartDateTime);
                    }

                    if (SfEvent.lc_send_confirmations__cSpecified) {
                        db_crm_event.event_confirmation_send_flag = SfEvent.lc_send_confirmations__c;
                    }

                    if (SfEvent.lc_send_reminders__cSpecified) {
                        db_crm_event.event_reminder_send_flag = SfEvent.lc_send_reminders__c;
                    }

                    db_crm_event.event_reminder_email_address = SfEvent.lc_organizer_email_address__c;

                    db_crm_event.event_reminder_dates = SfEvent.lc_event_reminder_dates__c;

                    db_crm_event.event_reminder_datetime = SfEvent.ReminderDateTime;

                    db_crm_event.event_location = SfEvent.Location;

                    db_crm_event.event_is_child = SfEvent.IsChild ?? false;

                    db_crm_event.event_group_event_type = SfEvent.GroupEventType;

                    db_crm_event.event_deleted = SfEvent.IsDeleted ?? false;
                    db_crm_event.event_description = SfEvent.Description;

                    db_crm_event.event_modified_by = SfEvent.LastModifiedById;

                    if (SfEvent.LastModifiedDateSpecified)
                    {
                        db_crm_event.event_modified_datetime = GetLocalTime(SfEvent.LastModifiedDate ?? DateTime.Now);
                    }

                    db_crm_event.event_account_id = SfEvent.AccountId;
                    db_crm_event.event_recurrence_activity_id = SfEvent.RecurrenceActivityId;

                    db_crm_event.event_archived = SfEvent.IsArchived ?? false;

                    db_crm_event.event_subject = SfEvent.Subject;

                    db_crm_event.event_department = SfEvent.Department__c;

                    db_crm_event.event_engagement_type = SfEvent.lc_engagement_type__c;

                    db_crm_event.event_activity_extender_id = SfEvent.lc_activity_extender__c;

                    db_crm_event.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveEventRegistration(Result result, lc_event_registration__c SfEventRegistration)
        {
            try
            {
                crm_event_registrations DbEventRegistration = new crm_event_registrations();

                var crm_eventRegistrations = db_ctx.crm_event_registrations.Where(x => x.event_registration_id == SfEventRegistration.Id).ToList();

                if (crm_eventRegistrations.Any())
                {
                    DbEventRegistration = crm_eventRegistrations.FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbEventRegistration.event_registration_guid = Guid.NewGuid();

                    DbEventRegistration.event_registration_id = SfEventRegistration.Id;

                    DbEventRegistration.event_registration_created_by = SfEventRegistration.CreatedById;
                    if (SfEventRegistration.CreatedDateSpecified) DbEventRegistration.event_registration_created_datetime = GetLocalTime(SfEventRegistration.CreatedDate);

                    db_ctx.crm_event_registrations.Add(DbEventRegistration);
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(DbEventRegistration.event_registration_id)) {
                    DbEventRegistration.event_registration_deleted = SfEventRegistration.IsDeleted ?? false;
                    DbEventRegistration.event_registration_activity_extender_id = SfEventRegistration.lc_activity_extender__c;
                    DbEventRegistration.event_registration_attended = SfEventRegistration.lc_attended__c;
                    DbEventRegistration.event_registration_registered = SfEventRegistration.lc_registered__c;
                    DbEventRegistration.event_registration_checkedin = SfEventRegistration.lc_checkedin__c;
                    DbEventRegistration.event_registration_cancelled = SfEventRegistration.lc_cancelled__c;
                    DbEventRegistration.event_registration_contact_id = SfEventRegistration.lc_contact__c;

                    if (SfEventRegistration.LastModifiedDateSpecified)
                    {
                        DbEventRegistration.event_registration_modified_by = SfEventRegistration.LastModifiedById;
                        DbEventRegistration.event_registration_modified_datetime = GetLocalTime(SfEventRegistration.LastModifiedDate ?? DateTime.Now);
                    }

                    DbEventRegistration.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveInquiry(Result result, lc_inquiry__c SfInquiry)
        {
            try
            {
                crm_inquiries crm_inquiry = new crm_inquiries();

                var crm_inquiries = db_ctx.crm_inquiries.Where(x => x.inquiry_id == SfInquiry.Id);

                if (crm_inquiries.Any())
                {
                    crm_inquiry = crm_inquiries.OrderByDescending(x=>x.inq_modified_datetime).FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    crm_inquiry.inquiry_guid = Guid.NewGuid();

                    crm_inquiry.inquiry_id = SfInquiry.Id;

                    crm_inquiry.inq_created_by = SfInquiry.CreatedById ?? Settings.CrmAdmin;
                    crm_inquiry.inq_created_datetime = GetLocalTime(SfInquiry.CreatedDate ?? SfInquiry.lc_inquiry_date__c ?? DateTime.Now);

                    db_ctx.crm_inquiries.Add(crm_inquiry);
                    result.InsertedCount++;
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(crm_inquiry.inquiry_id)) {
                    crm_inquiry.inquiry_datetime = GetLocalTime(SfInquiry.lc_inquiry_date__c ?? SfInquiry.CreatedDate ?? DateTime.Now);

                    if (!string.IsNullOrEmpty(SfInquiry.Name))
                    {
                        crm_inquiry.inq_number = SfInquiry.Name;
                    }

                    crm_inquiry.inq_owner_id = SfInquiry.OwnerId;
                    crm_inquiry.inq_deleted = SfInquiry.IsDeleted ?? false;

                    crm_inquiry.inq_contact_id = SfInquiry.lc_contact__c;
                    crm_inquiry.lc_inq_legacy_id = SfInquiry.lc_inquiry_legacy_id__c;

                    crm_inquiry.inq_anticipated_start_term = SfInquiry.lc_anticipated_start_term__c;

                    crm_inquiry.inq_pri_prog_interest = SfInquiry.lc_primary_program__c;
                    crm_inquiry.inq_sec_prog_interest = SfInquiry.lc_secondary_program__c;
                    crm_inquiry.inq_services_interest = SfInquiry.lc_services_interest__c;

                    crm_inquiry.inq_source = SfInquiry.lc_source__c;

                    crm_inquiry.inq_student_type = SfInquiry.lc_student_type__c;

                    crm_inquiry.inq_last_school = SfInquiry.lc_last_school__c;

                    crm_inquiry.inq_campus = SfInquiry.lc_inquiry_campus__c ?? "main";

                    crm_inquiry.inq_agent_flag = SfInquiry.lc_agent__c ?? false;
                    crm_inquiry.inq_agent_prev_flag = SfInquiry.lc_agent_previous_work__c ?? false;
                    crm_inquiry.inq_agency_name = SfInquiry.lc_agency_name__c;

                    if (SfInquiry.LastActivityDate != null)
                    {
                        crm_inquiry.inq_last_activity_date = GetLocalTime(SfInquiry.LastActivityDate);
                    }

                    if (SfInquiry.LastReferencedDate != null)
                    {
                        crm_inquiry.inq_last_referenced_datetime = GetLocalTime(SfInquiry.LastReferencedDate);
                    }

                    if (SfInquiry.LastModifiedDate != null)
                    {

                        crm_inquiry.inq_modified_by = SfInquiry.LastModifiedById ?? Settings.CrmAdmin;
                        crm_inquiry.inq_modified_datetime = GetLocalTime(SfInquiry.LastModifiedDate);
                    }

                    crm_inquiry.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveReport(Result result, Report SfReport)
        {
            try
            {
                crm_reports DbReport = new crm_reports();

                var DbReports = db_ctx.crm_reports.Where(x => x.crm_report_id == SfReport.Id).ToList();

                if (DbReports.Any())
                {
                    DbReport = DbReports.OrderByDescending(x => x.crm_report_modified_date).FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbReport.report_guid = Guid.NewGuid();

                    DbReport.crm_report_id = SfReport.Id;

                    if (SfReport.CreatedDateSpecified)
                    {
                        DbReport.crm_report_created_by = SfReport.CreatedById;
                        DbReport.crm_report_created_date = GetLocalTime(SfReport.CreatedDate);
                    }

                    db_ctx.crm_reports.Add(DbReport);
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(DbReport.crm_report_id)) {
                    DbReport.crm_report_name = SfReport.Name;
                    DbReport.crm_namespace_prefix = SfReport.NamespacePrefix;
                    DbReport.crm_report_description = SfReport.Description;
                    DbReport.crm_report_folder_name = SfReport.FolderName;
                    DbReport.crm_report_format = SfReport.Format;
                    DbReport.crm_report_owner_id = SfReport.OwnerId;
                    DbReport.crm_report_developer_name = SfReport.DeveloperName;

                    if (SfReport.IsDeletedSpecified)
                    {
                        DbReport.crm_report_deleted = SfReport.IsDeleted ?? false;
                    }

                    if (SfReport.LastRunDateSpecified)
                    {
                        DbReport.crm_report_last_run_date = SfReport.LastRunDate;
                    }

                    if (SfReport.SystemModstampSpecified)
                    {
                        DbReport.crm_report_system_modstamp = SfReport.SystemModstamp;
                    }

                    if (SfReport.LastReferencedDateSpecified)
                    {
                        DbReport.crm_report_last_referenced_date = SfReport.LastReferencedDate;
                    }

                    if (SfReport.LastViewedDateSpecified)
                    {
                        DbReport.crm_report_last_viewed_date = SfReport.LastViewedDate;
                    }

                    if (SfReport.LastModifiedDateSpecified)
                    {
                        DbReport.crm_report_modified_by = SfReport.LastModifiedById;
                        DbReport.crm_report_modified_date = SfReport.LastModifiedDate;
                    }

                    DbReport.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveRoleValue(Result result, lc_role_values__c SfRoleValue)
        {
            try
            {
                crm_role_values DbRoleValue = new crm_role_values();

                var DbRoleValues = db_ctx.crm_role_values.Where(x => x.crm_role_value_id == SfRoleValue.Id);

                if (DbRoleValues.Any())
                {
                    DbRoleValue = DbRoleValues.OrderByDescending(x => x.crm_rv_modified_datetime).FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbRoleValue.crm_role_value_guid = Guid.NewGuid();
                    DbRoleValue.crm_rv_created_by = SfRoleValue.CreatedById;
                    if (SfRoleValue.CreatedDateSpecified) DbRoleValue.crm_rv_created_datetime = GetLocalTime(SfRoleValue.CreatedDate);

                    db_ctx.crm_role_values.Add(DbRoleValue);
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(DbRoleValue.crm_role_value_id = SfRoleValue.Id)) {
                    DbRoleValue.crm_rv_name = SfRoleValue.Name;
                    DbRoleValue.crm_rv_owner_id = SfRoleValue.OwnerId;
                    DbRoleValue.crm_rv_role_id = SfRoleValue.lc_role_id__c;
                    DbRoleValue.crm_rv_sender_name = SfRoleValue.lc_sender_name__c;
                    DbRoleValue.crm_rv_send_emails = SfRoleValue.lc_send_emails__c;
                    DbRoleValue.crm_rv_default_contact_type = SfRoleValue.lc_default_contact_type__c;

                    if (SfRoleValue.IsDeletedSpecified)
                    {
                        DbRoleValue.crm_rv_deleted_flag = SfRoleValue.IsDeleted ?? false;
                    }

                    if (SfRoleValue.LastModifiedDateSpecified)
                    {
                        DbRoleValue.crm_rv_modified_by = SfRoleValue.LastModifiedById;
                        DbRoleValue.crm_rv_modified_datetime = GetLocalTime(SfRoleValue.LastModifiedDate);
                    }

                    DbRoleValue.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveTask(Result result, Task SfTask)
        {
            try
            {
                crm_tasks DbTask = new crm_tasks();

                var DbTasks = db_ctx.crm_tasks.Where(x => x.activity_id == SfTask.Id);

               if (DbTasks.Any())
                {
                    DbTask = DbTasks.FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbTask.activity_guid = Guid.NewGuid();

                    DbTask.activity_id = SfTask.Id;

                    DbTask.task_created_by = SfTask.CreatedById;
                    if (SfTask.CreatedDateSpecified) DbTask.task_created_datetime = GetLocalTime(SfTask.CreatedDate);

                    db_ctx.crm_tasks.Add(DbTask);
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(DbTask.activity_id = SfTask.Id)) {
                    DbTask.task_deleted = SfTask.IsDeleted ?? false;
                    DbTask.task_description = SfTask.Description;
                    DbTask.task_modified_by = SfTask.LastModifiedById;
                    DbTask.task_account_id = SfTask.AccountId;

                    DbTask.task_call_type = SfTask.CallType;
                    DbTask.task_call_object_identfier = SfTask.CallObject;
                    if (SfTask.CallDurationInSecondsSpecified) DbTask.task_call_duration = SfTask.CallDurationInSeconds;

                    DbTask.task_recurrence_activity_id = SfTask.RecurrenceActivityId;
                    DbTask.task_account_id = SfTask.AccountId;
                    DbTask.task_call_type = SfTask.CallType;
                    DbTask.task_owner_id = SfTask.OwnerId;
                    DbTask.task_priority = SfTask.Priority;
                    DbTask.task_call_object_identfier = SfTask.CallObject;
                    DbTask.task_recurrence_activity_id = SfTask.RecurrenceActivityId;
                    DbTask.task_recurrence_instance = SfTask.RecurrenceInstance;
                    DbTask.task_recurrence_month_of_year = SfTask.RecurrenceMonthOfYear;
                    DbTask.task_recurrence_timezone = SfTask.RecurrenceTimeZoneSidKey;
                    DbTask.task_regenerated_type = SfTask.RecurrenceRegeneratedType;
                    DbTask.task_status = SfTask.Status;
                    DbTask.task_subject = SfTask.Subject;
                    DbTask.task_subtype = SfTask.TaskSubtype;
                    DbTask.task_what_id = SfTask.WhatId;
                    DbTask.task_who_id = SfTask.WhoId;
                    DbTask.task_recurrence_type = SfTask.RecurrenceType;

                    if (SfTask.RecurrenceIntervalSpecified) DbTask.task_recurrence_interval = SfTask.RecurrenceInterval;
                    if (SfTask.RecurrenceEndDateOnlySpecified) DbTask.task_recurrence_end_datetime = GetLocalTime(SfTask.RecurrenceEndDateOnly);
                    if (SfTask.RecurrenceStartDateOnlySpecified) DbTask.task_recurrence_start_datetime = GetLocalTime(SfTask.RecurrenceStartDateOnly);
                    if (SfTask.RecurrenceDayOfMonthSpecified) DbTask.task_recurrence_day_of_month = SfTask.RecurrenceDayOfMonth;
                    if (SfTask.IsRecurrenceSpecified) DbTask.task_is_recurrence = SfTask.IsRecurrence ?? false;

                    if (SfTask.LastModifiedDateSpecified) DbTask.task_modified_datetime = GetLocalTime(SfTask.LastModifiedDate);
                    if (SfTask.CompletedDateTimeSpecified) DbTask.task_completed_datetime = GetLocalTime(SfTask.CompletedDateTime);
                    if (SfTask.SystemModstampSpecified) DbTask.task_system_modstamp = GetLocalTime(SfTask.SystemModstamp);

                    if (SfTask.ActivityDateSpecified) DbTask.tast_activity_datetime = GetLocalTime(SfTask.ActivityDate);
                    if (SfTask.CallDurationInSecondsSpecified) DbTask.task_call_duration = SfTask.CallDurationInSeconds;
                    if (SfTask.CompletedDateTimeSpecified) DbTask.task_completed_datetime = GetLocalTime(SfTask.CompletedDateTime);
                    if (SfTask.IsArchivedSpecified) DbTask.task_archived = SfTask.IsArchived ?? false;
                    if (SfTask.IsClosedSpecified) DbTask.task_closed = SfTask.IsClosed ?? false;
                    if (SfTask.IsHighPrioritySpecified) DbTask.task_high_priority = SfTask.IsHighPriority ?? false;
                    if (SfTask.IsReminderSetSpecified) DbTask.task_reminder_set = SfTask.IsReminderSet ?? false;
                    if (SfTask.IsHighPrioritySpecified) DbTask.task_priority = SfTask.Priority;
                    if (SfTask.IsReminderSetSpecified) DbTask.task_reminder_set = SfTask.IsReminderSet ?? false;
                    if (SfTask.RecurrenceDayOfMonthSpecified) DbTask.task_recurrence_day_of_month = SfTask.RecurrenceDayOfMonth;
                    if (SfTask.RecurrenceDayOfWeekMaskSpecified) DbTask.task_recurrence_day_of_week_mask = SfTask.RecurrenceDayOfWeekMask;
                    if (SfTask.RecurrenceIntervalSpecified) DbTask.task_recurrence_interval = SfTask.RecurrenceInterval;
                    if (SfTask.RecurrenceStartDateOnlySpecified) DbTask.task_recurrence_start_datetime = GetLocalTime(SfTask.RecurrenceStartDateOnly);
                    if (SfTask.ReminderDateTimeSpecified) DbTask.task_reminder_datetime = GetLocalTime(SfTask.ReminderDateTime);

                    DbTask.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveDynamicContent(Result result, lc_dynamic_content__c SfDynamicContent) {
            try
            {
                crm_dynamic_content DbDynamicContent = new crm_dynamic_content();

                var DbDynamicContents = db_ctx.crm_dynamic_content.Where(x => x.crm_dynamic_content_id == SfDynamicContent.Id);

                if (DbDynamicContents.Any())
                {
                    DbDynamicContent = DbDynamicContents.FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbDynamicContent.crm_dynamic_content_guid = Guid.NewGuid();

                    DbDynamicContent.crm_dynamic_content_id = SfDynamicContent.Id;

                    DbDynamicContent.crm_dynamic_content_created_by = SfDynamicContent.CreatedById;
                    if (SfDynamicContent.CreatedDateSpecified) DbDynamicContent.crm_dynamic_content_created_datetime = GetLocalTime(SfDynamicContent.CreatedDate);

                    db_ctx.crm_dynamic_content.Add(DbDynamicContent);
                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(DbDynamicContent.crm_dynamic_content_id = SfDynamicContent.Id))
                {
                    DbDynamicContent.crm_dynamic_content_deleted = SfDynamicContent.IsDeleted ?? false;

                    DbDynamicContent.last_sfsync_datetime = DateTime.Now;

                    if (SfDynamicContent.lc_dynamic_content_available__cSpecified) {
                        DbDynamicContent.crm_dynamic_content_available = SfDynamicContent.lc_dynamic_content_available__c ?? false;
                    }

                    if (!string.IsNullOrWhiteSpace(SfDynamicContent.Name)) {
                        DbDynamicContent.crm_dynamic_content_name = SfDynamicContent.Name;
                    }

                    if (!string.IsNullOrWhiteSpace(SfDynamicContent.lc_dynamic_content_label__c)) {
                        DbDynamicContent.crm_dynamic_content_label = SfDynamicContent.lc_dynamic_content_label__c;
                    }

                    if (!string.IsNullOrWhiteSpace(SfDynamicContent.lc_dynamic_content_name__c)) {
                        DbDynamicContent.crm_dynamic_content_number = SfDynamicContent.lc_dynamic_content_name__c;
                    }

                    if (!string.IsNullOrWhiteSpace(SfDynamicContent.lc_dynamic_content_matching_text__c)) {
                        DbDynamicContent.crm_dynamic_content_matching_text = SfDynamicContent.lc_dynamic_content_matching_text__c;
                    }

                    if (!string.IsNullOrWhiteSpace(SfDynamicContent.lc_dynamic_content_html__c)) {
                        DbDynamicContent.crm_dynamic_content_html = SfDynamicContent.lc_dynamic_content_html__c;
                    }

                    if (SfDynamicContent.LastModifiedDateSpecified)
                    {
                        DbDynamicContent.crm_dynamic_content_last_modified_by = SfDynamicContent.LastModifiedById;
                        DbDynamicContent.crm_dynamic_content_last_modified_datetime = GetLocalTime(SfDynamicContent.LastModifiedDate);
                    }

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveProgramInquiry(Result result, lc_inquiry_program__c SfInquiryProgram)
        {
            try
            {
                crm_inquiry_programs DbInquiryProgram = new crm_inquiry_programs();

                var DbInquiryPrograms = db_ctx.crm_inquiry_programs.Where(x => x.crm_inq_prog_id == SfInquiryProgram.Id);

                if (DbInquiryPrograms.Any())
                {
                    DbInquiryProgram = DbInquiryPrograms.FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    DbInquiryProgram.crm_inq_prog_guid = Guid.NewGuid();

                    DbInquiryProgram.crm_inq_prog_id = SfInquiryProgram.Id;

                    DbInquiryProgram.crm_inq_prog_created_by = SfInquiryProgram.CreatedById;
                    if (SfInquiryProgram.CreatedDateSpecified) {
                        DbInquiryProgram.crm_inq_prog_created_by = Settings.CrmAdmin;
                        DbInquiryProgram.crm_inq_prog_created_datetime = GetLocalTime(SfInquiryProgram.CreatedDate);
                    }

                    db_ctx.crm_inquiry_programs.Add(DbInquiryProgram);

                    result.Inserted = true;
                }

                if (!string.IsNullOrEmpty(DbInquiryProgram.crm_inq_prog_id = SfInquiryProgram.Id))
                {
                    DbInquiryProgram.crm_inq_prog_number = SfInquiryProgram.Name;

                    if (SfInquiryProgram.IsDeletedSpecified) {
                        DbInquiryProgram.crm_inq_prog_deleted = SfInquiryProgram.IsDeleted ?? false;
                    }

                    DbInquiryProgram.crm_inq_prog_inquiry_id = SfInquiryProgram.lc_inquiry__c;
                    DbInquiryProgram.crm_inq_prog_program_id = SfInquiryProgram.lc_program__c;
                    //DbInquiryProgram.crm_inq_prog_term_id = SfInquiryProgram.lc_term__c;

                    if (SfInquiryProgram.lc_inquiry_program_ack_sent__c != null) {
                        DbInquiryProgram.crm_inq_prog_ack_sent = GetLocalTime(SfInquiryProgram.lc_inquiry_program_ack_sent__c);
                    }

                    if (SfInquiryProgram.LastModifiedDateSpecified)
                    {
                        DbInquiryProgram.crm_inq_prog_last_modified_by = Settings.CrmAdmin;
                        DbInquiryProgram.crm_inq_prog_last_modified_datetime = GetLocalTime(SfInquiryProgram.LastModifiedDate ?? DateTime.Now);
                    }

                    DbInquiryProgram.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveShortForm(Result result, ShortFormDataModel ShortForm)
        {
            try
            {
                crm_form_submissions form_submission = new crm_form_submissions();

                var crm_form_submissions = db_ctx.crm_form_submissions.Where(x => x.crm_form_guid == ShortForm.Guid
                                                                                || (x.crm_form_firstname == ShortForm.FirstName
                                                                                && x.crm_form_lastname == ShortForm.LastName
                                                                                && x.crm_form_emailaddress == ShortForm.EmailAddress)).ToList();

                if (crm_form_submissions.Any())
                {
                    form_submission = crm_form_submissions.FirstOrDefault();
                    result.UpdatedCount++;
                }
                else
                {
                    form_submission.crm_form_guid = Guid.NewGuid();

                    form_submission.crm_form_created_datetime = DateTime.Now;

                    db_ctx.crm_form_submissions.Add(form_submission);
                    result.Inserted = true;
                }

                if (form_submission.crm_form_guid != null)
                {
                    form_submission.crm_form_firstname = ShortForm.FirstName;
                    form_submission.crm_form_lastname = ShortForm.LastName;
                    form_submission.crm_form_emailaddress = ShortForm.EmailAddress;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }

                    if (result.Success) {

                        if (ShortForm.Cookie != null) {
                            if (ShortForm.Cookie.visits.Any()) {
                                foreach (visit visit in ShortForm.Cookie.visits)
                                {
                                    crm_cookie_history history = new crm_cookie_history()
                                    {
                                        crm_cookie_uuid = new Guid(ShortForm.Cookie.uuid),
                                        crm_page_visited = visit.page,
                                        crm_created_datetime = visit.date,
                                    };

                                    db_ctx.crm_cookie_history.Add(history);
                                }
                            }
                        }
                    }

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        #endregion Salesforce Centric


        #region Colleague Centric
        // These functions do not update Colleague fields
        // Only update uniquely salesforce fields and identifiers
        // Mark non-existant records for deletion

        private Result SaveColleagueAccount(Result result, Account SfAccount)
        {
            try
            {
                if (SfAccount.RecordTypeId == AccountRecordTypes.AcademicProgram) {
                    crm_accounts crm_account = new crm_accounts();

                    var matches = db_ctx.crm_accounts.Where(x => x.account_id == SfAccount.Id
                                                                && x.account_deleted == false).ToList();

                    if (matches.Any())
                    {
                        crm_account = matches.OrderByDescending(x => x.account_modifed_datetime).FirstOrDefault();
                        result.UpdatedCount++;
                    }
                    else
                    {
                        crm_account.account_guid = Guid.NewGuid();
                        crm_account.account_created_by = SfAccount.CreatedById;
                        if (SfAccount.CreatedDateSpecified) crm_account.account_created_date = GetLocalTime(SfAccount.CreatedDate);

                        db_ctx.crm_accounts.Add(crm_account);
                        result.InsertedCount++;
                    }

                    if (string.IsNullOrEmpty(crm_account.account_id)) {
                        crm_account.account_id = SfAccount.Id;
                    }

                    if (SfAccount.LastActivityDateSpecified) { 
                        crm_account.account_last_activity_date = GetLocalTime(SfAccount.LastActivityDate); 
                    }

                    if (SfAccount.LastModifiedDateSpecified)
                    {
                        crm_account.account_modified_by = SfAccount.LastModifiedById;
                        crm_account.account_modifed_datetime = GetLocalTime(SfAccount.LastModifiedDate);
                    }

                    if (SfAccount.LastReferencedDateSpecified) { 
                        crm_account.account_last_referenced_date = GetLocalTime(SfAccount.LastReferencedDate); 
                    }

                    if (SfAccount.LastViewedDateSpecified) { 
                        crm_account.account_last_viewed_date = GetLocalTime(SfAccount.LastViewedDate); 
                    }

                    if (SfAccount.lc_account_active__cSpecified) { 
                        crm_account.account_active = SfAccount.lc_account_active__c ?? true; 
                    }

                    if (string.IsNullOrEmpty(crm_account.account_owner_id)) {
                        crm_account.account_owner_id = Settings.CrmAdmin;
                    }

                    if (string.IsNullOrEmpty(crm_account.account_record_type_id)) {
                        crm_account.account_record_type_id = SfAccount.RecordTypeId;
                    }

                    crm_account.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveApplication(Result result, lc_application__c SfApplication)
        {
            bool match = false;
            bool update = false;

            try
            {
                crm_applications DbApplication = new crm_applications();

                var applications = db_ctx.crm_applications.Where(x => x.crm_application_id == SfApplication.Id);

                if (applications.Any())
                {
                    DbApplication = applications.OrderByDescending(x => x.appl_modfied_date).FirstOrDefault();

                    if (DbApplication != null && !string.IsNullOrEmpty(SfApplication.Id))
                    {
                        match = true;

                        if (DbApplication.appl_mark_delete == true) {
                            DbApplication.appl_mark_delete = false;
                            update = true;
                        }

                        if (DbApplication.application_deleted == true) {
                            DbApplication.application_deleted = false;
                            update = true;
                        }

                        result.UpdatedCount++;
                    }
                }

                if (!match) {
                    DbApplication.application_guid = Guid.NewGuid();

                    DbApplication.crm_application_id = SfApplication.Id;
                    DbApplication.sis_application_id = SfApplication.lc_application_id__c;

                    DbApplication.crm_appl_number = SfApplication.Name;
                    DbApplication.crm_contact_id = SfApplication.lc_applicant__c;
                    DbApplication.intended_start_term = SfApplication.lc_intended_start_term__c;
                    DbApplication.crm_program_id = SfApplication.lc_academic_program__c;

                    DbApplication.appl_created_by = SfApplication.CreatedById;
                    DbApplication.appl_created_date = GetLocalTime(SfApplication.CreatedDate);

                    DbApplication.appl_mark_delete = true;

                    db_ctx.crm_applications.Add(DbApplication);

                    result.Deleted = true;
                    update = true;
                }

                if (update) {
                    DbApplication.appl_modfied_date = DateTime.Now;
                    DbApplication.appl_modified_by = Settings.CrmAdmin;
                }
                
                DbApplication.last_sfsync_datetime = DateTime.Now;

                if (db_ctx.SaveChanges() > 0)
                {
                    result.Success = true;
                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveColleagueAffiliation(Result result, hed__Affiliation__c SfAffiliation)
        {
            crm_affiliations affiliation = new crm_affiliations();

            try
            {
                DateTime? StartDate = GetLocalTime(SfAffiliation.hed__StartDate__c);

                var crm_affiliations = db_ctx.crm_affiliations.Where(x => (x.affiliation_record_type_id == AffiliationRecordTypes.StudentProgramAffiliation
                                                                            && x.affiliation_contact_id == SfAffiliation.hed__Contact__c
                                                                            && x.affiliation_organization_id == SfAffiliation.hed__Account__c
                                                                        && (x.affiliation_id == SfAffiliation.Id
                                                                        || ((x.lc_student_program_id == SfAffiliation.lc_student_program_id__c
                                                                            && x.lc_student_program_id != null)
                                                                            && (x.affiliation_start_date == SfAffiliation.hed__StartDate__c
                                                                                && SfAffiliation.hed__StartDate__cSpecified == true)))))
                                                              .OrderByDescending(x => x.affiliation_last_modified_datetime)
                                                              .ToList();

                if (!crm_affiliations.Any())
                {
                    affiliation.affiliation_guid = Guid.NewGuid();
                    db_ctx.crm_affiliations.Add(affiliation);
                    result.InsertedCount++;
                }
                else {

                    bool FirstRecord = true;
                    foreach (crm_affiliations aff in crm_affiliations.OrderByDescending(x => x.affiliation_last_modified_datetime)) {

                        if (FirstRecord)
                        {
                            affiliation = aff;
                            FirstRecord = false;
                            result.UpdatedCount++;
                        }
                        else {

                            aff.affiliation_guid_mismatch = true;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(SfAffiliation.Id))
                {
                    affiliation.affiliation_id = SfAffiliation.Id; 
                }

                if (affiliation.affiliation_key != SfAffiliation.Name)
                {
                    affiliation.affiliation_key = SfAffiliation.Name;
                }

                if (SfAffiliation.SystemModstampSpecified)
                {
                    affiliation.affiliation_system_modstamp = GetLocalTime(SfAffiliation.SystemModstamp);
                }

                if (SfAffiliation.LastViewedDateSpecified)
                {
                    affiliation.affiliation_last_viewed_datetime = GetLocalTime(SfAffiliation.LastViewedDate);
                }

                if (SfAffiliation.LastReferencedDateSpecified)
                {
                    affiliation.affiliation_last_referenced_datetime = GetLocalTime(SfAffiliation.LastReferencedDate);
                }

                // Invalid affiliations
                if (SfAffiliation.hed__StartDate__c == null || string.IsNullOrWhiteSpace(SfAffiliation.lc_student_program_id__c))
                {
                    affiliation.affiliation_mark_delete = true;
                    affiliation.affiliation_last_modified_datetime = DateTime.Now;
                }

                affiliation.last_sfsync_datetime = DateTime.Now;

                db_ctx.SaveChanges();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveColleagueContact(Result result, Contact SfContact)
        {

            try
            {
                if (SfContact.RecordTypeId == ContactRecordTypes.DefaultContactRecordType ||
                    SfContact.RecordTypeId == ContactRecordTypes.DuplicateContactRecordType ||
                    SfContact.RecordTypeId == ContactRecordTypes.PrivateContactRecordType) {

                    crm_contacts crm_contact = new crm_contacts();

                    var crm_contacts = db_ctx.crm_contacts.Where(x => x.contact_id == SfContact.Id).ToList();

                    if (crm_contacts.Any())
                    {
                        crm_contact = crm_contacts.OrderByDescending(x => x.contact_last_modified_datetime).FirstOrDefault();

                        if (crm_contact != null && !string.IsNullOrEmpty(SfContact.Id))
                        {

                            if (crm_contact.contact_mark_delete == true)
                            {
                                crm_contact.contact_mark_delete = false;
                            }

                            if (crm_contact.contact_deleted == true)
                            {
                                crm_contact.contact_deleted = false;
                            }

                            if (string.IsNullOrEmpty(crm_contact.contact_id)) {
                                crm_contact.contact_id = SfContact.Id;
                            }
                            
                            crm_contact.contact_record_type_id = SfContact.RecordTypeId;

                            crm_contact.contact_account_id = SfContact.AccountId;

                            crm_contact.contact_owner_id = SfContact.OwnerId;

                            if (SfContact.HasOptedOutOfEmailSpecified) crm_contact.contact_email_opt_out = SfContact.HasOptedOutOfEmail ?? false;
                            if (SfContact.IsEmailBouncedSpecified) crm_contact.contact_is_email_bounced = SfContact.IsEmailBounced ?? false;

                            if (SfContact.LastReferencedDateSpecified) crm_contact.contact_last_referenced_datetime = GetLocalTime(SfContact.LastReferencedDate);
                            if (SfContact.LastViewedDateSpecified) crm_contact.contact_last_viewed_datetime = GetLocalTime(SfContact.LastViewedDate);
                            if (SfContact.LastActivityDateSpecified) crm_contact.contact_last_activity_date = GetLocalTime(SfContact.LastActivityDate);

                            if (SfContact.IsEmailBouncedSpecified) crm_contact.contact_is_email_bounced = SfContact.IsEmailBounced ?? false;
                            if (SfContact.EmailBouncedDateSpecified) crm_contact.contact_email_bounced_date = GetLocalTime(SfContact.EmailBouncedDate);
                            if (SfContact.EmailBouncedReason != null) crm_contact.contact_email_bounced_reason = SfContact.EmailBouncedReason;

                            crm_contact.contact_last_modified_by = SfContact.LastModifiedById;
                            crm_contact.contact_last_modified_datetime = GetLocalTime(SfContact.LastModifiedDate);

                            crm_contact.last_sfsync_datetime = DateTime.Now;

                            db_ctx.crm_contacts.Add(crm_contact);

                            result.UpdatedCount++;
                        }
                    }

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveCourse(Result result, hed__Course__c sf_course)
        {
            bool match = false;
            bool update = false;

            try
            {
                crm_courses DbCourse = new crm_courses();

                var DbCourses = db_ctx.crm_courses.Where(x => x.crm_course_id == sf_course.Id)
                                                   .ToList();

                if (DbCourses.Any())
                {
                    DbCourse = DbCourses.OrderBy(x=>x.course_modified_datetime).FirstOrDefault();

                    if (DbCourse != null && !string.IsNullOrEmpty(DbCourse.crm_course_id)) {

                        match = true;

                        if (DbCourse.course_mark_delete == true)
                        {
                            DbCourse.course_mark_delete = false;
                            update = true;
                        }

                        if (DbCourse.course_deleted == true)
                        {
                            DbCourse.course_deleted = false;
                            update = true;
                        }
                    }

                    result.UpdatedCount++;
                }

                if (!match) {

                    DbCourse.course_guid = Guid.NewGuid();

                    DbCourse.crm_course_id = sf_course.Id;
                    DbCourse.course_number = sf_course.hed__Course_ID__c;
                    DbCourse.course_name = sf_course.Name;
                    DbCourse.course_description = sf_course.hed__Description__c;
                    DbCourse.course_desc_extended = sf_course.hed__Extended_Description__c;

                    DbCourse.course_created_by = sf_course.CreatedById;
                    DbCourse.course_created_datetime = GetLocalTime(sf_course.CreatedDate);

                    DbCourse.course_department = sf_course.hed__Account__c;

                    DbCourse.course_mark_delete = true;

                    db_ctx.crm_courses.Add(DbCourse);

                    result.Deleted = true;
                    update = true;
                }

                if (update)
                {
                    DbCourse.course_modified_datetime = DateTime.Now;
                    DbCourse.course_modified_by = Settings.CrmAdmin;
                }

                DbCourse.last_sfsync_datetime = DateTime.Now;

                if (db_ctx.SaveChanges() > 0)
                {
                    result.Success = true;
                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveCourseConnection(Result result, hed__Course_Enrollment__c sf_course_enrollment)
        {
            bool match = false;
            bool update = false;

            try
            {
                crm_course_connections DbCourseConnection = new crm_course_connections();

                var DbCourseConnections = db_ctx.crm_course_connections.Where(x => x.course_connection_id == DbCourseConnection.course_connection_id)
                                                                         .ToList();

                if (DbCourseConnections.Any())
                {
                    DbCourseConnection = DbCourseConnections.OrderBy(x => x.contact_last_modified_datetime).FirstOrDefault();

                    if (DbCourseConnection != null && !string.IsNullOrEmpty(DbCourseConnection.course_connection_id))
                    {

                        match = true;

                        if (DbCourseConnection.course_connection_mark_delete == true)
                        {
                            DbCourseConnection.course_connection_mark_delete = false;
                            update = true;
                        }

                        if (DbCourseConnection.course_connection_deleted == true)
                        {
                            DbCourseConnection.course_connection_deleted = false;
                            update = true;
                        }
                    }

                    result.UpdatedCount++;
                }

                if (!match)
                {
                    DbCourseConnection.course_connection_guid = Guid.NewGuid();
                    DbCourseConnection.course_connection_id = sf_course_enrollment.Id;

                    DbCourseConnection.course_connection_affiliation_id = sf_course_enrollment.hed__Affiliation__c;
                    DbCourseConnection.course_connection_program_id = sf_course_enrollment.hed__Program_Enrollment__c;
                    DbCourseConnection.course_connection_program_enrollment_id = sf_course_enrollment.hed__Program_Enrollment__c;
                    DbCourseConnection.course_connection_name = sf_course_enrollment.Name;
                    DbCourseConnection.course_offering_id = sf_course_enrollment.hed__Course_Offering__c;
                    DbCourseConnection.course_credits_attempted = (decimal)sf_course_enrollment.hed__Credits_Attempted__c;
                    DbCourseConnection.course_credits_earned = (decimal)sf_course_enrollment.hed__Credits_Earned__c;
                    DbCourseConnection.course_grade = (decimal)sf_course_enrollment.hed__Grade__c;

                    DbCourseConnection.course_connection_record_type = sf_course_enrollment.RecordTypeId;

                    DbCourseConnection.course_connection_created_by = sf_course_enrollment.CreatedById;
                    DbCourseConnection.course_connection_created_datetime = GetLocalTime(sf_course_enrollment.CreatedDate);

                    DbCourseConnection.course_connection_mark_delete = true;

                    db_ctx.crm_course_connections.Add(DbCourseConnection);

                    result.Deleted = true;
                    update = true;

                }

                if (update)
                {
                    DbCourseConnection.course_connection_modified_by = Settings.CrmAdmin;
                    DbCourseConnection.course_connection_modified_datetime = DateTime.Now;
                }

                DbCourseConnection.last_sfsync_datetime = DateTime.Now;

                if (db_ctx.SaveChanges() > 0)
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveCourseOffering(Result result, hed__Course_Offering__c SfCourseOffering)
        {
            bool match = false;
            bool update = false;

            try
            {
                crm_course_offerings DbCourseOffering = new crm_course_offerings();

                var DbCourseOfferings = db_ctx.crm_course_offerings.Where(x => x.sis_course_section_id == SfCourseOffering.hed__Section_ID__c
                                                                            && x.course_offering_deleted == false
                                                                            && x.course_offering_mark_delete == false)
                                                                  .ToList();

                if (DbCourseOfferings.Any())
                {
                    DbCourseOffering = DbCourseOfferings.OrderBy(x => x.course_offering_modified_datetime).FirstOrDefault();

                    if (DbCourseOffering != null && !string.IsNullOrEmpty(DbCourseOffering.course_offering_id))
                    {
                        match = true;

                        if (DbCourseOffering.course_offering_mark_delete == true)
                        {
                            DbCourseOffering.course_offering_mark_delete = false;
                            update = true;
                        }

                        if (DbCourseOffering.course_offering_deleted == true)
                        {
                            DbCourseOffering.course_offering_deleted = false;
                            update = true;
                        }
                    }

                    result.UpdatedCount++;
                }

                if (!match) {
                    DbCourseOffering.course_offering_guid = Guid.NewGuid();
                    DbCourseOffering.course_offering_id = SfCourseOffering.Id;

                    DbCourseOffering.course_offering_capacity = (decimal)SfCourseOffering.hed__Capacity__c;
                    DbCourseOffering.course_offering_course = SfCourseOffering.hed__Course__c;
                    DbCourseOffering.course_offering_name = SfCourseOffering.Name;
                    DbCourseOffering.sis_course_section_id = SfCourseOffering.hed__Section_ID__c;
                    DbCourseOffering.course_offering_term = SfCourseOffering.hed__Term__c;
                    DbCourseOffering.course_offering_primary_faculty = SfCourseOffering.hed__Faculty__c;

                    DbCourseOffering.course_offering_created_by = SfCourseOffering.CreatedById;
                    DbCourseOffering.course_offering_created_datetime = GetLocalTime(SfCourseOffering.CreatedDate);

                    db_ctx.crm_course_offerings.Add(DbCourseOffering);

                    result.Deleted = true;
                    update = true;
                }

                if (update) {
                    DbCourseOffering.course_offering_modified_by = Settings.CrmAdmin;
                    DbCourseOffering.course_offering_modified_datetime = DateTime.Now ;
                }
                
                DbCourseOffering.last_sfsync_datetime = DateTime.Now;

                if (db_ctx.SaveChanges() > 0)
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveAcademicProgram(Result result, Account SfAccount)
        {

            try
            {
                if (SfAccount.RecordTypeId == AccountRecordTypes.AcademicProgram) {
                    crm_accounts DbProgram = new crm_accounts();

                    var DbPrograms = db_ctx.crm_accounts.Where(x => x.account_id == SfAccount.Id
                                                                && x.account_deleted == false
                                                                && x.account_mark_delete == false)
                                                          .ToList();
                    if (DbPrograms.Any())
                    {
                        DbProgram = DbPrograms.OrderBy(x => x.account_modifed_datetime).FirstOrDefault();

                        if (DbProgram != null && !string.IsNullOrEmpty(DbProgram.account_id))
                        {
                            result.UpdatedCount++;
                        }
                        else {
                            DbProgram.account_guid = Guid.NewGuid();
                            DbProgram.account_id = SfAccount.Id;

                            DbProgram.account_number = SfAccount.AccountNumber;
                            DbProgram.account_name = SfAccount.Name;

                            DbProgram.account_type = SfAccount.lc_account_type__c;
                            DbProgram.account_record_type_id = SfAccount.RecordTypeId;

                            DbProgram.account_phone = SfAccount.Phone;

                            DbProgram.account_owner_id = SfAccount.OwnerId;

                            DbProgram.account_parent_account_id = SfAccount.ParentId;

                            DbProgram.account_created_by = SfAccount.CreatedById;
                            DbProgram.account_created_date = GetLocalTime(SfAccount.CreatedDate);

                            db_ctx.crm_accounts.Add(DbProgram);

                            result.InsertedCount++;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(SfAccount.lc_account_name_alias__c)) {
                        DbProgram.account_name_alias = SfAccount.lc_account_name_alias__c;
                    }

                   // if (SfAccount.lc_rcr_show_web__cSpecified) {
                        DbProgram.account_show_web = SfAccount.lc_rcr_show_web__c ?? false;
                   // }

                    DbProgram.account_last_referenced_date = GetLocalTime(SfAccount.LastReferencedDate);
                    DbProgram.account_last_viewed_date = GetLocalTime(SfAccount.LastViewedDate);

                    DbProgram.account_modified_by = SfAccount.LastModifiedById;
                    DbProgram.account_modifed_datetime = GetLocalTime(SfAccount.LastModifiedDate);

                    DbProgram.account_system_modstamp = GetLocalTime(SfAccount.SystemModstamp);

                    DbProgram.last_sfsync_datetime = DateTime.Now;

                    if (db_ctx.SaveChanges() > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveProgramEnrollment(Result result, hed__Program_Enrollment__c SfProgram_enrollment)
        {
            bool match = false;
            bool update = false;

            try
            {
                crm_program_enrollments DbProgram_enrollment = new crm_program_enrollments();

                var DbProgram_enrollments = db_ctx.crm_program_enrollments.Where(x => x.crm_program_enrollment_id == SfProgram_enrollment.Id)
                                                                     .ToList();
                
                if (DbProgram_enrollments.Any())
                {
                    DbProgram_enrollment = DbProgram_enrollments.OrderByDescending(x => x.crm_program_enrollment_modified_datetime).FirstOrDefault();

                    if (DbProgram_enrollment != null && !string.IsNullOrEmpty(DbProgram_enrollment.crm_program_enrollment_id))
                    {
                        match = true;

                        if (DbProgram_enrollment.crm_program_enrollment_mark_delete == true)
                        {
                            DbProgram_enrollment.crm_program_enrollment_mark_delete = false;
                            update = true;
                        }

                        if (DbProgram_enrollment.crm_program_enrollment_deleted == true)
                        {
                            DbProgram_enrollment.crm_program_enrollment_deleted = false;
                            update = true;
                        }
                    }

                    result.UpdatedCount++;
                }

                if (!match) {

                    DbProgram_enrollment.crm_program_enrollment_guid = Guid.NewGuid();

                    DbProgram_enrollment.crm_program_enrollment_id = SfProgram_enrollment.Id;

                    DbProgram_enrollment.crm_program_enrollment_contact_id = SfProgram_enrollment.hed__Contact__c;
                    DbProgram_enrollment.crm_program_enrollment_program_id = SfProgram_enrollment.hed__Account__c;

                    DbProgram_enrollment.crm_program_enrollment_key = SfProgram_enrollment.Name;
                    DbProgram_enrollment.crm_program_enrollment_affiliation = SfProgram_enrollment.hed__Affiliation__c;

                    DbProgram_enrollment.crm_program_enrollment_created_by = SfProgram_enrollment.CreatedById ?? Settings.CrmAdmin;
                    DbProgram_enrollment.crm_program_enrollment_created_datetime = GetLocalTime(SfProgram_enrollment.CreatedDate);

                    if (SfProgram_enrollment.hed__Start_Date__cSpecified)
                    {
                        DbProgram_enrollment.crm_program_enrollment_start_datetime = GetLocalTime(SfProgram_enrollment.hed__Start_Date__c);
                    }

                    if (SfProgram_enrollment.hed__End_Date__cSpecified)
                    {
                        DbProgram_enrollment.crm_program_enrollment_end_datetime = GetLocalTime(SfProgram_enrollment.hed__End_Date__c);
                    }

                    if (SfProgram_enrollment.LastViewedDateSpecified)
                    {
                        DbProgram_enrollment.crm_program_enrollment_last_viewed_date = GetLocalTime(SfProgram_enrollment.LastViewedDate);
                    }

                    if (SfProgram_enrollment.LastReferencedDateSpecified)
                    {
                        DbProgram_enrollment.crm_program_enrollment_last_referenced_date = GetLocalTime(SfProgram_enrollment.LastReferencedDate);
                    }

                    DbProgram_enrollment.crm_program_enrollment_mark_delete = true;

                    db_ctx.crm_program_enrollments.Add(DbProgram_enrollment);

                    result.Deleted = true;
                    update = true;
                }

                if (update)
                {
                    DbProgram_enrollment.crm_program_enrollment_modified_by = Settings.CrmAdmin;
                    DbProgram_enrollment.crm_program_enrollment_modified_datetime = DateTime.Now;
                }

                DbProgram_enrollment.last_sfsync_datetime = DateTime.Now;

                if (db_ctx.SaveChanges() > 0)
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        private Result SaveTerm(Result result, hed__Term__c SfTerm)
        {
            bool match = false;
            bool update = false;

            try
            {
                crm_terms crm_term = new crm_terms();

                var crm_terms = db_ctx.crm_terms.Where(x => x.term_id == crm_term.term_id)
                                                .ToList();

                

                if (crm_terms.Any())
                {
                    crm_term = crm_terms.OrderByDescending(x => x.term_modifed_datetime).FirstOrDefault();

                    if (crm_term != null && !string.IsNullOrEmpty(crm_term.term_id))
                    {
                        match = true;

                        if (crm_term.term_mark_delete == true)
                        {
                            crm_term.term_mark_delete = false;
                            update = true;
                        }

                        if (crm_term.term_deleted == true)
                        {
                            crm_term.term_deleted = false;
                            update = true;
                        }
                    }

                    result.UpdatedCount++;
                }

                if (!match) {

                    crm_term.term_id = SfTerm.Id;
                    crm_term.term_guid = Guid.NewGuid();

                    crm_term.term_deleted = SfTerm.IsDeleted ?? false;
                    crm_term.term_name = SfTerm.Name;
                    crm_term.term_modified_by = SfTerm.LastModifiedById;
                    crm_term.term_account_id = SfTerm.hed__Account__c;
                    crm_term.term_parent_term_id = SfTerm.hed__Parent_Term__c;
                    crm_term.term_type = SfTerm.hed__Type__c;
                    crm_term.term_code = SfTerm.term_id__c;

                    if (SfTerm.SystemModstampSpecified) crm_term.term_system_modstamp = GetLocalTime(SfTerm.SystemModstamp);
                    if (SfTerm.LastViewedDateSpecified) crm_term.term_last_viewed_datetime = GetLocalTime(SfTerm.LastViewedDate);
                    if (SfTerm.LastReferencedDateSpecified) crm_term.term_last_referenced_datetime = GetLocalTime(SfTerm.LastReferencedDate);
                    if (SfTerm.hed__End_Date__cSpecified) crm_term.term_end_date = GetLocalTime(SfTerm.hed__End_Date__c);
                    if (SfTerm.hed__Start_Date__cSpecified) crm_term.term_start_date = GetLocalTime(SfTerm.hed__Start_Date__c);
                    if (SfTerm.hed__Grading_Period_Sequence__cSpecified) crm_term.term_grade_period_sequence = (Decimal)SfTerm.hed__Grading_Period_Sequence__c;
                    if (SfTerm.hed__Instructional_Days__cSpecified) crm_term.term_instructional_days = (Decimal)SfTerm.hed__Instructional_Days__c;

                    if (SfTerm.lc_rcr_show_web__cSpecified) crm_term.term_show_web = SfTerm.lc_rcr_show_web__c;

                    if (SfTerm.IsDeletedSpecified)
                    {
                        crm_term.term_mark_delete = SfTerm.IsDeleted ?? false;
                    }

                    if (string.IsNullOrWhiteSpace(SfTerm.lc_term_name_alias__c)) {
                        crm_term.term_name_alias = SfTerm.lc_term_name_alias__c;
                    }

                    crm_term.term_created_by = SfTerm.CreatedById;
                    crm_term.term_created_datetime = GetLocalTime(SfTerm.CreatedDate);

                    db_ctx.crm_terms.Add(crm_term);
                    result.Deleted = false;
                    update = true;
                }

                if (update) {
                    crm_term.term_modified_by = Settings.CrmAdmin;
                    crm_term.term_modifed_datetime = GetLocalTime(SfTerm.LastModifiedDate);
                }

                crm_term.last_sfsync_datetime = DateTime.Now;

                if (db_ctx.SaveChanges() > 0)
                {
                    result.Success = true;
                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg += ex.ToString();
                RecordError(GetCurrentMethod(), result.ErrorMsg);
            }

            return result;
        }

        #endregion Colleague Centric
    }
}
