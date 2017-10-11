using System;
using Newtonsoft.Json;
using Sharpy.Builder.Enums;

namespace Sharpy.Builder.Implementation.DataObjects {
    /// <summary>
    ///     The data structure used by <see cref="NameByOrigin" />.
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
        ///     Returns the name.
        /// </summary>
        public override string ToString() => Data;
    }
}