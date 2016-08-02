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
            NameType = (NameType) type;
        }

        public NameType NameType { get; }
        public string Data { get; }
        public string Region { get; }
        public string Country { get; }
    }

    public enum NameType {
        Female = 1,
        Male = 2,
        LastName = 3
    }
}