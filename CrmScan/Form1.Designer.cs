namespace CrmScan {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.departmentLabel = new System.Windows.Forms.Label();
            this.eventLabel = new System.Windows.Forms.Label();
            this.departmentComboBox = new System.Windows.Forms.ComboBox();
            this.eventComboBox = new System.Windows.Forms.ComboBox();
            this.scannerInput = new System.Windows.Forms.TextBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.registerButton = new System.Windows.Forms.Button();
            this.eventSelectionPanel = new System.Windows.Forms.Panel();
            this.lookUpRadioButton = new System.Windows.Forms.RadioButton();
            this.scannerRadioButton = new System.Windows.Forms.RadioButton();
            this.inputTypeLabel = new System.Windows.Forms.Label();
            this.scanInputPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textInputPanel = new System.Windows.Forms.Panel();
            this.dayLabel = new System.Windows.Forms.Label();
            this.monthLabel = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.yearComboBox = new System.Windows.Forms.ComboBox();
            this.monthComboBox = new System.Windows.Forms.ComboBox();
            this.dayComboBox = new System.Windows.Forms.ComboBox();
            this.lookUpSubmitButton = new System.Windows.Forms.Button();
            this.lnameInput = new System.Windows.Forms.TextBox();
            this.dobLabel = new System.Windows.Forms.Label();
            this.lnameLabel = new System.Windows.Forms.Label();
            this.fnameLabel = new System.Windows.Forms.Label();
            this.fnameInput = new System.Windows.Forms.TextBox();
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.successLabel = new System.Windows.Forms.Label();
            this.failedLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.eventSelectionPanel.SuspendLayout();
            this.scanInputPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.textInputPanel.SuspendLayout();
            this.LoginPanel.SuspendLayout();
            this.resultPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // departmentLabel
            // 
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.departmentLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.departmentLabel.Location = new System.Drawing.Point(151, 49);
            this.departmentLabel.Name = "departmentLabel";
            this.departmentLabel.Size = new System.Drawing.Size(190, 25);
            this.departmentLabel.TabIndex = 0;
            this.departmentLabel.Text = "Hosting Department:";
            // 
            // eventLabel
            // 
            this.eventLabel.AutoSize = true;
            this.eventLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.eventLabel.Location = new System.Drawing.Point(151, 85);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(167, 25);
            this.eventLabel.TabIndex = 2;
            this.eventLabel.Text = "Scheduled Event:";
            // 
            // departmentComboBox
            // 
            this.departmentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.departmentComboBox.Enabled = false;
            this.departmentComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.departmentComboBox.FormattingEnabled = true;
            this.departmentComboBox.Location = new System.Drawing.Point(338, 49);
            this.departmentComboBox.Name = "departmentComboBox";
            this.departmentComboBox.Size = new System.Drawing.Size(247, 21);
            this.departmentComboBox.TabIndex = 3;
            this.departmentComboBox.SelectedIndexChanged += new System.EventHandler(this.departmentComboBox_SelectedIndexChanged);
            // 
            // eventComboBox
            // 
            this.eventComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventComboBox.Enabled = false;
            this.eventComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.eventComboBox.FormattingEnabled = true;
            this.eventComboBox.Location = new System.Drawing.Point(340, 89);
            this.eventComboBox.Name = "eventComboBox";
            this.eventComboBox.Size = new System.Drawing.Size(245, 21);
            this.eventComboBox.TabIndex = 5;
            this.eventComboBox.SelectedIndexChanged += new System.EventHandler(this.eventComboBox_SelectedIndexChanged);
            // 
            // scannerInput
            // 
            this.scannerInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scannerInput.Location = new System.Drawing.Point(118, 98);
            this.scannerInput.MaxLength = 18;
            this.scannerInput.Multiline = true;
            this.scannerInput.Name = "scannerInput";
            this.scannerInput.Size = new System.Drawing.Size(286, 34);
            this.scannerInput.TabIndex = 6;
            this.scannerInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scannerInput_KeyPressAsync);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.inputLabel.Location = new System.Drawing.Point(151, 45);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(220, 31);
            this.inputLabel.TabIndex = 7;
            this.inputLabel.Text = "Scan Student ID:";
            // 
            // registerButton
            // 
            this.registerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.registerButton.Enabled = false;
            this.registerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.registerButton.Location = new System.Drawing.Point(287, 177);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(145, 63);
            this.registerButton.TabIndex = 8;
            this.registerButton.Text = "Select This Event";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // eventSelectionPanel
            // 
            this.eventSelectionPanel.BackColor = System.Drawing.Color.Transparent;
            this.eventSelectionPanel.Controls.Add(this.lookUpRadioButton);
            this.eventSelectionPanel.Controls.Add(this.scannerRadioButton);
            this.eventSelectionPanel.Controls.Add(this.inputTypeLabel);
            this.eventSelectionPanel.Controls.Add(this.departmentLabel);
            this.eventSelectionPanel.Controls.Add(this.eventLabel);
            this.eventSelectionPanel.Controls.Add(this.departmentComboBox);
            this.eventSelectionPanel.Controls.Add(this.eventComboBox);
            this.eventSelectionPanel.Controls.Add(this.registerButton);
            this.eventSelectionPanel.Location = new System.Drawing.Point(358, 290);
            this.eventSelectionPanel.Name = "eventSelectionPanel";
            this.eventSelectionPanel.Size = new System.Drawing.Size(727, 286);
            this.eventSelectionPanel.TabIndex = 9;
            this.eventSelectionPanel.Visible = false;
            // 
            // lookUpRadioButton
            // 
            this.lookUpRadioButton.AutoSize = true;
            this.lookUpRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpRadioButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lookUpRadioButton.Location = new System.Drawing.Point(425, 129);
            this.lookUpRadioButton.Name = "lookUpRadioButton";
            this.lookUpRadioButton.Size = new System.Drawing.Size(103, 29);
            this.lookUpRadioButton.TabIndex = 11;
            this.lookUpRadioButton.Text = "Look Up";
            this.lookUpRadioButton.UseVisualStyleBackColor = true;
            // 
            // scannerRadioButton
            // 
            this.scannerRadioButton.AutoSize = true;
            this.scannerRadioButton.Checked = true;
            this.scannerRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scannerRadioButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.scannerRadioButton.Location = new System.Drawing.Point(315, 129);
            this.scannerRadioButton.Name = "scannerRadioButton";
            this.scannerRadioButton.Size = new System.Drawing.Size(104, 29);
            this.scannerRadioButton.TabIndex = 10;
            this.scannerRadioButton.TabStop = true;
            this.scannerRadioButton.Text = "Scanner";
            this.scannerRadioButton.UseVisualStyleBackColor = true;
            // 
            // inputTypeLabel
            // 
            this.inputTypeLabel.AutoSize = true;
            this.inputTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTypeLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.inputTypeLabel.Location = new System.Drawing.Point(198, 131);
            this.inputTypeLabel.Name = "inputTypeLabel";
            this.inputTypeLabel.Size = new System.Drawing.Size(111, 25);
            this.inputTypeLabel.TabIndex = 9;
            this.inputTypeLabel.Text = "Input Type:";
            // 
            // scanInputPanel
            // 
            this.scanInputPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.scanInputPanel.AutoSize = true;
            this.scanInputPanel.BackColor = System.Drawing.Color.Transparent;
            this.scanInputPanel.Controls.Add(this.inputLabel);
            this.scanInputPanel.Controls.Add(this.scannerInput);
            this.scanInputPanel.Location = new System.Drawing.Point(0, 0);
            this.scanInputPanel.Name = "scanInputPanel";
            this.scanInputPanel.Size = new System.Drawing.Size(523, 174);
            this.scanInputPanel.TabIndex = 10;
            this.scanInputPanel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.scanInputPanel);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.textInputPanel);
            this.panel1.Controls.Add(this.eventSelectionPanel);
            this.panel1.Controls.Add(this.LoginPanel);
            this.panel1.Controls.Add(this.resultPanel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1088, 579);
            this.panel1.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 162);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // textInputPanel
            // 
            this.textInputPanel.BackColor = System.Drawing.Color.Transparent;
            this.textInputPanel.Controls.Add(this.dayLabel);
            this.textInputPanel.Controls.Add(this.monthLabel);
            this.textInputPanel.Controls.Add(this.yearLabel);
            this.textInputPanel.Controls.Add(this.yearComboBox);
            this.textInputPanel.Controls.Add(this.monthComboBox);
            this.textInputPanel.Controls.Add(this.dayComboBox);
            this.textInputPanel.Controls.Add(this.lookUpSubmitButton);
            this.textInputPanel.Controls.Add(this.lnameInput);
            this.textInputPanel.Controls.Add(this.dobLabel);
            this.textInputPanel.Controls.Add(this.lnameLabel);
            this.textInputPanel.Controls.Add(this.fnameLabel);
            this.textInputPanel.Controls.Add(this.fnameInput);
            this.textInputPanel.Location = new System.Drawing.Point(358, 290);
            this.textInputPanel.Name = "textInputPanel";
            this.textInputPanel.Size = new System.Drawing.Size(650, 191);
            this.textInputPanel.TabIndex = 11;
            this.textInputPanel.Visible = false;
            // 
            // dayLabel
            // 
            this.dayLabel.AutoSize = true;
            this.dayLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dayLabel.Location = new System.Drawing.Point(495, 58);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(26, 13);
            this.dayLabel.TabIndex = 18;
            this.dayLabel.Text = "Day";
            // 
            // monthLabel
            // 
            this.monthLabel.AutoSize = true;
            this.monthLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.monthLabel.Location = new System.Drawing.Point(438, 58);
            this.monthLabel.Name = "monthLabel";
            this.monthLabel.Size = new System.Drawing.Size(37, 13);
            this.monthLabel.TabIndex = 17;
            this.monthLabel.Text = "Month";
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.yearLabel.Location = new System.Drawing.Point(374, 58);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(29, 13);
            this.yearLabel.TabIndex = 16;
            this.yearLabel.Text = "Year";
            // 
            // yearComboBox
            // 
            this.yearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.yearComboBox.FormattingEnabled = true;
            this.yearComboBox.Location = new System.Drawing.Point(377, 71);
            this.yearComboBox.Name = "yearComboBox";
            this.yearComboBox.Size = new System.Drawing.Size(58, 21);
            this.yearComboBox.TabIndex = 13;
            // 
            // monthComboBox
            // 
            this.monthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.monthComboBox.FormattingEnabled = true;
            this.monthComboBox.Items.AddRange(new object[] {
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"});
            this.monthComboBox.Location = new System.Drawing.Point(441, 71);
            this.monthComboBox.Name = "monthComboBox";
            this.monthComboBox.Size = new System.Drawing.Size(51, 21);
            this.monthComboBox.TabIndex = 14;
            // 
            // dayComboBox
            // 
            this.dayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dayComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dayComboBox.FormattingEnabled = true;
            this.dayComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.dayComboBox.Location = new System.Drawing.Point(498, 71);
            this.dayComboBox.Name = "dayComboBox";
            this.dayComboBox.Size = new System.Drawing.Size(40, 21);
            this.dayComboBox.TabIndex = 15;
            // 
            // lookUpSubmitButton
            // 
            this.lookUpSubmitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpSubmitButton.Location = new System.Drawing.Point(249, 126);
            this.lookUpSubmitButton.Name = "lookUpSubmitButton";
            this.lookUpSubmitButton.Size = new System.Drawing.Size(112, 45);
            this.lookUpSubmitButton.TabIndex = 12;
            this.lookUpSubmitButton.Text = "Submit";
            this.lookUpSubmitButton.UseVisualStyleBackColor = true;
            this.lookUpSubmitButton.Click += new System.EventHandler(this.lookUpSubmitButton_Click);
            // 
            // lnameInput
            // 
            this.lnameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnameInput.Location = new System.Drawing.Point(207, 71);
            this.lnameInput.MaxLength = 255;
            this.lnameInput.Name = "lnameInput";
            this.lnameInput.Size = new System.Drawing.Size(144, 22);
            this.lnameInput.TabIndex = 10;
            // 
            // dobLabel
            // 
            this.dobLabel.AutoSize = true;
            this.dobLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dobLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dobLabel.Location = new System.Drawing.Point(372, 26);
            this.dobLabel.Name = "dobLabel";
            this.dobLabel.Size = new System.Drawing.Size(124, 25);
            this.dobLabel.TabIndex = 9;
            this.dobLabel.Text = "Date of Birth:";
            // 
            // lnameLabel
            // 
            this.lnameLabel.AutoSize = true;
            this.lnameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnameLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lnameLabel.Location = new System.Drawing.Point(202, 26);
            this.lnameLabel.Name = "lnameLabel";
            this.lnameLabel.Size = new System.Drawing.Size(112, 25);
            this.lnameLabel.TabIndex = 8;
            this.lnameLabel.Text = "Last Name:";
            // 
            // fnameLabel
            // 
            this.fnameLabel.AutoSize = true;
            this.fnameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fnameLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.fnameLabel.Location = new System.Drawing.Point(34, 26);
            this.fnameLabel.Name = "fnameLabel";
            this.fnameLabel.Size = new System.Drawing.Size(112, 25);
            this.fnameLabel.TabIndex = 7;
            this.fnameLabel.Text = "First Name:";
            // 
            // fnameInput
            // 
            this.fnameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fnameInput.Location = new System.Drawing.Point(39, 71);
            this.fnameInput.MaxLength = 255;
            this.fnameInput.Name = "fnameInput";
            this.fnameInput.Size = new System.Drawing.Size(144, 22);
            this.fnameInput.TabIndex = 6;
            // 
            // LoginPanel
            // 
            this.LoginPanel.BackColor = System.Drawing.Color.Transparent;
            this.LoginPanel.Controls.Add(this.txtPassword);
            this.LoginPanel.Controls.Add(this.txtUserName);
            this.LoginPanel.Controls.Add(this.label2);
            this.LoginPanel.Controls.Add(this.label1);
            this.LoginPanel.Location = new System.Drawing.Point(283, 225);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(523, 129);
            this.LoginPanel.TabIndex = 12;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(203, 76);
            this.txtPassword.MaxLength = 14;
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(252, 31);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(203, 34);
            this.txtUserName.MaxLength = 14;
            this.txtUserName.Multiline = true;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(252, 30);
            this.txtUserName.TabIndex = 9;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(63, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(46, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "User Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // resultPanel
            // 
            this.resultPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.resultPanel.BackColor = System.Drawing.Color.Transparent;
            this.resultPanel.Controls.Add(this.successLabel);
            this.resultPanel.Controls.Add(this.failedLabel);
            this.resultPanel.Location = new System.Drawing.Point(283, 225);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(523, 129);
            this.resultPanel.TabIndex = 11;
            this.resultPanel.Visible = false;
            // 
            // successLabel
            // 
            this.successLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.successLabel.AutoSize = true;
            this.successLabel.BackColor = System.Drawing.Color.Transparent;
            this.successLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.successLabel.ForeColor = System.Drawing.Color.Lime;
            this.successLabel.Location = new System.Drawing.Point(104, 49);
            this.successLabel.Name = "successLabel";
            this.successLabel.Size = new System.Drawing.Size(314, 31);
            this.successLabel.TabIndex = 7;
            this.successLabel.Text = "Successfully Registered!";
            this.successLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.successLabel.Visible = false;
            // 
            // failedLabel
            // 
            this.failedLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.failedLabel.AutoSize = true;
            this.failedLabel.BackColor = System.Drawing.Color.Transparent;
            this.failedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedLabel.ForeColor = System.Drawing.Color.Red;
            this.failedLabel.Location = new System.Drawing.Point(120, 18);
            this.failedLabel.Name = "failedLabel";
            this.failedLabel.Size = new System.Drawing.Size(283, 93);
            this.failedLabel.TabIndex = 8;
            this.failedLabel.Text = "Please Try Again \r\nor \r\nSee a Member of Staff";
            this.failedLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.failedLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(852, 446);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Event Registration Scanner";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.eventSelectionPanel.ResumeLayout(false);
            this.eventSelectionPanel.PerformLayout();
            this.scanInputPanel.ResumeLayout(false);
            this.scanInputPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.textInputPanel.ResumeLayout(false);
            this.textInputPanel.PerformLayout();
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label departmentLabel;
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.ComboBox departmentComboBox;
        private System.Windows.Forms.ComboBox eventComboBox;
        private System.Windows.Forms.TextBox scannerInput;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Panel eventSelectionPanel;
        private System.Windows.Forms.Panel scanInputPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.Label successLabel;
        private System.Windows.Forms.RadioButton lookUpRadioButton;
        private System.Windows.Forms.RadioButton scannerRadioButton;
        private System.Windows.Forms.Label inputTypeLabel;
        private System.Windows.Forms.Panel textInputPanel;
        private System.Windows.Forms.Label fnameLabel;
        private System.Windows.Forms.TextBox fnameInput;
        private System.Windows.Forms.Label dobLabel;
        private System.Windows.Forms.Label lnameLabel;
        private System.Windows.Forms.TextBox lnameInput;
        private System.Windows.Forms.Button lookUpSubmitButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label failedLabel;
        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox yearComboBox;
        private System.Windows.Forms.ComboBox monthComboBox;
        private System.Windows.Forms.ComboBox dayComboBox;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.Label monthLabel;
    }
}

