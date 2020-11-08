using System;

namespace Sharpy.Builder.IProviders
{
    /// <summary>
    ///  Methods to provide <see cref="System.DateTime" />.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        ///     Provides a <see cref="System.DateTime" /> by <paramref name="age"/>.
        /// </summary>
        DateTime DateTimeByAge(int age);

        /// <summary>
        ///     Provides a <see cref="System.DateTime" /> by <paramref name="year"/>.
        /// </summary>
        DateTime DateTimeByYear(int year);

        /// <summary>
        /// Provides a DateTime between DateTime.MinVale and DateTime.MaxValue.
        /// </summary>
        DateTime DateTime();

        /// <summary>
        /// Provides a DateTime between System.DateTime.MinVale and <paramref name="max"/>.
        /// </summary>
        DateTime DateTime(DateTime max);

        /// <summary>
        /// Provides a System.DateTime between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        DateTime DateTime(DateTime min, DateTime max);
    }
}