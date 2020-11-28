namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods providing System.Decimal
    /// </summary>
    public interface IDecimalProvider
    {
        /// <summary>
        ///     Provides a System.Int32 between 0 and <paramref name="max" />.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        decimal Decimal(decimal max);

        /// <summary>
        ///     Provides a System.Int32 between <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        decimal Decimal(decimal min, decimal max);

        /// <summary>
        ///     Provides a System.Int32 between 0 and System.Int32.MaxValue.
        /// </summary>
        decimal Decimal();
    }
}