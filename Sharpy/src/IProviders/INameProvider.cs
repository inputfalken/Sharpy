using Sharpy.Enums;
using Sharpy.Implementation;

namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>Represents a method for providing Names.</para>
    /// </summary>
    public interface INameProvider {
        /// <summary>
        /// <para>Provides a first name based on gender.</para>
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        string FirstName(Gender gender);

        /// <summary>
        /// <para>Provides a first name.</para>
        /// </summary>
        /// <returns></returns>
        string FirstName();

        /// <summary>
        /// <para>Provides a last name.</para>
        /// </summary>
        /// <returns></returns>
        string LastName();
    }
}