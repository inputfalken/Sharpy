using System;
using System.Collections.Generic;
using NodaTime;
using Sharpy.Enums;
using Sharpy.ExtensionMethods;
using Sharpy.Randomizer.Generators;

namespace Sharpy.Randomizer {
    internal sealed class Randomizer : IRandomizer<StringType> {
        private Tuple<int, int, int> _phoneState;

        public Randomizer(Config config) {
            Config = config;
            PhoneNumberGenerator = new NumberGenerator(Config.Random);
        }

        private Config Config { get; }

        private NumberGenerator PhoneNumberGenerator { get; }

        public TElement Params<TElement>(params TElement[] items) => items[Integer(items.Length)];

        public TElement CustomCollection<TElement>(IList<TElement> items) => items[Integer(items.Count)];


        public string String(StringType stringType) {
            if (!Config.Dictionary.ContainsKey(stringType))
                Config.Dictionary.Add(stringType, new Fetcher<string>(Config.StringType(stringType)));
            return Config.Dictionary[stringType].RandomItem(Config.Random);
        }

        public bool Bool() => Integer(2) != 0;

        public int Integer(int max) => Config.Random.Next(max);

        public int Integer(int min, int max) => Config.Random.Next(min, max);

        public int Integer() => Config.Random.Next(int.MinValue, int.MaxValue);


        public LocalDate DateByAge(int age) => Config.DateGenerator.RandomDateByAge(age);

        public LocalDate DateByYear(int year) => Config.DateGenerator.RandomDateByYear(year);

        public string SocialSecurityNumber(LocalDate date, bool formated = true) {
            var month = date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();
            var year = date.YearOfCentury < 10 ? $"0{date.YearOfCentury}" : date.YearOfCentury.ToString();
            var day = date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
            var controlNumber = Config.Random.Next(1000, 9999);
            var res = Config
                .SocialSecurityNumberGenerator
                .SecurityNumber(controlNumber, year + month + day)
                .ToString();
            return formated ? res.Insert(6, "-") : res;
        }

        public string MailAddress(string name, string secondName = null)
            => Config.Mailgen.Mail(name, secondName);

        public string PhoneNumber(int length, string prefix = null) {
            //If the field _phoneState not null and length inside phonestate is not changed.
            if (_phoneState != null && _phoneState.Item1 == length)
                return prefix + PhoneNumberGenerator.RandomNumber(_phoneState.Item2, _phoneState.Item3, true);

            //Else assign new value to _phoneState.
            var min = (int) Math.Pow(10, length - 1);
            var max = min*10 - 1;
            _phoneState = new Tuple<int, int, int>(length, min, max);
            return prefix + PhoneNumberGenerator.RandomNumber(_phoneState.Item2, _phoneState.Item3, true);
        }

        public long Long(long min, long max) => Config.Random.NextLong(min, max);

        public long Long(long max) => Config.Random.NextLong(max);

        public long Long() => Config.Random.NextLong();
    }
}