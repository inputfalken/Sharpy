using System;

namespace Sharpy.Builder.IProviders {
    /// <summary>
    ///   Methods providing <see cref="string" /> representing security numbers.
    /// </summary>
    public interface ISecurityNumberProvider {
        /// <summary>
        ///     Provides a security number based on argument <paramref name="date" />.
        /// </summary>
        string SecurityNumber(DateTime date);

        /// <summary>
        ///     Provides a security number
        /// </summary>
        string SecurityNumber();
    }
}