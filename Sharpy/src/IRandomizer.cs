using System.Collections.Generic;
using NodaTime;

namespace Sharpy {
    /// <summary>
    ///     <para>A contract containing various Methods to generate data.</para>
    /// </summary>
    /// <typeparam name="TStringArg"></typeparam>
    public interface IRandomizer<in TStringArg> {
        /// <summary>
        ///     <para>Returns a random item from the arguments given</para>
        /// </summary>
        /// <param name="items">Arguments...</param>
        /// <typeparam name="T">A random item from the arguments</typeparam>
        T Params<T>(params T[] items);

        /// <summary>
        ///     <para>Returns a random item from the supplied IList</para>
        /// </summary>
        /// <param name="items">The collection to be used.</param>
        /// <typeparam name="T">A random item from the collection.</typeparam>
        T CustomCollection<T>(IList<T> items);

        /// <summary>
        ///     <para>Returns a randomized  string based on argument.</para>
        ///     <param name="type">The type of string.</param>
        /// </summary>
        string String(TStringArg type);

        /// <summary>
        ///     <para>Returns a randomized bool</para>
        /// </summary>
        bool Bool();

        /// <summary>
        ///     <para>Returns a randomized Integer from 0 to max.</para>
        ///     <param name="max">The max value</param>
        /// </summary>
        int Integer(int max);

        /// <summary>
        ///     <para>Returns a randomized Integer from min to max.</para>
        /// </summary>
        int Integer(int min, int max);

        /// <summary>
        ///     <para>Returns a randomized Integer over all possible values</para>
        /// </summary>
        /// <returns></returns>
        int Integer();

        /// <summary>
        ///     <para>Returns a LocalDate with the CurrentYear minus the age and randomized date & month</para>
        /// </summary>
        LocalDate DateByAge(int age);

        /// <summary>
        ///     <para>Returns a LocalDate with the year supplied and randomized date & month.</para>
        /// </summary>
        LocalDate DateByYear(int year);

        /// <summary>
        ///     <para>Returns a string representing a social security number.</para>
        ///     <para>Will use the date given and then randomize 4 unique numbers as control numbers.</para>
        /// </summary>
        /// <param name="date">The date of birth</param>
        /// <param name="formated">Determines wether the string should be formated</param>
        string SocialSecurityNumber(LocalDate date, bool formated = true);

        /// <summary>
        ///     <para>Returns a string representing a MailAddress.</para>
        /// <param name="name">Is the first string in the mail address.</param>
        /// <param name="secondName">Optional second string.</param>
        /// </summary>
        string MailAddress(string name, string secondName = null);

        /// <summary>
        ///     <para>Returns a string representing a unique phonenumber</para>
        /// </summary>
        /// <param name="length">The length of the number. Prefix will not be counted for this argument</param>
        /// <param name="prefix">The prefix of the number.</param>
        string PhoneNumber(int length, string prefix = null);

        /// <summary>
        ///     <para>Returns a random long from min (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        long Long(long min, long max);

        /// <summary>
        ///     <para>Returns a random long from 0 (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
        long Long(long max);

        /// <summary>
        ///     <para>Returns a random long over all possible values of long</para>
        /// </summary>
        long Long();
    }
}