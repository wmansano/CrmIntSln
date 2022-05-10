using System;
using System.Web.Helpers;

namespace CrmCore
{
    public class SendEmail
    {
        private string _To = string.Empty;
        private string _From = string.Empty;
        private string _Bcc = string.Empty;
        private string _Title = string.Empty;
        private string _Subtitle = string.Empty;
        private string _Body = string.Empty;
        private string _ErrorMsg = string.Empty;

        public SendEmail(string project, string page, string function, string error, string message)
        {
            try
            {
                _To = "william.mansano@lethbridgecollege.ca";
                _Title = "CRM error message";
                _Body = "An error has occured<br />";
                _Body += "The error occured at: " + DateTime.Now + ".<br />";
                _Body += "Application: " + project + ".<br />";
                _Body += "Class: " + page + ".<br />";
                _Body += "Function: " + function + ".<br />";
                _Body += "Type: " + error + "<br />";
                _Body += "Message: " + message + "<br /><br />";
                _Body += "This is an automated email from the Lethbridge College CRM system.<br />";
                _Body += "Please do not respond.";

                Send();
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        public SendEmail() {

        }

        public bool Send()
        {
            bool Success = false;
            string ErrorMsg = string.Empty;

            try
            {
                WebMail.From = _From;
                WebMail.SmtpPort = 25;
                WebMail.SmtpServer = "exch2016.lethbridgecollege.ab.ca";
                WebMail.EnableSsl = false;

                if (!string.IsNullOrEmpty(_To) && !_Body.Contains("<<<"))
                {
                     WebMail.Send(to: _To, subject: _Title, body: _Body, from: _From, cc: null, filesToAttach: null, isBodyHtml: true, additionalHeaders: null, bcc: _Bcc, contentEncoding: null, headerEncoding: null, priority: null, replyTo: null);
                    Success = true;
                }

            }
            catch (Exception ex)
            {
                this.ErrorMsg = ex.ToString();
            }
            return Success;
        }

        public string Title { get { return _Title; } set { _Title = value; } }

        public string Subtitle { get { return _Subtitle; } set { _Subtitle = value; } }

        public string Body { get { return _Body; } set { _Body = value; } }

        public string From { get { return _From; } set { _From = value; } }

        public string To { get { return _To; } set { _To = value; } }

        public string Bcc { get { return _Bcc; } set { _Bcc = value; } }

        public string ErrorMsg { get { return _ErrorMsg; } set { _ErrorMsg = value; } }
    }


}