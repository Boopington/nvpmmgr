/*  ----------------------------------------------------------------------------
 *  Copyright somemorebytes 2010
	DavidP.
	somemorebytes@gmail.com
 *  ----------------------------------------------------------------------------
 *  NVidia PowerMizer Manager
 *  ----------------------------------------------------------------------------
 *  File:       MainForm.cs
 *  ----------------------------------------------------------------------------


This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Org.Mentalis.Utilities;



namespace NVPMManager
{
    public partial class MainForm : Form, ILoggable
    {
        //PowerMizerManager
        private PwrMzrManager pwrmzrManager;

        //Debug COnsole visible?
        bool expanded = false;

        //ILoggable implementation
        //logging device
        private TextBox _log_console;
        public TextBox Log_Console { set { this._log_console = value; } }


        public MainForm()
        {
            InitializeComponent();

            //Show the Disclaimer! With a checkbox -> save the preference to settings
            if (Properties.Settings.Default.ShowStartAd == true)
            {
                FormDisclaimer frm = new FormDisclaimer();
                frm.ShowDialog();
            }

            //Debug console
            this._log_console = this.textBoxDebug;

            //Fill Comboboxes
            this.FillComboBoxes();

            //Lets initialize the dialog
            this.Initialize();
        }


        private void Initialize()
        {

            this._log_console.Clear();

            log("***** Welcome to NVidia PowerMizer Manager***** ");
            log("Initializing...");

            //Initialize the powermizer manager
            try
            {
                this.pwrmzrManager = new PwrMzrManager();

                //Do we want to debug powerMizerManager?
                this.pwrmzrManager.Log_Console = this._log_console;
                //initialize powermizerManager
                this.pwrmzrManager.initialize();


                //SLI SUPPORT
                if (Properties.Settings.Default.SLIModeEnabled == true)
                {
                    this.Text = "NVidia PowerMizer Manager *Experimental SLI Mode*";
                    this.labelSLI.Visible = true;
                }
                else
                {
                    this.Text = "NVidia PowerMizer Manager";
                    this.labelSLI.Visible = false;
                }

                log("Checking if PowerMizer Settings are present...");

                if (!this.pwrmzrManager.powermizerSettingsExist())
                {
                    //No values for powermizer settings. Block the form until they're created
                    this.groupBoxSelect.Enabled = false;
                    this.groupBoxApply.Enabled = false;
                    this.groupBoxOther.Enabled = false;

                    this.buttonInsert.Enabled = true;
                    this.buttonDelete.Enabled = false;

                    logsub("PowerMizer settings not found. You need create the necessary registry entries in step 1. It is highly recommended to perform a backup first!");
                    MessageBox.Show("PowerMizer settings not found. You need create the necessary registry entries in step 1. It is highly recommended to perform a backup first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                else
                {
                    logsub("PowerMizer Settings found.");

                    this.buttonInsert.Enabled = false;
                    this.buttonDelete.Enabled = true;
                    this.groupBoxSelect.Enabled = true;
                    this.groupBoxApply.Enabled = true;
                    this.groupBoxOther.Enabled = true;
                }
            }
            catch
            {
                //All errors logged
                //We want the whole dialog blocked
                this.groupBoxCheck.Enabled = false;
                this.groupBoxSelect.Enabled = false;
                this.groupBoxApply.Enabled = false;
                this.groupBoxOther.Enabled = false;
                MessageBox.Show("NVidia registry key not found.  Check debug console for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }



            //ok, go ahead and initialize frame 2
            this.InitializeSettingsControls();



            //Add the tooltip to the Instant Apply! button
            ToolTip Tip1 = new ToolTip();
            Tip1.AutoPopDelay = 15000;
            Tip1.UseAnimation = true;
            Tip1.UseFading = true;
            Tip1.ToolTipIcon = ToolTipIcon.Info;
            Tip1.IsBalloon = true;
            Tip1.ShowAlways = true;
            Tip1.ToolTipTitle = "Insane Instant Apply!";
            string message1 = "\nInstant Apply! will apply your settings and reload the video driver without the need of a reboot.\n\nHolding the \"Shift Key\" while clicking on this button, will skip all further checks and confirmations, speeding up the process even more.\nPlease use it **only** if you're sure that the configuration you're applying has already been tested before!!!!.";
            Tip1.SetToolTip(this.buttonApplyNow, message1);
           

        }

        //Fill the levels in the comboboxes
        private void FillComboBoxes()
        {
            this.comboBoxLevelBatt.Items.Clear();
            this.comboBoxLevelAC.Items.Clear();

            KeyValuePair<PwrMzrValue, string> dis = new KeyValuePair<PwrMzrValue, string>(PwrMzrValue.PM_DISABLED, "None");
            this.comboBoxLevelBatt.Items.Add(dis);
            this.comboBoxLevelAC.Items.Add(dis);

            KeyValuePair<PwrMzrValue, string> min = new KeyValuePair<PwrMzrValue, string>(PwrMzrValue.MIN_PERF, "Min. Perf / Max. PowerSave");
            this.comboBoxLevelBatt.Items.Add(min);
            this.comboBoxLevelAC.Items.Add(min);

            KeyValuePair<PwrMzrValue, string> med = new KeyValuePair<PwrMzrValue, string>(PwrMzrValue.MED_PERF, "Med. Perf / Med. PowerSave");
            this.comboBoxLevelBatt.Items.Add(med);
            this.comboBoxLevelAC.Items.Add(med);

            KeyValuePair<PwrMzrValue, string> max = new KeyValuePair<PwrMzrValue, string>(PwrMzrValue.MAX_PERF, "Max. Perf / Min. PowerSave");
            this.comboBoxLevelBatt.Items.Add(max);
            this.comboBoxLevelAC.Items.Add(max);


            this.comboBoxLevelBatt.DisplayMember = "Value";
            this.comboBoxLevelBatt.ValueMember="Key";
            this.comboBoxLevelAC.DisplayMember = "Value";
            this.comboBoxLevelAC.ValueMember = "Key";

            this.comboBoxSlowDown.Items.Clear();
            KeyValuePair<int, string> nonav= new KeyValuePair<int, string>(0, "Unavailable");
            KeyValuePair<int, string> none = new KeyValuePair<int, string>(1, "Disable Overheat Slowdown");
            KeyValuePair<int, string> mem = new KeyValuePair<int, string>(2, "Slowdown Mem Only (NVRAM)");
            KeyValuePair<int, string> core = new KeyValuePair<int, string>(3, "Slowdown GPU Core Only");
            KeyValuePair<int, string> both = new KeyValuePair<int, string>(4, "Slowdown Mem & GPU Core");
            this.comboBoxSlowDown.Items.Add(nonav);
            this.comboBoxSlowDown.Items.Add(none);
            this.comboBoxSlowDown.Items.Add(mem);
            this.comboBoxSlowDown.Items.Add(core);
            this.comboBoxSlowDown.Items.Add(both);

            this.comboBoxSlowDown.DisplayMember = "Value";
            this.comboBoxSlowDown.ValueMember = "Key";

        }

        //Initialize the frame2 controls
        private void InitializeSettingsControls()
        {
            try
            {
                PwrMzrSettings pmset = this.pwrmzrManager.readPowermizerSettings();

                log("Populating Dialog Controls...");
                //Powermizer_Enabled checkbox
                if (pmset.PowerMizerEnabled.EntryValue == PwrMzrValue.PM_DISABLED)
                {
                    this.checkBoxEnablePM.Checked = false;
                }
                else
                {
                    this.checkBoxEnablePM.Checked = true;
                }
                this.checkBoxEnablePM_CheckedChanged(null, null);

                //Powermizer radiobuttons
                switch (pmset.PowerMizerGovernor.EntryValue)
                {
                    case PwrMzrValue.BAT_ON_AC_ON:
                        this.radioButtonATBAtt_Click(null,null);
                        this.radioButtonATAC_Click(null,null);
                        break;
                    case PwrMzrValue.BAT_ON_AC_FIXED:
                        this.radioButtonATBAtt_Click(null, null);
                        this.radioButtonFixedAC_Click(null, null);
                        break;
                    case PwrMzrValue.BAT_FIXED_AC_ON:
                        this.radioButtonFixedBatt_Click(null, null);
                        this.radioButtonATAC_Click(null, null);
                        break;
                    case PwrMzrValue.BAT_FIXED_AC_FIXED:
                        this.radioButtonFixedBatt_Click(null, null);
                        this.radioButtonFixedAC_Click(null, null);
                        break;
                }

                //BAttery fixed combo
                foreach (KeyValuePair<PwrMzrValue, string> pair in this.comboBoxLevelBatt.Items)
                {
                    if (pair.Key == pmset.PowerMizerBatteryFixedLevel.EntryValue)
                    {
                        this.comboBoxLevelBatt.SelectedItem = pair;
                        break;
                    }
                }

                //AC fixed combo
                foreach (KeyValuePair<PwrMzrValue, string> pair in this.comboBoxLevelAC.Items)
                {
                    if (pair.Key == pmset.PowerMizerACFixedLevel.EntryValue)
                    {
                        this.comboBoxLevelAC.SelectedItem = pair;
                        break;
                    }
                }

                //Overheat Slowdown
                if (pmset.OverheatSlowdownOverride)
                {
                    int selected = 0;
                    this.checkBoxEnableSlowDown.Checked = true;

                    if (pmset.OverheatSlowdownEnabled.EntryValue==PwrMzrValue.PM_ENABLED)
                    {
                        if (pmset.OverheatSlowdownMemory.EntryValue == PwrMzrValue.PM_ENABLED)
                        {
                            if (pmset.OverheatSlowdownCore.EntryValue == PwrMzrValue.PM_ENABLED)
                            {
                                //MEM & Core Slowdown
                                selected = 4;

                            }
                            else 
                            {
                                //MEM Slowdown Only
                                selected = 2;
                            }
                        }
                        else
                        {
                            if (pmset.OverheatSlowdownCore.EntryValue == PwrMzrValue.PM_ENABLED)
                            {
                                //COre Slowdown Only
                                selected = 3;
                            }
                            else
                            {
                                //Slowdown disabled on both MEM & Core. This state is the same as OverheatSlowdownEnabled==PM_DISABLED
                                selected=1;
                            }
                        }
                    }
                    else //Slowdown Disabled
                    {
                        selected = 1;
                    }

                    foreach (KeyValuePair<int, string> k in this.comboBoxSlowDown.Items)
                    {
                        if (k.Key == selected) this.comboBoxSlowDown.SelectedItem = k;
                    }


                }
                else
                {
                    this.checkBoxEnableSlowDown.Checked = false;
                }
                this.checkBoxEnableSlowDown_CheckedChanged(null, null);





                logsub("Success...");      
            }
            catch
            {
                //Errors already logged
                MessageBox.Show("Couldn't read PowerMizer settings. Check debug console for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }




        }

        //Backup the registry settings
        private void buttonBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = Environment.SpecialFolder.MyComputer.ToString();
            dlg.Filter = "Registry files (*.reg)|*.reg|All Files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //Make the export
                try
                {
                    this.pwrmzrManager.exportPowermizerSettings(dlg.FileName);
                }
                catch
                {
                    //All errors alreay logged
                    MessageBox.Show("Couldn't backup registry settings. Check debug console for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }
        }

        //Import from a registry backup file
        private void buttonRestore_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.SpecialFolder.MyComputer.ToString();
            dlg.Filter = "Registry files (*.reg)|*.reg|All Files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    //Before import, delete all the PowerMizer settings.
                    //If they were exported, they will be imported from the file. Otherwise, we created them, so we need to remove them
                    this.pwrmzrManager.deletePowermizerSettings();

                    //Import the registry tree
                    this.pwrmzrManager.importPowermizerSettings(dlg.FileName);
                }
                catch
                {
                    //All errors already logged
                    MessageBox.Show("Couldn't import registry settings. Check debug console for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }
        }

        //Show debug console
        private void buttonExpand_Click(object sender, EventArgs e)
        {
            if (!this.expanded)
            {
                this.Width = 900;
                this.expanded = true;
                this.buttonExpand.Text = "<<";
                this._log_console.Select(this._log_console.TextLength, 0);
                this._log_console.ScrollToCaret();

            }
            else
            {
                this.Width = 470;
                this.expanded = false;
                this.buttonExpand.Text = ">>";
            }

        }

        //Copy debug console contents to clipboard to report bugs
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.textBoxDebug.Text);
        }


        #region /******* Iloggable Implementation *******/
        //write something to the log, and scroll it down.
        public void log(string message)
        {

            if (this._log_console == null) return;


            this._log_console.Text += "(***) --> " + message + "\r\n";

            //Scroll the textbox  down
            this._log_console.Select(this._log_console.TextLength, 0);
            this._log_console.ScrollToCaret();



        }

        public void logsub(string message)
        {

            if (this._log_console == null) return;


            this._log_console.Text += "(***)\t --> " + message + "\r\n";

            //Scroll the textbox  down
            this._log_console.Select(this._log_console.TextLength, 0);
            this._log_console.ScrollToCaret();



        }


        #endregion


        //Checkbox Powermizer_Enabled click event
        private void checkBoxEnablePM_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxEnablePM.Checked == true)
            {
                this.groupBoxBattery.Enabled = true;
                this.groupBoxAC.Enabled = true;
            }
            else
            {
                this.groupBoxBattery.Enabled = false;
                this.groupBoxAC.Enabled = false;
            }
        }


        //Radio ATBatt click event
        private void radioButtonATBAtt_Click(object sender, EventArgs e)
        {
            this.radioButtonATBAtt.Checked = true;
            this.radioButtonFixedBatt.Checked = false;
            this.comboBoxLevelBatt.Enabled = false;
            
        }

        //Radio FixedBatt click event
        private void radioButtonFixedBatt_Click(object sender, EventArgs e)
        {
            this.radioButtonFixedBatt.Checked = true;
            this.radioButtonATBAtt.Checked = false;
            this.comboBoxLevelBatt.Enabled = true;
            

        }

        //Radio ATAC click event
        private void radioButtonATAC_Click(object sender, EventArgs e)
        {
            this.radioButtonATAC.Checked = true;
            this.radioButtonFixedAC.Checked = false;
            this.comboBoxLevelAC.Enabled = false;
        }

        //Radio FixedAC click event
        private void radioButtonFixedAC_Click(object sender, EventArgs e)
        {
            this.radioButtonFixedAC.Checked = true;
            this.radioButtonATAC.Checked = false;
            this.comboBoxLevelAC.Enabled = true;
        }

        //Create powermizer settings button click event
        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Powermizer Settings will be created in the registry.", "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {

                log("Inserting Default Powermizer entries...");

                try
                {
                    PwrMzrSettings pmset = new PwrMzrSettings(PwrMzrValue.DEFAULT);

                    this.pwrmzrManager.changePowermizerSettings(pmset);

                    logsub("Success...");
                }
                catch
                {
                    //All errors logged
                    MessageBox.Show("Could't create registry settings. Check debug console for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            //Reinitialize the dialog
            this.Initialize();

        }

        //DElete powermizer settings button click event
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Powermizer Settings will be deleted from the registry.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                log("Deleting Powermizer entries...");

                try
                {
                    this.pwrmzrManager.deletePowermizerSettings();

                    logsub("Success...");
                }
                catch
                {
                    //All errors logged
                    MessageBox.Show("Could't delete registry settings. Check debug console for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            //Reinitialize the dialog
            this.Initialize();

        }

        //Create a powermizerSettings structure from the controls configuration
        private PwrMzrSettings collectDataFromControls()
        {

            PwrMzrValue pm_enab, gov, battfix, acfix;
            PwrMzrValue OHSDEnabled = PwrMzrValue.PM_DISABLED, OHSDMem = PwrMzrValue.PM_DISABLED, OHSDCore = PwrMzrValue.PM_DISABLED;
            bool OHSDOverride=false;
            PwrMzrSettings pmset;

            log("Reading dialog...");
            //Checkbox ENABLE/DISABLE Powermizer
            if (!this.checkBoxEnablePM.Checked)
            {
                pm_enab = PwrMzrValue.PM_DISABLED;
                gov = PwrMzrValue.BAT_FIXED_AC_FIXED;
                battfix = PwrMzrValue.PM_DISABLED;
                acfix = PwrMzrValue.PM_DISABLED;
            }
            else
            {
                pm_enab = PwrMzrValue.PM_ENABLED;

                //Governor
                if(this.radioButtonATBAtt.Checked){
                    
                    if(this.radioButtonATAC.Checked)
                    {
                        //0x3333
                        gov = PwrMzrValue.BAT_ON_AC_ON;
                        acfix= PwrMzrValue.MAX_PERF;
                        battfix = PwrMzrValue.MAX_PERF;

                    }
                    else
                    {
                        //0x3322
                        gov = PwrMzrValue.BAT_ON_AC_FIXED;
                        acfix = ((KeyValuePair<PwrMzrValue, string>)this.comboBoxLevelAC.SelectedItem).Key;
                        battfix = PwrMzrValue.MAX_PERF;
                    }

                }
                else
                {
                    if (this.radioButtonATAC.Checked)
                    {
                        //0x2233
                        gov = PwrMzrValue.BAT_FIXED_AC_ON;
                        battfix = ((KeyValuePair<PwrMzrValue, string>)this.comboBoxLevelBatt.SelectedItem).Key;
                        acfix = PwrMzrValue.MAX_PERF;
                    }
                    else
                    {
                        //0x2222
                        gov = PwrMzrValue.BAT_FIXED_AC_FIXED;
                        acfix = ((KeyValuePair<PwrMzrValue, string>)this.comboBoxLevelAC.SelectedItem).Key;
                        battfix = ((KeyValuePair<PwrMzrValue, string>)this.comboBoxLevelBatt.SelectedItem).Key;
                    }

                }

               

            }

            //Overheat slowdown
            if (this.checkBoxEnableSlowDown.Checked)
            {
                OHSDOverride = true;
                int key = ((KeyValuePair<int, string>)this.comboBoxSlowDown.SelectedItem).Key;

                switch (key)
                {
                    case 1:
                        OHSDEnabled = PwrMzrValue.PM_DISABLED;
                        OHSDMem = PwrMzrValue.PM_DISABLED;
                        OHSDCore = PwrMzrValue.PM_DISABLED;
                        break;
                    case 2:
                        OHSDEnabled = PwrMzrValue.PM_ENABLED;
                        OHSDMem = PwrMzrValue.PM_ENABLED;
                        OHSDCore = PwrMzrValue.PM_DISABLED;
                        break;
                    case 3:
                        OHSDEnabled = PwrMzrValue.PM_ENABLED;
                        OHSDMem = PwrMzrValue.PM_DISABLED;
                        OHSDCore = PwrMzrValue.PM_ENABLED;
                        break;
                    case 4:
                        OHSDEnabled = PwrMzrValue.PM_ENABLED;
                        OHSDMem = PwrMzrValue.PM_ENABLED;
                        OHSDCore = PwrMzrValue.PM_ENABLED;
                        break;
                 
                }

            }
            else //They're already initialized. This else can be deleted
            {
                OHSDOverride = false;
                OHSDEnabled = PwrMzrValue.PM_DISABLED;
                OHSDMem = PwrMzrValue.PM_DISABLED;
                OHSDCore = PwrMzrValue.PM_DISABLED;

            }

            pmset = new PwrMzrSettings(pm_enab, gov, battfix, acfix, OHSDOverride, OHSDEnabled, OHSDMem, OHSDCore);

            if (pmset.PowerMizerEnabled.EntryValue == PwrMzrValue.PM_ENABLED)
            {
                log("Powermizer will be ENABLED");
            }
            else
            {
                log("Powermizer will be DISABLED");
            }
            logsub("Selected Governor: " + pmset.PowerMizerGovernor.EntryValue.ToString());
            logsub("Selected Fixed Batt Level: " + pmset.PowerMizerBatteryFixedLevel.EntryValue.ToString());
            logsub("Selected Fixed AC Level: " + pmset.PowerMizerACFixedLevel.EntryValue.ToString());

            if (pmset.OverheatSlowdownOverride)
            {
                logsub("Overheat Slowdown settings will be OVERRIDEN");
                if (pmset.OverheatSlowdownEnabled.EntryValue == PwrMzrValue.PM_ENABLED)
                {
                    logsub("Overheat Slowdown is ENABLED.");
                }
                else
                {
                    logsub("Overheat Slowdown is DISABLED.");
                }
                if (pmset.OverheatSlowdownMemory.EntryValue == PwrMzrValue.PM_ENABLED)
                {
                    logsub("Overheat MEMORY Slowdown is ENABLED.");
                }
                else
                {
                    logsub("Overheat MEMORY Slowdown is DISABLED.");
                }
                if (pmset.OverheatSlowdownCore.EntryValue == PwrMzrValue.PM_ENABLED)
                {
                    logsub("Overheat CORE Slowdown is ENABLED.");
                }
                else
                {
                    logsub("Overheat CORE Slowdown is DISABLED.");
                }

            }
            else
            {
                logsub("Will NOT override Overheat Slowdown settings");
            }
           
            return pmset;

        }

        //Test if the options selected are possible
        private bool testConfig()
        {
            log("Testing selected configuration...");

            if (this.checkBoxEnablePM.Checked)
            {

                if( (this.radioButtonFixedBatt.Checked) && (  ((KeyValuePair<PwrMzrValue, string>)this.comboBoxLevelBatt.SelectedItem).Key==PwrMzrValue.PM_DISABLED )){


                    MessageBox.Show("You must select a profile for the Fixed Performance Level on Battery.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    logsub("You must select a profile for the Fixed Performance Level on Battery.");
                    this.comboBoxLevelBatt.Focus();
                    return false;
                }

                if ((this.radioButtonFixedAC.Checked) && (((KeyValuePair<PwrMzrValue, string>)this.comboBoxLevelAC.SelectedItem).Key == PwrMzrValue.PM_DISABLED))
                {


                    MessageBox.Show("You must select a profile for the Fixed Performance Level on AC.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    logsub("You must select a profile for the Fixed Performance Level on AC.");
                    this.comboBoxLevelAC.Focus();
                    return false;
                }
            }

            //Overheat slowdown
            if (this.checkBoxEnableSlowDown.Checked)
            {
                if (((KeyValuePair<int, string>)this.comboBoxSlowDown.SelectedItem).Key == 0)
                {
                    MessageBox.Show("You must select an option to override the Overheat Slowdown settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    logsub("You must select an option to override the Overheat Slowdown settings.");
                    this.comboBoxSlowDown.Focus();
                    return false;
                }
            }


            logsub("Settings are correct!");
            return true;

        }

        //Apply selected settings and reboot
        private void buttonReboot_Click(object sender, EventArgs e)
        {

            if (this.testConfig())
            {
                PwrMzrSettings pm = this.collectDataFromControls();

                try
                {
                    if (MessageBox.Show("The changes will be applied to the registry.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.pwrmzrManager.changePowermizerSettings(pm);
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    //All errors logged
                    MessageBox.Show("Could't change powermizer settings. Check debug console for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                try
                {
                    if (MessageBox.Show("Do you want to reboot NOW?. Click NO if you want to reboot manually.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        WindowsController.ExitWindows(RestartOptions.Reboot, false);
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    log("Error while rebooting!. Try rebooting manually.");
                    return;
                }
            }

        }

        //Instant Apply!  button
        private void buttonApplyNow_Click(object sender, EventArgs e)
        {
            //IF shift is hold while ckicking on the button, skip all the unnecessary checks to speed up the settings applying. INSANE INSTANT APPLY!
            bool skipChecks = false;
            if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                skipChecks = true;
            }


            if (this.testConfig())
            {
                PwrMzrSettings pm = this.collectDataFromControls();

                try
                {
                    if (skipChecks == false)
                    {
                        if (MessageBox.Show("The changes will be applied to the registry.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.pwrmzrManager.changePowermizerSettings(pm);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        //Skipping checks. Right to the nitty-gritty
                        this.pwrmzrManager.changePowermizerSettings(pm);
                    }
                }
                catch
                {
                    //All errors logged
                    MessageBox.Show("Could't change powermizer settings. Check debug console for more details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //Instant Apply! 
                try
                {


                    log("Trying Instant Apply! ...");

                    //Query the data
                    List<string> data = this.pwrmzrManager.readInstantApplyInfo();

                    Guid videoGuid = new Guid(data[0]);
                    string instancePath = data[1].ToUpper();

                    //SLI SUPPORT
                    string instancePathSLI = null;
                    if (Properties.Settings.Default.SLIModeEnabled == true)
                    {
                        if (data.Count == 3)
                        {
                            instancePathSLI = data[2].ToUpper();
                        }
                    }




                    InstantApplyDisc dlg = new InstantApplyDisc();
                    DialogResult result;

                    
                    if (skipChecks == false)
                    {
                        result = dlg.ShowDialog();
                    }
                    else //If we are skipping checks, let's go for it without showing any dialog at all.
                    {
                        result = DialogResult.OK;
                    }


                    if (result == DialogResult.OK)
                    {
                        //Reboot the device
                        logsub("Reinitializing device: " + instancePath);

                        DeviceHelper.SetDeviceEnabled(videoGuid, instancePath, false);
                        DeviceHelper.SetDeviceEnabled(videoGuid, instancePath, true);

                        logsub("Success...");


                        //SLI SUPPORT
                        if (Properties.Settings.Default.SLIModeEnabled == true)
                        {
                            if (instancePathSLI != null)//If we really have another device here, let's reboot it
                            {
                                //Reboot the device
                                logsub("Reinitializing SLI device: " + instancePathSLI);

                                DeviceHelper.SetDeviceEnabled(videoGuid, instancePathSLI, false);
                                DeviceHelper.SetDeviceEnabled(videoGuid, instancePathSLI, true);

                                logsub("Success... (SLIDevice).");
                            }
                        }

                        //dirty patch: After rebooting the device, due to temporary resolution change, 
                        //the width of the dialog changes, so we fix it here.
                        if (this.expanded) this.Width = 900; else this.Width = 470;



                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    logsub("Error while trying Instant Apply! . Try rebooting manually.");
                    MessageBox.Show("At the moment your system does not support the Instant Apply! feature. Check debug console for more details.");
                    return;

                }
            }

        }

        //Collect information for a bug report
        private void buttonReport_Click(object sender, EventArgs e)
        {

           
            Support dlgs = new Support(this.pwrmzrManager, this._log_console.Text);
            
            dlgs.ShowDialog();

           

        
        }
        
        //Check Oberheat slowdown override 
        private void checkBoxEnableSlowDown_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxEnableSlowDown.Checked == true)
            {
                this.comboBoxSlowDown.Enabled = true;

                string message = "Please read this carefully before proceed.\n\n";
                message += "This feature will allow you to override the Overheat Slowdown settings.\n\n";
                message += "By default, your graphic card slowdowns the gpu memory and core clocks ";
                message += "when a certain temperature threshold is reached to avoid overheating. ";
                message += "This often produces performance problems after a while under intensive ";
                message += "GPU usage.\n\n";
                message += "Tweaking this settings may avoid performance issues in some cases, but ";
                message += "can lead to overheat and subsequent graphic glitches in others. ";
                message += "If you run into problems, just uncheck the Overheat Slowdow Override and ";
                message += "the default driver management settings will be restored.";

                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            else
            {
                this.comboBoxSlowDown.Enabled = false;
                this.comboBoxSlowDown.SelectedIndex = 0;
            }
        }

  


    }
}