using System.Collections.Generic;
using NodaTime;

namespace Sharpy {
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
        ///     Gives a string based on argument.
        /// </summary>
        string String(TStringArg type);

        /// <summary>
        ///     Gives a random bool
        /// </summary>
        bool Bool();

        /// <summary>
        ///     Gives a random number within below the argument value
        /// </summary>
        int Integer(int maxNum);

        /// <summary>
        ///     Gives a random number within within the two arguments
        /// </summary>
        int Integer(int minNum, int maxNum);

        /// <summary>
        ///     Gives a date with random month, date and subtract the current the current year by the argument
        /// </summary>
        LocalDate DateByAge(int age);

        /// <summary>
        ///     Gives a random month, date and use the argument given as year
        /// </summary>
        LocalDate DateByYear(int year);

        string SocialSecurityNumber(LocalDate date);

        /// <summary>
        ///     Gives a mail address by concatining the arguments into a mail address.
        /// </summary>
        string MailAdress(string name, string secondName = null);
    }
}