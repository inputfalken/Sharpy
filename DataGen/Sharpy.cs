using System;
using DataGen.Types.CountryCode;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using static DataGen.DataCollections;

namespace DataGen {
    public static class Sharpy {
        private static readonly Config Config = new Config(Names.Value, UserNames.Value,
            new MailGenerator("gmail.com", "hotmail.com", "yahoo.com"),
            CountryCodes.Value.RandomItem);

        public static Func<T> CreateGenerator<T>(Action<T, Config> func) where T : new() =>
            CreateGenerator(func, Config);


        public static Func<T> CreateGenerator<T>(Action<T, Config> func, Config config) where T : new() => () => {
            var t = new T();
            func(t, config);
            return t;
        };

        public static T Generate<T>(Action<T, Config> func) where T : new() =>
            Generate(func, Config);

        public static T Generate<T>(Action<T, Config> func, Config config) where T : new() =>
            CreateGenerator(func, config)();
    }

    public class Config {
        private PhoneNumberGenerator PhoneNumberGenerator { get; }
        private NameFilter NameFilter { get; }
        private StringFilter Usernames { get; }
        private MailGenerator MailGenerator { get; }

        public Config(NameFilter nameFilter = null, StringFilter usernames = null, MailGenerator mailGenerator = null,
            PhoneNumberGenerator phoneNumberGenerator = null) {
            PhoneNumberGenerator = phoneNumberGenerator ?? CountryCodes.Value.RandomItem;
            NameFilter = nameFilter ?? Names.Value;
            Usernames = usernames ?? UserNames.Value;
            MailGenerator = mailGenerator ?? new MailGenerator("gmail.com", "hotmail.com", "yahoo.com");
        }

        public string Name => NameFilter.RandomItem.Data;
        public string UserName => Usernames.RandomItem;

        public string PhoneNumber(string preNumber = null, int length = 4) =>
            PhoneNumberGenerator.RandomNumber(length, preNumber);

        public string MailAdress(string name, string secondName = null) => secondName == null
            ? MailGenerator.Mail(name)
            : MailGenerator.Mail(name, secondName);
    }
}