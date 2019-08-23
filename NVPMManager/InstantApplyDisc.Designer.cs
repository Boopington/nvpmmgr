namespace NVPMManager
{
    partial class InstantApplyDisc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstantApplyDisc));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSystemRestore = new System.Windows.Forms.Button();
            this.labelDis = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(28, 194);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(79, 33);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(113, 194);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(79, 33);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSystemRestore
            // 
            this.buttonSystemRestore.Location = new System.Drawing.Point(280, 194);
            this.buttonSystemRestore.Name = "buttonSystemRestore";
            this.buttonSystemRestore.Size = new System.Drawing.Size(155, 33);
            this.buttonSystemRestore.TabIndex = 2;
            this.buttonSystemRestore.Text = "Create System Restore Point";
            this.buttonSystemRestore.UseVisualStyleBackColor = true;
            this.buttonSystemRestore.Click += new System.EventHandler(this.buttonSystemRestore_Click);
            // 
            // labelDis
            // 
            this.labelDis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDis.Location = new System.Drawing.Point(14, 9);
            this.labelDis.Name = "labelDis";
            this.labelDis.Size = new System.Drawing.Size(417, 167);
            this.labelDis.TabIndex = 3;
            this.labelDis.Text = "disclaimer";
            this.labelDis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InstantApplyDisc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 239);
            this.Controls.Add(this.labelDis);
            this.Controls.Add(this.buttonSystemRestore);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstantApplyDisc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instant Apply! ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSystemRestore;
        private System.Windows.Forms.Label labelDis;
    }
}