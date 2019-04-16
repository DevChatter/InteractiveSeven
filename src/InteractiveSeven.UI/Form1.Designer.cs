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
            this.ExeLabel = new System.Windows.Forms.Label();
            this.ExeTextBox = new System.Windows.Forms.TextBox();
            this.ExeBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.topLeftGroup = new System.Windows.Forms.GroupBox();
            this.TopLeftColorButton = new System.Windows.Forms.Button();
            this.TopLeftBlueTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TopLeftGreenTextBox = new System.Windows.Forms.MaskedTextBox();
            this.TopLeftRedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.topRightGroup = new System.Windows.Forms.GroupBox();
            this.TopRightColorButton = new System.Windows.Forms.Button();
            this.TopRightBlueTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TopRightGreenTextBox = new System.Windows.Forms.MaskedTextBox();
            this.TopRightRedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.botLeftGroup = new System.Windows.Forms.GroupBox();
            this.BotLeftColorButton = new System.Windows.Forms.Button();
            this.BotLeftBlueTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BotLeftGreenTextBox = new System.Windows.Forms.MaskedTextBox();
            this.BotLeftRedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bottomRightGroup = new System.Windows.Forms.GroupBox();
            this.BotRightColorButton = new System.Windows.Forms.Button();
            this.BotRightBlueTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.BotRightGreenTextBox = new System.Windows.Forms.MaskedTextBox();
            this.BotRightRedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.colorGroup = new System.Windows.Forms.GroupBox();
            this.RefreshColorsButton = new System.Windows.Forms.Button();
            this.SetColorsButton = new System.Windows.Forms.Button();
            this.topLeftGroup.SuspendLayout();
            this.topRightGroup.SuspendLayout();
            this.botLeftGroup.SuspendLayout();
            this.bottomRightGroup.SuspendLayout();
            this.colorGroup.SuspendLayout();
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
            this.topLeftGroup.Controls.Add(this.TopLeftColorButton);
            this.topLeftGroup.Controls.Add(this.TopLeftBlueTextBox);
            this.topLeftGroup.Controls.Add(this.label3);
            this.topLeftGroup.Controls.Add(this.label2);
            this.topLeftGroup.Controls.Add(this.TopLeftGreenTextBox);
            this.topLeftGroup.Controls.Add(this.TopLeftRedTextBox);
            this.topLeftGroup.Controls.Add(this.label1);
            this.topLeftGroup.Location = new System.Drawing.Point(6, 19);
            this.topLeftGroup.Name = "topLeftGroup";
            this.topLeftGroup.Size = new System.Drawing.Size(114, 142);
            this.topLeftGroup.TabIndex = 3;
            this.topLeftGroup.TabStop = false;
            this.topLeftGroup.Text = "Top Left";
            // 
            // TopLeftColorButton
            // 
            this.TopLeftColorButton.Location = new System.Drawing.Point(6, 113);
            this.TopLeftColorButton.Name = "TopLeftColorButton";
            this.TopLeftColorButton.Size = new System.Drawing.Size(75, 23);
            this.TopLeftColorButton.TabIndex = 6;
            this.TopLeftColorButton.Tag = "";
            this.TopLeftColorButton.Text = "Pick Color";
            this.TopLeftColorButton.UseVisualStyleBackColor = true;
            this.TopLeftColorButton.Click += new System.EventHandler(this.TopLeftColorButton_Click);
            // 
            // TopLeftBlueTextBox
            // 
            this.TopLeftBlueTextBox.Location = new System.Drawing.Point(56, 81);
            this.TopLeftBlueTextBox.Mask = "000";
            this.TopLeftBlueTextBox.Name = "TopLeftBlueTextBox";
            this.TopLeftBlueTextBox.Size = new System.Drawing.Size(25, 20);
            this.TopLeftBlueTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Blue";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Green";
            // 
            // TopLeftGreenTextBox
            // 
            this.TopLeftGreenTextBox.Location = new System.Drawing.Point(56, 51);
            this.TopLeftGreenTextBox.Mask = "000";
            this.TopLeftGreenTextBox.Name = "TopLeftGreenTextBox";
            this.TopLeftGreenTextBox.Size = new System.Drawing.Size(25, 20);
            this.TopLeftGreenTextBox.TabIndex = 2;
            // 
            // TopLeftRedTextBox
            // 
            this.TopLeftRedTextBox.Location = new System.Drawing.Point(56, 20);
            this.TopLeftRedTextBox.Mask = "000";
            this.TopLeftRedTextBox.Name = "TopLeftRedTextBox";
            this.TopLeftRedTextBox.Size = new System.Drawing.Size(25, 20);
            this.TopLeftRedTextBox.TabIndex = 1;
            this.TopLeftRedTextBox.ValidatingType = typeof(int);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Red";
            // 
            // topRightGroup
            // 
            this.topRightGroup.Controls.Add(this.TopRightColorButton);
            this.topRightGroup.Controls.Add(this.TopRightBlueTextBox);
            this.topRightGroup.Controls.Add(this.label4);
            this.topRightGroup.Controls.Add(this.label5);
            this.topRightGroup.Controls.Add(this.TopRightGreenTextBox);
            this.topRightGroup.Controls.Add(this.TopRightRedTextBox);
            this.topRightGroup.Controls.Add(this.label6);
            this.topRightGroup.Location = new System.Drawing.Point(141, 19);
            this.topRightGroup.Name = "topRightGroup";
            this.topRightGroup.Size = new System.Drawing.Size(114, 142);
            this.topRightGroup.TabIndex = 7;
            this.topRightGroup.TabStop = false;
            this.topRightGroup.Text = "Top Right";
            // 
            // TopRightColorButton
            // 
            this.TopRightColorButton.Location = new System.Drawing.Point(6, 113);
            this.TopRightColorButton.Name = "TopRightColorButton";
            this.TopRightColorButton.Size = new System.Drawing.Size(75, 23);
            this.TopRightColorButton.TabIndex = 6;
            this.TopRightColorButton.Tag = "";
            this.TopRightColorButton.Text = "Pick Color";
            this.TopRightColorButton.UseVisualStyleBackColor = true;
            this.TopRightColorButton.Click += new System.EventHandler(this.TopRightColorButton_Click);
            // 
            // TopRightBlueTextBox
            // 
            this.TopRightBlueTextBox.Location = new System.Drawing.Point(56, 81);
            this.TopRightBlueTextBox.Mask = "000";
            this.TopRightBlueTextBox.Name = "TopRightBlueTextBox";
            this.TopRightBlueTextBox.Size = new System.Drawing.Size(25, 20);
            this.TopRightBlueTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Blue";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Green";
            // 
            // TopRightGreenTextBox
            // 
            this.TopRightGreenTextBox.Location = new System.Drawing.Point(56, 51);
            this.TopRightGreenTextBox.Mask = "000";
            this.TopRightGreenTextBox.Name = "TopRightGreenTextBox";
            this.TopRightGreenTextBox.Size = new System.Drawing.Size(25, 20);
            this.TopRightGreenTextBox.TabIndex = 2;
            // 
            // TopRightRedTextBox
            // 
            this.TopRightRedTextBox.Location = new System.Drawing.Point(56, 20);
            this.TopRightRedTextBox.Mask = "000";
            this.TopRightRedTextBox.Name = "TopRightRedTextBox";
            this.TopRightRedTextBox.Size = new System.Drawing.Size(25, 20);
            this.TopRightRedTextBox.TabIndex = 1;
            this.TopRightRedTextBox.ValidatingType = typeof(int);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Red";
            // 
            // botLeftGroup
            // 
            this.botLeftGroup.Controls.Add(this.BotLeftColorButton);
            this.botLeftGroup.Controls.Add(this.BotLeftBlueTextBox);
            this.botLeftGroup.Controls.Add(this.label7);
            this.botLeftGroup.Controls.Add(this.label8);
            this.botLeftGroup.Controls.Add(this.BotLeftGreenTextBox);
            this.botLeftGroup.Controls.Add(this.BotLeftRedTextBox);
            this.botLeftGroup.Controls.Add(this.label9);
            this.botLeftGroup.Location = new System.Drawing.Point(6, 167);
            this.botLeftGroup.Name = "botLeftGroup";
            this.botLeftGroup.Size = new System.Drawing.Size(114, 142);
            this.botLeftGroup.TabIndex = 8;
            this.botLeftGroup.TabStop = false;
            this.botLeftGroup.Text = "Bottom Left";
            // 
            // BotLeftColorButton
            // 
            this.BotLeftColorButton.Location = new System.Drawing.Point(6, 113);
            this.BotLeftColorButton.Name = "BotLeftColorButton";
            this.BotLeftColorButton.Size = new System.Drawing.Size(75, 23);
            this.BotLeftColorButton.TabIndex = 6;
            this.BotLeftColorButton.Tag = "";
            this.BotLeftColorButton.Text = "Pick Color";
            this.BotLeftColorButton.UseVisualStyleBackColor = true;
            this.BotLeftColorButton.Click += new System.EventHandler(this.BotLeftColorButton_Click);
            // 
            // BotLeftBlueTextBox
            // 
            this.BotLeftBlueTextBox.Location = new System.Drawing.Point(56, 81);
            this.BotLeftBlueTextBox.Mask = "000";
            this.BotLeftBlueTextBox.Name = "BotLeftBlueTextBox";
            this.BotLeftBlueTextBox.Size = new System.Drawing.Size(25, 20);
            this.BotLeftBlueTextBox.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Blue";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Green";
            // 
            // BotLeftGreenTextBox
            // 
            this.BotLeftGreenTextBox.Location = new System.Drawing.Point(56, 51);
            this.BotLeftGreenTextBox.Mask = "000";
            this.BotLeftGreenTextBox.Name = "BotLeftGreenTextBox";
            this.BotLeftGreenTextBox.Size = new System.Drawing.Size(25, 20);
            this.BotLeftGreenTextBox.TabIndex = 2;
            // 
            // BotLeftRedTextBox
            // 
            this.BotLeftRedTextBox.Location = new System.Drawing.Point(56, 20);
            this.BotLeftRedTextBox.Mask = "000";
            this.BotLeftRedTextBox.Name = "BotLeftRedTextBox";
            this.BotLeftRedTextBox.Size = new System.Drawing.Size(25, 20);
            this.BotLeftRedTextBox.TabIndex = 1;
            this.BotLeftRedTextBox.ValidatingType = typeof(int);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Red";
            // 
            // bottomRightGroup
            // 
            this.bottomRightGroup.Controls.Add(this.BotRightColorButton);
            this.bottomRightGroup.Controls.Add(this.BotRightBlueTextBox);
            this.bottomRightGroup.Controls.Add(this.label10);
            this.bottomRightGroup.Controls.Add(this.label11);
            this.bottomRightGroup.Controls.Add(this.BotRightGreenTextBox);
            this.bottomRightGroup.Controls.Add(this.BotRightRedTextBox);
            this.bottomRightGroup.Controls.Add(this.label12);
            this.bottomRightGroup.Location = new System.Drawing.Point(141, 167);
            this.bottomRightGroup.Name = "bottomRightGroup";
            this.bottomRightGroup.Size = new System.Drawing.Size(114, 142);
            this.bottomRightGroup.TabIndex = 8;
            this.bottomRightGroup.TabStop = false;
            this.bottomRightGroup.Text = "Bottom Right";
            // 
            // BotRightColorButton
            // 
            this.BotRightColorButton.Location = new System.Drawing.Point(6, 113);
            this.BotRightColorButton.Name = "BotRightColorButton";
            this.BotRightColorButton.Size = new System.Drawing.Size(75, 23);
            this.BotRightColorButton.TabIndex = 6;
            this.BotRightColorButton.Tag = "";
            this.BotRightColorButton.Text = "Pick Color";
            this.BotRightColorButton.UseVisualStyleBackColor = true;
            this.BotRightColorButton.Click += new System.EventHandler(this.BotRightColorButton_Click);
            // 
            // BotRightBlueTextBox
            // 
            this.BotRightBlueTextBox.Location = new System.Drawing.Point(56, 81);
            this.BotRightBlueTextBox.Mask = "000";
            this.BotRightBlueTextBox.Name = "BotRightBlueTextBox";
            this.BotRightBlueTextBox.Size = new System.Drawing.Size(25, 20);
            this.BotRightBlueTextBox.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Blue";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Green";
            // 
            // BotRightGreenTextBox
            // 
            this.BotRightGreenTextBox.Location = new System.Drawing.Point(56, 51);
            this.BotRightGreenTextBox.Mask = "000";
            this.BotRightGreenTextBox.Name = "BotRightGreenTextBox";
            this.BotRightGreenTextBox.Size = new System.Drawing.Size(25, 20);
            this.BotRightGreenTextBox.TabIndex = 2;
            // 
            // BotRightRedTextBox
            // 
            this.BotRightRedTextBox.Location = new System.Drawing.Point(56, 20);
            this.BotRightRedTextBox.Mask = "000";
            this.BotRightRedTextBox.Name = "BotRightRedTextBox";
            this.BotRightRedTextBox.Size = new System.Drawing.Size(25, 20);
            this.BotRightRedTextBox.TabIndex = 1;
            this.BotRightRedTextBox.ValidatingType = typeof(int);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Red";
            // 
            // colorGroup
            // 
            this.colorGroup.Controls.Add(this.SetColorsButton);
            this.colorGroup.Controls.Add(this.RefreshColorsButton);
            this.colorGroup.Controls.Add(this.topLeftGroup);
            this.colorGroup.Controls.Add(this.bottomRightGroup);
            this.colorGroup.Controls.Add(this.topRightGroup);
            this.colorGroup.Controls.Add(this.botLeftGroup);
            this.colorGroup.Location = new System.Drawing.Point(16, 56);
            this.colorGroup.Name = "colorGroup";
            this.colorGroup.Size = new System.Drawing.Size(381, 319);
            this.colorGroup.TabIndex = 9;
            this.colorGroup.TabStop = false;
            this.colorGroup.Text = "Menu Color Selection";
            // 
            // RefreshColorsButton
            // 
            this.RefreshColorsButton.Location = new System.Drawing.Point(269, 19);
            this.RefreshColorsButton.Name = "RefreshColorsButton";
            this.RefreshColorsButton.Size = new System.Drawing.Size(106, 40);
            this.RefreshColorsButton.TabIndex = 9;
            this.RefreshColorsButton.Text = "Refresh Colors";
            this.RefreshColorsButton.UseVisualStyleBackColor = true;
            this.RefreshColorsButton.Click += new System.EventHandler(this.RefreshColorsButton_Click);
            // 
            // SetColorsButton
            // 
            this.SetColorsButton.Location = new System.Drawing.Point(269, 269);
            this.SetColorsButton.Name = "SetColorsButton";
            this.SetColorsButton.Size = new System.Drawing.Size(106, 40);
            this.SetColorsButton.TabIndex = 10;
            this.SetColorsButton.Text = "Set Colors";
            this.SetColorsButton.UseVisualStyleBackColor = true;
            this.SetColorsButton.Click += new System.EventHandler(this.SetColorsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.colorGroup);
            this.Controls.Add(this.ExeBrowse);
            this.Controls.Add(this.ExeTextBox);
            this.Controls.Add(this.ExeLabel);
            this.Name = "Form1";
            this.Text = "Interative Seven";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.topLeftGroup.ResumeLayout(false);
            this.topLeftGroup.PerformLayout();
            this.topRightGroup.ResumeLayout(false);
            this.topRightGroup.PerformLayout();
            this.botLeftGroup.ResumeLayout(false);
            this.botLeftGroup.PerformLayout();
            this.bottomRightGroup.ResumeLayout(false);
            this.bottomRightGroup.PerformLayout();
            this.colorGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ExeLabel;
        private System.Windows.Forms.TextBox ExeTextBox;
        private System.Windows.Forms.Button ExeBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox topLeftGroup;
        private System.Windows.Forms.MaskedTextBox TopLeftBlueTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox TopLeftGreenTextBox;
        private System.Windows.Forms.MaskedTextBox TopLeftRedTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button TopLeftColorButton;
        private System.Windows.Forms.GroupBox topRightGroup;
        private System.Windows.Forms.Button TopRightColorButton;
        private System.Windows.Forms.MaskedTextBox TopRightBlueTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox TopRightGreenTextBox;
        private System.Windows.Forms.MaskedTextBox TopRightRedTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox botLeftGroup;
        private System.Windows.Forms.Button BotLeftColorButton;
        private System.Windows.Forms.MaskedTextBox BotLeftBlueTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox BotLeftGreenTextBox;
        private System.Windows.Forms.MaskedTextBox BotLeftRedTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox bottomRightGroup;
        private System.Windows.Forms.Button BotRightColorButton;
        private System.Windows.Forms.MaskedTextBox BotRightBlueTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox BotRightGreenTextBox;
        private System.Windows.Forms.MaskedTextBox BotRightRedTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox colorGroup;
        private System.Windows.Forms.Button SetColorsButton;
        private System.Windows.Forms.Button RefreshColorsButton;
    }
}

