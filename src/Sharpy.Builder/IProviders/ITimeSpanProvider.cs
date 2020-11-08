using System;

namespace Sharpy.Builder.IProviders
{
    /// <summary>
    /// Methods to provide System.TimeSpan.
    /// </summary>
    public interface ITimeSpanProvider
    {
        /// <summary>
        /// Provides a System.TimeSpan between System.TimeSpan.Zero and System.TimeSpan.MaxValue.
        /// </summary>
        TimeSpan TimeSpan();

        /// <summary>
        /// Provides a System.TimeSpan between System.TimeSpan.Zero and <paramref name="max"/>.
        /// </summary>
        TimeSpan TimeSpan(TimeSpan max);

        /// <summary>
        /// Provides a System.TimeSpan between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        TimeSpan TimeSpan(TimeSpan min, TimeSpan max);
    }
}