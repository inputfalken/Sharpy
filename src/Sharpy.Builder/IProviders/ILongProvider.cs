namespace Sharpy.Builder.IProviders
{
    /// <summary>
    ///   Methods providing System.Int64.
    /// </summary>
    public interface ILongProvider
    {
        /// <summary>
        /// Provides a System.Int64 between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        /// <returns>
        ///     A <see cref="long" /> greater than or equal to argument <paramref name="min" /> and less than argument
        ///     <paramref name="max" />.
        /// </returns>
        long Long(long min, long max);

        /// <summary>
        /// Provides a System.Int64 between 0 and <paramref name="max"/>.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        /// <returns>
        ///     A <see cref="long" /> greater than or equal to 0 and less than argument
        ///     <paramref name="max" />.
        /// </returns>
        long Long(long max);

        /// <summary>
        /// Provides a System.Int64 between 0 and System.Int64.MaxValue. 
        /// </summary>
        long Long();
    }
}