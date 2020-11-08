namespace Sharpy.Builder.IProviders {
    /// <summary>
    ///   Methods providing <see cref="long" />.
    /// </summary>
    public interface ILongProvider {
        /// <summary>
        ///     <para>
        ///         Provides a <see cref="long" /> that is greater than or equal to argument <paramref name="min" /> and less
        ///         than argument <paramref name="max" />.
        ///     </para>
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
        ///     <para>
        ///         Creates a <see cref="long" /> that is greater than or equal to 0 and less
        ///         than argument <paramref name="max" />.
        ///     </para>
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
        ///     <para>
        ///         <para>
        ///             Creates a <see cref="long" /> that is greater than or equal to 0 and less
        ///             than <see cref="long.MaxValue" />.
        ///         </para>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A <see cref="long" /> greater than or equal to 0 and less than <see cref="long.MaxValue" />.
        /// </returns>
        long Long();
    }
}