using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types;
using DataGen.Types.CountryCode;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;

namespace DataGen {
    public class Config {
        internal PhoneNumberGenerator PhoneNumberGenerator { get; private set; }
        internal NameFilter NameFilter { get; private set; }
        internal StringFilter Usernames { get; private set; }
        internal MailGenerator MailGenerator { get; private set; }
        private int RandomizerSeed { get; set; }

        /// <summary>
        ///     Will filter the result for the randomizer's name
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Config Names(Func<NameFilter, NameFilter> func) {
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
        /// <returns></returns>
        public Config CountryCode(string country) {
            PhoneNumberGenerator = DataCollections.CountryCodes.Value.First(generator => generator.Name == country);
            return this;
        }


        /// <summary>
        ///     Will let you filter the usernames result for the randomizer
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Config UserNames(Func<StringFilter, StringFilter> func) {
            Usernames = func(Usernames);
            return this;
        }

        /// <summary>
        ///     If you use this method it will set a seed for everything including future generators.
        ///     TODO: make seed only manipulate the generator instance.
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Config Seed(int seed) {
            HelperClass.Random = new Random(seed);
            return this;
        }

        public Config(NameFilter nameFilter = null, StringFilter usernames = null,
            MailGenerator mailGenerator = null,
            PhoneNumberGenerator phoneNumberGenerator = null) {
            PhoneNumberGenerator = phoneNumberGenerator ?? DataCollections.CountryCodes.Value.RandomItem;
            NameFilter = nameFilter ?? DataCollections.Names.Value;
            Usernames = usernames ?? DataCollections.UserNames.Value;
            MailGenerator = mailGenerator ?? new MailGenerator(new[] { "gmail.com", "hotmail.com", "yahoo.com" }, false);
        }
    }
}