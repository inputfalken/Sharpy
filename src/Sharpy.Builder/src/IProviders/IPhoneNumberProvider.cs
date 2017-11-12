namespace Sharpy.Builder.IProviders {
    /// <summary>
    ///     <para>
    ///         Methods providing <see cref="string" /> representing a phone number.
    ///     </para>
    /// </summary>
    public interface IPhoneNumberProvider {
        /// <summary>
        ///     Creates a <see cref="string" /> representing a phone number.
        /// </summary>
        /// <returns>
        ///     A <see cref="string" /> representing a phone number.
        /// </returns>
        string PhoneNumber();

        /// <summary>
        ///     Creates a <see cref="string" /> representing a phone number based on argument <paramref name="length"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="string" /> representing a phone number.
        /// </returns>
        string PhoneNumber(int length);
    }
}