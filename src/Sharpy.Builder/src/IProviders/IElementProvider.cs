using System.Collections.Generic;

namespace Sharpy.Builder.IProviders {
    /// <summary>
    ///     Returns elements from <see cref="IReadOnlyList{T}" />.
    /// </summary>
    public interface IElementProvider {
        /// <summary>
        ///     Returns an element from the <paramref name="list" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the elements in argument <paramref name="list" />.
        /// </typeparam>
        /// <param name="list">
        ///     The <see cref="IReadOnlyList{T}" /> where an element will be taken.
        /// </param>
        /// <returns>
        ///     An element from the provided <see cref="IReadOnlyList{T}" />.
        /// </returns>
        T Element<T>(IReadOnlyList<T> list);
    }
}