namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>
    ///         Methods providing <see cref="double"/>.
    ///     </para>
    /// </summary>
    public interface IDoubleProvider {
        /// <summary>
        ///     <para>
        ///         Returns a <see cref="double"/> from 0 to 1.
        ///     </para>
        /// </summary>
        double Double();

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="double"/> from 0 to argument <paramref name="max"/>.
        ///     </para>
        /// </summary>
        /// <param name="max">The maximum value.</param>
        double Double(double max);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="double"/> from argument <paramref name="min"/> to argument <paramref name="max"/>.
        ///     </para>
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        double Double(double min, double max);
    }
}