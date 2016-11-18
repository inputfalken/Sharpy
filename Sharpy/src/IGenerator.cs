using System.Collections.Generic;
using NodaTime;

namespace Sharpy {
    /// <summary>
    ///     <para>A contract containing various Methods to generate data.</para>
    /// </summary>
    public interface IGenerator : ILong, IDouble, IInteger {
        /// <summary>
        ///     <para>Returns one of the Arguments given.</para>
        /// </summary>
        T Params<T>(params T[] items);

        /// <summary>
        ///     <para>Returns one of the Items from the IReadOnlyList</para>
        /// </summary>
        /// <param name="items">The collection to be used.</param>
        /// <typeparam name="T">A random item from the collection.</typeparam>
        T CustomCollection<T>(IReadOnlyList<T> items);

        /// <summary>
        ///     <para>Returns a string based on the argument</para>
        ///     <param name="type">The type of string.</param>
        /// </summary>
        string String(string type);

        /// <summary>
        ///     <para>Returns a bool</para>
        /// </summary>
        bool Bool();


        /// <summary>
        ///     <para>Returns a LocalDate with the CurrentYear minus the age and generates date and month.</para>
        /// </summary>
        LocalDate DateByAge(int age);

        /// <summary>
        ///     <para>Returns a LocalDate with the year supplied and generates date and month.</para>
        /// </summary>
        LocalDate DateByYear(int year);

        /// <summary>
        ///     <para>Returns a string representing a social security number.</para>
        ///     <para>Will use the date given and then generate 4 numbers as control numbers.</para>
        /// </summary>
        /// <param name="date">The date of birth</param>
        /// <param name="formated">Determines wether the string should be formated</param>
        string SocialSecurityNumber(LocalDate date, bool formated = true);

        /// <summary>
        ///     <para>Returns a string representing a MailAddress.</para>
        ///     <param name="name">First string in the mail address.</param>
        ///     <param name="secondName">Optional second string.</param>
        /// </summary>
        string MailAddress(string name, string secondName = null);

        /// <summary>
        ///     <para>Returns a string representing a phonenumber</para>
        /// </summary>
        /// <param name="length">The length of the number. Prefix will not be counted for this argument</param>
        /// <param name="prefix">The prefix of the number.</param>
        string PhoneNumber(int length, string prefix = null);
    }

    public interface ILong {
        /// <summary>
        ///     <para>Returns a long from min (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        long Long(long min, long max);

        /// <summary>
        ///     <para>Returns a long from 0 (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
        long Long(long max);

        /// <summary>
        ///     <para>Returns a long within all possible values of long</para>
        /// </summary>
        long Long();
    }

    public interface IInteger {
        
        /// <summary>
        ///     <para>Returns a Integer from 0 to max.</para>
        ///     <param name="max">The max value</param>
        /// </summary>
        int Integer(int max);

        /// <summary>
        ///     <para>Returns a Integer from min to max.</para>
        /// </summary>
        int Integer(int min, int max);

        /// <summary>
        ///     <para>Returns a Integer within all possible values of integer (except int.MaxValue)</para>
        /// </summary>
        /// <returns></returns>
        int Integer();
    }

    public interface IDouble {
        /// <summary>
        ///     <para>Returns a generated double from 0 to 1</para>
        /// </summary>
        double Double();

        /// <summary>
        ///     <para>Returns a generated long from 0 (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
        double Double(double max);

        /// <summary>
        ///     <para>Returns a generated double from min (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        double Double(double min, double max);
    }
}