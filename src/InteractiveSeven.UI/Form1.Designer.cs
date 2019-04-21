using InteractiveSeven.UI.UserControls;

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
            this.label1 = new System.Windows.Forms.Label();
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.partyMemberStats1 = new InteractiveSeven.UI.UserControls.PartyMemberStats();
            this.partyMemberStats2 = new InteractiveSeven.UI.UserControls.PartyMemberStats();
            this.partyMemberStats3 = new InteractiveSeven.UI.UserControls.PartyMemberStats();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dateStatus1 = new InteractiveSeven.UI.UserControls.DateStatus();
            this.dateStatus2 = new InteractiveSeven.UI.UserControls.DateStatus();
            this.YuffieDateStatus = new InteractiveSeven.UI.UserControls.DateStatus();
            this.dateStatus4 = new InteractiveSeven.UI.UserControls.DateStatus();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.topLeftGroup.SuspendLayout();
            this.TwitchConnectGroup.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.menuColorTab.SuspendLayout();
            this.bottomRightGroup.SuspendLayout();
            this.botLeftGroup.SuspendLayout();
            this.topRightGroup.SuspendLayout();
            this.partyTab.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExeLabel
            // 
            this.ExeLabel.AutoSize = true;
            this.ExeLabel.Location = new System.Drawing.Point(6, 26);
            this.ExeLabel.Name = "ExeLabel";
            this.ExeLabel.Size = new System.Drawing.Size(76, 13);
            this.ExeLabel.TabIndex = 0;
            this.ExeLabel.Text = "Process Name";
            // 
            // ExeTextBox
            // 
            this.ExeTextBox.Location = new System.Drawing.Point(88, 23);
            this.ExeTextBox.Name = "ExeTextBox";
            this.ExeTextBox.Size = new System.Drawing.Size(98, 20);
            this.ExeTextBox.TabIndex = 1;
            this.ExeTextBox.Text = "ff7_en";
            // 
            // ExeBrowse
            // 
            this.ExeBrowse.Location = new System.Drawing.Point(192, 21);
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
            this.TwitchConnectGroup.Controls.Add(this.label1);
            this.TwitchConnectGroup.Controls.Add(this.TwitchDisconnectButton);
            this.TwitchConnectGroup.Controls.Add(this.TwitchConnectButton);
            this.TwitchConnectGroup.Location = new System.Drawing.Point(337, 13);
            this.TwitchConnectGroup.Name = "TwitchConnectGroup";
            this.TwitchConnectGroup.Size = new System.Drawing.Size(283, 53);
            this.TwitchConnectGroup.TabIndex = 10;
            this.TwitchConnectGroup.TabStop = false;
            this.TwitchConnectGroup.Text = "Twitch Connection";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightGray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(95, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Disconnected";
            // 
            // TwitchDisconnectButton
            // 
            this.TwitchDisconnectButton.Location = new System.Drawing.Point(201, 19);
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
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(13, 72);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(608, 590);
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
            this.menuColorTab.Size = new System.Drawing.Size(600, 564);
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
            this.allowChatMenuControl.Checked = true;
            this.allowChatMenuControl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allowChatMenuControl.Location = new System.Drawing.Point(12, 565);
            this.allowChatMenuControl.Name = "allowChatMenuControl";
            this.allowChatMenuControl.Size = new System.Drawing.Size(112, 17);
            this.allowChatMenuControl.TabIndex = 16;
            this.allowChatMenuControl.Text = "Allow Chat Control";
            this.allowChatMenuControl.UseVisualStyleBackColor = true;
            this.allowChatMenuControl.CheckedChanged += new System.EventHandler(this.AllowChatMenuControl_CheckedChanged);
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
            this.partyTab.AutoScroll = true;
            this.partyTab.Controls.Add(this.flowLayoutPanel1);
            this.partyTab.Controls.Add(this.groupBox1);
            this.partyTab.Location = new System.Drawing.Point(4, 22);
            this.partyTab.Name = "partyTab";
            this.partyTab.Padding = new System.Windows.Forms.Padding(3);
            this.partyTab.Size = new System.Drawing.Size(600, 564);
            this.partyTab.TabIndex = 1;
            this.partyTab.Text = "Party";
            this.partyTab.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.partyMemberStats1);
            this.flowLayoutPanel1.Controls.Add(this.partyMemberStats2);
            this.flowLayoutPanel1.Controls.Add(this.partyMemberStats3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(7, 198);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(570, 446);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // partyMemberStats1
            // 
            this.partyMemberStats1.CharacterName = "Cloud";
            this.partyMemberStats1.Location = new System.Drawing.Point(3, 3);
            this.partyMemberStats1.Name = "partyMemberStats1";
            this.partyMemberStats1.Size = new System.Drawing.Size(278, 179);
            this.partyMemberStats1.TabIndex = 0;
            // 
            // partyMemberStats2
            // 
            this.partyMemberStats2.CharacterName = "Barret";
            this.partyMemberStats2.Location = new System.Drawing.Point(287, 3);
            this.partyMemberStats2.Name = "partyMemberStats2";
            this.partyMemberStats2.Size = new System.Drawing.Size(278, 179);
            this.partyMemberStats2.TabIndex = 1;
            // 
            // partyMemberStats3
            // 
            this.partyMemberStats3.CharacterName = "Tifa";
            this.partyMemberStats3.Location = new System.Drawing.Point(3, 188);
            this.partyMemberStats3.Name = "partyMemberStats3";
            this.partyMemberStats3.Size = new System.Drawing.Size(278, 179);
            this.partyMemberStats3.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Commands";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.textBox10, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBox9, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox8, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox7, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label10, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBox3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBox4, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBox5, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox4, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox5, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBox6, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(557, 159);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(478, 131);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(56, 20);
            this.textBox10.TabIndex = 28;
            this.textBox10.Text = "0";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(478, 105);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(56, 20);
            this.textBox9.TabIndex = 27;
            this.textBox9.Text = "0";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(478, 79);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(56, 20);
            this.textBox8.TabIndex = 26;
            this.textBox8.Text = "0";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(478, 53);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(56, 20);
            this.textBox7.TabIndex = 25;
            this.textBox7.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(273, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Command";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(408, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Enabled";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Allow Max HP Changes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Allow Current HP Changes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Allow Stat Increases";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Allow Stat Decreases";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Allow Status Infliction";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(478, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 15);
            this.label10.TabIndex = 8;
            this.label10.Text = "Bit Cost";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(408, 27);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(408, 53);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(408, 79);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 16;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(408, 105);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 17;
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(408, 131);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(15, 14);
            this.checkBox5.TabIndex = 18;
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(273, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 19;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(273, 53);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 20;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(273, 79);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 21;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(273, 105);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 22;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(273, 131);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 23;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(478, 27);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(56, 20);
            this.textBox6.TabIndex = 24;
            this.textBox6.Text = "0";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flowLayoutPanel2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(600, 564);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Date";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.dateStatus1);
            this.flowLayoutPanel2.Controls.Add(this.dateStatus2);
            this.flowLayoutPanel2.Controls.Add(this.YuffieDateStatus);
            this.flowLayoutPanel2.Controls.Add(this.dateStatus4);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(5, 194);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(591, 367);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // dateStatus1
            // 
            this.dateStatus1.CharacterName = "Aeris";
            this.dateStatus1.Location = new System.Drawing.Point(3, 3);
            this.dateStatus1.Name = "dateStatus1";
            this.dateStatus1.Size = new System.Drawing.Size(240, 120);
            this.dateStatus1.TabIndex = 0;
            // 
            // dateStatus2
            // 
            this.dateStatus2.CharacterName = "Tifa";
            this.dateStatus2.Location = new System.Drawing.Point(249, 3);
            this.dateStatus2.Name = "dateStatus2";
            this.dateStatus2.Size = new System.Drawing.Size(240, 120);
            this.dateStatus2.TabIndex = 1;
            // 
            // YuffieDateStatus
            // 
            this.YuffieDateStatus.CharacterName = "Yuffie";
            this.YuffieDateStatus.Location = new System.Drawing.Point(3, 129);
            this.YuffieDateStatus.Name = "YuffieDateStatus";
            this.YuffieDateStatus.Size = new System.Drawing.Size(240, 120);
            this.YuffieDateStatus.TabIndex = 2;
            // 
            // dateStatus4
            // 
            this.dateStatus4.CharacterName = "Barret";
            this.dateStatus4.Location = new System.Drawing.Point(249, 129);
            this.dateStatus4.Name = "dateStatus4";
            this.dateStatus4.Size = new System.Drawing.Size(240, 120);
            this.dateStatus4.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ExeLabel);
            this.groupBox2.Controls.Add(this.ExeTextBox);
            this.groupBox2.Controls.Add(this.ExeBrowse);
            this.groupBox2.Location = new System.Drawing.Point(13, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 53);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FF7 Connection";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label12, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label19, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkBox6, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox15, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox20, 3, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(578, 159);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(499, 27);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(56, 20);
            this.textBox20.TabIndex = 24;
            this.textBox20.Text = "0";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(287, 27);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(100, 20);
            this.textBox15.TabIndex = 19;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(429, 27);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(15, 14);
            this.checkBox6.TabIndex = 14;
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(499, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 15);
            this.label19.TabIndex = 8;
            this.label19.Text = "Bit Cost";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Vote for Date";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(429, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 15);
            this.label13.TabIndex = 2;
            this.label13.Text = "Enabled";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(287, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 15);
            this.label12.TabIndex = 1;
            this.label12.Text = "Command";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "Description";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Location = new System.Drawing.Point(5, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(591, 185);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Commands";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 674);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.TwitchConnectGroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Interative Seven";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.topLeftGroup.ResumeLayout(false);
            this.TwitchConnectGroup.ResumeLayout(false);
            this.TwitchConnectGroup.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.menuColorTab.ResumeLayout(false);
            this.menuColorTab.PerformLayout();
            this.bottomRightGroup.ResumeLayout(false);
            this.botLeftGroup.ResumeLayout(false);
            this.topRightGroup.ResumeLayout(false);
            this.partyTab.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private PartyMemberStats partyMemberStats1;
        private PartyMemberStats partyMemberStats2;
        private PartyMemberStats partyMemberStats3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private DateStatus dateStatus1;
        private DateStatus dateStatus2;
        private DateStatus YuffieDateStatus;
        private DateStatus dateStatus4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox20;
    }
}

