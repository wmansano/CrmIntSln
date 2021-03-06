//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace lc.checkin.intws {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="eventsSoap", Namespace="http://tempuri.org/")]
    public partial class events : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConnectedOperationCompleted;
        
        private System.Threading.SendOrPostCallback RunIntegrationOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEventsOperationCompleted;
        
        private System.Threading.SendOrPostCallback ProcessEventRegistrationsOperationCompleted;
        
        private System.Threading.SendOrPostCallback SaveEventRegistrationOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public events() {
            this.Url = global::lc.checkin.Properties.Settings.Default.LC_CHECKIN_intws_events;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event ConnectedCompletedEventHandler ConnectedCompleted;
        
        /// <remarks/>
        public event RunIntegrationCompletedEventHandler RunIntegrationCompleted;
        
        /// <remarks/>
        public event GetEventsCompletedEventHandler GetEventsCompleted;
        
        /// <remarks/>
        public event ProcessEventRegistrationsCompletedEventHandler ProcessEventRegistrationsCompleted;
        
        /// <remarks/>
        public event SaveEventRegistrationCompletedEventHandler SaveEventRegistrationCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Connected", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool Connected() {
            object[] results = this.Invoke("Connected", new object[0]);
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ConnectedAsync() {
            this.ConnectedAsync(null);
        }
        
        /// <remarks/>
        public void ConnectedAsync(object userState) {
            if ((this.ConnectedOperationCompleted == null)) {
                this.ConnectedOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConnectedOperationCompleted);
            }
            this.InvokeAsync("Connected", new object[0], this.ConnectedOperationCompleted, userState);
        }
        
        private void OnConnectedOperationCompleted(object arg) {
            if ((this.ConnectedCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConnectedCompleted(this, new ConnectedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RunIntegration", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RunIntegration() {
            object[] results = this.Invoke("RunIntegration", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RunIntegrationAsync() {
            this.RunIntegrationAsync(null);
        }
        
        /// <remarks/>
        public void RunIntegrationAsync(object userState) {
            if ((this.RunIntegrationOperationCompleted == null)) {
                this.RunIntegrationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRunIntegrationOperationCompleted);
            }
            this.InvokeAsync("RunIntegration", new object[0], this.RunIntegrationOperationCompleted, userState);
        }
        
        private void OnRunIntegrationOperationCompleted(object arg) {
            if ((this.RunIntegrationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RunIntegrationCompleted(this, new RunIntegrationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetEvents", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public EventListObj GetEvents(string token) {
            object[] results = this.Invoke("GetEvents", new object[] {
                        token});
            return ((EventListObj)(results[0]));
        }
        
        /// <remarks/>
        public void GetEventsAsync(string token) {
            this.GetEventsAsync(token, null);
        }
        
        /// <remarks/>
        public void GetEventsAsync(string token, object userState) {
            if ((this.GetEventsOperationCompleted == null)) {
                this.GetEventsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEventsOperationCompleted);
            }
            this.InvokeAsync("GetEvents", new object[] {
                        token}, this.GetEventsOperationCompleted, userState);
        }
        
        private void OnGetEventsOperationCompleted(object arg) {
            if ((this.GetEventsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEventsCompleted(this, new GetEventsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ProcessEventRegistrations", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ProcessEventRegistrations(string token) {
            object[] results = this.Invoke("ProcessEventRegistrations", new object[] {
                        token});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void ProcessEventRegistrationsAsync(string token) {
            this.ProcessEventRegistrationsAsync(token, null);
        }
        
        /// <remarks/>
        public void ProcessEventRegistrationsAsync(string token, object userState) {
            if ((this.ProcessEventRegistrationsOperationCompleted == null)) {
                this.ProcessEventRegistrationsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnProcessEventRegistrationsOperationCompleted);
            }
            this.InvokeAsync("ProcessEventRegistrations", new object[] {
                        token}, this.ProcessEventRegistrationsOperationCompleted, userState);
        }
        
        private void OnProcessEventRegistrationsOperationCompleted(object arg) {
            if ((this.ProcessEventRegistrationsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ProcessEventRegistrationsCompleted(this, new ProcessEventRegistrationsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveEventRegistration", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SaveEventRegistration(string token, EventRegistrationObj eventRegistrationObj) {
            object[] results = this.Invoke("SaveEventRegistration", new object[] {
                        token,
                        eventRegistrationObj});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SaveEventRegistrationAsync(string token, EventRegistrationObj eventRegistrationObj) {
            this.SaveEventRegistrationAsync(token, eventRegistrationObj, null);
        }
        
        /// <remarks/>
        public void SaveEventRegistrationAsync(string token, EventRegistrationObj eventRegistrationObj, object userState) {
            if ((this.SaveEventRegistrationOperationCompleted == null)) {
                this.SaveEventRegistrationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveEventRegistrationOperationCompleted);
            }
            this.InvokeAsync("SaveEventRegistration", new object[] {
                        token,
                        eventRegistrationObj}, this.SaveEventRegistrationOperationCompleted, userState);
        }
        
        private void OnSaveEventRegistrationOperationCompleted(object arg) {
            if ((this.SaveEventRegistrationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveEventRegistrationCompleted(this, new SaveEventRegistrationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class EventListObj {
        
        private DepartmentObj[] departmentsField;
        
        /// <remarks/>
        public DepartmentObj[] Departments {
            get {
                return this.departmentsField;
            }
            set {
                this.departmentsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class DepartmentObj {
        
        private string departmentNameField;
        
        private TypeObj[] typesField;
        
        /// <remarks/>
        public string DepartmentName {
            get {
                return this.departmentNameField;
            }
            set {
                this.departmentNameField = value;
            }
        }
        
        /// <remarks/>
        public TypeObj[] Types {
            get {
                return this.typesField;
            }
            set {
                this.typesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class TypeObj {
        
        private string typeNameField;
        
        private EventObj[] eventsField;
        
        /// <remarks/>
        public string TypeName {
            get {
                return this.typeNameField;
            }
            set {
                this.typeNameField = value;
            }
        }
        
        /// <remarks/>
        public EventObj[] Events {
            get {
                return this.eventsField;
            }
            set {
                this.eventsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class EventObj {
        
        private string eventIdField;
        
        private string eventNameField;
        
        /// <remarks/>
        public string EventId {
            get {
                return this.eventIdField;
            }
            set {
                this.eventIdField = value;
            }
        }
        
        /// <remarks/>
        public string EventName {
            get {
                return this.eventNameField;
            }
            set {
                this.eventNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class EventRegistrationObj {
        
        private string eventIdField;
        
        private bool eventAttendedField;
        
        private bool eventRegisteredField;
        
        private string contactIdField;
        
        private string contactColleagueIdField;
        
        private string contactIdCardBarcodeField;
        
        private string contactFirstNameField;
        
        private string contactLastNameField;
        
        private System.Nullable<System.DateTime> contactBirthDateField;
        
        /// <remarks/>
        public string EventId {
            get {
                return this.eventIdField;
            }
            set {
                this.eventIdField = value;
            }
        }
        
        /// <remarks/>
        public bool EventAttended {
            get {
                return this.eventAttendedField;
            }
            set {
                this.eventAttendedField = value;
            }
        }
        
        /// <remarks/>
        public bool EventRegistered {
            get {
                return this.eventRegisteredField;
            }
            set {
                this.eventRegisteredField = value;
            }
        }
        
        /// <remarks/>
        public string ContactId {
            get {
                return this.contactIdField;
            }
            set {
                this.contactIdField = value;
            }
        }
        
        /// <remarks/>
        public string ContactColleagueId {
            get {
                return this.contactColleagueIdField;
            }
            set {
                this.contactColleagueIdField = value;
            }
        }
        
        /// <remarks/>
        public string ContactIdCardBarcode {
            get {
                return this.contactIdCardBarcodeField;
            }
            set {
                this.contactIdCardBarcodeField = value;
            }
        }
        
        /// <remarks/>
        public string ContactFirstName {
            get {
                return this.contactFirstNameField;
            }
            set {
                this.contactFirstNameField = value;
            }
        }
        
        /// <remarks/>
        public string ContactLastName {
            get {
                return this.contactLastNameField;
            }
            set {
                this.contactLastNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> ContactBirthDate {
            get {
                return this.contactBirthDateField;
            }
            set {
                this.contactBirthDateField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void ConnectedCompletedEventHandler(object sender, ConnectedCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConnectedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConnectedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void RunIntegrationCompletedEventHandler(object sender, RunIntegrationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RunIntegrationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RunIntegrationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void GetEventsCompletedEventHandler(object sender, GetEventsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEventsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEventsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public EventListObj Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((EventListObj)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void ProcessEventRegistrationsCompletedEventHandler(object sender, ProcessEventRegistrationsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ProcessEventRegistrationsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ProcessEventRegistrationsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    public delegate void SaveEventRegistrationCompletedEventHandler(object sender, SaveEventRegistrationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3190.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveEventRegistrationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SaveEventRegistrationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591