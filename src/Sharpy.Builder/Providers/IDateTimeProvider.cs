﻿using System;

namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods providing System.DateTime.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        ///     Provides a System.DateTime by <paramref name="age" />.
        /// </summary>
        DateTime DateTimeFromAge(in int age);

        /// <summary>
        ///     Provides a System.DateTime by <paramref name="year" />.
        /// </summary>
        DateTime DateTimeFromYear(in int year);

        /// <summary>
        ///     Provides a DateTime between DateTime.MinVale and DateTime.MaxValue.
        /// </summary>
        DateTime DateTime();

        /// <summary>
        ///     Provides a DateTime between System.DateTime.MinVale and <paramref name="max" />.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        DateTime DateTime(in DateTime max);

        /// <summary>
        ///     Provides a System.DateTime between <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        DateTime DateTime(in DateTime min, in DateTime max);
    }
}