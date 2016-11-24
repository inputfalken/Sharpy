namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Represents methods for providing doubles.</para>
    /// </summary>
    public interface IDoubleProvider {
        /// <summary>
        ///     <para>Returns a double from 0 to 1.</para>
        /// </summary>
        double Double();

        /// <summary>
        ///     <para>Returns a double from 0 to max.</para>
        /// </summary>
        /// <param name="max">The maximum value.</param>
        double Double(double max);

        /// <summary>
        ///     <para>Returns a double from min to max.</para>
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        double Double(double min, double max);
    }
}