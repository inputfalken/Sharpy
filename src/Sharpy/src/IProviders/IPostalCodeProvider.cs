namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>
    ///         Methods providing <see cref="string" /> representing postal codes.
    ///     </para>
    /// </summary>
    public interface IPostalCodeProvider {
        /// <summary>
        ///     Creates a string representing a postal code.
        /// </summary>
        /// <returns>
        ///     A string representing a postal code.
        /// </returns>
        string PostalCode();

        /// <summary>
        ///     Creates a string representing a postal code based on argument <paramref name="county" />.
        /// </summary>
        /// <param name="county">
        ///     The <paramref name="county" /> where
        /// </param>
        /// <returns>
        ///     A string representing a postal code.
        /// </returns>
        string PostalCode(string county);
    }
}