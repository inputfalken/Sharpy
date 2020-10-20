using System;

namespace Sharpy.Builder.IProviders
{
    /// <summary>
    ///     <para>
    ///         Methods to provide <see cref="DateTime" />.
    ///     </para>
    /// </summary>
    public interface IDateProvider
    {
        /// <summary>
        ///     Creates a <see cref="DateTime" />  based on <paramref name="age" />.
        /// </summary>
        /// <param name="age">
        ///     Determines the age the <see cref="DateTime"/> must have.
        /// </param>
        DateTime DateByAge(int age);

        /// <summary>
        ///     Creates a <see cref="DateTime" /> based on <paramref name="year" />.
        /// </summary>
        /// <param name="year">
        ///     Determines the year the <see cref="DateTime"/> must have.
        /// </param>
        DateTime DateByYear(int year);

        /// <summary>
        ///     Creates a <see cref="DateTime" />.
        /// </summary>
        DateTime Date();

        /// <summary>
        ///     Creates a <see cref="DateTime" />.
        /// </summary>
        /// <param name="max">
        ///     Determines the max <see cref="DateTime"/> that can be created.
        /// </param>
        DateTime Date(DateTime max);

        /// <summary>
        ///     Creates a <see cref="DateTime" /> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">
        ///     Determines the minimum <see cref="DateTime" /> that can be created.
        /// </param>
        /// <param name="max">
        ///     Determines the maximum <see cref="DateTime" /> that can be created.
        /// </param>
        DateTime Date(DateTime min, DateTime max);
    }
}