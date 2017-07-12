using System;
using System.Collections.Generic;
using System.Linq;
using static GeneratorAPI.Generator;

namespace GeneratorAPI.Linq {
    /// <summary>
    ///     Provides a set of static methods for <see cref="IGenerator{T}" />.
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> by casting each element to <typeparamref name="TResult" /> when invoking
        ///         <see cref="IGenerator.Generate" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">
        ///     The <see cref="IGenerator" /> that contains the elements to be cast to type
        ///     <typeparamref name="TResult" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations have been casted to type <typeparamref name="TResult" />.
        /// </returns>
        /// <typeparam name="TResult">The type to be used when casting.</typeparam>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="InvalidCastException">
        ///     A generated element cannot be cast to type
        ///     <typeparamref name="TResult" />.
        /// </exception>
        /// <example>
        ///     <code language="C#" region="Generator.Cast" source="Examples\Generator.cs" />
        /// </example>
        public static IGenerator<TResult> Cast<TResult>(this IGenerator generator) {
            var result = generator as IGenerator<TResult>;
            if (result != null) return result;
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => (TResult) generator.Generate());
        }

        /// <summary>
        ///     <para>
        ///         Releases the number given to <paramref name="amount" /> immediately from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <remarks>
        ///     <para>
        ///         This method is not lazy evaluated, for lazy evaluation see <see cref="Skip{TSource}" />.
        ///     </para>
        /// </remarks>
        /// <param name="generator">The <paramref name="generator" /> whose generations will be released.</param>
        /// <param name="amount">The number of generations to be released.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentException">Argument <paramref name="amount" /> is less than 0.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has been released by the number equal to argument
        ///     <paramref name="amount" />.
        /// </returns>
        public static IGenerator<TSource> Release<TSource>(this IGenerator<TSource> generator, int amount) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (amount == 0) return generator;
            if (amount < 0) throw new ArgumentException($"{nameof(amount)} can't be below ${amount}");
            return ReleaseIterator(generator, amount);
        }

        /// <summary>
        ///     <para>
        ///         Skips the number given to <paramref name="count" /> from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <param name="generator">The <paramref name="generator" /> whose generations will be skipped.</param>
        /// <param name="count">The number of generations to be released.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentException">Argument <paramref name="count" /> is less than 0.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has been skipped by the number equal to argument
        ///     <paramref name="count" />.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         This works the same way as <see cref="Release{TSource}"/> except that it's lazy evaluated.
        ///     </para>
        /// </remarks>
        public static IGenerator<TSource> Skip<TSource>(this IGenerator<TSource> generator, int count) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (count < 0) throw new ArgumentException($"{nameof(count)} Cant be negative");
            if (count == 0) return generator;
            var skipped = new Lazy<IGenerator<TSource>>(() => ReleaseIterator(generator, count));
            return Function(() => skipped.Value.Generate());
        }

        private static IGenerator<T> ReleaseIterator<T>(IGenerator<T> generator, int count) {
            for (var i = 0; i < count; i++) generator.Generate();
            return generator;
        }


        /// <summary>
        ///     <para>
        ///         Filters a <see cref="IGenerator{T}" /> based on a predicate.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If the <paramref name="predicate" /> is not satisfied, a new generation will occur.
        ///     </para>
        /// </remarks>
        /// <typeparam name="TSource">
        ///     The type to be used as input in the <paramref name="predicate" />.
        /// </typeparam>
        /// <param name="generator">The <see cref="IGenerator{T}" /> to filter.</param>
        /// <param name="predicate">A function to test each generated element for a condition.</param>
        /// <param name="threshold">The number of attempts before throwing an exception.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="predicate" /> is null.</exception>
        /// <exception cref="ArgumentException">The condition can't be matched within the <paramref name="threshold" />.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has satisfied the condition.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.Where" source="Examples\Generator.cs" />
        /// </example>
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
        ///         Exposes the element from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <param name="generator">The <paramref name="generator" /> whose elements is passed to <see cref="Action{T}" />.</param>
        /// <param name="action">The <see cref="Action{T}" /> which all generations will be passed to.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="action" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements has been exposed to <see cref="Action{T}" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.Do" source="Examples\Generator.cs" />
        /// </example>
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
        ///     The type returned by selector.
        /// </typeparam>
        /// <param name="generator">
        ///     The <paramref name="generator" /> whose generations will invoke the transform function.
        /// </param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements are the result of invoking the transform function on each element
        ///     genrated by
        ///     <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.Select" source="Examples\Generator.cs" />
        /// </example>
        public static IGenerator<TResult> Select<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, TResult> selector) {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => selector(generator.Generate()));
        }

        /// <summary>
        ///     <para>
        ///         Projects each generation into a new form and includes a counter for the current generation.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="generator" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type returned by selector.
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
        ///         Creates an <see cref="IEnumerable{T}" /> from <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of <paramref name="generator" />.
        /// </typeparam>
        /// <param name="generator">The <paramref name="generator" /> to generate from.</param>
        /// <param name="count">The number of elements to be returned in the <see cref="IEnumerable{T}"/>.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentException">Argument <paramref name="count"></paramref> is less or equal to zero.</exception>
        /// <returns>
        ///     An <see cref="IEnumerable{T}" /> that contains the specified number of elements generated from the argument
        ///     <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.Take" source="Examples\Generator.cs" />
        /// </example>
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
        ///         Creates a <see cref="IGenerator{T}" /> by flattening a <see cref="IGenerator{T}" /> with another
        ///         <see cref="IGenerator{T}" /> for each invokation of <see cref="IGenerator{T}.Generate" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements from <paramref name="generator" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The return type of function <paramref name="selector" />.
        /// </typeparam>
        /// <param name="generator">
        ///     The generator to be flattened.
        /// </param>
        /// <param name="selector">
        ///     A transform function to apply to each element.
        /// </param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="selector" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements have been flattened by another <see cref="IGenerator{T}" />.
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
        ///         Creates a <see cref="IGenerator{T}" /> by flattening a <see cref="IGenerator{T}" /> with another
        ///         <see cref="IGenerator{T}" /> for each invokation of <see cref="IGenerator{T}.Generate" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements from <paramref name="generator" />.
        /// </typeparam>
        /// <typeparam name="TGenerator">
        ///     The return type of function <paramref name="selector" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The return type from function <paramref name="resultSelector" />.
        /// </typeparam>
        /// <param name="generator">
        ///     The generator to be flattened.
        /// </param>
        /// <param name="selector">
        ///     A transform function to apply to each element.
        /// </param>
        /// <param name="resultSelector">
        ///     A transform function with the result from argument <paramref name="selector" /> and the element from
        ///     <paramref name="generator" />.
        /// </param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="selector" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements have been flattened by an <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<TResult> SelectMany<TSource, TGenerator, TResult>(
            this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TGenerator>> selector,
            Func<TSource, TGenerator, TResult> resultSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            return generator.SelectMany(a => selector(a).SelectMany(r => Function(() => resultSelector(a, r))));
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
        /// <param name="resultSelector">A function that specifies how to merge the elements from the two generators.</param>
        /// <exception cref="ArgumentNullException">Argument <paramref name="first" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="second" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> that contains merged elements of two input generators.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.Zip" source="Examples\Generator.cs" />
        /// </example>
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
        /// <param name="count">The number of elements to be returned in the <see cref="Dictionary{TKey,TValue}"/>.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="generator" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TValue">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
        /// <returns>
        ///     A <see cref="Dictionary{TKey,TValue}" /> that contains values of type TElement selected from the input
        ///     <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.ToDictionary" source="Examples\Generator.cs" />
        /// </example>
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
        /// <param name="count">The number of elements to be returned in the <see cref="List{T}"/>.</param>
        /// <typeparam name="TSource">The type of the elements of source..</typeparam>
        /// <returns>
        ///     A <see cref="List{T}" /> that contains elements generated from the input <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.ToList" source="Examples\Generator.cs" />
        /// </example>
        public static List<TSource> ToList<TSource>(this IGenerator<TSource> generator, int count) {
            return generator.Take(count).ToList();
        }

        /// <summary>
        ///     <para>
        ///         Creates an array from a <see cref="IGenerator{T}" />.
        ///     </para>
        /// </summary>
        /// <param name="generator">A <see cref="IGenerator{T}" /> to create an <see cref="Array" /> from.</param>
        /// <param name="length">The number of elements to be returned in the <see cref="Array"/>.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>
        ///     An array that contains elements generated from the input <paramref name="generator" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Generator.ToArray" source="Examples\Generator.cs" />
        /// </example>
        public static TSource[] ToArray<TSource>(this IGenerator<TSource> generator, int length) {
            return generator.Take(length).ToArray();
        }
    }
}