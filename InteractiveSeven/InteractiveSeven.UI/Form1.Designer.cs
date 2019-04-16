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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TopLeftColorButton = new System.Windows.Forms.Button();
            this.TopLeftBlueTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TopLeftGreenTextBox = new System.Windows.Forms.MaskedTextBox();
            this.TopLeftRedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TopLeftColorButton);
            this.groupBox1.Controls.Add(this.TopLeftBlueTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TopLeftGreenTextBox);
            this.groupBox1.Controls.Add(this.TopLeftRedTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 142);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Top Left";
            // 
            // TopLeftColorButton
            // 
            this.TopLeftColorButton.Location = new System.Drawing.Point(6, 113);
            this.TopLeftColorButton.Name = "TopLeftColorButton";
            this.TopLeftColorButton.Size = new System.Drawing.Size(75, 23);
            this.TopLeftColorButton.TabIndex = 6;
            this.TopLeftColorButton.Tag = "TopLeftColorButton";
            this.TopLeftColorButton.Text = "Pick Color";
            this.TopLeftColorButton.UseVisualStyleBackColor = true;
            this.TopLeftColorButton.Click += new System.EventHandler(this.TopLeftColorButton_Click);
            // 
            // TopLeftBlueTextBox
            // 
            this.TopLeftBlueTextBox.Location = new System.Drawing.Point(78, 81);
            this.TopLeftBlueTextBox.Mask = "000";
            this.TopLeftBlueTextBox.Name = "TopLeftBlueTextBox";
            this.TopLeftBlueTextBox.Size = new System.Drawing.Size(32, 20);
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
            this.TopLeftGreenTextBox.Location = new System.Drawing.Point(78, 51);
            this.TopLeftGreenTextBox.Mask = "000";
            this.TopLeftGreenTextBox.Name = "TopLeftGreenTextBox";
            this.TopLeftGreenTextBox.Size = new System.Drawing.Size(32, 20);
            this.TopLeftGreenTextBox.TabIndex = 2;
            // 
            // TopLeftRedTextBox
            // 
            this.TopLeftRedTextBox.Location = new System.Drawing.Point(78, 20);
            this.TopLeftRedTextBox.Mask = "000";
            this.TopLeftRedTextBox.Name = "TopLeftRedTextBox";
            this.TopLeftRedTextBox.Size = new System.Drawing.Size(32, 20);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ExeBrowse);
            this.Controls.Add(this.ExeTextBox);
            this.Controls.Add(this.ExeLabel);
            this.Name = "Form1";
            this.Text = "Interative Seven";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ExeLabel;
        private System.Windows.Forms.TextBox ExeTextBox;
        private System.Windows.Forms.Button ExeBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox TopLeftBlueTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox TopLeftGreenTextBox;
        private System.Windows.Forms.MaskedTextBox TopLeftRedTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button TopLeftColorButton;
    }
}

