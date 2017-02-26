namespace mvvFacialRecognition
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.enrolledImagePictureBox = new System.Windows.Forms.PictureBox();
            this.mainFeedPictureBox = new System.Windows.Forms.PictureBox();
            this.operationsTabControl = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.autoIdentifyGroupBox = new System.Windows.Forms.GroupBox();
            this.autoIdentifyLabel = new System.Windows.Forms.Label();
            this.autoIdentifyButton = new System.Windows.Forms.Button();
            this.drawRectangleCheckbox = new System.Windows.Forms.CheckBox();
            this.mainTabLabel = new System.Windows.Forms.Label();
            this.identifyButton = new System.Windows.Forms.Button();
            this.showEyeCheckBox = new System.Windows.Forms.CheckBox();
            this.faceConfCheckBox = new System.Windows.Forms.CheckBox();
            this.drawEyesCheckBox = new System.Windows.Forms.CheckBox();
            this.enrollmentTab = new System.Windows.Forms.TabPage();
            this.enrollInstructions = new System.Windows.Forms.Label();
            this.videoFileTxt = new System.Windows.Forms.Label();
            this.enrollButton = new System.Windows.Forms.Button();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.enrollLNameLabel = new System.Windows.Forms.Label();
            this.enrollFNameLabel = new System.Windows.Forms.Label();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.editProfileTab = new System.Windows.Forms.TabPage();
            this.accessLevelDropDown = new System.Windows.Forms.ComboBox();
            this.profileAccessLbl = new System.Windows.Forms.Label();
            this.profileUpdateUserBtn = new System.Windows.Forms.Button();
            this.profileFNameTxt = new System.Windows.Forms.TextBox();
            this.profileLNameTxt = new System.Windows.Forms.TextBox();
            this.profileFNameLbl = new System.Windows.Forms.Label();
            this.profileLNameLbl = new System.Windows.Forms.Label();
            this.profileVideoNameLbl = new System.Windows.Forms.Label();
            this.profileUserId = new System.Windows.Forms.TextBox();
            this.profileDeleteButton = new System.Windows.Forms.Button();
            this.UserIdDropLbl = new System.Windows.Forms.Label();
            this.fillUserInfoButton = new System.Windows.Forms.Button();
            this.userIdDropDown = new System.Windows.Forms.ComboBox();
            this.profileUserIdLbl = new System.Windows.Forms.Label();
            this.profileUserVideo = new System.Windows.Forms.TextBox();
            this.changeVideoButton = new System.Windows.Forms.Button();
            this.aboutTab = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.aboutTextBox = new System.Windows.Forms.TextBox();
            this.dennyTextBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.myNameTextBox = new System.Windows.Forms.TextBox();
            this.selectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.insetPictureBox = new System.Windows.Forms.PictureBox();
            this.updateFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.enrolledImageBox = new System.Windows.Forms.GroupBox();
            this.enrolledImageNameLabel = new System.Windows.Forms.Label();
            this.videoPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.enrolledImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPictureBox)).BeginInit();
            this.operationsTabControl.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.autoIdentifyGroupBox.SuspendLayout();
            this.enrollmentTab.SuspendLayout();
            this.editProfileTab.SuspendLayout();
            this.aboutTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.insetPictureBox)).BeginInit();
            this.enrolledImageBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // enrolledImagePictureBox
            // 
            this.enrolledImagePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.enrolledImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.enrolledImagePictureBox.Location = new System.Drawing.Point(30, 32);
            this.enrolledImagePictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.enrolledImagePictureBox.Name = "enrolledImagePictureBox";
            this.enrolledImagePictureBox.Size = new System.Drawing.Size(225, 225);
            this.enrolledImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enrolledImagePictureBox.TabIndex = 0;
            this.enrolledImagePictureBox.TabStop = false;
            // 
            // mainFeedPictureBox
            // 
            this.mainFeedPictureBox.Location = new System.Drawing.Point(100, 80);
            this.mainFeedPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.mainFeedPictureBox.Name = "mainFeedPictureBox";
            this.mainFeedPictureBox.Size = new System.Drawing.Size(700, 525);
            this.mainFeedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainFeedPictureBox.TabIndex = 0;
            this.mainFeedPictureBox.TabStop = false;
            this.mainFeedPictureBox.Click += new System.EventHandler(this.mainFeedPictureBox_Click);
            // 
            // operationsTabControl
            // 
            this.operationsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.operationsTabControl.Controls.Add(this.mainTab);
            this.operationsTabControl.Controls.Add(this.enrollmentTab);
            this.operationsTabControl.Controls.Add(this.editProfileTab);
            this.operationsTabControl.Controls.Add(this.aboutTab);
            this.operationsTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operationsTabControl.Location = new System.Drawing.Point(960, 6);
            this.operationsTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.operationsTabControl.Multiline = true;
            this.operationsTabControl.Name = "operationsTabControl";
            this.operationsTabControl.SelectedIndex = 0;
            this.operationsTabControl.Size = new System.Drawing.Size(300, 425);
            this.operationsTabControl.TabIndex = 1;
            this.operationsTabControl.TabStop = false;
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.autoIdentifyGroupBox);
            this.mainTab.Controls.Add(this.drawRectangleCheckbox);
            this.mainTab.Controls.Add(this.mainTabLabel);
            this.mainTab.Controls.Add(this.identifyButton);
            this.mainTab.Controls.Add(this.showEyeCheckBox);
            this.mainTab.Controls.Add(this.faceConfCheckBox);
            this.mainTab.Controls.Add(this.drawEyesCheckBox);
            this.mainTab.Location = new System.Drawing.Point(4, 25);
            this.mainTab.Margin = new System.Windows.Forms.Padding(4);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(4);
            this.mainTab.Size = new System.Drawing.Size(292, 396);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "Main";
            this.mainTab.UseVisualStyleBackColor = true;
            // 
            // autoIdentifyGroupBox
            // 
            this.autoIdentifyGroupBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.autoIdentifyGroupBox.Controls.Add(this.autoIdentifyLabel);
            this.autoIdentifyGroupBox.Controls.Add(this.autoIdentifyButton);
            this.autoIdentifyGroupBox.Location = new System.Drawing.Point(33, 256);
            this.autoIdentifyGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.autoIdentifyGroupBox.Name = "autoIdentifyGroupBox";
            this.autoIdentifyGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.autoIdentifyGroupBox.Size = new System.Drawing.Size(240, 90);
            this.autoIdentifyGroupBox.TabIndex = 7;
            this.autoIdentifyGroupBox.TabStop = false;
            // 
            // autoIdentifyLabel
            // 
            this.autoIdentifyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.autoIdentifyLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.autoIdentifyLabel.Location = new System.Drawing.Point(36, 16);
            this.autoIdentifyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.autoIdentifyLabel.Name = "autoIdentifyLabel";
            this.autoIdentifyLabel.Size = new System.Drawing.Size(160, 31);
            this.autoIdentifyLabel.TabIndex = 22;
            this.autoIdentifyLabel.Text = "Auto Identify";
            this.autoIdentifyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // autoIdentifyButton
            // 
            this.autoIdentifyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoIdentifyButton.Location = new System.Drawing.Point(75, 53);
            this.autoIdentifyButton.Margin = new System.Windows.Forms.Padding(4);
            this.autoIdentifyButton.Name = "autoIdentifyButton";
            this.autoIdentifyButton.Size = new System.Drawing.Size(80, 30);
            this.autoIdentifyButton.TabIndex = 21;
            this.autoIdentifyButton.Text = "OFF";
            this.autoIdentifyButton.UseVisualStyleBackColor = true;
            this.autoIdentifyButton.Click += new System.EventHandler(this.autoIdentifyButton_Click);
            // 
            // drawRectangleCheckbox
            // 
            this.drawRectangleCheckbox.AutoSize = true;
            this.drawRectangleCheckbox.Checked = true;
            this.drawRectangleCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawRectangleCheckbox.Location = new System.Drawing.Point(52, 89);
            this.drawRectangleCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.drawRectangleCheckbox.Name = "drawRectangleCheckbox";
            this.drawRectangleCheckbox.Size = new System.Drawing.Size(183, 21);
            this.drawRectangleCheckbox.TabIndex = 20;
            this.drawRectangleCheckbox.Text = "Show Face Rectangle";
            this.drawRectangleCheckbox.UseVisualStyleBackColor = true;
            this.drawRectangleCheckbox.CheckedChanged += new System.EventHandler(this.drawRectangleCheckbox_CheckedChanged);
            // 
            // mainTabLabel
            // 
            this.mainTabLabel.BackColor = System.Drawing.SystemColors.Control;
            this.mainTabLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainTabLabel.Location = new System.Drawing.Point(14, 17);
            this.mainTabLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mainTabLabel.Name = "mainTabLabel";
            this.mainTabLabel.Size = new System.Drawing.Size(267, 49);
            this.mainTabLabel.TabIndex = 19;
            this.mainTabLabel.Text = "To toggle full screen mode, click the display box.";
            this.mainTabLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // identifyButton
            // 
            this.identifyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.identifyButton.Location = new System.Drawing.Point(60, 208);
            this.identifyButton.Margin = new System.Windows.Forms.Padding(4);
            this.identifyButton.Name = "identifyButton";
            this.identifyButton.Size = new System.Drawing.Size(190, 40);
            this.identifyButton.TabIndex = 18;
            this.identifyButton.Text = "Identify Now";
            this.identifyButton.UseVisualStyleBackColor = true;
            this.identifyButton.Click += new System.EventHandler(this.identifyButton_Click);
            // 
            // showEyeCheckBox
            // 
            this.showEyeCheckBox.AutoSize = true;
            this.showEyeCheckBox.Location = new System.Drawing.Point(52, 179);
            this.showEyeCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.showEyeCheckBox.Name = "showEyeCheckBox";
            this.showEyeCheckBox.Size = new System.Drawing.Size(183, 21);
            this.showEyeCheckBox.TabIndex = 12;
            this.showEyeCheckBox.Text = "Show Eye Confidence";
            this.showEyeCheckBox.UseVisualStyleBackColor = true;
            // 
            // faceConfCheckBox
            // 
            this.faceConfCheckBox.AutoSize = true;
            this.faceConfCheckBox.Checked = true;
            this.faceConfCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.faceConfCheckBox.Location = new System.Drawing.Point(52, 122);
            this.faceConfCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.faceConfCheckBox.Name = "faceConfCheckBox";
            this.faceConfCheckBox.Size = new System.Drawing.Size(191, 21);
            this.faceConfCheckBox.TabIndex = 11;
            this.faceConfCheckBox.Text = "Show Face Confidence";
            this.faceConfCheckBox.UseVisualStyleBackColor = true;
            // 
            // drawEyesCheckBox
            // 
            this.drawEyesCheckBox.AutoSize = true;
            this.drawEyesCheckBox.Location = new System.Drawing.Point(52, 151);
            this.drawEyesCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.drawEyesCheckBox.Name = "drawEyesCheckBox";
            this.drawEyesCheckBox.Size = new System.Drawing.Size(102, 21);
            this.drawEyesCheckBox.TabIndex = 3;
            this.drawEyesCheckBox.Text = "Mark Eyes";
            this.drawEyesCheckBox.UseVisualStyleBackColor = true;
            // 
            // enrollmentTab
            // 
            this.enrollmentTab.Controls.Add(this.enrollInstructions);
            this.enrollmentTab.Controls.Add(this.videoFileTxt);
            this.enrollmentTab.Controls.Add(this.enrollButton);
            this.enrollmentTab.Controls.Add(this.selectFileButton);
            this.enrollmentTab.Controls.Add(this.enrollLNameLabel);
            this.enrollmentTab.Controls.Add(this.enrollFNameLabel);
            this.enrollmentTab.Controls.Add(this.lastNameTextBox);
            this.enrollmentTab.Controls.Add(this.firstNameTextBox);
            this.enrollmentTab.Location = new System.Drawing.Point(4, 25);
            this.enrollmentTab.Margin = new System.Windows.Forms.Padding(4);
            this.enrollmentTab.Name = "enrollmentTab";
            this.enrollmentTab.Padding = new System.Windows.Forms.Padding(4);
            this.enrollmentTab.Size = new System.Drawing.Size(292, 396);
            this.enrollmentTab.TabIndex = 1;
            this.enrollmentTab.Text = "Enroll";
            this.enrollmentTab.UseVisualStyleBackColor = true;
            // 
            // enrollInstructions
            // 
            this.enrollInstructions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.enrollInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enrollInstructions.Location = new System.Drawing.Point(10, 8);
            this.enrollInstructions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.enrollInstructions.Name = "enrollInstructions";
            this.enrollInstructions.Size = new System.Drawing.Size(270, 70);
            this.enrollInstructions.TabIndex = 14;
            this.enrollInstructions.Text = "Please enter your first and last names. Then smile for the camera, as it is going" +
    " to take your picture when you click enroll.";
            this.enrollInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // videoFileTxt
            // 
            this.videoFileTxt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.videoFileTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videoFileTxt.Location = new System.Drawing.Point(51, 256);
            this.videoFileTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.videoFileTxt.Name = "videoFileTxt";
            this.videoFileTxt.Size = new System.Drawing.Size(207, 25);
            this.videoFileTxt.TabIndex = 13;
            this.videoFileTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // enrollButton
            // 
            this.enrollButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enrollButton.Location = new System.Drawing.Point(72, 297);
            this.enrollButton.Margin = new System.Windows.Forms.Padding(4);
            this.enrollButton.Name = "enrollButton";
            this.enrollButton.Size = new System.Drawing.Size(173, 37);
            this.enrollButton.TabIndex = 6;
            this.enrollButton.Text = "Enroll";
            this.enrollButton.UseVisualStyleBackColor = true;
            this.enrollButton.Click += new System.EventHandler(this.enrollButton_Click);
            // 
            // selectFileButton
            // 
            this.selectFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFileButton.Location = new System.Drawing.Point(74, 202);
            this.selectFileButton.Margin = new System.Windows.Forms.Padding(4);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(163, 53);
            this.selectFileButton.TabIndex = 5;
            this.selectFileButton.Text = "Click to Select Your Video File";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // enrollLNameLabel
            // 
            this.enrollLNameLabel.AutoSize = true;
            this.enrollLNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enrollLNameLabel.Location = new System.Drawing.Point(34, 148);
            this.enrollLNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.enrollLNameLabel.Name = "enrollLNameLabel";
            this.enrollLNameLabel.Size = new System.Drawing.Size(67, 13);
            this.enrollLNameLabel.TabIndex = 3;
            this.enrollLNameLabel.Text = "Last Name";
            // 
            // enrollFNameLabel
            // 
            this.enrollFNameLabel.AutoSize = true;
            this.enrollFNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enrollFNameLabel.Location = new System.Drawing.Point(36, 98);
            this.enrollFNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.enrollFNameLabel.Name = "enrollFNameLabel";
            this.enrollFNameLabel.Size = new System.Drawing.Size(67, 13);
            this.enrollFNameLabel.TabIndex = 2;
            this.enrollFNameLabel.Text = "First Name";
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastNameTextBox.Location = new System.Drawing.Point(67, 166);
            this.lastNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(185, 23);
            this.lastNameTextBox.TabIndex = 1;
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstNameTextBox.Location = new System.Drawing.Point(69, 118);
            this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(185, 23);
            this.firstNameTextBox.TabIndex = 0;
            // 
            // editProfileTab
            // 
            this.editProfileTab.Controls.Add(this.accessLevelDropDown);
            this.editProfileTab.Controls.Add(this.profileAccessLbl);
            this.editProfileTab.Controls.Add(this.profileUpdateUserBtn);
            this.editProfileTab.Controls.Add(this.profileFNameTxt);
            this.editProfileTab.Controls.Add(this.profileLNameTxt);
            this.editProfileTab.Controls.Add(this.profileFNameLbl);
            this.editProfileTab.Controls.Add(this.profileLNameLbl);
            this.editProfileTab.Controls.Add(this.profileVideoNameLbl);
            this.editProfileTab.Controls.Add(this.profileUserId);
            this.editProfileTab.Controls.Add(this.profileDeleteButton);
            this.editProfileTab.Controls.Add(this.UserIdDropLbl);
            this.editProfileTab.Controls.Add(this.fillUserInfoButton);
            this.editProfileTab.Controls.Add(this.userIdDropDown);
            this.editProfileTab.Controls.Add(this.profileUserIdLbl);
            this.editProfileTab.Controls.Add(this.profileUserVideo);
            this.editProfileTab.Controls.Add(this.changeVideoButton);
            this.editProfileTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editProfileTab.Location = new System.Drawing.Point(4, 25);
            this.editProfileTab.Margin = new System.Windows.Forms.Padding(4);
            this.editProfileTab.Name = "editProfileTab";
            this.editProfileTab.Padding = new System.Windows.Forms.Padding(4);
            this.editProfileTab.Size = new System.Drawing.Size(292, 396);
            this.editProfileTab.TabIndex = 3;
            this.editProfileTab.Text = "Profile";
            this.editProfileTab.UseVisualStyleBackColor = true;
            // 
            // accessLevelDropDown
            // 
            this.accessLevelDropDown.Enabled = false;
            this.accessLevelDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accessLevelDropDown.FormattingEnabled = true;
            this.accessLevelDropDown.Location = new System.Drawing.Point(121, 242);
            this.accessLevelDropDown.Margin = new System.Windows.Forms.Padding(4);
            this.accessLevelDropDown.Name = "accessLevelDropDown";
            this.accessLevelDropDown.Size = new System.Drawing.Size(132, 21);
            this.accessLevelDropDown.TabIndex = 6;
            // 
            // profileAccessLbl
            // 
            this.profileAccessLbl.AutoSize = true;
            this.profileAccessLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileAccessLbl.Location = new System.Drawing.Point(15, 245);
            this.profileAccessLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.profileAccessLbl.Name = "profileAccessLbl";
            this.profileAccessLbl.Size = new System.Drawing.Size(83, 13);
            this.profileAccessLbl.TabIndex = 15;
            this.profileAccessLbl.Text = "Access Level";
            this.profileAccessLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // profileUpdateUserBtn
            // 
            this.profileUpdateUserBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileUpdateUserBtn.Location = new System.Drawing.Point(16, 291);
            this.profileUpdateUserBtn.Margin = new System.Windows.Forms.Padding(4);
            this.profileUpdateUserBtn.Name = "profileUpdateUserBtn";
            this.profileUpdateUserBtn.Size = new System.Drawing.Size(125, 30);
            this.profileUpdateUserBtn.TabIndex = 7;
            this.profileUpdateUserBtn.Text = "Update User";
            this.profileUpdateUserBtn.UseVisualStyleBackColor = true;
            this.profileUpdateUserBtn.Click += new System.EventHandler(this.profileUpdateUserBtn_Click);
            // 
            // profileFNameTxt
            // 
            this.profileFNameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileFNameTxt.Location = new System.Drawing.Point(120, 101);
            this.profileFNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.profileFNameTxt.Name = "profileFNameTxt";
            this.profileFNameTxt.Size = new System.Drawing.Size(132, 20);
            this.profileFNameTxt.TabIndex = 3;
            // 
            // profileLNameTxt
            // 
            this.profileLNameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileLNameTxt.Location = new System.Drawing.Point(121, 138);
            this.profileLNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.profileLNameTxt.Name = "profileLNameTxt";
            this.profileLNameTxt.Size = new System.Drawing.Size(132, 20);
            this.profileLNameTxt.TabIndex = 4;
            // 
            // profileFNameLbl
            // 
            this.profileFNameLbl.AutoSize = true;
            this.profileFNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileFNameLbl.Location = new System.Drawing.Point(31, 104);
            this.profileFNameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.profileFNameLbl.Name = "profileFNameLbl";
            this.profileFNameLbl.Size = new System.Drawing.Size(67, 13);
            this.profileFNameLbl.TabIndex = 11;
            this.profileFNameLbl.Text = "First Name";
            this.profileFNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // profileLNameLbl
            // 
            this.profileLNameLbl.AutoSize = true;
            this.profileLNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileLNameLbl.Location = new System.Drawing.Point(31, 141);
            this.profileLNameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.profileLNameLbl.Name = "profileLNameLbl";
            this.profileLNameLbl.Size = new System.Drawing.Size(67, 13);
            this.profileLNameLbl.TabIndex = 10;
            this.profileLNameLbl.Text = "Last Name";
            this.profileLNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // profileVideoNameLbl
            // 
            this.profileVideoNameLbl.AutoSize = true;
            this.profileVideoNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileVideoNameLbl.Location = new System.Drawing.Point(35, 180);
            this.profileVideoNameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.profileVideoNameLbl.Name = "profileVideoNameLbl";
            this.profileVideoNameLbl.Size = new System.Drawing.Size(63, 13);
            this.profileVideoNameLbl.TabIndex = 9;
            this.profileVideoNameLbl.Text = "Video File";
            this.profileVideoNameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // profileUserId
            // 
            this.profileUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileUserId.Location = new System.Drawing.Point(120, 65);
            this.profileUserId.Margin = new System.Windows.Forms.Padding(4);
            this.profileUserId.Name = "profileUserId";
            this.profileUserId.Size = new System.Drawing.Size(132, 20);
            this.profileUserId.TabIndex = 2;
            // 
            // profileDeleteButton
            // 
            this.profileDeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileDeleteButton.Location = new System.Drawing.Point(155, 291);
            this.profileDeleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.profileDeleteButton.Name = "profileDeleteButton";
            this.profileDeleteButton.Size = new System.Drawing.Size(125, 30);
            this.profileDeleteButton.TabIndex = 8;
            this.profileDeleteButton.Text = "Delete User";
            this.profileDeleteButton.UseVisualStyleBackColor = true;
            this.profileDeleteButton.Click += new System.EventHandler(this.profileDeleteButton_Click);
            // 
            // UserIdDropLbl
            // 
            this.UserIdDropLbl.AutoSize = true;
            this.UserIdDropLbl.Location = new System.Drawing.Point(67, 15);
            this.UserIdDropLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserIdDropLbl.Name = "UserIdDropLbl";
            this.UserIdDropLbl.Size = new System.Drawing.Size(129, 13);
            this.UserIdDropLbl.TabIndex = 1;
            this.UserIdDropLbl.Text = "Select user to update";
            // 
            // fillUserInfoButton
            // 
            this.fillUserInfoButton.Location = new System.Drawing.Point(91, 512);
            this.fillUserInfoButton.Margin = new System.Windows.Forms.Padding(4);
            this.fillUserInfoButton.Name = "fillUserInfoButton";
            this.fillUserInfoButton.Size = new System.Drawing.Size(100, 28);
            this.fillUserInfoButton.TabIndex = 2;
            this.fillUserInfoButton.Text = "Update My Info";
            this.fillUserInfoButton.UseVisualStyleBackColor = true;
            // 
            // userIdDropDown
            // 
            this.userIdDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userIdDropDown.FormattingEnabled = true;
            this.userIdDropDown.Location = new System.Drawing.Point(73, 33);
            this.userIdDropDown.Margin = new System.Windows.Forms.Padding(4);
            this.userIdDropDown.Name = "userIdDropDown";
            this.userIdDropDown.Size = new System.Drawing.Size(160, 21);
            this.userIdDropDown.TabIndex = 1;
            this.userIdDropDown.SelectionChangeCommitted += new System.EventHandler(this.userIdDropDown_SelectionChangeCommitted);
            // 
            // profileUserIdLbl
            // 
            this.profileUserIdLbl.AutoSize = true;
            this.profileUserIdLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileUserIdLbl.Location = new System.Drawing.Point(48, 68);
            this.profileUserIdLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.profileUserIdLbl.Name = "profileUserIdLbl";
            this.profileUserIdLbl.Size = new System.Drawing.Size(50, 13);
            this.profileUserIdLbl.TabIndex = 8;
            this.profileUserIdLbl.Text = "User ID";
            this.profileUserIdLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // profileUserVideo
            // 
            this.profileUserVideo.Enabled = false;
            this.profileUserVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.profileUserVideo.Location = new System.Drawing.Point(121, 177);
            this.profileUserVideo.Margin = new System.Windows.Forms.Padding(4);
            this.profileUserVideo.Multiline = true;
            this.profileUserVideo.Name = "profileUserVideo";
            this.profileUserVideo.Size = new System.Drawing.Size(130, 25);
            this.profileUserVideo.TabIndex = 1;
            this.profileUserVideo.TabStop = false;
            // 
            // changeVideoButton
            // 
            this.changeVideoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeVideoButton.Location = new System.Drawing.Point(122, 207);
            this.changeVideoButton.Margin = new System.Windows.Forms.Padding(4);
            this.changeVideoButton.Name = "changeVideoButton";
            this.changeVideoButton.Size = new System.Drawing.Size(130, 25);
            this.changeVideoButton.TabIndex = 5;
            this.changeVideoButton.Text = "Select new Video";
            this.changeVideoButton.UseVisualStyleBackColor = true;
            this.changeVideoButton.Click += new System.EventHandler(this.changeVideoButton_Click);
            // 
            // aboutTab
            // 
            this.aboutTab.Controls.Add(this.textBox2);
            this.aboutTab.Controls.Add(this.aboutTextBox);
            this.aboutTab.Controls.Add(this.dennyTextBox);
            this.aboutTab.Controls.Add(this.textBox1);
            this.aboutTab.Controls.Add(this.myNameTextBox);
            this.aboutTab.Location = new System.Drawing.Point(4, 25);
            this.aboutTab.Margin = new System.Windows.Forms.Padding(4);
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Padding = new System.Windows.Forms.Padding(4);
            this.aboutTab.Size = new System.Drawing.Size(292, 396);
            this.aboutTab.TabIndex = 2;
            this.aboutTab.Text = "About";
            this.aboutTab.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(15, 281);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(265, 97);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "the Biometrics Faculty Lead at Davenport University, using the Neurotechnology so" +
    "ftware developement kit version 4.2.";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // aboutTextBox
            // 
            this.aboutTextBox.Enabled = false;
            this.aboutTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutTextBox.Location = new System.Drawing.Point(14, 17);
            this.aboutTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.aboutTextBox.Multiline = true;
            this.aboutTextBox.Name = "aboutTextBox";
            this.aboutTextBox.Size = new System.Drawing.Size(265, 52);
            this.aboutTextBox.TabIndex = 0;
            this.aboutTextBox.Text = "This Application was developed by";
            this.aboutTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dennyTextBox
            // 
            this.dennyTextBox.Enabled = false;
            this.dennyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dennyTextBox.Location = new System.Drawing.Point(15, 233);
            this.dennyTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.dennyTextBox.Name = "dennyTextBox";
            this.dennyTextBox.Size = new System.Drawing.Size(265, 32);
            this.dennyTextBox.TabIndex = 8;
            this.dennyTextBox.Text = "Prof. Denton Bobeldyk";
            this.dennyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(14, 129);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(265, 90);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "as his Capstone project at Davenport University in the winter of 2015 under the l" +
    "eadership of";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // myNameTextBox
            // 
            this.myNameTextBox.Enabled = false;
            this.myNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myNameTextBox.Location = new System.Drawing.Point(14, 84);
            this.myNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.myNameTextBox.Name = "myNameTextBox";
            this.myNameTextBox.Size = new System.Drawing.Size(265, 32);
            this.myNameTextBox.TabIndex = 7;
            this.myNameTextBox.Text = "Mark Van Vleet";
            this.myNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // selectFileDialog
            // 
            this.selectFileDialog.FileName = "selectFileDialog";
            this.selectFileDialog.Filter = "mp4 Video Files (*.mp4)|*.mp4|Video Files|*.wmv|Windows Media Download (*.wmd)|*." +
    "wmd|Audio Visual Interleave (*.avi)|*.avi|Moving Picture Experts Group (*.mpg, *" +
    ".mpeg, *.mp3)|*.mpg, *.mpeg, *.mp3";
            // 
            // insetPictureBox
            // 
            this.insetPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insetPictureBox.Location = new System.Drawing.Point(30, 509);
            this.insetPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.insetPictureBox.Name = "insetPictureBox";
            this.insetPictureBox.Size = new System.Drawing.Size(225, 225);
            this.insetPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.insetPictureBox.TabIndex = 3;
            this.insetPictureBox.TabStop = false;
            this.insetPictureBox.Visible = false;
            // 
            // updateFileDialog
            // 
            this.updateFileDialog.FileName = "updateFileDialog";
            this.updateFileDialog.Filter = "mp4 Video Files (*.mp4)|*.mp4|Video Files|*.wmv|Windows Media Download (*.wmd)|*." +
    "wmd|Audio Visual Interleave (*.avi)|*.avi|Moving Picture Experts Group (*.mpg, *" +
    ".mpeg, *.mp3)|*.mpg, *.mpeg, *.mp3";
            // 
            // enrolledImageBox
            // 
            this.enrolledImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.enrolledImageBox.BackColor = System.Drawing.Color.Transparent;
            this.enrolledImageBox.Controls.Add(this.enrolledImageNameLabel);
            this.enrolledImageBox.Controls.Add(this.enrolledImagePictureBox);
            this.enrolledImageBox.Location = new System.Drawing.Point(968, 477);
            this.enrolledImageBox.Margin = new System.Windows.Forms.Padding(4);
            this.enrolledImageBox.Name = "enrolledImageBox";
            this.enrolledImageBox.Padding = new System.Windows.Forms.Padding(4);
            this.enrolledImageBox.Size = new System.Drawing.Size(280, 280);
            this.enrolledImageBox.TabIndex = 4;
            this.enrolledImageBox.TabStop = false;
            this.enrolledImageBox.Visible = false;
            // 
            // enrolledImageNameLabel
            // 
            this.enrolledImageNameLabel.AutoSize = true;
            this.enrolledImageNameLabel.Location = new System.Drawing.Point(27, 18);
            this.enrolledImageNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.enrolledImageNameLabel.Name = "enrolledImageNameLabel";
            this.enrolledImageNameLabel.Size = new System.Drawing.Size(63, 17);
            this.enrolledImageNameLabel.TabIndex = 1;
            this.enrolledImageNameLabel.Text = "Grab me";
            // 
            // videoPlayer
            // 
            this.videoPlayer.Enabled = true;
            this.videoPlayer.Location = new System.Drawing.Point(0, 0);
            this.videoPlayer.Margin = new System.Windows.Forms.Padding(4);
            this.videoPlayer.Name = "videoPlayer";
            this.videoPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("videoPlayer.OcxState")));
            this.videoPlayer.Size = new System.Drawing.Size(140, 57);
            this.videoPlayer.TabIndex = 6;
            this.videoPlayer.Visible = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 762);
            this.Controls.Add(this.enrolledImageBox);
            this.Controls.Add(this.videoPlayer);
            this.Controls.Add(this.insetPictureBox);
            this.Controls.Add(this.operationsTabControl);
            this.Controls.Add(this.mainFeedPictureBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Davenport University Facial Recognition Application by Mark Van Vleet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.enrolledImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPictureBox)).EndInit();
            this.operationsTabControl.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.mainTab.PerformLayout();
            this.autoIdentifyGroupBox.ResumeLayout(false);
            this.enrollmentTab.ResumeLayout(false);
            this.enrollmentTab.PerformLayout();
            this.editProfileTab.ResumeLayout(false);
            this.editProfileTab.PerformLayout();
            this.aboutTab.ResumeLayout(false);
            this.aboutTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.insetPictureBox)).EndInit();
            this.enrolledImageBox.ResumeLayout(false);
            this.enrolledImageBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mainFeedPictureBox;
        private System.Windows.Forms.TabControl operationsTabControl;
        private System.Windows.Forms.TabPage mainTab;
        private System.Windows.Forms.TabPage enrollmentTab;
        private System.Windows.Forms.ComboBox userIdDropDown;
        private System.Windows.Forms.TabPage aboutTab;
        private System.Windows.Forms.CheckBox drawEyesCheckBox;
        private System.Windows.Forms.CheckBox showEyeCheckBox;
        private System.Windows.Forms.CheckBox faceConfCheckBox;
        private System.Windows.Forms.Label enrollFNameLabel;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TabPage editProfileTab;
        private System.Windows.Forms.Label enrollLNameLabel;
        private System.Windows.Forms.OpenFileDialog selectFileDialog;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.PictureBox insetPictureBox;
        private System.Windows.Forms.Button identifyButton;
        private System.Windows.Forms.Button enrollButton;
        private System.Windows.Forms.Label profileUserIdLbl;
        private System.Windows.Forms.TextBox profileUserId;
        private System.Windows.Forms.Label UserIdDropLbl;
        private System.Windows.Forms.PictureBox enrolledImagePictureBox;
        private AxWMPLib.AxWindowsMediaPlayer videoPlayer;
        private System.Windows.Forms.Button profileDeleteButton;
        private System.Windows.Forms.TextBox profileUserVideo;
        private System.Windows.Forms.Button changeVideoButton;
        private System.Windows.Forms.OpenFileDialog updateFileDialog;
        private System.Windows.Forms.Button fillUserInfoButton;
        private System.Windows.Forms.Label mainTabLabel;
        private System.Windows.Forms.CheckBox drawRectangleCheckbox;
        private System.Windows.Forms.Label videoFileTxt;
        private System.Windows.Forms.GroupBox enrolledImageBox;
        private System.Windows.Forms.Label enrolledImageNameLabel;
        private System.Windows.Forms.Label enrollInstructions;
        private System.Windows.Forms.Label profileFNameLbl;
        private System.Windows.Forms.Label profileLNameLbl;
        private System.Windows.Forms.Label profileVideoNameLbl;
        private System.Windows.Forms.Button profileUpdateUserBtn;
        private System.Windows.Forms.TextBox profileFNameTxt;
        private System.Windows.Forms.TextBox profileLNameTxt;
        private System.Windows.Forms.ComboBox accessLevelDropDown;
        private System.Windows.Forms.Label profileAccessLbl;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox aboutTextBox;
        private System.Windows.Forms.TextBox dennyTextBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox myNameTextBox;
        private System.Windows.Forms.GroupBox autoIdentifyGroupBox;
        private System.Windows.Forms.Label autoIdentifyLabel;
        private System.Windows.Forms.Button autoIdentifyButton;

    }
}

