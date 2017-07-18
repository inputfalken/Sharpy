namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Methods providing <see cref="int" />.</para>
    /// </summary>
    public interface IIntegerProvider {
        /// <summary>
        ///     <para>
        ///         Returns a <see cref="int" /> that is greater than or equal to 0 and less
        ///         than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        int Integer(int max);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="int" /> that is greater than or equal to argument <paramref name="min" /> and less
        ///         than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="min">
        ///     The inclusive minimum bound
        /// </param>
        /// <param name="max">
        ///     The exclusive maximum bound.
        /// </param>
        int Integer(int min, int max);


        /// <summary>
        ///     <para>
        ///         <para>
        ///             Returns a <see cref="int" /> that is greater than or equal to 0 and less
        ///             than <see cref="int.MaxValue" />.
        ///         </para>
        ///     </para>
        /// </summary>
        int Integer();
    }
}