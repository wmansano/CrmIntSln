using lc.crm;
using System;
using System.Globalization;
using System.Linq;
using static CrmCore.Enumerations;

namespace CrmCore
{
    public partial class CrmCoreLogic : IDisposable
    {
        private readonly crmdb_entities db_ctx = new crmdb_entities();
        readonly IFormatProvider CurrentDateFormat = new CultureInfo("en-US");
        private readonly string SecurityToken = "6de89ae2-19b8-40c2-9a49-58248d18d36e60dcdb34-092c-40a3-b5dd-44372230da34";

        #region ProcessManagement
        public bool MonitorExecution(string method, int count)
        {
            bool success = false;
            string ErrorMsg = string.Empty;
            DateTime StartTime = DateTime.Now;
            String MethodName = string.Empty;

            try
            {
                MethodName = GetCurrentMethod();

                if (count > 1)
                {
                    success = true;
                }

                success = true;
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return success;
        }

        public bool SystemReady()
        {
            //int InsertedCount = 0;
            //int UpdatedCount = 0;
            //int TotalCount = 0;

            bool success = true;
            //string ErrorMsg = string.Empty;
            //string MethodName = string.Empty;
            //DateTime StartTime = DateTime.Now;
            //string transactionMessage = "Transaction Attempt";
            //transaction_logs log = new transaction_logs();
            //Guid TestTransactionGuid = Guid.NewGuid();
            //try
            //{
            //    MethodName = GetCurrentMethod();

            //    log.transaction_id = TestTransactionGuid;
            //    log.transaction_name = "TransactionTest";
            //    log.last_executed = StartTime;
            //    log.transaction_notes = transactionMessage;
            //    log.records_total = 0;
            //    log.records_updated = 0;
            //    log.records_inserted = 0;
            //    log.transaction_successful = false;
            //    log.run_time = Math.Round(((TimeSpan)(DateTime.Now - StartTime)).TotalSeconds, 2);

            //    db_ctx.transaction_logs.Add(log);
            //    int SavedRecords = db_ctx.SaveChanges();

            //    //if (SavedRecords > 0) {
            //    //    transaction_logs transaction_log = db_ctx.transaction_logs.Where(x => x.transaction_id == TestTransactionGuid).FirstOrDefault();

            //    //    if (transaction_log != null) {

            //    //        transaction_log.transaction_successful = true;

            //    //        int updated_log = db_ctx.SaveChanges();

            //    //        if (updated_log > 0) {
            //    //            SendEmail sendEmail = new SendEmail()
            //    //            {
            //    //                Body = "<html><head><title>CRM Integration Tests Successful</title></head><body><table><tr><td>",
            //    //                Title = "CRM Integration Tests Successful",
            //    //                To = "patrick.dudley@lethbridgecollege.ca",
            //    //                From = "crmadmin@lethbridgecollege.ca",
            //    //            };

            //    //            sendEmail.Body += "<br /><br />";
            //    //            sendEmail.Body += "<label>CRM Integration Test Successful</label><br /><br /><br/>";
            //    //            sendEmail.Body += "<label>CRMdb connection i.e. read/write successful</label><br />";
            //    //            sendEmail.Body += "<label>Email functionality Successful</label><br />";
            //    //            sendEmail.Body += "<br /><br />";
            //    //            sendEmail.Body += "</td></tr></table></body></html>";

            //    //            success = sendEmail.Send();
            //    //        }
            //    //    }
            //    //}
            //    success = true;
            //}
            //catch (Exception ex)
            //{
            //    ErrorMsg = ex.ToString();
            //}

            // RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return success;
        }

        #endregion End ProcessManagement

        public bool ProcessLandingForms()
        {
            bool Execute = false;
            bool Success = false;
            string MethodName = string.Empty;
            DateTime BackLoadPriorDate = DateTime.Now;

            try
            {
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.ProcessForms).Select(y => y.crm_setting_bool).FirstOrDefault();

                    if (Execute)
                    {
                        Success = ProcessFormSubmissions();
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool RunCrmDevelopment()
        {
            int InsertedCount = 0;
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool Success = false;
            bool Execute = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            DateTime BackLoadPriorDate = DateTime.Now;
            //int MaxRecordCount = 50000;
            try
            {
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmDevelopment).Select(y => y.crm_setting_bool).FirstOrDefault();

                    if (Execute)
                    {
                        //RunDownloads();

                        //RunBarcodeCreation();

                        //ScheduleBroadcasts();

                        //ScheduleEmailMessages();

                        //SendEmailMessages();

                        //RunSalesforceDeletion();

                        //RunUpserts();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return Success;

        }

        public bool RunIntegration()
        {
            bool Success = false;
            bool Execute = false;
            string MethodName = string.Empty;

            try
            {
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmIntegration).Select(y => y.crm_setting_bool).FirstOrDefault();


                    if (Execute)
                    {

                        RunDownloads();

                        //RunColleagueSyncronization();

                        RunBarcodeCreation();

                        ScheduleBroadcasts();

                        ScheduleEmailMessages();

                        SendEmailMessages();

                        RunSalesforceDeletion();

                        RunUpserts();
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool RunDownloads()
        {
            bool Execute = false;
            bool Success = false;
            bool BackLoad = false;
            int BatchSize = 1000;
            int PriorDays = 30;
            string MethodName = string.Empty;
            DateTime BackLoadPriorDate = DateTime.Now;

            try
            {
                
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmDownloads).Select(y => y.crm_setting_bool).FirstOrDefault();
                    BackLoad = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmBackLoad).Select(y => y.crm_setting_bool).FirstOrDefault();
                    BatchSize = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.BackLoadBatchSize).Select(y => y.crm_setting_int).FirstOrDefault() ?? 0;
                    BackLoadPriorDate = DateTime.Now.AddDays(-db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.BackLoadPriorDays).Select(y => y.crm_setting_int).FirstOrDefault() ?? PriorDays);

                    if (Execute) {
                        Success = DownloadSalesforce(BackLoad, BackLoadPriorDate, BatchSize);
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool RunBarcodeCreation()
        {
            bool Success = false;
            bool Execute = false;
            string MethodName = string.Empty;

            try
            {
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmBarcodes).Select(y => y.crm_setting_bool).FirstOrDefault();

                    if (Execute)
                    {
                        Success = GenerateContactIdBarCodes();
                    }

                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool RunEvents()
        {
            bool Success = false;
            bool Execute = false;
            string MethodName = string.Empty;

            try
            {
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmEvents).Select(y => y.crm_setting_bool).FirstOrDefault();

                    if (Execute)
                    {
                        Success = ProcessEventRegistrations();
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool ScheduleBroadcasts()
        {
            bool Execute = false;
            bool Success = false;
            string MethodName = string.Empty;
            DateTime BackLoadPriorDate = DateTime.Now;

            try
            {

                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.ScheduleEmailBroadcasts).Select(y => y.crm_setting_bool).FirstOrDefault();

                    if (Execute)
                    {
                        Success = CreateEmailBroadcasts();
                        Success = UpsertEmailBroadcasts(false, DateTime.Now, 100);
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool ScheduleBroadcastEmails()
        {
            bool Execute = false;
            bool Success = false;
            string MethodName = string.Empty;
            DateTime BackLoadPriorDate = DateTime.Now;

            try
            {

                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.ScheduleBroadcastEmails).Select(y => y.crm_setting_bool).FirstOrDefault();

                    if (Execute)
                    {
                        Success = ScheduleEmailMessages();
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool SendBroadcastEmails()
        {
            bool Execute = false;
            bool Success = false;
            string MethodName = string.Empty;
            DateTime BackLoadPriorDate = DateTime.Now;

            try
            {

                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.SendBroadcastEmails).Select(y => y.crm_setting_bool).FirstOrDefault();

                    if (Execute)
                    {
                        Success = SendEmailMessages();
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool RunUpserts()
        {
            bool Execute = false;
            bool Success = false;
            bool BackLoad = false;
            Int32 PriorDays = 30;
            int BatchSize = 500;
            string MethodName = string.Empty;
            DateTime BackLoadPriorDate = DateTime.Now;

            try
            {
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmDownloads).Select(y => y.crm_setting_bool).FirstOrDefault();
                    BackLoad = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmBackLoad).Select(y => y.crm_setting_bool).FirstOrDefault();
                    BatchSize = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.BackLoadBatchSize).Select(y => y.crm_setting_int).FirstOrDefault() ?? 0;
                    BackLoadPriorDate = DateTime.Now.AddDays(-db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.BackLoadPriorDays).Select(y => y.crm_setting_int).FirstOrDefault() ?? PriorDays);

                    if (Execute)
                    {
                        Success = UpsertSalesforce(BackLoad, BackLoadPriorDate, BatchSize);
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool RunDeletions()
        {
            bool Success = false;
            bool Execute = false;
            string MethodName = string.Empty;

            try
            {
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    Execute = db_ctx.crm_settings.Where(x => x.crm_setting_name == DbSettings.RunCrmDeletions).Select(y => y.crm_setting_bool).FirstOrDefault();

                    if (Execute) {
                        Success = RunSalesforceDeletion();
                    }
                }
            }
            catch (Exception ex)
            {
                RecordError(MethodName, ex.ToString());
            }

            return Success;
        }

        public bool RunTestInquiryPrograms(string InquiryId)
        {
            int InsertedCount = 0;
            int UpdatedCount = 0;
            int TotalCount = 0;

            bool Success = false;
            string ErrorMsg = string.Empty;
            string MethodName = string.Empty;
            DateTime StartTime = DateTime.Now;
            DateTime BackLoadPriorDate = DateTime.Now;
            //int MaxRecordCount = 50000;
            try
            {
                MethodName = GetCurrentMethod();

                if (SystemReady())
                {
                    TestUpsertInquiryPrograms(InquiryId, BackLoadPriorDate, 100);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }

            RecordTransaction(MethodName, StartTime, TotalCount, InsertedCount, UpdatedCount, ErrorMsg);

            return Success;

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db_ctx.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
