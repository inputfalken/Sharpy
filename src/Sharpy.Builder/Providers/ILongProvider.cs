namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods providing System.Int64.
    /// </summary>
    public interface ILongProvider
    {
        /// <summary>
        ///     Provides a System.Int64 between <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        long Long(in long min, in long max);

        /// <summary>
        ///     Provides a System.Int64 between 0 and <paramref name="max" />.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        long Long(in long max);

        /// <summary>
        ///     Provides a System.Int64 between 0 and System.Int64.MaxValue.
        /// </summary>
        long Long();
    }
}