/*  ----------------------------------------------------------------------------
 *  Copyright somemorebytes 2010
	DavidP.
	somemorebytes@gmail.com
 *  ----------------------------------------------------------------------------
 *  NVidia PowerMizer Manager
 *  ----------------------------------------------------------------------------
 *  File:       InstantApplyDisc.cs
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
using System.Management;

namespace NVPMManager
{
    public partial class InstantApplyDisc : Form
    {

        string text;
        public InstantApplyDisc()
        {
            InitializeComponent();


            //disclaimer text
            text = "";
            text += "Instant Apply! feature can apply your new settings without needing to reboot.\n\n";

            text += "BE AWARE that although this feature should work on most computers, and it has been tested with different OS's and video cards, it could ";
            text += "potentially leave your monitor blank until you make a registry restore.\n\n If this is THE FIRST TIME that you use this feature, it is highly ";
            text += "recommended that you make a system restore point just in case you needed to restore booting from the Windows CD/DVD.\n\n";

            text += "This software comes without warranty, yadda yadda yadda...";




            this.labelDis.Text = text;

            this.buttonOK.DialogResult = DialogResult.OK;
            this.buttonCancel.DialogResult = DialogResult.Cancel;

            
        }

        //Create a system restore point using WMI. Should work in XP, VIsta and 7
        private void buttonSystemRestore_Click(object sender, EventArgs e)
        {
            ManagementScope oScope = new ManagementScope("\\\\localhost\\root\\default");
            ManagementPath oPath = new ManagementPath("SystemRestore");
            ObjectGetOptions oGetOp = new ObjectGetOptions();
            ManagementClass oProcess = new ManagementClass(oScope, oPath, oGetOp);

            ManagementBaseObject oInParams = oProcess.GetMethodParameters("CreateRestorePoint");
            oInParams["Description"] = "Nvidia PowerMizer Manager";
            oInParams["RestorePointType"] = 10;
            oInParams["EventType"] = 100;

            this.buttonOK.Enabled = false;
            this.buttonCancel.Enabled = false;
            this.buttonSystemRestore.Enabled = false;

            this.labelDis.Text = "Creating System Restore Point. Please wait...";
                       

            ManagementBaseObject oOutParams = oProcess.InvokeMethod("CreateRestorePoint", oInParams, null);

           

            this.buttonOK.Enabled = true;
            this.buttonCancel.Enabled = true;
            this.buttonSystemRestore.Enabled = true;

            this.labelDis.Text = text;
            


        }
    }
}