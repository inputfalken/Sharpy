using System.Collections.Generic;
using NodaTime;
using Sharpy.Enums;

namespace Sharpy.Types {
    public interface IRandomizer {
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
        T CustomCollection<T>(List<T> items);

        /// <summary>
        ///     Gives a random name, it could be a female first name, male first name and a lastname.
        /// </summary>
        string Name();

        /// <summary>
        ///     Gives a random name based on type of argument.
        /// </summary>
        string Name(NameType nameType);

        /// <summary>
        ///     Gives a random username from a huge collection.
        /// </summary>
        string UserName();

        /// <summary>
        ///     Gives a random bool
        /// </summary>
        bool Bool();

        /// <summary>
        ///     Gives a random number within below the argument value
        /// </summary>
        int Number(int maxNum);

        /// <summary>
        ///     Gives a random number within within the two arguments
        /// </summary>
        int Number(int minNum, int maxNum);

        /// <summary>
        ///     Gives a date with random month, date and subtract the current the current year by the argument
        /// </summary>
        LocalDate DateByAge(int age);

        /// <summary>
        ///     Gives a random month, date and use the argument given as year
        /// </summary>
        LocalDate DateByYear(int year);

        /// <summary>
        ///     gives a random phonenumber using a random country code and lets you specify a number to start with as well as the
        ///     length.
        /// </summary>
        string PhoneNumber(string preNumber = null, int length = 4);

        /// <summary>
        ///     Gives a mail address by concatining the arguments into a mail address.
        /// </summary>
        string MailAdress(string name, string secondName = null);
    }
}