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
        double Double(double max);

        /// <summary>
        /// Provides a System.Double between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        double Double(double min, double max);
    }
}