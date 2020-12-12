namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods providing System.Single.
    /// </summary>
    public interface IFloatProvider
    {
        /// <summary>
        ///     Provides a System.Single between <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        float Float(in float min, in float max);

        /// <summary>
        ///     Provides a System.Single between 0 and <paramref name="max" />.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        float Float(in float max);

        /// <summary>
        ///     Provides a System.Single between 0 and System.Single.MaxValue.
        /// </summary>
        float Float();
    }
}