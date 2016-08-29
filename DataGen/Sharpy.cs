using System;
using System.Linq;
using DataGen.Types.Mail;
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
}