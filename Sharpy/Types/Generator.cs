using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Enums;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Mail;

namespace Sharpy.Types {
    /// <summary>
    ///     Contains methods for generating the supplied type
    ///     And a config to set settings for how the supplied type should get generated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Generator<T> {
        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<Randomizer, T> func) {
            Func = func;
        }

        public Generator(Func<Randomizer, int, T> func) {
            FuncIterator = func;
        }


        /// <summary>
        ///     Can be used to change settings for the randomizer
        /// </summary>
        private Randomizer Randomizer { get; } = new Randomizer();

        private Func<Randomizer, T> Func { get; }
        private Func<Randomizer, int, T> FuncIterator { get; }

        private int Iteratation { get; set; }

        private T Generate(int i) => FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, i);

        /// <summary>
        ///     Will give back one instance of the specified Type
        /// </summary>
        /// <returns></returns>
        public T Generate() => FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, Iteratation++);

        /// <summary>
        ///     Will give back an IEnumerable with the specified type.
        ///     Which contains the ammount of elements.
        /// </summary>
        /// <param name="ammount"></param>
        public IEnumerable<T> GenerateEnumerable(int ammount) {
            for (var i = 0; i < ammount; i++)
                yield return Generate(i);
        }


        /// <summary>
        ///     Executes the predicate on each name.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Generator<T> Name(Func<string, bool> predicate) {
            Randomizer.Names = new Fetcher<Name.Name>(Randomizer.Names.Where(name => predicate(name.Data)));
            return this;
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public Generator<T> Name(params Country[] countries) {
            Randomizer.Names = new Fetcher<Name.Name>(Randomizer.ByCountry(countries));
            return this;
        }


        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public Generator<T> Name(params Region[] regions) {
            Randomizer.Names = new Fetcher<Name.Name>(Randomizer.ByRegion(regions));
            return this;
        }

        /// <summary>
        ///     Lets you set the providers for the mail addresses.
        ///     You can also a set a bool for wether the addreses will be unique.
        ///     If set to unique numbers will be appended in case of replicate mail address.
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="uniqueAddresses">For Unique Addresses</param>
        /// <returns></returns>
        public Generator<T> Mail(IEnumerable<string> providers, bool uniqueAddresses = false) {
            Randomizer.MailGenerator = new MailGenerator(providers, Randomizer.Random, uniqueAddresses);
            return this;
        }

        /// <summary>
        ///     Lets you change the country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="length"></param>
        /// <param name="uniqueNumbers"></param>
        /// <returns></returns>
        public Generator<T> CountryCode(Country countryCode, int length, bool uniqueNumbers = false) {
            Randomizer.PhoneNumberGenerator =
                new PhoneNumberGenerator(Randomizer.LazyCountryCodes.Value.Single(number => number.Name == countryCode),
                    Randomizer.Random,
                    length,
                    uniqueNumbers);
            return this;
        }


        /// <summary>
        ///     Executes the predicate on each username.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Generator<T> UserName(Func<string, bool> predicate) {
            Randomizer.UserNames = new Fetcher<string>(Randomizer.UserNames.Where(predicate));
            return this;
        }

        /// <summary>
        ///     Will set a seed for the generator to use
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Generator<T> Seed(int seed) {
            Randomizer.Random = new Random(seed);
            return this;
        }
    }
}