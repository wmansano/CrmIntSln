using CrmCore;
using CrmLcLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;

namespace CrmIntWs
{
    /// <summary>
    /// Summary description for services
    /// </summary>
    [WebService(Namespace = "https://www.lethbridgecollege.ca")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class services : WebService
    {
        private readonly CrmCoreLogic CoreLogic = new CrmCoreLogic();
        private readonly string SecurityToken = "6de89ae2-19b8-40c2-9a49-58248d18d36e60dcdb34-092c-40a3-b5dd-44372230da34";
        private readonly string sfcs = ConfigurationManager.ConnectionStrings["SFNAPI"].ConnectionString;
        private readonly string dbcs = ConfigurationManager.ConnectionStrings["crmdb_entities"].ConnectionString;

        //[WebMethod]
        //public bool Connected()
        //{
        //    return true;
        //}

        [WebMethod]
        public void ProcessLandingForms()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.ProcessLandingForms();
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void RunCrmDevelopment()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.RunCrmDevelopment();
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void RunIntegration()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.RunIntegration();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void RunDownloads()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.RunDownloads();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void RunBarcodes()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.RunBarcodeCreation();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void RunEvents()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.RunEvents();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void ScheduleEmailBroadcasts()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.ScheduleBroadcasts();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void ScheduleBroadcastEmails()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.ScheduleBroadcastEmails();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void SendBroadcastEmails()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.SendBroadcastEmails();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void RunUpserts()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.RunUpserts();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        [WebMethod]
        public void RunDeletions()
        {
            try
            {
                if (CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.RunDeletions();
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }


        [WebMethod]
        public void RunTestInquiryPrograms(string InquiryId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(InquiryId) && CoreLogic.VerifyEnvironment(sfcs, dbcs))
                {
                    CoreLogic.RunTestInquiryPrograms(InquiryId);
                }

            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        #region ScannerWebMethods

        [WebMethod]
        public EventListObj GetEvents(string Token)
        {
            EventListObj Events = new EventListObj();

            try
            {
                if (Token == SecurityToken)
                {
                    Events.Departments = new List<DepartmentObj>();

                    var Departments = CoreLogic.GetDepartments();

                    List<DepartmentObj> DepartmentObjects = new List<DepartmentObj>();

                    if (Departments != null)
                    {

                        foreach (string dep in Departments)
                        {
                            if (!string.IsNullOrEmpty(dep))
                            {
                                DepartmentObj departmentObj = new DepartmentObj() { DepartmentName = dep };
                                List<EventObj> eventObjs = new List<EventObj>();
                                // Load Events
                                Dictionary<string, string> events = CoreLogic.GetEvents(Token, dep);
                                foreach (var eventItem in events)
                                {

                                    if (!string.IsNullOrEmpty(eventItem.Value))
                                    {
                                        eventObjs.Add(new EventObj()
                                        {
                                            EventId = eventItem.Key,
                                            EventName = eventItem.Value
                                        });
                                    }

                                }
                                departmentObj.Events = eventObjs;

                                DepartmentObjects.Add(departmentObj);
                            }
                        }
                    }

                    Events.Departments = DepartmentObjects;
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }

            return Events;
        }

        [WebMethod]
        public bool SaveEventRegistration(string Token, EventRegistration eventRegistrationObj)
        {
            bool success = false;
            try
            {
                if (Token == SecurityToken && eventRegistrationObj != null)
                {
                    success = CoreLogic.SaveEventRegistration(Token, eventRegistrationObj);
                }
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }

            return success;
        }
        #endregion End ScannerWebMethods

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //this.Dispose();
                CoreLogic.Dispose();
            }

            base.Dispose(disposing);
        }
        
    }

    [Serializable]
    public class EventListObj
    {
        public List<DepartmentObj> Departments { get; set; }
    }

    public class DepartmentObj
    {
        public string DepartmentName { get; set; }

        public List<EventObj> Events { get; set; }
    }

    public class EventObj
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
    }
}
