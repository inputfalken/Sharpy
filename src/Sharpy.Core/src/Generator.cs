using System;
using System.Collections.Generic;
using Sharpy.Core.Implementations;
using Sharpy.Core.Linq;

namespace Sharpy.Core {
    /// <summary>
    ///     Provides a set of static members for creating <see cref="IGenerator{T}" />.
    /// </summary>
    public static class Generator {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will use the same
        ///         <typeparamref name="TSource" />.
        ///     </para>
        /// </summary>
        /// <param name="source">The type to generate from.</param>
        /// <typeparam name="TSource">The type of argument <paramref name="source" /></typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will use argument <paramref name="source" />.
        /// </returns>
        public static IGenerator<TSource> Create<TSource>(TSource source) {
            return new Fun<TSource>(() => source);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will use the same
        ///         <typeparamref name="TSource" />.
        ///     </para>
        /// </summary>
        /// <param name="lazy">The lazy evaluated value.</param>
        /// <typeparam name="TSource">The type of <paramref name="lazy" />.</typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will use the lazy evaluated value from <paramref name="lazy" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="lazy" /> is null.</exception>
        public static IGenerator<TSource> Lazy<TSource>(Lazy<TSource> lazy) {
            if (lazy == null) throw new ArgumentNullException(nameof(lazy));
            return new Fun<TSource>(() => lazy.Value);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will use the same
        ///         <typeparamref name="TSource" />.
        ///     </para>
        /// </summary>
        /// <param name="fn">The lazy evaluated value.</param>
        /// <typeparam name="TSource">The type of <paramref name="fn" />.</typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will use the lazy evaluated value from <paramref name="fn" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="fn" /> is null.</exception>
        public static IGenerator<TSource> Lazy<TSource>(Func<TSource> fn) => Lazy(new Lazy<TSource>(fn));

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will
        ///         invoke the argument <paramref name="fn" />.
        ///     </para>
        /// </summary>
        /// <param name="fn">The function to invoke for each generation.</param>
        /// <typeparam name="TSource">The type returned from argument <paramref name="fn" />.</typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations will invoke argument <paramref name="fn" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="fn" /> is null.</exception>
        public static IGenerator<TSource> Function<TSource>(Func<TSource> fn) => new Fun<TSource>(fn);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> based on an <see cref="IEnumerable{T}" /> which resets if the end is
        ///         reached.
        ///     </para>
        /// </summary>
        /// <param name="enumerable">The <see cref="IEnumerable{T}" /> to create a <see cref="IGenerator{T}" /> from.</param>
        /// <typeparam name="TSource">The type of the <paramref name="enumerable" />.</typeparam>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose elements comes from argument <paramref name="enumerable" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="enumerable" /> is null.</exception>
        public static IGenerator<TSource> CircularSequence<TSource>(IEnumerable<TSource> enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return new Seq<TSource>(enumerable);
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> where each invocation of <see cref="IGenerator{T}.Generate" /> will
        ///         return a randomized <see cref="int" /> that is
        ///         greater than or equal to argument <paramref name="min" /> and less than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned. Argument <paramref name="max" /> must be greater than or
        ///     equal to
        ///     argument <paramref name="min" />.
        /// </param>
        /// <param name="random">
        /// The <see cref="Random"/> used for randomizing.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> who randomizes a <see cref="int" /> that is greater than or equal to argument
        ///     <paramref name="min" /> and less than argument <paramref name="max" /> when invoking
        ///     <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of creating a <see cref="IGenerator{T}"/> which randomizes between 0 and 100.
        ///     </para>
        ///     <code language="c#">
        ///         IGenerator&lt;int&gt; = Generator.Randomizer(0, 100);
        ///     </code>
        /// </example>
        public static IGenerator<int> Randomizer(int min, int max, Random random = null) => max > min
            ? Create(random ?? new Random()).Select(rnd => rnd.Next(min, max))
            : throw new ArgumentOutOfRangeException(nameof(max), @"max must be > min!");

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> where each invocation of <see cref="IGenerator{T}.Generate" /> will
        ///         return a randomized <see cref="int" /> that is
        ///         greater than or equal to 0 and less than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned. Argument <paramref name="max" /> must be greater than or
        ///     equal to
        ///     argument <paramref name="min" />.
        /// </param>
        /// <param name="random">
        /// The <see cref="Random"/> used for randomizing.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> who randomizes a <see cref="long" /> that is greater than or equal to argument
        ///     <paramref name="min" /> and less than argument <paramref name="max" /> when invoking
        ///     <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of creating a <see cref="IGenerator{T}"/> which randomizes <see cref="long.MinValue"/> and <see cref="long.MaxValue"/>
        ///     </para>
        ///     <code language="c#">
        ///         IGenerator&lt;int&gt; = Generator.Randomizer(long.MinValue, long.MaxValue);
        ///     </code>
        /// </example>
        public static IGenerator<long> Randomizer(long min, long max, Random random = null) {
            if (max <= min)
                throw new ArgumentOutOfRangeException(nameof(max), @"max must be > min!");
            //Working with ulong so that modulo works correctly with values > long.MaxValue
            var uRange = (ulong) (max - min);
            random = random ?? new Random();
            return Function(() => {
                //Prevent a modulo bias; see http://stackoverflow.com/a/10984975/238419
                //for more information.
                //In the worst case, the expected number of calls is 2 (though usually it's
                //much closer to 1) so this loop doesn't really hurt performance at all.
                ulong ulongRand;
                do {
                    var buf = new byte[8];
                    random.NextBytes(buf);
                    ulongRand = (ulong) BitConverter.ToInt64(buf, 0);
                } while (ulongRand > ulong.MaxValue - (ulong.MaxValue % uRange + 1) % uRange);

                return (long) (ulongRand % uRange) + min;
            });
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> where each invocation of <see cref="IGenerator{T}.Generate" /> will
        ///         return a new <see cref="System.Guid" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> where each invocation of <see cref="IGenerator{T}.Generate" /> will
        ///     return a new <see cref="System.Guid" />.
        /// </returns>
        public static IGenerator<Guid> Guid() => Function(System.Guid.NewGuid);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> who starts on argument <paramref name="start" /> and
        ///         returns a new <see cref="int" /> which is incremented by 1 for each invocation of
        ///         <see cref="IGenerator{T}.Generate" />.
        ///     </para>
        /// </summary>
        /// <exception cref="OverflowException">If the value exceeds <see cref="int" />.<see cref="int.MaxValue" />.</exception>
        /// <param name="start">The inclusive number to start at.</param>
        /// <returns>
        ///     A incrementation <see cref="IGenerator{T}" /> who starts on argument <paramref name="start" /> and returns a new
        ///     <see cref="int" /> which is incremented by 1 for each invocation of <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of creating a <see cref="IGenerator{T}"/> which inclusively increments from 20.
        ///     </para>
        ///     <code language="c#">
        ///         IGenerator&lt;int&gt; = Generator.Incrementer(20);
        ///     </code>
        /// </example>
        public static IGenerator<int> Incrementer(int start) => Function(() => checked(start++));

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> who starts on argument <paramref name="start" /> and
        ///         returns a new <see cref="int" /> which is decremented by 1 for each invocation of
        ///         <see cref="IGenerator{T}.Generate" />.
        ///     </para>
        /// </summary>
        /// <exception cref="OverflowException">If the value gets below <see cref="int" />.<see cref="int.MinValue" />.</exception>
        /// <param name="start">The inclusive number to start at.</param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> who starts on argument <paramref name="start" /> and returns a new
        ///     <see cref="int" /> which is decremented by 1 for each invocation of <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of creating a <see cref="IGenerator{T}"/> which inclusively decrements from 20.
        ///     </para>
        ///     <code language="c#">
        ///         IGenerator&lt;int&gt; = Generator.Incrementer(20);
        ///     </code>
        /// </example>
        public static IGenerator<int> Decrementer(int start) => Function(() => checked(start--));

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> whose generations is a randomized element from the <paramref name="items" />
        ///     .
        /// </summary>
        /// <typeparam name="T">The type of the arguments.</typeparam>
        /// <param name="random">The randomizer.</param>
        /// <param name="items">The arguments.</param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations is based on the argument <paramref name="items" />.
        /// </returns>
        public static IGenerator<T> ListRandomizer<T>(Random random, IReadOnlyList<T> items) {
            return random != null
                ? (items == null
                    ? throw new ArgumentNullException(nameof(items))
                    : Create(random).Select(r => items[r.Next(items.Count)]))
                : throw new ArgumentNullException(nameof(random));
        }

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> whose generations is a randomized element from the <paramref name="items" />
        ///     .
        /// </summary>
        /// <typeparam name="T">The type of the arguments.</typeparam>
        /// <param name="items">The arguments.</param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations is based on the argument <paramref name="items" />.
        /// </returns>
        public static IGenerator<T> ListRandomizer<T>(IReadOnlyList<T> items) => ListRandomizer(new Random(), items);

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> whose generations is a randomized element from the
        ///     <paramref name="additional" />
        ///     .
        /// </summary>
        /// <typeparam name="T">The type of the arguments.</typeparam>
        /// <param name="first">The first argument</param>
        /// <param name="second">The second argument</param>
        /// <param name="additional">Additional arguments.</param>
        /// <param name="random">The randomizer.</param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations is based on arguments supplied.
        /// </returns>
        public static IGenerator<T> ArgumentRandomizer<T>(Random random, T first, T second, params T[] additional) {
            return Function(() => {
                var res = random.Next(-2, additional.Length);
                switch (res) {
                    case -2: return first;
                    case -1: return second;
                    default: return additional[res];
                }
            });
        }

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> whose generations is a randomized element from the
        ///     <paramref name="additional" />
        ///     .
        /// </summary>
        /// <typeparam name="T">The type of the arguments.</typeparam>
        /// <param name="first">The first argument</param>
        /// <param name="second">The second argument</param>
        /// <param name="additional">Additional arguments.</param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose generations is based on arguments supplied.
        /// </returns>
        public static IGenerator<T> ArgumentRandomizer<T>(T first, T second, params T[] additional) =>
            ArgumentRandomizer(new Random(), first, second, additional);
    }
}