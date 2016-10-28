using System.Collections.Generic;
using NodaTime;
using Sharpy.Enums;

namespace Sharpy.Randomizer {
    internal sealed class Randomizer : IRandomizer<StringType> {
        public Randomizer(Config config) {
            Config = config;
        }

        private Config Config { get; }


        public TElement CustomCollection<TElement>(params TElement[] items) => items[Integer(items.Length)];

        public TElement CustomCollection<TElement>(IList<TElement> items) => items[Integer(items.Count)];


        public string String(StringType stringType) {
            if (stringType == StringType.Number) return Config.NumberGen.RandomNumber();
            if (!Config.Dictionary.ContainsKey(stringType))
                Config.Dictionary.Add(stringType, new Fetcher<string>(Config.StringType(stringType)));
            return Config.Dictionary[stringType].RandomItem(Config.Random);
        }

        public bool Bool() => Integer(2) != 0;

        public int Integer(int max) => Config.Random.Next(max);

        public int Integer(int min, int max) => Config.Random.Next(min, max);

        public LocalDate DateByAge(int age) => Config.DateGenerator.RandomDateByAge(age);

        public LocalDate DateByYear(int year) => Config.DateGenerator.RandomDateByYear(year);

        public string SocialSecurityNumber(LocalDate date, bool formated = true)
            => Config.SocialSecurityNumberGenerator.SocialSecurity(date, formated);

        public string MailAdress(string name, string secondName = null)
            => Config.Mailgen.Mail(name, secondName);
    }
}