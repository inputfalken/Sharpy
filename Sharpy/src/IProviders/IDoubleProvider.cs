namespace Sharpy.IProviders {
    /// <summary>
    /// <para>Represents methods for giving doubles.</para>
    /// </summary>
    public interface IDoubleProvider {
        /// <summary>
        ///     <para>Returns a generated double from 0 to 1</para>
        /// </summary>
        double Double();

        /// <summary>
        ///     <para>Returns a generated long from 0 (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
        double Double(double max);

        /// <summary>
        ///     <para>Returns a generated double from min (inclusive) to max (exclusive)</para>
        /// </summary>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        double Double(double min, double max);
    }
}