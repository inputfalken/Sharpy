using System;
using System.Linq;
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

        public Config Names(Func<NameFilter, NameFilter> func) {
            NameFilter = func(NameFilter);
            return this;
        }


        public Config Mail(string provider, params string[] providers) {
            MailGenerator = new MailGenerator(provider, providers);
            return this;
        }

        public Config CountryCode(string country) {
            PhoneNumberGenerator = DataCollections.CountryCodes.Value.First(generator => generator.Name == country);
            return this;
        }


        public Config UserNames(Func<StringFilter, StringFilter> func) {
            Usernames = func(Usernames);
            return this;
        }

        public Config(NameFilter nameFilter = null, StringFilter usernames = null,
            MailGenerator mailGenerator = null,
            PhoneNumberGenerator phoneNumberGenerator = null) {
            PhoneNumberGenerator = phoneNumberGenerator ?? DataCollections.CountryCodes.Value.RandomItem;
            NameFilter = nameFilter ?? DataCollections.Names.Value;
            Usernames = usernames ?? DataCollections.UserNames.Value;
            MailGenerator = mailGenerator ?? new MailGenerator("gmail.com", "hotmail.com", "yahoo.com");
        }
    }
}