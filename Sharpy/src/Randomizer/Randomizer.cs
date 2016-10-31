using System;
using System.Collections.Generic;
using NodaTime;
using Sharpy.Enums;

namespace Sharpy.Randomizer {
    internal sealed class Randomizer : IRandomizer<StringType> {
        public Randomizer(Config config) {
            Config = config;
        }

        internal Config Config { get; }

        /// <summary>
        /// Tracks the max of elements which could be generated.
        /// </summary>
        internal int MaxAmmount { private get; set; }

        public TElement CustomCollection<TElement>(params TElement[] items) => items[Integer(items.Length)];

        public TElement CustomCollection<TElement>(IList<TElement> items) => items[Integer(items.Count)];


        public string String(StringType stringType) {
            Console.WriteLine(MaxAmmount);
            if (!Config.Dictionary.ContainsKey(stringType))
                Config.Dictionary.Add(stringType, new Fetcher<string>(Config.StringType(stringType)));
            return Config.Dictionary[stringType].RandomItem(Config.Random);
        }

        public bool Bool() => Integer(2) != 0;

        public int Integer(int max, bool unique = false) {
            if (unique) {}
            return Config.Random.Next(max);
        }

        public int Integer(int min, int max, bool unique = false) {
            if (unique) {}
            return Config.Random.Next(min, max);
        }


        public LocalDate DateByAge(int age) => Config.DateGenerator.RandomDateByAge(age);

        public LocalDate DateByYear(int year) => Config.DateGenerator.RandomDateByYear(year);

        public string SocialSecurityNumber(LocalDate date, bool formated = true) {
            var month = date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();
            var year = date.YearOfCentury < 10 ? $"0{date.YearOfCentury}" : date.YearOfCentury.ToString();
            var day = date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
            var controlNumber = Config.Random.Next(1000, 9999);
            var res = Config
                .SocialSecurityNumberGenerator
                .RandomNumber(long.Parse(year + month + day + controlNumber))
                .ToString();
            return formated ? res.Insert(6, "-") : res;
        }

        public string MailAddress(string name, string secondName = null)
            => Config.Mailgen.Mail(name, secondName);
    }
}