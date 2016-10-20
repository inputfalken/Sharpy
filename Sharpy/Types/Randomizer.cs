using System.Collections.Generic;
using NodaTime;
using Sharpy.Enums;

namespace Sharpy.Types {
    /// <summary>
    ///     Will randomize all data that these methods return.
    /// </summary>
    public sealed class Randomizer : IRandomizer<StringType> {
        public Randomizer(Config config = null) {
            Config = config ?? new Config();
        }

        private Config Config { get; }


        /// <summary>
        ///     Can be used if you have your own collection of items that you would want an random item from.
        /// </summary>
        /// <returns></returns>
        public TElement CustomCollection<TElement>(params TElement[] items) => items[Integer(items.Length)];

        /// <summary>
        ///     Can be used if you have your own collection of items that you would want an random item from.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public TElement CustomCollection<TElement>(IList<TElement> items) => items[Integer(items.Count)];


        /// <summary>
        ///     Gives a random string based on argument.
        /// </summary>
        public string String(StringType stringType) {
            if (stringType == StringType.Number) return Config.NumberGen.RandomNumber();
            if (!Config.Dictionary.ContainsKey(stringType))
                Config.Dictionary.Add(stringType, new Fetcher<string>(Config.StringType(stringType)));
            return Config.Dictionary[stringType].RandomItem(Config.Random);
        }

        /// <summary>
        ///     Gives a random bool
        /// </summary>
        public bool Bool() => Integer(2) != 0;

        /// <summary>
        ///     Gives a random number within below the argument value
        /// </summary>
        public int Integer(int maxNum) => Config.Random.Next(maxNum);

        /// <summary>
        ///     Gives a random number within within the two arguments
        /// </summary>
        public int Integer(int minNum, int maxNum) => Config.Random.Next(minNum, maxNum);

        /// <summary>
        ///     Gives a date with random month, date and subtract the current the current year by the argument
        /// </summary>
        public LocalDate DateByAge(int age) => Config.DateGenerator.RandomDateByAge(age);

        /// <summary>
        ///     Gives a random month, date and use the argument given as year
        /// </summary>
        public LocalDate DateByYear(int year) => Config.DateGenerator.RandomDateByYear(year);

        /// <summary>
        ///     Gives a mail address by concatining the arguments into a mail address.
        /// </summary>
        public string MailAdress(string name, string secondName = null)
            => Config.Mailgen.Mail(name, secondName);
    }
}