using System.Collections.Generic;

namespace Sharpy.IProviders {
    public interface IListElementPicker {
        /// <summary>
        ///     Returns an element from the argument <paramref name="list" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the elements in argument <paramref name="list" />.
        /// </typeparam>
        /// <param name="list">
        ///     The <see cref="IReadOnlyList{T}" /> to choose an element from.
        /// </param>
        /// <returns>
        ///     An element from the provided <see cref="IReadOnlyList{T}" />.
        /// </returns>
        T TakeElement<T>(IReadOnlyList<T> list);

        /// <summary>
        ///     Returns one of the arguments.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the elements in argument <paramref name="arguments" />.
        /// </typeparam>
        /// <param name="arguments">
        ///     The arguments provided.
        /// </param>
        /// <returns>
        ///     An element from the provided <paramref name="arguments" />.
        /// </returns>
        T TakeArgument<T>(params T[] arguments);
    }
}