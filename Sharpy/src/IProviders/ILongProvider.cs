namespace Sharpy.IProviders {
    /// <summary>
    /// <para>Represents methods for giving Longs.</para>
    /// </summary>
    public interface ILongProvider {
        /// <summary>
        ///     <para>Returns a long from min (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        long Long(long min, long max);

        /// <summary>
        ///     <para>Returns a long from 0 (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
        long Long(long max);

        /// <summary>
        ///     <para>Returns a long within all possible values of long</para>
        /// </summary>
        long Long();
    }
}