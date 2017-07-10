namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Methods providing <see cref="long" />.</para>
    /// </summary>
    public interface ILongProvider {
        /// <summary>
        ///     <para>
        ///         Returns a <see cref="long" /> that is greater than or equal to argument <paramref name="min" /> and less
        ///         than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        long Long(long min, long max);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="long" /> that is greater than or equal to 0 and less
        ///         than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        long Long(long max);

        /// <summary>
        ///     <para>
        ///         <para>
        ///             Returns a <see cref="long" /> that is greater than or equal to <see cref="long.MinValue"/> and less
        ///             than <see cref="long.MaxValue" />.
        ///         </para>
        ///     </para>
        /// </summary>
        long Long();
    }
}