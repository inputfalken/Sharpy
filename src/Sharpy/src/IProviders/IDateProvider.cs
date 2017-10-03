using System;

namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>
    ///         Methods providing <see cref="DateTime" />.
    ///     </para>
    /// </summary>
    public interface IDateProvider {
        /// <summary>
        ///     Creates a <see cref="DateTime" /> based on argument <paramref name="age" />.
        /// </summary>
        /// <param name="age">
        ///     TODO
        /// </param>
        DateTime DateByAge(int age);

        /// <summary>
        ///     Creates a <see cref="DateTime" /> based on argument <paramref name="year" />.
        /// </summary>
        /// <param name="year">
        ///     TODO
        /// </param>
        DateTime DateByYear(int year);

        /// <summary>
        ///     Creates a <see cref="DateTime"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="DateTime"/>.
        /// </returns>
        DateTime Date();
    }
}