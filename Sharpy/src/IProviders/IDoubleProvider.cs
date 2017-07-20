namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>
    ///         Methods providing <see cref="double" />.
    ///     </para>
    /// </summary>
    public interface IDoubleProvider {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="double" /> from 0 to 1.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A <see cref="double" /> greater than or equal to 0 and less than 1.
        /// </returns>
        double Double();

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="double" /> from 0 to argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="max">
        ///     The maximum value.
        /// </param>
        /// <returns>
        ///     A <see cref="double" /> greater than or equal to 0 and less than argument <paramref name="max" />.
        /// </returns>
        double Double(double max);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="double" /> from argument <paramref name="min" /> to argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="min">
        ///     The minimum value.
        /// </param>
        /// <param name="max">
        ///     The maximum value.
        /// </param>
        /// <returns>
        ///     A <see cref="double" /> greater than or equal to argument <paramref name="min" /> and less than argument
        ///     <paramref name="max" />.
        /// </returns>
        double Double(double min, double max);
    }
}