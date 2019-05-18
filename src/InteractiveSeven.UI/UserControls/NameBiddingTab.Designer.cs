namespace InteractiveSeven.UI.UserControls
{
    partial class NameBiddingTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nameBiddingSettings = new System.Windows.Forms.CheckedListBox();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoScroll = true;
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 100);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(600, 510);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nameBiddingSettings);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(600, 100);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Settings";
            // 
            // nameBiddingSettings
            // 
            this.nameBiddingSettings.CheckOnClick = true;
            this.nameBiddingSettings.ColumnWidth = 175;
            this.nameBiddingSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameBiddingSettings.FormattingEnabled = true;
            this.nameBiddingSettings.Items.AddRange(new object[] {
            "Enable Feature",
            "Limit Name Choices",
            "Allow Cloud Naming",
            "Allow Barret Naming",
            "Allow Tifa Naming",
            "Allow Aeris Naming",
            "Allow Cait Sith Naming",
            "Allow Cid Naming",
            "Allow Red XIII Naming",
            "Allow Vincent Naming",
            "Allow Yuffie Naming"});
            this.nameBiddingSettings.Location = new System.Drawing.Point(3, 16);
            this.nameBiddingSettings.MultiColumn = true;
            this.nameBiddingSettings.Name = "nameBiddingSettings";
            this.nameBiddingSettings.Size = new System.Drawing.Size(594, 81);
            this.nameBiddingSettings.TabIndex = 0;
            // 
            // NameBiddingTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.groupBox4);
            this.Name = "NameBiddingTab";
            this.Size = new System.Drawing.Size(600, 610);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox nameBiddingSettings;
    }
}
