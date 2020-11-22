using System;
using System.Collections.Generic;

namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Provides elements from various collections.
    /// </summary>
    public interface ICollectionElementProvider
    {
        /// <summary>
        ///     Provides an element from the <paramref name="list" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the elements in argument <paramref name="list" />.
        /// </typeparam>
        T FromList<T>(IReadOnlyList<T> list);

        /// <summary>
        ///     Provides an element from the <paramref name="span" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the elements in argument <paramref name="span" />.
        /// </typeparam>
        T FromSpan<T>(ReadOnlySpan<T> span);
        
        /// <summary>
        ///     Provides an element from the <paramref name="span" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The generic type of the elements in argument <paramref name="span" />.
        /// </typeparam>
        T FromSpan<T>(Span<T> span);
    }
}