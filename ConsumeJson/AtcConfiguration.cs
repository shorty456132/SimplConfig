using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ConsumeJson
{
    public class AtcConfiguration
    {
        public static ATCNumberDelegate AtcNumber { get; set; }
        public static ATCVoipDelegate AtcVoIP { get; set; }
        public static ATCMakeDelegate AtcMake { get; set; }
        public static ATCModelDelegate AtcModel { get; set; }
        public static ATCActiveDelegate AtcActive { get; set; }

        public delegate void ATCActiveDelegate(ushort atcActive);
        public delegate void ATCNumberDelegate(SimplSharpString atcNumber);
        public delegate void ATCMakeDelegate(SimplSharpString atcMake);
        public delegate void ATCModelDelegate(SimplSharpString atcModel);
        public delegate void ATCVoipDelegate(bool atcVoIP);


        public static void updateAtcEnable(ushort state)
        {
            CrestronConsole.PrintLine(String.Format("atc state = {0}", state));

            if (state == 1)
            {
                ConfigurationLoader.Config.HasATC = true; 
                ConfigurationLoader.saveFile();
            }
            if (state == 0)
            {
                ConfigurationLoader.Config.HasATC = false;
                ConfigurationLoader.saveFile();
            }

            CrestronConsole.PrintLine(String.Format("hasATC = {0}", ConfigurationLoader.Config.HasATC ? true : false));
            AtcActive(ConfigurationLoader.Config.HasATC ? (ushort)1 : (ushort)0);
        }

        public static void updateAtcInfo()
        {
            AtcNumber(ConfigurationLoader.Config.atc[0].atcNumber.ToString());
            CrestronConsole.PrintLine(String.Format("atcNumber = {0}", ConfigurationLoader.Config.atc[0].atcNumber.ToString()));

            AtcActive(ConfigurationLoader.Config.HasATC ? (ushort)1:(ushort)0);

            if (ConfigurationLoader.Config.HasATC == true)
            {
                AtcVoIP(ConfigurationLoader.Config.VoIPEnable);

            }
            else 
            {
                CrestronConsole.PrintLine("ATC Is Not Activated");
            }


        }

        public static void SaveATCNumber(string _atcNumber)
        {
            ConfigurationLoader.Config.atc[0].atcNumber = _atcNumber;
            ConfigurationLoader.saveFile();
            updateAtcInfo();
        }
    }
}