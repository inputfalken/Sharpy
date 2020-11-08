namespace Sharpy.Builder.IProviders
{
    /// <summary>
    ///  Methods providing <see cref="string" /> representing a phone number.
    /// </summary>
    public interface IPhoneNumberProvider
    {
        /// <summary>
        ///     Provides a <see cref="string" /> representing a phone number.
        /// </summary>
        string PhoneNumber();

        /// <summary>
        ///     Provides a <see cref="string" /> representing a phone number based on argument <paramref name="length"/>.
        /// </summary>
        string PhoneNumber(int length);
    }
}