namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///  Methods providing System.Double.
    /// </summary>
    public interface IDoubleProvider
    {
        /// <summary>
        /// Provides a System.Double between 0 and 1.
        /// </summary>
        double Double();

        /// <summary>
        /// Provides a System.Double between 0 and <paramref name="max"/>.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        double Double(double max);

        /// <summary>
        /// Provides a System.Double between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        double Double(double min, double max);
    }
}