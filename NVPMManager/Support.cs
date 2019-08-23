/*  ----------------------------------------------------------------------------
 *  Copyright somemorebytes 2010
	DavidP.
	somemorebytes@gmail.com
 *  ----------------------------------------------------------------------------
 *  NVidia PowerMizer Manager
 *  ----------------------------------------------------------------------------
 *  File:       Support.cs
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
using Ionic.Zip;
using System.IO;

namespace NVPMManager
{
    public partial class Support : Form
    {

        private  PwrMzrManager pwrmgr;
        string debugtext;

        public Support(PwrMzrManager pm, string dbgtxt)
        {
            InitializeComponent();

            this.pwrmgr = pm;
            this.debugtext = dbgtxt;

            string text;
            text = "This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation; either version 2.1 of the License, or (at your option) any later version. This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.\n\n";
            text += "Feedback is welcome. If you want to report a bug, please USE THE COLLECT INFO BUTTON. It will generate a zip file with some relevant registry keys, and the content of the debug console (you can check its contents before sending it). Send it by email to the address stated below.\nNo zip file attached=>God kills a kitten.\n\n";
            text += "This program uses in some way:\n     Mentalis WindowsController Library (http://mentalis.org)\n\t     Justin Grant's DeviceHelper Library (http://www.justingrant.net)\n\t     DotNetZip Library (http://dotnetzip.codeplex.com) ";

            this.labelText.Text = text; 
        }

        //link clicked
        private void linkLabelSMB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://somemorebytes.com/wp/index.php/nvpmmanager/");
        }

        //email clicked
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:somemorebytes@gmail.com?subject=[NvPmMgr] Support Request");
        }

        private void buttonCollect_Click(object sender, EventArgs e)
        {
            try
            {
                string tempPath = Environment.GetEnvironmentVariable("TEMP");

                //Create a new subfolder under the current temp folder
                string newPath = System.IO.Path.Combine(tempPath, "NvPMMgr");
                // Create the subfolder
                System.IO.Directory.CreateDirectory(newPath);

                //videoreg path
                string videoRegPath = System.IO.Path.Combine(newPath, "videoReg.reg");

                //Export
                this.pwrmgr.exportPowermizerSettings(videoRegPath);

                //pciIDPath
                string pciIDPath = System.IO.Path.Combine(newPath, "pciID.reg");

                //Export
                this.pwrmgr.exportPciIDSettings(pciIDPath);

                //Console debug text
               
                string debugTextPath = System.IO.Path.Combine(newPath, "debug.txt");

                using (StreamWriter outfile = new StreamWriter(debugTextPath))
                {
                    outfile.Write(debugtext);
                }

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = Environment.SpecialFolder.MyComputer.ToString();
                dlg.Filter = "Zip Files (*.zip)|*.zip|All Files (*.*)|*.*";
                dlg.FilterIndex = 1;
                dlg.RestoreDirectory = true;


                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //Create zip file
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(newPath);
                        zip.Save(dlg.FileName);
                    }
                }

                System.IO.Directory.Delete(newPath, true);
            }
            catch
            {
                MessageBox.Show("Having an error in the support form... Isn't it ironic?");
                return;
            }
        }

        private void buttonWarn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowStartAd = true;
            Properties.Settings.Default.Save();
        }
    }
}