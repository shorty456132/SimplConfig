using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ConsumeJson
{
    public class Display
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public bool IsProj { get; set; }
        public ushort OutputNum { get; set; }
    }

    public class Source
    {
        public ushort sID { get; set; }
        public string sName { get; set; }
        public ushort sInput { get; set; }
        public string subPage { get; set; }
    }

    public class ATC
    {
        public string atcNumber { get; set; }
        public string atcDeviceMake { get; set; }
        public string atcDeviceModel { get; set; }
    }


    //Main class
    public class Configuration
    {
        public List<Display> Displays { get; set; }
        public List<Source> Sources { get; set; }
        public List<ATC> atc { get; set; }
        public ushort NumberOfDisplays { get; set; }
        public ushort NumberOfSources { get; set; }
        public bool HasATC { get; set; }
        public bool HasVTC { get; set; }
        public bool VoIPEnable { get; set; }

        public Configuration()
        {
            Displays = new List<Display>();
            Sources = new List<Source>();
            atc = new List<ATC>();
        }

    }
}