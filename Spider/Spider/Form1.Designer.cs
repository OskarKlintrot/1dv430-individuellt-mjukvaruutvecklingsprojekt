namespace Spider
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
            this.On_btn = new System.Windows.Forms.Button();
            this.Off_btn = new System.Windows.Forms.Button();
            this.Start_btn = new System.Windows.Forms.Button();
            this.Stop_btn = new System.Windows.Forms.Button();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.BaudrateTextBox = new System.Windows.Forms.TextBox();
            this.DataTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PortLabel = new System.Windows.Forms.Label();
            this.BaudrateLabel = new System.Windows.Forms.Label();
            this.Reset_btn = new System.Windows.Forms.Button();
            this.ReadingProgressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // On_btn
            // 
            this.On_btn.Location = new System.Drawing.Point(244, 96);
            this.On_btn.Name = "On_btn";
            this.On_btn.Size = new System.Drawing.Size(75, 23);
            this.On_btn.TabIndex = 0;
            this.On_btn.Text = "Light on";
            this.On_btn.UseVisualStyleBackColor = true;
            this.On_btn.Click += new System.EventHandler(this.On_btn_Click);
            // 
            // Off_btn
            // 
            this.Off_btn.Location = new System.Drawing.Point(244, 125);
            this.Off_btn.Name = "Off_btn";
            this.Off_btn.Size = new System.Drawing.Size(75, 23);
            this.Off_btn.TabIndex = 0;
            this.Off_btn.Text = "Light off";
            this.Off_btn.UseVisualStyleBackColor = true;
            this.Off_btn.Click += new System.EventHandler(this.Off_btn_Click);
            // 
            // Start_btn
            // 
            this.Start_btn.Location = new System.Drawing.Point(244, 20);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(75, 23);
            this.Start_btn.TabIndex = 1;
            this.Start_btn.Text = "Start";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // Stop_btn
            // 
            this.Stop_btn.Location = new System.Drawing.Point(244, 49);
            this.Stop_btn.Name = "Stop_btn";
            this.Stop_btn.Size = new System.Drawing.Size(75, 23);
            this.Stop_btn.TabIndex = 1;
            this.Stop_btn.Text = "Stop";
            this.Stop_btn.UseVisualStyleBackColor = true;
            this.Stop_btn.Click += new System.EventHandler(this.Stop_btn_Click);
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(244, 202);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(72, 20);
            this.PortTextBox.TabIndex = 2;
            this.PortTextBox.Text = "COM4";
            // 
            // BaudrateTextBox
            // 
            this.BaudrateTextBox.Location = new System.Drawing.Point(244, 238);
            this.BaudrateTextBox.Name = "BaudrateTextBox";
            this.BaudrateTextBox.Size = new System.Drawing.Size(75, 20);
            this.BaudrateTextBox.TabIndex = 2;
            this.BaudrateTextBox.Text = "9600";
            // 
            // DataTextBox
            // 
            this.DataTextBox.Location = new System.Drawing.Point(23, 36);
            this.DataTextBox.Multiline = true;
            this.DataTextBox.Name = "DataTextBox";
            this.DataTextBox.Size = new System.Drawing.Size(194, 219);
            this.DataTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Baudrate:";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(20, 20);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(26, 13);
            this.PortLabel.TabIndex = 4;
            this.PortLabel.Text = "Port";
            // 
            // BaudrateLabel
            // 
            this.BaudrateLabel.AutoSize = true;
            this.BaudrateLabel.Location = new System.Drawing.Point(167, 20);
            this.BaudrateLabel.Name = "BaudrateLabel";
            this.BaudrateLabel.Size = new System.Drawing.Size(50, 13);
            this.BaudrateLabel.TabIndex = 4;
            this.BaudrateLabel.Text = "Baudrate";
            this.BaudrateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Reset_btn
            // 
            this.Reset_btn.Location = new System.Drawing.Point(244, 265);
            this.Reset_btn.Name = "Reset_btn";
            this.Reset_btn.Size = new System.Drawing.Size(75, 23);
            this.Reset_btn.TabIndex = 1;
            this.Reset_btn.Text = "Reset";
            this.Reset_btn.UseVisualStyleBackColor = true;
            this.Reset_btn.Click += new System.EventHandler(this.Reset_btn_Click);
            // 
            // ReadingProgressBar
            // 
            this.ReadingProgressBar.Location = new System.Drawing.Point(23, 265);
            this.ReadingProgressBar.Maximum = 30;
            this.ReadingProgressBar.Name = "ReadingProgressBar";
            this.ReadingProgressBar.Size = new System.Drawing.Size(194, 23);
            this.ReadingProgressBar.Step = 1;
            this.ReadingProgressBar.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "30";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 317);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReadingProgressBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BaudrateLabel);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataTextBox);
            this.Controls.Add(this.BaudrateTextBox);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.Reset_btn);
            this.Controls.Add(this.Stop_btn);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.Off_btn);
            this.Controls.Add(this.On_btn);
            this.Name = "Form1";
            this.Text = "SerialDBSpider";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button On_btn;
        private System.Windows.Forms.Button Off_btn;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Button Stop_btn;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.TextBox BaudrateTextBox;
        private System.Windows.Forms.TextBox DataTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label BaudrateLabel;
        private System.Windows.Forms.Button Reset_btn;
        private System.Windows.Forms.ProgressBar ReadingProgressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

