﻿using System.Collections.Generic;
using System.IO;
using DataGen.Types.CountryCode;
using DataGen.Types.Name;
using DataGen.Types.String;
using Newtonsoft.Json;

namespace DataGen {
    public static class DataCollections {
        /// <summary>
        ///     This collection of objects contains alot of common names.
        ///     which can be filtered by Region, Country, NameType(female,male,lastname)
        /// </summary>
        public static readonly NameFilter Names =
            new NameFilter(new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                File.ReadAllText("Data/Types/Name/NamesByOrigin.json"))));

        /// <summary>
        ///     This collection of strings contains alot of random usernames.
        ///     Which can be filtered by string index.
        /// </summary>
        public static readonly StringFilter UserNames =
            new StringFilter(File.ReadAllLines("Data/Types/Name/usernames.txt"));


        /// <summary>
        ///     This collection of objects contains all Country Codes & Country names.
        ///     Which can be filtered by Country name.
        ///     Each Object got methods that can randomize phone numbers by taking country code to consideration.
        /// </summary>
        public static readonly CountryCodeFilter CountryCodes =
            new CountryCodeFilter(JsonConvert.DeserializeObject<IEnumerable<PhoneNumberGenerator>>(
                File.ReadAllText("Data/Types/CountryCodes/CountryCodes.json")));
    }
}