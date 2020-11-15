using Sharpy.Builder.Enums;

namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///  Methods providing System.String representing names.
    /// </summary>
    public interface INameProvider
    {
        /// <summary>
        ///      Provides a string representing a first name.
        /// </summary>
        /// <param name="gender">
        ///     The gender of the first name.
        /// </param>
        /// <returns>
        ///     A string representing a first name and taking the argument <see cref="Gender" /> to account.
        /// </returns>
        string FirstName(Gender gender);

        /// <summary>
        ///     Provides a string representing a first name.
        /// </summary>
        /// <returns>
        ///     A string representing a first name.
        /// </returns>
        string FirstName();

        /// <summary>
        ///     Provides a string representing a last name.
        /// </summary>
        /// <returns>
        ///     A string representing a last name.
        /// </returns>
        string LastName();
    }
}