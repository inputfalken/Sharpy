using System;
using System.Collections.Generic;
using RandomExtended;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Randomizes <see cref="IReadOnlyList{T}" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class ElementRandomizer : IElementProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="ElementRandomizer" />.
        /// </summary>
        public ElementRandomizer(Random random)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }


        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second, in T third)
        {
            return _random.Argument(in first, in second, in third);
        }

        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second, in T third, in T fourth)
        {
            return _random.Argument(in first, in second, in third, in fourth);
        }

        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second, in T third, in T fourth, in T fifth)
        {
            return _random.Argument(in first, in second, in third, in fourth, in fifth);
        }

        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second, in T third, in T fourth, in T fifth, params T[] additional)
        {
            return _random.Argument(in first, in second, in third, in fourth, in fifth, additional);
        }

        /// <summary>
        ///     Returns a randomized element from the arguments supplied.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the arguments.
        /// </typeparam>
        /// <param name="first">
        ///     First argument.
        /// </param>
        /// <param name="second">
        ///     Second argument.
        /// </param>
        /// <returns>
        ///     One of the arguments supplied.
        /// </returns>
        public T FromArgument<T>(in T first, in T second)
        {
            return _random.Argument(in first, in second);
        }

        /// <summary>
        ///     Returns a randomized element from the argument <paramref name="list" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the elements inside the  <paramref name="list" /> argument.
        /// </typeparam>
        /// <param name="list">
        ///     The collection of elements.
        /// </param>
        /// <returns>
        ///     One of the elements inside argument <paramref name="list" />.
        /// </returns>
        public T FromList<T>(in IReadOnlyList<T> list)
        {
            return _random.ListElement(in list);
        }

        /// <inheritdoc />
        public T FromSpan<T>(in ReadOnlySpan<T> span)
        {
            return _random.SpanElement(in span);
        }

        /// <inheritdoc />
        public T FromSpan<T>(in Span<T> span)
        {
            return _random.SpanElement(in span);
        }
    }
}