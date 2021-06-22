using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ConsumeJson
{
    public class SourceConfiguration
    {

        public static UpdateSourceNumberDelegate SourceNumber { get; set; }
        public static UpdateSourceIdDelegate SourceID { get; set; }
        public static UpdateSourceNameDelegate SourceName { get; set; }
        public static UpdateSOurceInputDelegate SourceInput { get; set; }
        public static UpdateSourceSubpageDelegate SourceSubpage { get; set; }
        public static ShowSetupDelegate ShowSetupPage { get; set; }

        public delegate void UpdateSourceNumberDelegate(ushort sourceNumber);
        public delegate void UpdateSourceIdDelegate(ushort sourceID);
        public delegate void UpdateSOurceInputDelegate(ushort sourceInput);
        public delegate void UpdateSourceNameDelegate(SimplSharpString sourceName);
        public delegate void UpdateSourceSubpageDelegate(SimplSharpString sourceSubpage);
        public delegate void ShowSetupDelegate(ushort showSetupPage);

#region update Number Of Sources
        public static void updateNumberOfSources(ushort sourceAmount)
        {
            ConfigurationLoader.Config.NumberOfSources = sourceAmount;
            ConfigurationLoader.saveFile();
            SourceNumber(ConfigurationLoader.Config.NumberOfSources);
        }
#endregion

#region update All Sources
        public static void updateSPlusSources()
        {
            SourceNumber(ConfigurationLoader.Config.NumberOfSources);
            for (var i = 0; i < ConfigurationLoader.Config.Sources.Count; i++)
            {
                SourceID(ConfigurationLoader.Config.Sources[i].sID);
                SourceName(ConfigurationLoader.Config.Sources[i].sName.ToString());
                SourceInput(ConfigurationLoader.Config.Sources[i].sInput);
                SourceSubpage(ConfigurationLoader.Config.Sources[i].subPage.ToString());

                if (ConfigurationLoader.Config.Sources[0].sInput == 0)      //new files begin with sources all at 0. so finding the 1st one at 0 says that its a new file.
                {
                    ShowSetupPage(1);
                }
                else
                {
                    ShowSetupPage(0);
                }
            }
        }
#endregion

#region update Source At Index...
        public static void updateSource(int index)
        {
            SourceID(ConfigurationLoader.Config.Sources[index].sID);
            SourceName(ConfigurationLoader.Config.Sources[index].sName.ToString());
            SourceInput(ConfigurationLoader.Config.Sources[index].sInput);
            SourceSubpage(ConfigurationLoader.Config.Sources[index].subPage.ToString());
        }
#endregion

#region Save Source At Index...
        public static void saveSource(ushort index, string newName, ushort newInput, string newSubpage)
        {
            ConfigurationLoader.Config.Sources[index].sName = newName;
            ConfigurationLoader.Config.Sources[index].sInput = newInput;
            ConfigurationLoader.Config.Sources[index].subPage = newSubpage;
            ConfigurationLoader.Config.Sources[index].sID = index;

            ConfigurationLoader.saveFile();
            updateSource(index);
        }
#endregion

        public static void updateSubpage(ushort index)
        { 
            SourceSubpage(ConfigurationLoader.Config.Sources[index].subPage.ToString());
        }
    }
}