using System;
using System.Collections.Generic;

namespace Sharpy.Builder.Providers
{
    /// <summary>
    ///     Provides elements from various collections.
    /// </summary>
    public interface IElementProvider
    {
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
        /// <returns>
        ///     One of the elements from the arguments.
        /// </returns>
        T FromArgument<T>(T first, T second);

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
        /// <param name="third">
        ///     The second argument.
        /// </param>
        /// <returns>
        ///     One of the elements from the arguments.
        /// </returns>
        T FromArgument<T>(T first, T second, T third);

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
        /// <param name="third">
        ///     The second argument.
        /// </param>
        /// <param name="fourth">
        ///     The second argument.
        /// </param>
        /// <returns>
        ///     One of the elements from the arguments.
        /// </returns>
        T FromArgument<T>(T first, T second, T third, T fourth);

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
        /// <param name="third">
        ///     The second argument.
        /// </param>
        /// <param name="fourth">
        ///     The second argument.
        /// </param>
        /// <param name="fifth">
        ///     The second argument.
        /// </param>
        /// <returns>
        ///     One of the elements from the arguments.
        /// </returns>
        T FromArgument<T>(T first, T second, T third, T fourth, T fifth);

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
        /// <param name="third">
        ///     The second argument.
        /// </param>
        /// <param name="fourth">
        ///     The second argument.
        /// </param>
        /// <param name="fifth">
        ///     The second argument.
        /// </param>
        /// <param name="additional">
        ///     The additional arguments.
        /// </param>
        /// <returns>
        ///     One of the elements from the arguments.
        /// </returns>
        T FromArgument<T>(T first, T second, T third, T fourth, T fifth, params T[] additional);


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