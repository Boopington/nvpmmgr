/*  ----------------------------------------------------------------------------
 *  Copyright Somemorebytes 2010
	DavidP.
	somemorebytes@gmail.com
 *  ----------------------------------------------------------------------------
 *  NVidia PowerMizer Manager
 *  ----------------------------------------------------------------------------
 *  File:       RegistryManager.cs
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
using System.IO;
using System.Security;
using System.Diagnostics;


namespace NVPMManager
{
    
    //This class just place the calls to the methods of RegistryKey. The framework already provides a simple enough to manage the keys.
    //I made this class just to add full exception control here and logging, and a couple of functions. However, the exceptions are thrown up to the
    //caller once logged, so the caller can terminate the program if needed. Caller won't need to take care of the exception type though.

    class RegistryManager : ILoggable
    {
        
     
        //ILoggable implementation
        //logging device
        private TextBox _log_console;
        public TextBox Log_Console { set { this._log_console = value; } }
        
     
        public RegistryManager()
        {
        }


        #region     /******* Key Management *******/



        //Extract a key from the registry. Root of the search is parentKey
        public RegistryKey getRegistryKey(RegistryKey parentKey, string name)
        {
            RegistryKey aux;

            log("Retrieving key: " + parentKey.Name + "\\" + name +".");

            try
            {
                aux = parentKey.OpenSubKey(name,true);
            }
            catch(ArgumentNullException w)
            {
                logsub("Key name is null.");
                throw (w);
            }
            catch (ArgumentException w)
            {
                logsub("Key name is too long.");
                throw (w);
                
            }
            catch (ObjectDisposedException w)
            {
                logsub("Key is closed so it can't be accessed.");
                throw (w);
            }
            catch (SecurityException w)
            {
                logsub("User has not enough privileges to access this key.");
                throw (w);
            }
            

            if (aux == null)
            {
                logsub("subKey not found.");
                return null;
            }
            else
            {
                logsub("Success...");
                return aux;
            }
                

            

        }

        //Insert a key hanging from parentKey. 
        public void setRegistryKey(RegistryKey parentKey, string name)
        {
            log("Creating subkey: " + parentKey.Name + "\\" + name +".");

            try
            {
                parentKey.CreateSubKey(name);
                logsub("Success...");
            }
            catch (ArgumentNullException w)
            {
                logsub("Key name is null.");
                throw (w);
            }
            catch (ArgumentException w)
            {
                logsub("Key name is too long.");
                throw (w);
            }
            catch (ObjectDisposedException w)
            {
                logsub("Key is closed so it can't be accessed.");
                throw (w);
            }
            catch (SecurityException w)
            {
                logsub("User has not enough privileges to access this key.");
                throw (w);
            }
            catch (UnauthorizedAccessException w)
            {
                logsub("The key cannot be written.Maybe it has been open as read-only.");
                throw (w);
            }

            
        }

     

        //Deletes a key. Or at least tries to.
        public void deleteKey(RegistryKey parentKey, string keytodelete)
        {
            log("Trying to delete key: " + parentKey.Name + ".");

            try
            {
                parentKey.DeleteSubKey(keytodelete);
                logsub("Success...");
            }
            catch (ObjectDisposedException w)
            {
                logsub("Key is closed so it can't be accessed.");
                throw (w);
            }
            catch (InvalidOperationException w)
            {
                logsub("The key cannot be deleted because it's not empty.");
                throw (w);
            }
            catch (ArgumentNullException w)
            {
                logsub("Key name is null.");
                throw (w);
            }
            catch (ArgumentException w)
            {
                logsub("Key name is too long.");
                throw (w);

            }
            catch (SecurityException w)
            {
                logsub("User has not enough privileges to access this key.");
                throw (w);
            }


        }

        //Calls the flush method, to make the write effective.
        public void PersistKeyChanges(RegistryKey parentKey)
        {
            log("Flushing contents of key: " + parentKey.Name +".");
            //parentKey.Close();
            ///When the key is closed, it is automatically flushed.
             parentKey.Flush();
        }


        //Reopen an ALREADY OPENED key with write permissions
        public RegistryKey reOpenKeyWriteAccess(RegistryKey key)
        {
            string name = key.Name;
            int first = name.IndexOf(@"\");
            int last = name.LastIndexOf(@"\");

           // string child = name.Substring(last + 1);
           // string parent = name.Substring(first + 1, name.Length - child.Length - first);
            string root = key.Name.Substring(0, first);
            string strip = name.Substring(first + 1, name.Length - first - 1);

            RegistryKey result=null;

            log("Reopening key for write access: " + key.Name);

            try
            {
                switch (root)
                {
                    case "HKEY_LOCAL_MACHINE":
                        result = Registry.LocalMachine.OpenSubKey(strip, RegistryKeyPermissionCheck.ReadWriteSubTree);
                        break;

                    //TODO: Add all possible roots
                }
            }
            catch(Exception w)
            {
                logsub("Error!. Can't open key. Original error: " + w.Message);
                throw(w);
            }

            logsub("Success...");
            return result;

        }

        //Recursive.With pattern = "" Returns a list with all the keys using parentKey as root.
        //With pattern != "", returns a list of all the keys with name pattern
        public List<RegistryKey> GetSubKeysRecursive(RegistryKey parentKey, string pattern)
        {

            List<RegistryKey> childKeysList = null;
            bool match = false;
            
            if (pattern == "")
            {
                log("Retrieving all the keys for subtree: " + parentKey.Name + ".");
            }
            else
            {
                match = true;
                log("Retrieving all the keys matching \\" + pattern + "\\ from key: " + parentKey.Name + ".");
            }

            try
            {
                //If there are no subkeys, return null;
                if (parentKey.SubKeyCount > 0)
                {
                    //List to add keys
                    childKeysList = new List<RegistryKey>();
                }
                else
                {
                    return null;
                }


                //Get all the keys of one level
                foreach (string childName in parentKey.GetSubKeyNames())
                {
                    try
                    {
                        //Take the key, and insert on the list (For some reason, the recursive function crashes if we open recursively with write access)
                        RegistryKey childKey = parentKey.OpenSubKey(childName);

                        //Only add the key, if no pattern is give, so all keys will be added, or if the pattern matches the name otherwise.
                        if ((match == false) || (childName == pattern))
                        {
                            childKeysList.Add(childKey);
                        }

                        List<RegistryKey> grandchildrenKeysList = new List<RegistryKey>();

                        //Recursive call
                        grandchildrenKeysList = GetSubKeysRecursive(childKey, pattern);

                        //If there were grandchildrens, add them to the list
                        if ((grandchildrenKeysList != null) && (grandchildrenKeysList.Count > 0))
                        {
                            childKeysList.AddRange(grandchildrenKeysList);
                        }
                    }
                    catch (Exception w)
                    {
                        logsub("Unexpected problem while scanning the child key: " + childName + " inside " + parentKey.Name + ".");
                        log(w.Message);
                        throw (w);
                    }

                }
            }
            catch(Exception w)
            {
                logsub("Unexpected problem while scanning: " + parentKey.Name + ".");
                log(w.Message);
                throw (w);
            }

            return childKeysList;

        }

        #endregion

        #region   /******* Name/Value Management *******/


        //Gets the data of the value valuetofind from a key. The caller should know what type the value is, to make the right cast!
        //If needed, use RegistryKey.GetValueKind();
        public object getDataValue(RegistryKey parentKey, string valuetofind)
        {
            Object aux;

            log("Retrieving value <" + valuetofind + "> from key " + parentKey.Name + ".");

            try
            {
                aux = parentKey.GetValue(valuetofind);
            }
            catch (SecurityException w)
            {
                logsub("User has not enough privileges to access this key.");
                throw (w);
            }
            catch (UnauthorizedAccessException w)
            {
                logsub("The key cannot be written.Maybe it has been open as read-only.");
                throw (w);
            }
            catch (ObjectDisposedException w)
            {
                logsub("Key is closed so it can't be accessed.");
                throw (w);
            }
            catch (IOException w)
            {
                logsub("The key has been marked to delete");
                throw (w);
            }


            if (aux == null)
            {
                logsub("Value not found.");
                return null;
            }
            else
            {
                logsub("Success...");
                return aux;
            }

        }

        //Sets the data of the value nametofind, with type type.
        public void setValue(RegistryKey parentKey, string nametofind, object data, RegistryValueKind type)
        {
            log("Setting up data [" + data.ToString() + "] with name <" + type.ToString() +"::" + nametofind + "> under key " + parentKey.Name + ".");

            try
            {
                parentKey.SetValue(nametofind, data, type);
                logsub("Success...");
            }
            catch (ArgumentNullException w)
            {
                logsub("Data to set is null.");
                throw (w);
            }
            catch (ArgumentException w)
            {
                logsub("Value name is too long.");
                throw (w);

            }
            catch (ObjectDisposedException w)
            {
                logsub("Key is closed so it can't be accessed.");
                throw (w);
            }
            catch (SecurityException w)
            {
                logsub("User has not enough privileges to access this key.");
                throw (w);
            }
            catch (UnauthorizedAccessException w)
            {
                logsub("The key cannot be written.Maybe it has been open as read-only.");
                throw (w);
            }
            catch (IOException w)
            {
                ///check http://msdn.microsoft.com/en-us/library/k23f0345.aspx
                logsub("The key is a root-level node, and you're nobody to do this(kidding). This operation is not permitted in your O.S so I throw this error.");
                throw (w);
            }
            

        }

        //Deletes a pair Name/Value with name valueToDelete from key parentKey
        public void deleteValue(RegistryKey parentKey, string valueToDelete)
        {
            log("Delete value [" + valueToDelete+ "] under key " + parentKey.Name + ".");

            try
            {
                parentKey.DeleteValue(valueToDelete,false); //Added the bool parameter. We do not want to raise an exception if the value is not found!
                logsub("Success...");
            }
            
                //This exception will never be raised, because of the bool parameter set up to false in the DeleteValue() Method.
                //It is just silly. We are trying to delete an estry does already does not exists. However, the rest of the exceptions will be raised.
            catch (ArgumentException w)
            {
                logsub("Value name is not valid.");
                throw (w);

            }
            catch (ObjectDisposedException w)
            {
                logsub("Key is closed so it can't be accessed.");
                throw (w);
            }
            catch (SecurityException w)
            {
                logsub("User has not enough privileges to access this key.");
                throw (w);
            }
            catch (UnauthorizedAccessException w)
            {
                logsub("The key cannot be written.Maybe it has been open as read-only.");
                throw (w);
            }
            
            
        }
        

        //Sets the data of the value nametofind
        /// Simpler version
        //public void setValue(RegistryKey parentKey, string nametoset, object data);



        #endregion
        #region /******* Export / Import Registry backups *******/

        ///The problem with recursing over the registry to save the keys and values one at a time, is that the types provided by the
        ///RegistryValueKind enum  are not explicits enough. For example, if we write in a file RegistryValueKind.MultiString.ToString()
        ///and then the value, we will obtain something like MultiString:"blah blah blah", while in a proper registry export we SHOULD HAVE
        /// hex(7):"blah blah blah". So we should include here a lot of pInvoke() calls to the win32 API to obtain the functions that could
        /// identify those types to write them to a file.
        /// SOLUTION: Use the regedit executable, which does this better than me, and it's present in every windows machine.
        /// 
        ///regedit syntax is in the form:
        ///TO EXPORT: regedit.exe /E filetowrite.reg "ROOT_KEY_TO_EXPORT"
        ///TO IMPORT: regedit.exe /I /S file.reg
        

        //Saves a registry tree with root  rootkey to disk
        public void exportRegTreetoDisk(RegistryKey rootkey, string filePath)
        {

            //Construct arguments string
            string arguments = "/E " + filePath +  " \"" + rootkey.ToString() + "\" ";

            //Create the process
            Process prexport = new Process();
            prexport.StartInfo.FileName = "regedit.exe";
            prexport.StartInfo.UseShellExecute = false;
            prexport.StartInfo.Arguments = arguments;

            //Launch the process
            try
            {
                log("Backing up registry subtree: " + "\"" + rootkey.ToString() + "\" -----> " + filePath);
                prexport.Start();
                prexport.WaitForExit();
            }
            catch(Exception w)
            {
                logsub("Unexpected error while backing up the registry.");
                log(w.Message);
                throw (w);
            }
            finally
            {
                prexport.Dispose();
            }

            logsub("Success...");

        }

        //Imports a registry tree from a file
        public void importRegTreefromDisk(string filePath)
        {
            //Construct arguments string
            string arguments = "/I /S " + filePath;

            //Create the process
            Process prexport = new Process();
            prexport.StartInfo.FileName = "regedit.exe";
            prexport.StartInfo.UseShellExecute = false;
            prexport.StartInfo.Arguments = arguments;

            //Launch the process
            try
            {
                log("Importing registry subtree from file: " + filePath);
                prexport.Start();
                prexport.WaitForExit();
            }
            catch (Exception w)
            {
                logsub("Unexpected error while importing the registry backup file.");
                log(w.Message);
                throw (w);
            }
            finally
            {
                prexport.Dispose();
            }

            logsub("Success...");

        }

        #endregion

        #region /******* Iloggable Implementation *******/
        //write something to the log, and scroll it down.
        public void log(string message)
        {
            
            if (this._log_console == null) return;
            

            this._log_console.Text += "(REGMGR) --> " + message + "\r\n";

            //Scroll the textbox  down
            this._log_console.Select(this._log_console.TextLength, 0);
            this._log_console.ScrollToCaret();



        }

        public void logsub(string message)
        {

            if (this._log_console == null) return;


            this._log_console.Text += "(REGMGR)\t--> " + message + "\r\n";

            //Scroll the textbox  down
            this._log_console.Select(this._log_console.TextLength, 0);
            this._log_console.ScrollToCaret();



        }


        #endregion


        

       
    }
}