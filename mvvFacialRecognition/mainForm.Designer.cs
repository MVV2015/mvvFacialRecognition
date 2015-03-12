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
            this.detectAllcheckBox = new System.Windows.Forms.CheckBox();
            this.showEyeCheckBox = new System.Windows.Forms.CheckBox();
            this.faceConfCheckBox = new System.Windows.Forms.CheckBox();
            this.drawEyesCheckBox = new System.Windows.Forms.CheckBox();
            this.detectAllFeaturesButton = new System.Windows.Forms.RadioButton();
            this.noseConfButton = new System.Windows.Forms.RadioButton();
            this.mouthConfButton = new System.Windows.Forms.RadioButton();
            this.markMouthButton = new System.Windows.Forms.RadioButton();
            this.markNoseButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bitmapSelectButton = new System.Windows.Forms.RadioButton();
            this.NlViewSelectButton = new System.Windows.Forms.RadioButton();
            this.userIdDropDown = new System.Windows.Forms.ComboBox();
            this.NleFaceSettingsTab = new System.Windows.Forms.TabPage();
            this.NlViewSettingsTab = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPictureBox)).BeginInit();
            this.operationsTabControl.SuspendLayout();
            this.userInfoTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentView
            // 
            this.currentView.AutoScroll = true;
            this.currentView.AutoScrollMinSize = new System.Drawing.Size(2, 2);
            this.currentView.DetectionDetails = null;
            this.currentView.FaceIds = null;
            this.currentView.FaceRectangleWidth = 3;
            this.currentView.Image = null;
            this.currentView.Location = new System.Drawing.Point(100, 110);
            this.currentView.Name = "currentView";
            this.currentView.ShowEyes = false;
            this.currentView.ShowMouth = false;
            this.currentView.ShowNose = false;
            this.currentView.Size = new System.Drawing.Size(480, 360);
            this.currentView.TabIndex = 2;
            this.currentView.Zoom = 2F;
            // 
            // mainFeedPictureBox
            // 
            this.mainFeedPictureBox.Location = new System.Drawing.Point(100, 110);
            this.mainFeedPictureBox.Name = "mainFeedPictureBox";
            this.mainFeedPictureBox.Size = new System.Drawing.Size(480, 360);
            this.mainFeedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainFeedPictureBox.TabIndex = 0;
            this.mainFeedPictureBox.TabStop = false;
            // 
            // operationsTabControl
            // 
            this.operationsTabControl.Controls.Add(this.userInfoTab);
            this.operationsTabControl.Controls.Add(this.NleFaceSettingsTab);
            this.operationsTabControl.Controls.Add(this.NlViewSettingsTab);
            this.operationsTabControl.Location = new System.Drawing.Point(461, 30);
            this.operationsTabControl.Name = "operationsTabControl";
            this.operationsTabControl.SelectedIndex = 0;
            this.operationsTabControl.Size = new System.Drawing.Size(409, 401);
            this.operationsTabControl.TabIndex = 1;
            // 
            // userInfoTab
            // 
            this.userInfoTab.Controls.Add(this.detectAllcheckBox);
            this.userInfoTab.Controls.Add(this.showEyeCheckBox);
            this.userInfoTab.Controls.Add(this.faceConfCheckBox);
            this.userInfoTab.Controls.Add(this.drawEyesCheckBox);
            this.userInfoTab.Controls.Add(this.detectAllFeaturesButton);
            this.userInfoTab.Controls.Add(this.noseConfButton);
            this.userInfoTab.Controls.Add(this.mouthConfButton);
            this.userInfoTab.Controls.Add(this.markMouthButton);
            this.userInfoTab.Controls.Add(this.markNoseButton);
            this.userInfoTab.Controls.Add(this.groupBox1);
            this.userInfoTab.Controls.Add(this.userIdDropDown);
            this.userInfoTab.Location = new System.Drawing.Point(4, 22);
            this.userInfoTab.Name = "userInfoTab";
            this.userInfoTab.Padding = new System.Windows.Forms.Padding(3);
            this.userInfoTab.Size = new System.Drawing.Size(401, 375);
            this.userInfoTab.TabIndex = 0;
            this.userInfoTab.Text = "User Information";
            this.userInfoTab.UseVisualStyleBackColor = true;
            // 
            // detectAllcheckBox
            // 
            this.detectAllcheckBox.AutoSize = true;
            this.detectAllcheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detectAllcheckBox.Location = new System.Drawing.Point(221, 214);
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
            this.showEyeCheckBox.Location = new System.Drawing.Point(241, 186);
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
            this.faceConfCheckBox.Location = new System.Drawing.Point(241, 163);
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
            this.drawEyesCheckBox.Location = new System.Drawing.Point(241, 141);
            this.drawEyesCheckBox.Name = "drawEyesCheckBox";
            this.drawEyesCheckBox.Size = new System.Drawing.Size(76, 17);
            this.drawEyesCheckBox.TabIndex = 3;
            this.drawEyesCheckBox.Text = "Mark Eyes";
            this.drawEyesCheckBox.UseVisualStyleBackColor = true;
            this.drawEyesCheckBox.CheckedChanged += new System.EventHandler(this.drawEyesCheckBox_CheckedChanged);
            // 
            // detectAllFeaturesButton
            // 
            this.detectAllFeaturesButton.AutoSize = true;
            this.detectAllFeaturesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detectAllFeaturesButton.Location = new System.Drawing.Point(38, 213);
            this.detectAllFeaturesButton.Name = "detectAllFeaturesButton";
            this.detectAllFeaturesButton.Size = new System.Drawing.Size(134, 17);
            this.detectAllFeaturesButton.TabIndex = 10;
            this.detectAllFeaturesButton.TabStop = true;
            this.detectAllFeaturesButton.Text = "Detect All Features";
            this.detectAllFeaturesButton.UseVisualStyleBackColor = true;
            // 
            // noseConfButton
            // 
            this.noseConfButton.AutoSize = true;
            this.noseConfButton.Location = new System.Drawing.Point(57, 259);
            this.noseConfButton.Name = "noseConfButton";
            this.noseConfButton.Size = new System.Drawing.Size(137, 17);
            this.noseConfButton.TabIndex = 8;
            this.noseConfButton.TabStop = true;
            this.noseConfButton.Text = "Show Nose Confidence";
            this.noseConfButton.UseVisualStyleBackColor = true;
            this.noseConfButton.Click += new System.EventHandler(this.noseConfButton_Click);
            // 
            // mouthConfButton
            // 
            this.mouthConfButton.AutoSize = true;
            this.mouthConfButton.Location = new System.Drawing.Point(57, 305);
            this.mouthConfButton.Name = "mouthConfButton";
            this.mouthConfButton.Size = new System.Drawing.Size(142, 17);
            this.mouthConfButton.TabIndex = 7;
            this.mouthConfButton.TabStop = true;
            this.mouthConfButton.Text = "Show Mouth Confidence";
            this.mouthConfButton.UseVisualStyleBackColor = true;
            this.mouthConfButton.Click += new System.EventHandler(this.mouthConfButton_Click);
            // 
            // markMouthButton
            // 
            this.markMouthButton.AutoSize = true;
            this.markMouthButton.Location = new System.Drawing.Point(57, 282);
            this.markMouthButton.Name = "markMouthButton";
            this.markMouthButton.Size = new System.Drawing.Size(82, 17);
            this.markMouthButton.TabIndex = 6;
            this.markMouthButton.TabStop = true;
            this.markMouthButton.Text = "Mark Mouth";
            this.markMouthButton.UseVisualStyleBackColor = true;
            this.markMouthButton.Click += new System.EventHandler(this.markMouthButton_Click);
            // 
            // markNoseButton
            // 
            this.markNoseButton.AutoSize = true;
            this.markNoseButton.Location = new System.Drawing.Point(57, 236);
            this.markNoseButton.Name = "markNoseButton";
            this.markNoseButton.Size = new System.Drawing.Size(77, 17);
            this.markNoseButton.TabIndex = 5;
            this.markNoseButton.TabStop = true;
            this.markNoseButton.Text = "Mark Nose";
            this.markNoseButton.UseVisualStyleBackColor = true;
            this.markNoseButton.Click += new System.EventHandler(this.markNoseButton_Click);
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
            // NleFaceSettingsTab
            // 
            this.NleFaceSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.NleFaceSettingsTab.Name = "NleFaceSettingsTab";
            this.NleFaceSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.NleFaceSettingsTab.Size = new System.Drawing.Size(401, 375);
            this.NleFaceSettingsTab.TabIndex = 1;
            this.NleFaceSettingsTab.Text = "NleFace Settings";
            this.NleFaceSettingsTab.UseVisualStyleBackColor = true;
            // 
            // NlViewSettingsTab
            // 
            this.NlViewSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.NlViewSettingsTab.Name = "NlViewSettingsTab";
            this.NlViewSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.NlViewSettingsTab.Size = new System.Drawing.Size(401, 375);
            this.NlViewSettingsTab.TabIndex = 2;
            this.NlViewSettingsTab.Text = "NlView Settings";
            this.NlViewSettingsTab.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 621);
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
            this.ResumeLayout(false);

        }

        #endregion

        private Neurotec.Biometrics.Gui.NLView currentView;
        private System.Windows.Forms.PictureBox mainFeedPictureBox;
        private System.Windows.Forms.TabControl operationsTabControl;
        private System.Windows.Forms.TabPage userInfoTab;
        private System.Windows.Forms.TabPage NleFaceSettingsTab;
        private System.Windows.Forms.ComboBox userIdDropDown;
        private System.Windows.Forms.TabPage NlViewSettingsTab;
        private System.Windows.Forms.RadioButton NlViewSelectButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton bitmapSelectButton;
        private System.Windows.Forms.RadioButton markMouthButton;
        private System.Windows.Forms.RadioButton markNoseButton;
        private System.Windows.Forms.RadioButton noseConfButton;
        private System.Windows.Forms.RadioButton mouthConfButton;
        private System.Windows.Forms.RadioButton detectAllFeaturesButton;
        private System.Windows.Forms.CheckBox drawEyesCheckBox;
        private System.Windows.Forms.CheckBox detectAllcheckBox;
        private System.Windows.Forms.CheckBox showEyeCheckBox;
        private System.Windows.Forms.CheckBox faceConfCheckBox;

    }
}

