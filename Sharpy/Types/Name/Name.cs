using System;
using Newtonsoft.Json;
using Sharpy.Enums;

namespace Sharpy.Types.Name {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    /// <summary>
    /// 
    /// </summary>
    internal sealed class Name {
        [JsonConstructor]
        internal Name(int type, string country, string region, string name) {
            Data = name;
            Type = type;
            Country = (Country) Enum.Parse(typeof(Country), country);
            Region = (Region) Enum.Parse(typeof(Region), region);
        }

        internal int Type { get; }
        internal string Data { get; }
        internal Region Region { get; }
        internal Country Country { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Data;
    }
}