using Sharpy.Enums;

namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Methods providing <see cref="string" /> representing names.</para>
    /// </summary>
    public interface INameProvider {
        /// <summary>
        ///     <para>Provides a first name based on gender.</para>
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        string FirstName(Gender gender);

        /// <summary>
        ///     <para>Provides a first name.</para>
        /// </summary>
        /// <returns></returns>
        string FirstName();

        /// <summary>
        ///     <para>Provides a last name.</para>
        /// </summary>
        /// <returns></returns>
        string LastName();
    }
}