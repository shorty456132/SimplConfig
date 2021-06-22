using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ConsumeJson
{
    public class DisplayConfiguration
    {
        public static UpdateDisplayIdDelegate DisplayID { get; set; }
        public static UpdateDisplayNameDelegate DisplayName { get; set; }
        public static UpdateDisplayMakeDelegate DisplayMake { get; set; }
        public static UpdateDisplayModelDelegate DisplayModel { get; set; }
        public static UpdateDisplayTypeDelegate DisplayIsProj { get; set; }
        public static UpdateDisplayOutputNumDelegate DisplayOutputNum { get; set; }
        public static UpdateDisplayNumberDelegate DisplayNumber { get; set; }

        public delegate void UpdateDisplayNumberDelegate(ushort displayNumber);
        public delegate void UpdateDisplayIdDelegate(SimplSharpString displayID);
        public delegate void UpdateDisplayNameDelegate(SimplSharpString displayName);
        public delegate void UpdateDisplayMakeDelegate(SimplSharpString displayMake);
        public delegate void UpdateDisplayModelDelegate(SimplSharpString displayModel);
        public delegate void UpdateDisplayTypeDelegate(ushort displayIsProj);
        public delegate void UpdateDisplayOutputNumDelegate(ushort displayOutputNum);


#region update Number Of Displays
        public static void updateDisplayNumber(ushort displayAmount)
        {
            ConfigurationLoader.Config.NumberOfDisplays = displayAmount;
            ConfigurationLoader.saveFile();
            DisplayNumber(ConfigurationLoader.Config.NumberOfDisplays);
        }
#endregion

#region update all displays info
        public static void updateSplusDisplayInfo()
        {
            CrestronConsole.PrintLine("Getting Display Info");

            DisplayNumber(ConfigurationLoader.Config.NumberOfDisplays);
            for (var i = 0; i < ConfigurationLoader.Config.Displays.Count; i++)
            {
                DisplayID(ConfigurationLoader.Config.Displays[i].ID.ToString());
                DisplayName(ConfigurationLoader.Config.Displays[i].Name.ToString());
                DisplayMake(ConfigurationLoader.Config.Displays[i].Make.ToString());
                DisplayModel(ConfigurationLoader.Config.Displays[i].Model.ToString());
                DisplayIsProj(ConfigurationLoader.Config.Displays[i].IsProj ? (ushort)1 : (ushort)0);
                DisplayOutputNum(ConfigurationLoader.Config.Displays[i].OutputNum);
            }
        }
#endregion

#region update Display At index...
        public static void updateDisplay(int index)
        {
            DisplayID(ConfigurationLoader.Config.Displays[index].ID.ToString());
            DisplayName(ConfigurationLoader.Config.Displays[index].Name.ToString());
            DisplayMake(ConfigurationLoader.Config.Displays[index].Make.ToString());
            DisplayModel(ConfigurationLoader.Config.Displays[index].Model.ToString());
            DisplayIsProj(ConfigurationLoader.Config.Displays[index].IsProj ? (ushort)1: (ushort)0);
            DisplayOutputNum(ConfigurationLoader.Config.Displays[index].OutputNum);
        }
#endregion

#region SaveDisplay At Index...
        //save selected display info

        public static void saveDisplay(ushort index, string newName, string newModel, string newMake, ushort newIsProj, ushort newOutputNum)
        {
            ConfigurationLoader.Config.Displays[index + 1].ID = index;
            ConfigurationLoader.Config.Displays[index].Name = newName;
            ConfigurationLoader.Config.Displays[index].Make = newMake;
            ConfigurationLoader.Config.Displays[index].Model = newModel;
            ConfigurationLoader.Config.Displays[index].OutputNum = newOutputNum;

            if (newIsProj == 1)
                ConfigurationLoader.Config.Displays[index].IsProj = true;
            else if(newIsProj == 0)
                ConfigurationLoader.Config.Displays[index].IsProj = false;

            ConfigurationLoader.saveFile();
            updateDisplay(index);
        }
#endregion
    }
}