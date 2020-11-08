namespace Sharpy.Builder.IProviders
{
    /// <summary>
    ///  Methods providing System.Int32.
    /// </summary>
    public interface IIntegerProvider
    {
        /// <summary>
        /// Provides a System.Int32 between 0 and <paramref name="max"/>.
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        int Integer(int max);

        /// <summary>
        /// Provides a System.Int32 between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound.
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        int Integer(int min, int max);

        /// <summary>
        /// Provides a System.Int32 between 0 and System.Int32.MaxValue. 
        /// </summary>
        int Integer();
    }
}