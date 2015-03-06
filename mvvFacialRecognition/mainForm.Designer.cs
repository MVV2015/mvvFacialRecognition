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
            this.currentView.DetectionDetails = null;
            this.currentView.FaceIds = null;
            this.currentView.Image = null;
            this.currentView.Location = new System.Drawing.Point(3, 1);
            this.currentView.Name = "currentView";
            this.currentView.Size = new System.Drawing.Size(314, 290);
            this.currentView.TabIndex = 2;
            // 
            // mainFeedPictureBox
            // 
            this.mainFeedPictureBox.Location = new System.Drawing.Point(100, 110);
            this.mainFeedPictureBox.Name = "mainFeedPictureBox";
            this.mainFeedPictureBox.Size = new System.Drawing.Size(320, 250);
            this.mainFeedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainFeedPictureBox.TabIndex = 0;
            this.mainFeedPictureBox.TabStop = false;
            // 
            // operationsTabControl
            // 
            this.operationsTabControl.Controls.Add(this.userInfoTab);
            this.operationsTabControl.Controls.Add(this.NleFaceSettingsTab);
            this.operationsTabControl.Controls.Add(this.NlViewSettingsTab);
            this.operationsTabControl.Location = new System.Drawing.Point(591, 30);
            this.operationsTabControl.Name = "operationsTabControl";
            this.operationsTabControl.SelectedIndex = 0;
            this.operationsTabControl.Size = new System.Drawing.Size(279, 401);
            this.operationsTabControl.TabIndex = 1;
            // 
            // userInfoTab
            // 
            this.userInfoTab.Controls.Add(this.groupBox1);
            this.userInfoTab.Controls.Add(this.userIdDropDown);
            this.userInfoTab.Location = new System.Drawing.Point(4, 22);
            this.userInfoTab.Name = "userInfoTab";
            this.userInfoTab.Padding = new System.Windows.Forms.Padding(3);
            this.userInfoTab.Size = new System.Drawing.Size(271, 375);
            this.userInfoTab.TabIndex = 0;
            this.userInfoTab.Text = "User Information";
            this.userInfoTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bitmapSelectButton);
            this.groupBox1.Controls.Add(this.NlViewSelectButton);
            this.groupBox1.Location = new System.Drawing.Point(65, 206);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // bitmapSelectButton
            // 
            this.bitmapSelectButton.AutoSize = true;
            this.bitmapSelectButton.Location = new System.Drawing.Point(32, 57);
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
            this.NlViewSelectButton.Location = new System.Drawing.Point(32, 33);
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
            this.userIdDropDown.Location = new System.Drawing.Point(41, 70);
            this.userIdDropDown.Name = "userIdDropDown";
            this.userIdDropDown.Size = new System.Drawing.Size(121, 21);
            this.userIdDropDown.TabIndex = 0;
            // 
            // NleFaceSettingsTab
            // 
            this.NleFaceSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.NleFaceSettingsTab.Name = "NleFaceSettingsTab";
            this.NleFaceSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.NleFaceSettingsTab.Size = new System.Drawing.Size(271, 375);
            this.NleFaceSettingsTab.TabIndex = 1;
            this.NleFaceSettingsTab.Text = "NleFace Settings";
            this.NleFaceSettingsTab.UseVisualStyleBackColor = true;
            // 
            // NlViewSettingsTab
            // 
            this.NlViewSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.NlViewSettingsTab.Name = "NlViewSettingsTab";
            this.NlViewSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.NlViewSettingsTab.Size = new System.Drawing.Size(271, 375);
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
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.mainFeedPictureBox)).EndInit();
            this.operationsTabControl.ResumeLayout(false);
            this.userInfoTab.ResumeLayout(false);
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

    }
}

