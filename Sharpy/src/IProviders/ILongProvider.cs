namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Methods providing <see cref="long"/>.</para>
    /// </summary>
    public interface ILongProvider {
        /// <summary>
        ///     <para>
        ///         Returns a <see cref="long"/> from argument <paramref name="min"/> to argument <paramref name="max"/>. 
        ///     </para>
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maxium value.</param>
        long Long(long min, long max);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="long"/> from 0 to argument <paramref name="max"/>.
        ///     </para>
        /// </summary>
        /// <param name="max">The maximum value.</param>
        long Long(long max);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="long"/> within <see cref="long.MinValue"/> and <see cref="long.MaxValue"/>.
        ///     </para>
        /// </summary>
        long Long();
    }
}