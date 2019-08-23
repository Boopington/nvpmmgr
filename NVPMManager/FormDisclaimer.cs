/*  ----------------------------------------------------------------------------
 *  Copyright Somemorebytes 2010
	DavidP.
	somemorebytes@gmail.com
 *  ----------------------------------------------------------------------------
 *  NVidia PowerMizer Manager
 *  ----------------------------------------------------------------------------
 *  File:       FormDisclaimer.cs
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
//
using System.Text;
using System.Windows.Forms;

namespace NVPMManager
{
    public partial class FormDisclaimer : Form
    {
        public FormDisclaimer()
        {
            InitializeComponent();
            string text;

            //let's make it in mutiple lines for easy editing, ok?
            //text = "Welcome to Nvidia PowerMizer Manager.\n\n";
            text = "With this application you will be able to tweak the registry settings of your NVidia driver to make the buggy power saving feature (PowerMizer) ";
            text += "behave as you want. This is very desirable in cases where PowerMizer lowers the clock speed of the card while gaming causing performace problems ";
            text += "or when due to a bug in some cards like the 8600M GT the frecuency speed changes causes the screen to blink and some annoying horizontal lines ";
            text += "on your monitor. \n\n";
            text += "Even that disabling powermizer should not cause you any trouble, be adviced that your battery can drain faster if Powermizer is disabled, and your ";
            text += "video card can get a bit hotter than before.\n\n";
            text += "You do this at your own risk. This software comes without any warranty.\n\n";
            text += "It is recommended that you make a backup of your video related registry entries before modify them. The application will allow you to backup the relevant ";
            text += "registry keys and restore them later if needed. Anyway, you could always just reinstall your video driver to revert to the original configuration.";


            this.labelDisclaimer.Text=text;

            
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
           
            //Access the settings, and memorize the state of the radio to not showing again the dialog
            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}