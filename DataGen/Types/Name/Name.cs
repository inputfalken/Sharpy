using System;
using DataGen.Types.Enums;

namespace DataGen.Types.Name {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Name {
        public Name(int type, string country, string region, string name) {
            Data = name;
            Type = type;
            Country = (Country) Enum.Parse(typeof(Country), country);
            Region = (Region) Enum.Parse(typeof(Region), region);
        }

        public int Type { get; }
        public string Data { get; }
        public Region Region { get; }
        public Country Country { get; }
        public override string ToString() => Data;
    }
}