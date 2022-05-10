using CrmCore;
using lc.crm.api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrmSfApi
{
    public class ApiService : IDisposable
    {
        public static Dictionary<Guid, List<sObject>> asyncResults;

        private SforceService salesforceService;
        const int defaultTimeout = 30000;

        public ApiService()
        {
            salesforceService = new SforceService();
            salesforceService.Timeout = defaultTimeout;
            asyncResults = new Dictionary<Guid, List<sObject>>();
        }

        public ApiService(int timeout) : this()
        {
            salesforceService.Timeout = timeout;
        }

        public List<T> Query<T>(string soql) where T : sObject, new()
        {
            List<T> returnList = new List<T>();

            QueryOptions qo = new QueryOptions();
            qo.batchSize = 1000;
            qo.batchSizeSpecified = true;
            salesforceService.QueryOptionsValue = qo;

            SetupService();

            QueryResult results = salesforceService.query(soql);

            Boolean done = false;
            if (results.size > 0)
            {
                while (!done)
                {
                    for (int i = 0; i < results.records.Length; i++)
                    {
                        T item = results.records[i] as T;

                        if (item != null)
                            returnList.Add(item);
                    }

                    if (results.done)
                    {
                        done = true;
                    }
                    else
                    {
                        results = salesforceService.queryMore(results.queryLocator);
                    }
                }
            }

            return returnList;
        }

        public List<T> QueryAll<T>(string soql) where T : sObject, new()
        {
            List<T> returnList = new List<T>();

            SetupService();

            QueryOptions qo = new QueryOptions();
            qo.batchSize = 1000;
            qo.batchSizeSpecified = true;
            salesforceService.QueryOptionsValue = qo;

            QueryResult results = salesforceService.queryAll(soql);

            Boolean done = false;
            if (results.size > 0)
            {
                while (!done)
                {
                    for (int i = 0; i < results.records.Length; i++)
                    {
                        T item = results.records[i] as T;

                        if (item != null)
                            returnList.Add(item);
                    }

                    if (results.done)
                    {
                        done = true;
                    }
                    else
                    {
                        results = salesforceService.queryMore(results.queryLocator);
                    }
                }
            }

            return returnList;
        }

        public T QuerySingle<T>(string soql) where T : sObject, new()
        {
            T returnValue = new T();

            SetupService();

            QueryResult results = salesforceService.query(soql);

            if (results.size == 1)
                returnValue = results.records[0] as T;

            return returnValue;
        }

        public Guid QueryAsync(string soql)
        {
            SetupService();
            salesforceService.queryCompleted += salesforceService_queryCompleted;
            
            Guid id = Guid.NewGuid();

            salesforceService.queryAsync(soql, id);

            return id;
        }

        void salesforceService_queryCompleted(object sender, queryCompletedEventArgs e)
        {
            Guid id = (Guid)e.UserState;
            List<sObject> results = e.Result.records.ToList();

            if (asyncResults.ContainsKey(id))
                asyncResults[id].AddRange(results);
            else
                asyncResults.Add((Guid)e.UserState, results);
        }
   
        public Dictionary<string, SaveResult> Upsert(Dictionary<string,sObject> items)
        {
            String ErrorMsg = string.Empty;
            Dictionary<string, SaveResult> SaveResults = new Dictionary<string, SaveResult>();

            try
            {
                SetupService();

                var updates = items.Where(x => !string.IsNullOrWhiteSpace(x.Value.Id));

                if (updates != null && updates.Any()) {
                    var results = salesforceService.update(updates.Select(z => z.Value).ToArray());

                    if (results != null && results.Any())
                    {
                        for (int b = 0; b < results.Length; b += 1)
                        {
                            SaveResults.Add(updates.ToArray()[b].Key, results[b]);
                        }
                    }
                }

                var inserts = items.Where(x => string.IsNullOrWhiteSpace(x.Value.Id));
                if (inserts != null && inserts.Any()) {
                    var results = salesforceService.create(inserts.Select(z => z.Value).ToArray());

                    if (results != null && results.Any())
                    {
                        for (int b = 0; b < results.Length; b += 1)
                        {
                            SaveResults.Add(inserts.ToArray()[b].Key, results[b]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg += ex.ToString();
            }

            //RecordError(GetCurrentMethod(), ErrorMsg);

            return SaveResults;
        }

        //public UpsertResult[] Upsert(string externalID, sObject[] items)
        //{
        //    SetupService();

        //    return salesforceService.upsert(externalID, items);
        //}

        public SaveResult[] Insert(sObject[] items)
        {
            string ErrorMsg = string.Empty;

            SetupService();

            SaveResult[] results = salesforceService.create(items);

            return results;
        }

        public SaveResult[] Update(sObject[] arry)
        {
            List<SaveResult> results = new List<SaveResult>();

            SetupService();

            for (int i = 0; i < arry.Length; i = i + 100)
            {
                var items = arry.Skip(i).Take(100);

                SaveResult[] batch_results = salesforceService.update(items.ToArray());

                results.AddRange(batch_results);
            }

            return results.ToArray();
        }

        public DeleteResult[] Delete(string[] ids)
        {
            SetupService();

            return salesforceService.delete(ids);
        }

        public UndeleteResult[] Undelete(string[] ids)
        {
            SetupService();

            return salesforceService.undelete(ids);
        }

        private void SetupService()
        {
            ForceConnection connection = new ForceConnection("SFNAPI");
            salesforceService.SessionHeaderValue = new SessionHeader() { sessionId = connection.SessionID };

            salesforceService.Url = connection.ServerUrl;
        }

        public void SF_SendSingleEmail()
        {
            SetupService();

            // Single Email
            SingleEmailMessage message = new SingleEmailMessage();
            message.toAddresses = new string[] { "0031U00000LO9XmQAL" };
            message.optOutPolicy = SendEmailOptOutPolicy.FILTER;
            message.subject = "Test Subject";
            message.plainTextBody = "This is the message body.";
            SingleEmailMessage[] messages = { message };
            SendEmailResult[] results = salesforceService.sendEmail(messages);
            if (results != null && !results[0].success)
            {
                string ErrorMsg = "The email failed to send: "+ results[0].errors[0].message;
            }

            
        }

        public void SF_SendMassEmail()
        {
            SetupService();

            // Mass Email
            MassEmailMessage massMessage = new MassEmailMessage();
            //massMessage.bccSender = true;
            massMessage.emailPriority = EmailPriority.Normal;
            massMessage.replyTo = "william.mansano@lethbridgecollege.ca";
            massMessage.templateId = "00X1U000000dOPlUAM";
            massMessage.senderDisplayName = "Lethbridge College";
            massMessage.description = "Mass Email Test";
            massMessage.targetObjectIds = new string[] { "0031U00000LO9XmQAL" };  // List of Recipient/Contact Ids

            MassEmailMessage[] massMessages = { massMessage };
            SendEmailResult[] massResults = salesforceService.sendEmail(massMessages);
            if (massResults != null && !massResults[0].success)
            {
                string ErrorMsg = "The email failed to send: " + massResults[0].errors[0].message;
            }
        }

        public void  Dispose()
        {
            salesforceService.Dispose();
        }
    }
}