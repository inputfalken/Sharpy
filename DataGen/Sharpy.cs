using System;
using DataGen.Types.Mail;
using static DataGen.DataCollections;

namespace DataGen {
    public static class Sharpy {
        public static T Map<T>(Action<T, Config> func) where T : new() {
            var t = new T();
            func(t, new Config());
            return t;
        }
    }

    public class Config {
        public Config() {
        }

        private static readonly MailGenerator MailGenerator = new MailGenerator();
        public string RandomName => Names.Value.RandomItem.Data;
        public string UserName => UserNames.Value.RandomItem;
        public Func<string, string, string> MailAddress => MailGenerator.Mail;
    }
}