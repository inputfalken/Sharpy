﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace DataGen.Types.NameCollection {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Name {
        public static Lazy<NameFilter> Filter
            => new Lazy<NameFilter>(() => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                File.ReadAllText("Data/Types/Name/newData.json"))));

        public static Lazy<UserNameFilter> UserNames
            => new Lazy<UserNameFilter>(() => new UserNameFilter(File.ReadAllLines("Data/Types/Name/usernames.txt")));

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
        public override string ToString() => Data;
    }
}