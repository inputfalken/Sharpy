using System.Collections.Generic;
using NodaTime;
using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    ///     <para>Represents a Generator with methods for  generating common datatypes.</para>
    ///     <remarks>This will overtime get new methods</remarks>
    /// </summary>
    public interface IGenerator : ILongProvider, IDoubleProvider, IIntegerProvider, INameProvider, IStringProvider {
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
}