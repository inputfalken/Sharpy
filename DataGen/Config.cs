using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types;
using DataGen.Types.CountryCode;
using DataGen.Types.Enums;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using static DataGen.Types.HelperClass;

namespace DataGen {
    //TODO make each Config contain an instance of Random which gets passed arround everywhere
    public class Config {
        internal PhoneNumberGenerator PhoneNumberGenerator { get; private set; }
        internal NameFilter NameFilter { get; private set; }
        internal StringFilter Usernames { get; private set; }
        internal MailGenerator MailGenerator { get; private set; }



        public Config NameOrigin(params Country[] countries) {
            NameFilter = NameFilter.ByCountry(countries);
            return this;
        }

        public Config NameOrigin(params Region[] regions) {
            NameFilter = NameFilter.ByRegion(regions);
            return this;
        }

        public Config Name(Func<IStringFilter<NameFilter>, NameFilter> func) {
            NameFilter = func(NameFilter);
            return this;
        }


        /// <summary>
        ///     Lets you change the providers for the mail addresses.
        ///     You can also a set a bool for wether the addreses will be unique.
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="uniqueAddresses">For Unique Addresses</param>
        /// <returns></returns>
        public Config Mail(IEnumerable<string> providers, bool uniqueAddresses = false) {
            MailGenerator = new MailGenerator(providers, uniqueAddresses);
            return this;
        }

        /// <summary>
        ///  Lets you change the country code
        /// </summary>
        /// <param name="country"></param>
        /// <param name="uniqueNumbers"></param>
        /// <returns></returns>
        public Config CountryCode(string country, bool uniqueNumbers = false) {
            PhoneNumberGenerator = DataCollections.CountryCodes.First(generator => generator.Name == country);
            PhoneNumberGenerator.Unique = uniqueNumbers;
            return this;
        }


        /// <summary>
        ///     Will let you filter the usernames result for the randomizer
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Config UserName(Func<StringFilter, StringFilter> func) {
            Usernames = func(Usernames);
            return this;
        }

        /// <summary>
        ///     NOTE: If you use this method it will set a seed for everything including future generators.
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Config Seed(int seed) {
            SetRandomizer(new Random(seed));
            return this;
        }

        public Config(NameFilter nameFilter = null, StringFilter usernames = null,
            MailGenerator mailGenerator = null,
            PhoneNumberGenerator phoneNumberGenerator = null) {
            PhoneNumberGenerator = phoneNumberGenerator ?? DataCollections.CountryCodes.RandomItem;
            NameFilter = nameFilter ?? DataCollections.Names;
            Usernames = usernames ?? DataCollections.UserNames;
            MailGenerator = mailGenerator ?? new MailGenerator(new[] { "gmail.com", "hotmail.com", "yahoo.com" }, false);
        }
    }
}