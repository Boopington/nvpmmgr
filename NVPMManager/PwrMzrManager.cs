/*  ----------------------------------------------------------------------------
 *  Copyright Somemorebytes 2010
	DavidP.
	somemorebytes@gmail.com
 *  ----------------------------------------------------------------------------
 *  NVidia PowerMizer Manager
 *  ----------------------------------------------------------------------------
 *  File:       PwrMzrManager.cs
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

using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace NVPMManager
{
    //This class will make use of the RegistryManager to modify the nvidia powermizer settings.

    public class PwrMzrManager : ILoggable
    {

        //The root key. A subkey of this key will contain the powermizer settings
        string baseKeyName = @"SYSTEM\CurrentControlSet\Control\Video";

        public RegistryKey baseKey;

        //NVIDIA regkey.
        public RegistryKey nvidiaKey;

        //NVIDIA Secondary key (FOR SLI/HYBRID SYSTEMS)
        public RegistryKey nvidiaSLIKey;
        

        //Registry Manager
        private RegistryManager regmgr;


        //ILoggable implementation
        //logging device
        private TextBox _log_console;
        public TextBox Log_Console { set { this._log_console = value; } }


        public PwrMzrManager()
        {

        }

        public void initialize()
        {
            //initialize registry manager
            this.regmgr = new RegistryManager();

            //Do we want to debug the registry manager? If not, just comment this line
            this.regmgr.Log_Console = this._log_console;

            //Open Base key
            this.baseKey = Registry.LocalMachine.OpenSubKey(this.baseKeyName);

            //First things first. Locate the NVIDIA key
            try
            {
                this.nvidiaKey = this.locateNvidiaKey();
                
            }
            catch
            {
                //Errors already logged
                throw;
            }

            this.readHarwareDetails();
        }

        #region /******* PowerMizer Settings *******/

        //Read and log some hardware details
        public void readHarwareDetails()
        {
            if (this.nvidiaKey != null)
            {
                object driverdesc, driverversion, biosstring;
                log("Trying to locate some useful unformation...");

                driverdesc = this.regmgr.getDataValue(this.nvidiaKey, "DriverDesc");
                driverversion = this.regmgr.getDataValue(this.nvidiaKey, "DriverVersion");
                biosstring =this.regmgr.getDataValue(this.nvidiaKey, "HardwareInformation.BiosString");

                if (driverdesc != null)
                {
                    logsub("Driver Description: " + driverdesc.ToString());
                }
                if (driverversion != null)
                {
                    logsub("Driver Version: " + driverversion.ToString());
                }
                if (biosstring != null)
                {
                    logsub("Video BIOS Version: " + biosstring.ToString());
                }

            }

        }

        //Check if the powermizer settings are present in the nvidia Key
        public bool powermizerSettingsExist()
        {
            if (this.nvidiaKey != null)
            {
                log("Looking for PerfLEvelSrc Key...");
                object val= this.regmgr.getDataValue(this.nvidiaKey, "PerfLevelSrc");
                if (val != null)
                {
                    logsub("Value Found.");
                    return true ;
                }
                else
                {
                    logsub("Value not found!.");
                    return false;
                }

            }

            return false;
        }



        //Locate the key where  which the Nvidia powermizer settings should be under
        public RegistryKey locateNvidiaKey()
        {

            try
            {

                //Get all the possible subkkeys named "0000"
                log("Looking for suitable keys...");
                List<RegistryKey> matches = regmgr.GetSubKeysRecursive(this.baseKey, "0000");


                //The key we're looking for should have the pair Name Value ProviderName/NVIDIA
                //We could use as well number of elements to identify them, as there should be dozens.
                List<RegistryKey> possibleKeys = new List<RegistryKey>();
                foreach (RegistryKey k in matches)
                {
                    if (((string)regmgr.getDataValue(k, "ProviderName")) == "NVIDIA")
                    {
                        possibleKeys.Add(k);
                    }
                }

                
                //if (possibleKeys.Count == 0)
                //{
                //    Exception w = new Exception("Key not found! This software will work only with NVidia cards.");
                //    logsub(w.Message);
                //    throw (w);

                //}
                //0.95 patch. Some rare cases do not have the ProviderName entry. However, they have other entries that can be identified. Let's give it another
                //chance to identify the key, alright? This is not tested. Need feedback here!
                if (possibleKeys.Count == 0)
                {
                    logsub("Changing to alternate Nvidia root key identification... (Not fully tested. Please provide feedback)");
                   
                    string devdesc;
                    foreach (RegistryKey k in matches)
                    {
                        devdesc=((string)regmgr.getDataValue(k, "Device Description"));
                        if ( devdesc !=null   )   
                        {
                            if (devdesc.Contains("NVIDIA"))
                            {
                                possibleKeys.Add(k);
                            }
                        }
                    }

                    //If it is still 0
                    if (possibleKeys.Count == 0)
                    {
                        Exception w = new Exception("Key not found! This software will work only with NVidia cards.");
                        logsub(w.Message);
                        throw (w);
                    }

                }
                                
                //Ok, now we have all the matches. THERE SHOULD BE ONLY ONE. If there are more, how can we be sure which one is correct?
                //0.98 ADDED SUPPORT FOR SLI/HYBRID SYSTEMS.
                //Let's admit a secondary nvidia key. Will be almost transparent to the app. Won't change the dialogs whatsoever.
                //To add support the PowermizerManager class will check a global variable, and when in SLIMode, will apply the exact same registry changes
                //To a secondary key as well. 
                //Not the most elegant solution. Will improve over time if it works at all, which is still improbable.
                if (possibleKeys.Count > 2)
                {
                    Exception w = new Exception("More than TWO keys identified. Can't be sure which one to modify. This was not expected as from version 0.98 Powermizer Manager added experimental support for SLI/Hybrid systems. You can try to reinstall your video drivers. This may fix any registry mess that's causing this problem. You can contact devs and send debug info using the [Problems?] button and contribute to solve this problem.");
                    logsub(w.Message);
                    throw (w);

                }

                  

                //So we have 1 key!
                RegistryKey nvidiaKey = possibleKeys[0];
                logsub("Key found at: " + nvidiaKey.ToString());
                
                try
                {
                    //We need to RE-open the key with write access.
                     nvidiaKey= regmgr.reOpenKeyWriteAccess(nvidiaKey);

                     //Failsafe.//Global variable indicating SLI Mode is DISABLED
                     Properties.Settings.Default.SLIModeEnabled = false;
                     
                }
                catch
                {
                    //All errors logged
                    logsub("If you have UAC activated, you must run this application with Administrator Privileges (Right click, Run as administrator...)");
                    throw;
                }
               
                
                //SLI MODE SUPPORT
                if (possibleKeys.Count > 1)
                {
                    //SLI MODE. We have exactly 2 keys. Let's init the secondary as well.
                    this.nvidiaSLIKey = possibleKeys[1];
                    logsub("Secondary NVIDIA Key Found. SLI/Hybrid Mode ACTIVATED.");
                    logsub("Key found at: " + this.nvidiaSLIKey.ToString());
                    
                    //Global variable indicating SLI Mode is ENABLED
                    Properties.Settings.Default.SLIModeEnabled = true;


                    try
                    {
                        //We need to RE-open the key with write access.
                        this.nvidiaSLIKey = regmgr.reOpenKeyWriteAccess(this.nvidiaSLIKey);

                    }
                    catch
                    {
                        //All errors logged
                        logsub("If you have UAC activated, you must run this application with Administrator Privileges (Right click, Run as administrator...)");
                        throw;
                    }

                }   

                return nvidiaKey; //THIS RETURN ONLY THE FIRST ONE, AS IT DID FROM THE BEGINNING

            }
            catch
            {
                //Errors already logged
                throw;
            }
        }


        //Delete the powermizer Name/Value entries in the registry (for example, to use prior a reg restore)
        public void deletePowermizerSettings()
        {
            if (this.nvidiaKey != null)
            {
                log("Deleting Powermizer settings from the registry...");

                try
                {
                                   

                    //Let's extract the name of the keys from a settings struct 
                    PwrMzrSettings auxstruct = new PwrMzrSettings(PwrMzrValue.DEFAULT);
                    this.regmgr.deleteValue(this.nvidiaKey, auxstruct.PowerMizerEnabled.EntryName);
                    this.regmgr.deleteValue(this.nvidiaKey, auxstruct.PowerMizerGovernor.EntryName);
                    this.regmgr.deleteValue(this.nvidiaKey, auxstruct.PowerMizerBatteryFixedLevel.EntryName);
                    this.regmgr.deleteValue(this.nvidiaKey, auxstruct.PowerMizerACFixedLevel.EntryName);

                    //SLI/HYBRID SUPPORT
                    if (Properties.Settings.Default.SLIModeEnabled == true)
                    {

                        this.regmgr.deleteValue(this.nvidiaSLIKey, auxstruct.PowerMizerEnabled.EntryName);
                        this.regmgr.deleteValue(this.nvidiaSLIKey, auxstruct.PowerMizerGovernor.EntryName);
                        this.regmgr.deleteValue(this.nvidiaSLIKey, auxstruct.PowerMizerBatteryFixedLevel.EntryName);
                        this.regmgr.deleteValue(this.nvidiaSLIKey, auxstruct.PowerMizerACFixedLevel.EntryName);
                    }


                    deletePowermizerSlowDownSettings();


                    //Flush the key for the changes to take effect
                    this.regmgr.PersistKeyChanges(this.nvidiaKey);
                    logsub("Success...");

                    //SLI/HYBRID SUPPORT
                    if (Properties.Settings.Default.SLIModeEnabled == true)
                    {
                        this.regmgr.PersistKeyChanges(this.nvidiaSLIKey);
                        logsub("Success... (SLIKey).");
                    }


                    
                }
                catch
                {
                    //All errors logged
                    throw;
                }

                
            }


        }

        //Delete the powermizer Slowdown override Name/Value entries in the registry (Need a separate function because may want to delete this settings,
        //but no the original powermizer ones
        public void deletePowermizerSlowDownSettings()
        {
            if (this.nvidiaKey != null)
            {
                log("Deleting Powermizer Overheat SlowDown Override settings from the registry...");

                try
                {


                    //Let's extract the name of the keys from a settings struct 
                    PwrMzrSettings auxstruct = new PwrMzrSettings(PwrMzrValue.DEFAULT);
                    if (this.regmgr.getDataValue(this.nvidiaKey, auxstruct.OverheatSlowdownEnabled.EntryName) != null)
                    {
                        logsub("Deleting...");
                        this.regmgr.deleteValue(this.nvidiaKey, auxstruct.OverheatSlowdownEnabled.EntryName);
                    }

                    if (this.regmgr.getDataValue(this.nvidiaKey, auxstruct.OverheatSlowdownMemory.EntryName) != null)
                    {
                        logsub("Deleting...");
                        this.regmgr.deleteValue(this.nvidiaKey, auxstruct.OverheatSlowdownMemory.EntryName);
                    }

                    if (this.regmgr.getDataValue(this.nvidiaKey, auxstruct.OverheatSlowdownCore.EntryName) != null)
                    {
                        logsub("Deleting...");
                        this.regmgr.deleteValue(this.nvidiaKey, auxstruct.OverheatSlowdownCore.EntryName);
                    }


                    //Flush the key for the changes to take effect
                    this.regmgr.PersistKeyChanges(this.nvidiaKey);
                    logsub("Success...");


                    //SLI/HYBRID SUPPORT
                    if (Properties.Settings.Default.SLIModeEnabled == true)
                    {
                        if (this.regmgr.getDataValue(this.nvidiaSLIKey, auxstruct.OverheatSlowdownEnabled.EntryName) != null)
                        {
                            logsub("Deleting...");
                            this.regmgr.deleteValue(this.nvidiaSLIKey, auxstruct.OverheatSlowdownEnabled.EntryName);
                        }

                        if (this.regmgr.getDataValue(this.nvidiaSLIKey, auxstruct.OverheatSlowdownMemory.EntryName) != null)
                        {
                            logsub("Deleting...");
                            this.regmgr.deleteValue(this.nvidiaSLIKey, auxstruct.OverheatSlowdownMemory.EntryName);
                        }

                        if (this.regmgr.getDataValue(this.nvidiaSLIKey, auxstruct.OverheatSlowdownCore.EntryName) != null)
                        {
                            logsub("Deleting...");
                            this.regmgr.deleteValue(this.nvidiaSLIKey, auxstruct.OverheatSlowdownCore.EntryName);
                        }


                        //Flush the key for the changes to take effect
                        this.regmgr.PersistKeyChanges(this.nvidiaSLIKey);
                        logsub("Success... (SLIKey).");

                    }



                }
                catch
                {
                    //All errors logged
                    throw;
                }


            }


        }

        //Modify the Powermizer Settings
        public void changePowermizerSettings(PwrMzrSettings settings)
        {
            if (this.nvidiaKey != null)
            {
                log("Writing Powermizer settings into the registry...");

                try
                {
                    
                    this.regmgr.setValue(this.nvidiaKey, settings.PowerMizerEnabled.EntryName, settings.PowerMizerEnabled.EntryValue, settings.PowerMizerEnabled.EntryType);
                    this.regmgr.setValue(this.nvidiaKey, settings.PowerMizerGovernor.EntryName, settings.PowerMizerGovernor.EntryValue, settings.PowerMizerGovernor.EntryType);
                    this.regmgr.setValue(this.nvidiaKey, settings.PowerMizerBatteryFixedLevel.EntryName, settings.PowerMizerBatteryFixedLevel.EntryValue, settings.PowerMizerBatteryFixedLevel.EntryType);
                    this.regmgr.setValue(this.nvidiaKey, settings.PowerMizerACFixedLevel.EntryName, settings.PowerMizerACFixedLevel.EntryValue, settings.PowerMizerACFixedLevel.EntryType);

                    //SLI/HYBRID SUPPORT
                    if (Properties.Settings.Default.SLIModeEnabled == true)
                    {
                        this.regmgr.setValue(this.nvidiaSLIKey, settings.PowerMizerEnabled.EntryName, settings.PowerMizerEnabled.EntryValue, settings.PowerMizerEnabled.EntryType);
                        this.regmgr.setValue(this.nvidiaSLIKey, settings.PowerMizerGovernor.EntryName, settings.PowerMizerGovernor.EntryValue, settings.PowerMizerGovernor.EntryType);
                        this.regmgr.setValue(this.nvidiaSLIKey, settings.PowerMizerBatteryFixedLevel.EntryName, settings.PowerMizerBatteryFixedLevel.EntryValue, settings.PowerMizerBatteryFixedLevel.EntryType);
                        this.regmgr.setValue(this.nvidiaSLIKey, settings.PowerMizerACFixedLevel.EntryName, settings.PowerMizerACFixedLevel.EntryValue, settings.PowerMizerACFixedLevel.EntryType);
                    }

                    //Overheat SlowDown 
                    if (settings.OverheatSlowdownOverride)
                    {
                        logsub("Overriding Overheat Slowdown settings...");
                        this.regmgr.setValue(this.nvidiaKey, settings.OverheatSlowdownEnabled.EntryName, settings.OverheatSlowdownEnabled.EntryValue, settings.OverheatSlowdownEnabled.EntryType);
                        this.regmgr.setValue(this.nvidiaKey, settings.OverheatSlowdownMemory.EntryName, settings.OverheatSlowdownMemory.EntryValue, settings.OverheatSlowdownMemory.EntryType);
                        this.regmgr.setValue(this.nvidiaKey, settings.OverheatSlowdownCore.EntryName, settings.OverheatSlowdownCore.EntryValue, settings.OverheatSlowdownCore.EntryType);

                        //SLI SUPPORT
                        if (Properties.Settings.Default.SLIModeEnabled == true)
                        {
                            this.regmgr.setValue(this.nvidiaSLIKey, settings.OverheatSlowdownEnabled.EntryName, settings.OverheatSlowdownEnabled.EntryValue, settings.OverheatSlowdownEnabled.EntryType);
                            this.regmgr.setValue(this.nvidiaSLIKey, settings.OverheatSlowdownMemory.EntryName, settings.OverheatSlowdownMemory.EntryValue, settings.OverheatSlowdownMemory.EntryType);
                            this.regmgr.setValue(this.nvidiaSLIKey, settings.OverheatSlowdownCore.EntryName, settings.OverheatSlowdownCore.EntryValue, settings.OverheatSlowdownCore.EntryType);
                        }
                    }
                    else //If we dont want them overriden
                    {
                        if (this.regmgr.getDataValue(this.nvidiaKey, settings.OverheatSlowdownEnabled.EntryName) != null)//If they were overriden before
                        {
                            this.deletePowermizerSlowDownSettings();
                        }
                    }

                    //Flush the key for the changes to take effect
                    this.regmgr.PersistKeyChanges(this.nvidiaKey);
                    logsub("Success...");

                    //SLI SUPPORT
                    if (Properties.Settings.Default.SLIModeEnabled == true)
                    {
                        this.regmgr.PersistKeyChanges(this.nvidiaSLIKey);
                        logsub("Success... (SLIKey).");
                    }

                }
                catch
                {
                    //All errors logged
                    throw;
                }

               
            }

        }

        //Read the prowermizer Settings
        public PwrMzrSettings readPowermizerSettings()
        {

            //Read the settings from the registry
            log("Extracting PowerMizer settings...");

            object enab, gov, batfix, acfix;
            object sdenab, sdmem, sdcore;
            bool sdoverr=false;
            try
            {
                //Let's extract the name of the keys from a settings struct 
                PwrMzrSettings auxstruct = new PwrMzrSettings(PwrMzrValue.DEFAULT);
                enab = regmgr.getDataValue(this.nvidiaKey, auxstruct.PowerMizerEnabled.EntryName);
                gov = regmgr.getDataValue(this.nvidiaKey, auxstruct.PowerMizerGovernor.EntryName);
                batfix = regmgr.getDataValue(this.nvidiaKey, auxstruct.PowerMizerBatteryFixedLevel.EntryName);
                acfix = regmgr.getDataValue(this.nvidiaKey, auxstruct.PowerMizerACFixedLevel.EntryName);

                //This settings may not be there!
                sdenab = regmgr.getDataValue(this.nvidiaKey, auxstruct.OverheatSlowdownEnabled.EntryName);
                sdmem = regmgr.getDataValue(this.nvidiaKey, auxstruct.OverheatSlowdownMemory.EntryName);
                sdcore = regmgr.getDataValue(this.nvidiaKey, auxstruct.OverheatSlowdownCore.EntryName);

            }
            catch
            {
                logsub("There were some major problems while reading the PowerMizer Settings.");
                throw;
            }

            //Patch in 0.95. Detect inconsistet registry keys caused by playing with different driver versions.
            //In some rare cases, the PerfLevelSrc entry is there, but the others are not, leading to strange situations. This provides a way out.
            if ((enab == null) || (gov == null) || (batfix == null) || (acfix == null))
            {
                logsub("Your Registry settings seem to be inconsistent. You have **SOME** of the required entries for PowerMizer Management, but strangely enough, not **ALL** of them.This can be caused by installing/uninstalling several different drivers versions.");
                logsub("Please click the [Delete Powermizer Settings] button, and then on [Create Powermizer Settings] button again. This should solve the trouble. In case it does not, please send debug info to devs through the [Problems?] button.");
                throw new Exception();
            }

            logsub("Success...");

            //Create the settings structure (convert from object to the enumtype)

            PwrMzrValue governor = (PwrMzrValue)Enum.ToObject(typeof(PwrMzrValue), gov);
            PwrMzrValue levelbatt = (PwrMzrValue)Enum.ToObject(typeof(PwrMzrValue), batfix);
            PwrMzrValue levelac = (PwrMzrValue)Enum.ToObject(typeof(PwrMzrValue), acfix);
            PwrMzrValue pm_enab;

         
            if (Convert.ToBoolean(enab))
            {
                pm_enab = PwrMzrValue.PM_ENABLED;
                logsub("Powermizer is ENABLED.");
            }
            else
            {
                pm_enab = PwrMzrValue.PM_DISABLED;
                logsub("Powermizer is DISABLED.");
            }

         
            logsub("Actual Governor: " + governor.ToString());
            logsub("Actual Fixed Batt Level: " + levelbatt.ToString());
            logsub("Actual Fixed AC Level: " + levelac.ToString());


            //This settings may not be there, so we may have a bunch if nulls. Careful!
            PwrMzrValue OHSDEnabled = PwrMzrValue.PM_DISABLED, OHSDMemory = PwrMzrValue.PM_DISABLED, OHSDCore = PwrMzrValue.PM_DISABLED;
            if (sdenab != null)
            {
                sdoverr = true;
                logsub("Overheat Slowdown settings are OVERRIDEN...");

                if (Convert.ToBoolean(sdenab))
                {
                    OHSDEnabled = PwrMzrValue.PM_ENABLED;
                    logsub("Overheat Slowdown is ENABLED.");
                }
                else
                {
                    OHSDEnabled = PwrMzrValue.PM_DISABLED;
                    logsub("Overheat Slowdown is DISABLED.");
                }

                if (Convert.ToBoolean(sdmem))
                {
                    OHSDMemory = PwrMzrValue.PM_ENABLED;
                    logsub("Overheat MEMORY Slowdown is ENABLED.");
                }
                else
                {
                    OHSDMemory = PwrMzrValue.PM_DISABLED;
                    logsub("Overheat MEMORY Slowdown is DISABLED.");
                }

                if (Convert.ToBoolean(sdcore))
                {
                    OHSDCore = PwrMzrValue.PM_ENABLED;
                    logsub("Overheat CORE Slowdown is ENABLED.");
                }
                else
                {
                    OHSDCore = PwrMzrValue.PM_DISABLED;
                    logsub("Overheat CORE Slowdown is DISABLED.");
                }
            }
            else
            {
                sdoverr = false;
                logsub("No Overheat Slowdown settings found...");
            }

           
           
        
           

            //Create the settings to return them
            PwrMzrSettings pmset;
            if (sdoverr) //IF we have detected overriden settings we need to take them into account
            {
                pmset = new PwrMzrSettings(pm_enab, governor, levelbatt, levelac, sdoverr, OHSDEnabled, OHSDMemory, OHSDCore);
            }
            else 
            {
                pmset = new PwrMzrSettings(pm_enab, governor, levelbatt, levelac);
            }

            return pmset;
            

        }

     
        //return the isntancePath and the GUID 
        public List<string> readInstantApplyInfo()
        {

            log("Looking for the video device instancePath and GUID device string...");

            List<string> ret = new List<string>();
            

            try
            {
                //Add in ret[0] the GUID string. This is hardcoded for each device type. Video is:
                string guidstr = "{4d36e968-e325-11ce-bfc1-08002be10318}";
                ret.Add(guidstr);

                //Read from the registry the instancePath
                RegistryKey instancePathKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\nvlddmkm\Enum");

                if (instancePathKey == null)
                {
                    throw (new Exception());
                }

                object path = this.regmgr.getDataValue(instancePathKey, "0");
                if (path == null)
                {
                    throw (new Exception());
                }

                // get this from the properties dialog box of this device in Device Manager
                // @"PCI\VEN_10DE&DEV_0407&SUBSYS_01211025&REV_A1\4&176ACB4A&0&0008";
                //ret[1]=instancePath
                ret.Add(path.ToString());

                
                //SLI SUPPORT
                if (Properties.Settings.Default.SLIModeEnabled == true)
                {
                    //I do not really know if SLI/Hybrid systems have 2 separate devices here.
                    //Just in case we look for it. If it's there, we get it and return it so we can reboot it later. If it's not, we do nothing.
                    object pathSLI = this.regmgr.getDataValue(instancePathKey, "1");
                    if (pathSLI != null)
                    {
                        ret.Add(pathSLI.ToString());
                    }
                }

            }
            catch
            {
                logsub("There was a problem while trying to retrieve the DeviceInstancePath from the registry.");
                throw;
            }

            logsub("Success...");

            return ret;


        }

        //Export the registry tree with root this.basekey to a file
        public void exportPowermizerSettings(string pathToFile)
        {
            try
            {
                log("Exporting registry settings...");
                this.regmgr.exportRegTreetoDisk(this.baseKey, pathToFile);
            }
            catch
            {
                throw;
            }

        }

        //import a file to the registry
        public void importPowermizerSettings(string pathToFile)
        {
            try
            {
                log("Importing registry settings...");
                this.regmgr.importRegTreefromDisk(pathToFile);
            }
            catch
            {
                throw;
            }


        }

        //Export the registry tree with root SYSTEM\CurrentControlSet\services\ to a file
        public void exportPciIDSettings(string pathToFile)
        {
            try
            {
                log("Exporting registry settings...");
                this.regmgr.exportRegTreetoDisk(Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services"), pathToFile);
            }
            catch
            {
                throw;
            }

        }


        #endregion

        #region /******* Iloggable Implementation *******/
        //write something to the log, and scroll it down.
        public void log(string message)
        {

            if (this._log_console == null) return;


            this._log_console.Text += "(PMZMGR) --> " + message + "\r\n";

            //Scroll the textbox  down
            this._log_console.Select(this._log_console.TextLength, 0);
            this._log_console.ScrollToCaret();



        }

        public void logsub(string message)
        {

            if (this._log_console == null) return;


            this._log_console.Text += "(PMZMGR)\t--> " + message + "\r\n";

            //Scroll the textbox  down
            this._log_console.Select(this._log_console.TextLength, 0);
            this._log_console.ScrollToCaret();



        }


        #endregion

    }
}