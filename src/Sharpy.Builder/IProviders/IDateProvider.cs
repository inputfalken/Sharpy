using System;

namespace Sharpy.Builder.IProviders
{
    /// <summary>
    ///  Methods to provide <see cref="DateTime" />.
    /// </summary>
    public interface IDateProvider
    {
        /// <summary>
        ///     Provides a <see cref="DateTime" /> by <paramref name="age"/>.
        /// </summary>
        DateTime DateByAge(int age);

        /// <summary>
        ///     Provides a <see cref="DateTime" /> by <paramref name="year"/>.
        /// </summary>
        DateTime DateByYear(int year);

        /// <summary>
        /// Provides a DateTime between DateTime.MinVale and DateTime.MaxValue.
        /// </summary>
        DateTime Date();

        /// <summary>
        /// Provides a DateTime between System.DateTime.MinVale and <paramref name="max"/>.
        /// </summary>
        DateTime Date(DateTime max);

        /// <summary>
        /// Provides a System.DateTime between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        DateTime Date(DateTime min, DateTime max);
    }
}