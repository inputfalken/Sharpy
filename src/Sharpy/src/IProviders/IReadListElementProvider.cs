using System.Collections.Generic;

namespace Sharpy.IProviders {
    public interface IReadListElementProvider {
        /// <summary>
        ///     Returns an element from the argument <paramref name="list" />.
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

        /// <summary>
        ///     Returns one of the arguments.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the elements in argument <paramref name="arguments" />.
        /// </typeparam>
        /// <param name="arguments">
        ///     The arguments separated by comma.
        /// </param>
        /// <returns>
        ///     An from the <paramref name="arguments"/>.
        /// </returns>
        T Argument<T>(params T[] arguments);
    }
}