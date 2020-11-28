namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Methods providing System.String representing a phone number.
    /// </summary>
    public interface IPhoneNumberProvider
    {
        /// <summary>
        ///     Provides a System.String representing a phone number.
        /// </summary>
        string PhoneNumber();

        /// <summary>
        ///     Provides a System.String representing a phone number based on argument <paramref name="length" />.
        /// </summary>
        string PhoneNumber(int length);
    }
}