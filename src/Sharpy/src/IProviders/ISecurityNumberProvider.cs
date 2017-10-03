using System;

namespace Sharpy.IProviders {
    /// <summary>
    ///     <para>
    ///         Methods providing <see cref="string" /> representing security numbers.
    ///     </para>
    /// </summary>
    public interface ISecurityNumberProvider {
        /// <summary>
        ///     Creates a security number based on argument <paramref name="date" />.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        string SecurityNumber(DateTime date);

        /// <summary>
        ///     Creates a security number
        /// </summary>
        /// <returns></returns>
        string SecurityNumber();
    }
}