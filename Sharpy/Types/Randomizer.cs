using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Types;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Date;
using Sharpy.Types.Mail;

namespace Sharpy.Types {
    /// <summary>
    ///     Will randomize all data that these methods return.
    /// </summary>
    public sealed class Randomizer<T> : IRandomizer {
        public Randomizer(Generator<T> generator) {
            Generator = generator;
        }

        private Generator<T> Generator { get; }


        /// <summary>
        ///     Can be used if you have your own collection of items that you would want an random item from.
        /// </summary>
        /// <returns></returns>
        public TElement CustomCollection<TElement>(params TElement[] items) => items[Number(items.Length)];

        /// <summary>
        ///     Can be used if you have your own collection of items that you would want an random item from.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public TElement CustomCollection<TElement>(List<TElement> items) => items[Number(items.Count)];

        /// <summary>
        ///     Gives a random name, it could be a female first name, male first name and a lastname.
        /// </summary>
        public string Name() => Generator.Names.RandomItem(Generator.Random).Data;


        /// <summary>
        ///     Gives a random name based on type of argument.
        /// </summary>
        public string Name(NameType nameType) {
            if (!Generator.Dictionary.ContainsKey(nameType))
                Generator.Dictionary.Add(nameType,
                    new Fetcher<string>(Generator.Type(nameType).Select(name => name.Data)));
            return Generator.Dictionary[nameType].RandomItem(Generator.Random);
        }

        /// <summary>
        ///     Gives a random username from a huge collection.
        /// </summary>
        public string UserName() => Generator.UserNames.RandomItem(Generator.Random);

        /// <summary>
        ///     Gives a random bool
        /// </summary>
        public bool Bool() => Number(2) != 0;

        /// <summary>
        ///     Gives a random number within below the argument value
        /// </summary>
        public int Number(int maxNum) => Generator.Random.Next(maxNum);

        /// <summary>
        ///     Gives a random number within within the two arguments
        /// </summary>
        public int Number(int minNum, int maxNum) => Generator.Random.Next(minNum, maxNum);

        /// <summary>
        ///     Gives a date with random month, date and subtract the current the current year by the argument
        /// </summary>
        public LocalDate DateByAge(int age) => Generator.DateGenerator.RandomDateByAge(age);

        /// <summary>
        ///     Gives a random month, date and use the argument given as year
        /// </summary>
        public LocalDate DateByYear(int year) => Generator.DateGenerator.RandomDateByYear(year);

        /// <summary>
        ///     gives a random phonenumber using a random country code and lets you specify a number to start with as well as the
        ///     length.
        /// </summary>
        public string PhoneNumber(string preNumber = null) =>
            Generator.PhoneNumberGenerator.RandomNumber(preNumber);

        /// <summary>
        ///     Gives a mail address by concatining the arguments into a mail address.
        /// </summary>
        public string MailAdress(string name, string secondName = null)
            => Generator.MailGenerator.Mail(name, secondName);
    }
}