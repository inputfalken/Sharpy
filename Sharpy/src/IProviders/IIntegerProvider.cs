namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Methods providing <see cref="int"/>.</para>
    /// </summary>
    public interface IIntegerProvider {
        /// <summary>
        ///     <para>
        ///         Returns a <see cref="int"/> from 0 to argument <paramref name="max"/>.
        ///     </para>
        ///     <param name="max">The maximum <see cref="int"/> value.</param>
        /// </summary>
        int Integer(int max);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="int"/> within argument <paramref name="min"/> to argument <paramref name="max"/>.
        ///     </para>
        /// </summary>
        int Integer(int min, int max);

        /// <summary>
        ///     <para>
        ///         Returns and <see cref="int"/> within 0 and <see cref="int.MaxValue"/>.
        ///     </para>
        /// </summary>
        /// <returns></returns>
        int Integer();
    }
}