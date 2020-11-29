namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods providing System.Char.
    /// </summary>
    public interface ICharProvider
    {
        /// <summary>
        ///     Provides a System.Char between 0 and System.Int32.MaxValue.
        /// </summary>
        char Char();

        /// <summary>
        ///     Provides a System.Char between 0 and <paramref name="max" />.
        /// </summary>
        /// <param name="max">
        ///     The inclusive maximum bound.
        /// </param>
        char Char(char max);

        /// <summary>
        ///     Provides a System.Char between <paramref name="min" /> and <paramref name="max" />.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The inclusive maximum bound.
        /// </param>
        char Char(char min, char max);
    }
}