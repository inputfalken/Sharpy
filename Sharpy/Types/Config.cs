using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Enums;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Mail;

namespace Sharpy.Types {
    public class Config<T> : IConfig<T> {
        private Generator<T> Generator { get; }

        public Config(Generator<T> generator) {
            Generator = generator;
        }


        /// <summary>
        ///     Executes the predicate on each name.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Config<T> Name(Func<string, bool> predicate) {
            Generator.Names = new Fetcher<Name.Name>(Generator.Names.Where(name => predicate(name.Data)));
            return this;
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public Config<T> Name(params Country[] countries) {
            Generator.Names = new Fetcher<Name.Name>(ByCountry(countries));
            return this;
        }


        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public Config<T> Name(params Region[] regions) {
            Generator.Names = new Fetcher<Name.Name>(ByRegion(regions));
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
        public Config<T> MailGenerator(IEnumerable<string> providers, bool uniqueAddresses = false) {
            Generator.MailGenerator = new MailGenerator(providers, Generator.Random, uniqueAddresses);
            return this;
        }

        /// <summary>
        ///     Lets you change the settings for the number generator.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="length"></param>
        /// <param name="uniqueNumbers"></param>
        /// <returns></returns>
        public Config<T> PhoneGenerator(Country countryCode, int length, bool uniqueNumbers = false) {
            Generator.PhoneNumberGenerator =
                new PhoneNumberGenerator(Generator.LazyCountryCodes.Value.Single(number => number.Name == countryCode),
                    Generator.Random,
                    length, uniqueNumbers);
            return this;
        }


        /// <summary>
        ///     Executes the predicate on each username.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Config<T> UserName(Func<string, bool> predicate) {
            Generator.UserNames = new Fetcher<string>(Generator.UserNames.Where(predicate));
            return this;
        }

        /// <summary>
        ///     Will set a seed for the generator to use
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Config<T> Seed(int seed) {
            Generator.Random = new Random(seed);
            return this;
        }


        private IEnumerable<Name.Name> ByCountry(params Country[] args)
            => new Fetcher<Name.Name>(Generator.Names.Where(name => args.Contains(name.Country)));


        private IEnumerable<Name.Name> ByRegion(params Region[] args)
            => new Fetcher<Name.Name>(Generator.Names.Where(name => args.Contains(name.Region)));
    }
}