using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataGen.Types.Name {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Name {
        //Todo rework json file and this object

        public Name(int type, string country, string region, string name) {
            Country = country;
            Region = region;
            Data = name;
            Type = type;
        }

        public int Type { get; }
        public string Data { get; }
        public string Region { get; }
        public string Country { get; }
    }
}