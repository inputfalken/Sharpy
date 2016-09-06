using System;
using System.Collections.Generic;
using System.Text;
using DataGen.Types.CountryCode;
using DataGen.Types.Name;
using DataGen.Types.String;
using Newtonsoft.Json;

namespace DataGen {
    public static class DataCollections {
        static DataCollections() {
            Names = new NameFilter(new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Properties.Resources.NamesByOrigin))));
            UserNames = new StringFilter(Properties.Resources.usernames.Split(Convert.ToChar("\n")));
            CountryCodes = new CountryCodeFilter(JsonConvert.DeserializeObject<IEnumerable<PhoneNumberGenerator>>(
                Encoding.Default.GetString(Properties.Resources.CountryCodes)));
        }

        /// <summary>
        ///     This collection of objects contains alot of common names.
        ///     which can be filtered by Region, Country, NameType(female,male,lastname)
        /// </summary>
        public static NameFilter Names { get; }


        /// <summary>
        ///     This collection of strings contains alot of random usernames.
        ///     Which can be filtered by string index.
        /// </summary>
        public static StringFilter UserNames { get; }

        /// <summary>
        ///     This collection of objects contains all Country Codes & Country names.
        ///     Which can be filtered by Country name.
        ///     Each Object got methods that can randomize phone numbers by taking country code to consideration.
        /// </summary>
        public static CountryCodeFilter CountryCodes { get; }
    }
}