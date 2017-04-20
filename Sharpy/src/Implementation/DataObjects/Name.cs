using System;
using Newtonsoft.Json;
using Sharpy.Enums;

namespace Sharpy.Implementation.DataObjects {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    /// <summary>
    /// </summary>
    internal sealed class Name {
        [JsonConstructor]
        private Name(int type, string country, string region, string name) {
            Data = name;
            Type = (NameType) type;
            Country = (Origin) Enum.Parse(typeof(Origin), country);
            Region = (Origin) Enum.Parse(typeof(Origin), region);
        }

        internal NameType Type { get; }
        internal string Data { get; }
        internal Origin Region { get; }
        internal Origin Country { get; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Data;
        }
    }
}