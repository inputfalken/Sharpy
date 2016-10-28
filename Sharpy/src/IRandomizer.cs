using System.Collections.Generic;
using NodaTime;

namespace Sharpy {
    /// <summary>
    ///     A contract containng various Methods to randomize data.
    /// </summary>
    /// <typeparam name="TStringArg"></typeparam>
    public interface IRandomizer<in TStringArg> {
        /// <summary>
        ///     Can be used if you have your own collection of items that you would want an random item from.
        /// </summary>
        /// <returns></returns>
        T CustomCollection<T>(params T[] items);

        /// <summary>
        ///     Can be used if you have your own collection of items that you would want an random item from.
        /// </summary>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CustomCollection<T>(IList<T> items);

        /// <summary>
        ///     Randomizes a string based on argument.
        /// </summary>
        string String(TStringArg type);

        /// <summary>
        ///     Randomizes a bool
        /// </summary>
        bool Bool();

        /// <summary>
        ///     Randomizes a Integer from 0 to argument.
        /// </summary>
        int Integer(int maxNum);

        /// <summary>
        ///     Randomizes a Integer from minNum to maxNum.
        /// </summary>
        int Integer(int minNum, int maxNum);

        /// <summary>
        ///     Randomizes a random month and date then subtracts current year by value supplied.
        /// </summary>
        LocalDate DateByAge(int age);

        /// <summary>
        ///     Randomizes a random month and date then uses argument as year.
        /// </summary>
        LocalDate DateByYear(int year);

        /// <summary>
        ///     Gives a string representing a social security number.
        ///     Will use the date given and then randomize 4 unique numbers as control numbers.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="formated">Determines wether the string should be formated</param>
        /// <returns></returns>
        string SocialSecurityNumber(LocalDate date, bool formated = true);

        /// <summary>
        ///     Gives a mail address by concatinating the arguments into a mail address.
        /// </summary>
        string MailAdress(string name, string secondName = null);
    }
}