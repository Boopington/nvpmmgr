namespace NVPMManager
{
    partial class Support
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Support));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelSMB = new System.Windows.Forms.LinkLabel();
            this.labelText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCollect = new System.Windows.Forms.Button();
            this.buttonWarn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "NVidia PowerMizer Manager 1.01";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(173, 289);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "For FAQ, updates and support visit:";
            // 
            // linkLabelSMB
            // 
            this.linkLabelSMB.AutoSize = true;
            this.linkLabelSMB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelSMB.Location = new System.Drawing.Point(358, 287);
            this.linkLabelSMB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabelSMB.Name = "linkLabelSMB";
            this.linkLabelSMB.Size = new System.Drawing.Size(175, 15);
            this.linkLabelSMB.TabIndex = 2;
            this.linkLabelSMB.TabStop = true;
            this.linkLabelSMB.Text = "http://somemorebytes.com";
            this.linkLabelSMB.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSMB_LinkClicked);
            // 
            // labelText
            // 
            this.labelText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelText.Location = new System.Drawing.Point(24, 58);
            this.labelText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(673, 205);
            this.labelText.TabIndex = 3;
            this.labelText.Text = "disclaimer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 321);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Or send an email to:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(198, 321);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(138, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "somemorebytes@gmail.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(342, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "with the attachment  -->";
            // 
            // buttonCollect
            // 
            this.buttonCollect.Location = new System.Drawing.Point(469, 316);
            this.buttonCollect.Name = "buttonCollect";
            this.buttonCollect.Size = new System.Drawing.Size(165, 23);
            this.buttonCollect.TabIndex = 7;
            this.buttonCollect.Text = "Collect Debug Info";
            this.buttonCollect.UseVisualStyleBackColor = true;
            this.buttonCollect.Click += new System.EventHandler(this.buttonCollect_Click);
            // 
            // buttonWarn
            // 
            this.buttonWarn.Location = new System.Drawing.Point(588, 266);
            this.buttonWarn.Name = "buttonWarn";
            this.buttonWarn.Size = new System.Drawing.Size(109, 23);
            this.buttonWarn.TabIndex = 8;
            this.buttonWarn.Text = "Reset Warnings";
            this.buttonWarn.UseVisualStyleBackColor = true;
            this.buttonWarn.Click += new System.EventHandler(this.buttonWarn_Click);
            // 
            // Support
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 354);
            this.Controls.Add(this.buttonWarn);
            this.Controls.Add(this.buttonCollect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.linkLabelSMB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Support";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Support / About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelSMB;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCollect;
        private System.Windows.Forms.Button buttonWarn;
    }
}