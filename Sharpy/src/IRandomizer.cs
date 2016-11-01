using System.Collections.Generic;
using NodaTime;

namespace Sharpy {
    /// <summary>
    ///     <para>A contract containing various Methods to randomize data.</para>
    /// </summary>
    /// <typeparam name="TStringArg"></typeparam>
    public interface IRandomizer<in TStringArg> {
        /// <summary>
        ///     <para>Can be used if you have your own collection of items that you would want an random item from.</para>
        /// </summary>
        /// <returns></returns>
        T CustomCollection<T>(params T[] items);

        /// <summary>
        ///     <para>Can be used if you have your own collection of items that you would want an random item from.</para>
        /// </summary>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CustomCollection<T>(IList<T> items);

        /// <summary>
        ///     <para>Randomizes a string based on argument.</para>
        /// </summary>
        string String(TStringArg type);

        /// <summary>
        ///     <para>Randomizes a bool</para>
        /// </summary>
        bool Bool();

        /// <summary>
        ///     <para>Randomizes a Integer from 0 to max.</para>
        /// </summary>
        int Integer(int max);

        /// <summary>
        ///     <para>Randomizes a Integer from min to max.</para>
        /// </summary>
        int Integer(int min, int max);

        /// <summary>
        ///     <para>Randomizes a random month and date then subtracts current year by value supplied.</para>
        /// </summary>
        LocalDate DateByAge(int age);

        /// <summary>
        ///     <para>Randomizes a random month and date then uses argument as year.</para>
        /// </summary>
        LocalDate DateByYear(int year);

        /// <summary>
        ///     <para>Gives a string representing a social security number.</para>
        ///     <para>Will use the date given and then randomize 4 unique numbers as control numbers.</para>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="formated">Determines wether the string should be formated</param>
        /// <returns></returns>
        string SocialSecurityNumber(LocalDate date, bool formated = true);

        /// <summary>
        ///     <para>Gives a mail address by concatenating the arguments into a mail address.</para>
        /// </summary>
        string MailAddress(string name, string secondName = null);

        string PhoneNumber(int length, string prefix = null);

        /// <summary>
        ///     <para>Returns a random long from min (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        /// <returns></returns>
        long Long(long min, long max);

        /// <summary>
        ///    <para>Returns a random long from 0 (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
        /// <returns></returns>
        long Long(long max);

        /// <summary>
        ///     <para>Returns a random long over all possible values of long (except long.MaxValue, similar to</para>
        /// </summary>
        /// <returns></returns>
        long Long();
    }
}