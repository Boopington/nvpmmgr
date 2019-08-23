/*  ----------------------------------------------------------------------------
 *  Copyright Somemorebytes 2010
	DavidP.
	somemorebytes@gmail.com
 *  ----------------------------------------------------------------------------
 *  NVidia PowerMizer Manager
 *  ----------------------------------------------------------------------------
 *  File:       PwrMzrSettings.cs
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

namespace NVPMManager
{
    //This struct will encapsulate all the powermizer settings that can be read/written to the registry
    public struct PwrMzrSettings
    {
        //If disabled, no powermizer settings are effective.
        ///1-Enabled  0-Disabled
        public Entry PowerMizerEnabled;

        //Powermizer governor expressed in the form: 0xXXYY  
        /// XX==22-> Fixed Performance level on Battery
        /// XX==33-> Dynamic Performance level on Battery
        /// YY==22-> Fixed Performance level on AC
        /// YY==33-> Dynamic Performance level on AC
        public Entry PowerMizerGovernor;

        //Performace level on Battery (and on AC), when Fixed is selected
        /// 1- Maximun Performance, Minimum power saving
        /// 2- Medium Performance , Medium power saving
        /// 3- Minimum Performance, Maximum power saving
        public Entry PowerMizerBatteryFixedLevel;
        public Entry PowerMizerACFixedLevel;

        //Overheat slowdown override
        //Bool value indicating if the settings should be overriden or not.
        public bool OverheatSlowdownOverride;

        //Global Slowdown
        //1.-Enabled 0.-Disabled
        public Entry OverheatSlowdownEnabled;
       
        //Memory Slowdown
        //1.-Enabled 0.-Disabled
        public Entry OverheatSlowdownMemory;
        
        //GPU Core Slowdown
        //1.-Enabled 0.-Disabled
        public Entry OverheatSlowdownCore;


        //Main constructor
        public PwrMzrSettings(PwrMzrValue enabled, PwrMzrValue gov, PwrMzrValue batt_perf, PwrMzrValue ac_perf, bool OHSDOverride, PwrMzrValue OHSDEnabled, PwrMzrValue OHSDMemoryEnabled, PwrMzrValue OHSDCoreEnabled)
        {

            //Let's initialize the Entries with appropiate values

            if (enabled == PwrMzrValue.PM_DISABLED)
            {
                PowerMizerEnabled = new Entry("PowerMizerEnable", PwrMzrValue.PM_DISABLED, RegistryValueKind.DWord);
                PowerMizerGovernor = new Entry("PerfLevelSrc", PwrMzrValue.BAT_FIXED_AC_FIXED, RegistryValueKind.DWord);
            }
            else
            {
                PowerMizerEnabled = new Entry("PowerMizerEnable", PwrMzrValue.PM_ENABLED, RegistryValueKind.DWord);
                PowerMizerGovernor = new Entry("PerfLevelSrc", gov, RegistryValueKind.DWord);
            }


            PowerMizerBatteryFixedLevel = new Entry("PowerMizerLevel", batt_perf, RegistryValueKind.DWord);

            PowerMizerACFixedLevel = new Entry("PowerMizerLevelAC", ac_perf, RegistryValueKind.DWord);

            //Overheat Slowdown values
            OverheatSlowdownOverride = OHSDOverride;

            OverheatSlowdownEnabled = new Entry("EnableCoreSlowdown", OHSDEnabled, RegistryValueKind.DWord);
            OverheatSlowdownMemory = new Entry("EnableMClkSlowdown", OHSDMemoryEnabled, RegistryValueKind.DWord);
            OverheatSlowdownCore = new Entry("EnableNVClkSlowdown", OHSDCoreEnabled, RegistryValueKind.DWord);


        }

        //Ok, i'm tired of creating structs just to obtain the entry names or something , so this  are a couple of default contructors.
        //Not the cleanest thing ever, but it'll do the work:D
        public PwrMzrSettings(PwrMzrValue enabled, PwrMzrValue gov, PwrMzrValue batt_perf, PwrMzrValue ac_perf) : this(enabled, gov, batt_perf, ac_perf, false, PwrMzrValue.PM_DISABLED, PwrMzrValue.PM_DISABLED, PwrMzrValue.PM_DISABLED) { }

        public PwrMzrSettings(PwrMzrValue value) : this(PwrMzrValue.PM_DISABLED, PwrMzrValue.BAT_FIXED_AC_FIXED, PwrMzrValue.PM_DISABLED, PwrMzrValue.PM_DISABLED) { }
        
               

    }

    //One Registry entry, with its name, value, and type
    public struct Entry
    {
        public string EntryName;
        public PwrMzrValue EntryValue;
        public RegistryValueKind EntryType;

        public Entry(string name, PwrMzrValue value, RegistryValueKind type)
        {
            this.EntryName = name;
            this.EntryValue = value;
            this.EntryType = type;
        }
    }

    //Governor types and Performance level types
   public  enum PwrMzrValue {BAT_ON_AC_FIXED=0x3322, BAT_FIXED_AC_ON=0x2233, BAT_FIXED_AC_FIXED=0x2222, BAT_ON_AC_ON=0x3333, MAX_PERF = 1, MED_PERF = 2, MIN_PERF = 3, PM_ENABLED=1, PM_DISABLED=0 , DEFAULT=-1}

            
    
    
    

}