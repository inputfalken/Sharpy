using System.Collections.Generic;

namespace Sharpy.IProviders {
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

    /// <summary>
    ///     Returns elements from arguments.
    /// </summary>
    public interface IArgumentProvider {
        /// <summary>
        ///     Returns one of the arguments.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the arguments.
        /// </typeparam>
        /// <param name="first">
        ///     The first argument.
        /// </param>
        /// <param name="second">
        ///     The second argument.
        /// </param>
        /// <param name="additional">
        ///     The additional arguments.
        /// </param>
        /// <returns>
        ///     An from the <paramref name="additional" />.
        /// </returns>
        T Argument<T>(T first, T second, params T[] additional);
    }
}