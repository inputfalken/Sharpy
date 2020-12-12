using System;

namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods to provide System.TimeSpan.
    /// </summary>
    public interface ITimeSpanProvider
    {
        /// <summary>
        ///     Provides a System.TimeSpan between System.TimeSpan.Zero and System.TimeSpan.MaxValue.
        /// </summary>
        TimeSpan TimeSpan();

        /// <summary>
        ///     Provides a System.TimeSpan between System.TimeSpan.Zero and <paramref name="max" />.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        TimeSpan TimeSpan(in TimeSpan max);

        /// <summary>
        ///     Provides a System.TimeSpan between <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        TimeSpan TimeSpan(in TimeSpan min, in TimeSpan max);
    }
}