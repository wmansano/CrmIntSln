using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmScan
{

    public partial class Form1 : Form
    {
        //TODO: test offline mode
        //      when debugging comment out: this.TopMost = true; && InitializeTimer();

        private readonly string token = "6de89ae2-19b8-40c2-9a49-58248d18d36e60dcdb34-092c-40a3-b5dd-44372230da34";
        private const string CRYPTOKEY = "mgvqsBFIQE0Y5z6XJzHgI5RgfipZuuL/fAG5FNeUy5Q=";
        private const string EVENT_DATA_FILENAME_ENC = "eventdataenc.dat";
        private const string ATTENDEES_DATA_FILENAME_ENC = "attendeedataenc.dat";
        private intws.EventListObj eventListObj;
        private intws.EventObj selectedEvent;
        private List<intws.EventRegistration> eventAttendees;
        private string ErrorMsg;

        private const string username = "crm";
        private const string password = "lethbridge";

        #region Form Load

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On form loading - Sets window to always be on top
        ///                 - Gets events and registrations from web service
        ///                 - Fills Department combobox
        /// </summary>
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                #region Comment out when debugging
                
                //initialise timer to always keep window on top and active 
                this.TopMost = true;
                InitializeTimer();

                #endregion

                ////autofill login credentials when debugging
                txtUserName.Text = username;
                txtPassword.Text = password;

                //add please wait to combobox and select it
                departmentComboBox.Items.Add("Please Wait...");
                departmentComboBox.SelectedIndex = 0;

                PositionLoginPanel();
                PositionScanInputPanel();
                PositionTextInputPanel();
                PositionEventSelectionPanel();

                //initialise EventRegistrationObj list
                eventAttendees = new List<intws.EventRegistration>();

                if (CheckForInternetConnection())
                {
                    //theres an internet connection

                    // get events from web service
                    await Task.Run(() => GetEvents());

                    //save the latest data to file
                    SaveEventData();

                    #region upload from file

                    //check if there are un-uploaded attendees
                    if (File.Exists(ATTENDEES_DATA_FILENAME_ENC))
                    {
                        int countSuccess = 0;
                        int countFail = 0;
                        string message = null;

                        //load data from file
                        eventAttendees = LoadAttendeeData();

                        //update attendees
                        foreach (var attendee in eventAttendees)
                        {
                            //set event reg with attendee
                            if (SaveEventRegistration(attendee))
                            {
                                countSuccess++;
                            }
                            else
                            {
                                //write to an error file?
                                countFail++;
                            }
                        }

                        eventAttendees.Clear();

                        File.Delete(ATTENDEES_DATA_FILENAME_ENC);

                        //SaveAttendeeData(failedRecords);

                        if (countFail > 0)
                        {
                            message = countSuccess + " records have been uploaded & " + countFail + " records failed to upload";
                        }
                        else
                        {
                            message = countSuccess + " records have been uploaded";
                        }
                        MessageBox.Show(message, "Upload Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    #endregion

                    // get events from web service
                    await Task.Run(() => GetEvents());

                    //save the latest data to file
                    SaveEventData();
                }
                else if (File.Exists(EVENT_DATA_FILENAME_ENC))
                {
                    //no internet connection but data is saved from a previous connection

                    //load data from file
                    LoadEventData();
                }
                else
                {
                    //no internet connection, no files
                    departmentComboBox.Items.Clear();
                    departmentComboBox.Items.Add("Please connect to the Internet");
                    departmentComboBox.SelectedIndex = 0;

                    return;
                }

                //empty combobox
                departmentComboBox.Items.Clear();

                //enable and empty combobox
                departmentComboBox.Enabled = true;
                departmentComboBox.Text = String.Empty;

                //load comboBox with the Departments
                departmentComboBox.BeginUpdate();

                intws.DepartmentObj[] departmentObj = eventListObj.Departments;

                foreach (var dept in departmentObj)
                {
                    departmentComboBox.Items.Add(dept.DepartmentName);
                }
                departmentComboBox.EndUpdate();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(ATTENDEES_DATA_FILENAME_ENC))
            {
                MessageBox.Show("There is un-uploaded data - please connect to the internet and re-open app", "Un-uploaded Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PositionScanInputPanel() {
            this.scanInputPanel.Anchor = AnchorStyles.None;
            this.scanInputPanel.Left = (this.ClientSize.Width - this.scanInputPanel.Width) / 2;
            this.scanInputPanel.Top = (this.ClientSize.Height - this.scanInputPanel.Height) / 2;
        }

        private void PositionResultPanel()
        {
            this.resultPanel.Anchor = AnchorStyles.None;
            this.resultPanel.Left = (this.ClientSize.Width - this.resultPanel.Width) / 2;
            this.resultPanel.Top = (this.ClientSize.Height - this.resultPanel.Height) / 2;
        }

        private void PositionLoginPanel()
        {
            this.LoginPanel.Anchor = AnchorStyles.None;
            this.LoginPanel.Left = (this.ClientSize.Width - this.LoginPanel.Width) / 2;
            this.LoginPanel.Top = (this.ClientSize.Height - this.LoginPanel.Height) / 2;
        }

        private void PositionTextInputPanel()
        {
            this.textInputPanel.Anchor = AnchorStyles.None;
            this.textInputPanel.Left = (this.ClientSize.Width - this.textInputPanel.Width) / 2;
            this.textInputPanel.Top = (this.ClientSize.Height - this.textInputPanel.Height) / 2;
        }

        private void PositionEventSelectionPanel()
        {
            this.eventSelectionPanel.Anchor = AnchorStyles.None;
            this.eventSelectionPanel.Left = (this.ClientSize.Width - this.eventSelectionPanel.Width) / 2;
            this.eventSelectionPanel.Top = (this.ClientSize.Height - this.eventSelectionPanel.Height) / 2;
        }

        #endregion

        #region combobox index changes

        /// <summary>
        /// A department has been selected - enable and populate Type combobox
        /// </summary>
        private void departmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ignore combobox index changes if it's for please wait or connect to the internet warnings
            if (departmentComboBox.Text == "Please Wait..." || departmentComboBox.Text == "Please connect to the Internet")
            {
                return;
            }

            try
            {
                if (departmentComboBox.SelectedIndex != -1)
                {
                     //enable event combobox and make sure its empty
                    eventComboBox.Enabled = true;
                    eventComboBox.SelectedIndex = -1;
                    eventComboBox.Items.Clear();

                    // Empty & disable event combobox if it has been enabled
                    //if (eventComboBox.Enabled)
                    //{
                    //    eventComboBox.SelectedIndex = -1;
                    //    eventComboBox.Items.Clear();
                    //    eventComboBox.Enabled = false;
                    //    registerButton.Enabled = false;
                    //}

                    //load event comboBox with the events
                    eventComboBox.BeginUpdate();

                    intws.DepartmentObj[] departmentObj = eventListObj.Departments;

                    foreach (var dept in departmentObj)
                    {
                        if (departmentComboBox.Text == dept.DepartmentName)
                        {
                            intws.EventObj[] eventObjs = dept.Events;
                            foreach (var @event in eventObjs)
                            {
                                eventComboBox.Items.Add(@event.EventName);
                            }
                        }
                    }
                    eventComboBox.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// An event has been selected - enable 'select event' button 
        ///                            - set selectedEvent & selectedEventRegistration global variables
        /// </summary>
        private void eventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedIndex != -1)
            {
                //enable event selection button
                registerButton.Enabled = true;
                try
                {
                    // set selected Event global variable
                    intws.DepartmentObj[] departmentObj = eventListObj.Departments;

                    foreach (var dept in departmentObj)
                    {
                        if (departmentComboBox.Text == dept.DepartmentName)
                        {
                            intws.EventObj[] eventObjs = dept.Events;
                            foreach (var @event in eventObjs)
                            {
                                if (eventComboBox.Text == @event.EventName)
                                {
                                    selectedEvent = @event;
                                   // eventComboBox.Text = @event.EventName;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.ToString();
                }
            }
        }

        #endregion

        #region button clicks

        /// <summary>
        /// Check key presses on login - when enter is pressed, check credentials
        /// </summary>
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            // Login Form
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserName.Text.Trim() == username && txtPassword.Text.Trim() == password)
                {
                    LoginPanel.Visible = false;
                    eventSelectionPanel.Visible = true;
                    PositionEventSelectionPanel();
                }
                else
                {
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
            }
        }

        /// <summary>
        /// Event selection button clicked - check selected input type 
        ///                                - display appropriate input panel
        /// </summary>
        private void registerButton_Click(object sender, EventArgs e)
        {
            try
            {
                //hide comboboxes from form
                eventSelectionPanel.Visible = false;

                //disable comboboxes
                if (departmentComboBox.Enabled == true)
                {
                    departmentComboBox.Enabled = false;
                    eventComboBox.Enabled = false;
                }

                //if scanner radio button is selected
                if (scannerRadioButton.Checked)
                {
                    //show scanner input and move focus to textbox
                    resultPanel.BringToFront();
                    scanInputPanel.Visible = true;
                    PositionScanInputPanel();

                    //hide cursor
                    Cursor.Hide();

                    //move focus to textbox
                    scannerInput.Focus();
                }

                //if lookup radio button is selected
                if (lookUpRadioButton.Checked)
                {
                    //show lookup input and move focus to name textbox
                    resultPanel.BringToFront();
                    textInputPanel.Visible = true;
                    PositionTextInputPanel();

                    //populate date comboboxes
                    for (int i = DateTime.Now.Year; i >= 1900 ; i--)
                    {
                        yearComboBox.Items.Add(i);
                    }

                    fnameInput.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// Lookup event attendee by name and DoB - display success or fail panel if input is good/bad
        /// </summary>
        private async void lookUpSubmitButton_Click(object sender, EventArgs e)
        {
            string message = null;
            try
            {
                //get DoB
                string date = yearComboBox.Text + "," + monthComboBox.Text + "," + dayComboBox.Text;                

                swapPanels();
                if (lookUpCheckRegistration(fnameInput.Text, lnameInput.Text, date, ref message))
                {
                    //checkregistration returned true
                    success();
                }
                else
                {
                    //checkRegistration returned false
                    fail(message);
                }

                //wait for 3 seconds                
                await waitFuncAsync();

                resetForm();

                //move focus back to name entry
                fnameInput.Focus();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        #endregion

        #region key presses

        /// <summary>
        /// Check key presses on scanner input - when enter is pressed, take input
        ///                                    - remove S if it is an Snumber
        ///                                    - display success or fail panel if input is good/bad
        ///                                   
        /// </summary>
        private async void scannerInput_KeyPressAsync(object sender, KeyPressEventArgs e)
        {
            string inputString;
            string message = null;
            try
            {
                //check if the enter key has been pressed
                if (e.KeyChar == 13)
                {
                    if (!String.IsNullOrEmpty(scannerInput.Text))
                    {
                        inputString = scannerInput.Text;

                        swapPanels();

                        if (scannerCheckRegistration(inputString, ref message))
                        {
                            //checkregistration returned true
                            success();
                        }
                        else
                        {
                            //checkRegistration returned false
                            fail(message);
                        }

                        //wait for 3 seconds                
                        await waitFuncAsync();

                        resetForm();

                        PositionScanInputPanel();

                        scannerInput.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// check for keyboard shortcuts
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //ignore alt+tab
                if (e.KeyCode == Keys.Alt && e.KeyCode == Keys.Tab)
                {
                    e.SuppressKeyPress = true;
                }

                //check if ctrl + b shortcut is pressed (Back)
                if (e.Control && e.KeyCode == Keys.B)
                {
                    //suppresskeypress to prevent system sound
                    e.SuppressKeyPress = true;

                    if (!eventSelectionPanel.Visible)
                    {
                        //bring back the event selection panel and hide all others
                        textInputPanel.Visible = false;
                        scanInputPanel.Visible = false;
                        resultPanel.Visible = false;
                        eventSelectionPanel.Visible = true;
                        PositionEventSelectionPanel();
                        //if app was in scanner mode, show cursor
                        if (scannerRadioButton.Checked)
                        {
                            Cursor.Show();
                        }
                    }
                }

                //check if ctrl + q shortcut is pressed (Quit)
                if (e.Control && e.KeyCode == Keys.Q)
                {
                    //suppresskeypress to prevent system sound
                    e.SuppressKeyPress = true;

                    //close application
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        #endregion

        #region Registration Checks

        /// <summary>
        /// Check if input from the scanner matches a registered attendee
        /// OR create new attendee if pre-registration is not required
        /// </summary>
        /// <param name="idCode">input string from scanner</param>
        /// <param name="message">message to display if not successful</param>
        /// <returns></returns>
        private bool scannerCheckRegistration(string idCode, ref string message)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrEmpty(idCode))
                {
                    // pre-load object
                    var attendee = new intws.EventRegistration
                    {
                        EventId = selectedEvent.EventId,
                        EventAttended = true,
                    };

                    switch (idCode.Length)
                    {
                        case 7:  // sNumber
                        case 8:
                        case 9:
                            idCode = Regex.Replace(idCode, "[^0-9.]", "");
                            attendee.ContactColleagueId = idCode;
                            success = SaveEventRegistration(attendee);
                            break;
                        case 14:                     
                        case 18:
                            attendee.ContactIdCardBarcode = idCode;
                            success = SaveEventRegistration(attendee);
                            break;
                        default:
                            message = "This does not appear to be a valid barcode or id for this system." + Environment.NewLine + "Please, try again or speak with the support staff.";
                            break;
                    }

                    //if (!string.IsNullOrEmpty(attendee.ContactColleagueId) || !string.IsNullOrEmpty(attendee.ContactIdCardBarcode))
                    //{
                    //    if (CheckForInternetConnection())
                    //    {

                    //        success = SaveEventRegistration(attendee);

                    //        //// Attendee is registered - record attendance
                    //        //if (validateRegistration(attendee))
                    //        //{
                    //        //    attendee.EventRegistered = true;

                    //        //    // Update via web service
                    //        //    success = SetEventRegistrations(attendee);
                    //        //}
                    //        //else
                    //        //{
                    //        //    // Attendee is NOT registered: check if registration is required
                    //        //    attendee.EventRegistered = false;
                    //        //    if (!preRegistration)
                    //        //    {
                    //        //        // Attendee is not registered but is in CRM- record attendance
                    //        //        if (ValidateNumber(idCode))
                    //        //        {
                    //        //            // Update via web service
                    //        //            success = SetEventRegistrations(attendee);
                    //        //        }
                    //        //        else
                    //        //        {
                    //        //            // Contact sNumber or barcode not Found!
                    //        //            // Create contact only in Lookup mode (First/Last Name and Birthdate)
                    //        //            message = "Student Id Card" + Environment.NewLine + "Not found!";
                    //        //        }
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        // Student Not Register
                    //        //        message = "Student not registered" + Environment.NewLine + "for the event!";
                    //        //    }
                    //        //}
                    //    }
                    //    else
                    //    {
                    //        message = "No Internet Connection!" + Environment.NewLine + "Please connect to the Internet";
                    //    }
                    //}
                }
                else
                {
                    // Student Id card or sNumber is empty
                    message = "This does not appear to be a valid Student Id!" + Environment.NewLine + "Please, try again or speak with the support staff.";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
            return success;
        }

        /// <summary>
        /// Check if input from the lookup matches a registered attendee
        /// OR create new attendee if pre-registration is not required
        /// </summary>
        /// <param name="fname">First name entered on form</param>
        /// <param name="lname">Last name entered on form</param>
        /// <param name="dob">date of birth selected from datepicker</param>
        /// <param name="message">message to display if not successful</param>
        /// <returns>returns true if atendee is pre-registered or if pre-registration is not required</returns>
        private bool lookUpCheckRegistration(string fname, string lname, string dob, ref string message)
        {
            bool success = false;

            try
            {
                if (!string.IsNullOrEmpty(fname) && !string.IsNullOrEmpty(lname) && !string.IsNullOrEmpty(dob))
                {
                    //convert string dob to datetime temp
                    if (DateTime.TryParse(dob, out DateTime birth))
                    {
                        var attendee = new intws.EventRegistration
                        {
                            EventId = selectedEvent.EventId,
                            ContactFirstName = fname,
                            ContactLastName = lname,
                            ContactBirthDate = birth,
                            EventAttended = true,
                        };

                        if (CheckForInternetConnection())
                        {
                            //update via web service
                            success = SaveEventRegistration(attendee);

                            //// Attendee is registered - record attendance and return true                    
                            //if (validateRegistration(attendee))
                            //{
                            //    attendee.EventRegistered = true;

                            //    //update via web service
                            //    success = SetEventRegistrations(attendee);
                            //}
                            //else
                            //{
                            //    //pre-registration not required - create an event registration for user
                            //    attendee.EventRegistered = false;
                            //    if (!preRegistration)
                            //    {
                            //        //attendee is not registered but is in CRM- record attendance
                            //        if (ValidateLookup(lname, fname, dob))
                            //        {
                            //            //update via web service
                            //            success = SetEventRegistrations(attendee);
                            //        }
                            //        else
                            //        {
                            //            // Student not Found!
                            //            // Create contact
                            //            //message = "Create contact!";
                            //            success = SetEventRegistrations(attendee);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        // Student Not Register
                            //        message = "Student not registered" + Environment.NewLine + "for the event!";
                            //    }
                            //}
                        }
                        else
                        {
                            ////save locally to upload later
                            //eventAttendees.Add(attendee);
                            //SaveAttendeeData();
                            //retVal = true;
                        }
                    }
                    else
                    {
                        // Invalid birthdate
                        message = "Invalid Birthdate!" + Environment.NewLine + "Please, try again!";
                    }
                }
                else
                {
                    // Student information is empty
                    message = "Student information is missing!" + Environment.NewLine + "Please, try again!";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
            return success;
        }

        #endregion

        #region connectionCheck & timers

        /// <summary>
        /// Checks for internet connection
        /// </summary>
        /// <returns>returns true if connection is established</returns>
        private bool CheckForInternetConnection()
        {
            bool retVal = false;
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                ErrorMsg = ex.ToString();
            }
            return retVal;
        }

        /// <summary>
        /// timer to run timer1_tick every 100ms
        /// </summary>
        private void InitializeTimer()
        {
            try
            {
                timer1.Interval = 100;
                timer1.Tick += new EventHandler(timer1_tick);

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// Force window to always be on top
        /// </summary>        
        private void timer1_tick(object sender, EventArgs e)
        {
            try
            {
                if (!this.Focused)
                {
                    this.Activate();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// wait for 3 seconds
        /// </summary>
        private async Task waitFuncAsync()
        {
            try
            {
                //ignore key inputs while result is showing
                this.KeyPreview = false;

                //wait 3 seconds
                await Task.Run(() => Thread.Sleep(3000));

                //listen for key inputs again
                this.KeyPreview = true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        #endregion

        #region FileIO

        /// <summary>
        /// Load the eventListObj from file
        /// </summary>
        public void LoadEventData()
        {
            try
            {
                //if (File.Exists(EVENT_DATA_FILENAME))
                //{
                //    try
                //    {
                //        //Load event data
                //        // Create a FileStream & BinaryFormatter
                //        FileStream readerFileStream = new FileStream(EVENT_DATA_FILENAME, FileMode.Open, FileAccess.Read);
                //        BinaryFormatter formatter = new BinaryFormatter();

                //        // Reconstruct eventListObj
                //        eventListObj = (lcevents.EventListObj)formatter.Deserialize(readerFileStream);

                //        //Close the reader stream
                //        readerFileStream.Close();
                //    }
                //    catch (Exception ex)
                //    {
                //        ErrorMsg = ex.ToString();
                //    }
                //}

                // Alan's Changes:

                if (File.Exists(EVENT_DATA_FILENAME_ENC))
                {
                    //create new memory stream & binaryformatter
                    MemoryStream ms = new MemoryStream();
                    BinaryFormatter bf = new BinaryFormatter();

                    //decrypt file and write it to a new file
                    using (var inStream = File.OpenRead(EVENT_DATA_FILENAME_ENC))
                    using (var provider = new AesCryptoServiceProvider())
                    {
                        //set IV from the data written to file
                        var IV = new byte[provider.IV.Length];
                        inStream.Read(IV, 0, IV.Length);

                        using (var cryptoTransform = provider.CreateDecryptor(Convert.FromBase64String(CRYPTOKEY), IV))
                        using (var cryptoStream = new CryptoStream(inStream, cryptoTransform, CryptoStreamMode.Read))
                        {
                            cryptoStream.CopyTo(ms);

                            ms.Seek(0, SeekOrigin.Begin);

                            eventListObj = (intws.EventListObj)bf.Deserialize(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// Load the eventListObj from file
        /// </summary>
        public List<intws.EventRegistration> LoadAttendeeData()
        {
            List<intws.EventRegistration> loadData = new List<intws.EventRegistration>();
            try
            {

                //load attendee data from file
                if (File.Exists(ATTENDEES_DATA_FILENAME_ENC))
                {
                    //create new memory stream & binaryformatter
                    MemoryStream ms = new MemoryStream();
                    BinaryFormatter bf = new BinaryFormatter();

                    //decrypt file and write it to a new file
                    using (var inStream = File.OpenRead(ATTENDEES_DATA_FILENAME_ENC))
                    using (var provider = new AesCryptoServiceProvider())
                    {
                        //set IV from the data written to file
                        var IV = new byte[provider.IV.Length];
                        inStream.Read(IV, 0, IV.Length);

                        using (var cryptoTransform = provider.CreateDecryptor(Convert.FromBase64String(CRYPTOKEY), IV))
                        using (var cryptoStream = new CryptoStream(inStream, cryptoTransform, CryptoStreamMode.Read))
                        {
                            //copy cryptostream to the memory stream
                            cryptoStream.CopyTo(ms);

                            //reset to start of memory stream
                            ms.Seek(0, SeekOrigin.Begin);

                            // Reconstruct attendee list obj from memory stream
                            loadData = (List<intws.EventRegistration>)bf.Deserialize(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
            return loadData;
        }

        /// <summary>
        /// Save the state of the eventListObj to file
        /// </summary>
        public void SaveEventData()
        {
            try
            {
                //// Save event data
                //// Create a FileStream & BinaryFormatter
                //FileStream writerFileStream = new FileStream(EVENT_DATA_FILENAME, FileMode.Create, FileAccess.Write);
                //BinaryFormatter formatter = new BinaryFormatter();

                //// Save eventListObj to file
                //formatter.Serialize(writerFileStream, eventListObj);

                //// Close the writer stream
                //writerFileStream.Close();

                // Alan's Changes:

                //create new memory stream & binaryformatter
                MemoryStream ms = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();

                //serialize eventListObj to memory stream
                bf.Serialize(ms, eventListObj);

                //reset the memory stream to beginning for read operation
                ms.Seek(0, SeekOrigin.Begin);

                //encrypt file                
                using (var outStream = File.Create(EVENT_DATA_FILENAME_ENC))
                using (var provider = new AesCryptoServiceProvider())
                {
                    //set key
                    provider.Key = Convert.FromBase64String(CRYPTOKEY);

                    using (var cryptoTransform = provider.CreateEncryptor())
                    using (var cryptoStream = new CryptoStream(outStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        //write IV to file before encrypted data
                        outStream.Write(provider.IV, 0, provider.IV.Length);

                        //write encrypted data
                        ms.CopyTo(cryptoStream);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        #endregion

        #region Web Service Calls

        /// <summary>
        /// Get the events from web service - populate eventListObj
        /// </summary>
        private void GetEvents()
        {
            try
            {
                // Populate events list from web reference
                using (intws.Events svc = new intws.Events())
                {
                    eventListObj = svc.GetEvents(token);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// Updates attendees record with their attendance
        /// </summary>
        /// <param name="attendee">EventRegistrationObj with attendees information</param>
        /// <returns>returns true if attendees record has been updated</returns>
        private bool SaveEventRegistration(intws.EventRegistration attendee)
        {
            bool retVal = false;
            try
            {
                //send eventregistrationobj back to webservice to update attendance
                using (intws.Events svc = new intws.Events())
                {
                    //send obj to web service w/ token
                    retVal = svc.SaveEventRegistration(token, attendee);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
            return retVal;
        }

        /// <summary>
        /// Save the state of the eventAttendees object to file
        /// </summary>
        public void SaveAttendeeData(List<intws.EventRegistration> saveData)
        {
            try
            {

                //create new memory stream & binaryformatter
                MemoryStream ms = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();

                //serialize eventListObj to memory stream
                bf.Serialize(ms, saveData);

                //reset the memory stream to beginning for read operation
                ms.Seek(0, SeekOrigin.Begin);

                //encrypt file                
                using (var outStream = File.Create(ATTENDEES_DATA_FILENAME_ENC))
                using (var provider = new AesCryptoServiceProvider())
                {
                    //set key
                    provider.Key = Convert.FromBase64String(CRYPTOKEY);

                    using (var cryptoTransform = provider.CreateEncryptor())
                    using (var cryptoStream = new CryptoStream(outStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        //write IV to file before encrypted data
                        outStream.Write(provider.IV, 0, provider.IV.Length);

                        //write encrypted data
                        ms.CopyTo(cryptoStream);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        #endregion

        #region FormPanels

        /// <summary>
        /// Swap to show results panel/show input panel
        /// </summary>
        private void swapPanels()
        {
            PositionResultPanel();

            if (scannerRadioButton.Checked)
            {
                scanInputPanel.Visible = !scanInputPanel.Visible;

                resultPanel.Visible = !resultPanel.Visible;
                successLabel.Visible = true;
            }

            if (lookUpRadioButton.Checked)
            {
                textInputPanel.Visible = !textInputPanel.Visible;

                resultPanel.Visible = !resultPanel.Visible;
                successLabel.Visible = true;
            }

        }

        /// <summary>
        /// user is registered for event or pre-registration is not required
        /// display success banner and play sound
        /// </summary>
        private void success()
        {
            try
            {
                failedLabel.Visible = false;
                successLabel.Visible = true;

                SoundPlayer player = new SoundPlayer(@"C:\Windows\media\chimes.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// user is not registered for event or pre-registration is required
        /// display fail banner and play sound
        /// </summary>
        private void fail(string ErrorMsg = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(ErrorMsg))
                {
                    failedLabel.Text = ErrorMsg;
                }
                else
                {
                    failedLabel.Text = @"Please Try Again" + Environment.NewLine +
                                        "        or      " + Environment.NewLine +
                                        "See a Member of Staff";
                }

                successLabel.Visible = false;
                failedLabel.Visible = true;

                SoundPlayer player = new SoundPlayer(@"C:\Windows\media\chord.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// reset the form back to prepare for next input
        /// </summary>
        private void resetForm()
        {
            try
            {
                //hide success/fail label
                successLabel.Visible = false;
                failedLabel.Visible = false;

                //clear input field for next entry
                //swap panels back
                //move focus to textbox
                scannerInput.Text = string.Empty;

                //clear input field for next entry
                //swap panels back
                //move focus to textbox
                fnameInput.Text = string.Empty;
                lnameInput.Text = string.Empty;
                yearComboBox.SelectedIndex = -1;
                monthComboBox.SelectedIndex = -1;
                dayComboBox.SelectedIndex = -1;

                swapPanels();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.ToString();
            }
        }

        #endregion
    }
}