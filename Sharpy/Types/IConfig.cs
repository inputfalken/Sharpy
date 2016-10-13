using System;
using System.Collections.Generic;
using Sharpy.Enums;

namespace Sharpy.Types {
    public interface IConfig<T> {
        /// <summary>
        ///     Executes the predicate on each name.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Config<T> Name(Func<string, bool> predicate);

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        Config<T> Name(params Country[] countries);

        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        Config<T> Name(params Region[] regions);

        /// <summary>
        ///     Lets you set the providers for the mail addresses.
        ///     You can also a set a bool for wether the addreses will be unique.
        ///     If set to unique numbers will be appended in case of replicate mail address.
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="uniqueAddresses">For Unique Addresses</param>
        /// <returns></returns>
        Config<T> MailGenerator(IEnumerable<string> providers, bool uniqueAddresses = false);

        /// <summary>
        ///     Lets you change the settings for the number generator.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="length"></param>
        /// <param name="uniqueNumbers"></param>
        /// <returns></returns>
        Config<T> PhoneGenerator(Country countryCode, int length, bool uniqueNumbers = false);

        /// <summary>
        ///     Executes the predicate on each username.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Config<T> UserName(Func<string, bool> predicate);

        /// <summary>
        ///     Will set a seed for the generator to use
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        Config<T> Seed(int seed);
    }
}