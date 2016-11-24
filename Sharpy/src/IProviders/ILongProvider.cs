namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Represents methods for providing Longs.</para>
    /// </summary>
    public interface ILongProvider {
        /// <summary>
        ///     <para>Returns a long from min to max. </para>
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maxium value.</param>
        long Long(long min, long max);

        /// <summary>
        ///     <para>Returns a long from 0 to max.</para>
        /// </summary>
        /// <param name="max">The maximum value.</param>
        long Long(long max);

        /// <summary>
        ///     <para>Returns a long within min and max value of long.</para>
        /// </summary>
        long Long();
    }
}