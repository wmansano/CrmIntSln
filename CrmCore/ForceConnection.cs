using lc.crm;
using lc.crm.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmCore
{
    public class ForceConnection
    {
        public string SessionID { get; set; }
        public string ServerUrl { get; set; }

        public ForceConnection(string connectionString)
        {
            ForceConnectionStringBuilder connectionBuilder = new ForceConnectionStringBuilder(connectionString);

            Login(connectionBuilder.Username, connectionBuilder.Password, connectionBuilder.Token);
        }

        public ForceConnection(string username, string password, string securityToken)
        {
            Login(username, password, securityToken);
        }

        private bool Login(string username, string password, string securityToken)
        {
            try
            {
                using (SforceService service = new SforceService())
                {
                    LoginResult loginResult = service.login(username, String.Concat(password, securityToken));

                    this.SessionID = loginResult.sessionId;
                    this.ServerUrl = loginResult.serverUrl;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}