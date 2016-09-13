using System;
using Sharpy.Types.Enums;

namespace Sharpy.Types.Name {
    // Is generated from json
    // ReSharper disable once ClassNeverInstantiated.Global
    /// <summary>
    /// 
    /// </summary>
    public sealed class Name {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="country"></param>
        /// <param name="region"></param>
        /// <param name="name"></param>
        public Name(int type, string country, string region, string name) {
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