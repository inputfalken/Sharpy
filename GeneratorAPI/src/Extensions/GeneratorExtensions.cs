using System;
using System.Collections.Generic;
using System.Linq;
using static GeneratorAPI.Generator;

namespace GeneratorAPI.Extensions {
    public static class GeneratorExtensions {
        /// <summary>
        ///     <para>
        ///         Casts each element from <see cref="IGenerator.Generate" /> to <see cref=" IGenerator&lt;TResult&gt;" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">The <see cref="IGenerator" /> that contains the elements to be cast to type TResult.</param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> that contains each element of the source <see cref="IGenerator" /> cast to the
        ///     specified type.
        /// </returns>
        /// <typeparam name="TResult">The type to cast the elements of <paramref name="generator" /> to.</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="generator" /> is null.</exception>
        /// <exception cref="InvalidCastException">
        ///     An element in the sequence cannot be cast to type
        ///     <typeparamref name="TResult" />.
        /// </exception>
        public static IGenerator<TResult> Cast<TResult>(this IGenerator generator) {
            var result = generator as IGenerator<TResult>;
            if (result != null) return result;
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => (TResult) generator.Generate());
        }

        /// <summary>
        ///     <para>
        ///         Filters a <see cref="IGenerator{T}" /> based on a predicate.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <param name="generator">The <see cref="IGenerator{T}" /> to filter.</param>
        /// <param name="predicate">A function to test each generated element for a condition.</param>
        /// <param name="threshold">The number of attempts before throwing an exception.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate" /> is null.</exception>
        /// <exception cref="ArgumentException">If the condition can't be matched within the <paramref name="threshold" />.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has satisfied the condition.
        /// </returns>
        public static IGenerator<TSource> Where<TSource>(this IGenerator<TSource> generator,
            Func<TSource, bool> predicate,
            int threshold = 100000) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => {
                for (var i = 0; i < threshold; i++) {
                    var generation = generator.Generate();
                    if (predicate(generation)) return generation;
                }
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts. ");
            });
        }


        /// <summary>
        ///     <para>
        ///         Exposes the type from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <param name="generator">Then <paramref name="generator" /> whose elements is passed to <see cref="Action{T}" />.</param>
        /// <param name="action">The <see cref="Delegate" /> which all generations will be passed to.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="action" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has been exposed by <see cref="Action{T}" />.
        /// </returns>
        public static IGenerator<TSource> Do<TSource>(this IGenerator<TSource> generator, Action<TSource> action) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => {
                var generation = generator.Generate();
                action(generation);
                return generation;
            });
        }

        /// <summary>
        ///     <para>
        ///         Projects each generation into a new form.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the value returned by selector.
        /// </typeparam>
        /// <param name="generator">
        ///     The <paramref name="generator" /> whose generations will invoke the transform function.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements are the result of invoking the transform function on each element of
        ///     <paramref name="generator" />.
        /// </returns>
        public static IGenerator<TResult> Select<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, TResult> selector) {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => selector(generator.Generate()));
        }

        /// <summary>
        ///     <para>
        ///         Projects each element of a sequence into a new form by incorporating a counter for the current generation.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the value returned by selector.
        /// </typeparam>
        /// <param name="generator">
        ///     The <paramref name="generator" /> whose generations will invoke the transform function.
        /// </param>
        /// <param name="selector">
        ///     A transform function to apply to each source element; the second parameter of the
        ///     function represents a counter for the generation.
        /// </param>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements are the result of invoking the transform function on each element of
        ///     <paramref name="generator" />.
        /// </returns>
        public static IGenerator<TResult> Select<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, int, TResult> selector) {
            var count = 0;
            return generator.Select(source => selector(source, count++));
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref=" IEnumerable&lt;TSource&gt;" /> by invoking <see cref="IGenerator{T}.Generate" /> with
        ///         argument <paramref name="count" /> amount of times.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of source.
        /// </typeparam>
        /// <param name="generator">The <paramref name="generator" /> to generate from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentException">When argument <paramref name="count"></paramref> is less or equal to zero.</exception>
        /// <returns>
        ///     An <see cref=" IEnumerable&lt;TSource&gt;" /> with its count equal to argument <paramref name="count" />.
        /// </returns>
        public static IEnumerable<TSource> Take<TSource>(this IGenerator<TSource> generator, int count) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (count <= 0) throw new ArgumentException($"{nameof(count)} Must be more than zero");
            //Is needed so the above if statement is checked.
            return Iterator(count, generator);
        }

        private static IEnumerable<TSource> Iterator<TSource>(int count, IGenerator<TSource> generator) {
            for (var i = 0; i < count; i++) yield return generator.Generate();
        }

        /// <summary>
        ///     <para>
        ///         Projects each element of a generator to an <see cref="IGenerator{T}" /> and flattens the resulting generators
        ///         into one generator.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The result from invoking <see cref="IGenerator{T}.Generate" /> on
        ///     <see cref=" IGenerator&lt;TSource&gt;" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the elements of the <paramref name="generator" /> returned by selector.
        /// </typeparam>
        /// <param name="generator">
        ///     The <paramref name="generator" /> to flatmap the result from
        ///     <paramref name="selector" />.
        /// </param>
        /// <param name="selector">A transform function to apply to each element..</param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="selector" /> is null.</exception>
        /// <returns>
        ///     An <see cref="IGenerator{T}" /> whose elements are the result of flattening a nested <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<TResult> SelectMany<TSource, TResult>(
            this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TResult>> selector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return Function(() => selector(generator.Generate()).Generate());
        }

        /// <summary>
        ///     <para>
        ///         Projects each element of a generator to an <see cref="IGenerator{T}" /> and flattens the resulting generators
        ///         into one generator.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The result from invoking <see cref="IGenerator{T}.Generate" /> on
        ///     <see cref=" IGenerator&lt;TSource&gt;" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the elements of the <paramref name="generator" /> returned by selector.
        /// </typeparam>
        /// <typeparam name="TCompose">
        ///     The result from composing <typeparamref name="TSource"></typeparamref> with <typeparamref name="TResult" />.
        /// </typeparam>
        /// <param name="generator">
        ///     The <paramref name="generator" /> to flatmap the result from
        ///     <paramref name="selector" />.
        /// </param>
        /// <param name="selector">A transform function to apply to each element..</param>
        /// <param name="resultSelector">
        ///     The result from composing <typeparamref name="TSource"></typeparamref> with
        ///     <typeparamref name="TResult" />.
        /// </param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="selector" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>
        ///     TODO
        /// </returns>
        public static IGenerator<TCompose> SelectMany<TSource, TResult, TCompose>(
            this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TResult>> selector,
            Func<TSource, TResult, TCompose> resultSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            return generator.SelectMany(a => selector(a)
                .SelectMany(r => Function(() => resultSelector(a, r))));
        }

        /// <summary>
        ///     <para>
        ///         Applies a specified function to the corresponding elements of two Generators, producing a Generator of the
        ///         results.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of the first generator.
        /// </typeparam>
        /// <typeparam name="TSecond">
        ///     The type of the elements of the second generator.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the result from argument <paramref name="resultSelector" />.
        /// </typeparam>
        /// <param name="first">The first generator to merge.</param>
        /// <param name="second">The second generator to merge.</param>
        /// <param name="resultSelector">A function that specifies how to merge the elements from the two sequences.</param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="first" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="second" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> that contains merged elements of two input generators.
        /// </returns>
        public static IGenerator<TResult> Zip<TSource, TSecond, TResult>(this IGenerator<TSource> first,
            IGenerator<TSecond> second,
            Func<TSource, TSecond, TResult> resultSelector) {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            return Function(() => resultSelector(first.Generate(), second.Generate()));
        }

        /// <summary>
        ///     <para>
        ///         Creates an <see cref="Dictionary{TKey,TValue}" /> from a <see cref="IGenerator{T}" /> according to specified
        ///         key selector and element selector functions.
        ///     </para>
        /// </summary>
        /// <param name="generator">A <paramref name="generator" /> to create a <see cref="Dictionary{TKey,TValue}" /> from.</param>
        /// <param name="count">The number of elements to generate.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="generator" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TValue">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
        /// <returns>
        ///     A <see cref="Dictionary{TKey,TValue}" /> that contains values of type TElement selected from the input
        ///     <paramref name="generator" />.
        /// </returns>
        public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(this IGenerator<TSource> generator,
            int count, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector) {
            return generator.Take(count).ToDictionary(keySelector, elementSelector);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="List{T}" /> from a <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">The <see cref="IGenerator{T}" /> to create a <see cref="List{T}" /> from.</param>
        /// <param name="count">The number of elements to generate.</param>
        /// <typeparam name="TSource">The type of the elements of source..</typeparam>
        /// <returns>
        ///     A <see cref="List{T}" /> that contains elements generated from the input <paramref name="generator" />.
        /// </returns>
        public static List<TSource> ToList<TSource>(this IGenerator<TSource> generator, int count) {
            return generator.Take(count).ToList();
        }

        /// <summary>
        ///     <para>
        ///         Creates an array from a <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">A <see cref="IGenerator{T}" /> to create an <see cref="Array" /> from.</param>
        /// <param name="length">The number of elements to generate.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>
        ///     An array that contains elements generated from the input <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.ToArray" source="..\Examples\Generator.cs" />
        /// </example>
        public static TSource[] ToArray<TSource>(this IGenerator<TSource> generator, int length) {
            return generator.Take(length).ToArray();
        }
    }
}