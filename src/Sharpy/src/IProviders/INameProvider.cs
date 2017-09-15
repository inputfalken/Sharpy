using Sharpy.Enums;

namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>
    ///         Methods providing <see cref="string" /> representing names.
    ///     </para>
    /// </summary>
    public interface INameProvider {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="string" /> representing a first name.
        ///     </para>
        /// </summary>
        /// <param name="gender">
        ///     The gender of the first name.
        /// </param>
        /// <returns>
        ///     A string representing a first name and taking the argument <see cref="Gender" /> to account.
        /// </returns>
        string FirstName(Gender gender);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="string" /> representing a first name.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A string representing a first name.
        /// </returns>
        string FirstName();

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="string" /> representing a last name.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A string representing a last name.
        /// </returns>
        string LastName();
    }
}