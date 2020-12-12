using System;

namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods providing System.DateTimeOffset.
    /// </summary>
    public interface IDateTimeOffsetProvider
    {
        /// <summary>
        ///     Provides a System.DateTimeOffset between DateTime.MinVale and DateTime.MaxValue.
        /// </summary>
        DateTimeOffset DateTimeOffset();

        /// <summary>
        ///     Provides a System.DateTimeOffset between System.DateTimeOffset.MinVal and <paramref name="max" />.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        DateTimeOffset DateTimeOffset(in DateTimeOffset max);

        /// <summary>
        ///     Provides a System.DateTimeOffset between <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        DateTimeOffset DateTimeOffset(in DateTimeOffset min, in DateTimeOffset max);
    }
}