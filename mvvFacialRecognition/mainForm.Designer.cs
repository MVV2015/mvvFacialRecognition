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
            this.currentView = new Neurotec.Biometrics.Gui.NLView();
            this.mainFeedPictureBox = new System.Windows.Forms.PictureBox();
            this.operationsTabControl = new System.Windows.Forms.TabControl();
            this.userInfoTab = new System.Windows.Forms.TabPage();
            this.identifyButton = new System.Windows.Forms.Button();
            this.showMouthConfCheckBox = new System.Windows.Forms.CheckBox();
            this.markMouthheckBox = new System.Windows.Forms.CheckBox();
            this.NoseConfCheckBox = new System.Windows.Forms.CheckBox();
            this.markNoseCheckBox = new System.Windows.Forms.CheckBox();
            this.detectAllcheckBox = new System.Windows.Forms.CheckBox();
            this.showEyeCheckBox = new System.Windows.Forms.CheckBox();
            this.faceConfCheckBox = new System.Windows.Forms.CheckBox();
            this.drawEyesCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bitmapSelectButton = new System.Windows.Forms.RadioButton();
            this.NlViewSelectButton = new System.Windows.Forms.RadioButton();
            this.userIdDropDown = new System.Windows.Forms.ComboBox();
            this.UserOptionsTab = new System.Windows.Forms.TabPage();
            this.enrollmentTab = new System.Windows.Forms.TabPage();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.NlViewSettingsTab = new System.Windows.Forms.TabPage();
            this.selectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.enrollmentPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPictureBox)).BeginInit();
            this.operationsTabControl.SuspendLayout();
            this.userInfoTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.enrollmentTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enrollmentPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // currentView
            // 
            this.currentView.AutoScroll = true;
            this.currentView.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.currentView.DetectionDetails = null;
            this.currentView.FaceIds = null;
            this.currentView.FaceRectangleWidth = 3;
            this.currentView.Image = null;
            this.currentView.Location = new System.Drawing.Point(50, 100);
            this.currentView.Name = "currentView";
            this.currentView.ShowEyes = false;
            this.currentView.ShowMouth = false;
            this.currentView.ShowNose = false;
            this.currentView.Size = new System.Drawing.Size(640, 480);
            this.currentView.TabIndex = 2;
            //this.currentView.Paint += new System.Windows.Forms.PaintEventHandler(this.currentView_Paint);
            this.currentView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.currentView_MouseDoubleClick);
            // 
            // mainFeedPictureBox
            // 
            this.mainFeedPictureBox.Location = new System.Drawing.Point(50, 100);
            this.mainFeedPictureBox.Name = "mainFeedPictureBox";
            this.mainFeedPictureBox.Size = new System.Drawing.Size(640, 480);
            this.mainFeedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainFeedPictureBox.TabIndex = 0;
            this.mainFeedPictureBox.TabStop = false;
            // 
            // operationsTabControl
            // 
            this.operationsTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.operationsTabControl.Controls.Add(this.userInfoTab);
            this.operationsTabControl.Controls.Add(this.UserOptionsTab);
            this.operationsTabControl.Controls.Add(this.enrollmentTab);
            this.operationsTabControl.Controls.Add(this.NlViewSettingsTab);
            this.operationsTabControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.operationsTabControl.Location = new System.Drawing.Point(844, 0);
            this.operationsTabControl.Multiline = true;
            this.operationsTabControl.Name = "operationsTabControl";
            this.operationsTabControl.SelectedIndex = 0;
            this.operationsTabControl.Size = new System.Drawing.Size(240, 662);
            this.operationsTabControl.TabIndex = 1;
            // 
            // userInfoTab
            // 
            this.userInfoTab.Controls.Add(this.identifyButton);
            this.userInfoTab.Controls.Add(this.showMouthConfCheckBox);
            this.userInfoTab.Controls.Add(this.markMouthheckBox);
            this.userInfoTab.Controls.Add(this.NoseConfCheckBox);
            this.userInfoTab.Controls.Add(this.markNoseCheckBox);
            this.userInfoTab.Controls.Add(this.detectAllcheckBox);
            this.userInfoTab.Controls.Add(this.showEyeCheckBox);
            this.userInfoTab.Controls.Add(this.faceConfCheckBox);
            this.userInfoTab.Controls.Add(this.drawEyesCheckBox);
            this.userInfoTab.Controls.Add(this.groupBox1);
            this.userInfoTab.Controls.Add(this.userIdDropDown);
            this.userInfoTab.Location = new System.Drawing.Point(23, 4);
            this.userInfoTab.Name = "userInfoTab";
            this.userInfoTab.Padding = new System.Windows.Forms.Padding(3);
            this.userInfoTab.Size = new System.Drawing.Size(213, 654);
            this.userInfoTab.TabIndex = 0;
            this.userInfoTab.Text = "Dislay Options";
            this.userInfoTab.UseVisualStyleBackColor = true;
            // 
            // identifyButton
            // 
            this.identifyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.identifyButton.Location = new System.Drawing.Point(45, 395);
            this.identifyButton.Name = "identifyButton";
            this.identifyButton.Size = new System.Drawing.Size(115, 40);
            this.identifyButton.TabIndex = 18;
            this.identifyButton.Text = "Identify";
            this.identifyButton.UseVisualStyleBackColor = true;
            this.identifyButton.Click += new System.EventHandler(this.identifyButton_Click);
            // 
            // showMouthConfCheckBox
            // 
            this.showMouthConfCheckBox.AutoSize = true;
            this.showMouthConfCheckBox.Location = new System.Drawing.Point(51, 314);
            this.showMouthConfCheckBox.Name = "showMouthConfCheckBox";
            this.showMouthConfCheckBox.Size = new System.Drawing.Size(143, 17);
            this.showMouthConfCheckBox.TabIndex = 17;
            this.showMouthConfCheckBox.Text = "Show Mouth Confidence";
            this.showMouthConfCheckBox.UseVisualStyleBackColor = true;
            this.showMouthConfCheckBox.CheckedChanged += new System.EventHandler(this.showMouthConfCheckBox_CheckedChanged);
            // 
            // markMouthheckBox
            // 
            this.markMouthheckBox.AutoSize = true;
            this.markMouthheckBox.Location = new System.Drawing.Point(51, 291);
            this.markMouthheckBox.Name = "markMouthheckBox";
            this.markMouthheckBox.Size = new System.Drawing.Size(83, 17);
            this.markMouthheckBox.TabIndex = 16;
            this.markMouthheckBox.Text = "Mark Mouth";
            this.markMouthheckBox.UseVisualStyleBackColor = true;
            this.markMouthheckBox.CheckedChanged += new System.EventHandler(this.markMouthheckBox_CheckedChanged);
            // 
            // NoseConfCheckBox
            // 
            this.NoseConfCheckBox.AutoSize = true;
            this.NoseConfCheckBox.Location = new System.Drawing.Point(51, 268);
            this.NoseConfCheckBox.Name = "NoseConfCheckBox";
            this.NoseConfCheckBox.Size = new System.Drawing.Size(138, 17);
            this.NoseConfCheckBox.TabIndex = 15;
            this.NoseConfCheckBox.Text = "Show Nose Confidence";
            this.NoseConfCheckBox.UseVisualStyleBackColor = true;
            this.NoseConfCheckBox.CheckedChanged += new System.EventHandler(this.NoseConfCheckBox_CheckedChanged);
            // 
            // markNoseCheckBox
            // 
            this.markNoseCheckBox.AutoSize = true;
            this.markNoseCheckBox.Location = new System.Drawing.Point(51, 245);
            this.markNoseCheckBox.Name = "markNoseCheckBox";
            this.markNoseCheckBox.Size = new System.Drawing.Size(78, 17);
            this.markNoseCheckBox.TabIndex = 14;
            this.markNoseCheckBox.Text = "Mark Nose";
            this.markNoseCheckBox.UseVisualStyleBackColor = true;
            this.markNoseCheckBox.CheckedChanged += new System.EventHandler(this.markNoseCheckBox_CheckedChanged);
            // 
            // detectAllcheckBox
            // 
            this.detectAllcheckBox.AutoSize = true;
            this.detectAllcheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detectAllcheckBox.Location = new System.Drawing.Point(31, 223);
            this.detectAllcheckBox.Name = "detectAllcheckBox";
            this.detectAllcheckBox.Size = new System.Drawing.Size(135, 17);
            this.detectAllcheckBox.TabIndex = 13;
            this.detectAllcheckBox.Text = "Detect All Features";
            this.detectAllcheckBox.UseVisualStyleBackColor = true;
            this.detectAllcheckBox.CheckedChanged += new System.EventHandler(this.detectAllcheckBox_CheckedChanged);
            // 
            // showEyeCheckBox
            // 
            this.showEyeCheckBox.AutoSize = true;
            this.showEyeCheckBox.Location = new System.Drawing.Point(51, 195);
            this.showEyeCheckBox.Name = "showEyeCheckBox";
            this.showEyeCheckBox.Size = new System.Drawing.Size(131, 17);
            this.showEyeCheckBox.TabIndex = 12;
            this.showEyeCheckBox.Text = "Show Eye Confidence";
            this.showEyeCheckBox.UseVisualStyleBackColor = true;
            this.showEyeCheckBox.CheckedChanged += new System.EventHandler(this.showEyeCheckBox_CheckedChanged);
            // 
            // faceConfCheckBox
            // 
            this.faceConfCheckBox.AutoSize = true;
            this.faceConfCheckBox.Checked = true;
            this.faceConfCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.faceConfCheckBox.Location = new System.Drawing.Point(51, 149);
            this.faceConfCheckBox.Name = "faceConfCheckBox";
            this.faceConfCheckBox.Size = new System.Drawing.Size(137, 17);
            this.faceConfCheckBox.TabIndex = 11;
            this.faceConfCheckBox.Text = "Show Face Confidence";
            this.faceConfCheckBox.UseVisualStyleBackColor = true;
            this.faceConfCheckBox.CheckedChanged += new System.EventHandler(this.faceConfCheckBox_CheckedChanged);
            // 
            // drawEyesCheckBox
            // 
            this.drawEyesCheckBox.AutoSize = true;
            this.drawEyesCheckBox.Location = new System.Drawing.Point(51, 172);
            this.drawEyesCheckBox.Name = "drawEyesCheckBox";
            this.drawEyesCheckBox.Size = new System.Drawing.Size(76, 17);
            this.drawEyesCheckBox.TabIndex = 3;
            this.drawEyesCheckBox.Text = "Mark Eyes";
            this.drawEyesCheckBox.UseVisualStyleBackColor = true;
            this.drawEyesCheckBox.CheckedChanged += new System.EventHandler(this.drawEyesCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bitmapSelectButton);
            this.groupBox1.Controls.Add(this.NlViewSelectButton);
            this.groupBox1.Location = new System.Drawing.Point(40, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 90);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // bitmapSelectButton
            // 
            this.bitmapSelectButton.AutoSize = true;
            this.bitmapSelectButton.Location = new System.Drawing.Point(22, 50);
            this.bitmapSelectButton.Name = "bitmapSelectButton";
            this.bitmapSelectButton.Size = new System.Drawing.Size(83, 17);
            this.bitmapSelectButton.TabIndex = 2;
            this.bitmapSelectButton.TabStop = true;
            this.bitmapSelectButton.Text = "Bitmap View";
            this.bitmapSelectButton.UseVisualStyleBackColor = true;
            this.bitmapSelectButton.CheckedChanged += new System.EventHandler(this.bitmapSelectButton_CheckedChanged);
            // 
            // NlViewSelectButton
            // 
            this.NlViewSelectButton.AutoSize = true;
            this.NlViewSelectButton.Checked = true;
            this.NlViewSelectButton.Location = new System.Drawing.Point(22, 27);
            this.NlViewSelectButton.Name = "NlViewSelectButton";
            this.NlViewSelectButton.Size = new System.Drawing.Size(58, 17);
            this.NlViewSelectButton.TabIndex = 1;
            this.NlViewSelectButton.TabStop = true;
            this.NlViewSelectButton.Text = "NlView";
            this.NlViewSelectButton.UseVisualStyleBackColor = true;
            this.NlViewSelectButton.CheckedChanged += new System.EventHandler(this.NlViewSelectButton_CheckedChanged);
            // 
            // userIdDropDown
            // 
            this.userIdDropDown.FormattingEnabled = true;
            this.userIdDropDown.Location = new System.Drawing.Point(6, 6);
            this.userIdDropDown.Name = "userIdDropDown";
            this.userIdDropDown.Size = new System.Drawing.Size(121, 21);
            this.userIdDropDown.TabIndex = 0;
            // 
            // UserOptionsTab
            // 
            this.UserOptionsTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserOptionsTab.Location = new System.Drawing.Point(23, 4);
            this.UserOptionsTab.Name = "UserOptionsTab";
            this.UserOptionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.UserOptionsTab.Size = new System.Drawing.Size(213, 654);
            this.UserOptionsTab.TabIndex = 3;
            this.UserOptionsTab.Text = "User Options";
            this.UserOptionsTab.UseVisualStyleBackColor = true;
            // 
            // enrollmentTab
            // 
            this.enrollmentTab.Controls.Add(this.selectFileButton);
            this.enrollmentTab.Controls.Add(this.label2);
            this.enrollmentTab.Controls.Add(this.label1);
            this.enrollmentTab.Controls.Add(this.textBox2);
            this.enrollmentTab.Controls.Add(this.firstNameTextBox);
            this.enrollmentTab.Location = new System.Drawing.Point(23, 4);
            this.enrollmentTab.Name = "enrollmentTab";
            this.enrollmentTab.Padding = new System.Windows.Forms.Padding(3);
            this.enrollmentTab.Size = new System.Drawing.Size(213, 654);
            this.enrollmentTab.TabIndex = 1;
            this.enrollmentTab.Text = "Enrollment";
            this.enrollmentTab.UseVisualStyleBackColor = true;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFileButton.Location = new System.Drawing.Point(40, 183);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(115, 23);
            this.selectFileButton.TabIndex = 5;
            this.selectFileButton.Text = "Select Video File";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Last Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "First Name";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(55, 144);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Location = new System.Drawing.Point(55, 93);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.firstNameTextBox.TabIndex = 0;
            // 
            // NlViewSettingsTab
            // 
            this.NlViewSettingsTab.Location = new System.Drawing.Point(23, 4);
            this.NlViewSettingsTab.Name = "NlViewSettingsTab";
            this.NlViewSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.NlViewSettingsTab.Size = new System.Drawing.Size(213, 654);
            this.NlViewSettingsTab.TabIndex = 2;
            this.NlViewSettingsTab.Text = "Admin";
            this.NlViewSettingsTab.UseVisualStyleBackColor = true;
            // 
            // selectFileDialog
            // 
            this.selectFileDialog.FileName = "selectFileDialog";
            // 
            // enrollmentPictureBox
            // 
            this.enrollmentPictureBox.Location = new System.Drawing.Point(25, 25);
            this.enrollmentPictureBox.Name = "enrollmentPictureBox";
            this.enrollmentPictureBox.Size = new System.Drawing.Size(200, 200);
            this.enrollmentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enrollmentPictureBox.TabIndex = 3;
            this.enrollmentPictureBox.TabStop = false;
            this.enrollmentPictureBox.Visible = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 662);
            this.Controls.Add(this.enrollmentPictureBox);
            this.Controls.Add(this.operationsTabControl);
            this.Controls.Add(this.currentView);
            this.Controls.Add(this.mainFeedPictureBox);
            this.Name = "mainForm";
            this.Text = "Main Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPictureBox)).EndInit();
            this.operationsTabControl.ResumeLayout(false);
            this.userInfoTab.ResumeLayout(false);
            this.userInfoTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.enrollmentTab.ResumeLayout(false);
            this.enrollmentTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enrollmentPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Neurotec.Biometrics.Gui.NLView currentView;
        private System.Windows.Forms.PictureBox mainFeedPictureBox;
        private System.Windows.Forms.TabControl operationsTabControl;
        private System.Windows.Forms.TabPage userInfoTab;
        private System.Windows.Forms.TabPage enrollmentTab;
        private System.Windows.Forms.ComboBox userIdDropDown;
        private System.Windows.Forms.TabPage NlViewSettingsTab;
        private System.Windows.Forms.RadioButton NlViewSelectButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton bitmapSelectButton;
        private System.Windows.Forms.CheckBox drawEyesCheckBox;
        private System.Windows.Forms.CheckBox detectAllcheckBox;
        private System.Windows.Forms.CheckBox showEyeCheckBox;
        private System.Windows.Forms.CheckBox faceConfCheckBox;
        private System.Windows.Forms.CheckBox showMouthConfCheckBox;
        private System.Windows.Forms.CheckBox markMouthheckBox;
        private System.Windows.Forms.CheckBox NoseConfCheckBox;
        private System.Windows.Forms.CheckBox markNoseCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TabPage UserOptionsTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog selectFileDialog;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.PictureBox enrollmentPictureBox;
        private System.Windows.Forms.Button identifyButton;

    }
}

