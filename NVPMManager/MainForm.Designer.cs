namespace NVPMManager
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBoxBattery = new System.Windows.Forms.GroupBox();
            this.comboBoxLevelBatt = new System.Windows.Forms.ComboBox();
            this.radioButtonFixedBatt = new System.Windows.Forms.RadioButton();
            this.radioButtonATBAtt = new System.Windows.Forms.RadioButton();
            this.groupBoxAC = new System.Windows.Forms.GroupBox();
            this.comboBoxLevelAC = new System.Windows.Forms.ComboBox();
            this.radioButtonFixedAC = new System.Windows.Forms.RadioButton();
            this.radioButtonATAC = new System.Windows.Forms.RadioButton();
            this.buttonBackup = new System.Windows.Forms.Button();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.groupBoxCheck = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.groupBoxSelect = new System.Windows.Forms.GroupBox();
            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this.comboBoxSlowDown = new System.Windows.Forms.ComboBox();
            this.checkBoxEnableSlowDown = new System.Windows.Forms.CheckBox();
            this.checkBoxEnablePM = new System.Windows.Forms.CheckBox();
            this.groupBoxApply = new System.Windows.Forms.GroupBox();
            this.buttonReboot = new System.Windows.Forms.Button();
            this.buttonApplyNow = new System.Windows.Forms.Button();
            this.textBoxDebug = new System.Windows.Forms.TextBox();
            this.buttonExpand = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReport = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelSLI = new System.Windows.Forms.Label();
            this.groupBoxBattery.SuspendLayout();
            this.groupBoxAC.SuspendLayout();
            this.groupBoxCheck.SuspendLayout();
            this.groupBoxSelect.SuspendLayout();
            this.groupBoxOther.SuspendLayout();
            this.groupBoxApply.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxBattery
            // 
            this.groupBoxBattery.Controls.Add(this.comboBoxLevelBatt);
            this.groupBoxBattery.Controls.Add(this.radioButtonFixedBatt);
            this.groupBoxBattery.Controls.Add(this.radioButtonATBAtt);
            this.groupBoxBattery.Location = new System.Drawing.Point(13, 49);
            this.groupBoxBattery.Name = "groupBoxBattery";
            this.groupBoxBattery.Size = new System.Drawing.Size(352, 86);
            this.groupBoxBattery.TabIndex = 1;
            this.groupBoxBattery.TabStop = false;
            this.groupBoxBattery.Text = "PowerMizer On Battery is:";
            // 
            // comboBoxLevelBatt
            // 
            this.comboBoxLevelBatt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelBatt.FormattingEnabled = true;
            this.comboBoxLevelBatt.Location = new System.Drawing.Point(164, 56);
            this.comboBoxLevelBatt.Name = "comboBoxLevelBatt";
            this.comboBoxLevelBatt.Size = new System.Drawing.Size(174, 21);
            this.comboBoxLevelBatt.TabIndex = 2;
            // 
            // radioButtonFixedBatt
            // 
            this.radioButtonFixedBatt.AutoSize = true;
            this.radioButtonFixedBatt.Location = new System.Drawing.Point(19, 60);
            this.radioButtonFixedBatt.Name = "radioButtonFixedBatt";
            this.radioButtonFixedBatt.Size = new System.Drawing.Size(139, 17);
            this.radioButtonFixedBatt.TabIndex = 1;
            this.radioButtonFixedBatt.TabStop = true;
            this.radioButtonFixedBatt.Text = "Fixed Performace Level:";
            this.radioButtonFixedBatt.UseVisualStyleBackColor = true;
            this.radioButtonFixedBatt.Click += new System.EventHandler(this.radioButtonFixedBatt_Click);
            // 
            // radioButtonATBAtt
            // 
            this.radioButtonATBAtt.AutoSize = true;
            this.radioButtonATBAtt.Location = new System.Drawing.Point(19, 29);
            this.radioButtonATBAtt.Name = "radioButtonATBAtt";
            this.radioButtonATBAtt.Size = new System.Drawing.Size(198, 17);
            this.radioButtonATBAtt.TabIndex = 0;
            this.radioButtonATBAtt.TabStop = true;
            this.radioButtonATBAtt.Text = "Auto Throttle. Adaptive clock speed.";
            this.radioButtonATBAtt.UseVisualStyleBackColor = true;
            this.radioButtonATBAtt.Click += new System.EventHandler(this.radioButtonATBAtt_Click);
            // 
            // groupBoxAC
            // 
            this.groupBoxAC.Controls.Add(this.comboBoxLevelAC);
            this.groupBoxAC.Controls.Add(this.radioButtonFixedAC);
            this.groupBoxAC.Controls.Add(this.radioButtonATAC);
            this.groupBoxAC.Location = new System.Drawing.Point(13, 141);
            this.groupBoxAC.Name = "groupBoxAC";
            this.groupBoxAC.Size = new System.Drawing.Size(352, 86);
            this.groupBoxAC.TabIndex = 2;
            this.groupBoxAC.TabStop = false;
            this.groupBoxAC.Text = "PowerMizer on AC adapter is:";
            // 
            // comboBoxLevelAC
            // 
            this.comboBoxLevelAC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelAC.FormattingEnabled = true;
            this.comboBoxLevelAC.Location = new System.Drawing.Point(164, 56);
            this.comboBoxLevelAC.Name = "comboBoxLevelAC";
            this.comboBoxLevelAC.Size = new System.Drawing.Size(174, 21);
            this.comboBoxLevelAC.TabIndex = 2;
            // 
            // radioButtonFixedAC
            // 
            this.radioButtonFixedAC.AutoSize = true;
            this.radioButtonFixedAC.Location = new System.Drawing.Point(19, 60);
            this.radioButtonFixedAC.Name = "radioButtonFixedAC";
            this.radioButtonFixedAC.Size = new System.Drawing.Size(145, 17);
            this.radioButtonFixedAC.TabIndex = 1;
            this.radioButtonFixedAC.TabStop = true;
            this.radioButtonFixedAC.Text = "Fixed Performance Level:";
            this.radioButtonFixedAC.UseVisualStyleBackColor = true;
            this.radioButtonFixedAC.Click += new System.EventHandler(this.radioButtonFixedAC_Click);
            // 
            // radioButtonATAC
            // 
            this.radioButtonATAC.AutoSize = true;
            this.radioButtonATAC.Location = new System.Drawing.Point(19, 29);
            this.radioButtonATAC.Name = "radioButtonATAC";
            this.radioButtonATAC.Size = new System.Drawing.Size(198, 17);
            this.radioButtonATAC.TabIndex = 0;
            this.radioButtonATAC.TabStop = true;
            this.radioButtonATAC.Text = "Auto Throttle. Adaptive clock speed.";
            this.radioButtonATAC.UseVisualStyleBackColor = true;
            this.radioButtonATAC.Click += new System.EventHandler(this.radioButtonATAC_Click);
            // 
            // buttonBackup
            // 
            this.buttonBackup.Location = new System.Drawing.Point(32, 49);
            this.buttonBackup.Name = "buttonBackup";
            this.buttonBackup.Size = new System.Drawing.Size(153, 22);
            this.buttonBackup.TabIndex = 5;
            this.buttonBackup.Text = "Backup Settings";
            this.buttonBackup.UseVisualStyleBackColor = true;
            this.buttonBackup.Click += new System.EventHandler(this.buttonBackup_Click);
            // 
            // buttonRestore
            // 
            this.buttonRestore.Location = new System.Drawing.Point(191, 49);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(153, 22);
            this.buttonRestore.TabIndex = 6;
            this.buttonRestore.Text = "Import Settings";
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // groupBoxCheck
            // 
            this.groupBoxCheck.Controls.Add(this.buttonDelete);
            this.groupBoxCheck.Controls.Add(this.buttonInsert);
            this.groupBoxCheck.Controls.Add(this.buttonBackup);
            this.groupBoxCheck.Controls.Add(this.buttonRestore);
            this.groupBoxCheck.Location = new System.Drawing.Point(58, 13);
            this.groupBoxCheck.Name = "groupBoxCheck";
            this.groupBoxCheck.Size = new System.Drawing.Size(381, 86);
            this.groupBoxCheck.TabIndex = 7;
            this.groupBoxCheck.TabStop = false;
            this.groupBoxCheck.Text = "Check Registry";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(191, 21);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(153, 22);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete PowerMizer Settings";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(32, 21);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(153, 22);
            this.buttonInsert.TabIndex = 1;
            this.buttonInsert.Text = "Create PowerMizer Settings";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // groupBoxSelect
            // 
            this.groupBoxSelect.Controls.Add(this.labelSLI);
            this.groupBoxSelect.Controls.Add(this.groupBoxOther);
            this.groupBoxSelect.Controls.Add(this.checkBoxEnablePM);
            this.groupBoxSelect.Controls.Add(this.groupBoxAC);
            this.groupBoxSelect.Controls.Add(this.groupBoxBattery);
            this.groupBoxSelect.Location = new System.Drawing.Point(58, 104);
            this.groupBoxSelect.Name = "groupBoxSelect";
            this.groupBoxSelect.Size = new System.Drawing.Size(381, 311);
            this.groupBoxSelect.TabIndex = 9;
            this.groupBoxSelect.TabStop = false;
            this.groupBoxSelect.Text = "Configure PowerMizer";
            // 
            // groupBoxOther
            // 
            this.groupBoxOther.Controls.Add(this.comboBoxSlowDown);
            this.groupBoxOther.Controls.Add(this.checkBoxEnableSlowDown);
            this.groupBoxOther.Location = new System.Drawing.Point(13, 233);
            this.groupBoxOther.Name = "groupBoxOther";
            this.groupBoxOther.Size = new System.Drawing.Size(352, 64);
            this.groupBoxOther.TabIndex = 17;
            this.groupBoxOther.TabStop = false;
            this.groupBoxOther.Text = "Configure other features";
            // 
            // comboBoxSlowDown
            // 
            this.comboBoxSlowDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSlowDown.FormattingEnabled = true;
            this.comboBoxSlowDown.Location = new System.Drawing.Point(168, 24);
            this.comboBoxSlowDown.Name = "comboBoxSlowDown";
            this.comboBoxSlowDown.Size = new System.Drawing.Size(178, 21);
            this.comboBoxSlowDown.TabIndex = 1;
            // 
            // checkBoxEnableSlowDown
            // 
            this.checkBoxEnableSlowDown.AutoSize = true;
            this.checkBoxEnableSlowDown.Location = new System.Drawing.Point(6, 28);
            this.checkBoxEnableSlowDown.Name = "checkBoxEnableSlowDown";
            this.checkBoxEnableSlowDown.Size = new System.Drawing.Size(165, 17);
            this.checkBoxEnableSlowDown.TabIndex = 0;
            this.checkBoxEnableSlowDown.Text = "Overheat Slowdown Override";
            this.checkBoxEnableSlowDown.UseVisualStyleBackColor = true;
            this.checkBoxEnableSlowDown.CheckedChanged += new System.EventHandler(this.checkBoxEnableSlowDown_CheckedChanged);
            // 
            // checkBoxEnablePM
            // 
            this.checkBoxEnablePM.AutoSize = true;
            this.checkBoxEnablePM.Location = new System.Drawing.Point(19, 24);
            this.checkBoxEnablePM.Name = "checkBoxEnablePM";
            this.checkBoxEnablePM.Size = new System.Drawing.Size(156, 17);
            this.checkBoxEnablePM.TabIndex = 3;
            this.checkBoxEnablePM.Text = "Enable PowerMizer Feature";
            this.checkBoxEnablePM.UseVisualStyleBackColor = true;
            this.checkBoxEnablePM.CheckedChanged += new System.EventHandler(this.checkBoxEnablePM_CheckedChanged);
            // 
            // groupBoxApply
            // 
            this.groupBoxApply.Controls.Add(this.buttonReboot);
            this.groupBoxApply.Controls.Add(this.buttonApplyNow);
            this.groupBoxApply.Location = new System.Drawing.Point(58, 421);
            this.groupBoxApply.Name = "groupBoxApply";
            this.groupBoxApply.Size = new System.Drawing.Size(381, 68);
            this.groupBoxApply.TabIndex = 11;
            this.groupBoxApply.TabStop = false;
            this.groupBoxApply.Text = "Apply Settings";
            // 
            // buttonReboot
            // 
            this.buttonReboot.Location = new System.Drawing.Point(191, 34);
            this.buttonReboot.Name = "buttonReboot";
            this.buttonReboot.Size = new System.Drawing.Size(153, 22);
            this.buttonReboot.TabIndex = 1;
            this.buttonReboot.Text = "Apply && Reboot";
            this.buttonReboot.UseVisualStyleBackColor = true;
            this.buttonReboot.Click += new System.EventHandler(this.buttonReboot_Click);
            // 
            // buttonApplyNow
            // 
            this.buttonApplyNow.Location = new System.Drawing.Point(32, 34);
            this.buttonApplyNow.Name = "buttonApplyNow";
            this.buttonApplyNow.Size = new System.Drawing.Size(153, 22);
            this.buttonApplyNow.TabIndex = 0;
            this.buttonApplyNow.Text = "Instant Apply! ";
            this.buttonApplyNow.UseVisualStyleBackColor = true;
            this.buttonApplyNow.Click += new System.EventHandler(this.buttonApplyNow_Click);
            // 
            // textBoxDebug
            // 
            this.textBoxDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDebug.HideSelection = false;
            this.textBoxDebug.Location = new System.Drawing.Point(469, 34);
            this.textBoxDebug.Multiline = true;
            this.textBoxDebug.Name = "textBoxDebug";
            this.textBoxDebug.ReadOnly = true;
            this.textBoxDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDebug.Size = new System.Drawing.Size(413, 405);
            this.textBoxDebug.TabIndex = 12;
            // 
            // buttonExpand
            // 
            this.buttonExpand.Location = new System.Drawing.Point(443, 215);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(17, 67);
            this.buttonExpand.TabIndex = 13;
            this.buttonExpand.Text = ">>>";
            this.buttonExpand.UseVisualStyleBackColor = true;
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(472, 455);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(153, 22);
            this.buttonCopy.TabIndex = 14;
            this.buttonCopy.Text = "Copy to Clipboard";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Debug Console:";
            // 
            // buttonReport
            // 
            this.buttonReport.Location = new System.Drawing.Point(812, 455);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(64, 22);
            this.buttonReport.TabIndex = 16;
            this.buttonReport.Text = "Problems?";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::NVPMManager.Properties.Resources._3;
            this.pictureBox4.Location = new System.Drawing.Point(12, 421);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(43, 43);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::NVPMManager.Properties.Resources._2;
            this.pictureBox3.Location = new System.Drawing.Point(12, 104);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(43, 43);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::NVPMManager.Properties.Resources._1;
            this.pictureBox2.Location = new System.Drawing.Point(12, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 43);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // labelSLI
            // 
            this.labelSLI.AutoSize = true;
            this.labelSLI.ForeColor = System.Drawing.Color.Red;
            this.labelSLI.Location = new System.Drawing.Point(125, 0);
            this.labelSLI.Name = "labelSLI";
            this.labelSLI.Size = new System.Drawing.Size(70, 13);
            this.labelSLI.TabIndex = 18;
            this.labelSLI.Text = "[ SLI/Hybrid ]";
            this.labelSLI.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(464, 497);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonExpand);
            this.Controls.Add(this.textBoxDebug);
            this.Controls.Add(this.groupBoxApply);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.groupBoxSelect);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBoxCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "NVidia PowerMizer Manager";
            this.groupBoxBattery.ResumeLayout(false);
            this.groupBoxBattery.PerformLayout();
            this.groupBoxAC.ResumeLayout(false);
            this.groupBoxAC.PerformLayout();
            this.groupBoxCheck.ResumeLayout(false);
            this.groupBoxSelect.ResumeLayout(false);
            this.groupBoxSelect.PerformLayout();
            this.groupBoxOther.ResumeLayout(false);
            this.groupBoxOther.PerformLayout();
            this.groupBoxApply.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBattery;
        private System.Windows.Forms.GroupBox groupBoxAC;
        private System.Windows.Forms.ComboBox comboBoxLevelBatt;
        private System.Windows.Forms.RadioButton radioButtonFixedBatt;
        private System.Windows.Forms.RadioButton radioButtonATBAtt;
        private System.Windows.Forms.ComboBox comboBoxLevelAC;
        private System.Windows.Forms.RadioButton radioButtonFixedAC;
        private System.Windows.Forms.RadioButton radioButtonATAC;
        private System.Windows.Forms.Button buttonBackup;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.GroupBox groupBoxCheck;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.GroupBox groupBoxSelect;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.GroupBox groupBoxApply;
        private System.Windows.Forms.Button buttonReboot;
        private System.Windows.Forms.Button buttonApplyNow;
        private System.Windows.Forms.Button buttonDelete;
        public System.Windows.Forms.TextBox textBoxDebug;
        private System.Windows.Forms.Button buttonExpand;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.CheckBox checkBoxEnablePM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.GroupBox groupBoxOther;
        private System.Windows.Forms.ComboBox comboBoxSlowDown;
        private System.Windows.Forms.CheckBox checkBoxEnableSlowDown;
        private System.Windows.Forms.Label labelSLI;
    }
}

