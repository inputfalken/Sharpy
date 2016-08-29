using System;
using System.Linq;
using DataGen.Types;
using DataGen.Types.CountryCode;
using DataGen.Types.Date;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using NodaTime;
using static DataGen.DataCollections;

namespace DataGen {
    public static class Sharpy {
        private static readonly Fetcher DefaultFetcher = new Fetcher(Names.Value, UserNames.Value,
            new MailGenerator("gmail.com", "hotmail.com", "yahoo.com"), CountryCodes.Value.RandomItem);

        public static Func<T> CreateGenerator<T>(Action<T, Fetcher> action, Fetcher fetcher)
            where T : new() => () => {
            var t = new T();
            action(t, fetcher);
            return t;
        };

        public static Func<T> CreateGenerator<T>(Action<T, Fetcher> action) where T : new() =>
            CreateGenerator(action, DefaultFetcher);

        public static Func<T> CreateGenerator<T>(Func<Fetcher, T> func) => ()
            => func(DefaultFetcher);

        public static Func<T> CreateGenerator<T>(Func<Fetcher, T> func, Fetcher fetcher)
            => () => func(fetcher);

        public static T Generate<T>(Func<Fetcher, T> func, Fetcher fetcher) => func(fetcher);

        public static T Generate<T>(Func<Fetcher, T> func) => func(DefaultFetcher);

        public static T Generate<T>(Action<T, Fetcher> action) where T : new() =>
            Generate(action, DefaultFetcher);

        public static T Generate<T>(Action<T, Fetcher> action, Fetcher fetcher) where T : new() =>
            CreateGenerator(action, fetcher)();
    }

    public class Fetcher {
        private PhoneNumberGenerator PhoneNumberGenerator { get; }
        private NameFilter NameFilter { get; }
        private StringFilter Usernames { get; }
        private MailGenerator MailGenerator { get; }

        public Fetcher(NameFilter nameFilter = null, StringFilter usernames = null,
            MailGenerator mailGenerator = null,
            PhoneNumberGenerator phoneNumberGenerator = null) {
            PhoneNumberGenerator = phoneNumberGenerator ?? CountryCodes.Value.RandomItem;
            NameFilter = nameFilter ?? Names.Value;
            Usernames = usernames ?? UserNames.Value;
            MailGenerator = mailGenerator ?? new MailGenerator("gmail.com", "hotmail.com", "yahoo.com");
        }

        public string Name => NameFilter.RandomItem.Data;
        public string UserName => Usernames.RandomItem;
        public bool Bool => Number(2) != 0;
        public int Number(int maxNum) => HelperClass.Randomizer(maxNum);
        public int Number(int minNum, int maxNum) => HelperClass.Randomizer(minNum, maxNum);
        public LocalDate DateByAge(int age) => DateGenerator.RandomDateByAge(age);
        public LocalDate DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        public string PhoneNumber(string preNumber = null, int length = 4) =>
            PhoneNumberGenerator.RandomNumber(length, preNumber);

        public string MailAdress(string name, string secondName = null) => secondName == null
            ? MailGenerator.Mail(name)
            : MailGenerator.Mail(name, secondName);
    }
}