namespace InteractiveSeven.UI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ExeLabel = new System.Windows.Forms.Label();
            this.ExeTextBox = new System.Windows.Forms.TextBox();
            this.ExeBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.topLeftGroup = new System.Windows.Forms.GroupBox();
            this.topLeftColorPicker = new Cyotek.Windows.Forms.ColorEditor();
            this.SetColorsButton = new System.Windows.Forms.Button();
            this.RefreshColorsButton = new System.Windows.Forms.Button();
            this.TwitchConnectGroup = new System.Windows.Forms.GroupBox();
            this.TwitchDisconnectButton = new System.Windows.Forms.Button();
            this.TwitchConnectButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuColorTab = new System.Windows.Forms.TabPage();
            this.botRightColorSwatch = new System.Windows.Forms.Panel();
            this.botLeftColorSwatch = new System.Windows.Forms.Panel();
            this.topRightColorSwatch = new System.Windows.Forms.Panel();
            this.topLeftColorSwatch = new System.Windows.Forms.Panel();
            this.allowChatMenuControl = new System.Windows.Forms.CheckBox();
            this.bottomRightGroup = new System.Windows.Forms.GroupBox();
            this.botRightColorPicker = new Cyotek.Windows.Forms.ColorEditor();
            this.botLeftGroup = new System.Windows.Forms.GroupBox();
            this.botLeftColorPicker = new Cyotek.Windows.Forms.ColorEditor();
            this.topRightGroup = new System.Windows.Forms.GroupBox();
            this.topRightColorPicker = new Cyotek.Windows.Forms.ColorEditor();
            this.partyTab = new System.Windows.Forms.TabPage();
            this.topLeftGroup.SuspendLayout();
            this.TwitchConnectGroup.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.menuColorTab.SuspendLayout();
            this.bottomRightGroup.SuspendLayout();
            this.botLeftGroup.SuspendLayout();
            this.topRightGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExeLabel
            // 
            this.ExeLabel.AutoSize = true;
            this.ExeLabel.Location = new System.Drawing.Point(13, 13);
            this.ExeLabel.Name = "ExeLabel";
            this.ExeLabel.Size = new System.Drawing.Size(100, 13);
            this.ExeLabel.TabIndex = 0;
            this.ExeLabel.Text = "FF7 Executable File";
            // 
            // ExeTextBox
            // 
            this.ExeTextBox.Location = new System.Drawing.Point(120, 13);
            this.ExeTextBox.Name = "ExeTextBox";
            this.ExeTextBox.Size = new System.Drawing.Size(259, 20);
            this.ExeTextBox.TabIndex = 1;
            // 
            // ExeBrowse
            // 
            this.ExeBrowse.Location = new System.Drawing.Point(397, 13);
            this.ExeBrowse.Name = "ExeBrowse";
            this.ExeBrowse.Size = new System.Drawing.Size(75, 23);
            this.ExeBrowse.TabIndex = 2;
            this.ExeBrowse.Text = "Browse";
            this.ExeBrowse.UseVisualStyleBackColor = true;
            this.ExeBrowse.Click += new System.EventHandler(this.ExeBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // topLeftGroup
            // 
            this.topLeftGroup.Controls.Add(this.topLeftColorPicker);
            this.topLeftGroup.Location = new System.Drawing.Point(6, 6);
            this.topLeftGroup.Name = "topLeftGroup";
            this.topLeftGroup.Size = new System.Drawing.Size(207, 187);
            this.topLeftGroup.TabIndex = 3;
            this.topLeftGroup.TabStop = false;
            this.topLeftGroup.Text = "Top Left";
            // 
            // topLeftColorPicker
            // 
            this.topLeftColorPicker.Location = new System.Drawing.Point(6, 19);
            this.topLeftColorPicker.Name = "topLeftColorPicker";
            this.topLeftColorPicker.ShowAlphaChannel = false;
            this.topLeftColorPicker.ShowColorSpaceLabels = false;
            this.topLeftColorPicker.Size = new System.Drawing.Size(191, 168);
            this.topLeftColorPicker.TabIndex = 12;
            this.topLeftColorPicker.ColorChanged += new System.EventHandler(this.TopLeftColorPicker_ColorChanged);
            // 
            // SetColorsButton
            // 
            this.SetColorsButton.Location = new System.Drawing.Point(251, 484);
            this.SetColorsButton.Name = "SetColorsButton";
            this.SetColorsButton.Size = new System.Drawing.Size(195, 60);
            this.SetColorsButton.TabIndex = 10;
            this.SetColorsButton.Text = "Send to Game";
            this.SetColorsButton.UseVisualStyleBackColor = true;
            this.SetColorsButton.Click += new System.EventHandler(this.SetColorsButton_Click);
            // 
            // RefreshColorsButton
            // 
            this.RefreshColorsButton.Location = new System.Drawing.Point(12, 484);
            this.RefreshColorsButton.Name = "RefreshColorsButton";
            this.RefreshColorsButton.Size = new System.Drawing.Size(191, 60);
            this.RefreshColorsButton.TabIndex = 9;
            this.RefreshColorsButton.Text = "Pull from Game";
            this.RefreshColorsButton.UseVisualStyleBackColor = true;
            this.RefreshColorsButton.Click += new System.EventHandler(this.RefreshColorsButton_Click);
            // 
            // TwitchConnectGroup
            // 
            this.TwitchConnectGroup.Controls.Add(this.TwitchDisconnectButton);
            this.TwitchConnectGroup.Controls.Add(this.TwitchConnectButton);
            this.TwitchConnectGroup.Location = new System.Drawing.Point(16, 49);
            this.TwitchConnectGroup.Name = "TwitchConnectGroup";
            this.TwitchConnectGroup.Size = new System.Drawing.Size(456, 53);
            this.TwitchConnectGroup.TabIndex = 10;
            this.TwitchConnectGroup.TabStop = false;
            this.TwitchConnectGroup.Text = "Twitch Connection";
            // 
            // TwitchDisconnectButton
            // 
            this.TwitchDisconnectButton.Location = new System.Drawing.Point(375, 20);
            this.TwitchDisconnectButton.Name = "TwitchDisconnectButton";
            this.TwitchDisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.TwitchDisconnectButton.TabIndex = 1;
            this.TwitchDisconnectButton.Text = "Disconnect";
            this.TwitchDisconnectButton.UseVisualStyleBackColor = true;
            this.TwitchDisconnectButton.Click += new System.EventHandler(this.TwitchDisconnectButton_Click);
            // 
            // TwitchConnectButton
            // 
            this.TwitchConnectButton.Location = new System.Drawing.Point(7, 20);
            this.TwitchConnectButton.Name = "TwitchConnectButton";
            this.TwitchConnectButton.Size = new System.Drawing.Size(75, 23);
            this.TwitchConnectButton.TabIndex = 0;
            this.TwitchConnectButton.Text = "Connect";
            this.TwitchConnectButton.UseVisualStyleBackColor = true;
            this.TwitchConnectButton.Click += new System.EventHandler(this.TwitchConnectButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.menuColorTab);
            this.tabControl1.Controls.Add(this.partyTab);
            this.tabControl1.Location = new System.Drawing.Point(16, 108);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 614);
            this.tabControl1.TabIndex = 11;
            // 
            // menuColorTab
            // 
            this.menuColorTab.Controls.Add(this.botRightColorSwatch);
            this.menuColorTab.Controls.Add(this.botLeftColorSwatch);
            this.menuColorTab.Controls.Add(this.topRightColorSwatch);
            this.menuColorTab.Controls.Add(this.topLeftColorSwatch);
            this.menuColorTab.Controls.Add(this.allowChatMenuControl);
            this.menuColorTab.Controls.Add(this.SetColorsButton);
            this.menuColorTab.Controls.Add(this.bottomRightGroup);
            this.menuColorTab.Controls.Add(this.RefreshColorsButton);
            this.menuColorTab.Controls.Add(this.botLeftGroup);
            this.menuColorTab.Controls.Add(this.topRightGroup);
            this.menuColorTab.Controls.Add(this.topLeftGroup);
            this.menuColorTab.Location = new System.Drawing.Point(4, 22);
            this.menuColorTab.Name = "menuColorTab";
            this.menuColorTab.Padding = new System.Windows.Forms.Padding(3);
            this.menuColorTab.Size = new System.Drawing.Size(462, 588);
            this.menuColorTab.TabIndex = 0;
            this.menuColorTab.Text = "Menu Color";
            this.menuColorTab.UseVisualStyleBackColor = true;
            // 
            // botRightColorSwatch
            // 
            this.botRightColorSwatch.BackColor = System.Drawing.Color.Black;
            this.botRightColorSwatch.Location = new System.Drawing.Point(233, 239);
            this.botRightColorSwatch.Name = "botRightColorSwatch";
            this.botRightColorSwatch.Size = new System.Drawing.Size(58, 33);
            this.botRightColorSwatch.TabIndex = 18;
            // 
            // botLeftColorSwatch
            // 
            this.botLeftColorSwatch.BackColor = System.Drawing.Color.Black;
            this.botLeftColorSwatch.Location = new System.Drawing.Point(167, 239);
            this.botLeftColorSwatch.Name = "botLeftColorSwatch";
            this.botLeftColorSwatch.Size = new System.Drawing.Size(58, 33);
            this.botLeftColorSwatch.TabIndex = 18;
            // 
            // topRightColorSwatch
            // 
            this.topRightColorSwatch.BackColor = System.Drawing.Color.Black;
            this.topRightColorSwatch.Location = new System.Drawing.Point(233, 200);
            this.topRightColorSwatch.Name = "topRightColorSwatch";
            this.topRightColorSwatch.Size = new System.Drawing.Size(58, 33);
            this.topRightColorSwatch.TabIndex = 18;
            // 
            // topLeftColorSwatch
            // 
            this.topLeftColorSwatch.BackColor = System.Drawing.Color.Black;
            this.topLeftColorSwatch.Location = new System.Drawing.Point(167, 200);
            this.topLeftColorSwatch.Name = "topLeftColorSwatch";
            this.topLeftColorSwatch.Size = new System.Drawing.Size(58, 33);
            this.topLeftColorSwatch.TabIndex = 17;
            // 
            // allowChatMenuControl
            // 
            this.allowChatMenuControl.AutoSize = true;
            this.allowChatMenuControl.Location = new System.Drawing.Point(12, 565);
            this.allowChatMenuControl.Name = "allowChatMenuControl";
            this.allowChatMenuControl.Size = new System.Drawing.Size(112, 17);
            this.allowChatMenuControl.TabIndex = 16;
            this.allowChatMenuControl.Text = "Allow Chat Control";
            this.allowChatMenuControl.UseVisualStyleBackColor = true;
            // 
            // bottomRightGroup
            // 
            this.bottomRightGroup.Controls.Add(this.botRightColorPicker);
            this.bottomRightGroup.Location = new System.Drawing.Point(245, 278);
            this.bottomRightGroup.Name = "bottomRightGroup";
            this.bottomRightGroup.Size = new System.Drawing.Size(207, 187);
            this.bottomRightGroup.TabIndex = 15;
            this.bottomRightGroup.TabStop = false;
            this.bottomRightGroup.Text = "Bottom Right";
            // 
            // botRightColorPicker
            // 
            this.botRightColorPicker.Location = new System.Drawing.Point(6, 19);
            this.botRightColorPicker.Name = "botRightColorPicker";
            this.botRightColorPicker.ShowAlphaChannel = false;
            this.botRightColorPicker.ShowColorSpaceLabels = false;
            this.botRightColorPicker.Size = new System.Drawing.Size(191, 168);
            this.botRightColorPicker.TabIndex = 12;
            this.botRightColorPicker.ColorChanged += new System.EventHandler(this.BotRightColorPicker_ColorChanged);
            // 
            // botLeftGroup
            // 
            this.botLeftGroup.Controls.Add(this.botLeftColorPicker);
            this.botLeftGroup.Location = new System.Drawing.Point(6, 278);
            this.botLeftGroup.Name = "botLeftGroup";
            this.botLeftGroup.Size = new System.Drawing.Size(207, 187);
            this.botLeftGroup.TabIndex = 14;
            this.botLeftGroup.TabStop = false;
            this.botLeftGroup.Text = "Bottom Left";
            // 
            // botLeftColorPicker
            // 
            this.botLeftColorPicker.Location = new System.Drawing.Point(6, 19);
            this.botLeftColorPicker.Name = "botLeftColorPicker";
            this.botLeftColorPicker.ShowAlphaChannel = false;
            this.botLeftColorPicker.ShowColorSpaceLabels = false;
            this.botLeftColorPicker.Size = new System.Drawing.Size(191, 168);
            this.botLeftColorPicker.TabIndex = 12;
            this.botLeftColorPicker.ColorChanged += new System.EventHandler(this.BotLeftColorPicker_ColorChanged);
            // 
            // topRightGroup
            // 
            this.topRightGroup.Controls.Add(this.topRightColorPicker);
            this.topRightGroup.Location = new System.Drawing.Point(245, 6);
            this.topRightGroup.Name = "topRightGroup";
            this.topRightGroup.Size = new System.Drawing.Size(207, 187);
            this.topRightGroup.TabIndex = 13;
            this.topRightGroup.TabStop = false;
            this.topRightGroup.Text = "Top Right";
            // 
            // topRightColorPicker
            // 
            this.topRightColorPicker.Location = new System.Drawing.Point(6, 19);
            this.topRightColorPicker.Name = "topRightColorPicker";
            this.topRightColorPicker.ShowAlphaChannel = false;
            this.topRightColorPicker.ShowColorSpaceLabels = false;
            this.topRightColorPicker.Size = new System.Drawing.Size(191, 168);
            this.topRightColorPicker.TabIndex = 12;
            this.topRightColorPicker.ColorChanged += new System.EventHandler(this.TopRightColorPicker_ColorChanged);
            // 
            // partyTab
            // 
            this.partyTab.Location = new System.Drawing.Point(4, 22);
            this.partyTab.Name = "partyTab";
            this.partyTab.Padding = new System.Windows.Forms.Padding(3);
            this.partyTab.Size = new System.Drawing.Size(462, 588);
            this.partyTab.TabIndex = 1;
            this.partyTab.Text = "Party";
            this.partyTab.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 734);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.TwitchConnectGroup);
            this.Controls.Add(this.ExeBrowse);
            this.Controls.Add(this.ExeTextBox);
            this.Controls.Add(this.ExeLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Interative Seven";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.topLeftGroup.ResumeLayout(false);
            this.TwitchConnectGroup.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.menuColorTab.ResumeLayout(false);
            this.menuColorTab.PerformLayout();
            this.bottomRightGroup.ResumeLayout(false);
            this.botLeftGroup.ResumeLayout(false);
            this.topRightGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ExeLabel;
        private System.Windows.Forms.TextBox ExeTextBox;
        private System.Windows.Forms.Button ExeBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox topLeftGroup;
        private System.Windows.Forms.Button SetColorsButton;
        private System.Windows.Forms.Button RefreshColorsButton;
        private System.Windows.Forms.GroupBox TwitchConnectGroup;
        private System.Windows.Forms.Button TwitchConnectButton;
        private System.Windows.Forms.Button TwitchDisconnectButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage menuColorTab;
        private System.Windows.Forms.TabPage partyTab;
        private Cyotek.Windows.Forms.ColorEditor topLeftColorPicker;
        private System.Windows.Forms.GroupBox topRightGroup;
        private Cyotek.Windows.Forms.ColorEditor topRightColorPicker;
        private System.Windows.Forms.GroupBox bottomRightGroup;
        private Cyotek.Windows.Forms.ColorEditor botRightColorPicker;
        private System.Windows.Forms.GroupBox botLeftGroup;
        private Cyotek.Windows.Forms.ColorEditor botLeftColorPicker;
        private System.Windows.Forms.CheckBox allowChatMenuControl;
        private System.Windows.Forms.Panel botRightColorSwatch;
        private System.Windows.Forms.Panel botLeftColorSwatch;
        private System.Windows.Forms.Panel topRightColorSwatch;
        private System.Windows.Forms.Panel topLeftColorSwatch;
    }
}

