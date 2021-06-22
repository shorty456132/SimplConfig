using System;
using System.Text;
using System.Collections.Generic;
using Crestron.SimplSharp;      // For Basic SIMPL# Classes
using Crestron.SimplSharp.CrestronIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace ConsumeJson
{

    public class ConfigurationLoader
    {
        public static string FileLoc { get; set; }
        public static Configuration Config { get; set; }
        public static ushort AtcEnabled { get; set; }
        
        
        #region InitMethod
        public void InitMethod(string fileLoc)
        {
            if (File.Exists(fileLoc))
            {
                FileLoc = fileLoc;
                LoadConfigurationFile(FileLoc);
            }
            else
            {
                CrestronConsole.PrintLine("No Config File Loaded. Please load file to the correct file path.");
                using (var fileCreate = File.CreateText(fileLoc))
                {
                    try
                    {
                        string startConfig = "{\"Displays\": [{\"ID\": 1,\"Name\": \"Left Display\",\"Make\": \"Samsung\",\"Model\": \"LC-Series\",\"PowerON\": \"POWR   1\r\",\"PowerOFF\": \"POWR   0\r\",\"OutputNum\": 1}]}";

                        fileCreate.Write(JsonConvert.SerializeObject(startConfig, Formatting.Indented));
                        FileLoc = fileLoc;
                        LoadConfigurationFile(FileLoc);

                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                }
            }
        }

        public static void LoadConfigurationFile(string fileLoc)
        {
            getConfigurationFile(fileLoc);   
        }

        public static void getConfigurationFile(string fileLoc)
        {
            using (var fileReader = File.OpenText(fileLoc))
            {
                try
                {
                    Config = JsonConvert.DeserializeObject<Configuration>(fileReader.ReadToEnd());

                    DisplayConfiguration.updateSplusDisplayInfo();
                    SourceConfiguration.updateSPlusSources();
                }
                catch (Exception e)
                {
                    CrestronConsole.PrintLine(String.Format("fileReader Exception: {0}", e));
                    throw;
                }
            }
        }
        #endregion


        
        #region SaveFile
        public static void saveFile()
        {
            if (File.Exists(FileLoc))
            {
                File.Delete(FileLoc);
            }

            using (var fileWriter = File.CreateText(FileLoc))
            {
                try
                {
                    fileWriter.Write(JsonConvert.SerializeObject(Config, Formatting.Indented));
                }
                catch (Exception e)
                {
                    CrestronConsole.PrintLine(String.Format("FileWrite Exception: {0}", e));
                    throw;
                }
            }
        }
        #endregion


        #region Display Info
        public void getDisplay(int index)
        {
            DisplayConfiguration.updateDisplay(index);
        }

        public void SaveDisplay(ushort index, string _name, string _make, string _model, ushort _isProj, ushort _outputNum)
        {
            DisplayConfiguration.saveDisplay(index, _name, _make, _model, _isProj, _outputNum);
        }
        public void saveDisplayNumber(ushort numOfDisplays)
        {
            DisplayConfiguration.updateDisplayNumber(numOfDisplays);
        }
        #endregion

        #region Source Info
        public void getSource(int index)
        {
            SourceConfiguration.updateSource(index);
        }

        public void SaveSource(ushort index, string _sName, ushort _sInput, string _sSubPage)
        {
            SourceConfiguration.saveSource(index, _sName, _sInput, _sSubPage);
        }

        public void saveSourceNumber(ushort numOfSources)
        {
            SourceConfiguration.updateNumberOfSources(numOfSources);
        }

        public void getSubpage(ushort index)
        {
            SourceConfiguration.updateSubpage(index);
        }
        #endregion

        #region atc info
        public void getAtcInfo()
        {
            AtcConfiguration.updateAtcInfo();
        }
        public void saveAtcEnable(ushort atcActivity)
        {
            AtcConfiguration.updateAtcEnable(atcActivity);
        }
        public void SaveAtcInfo(string atcNumber)
        {
            AtcConfiguration.SaveATCNumber(atcNumber);
        }

        #endregion
    }

}
