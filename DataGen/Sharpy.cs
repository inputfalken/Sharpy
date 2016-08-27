using System;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using static DataGen.DataCollections;

namespace DataGen {
    public static class Sharpy {
        public static Func<T> CreateGenerator<T>(Action<T, Config> func) where T : new() =>
            CreateGenerator(func, new Config(Names.Value, UserNames.Value, new MailGenerator()));


        public static Func<T> CreateGenerator<T>(Action<T, Config> func, Config config) where T : new() => () => {
            var t = new T();
            func(t, config);
            return t;
        };

        public static T Generate<T>(Action<T, Config> func) where T : new() =>
            Generate(func, new Config(Names.Value, UserNames.Value, new MailGenerator()));

        public static T Generate<T>(Action<T, Config> func, Config config) where T : new() =>
            CreateGenerator(func, config)();
    }

    public class Config {
        private NameFilter NameFilter { get; }
        private StringFilter Usernames { get; }
        private MailGenerator MailGenerator { get; }

        public Config(NameFilter nameFilter, StringFilter usernames, MailGenerator mailGenerator) {
            NameFilter = nameFilter;
            Usernames = usernames;
            MailGenerator = mailGenerator;
        }

        public string RandomName => NameFilter.RandomItem.Data;
        public string UserName => Usernames.RandomItem;
        public Func<string, string, string> MailAddress => MailGenerator.Mail;
    }
}