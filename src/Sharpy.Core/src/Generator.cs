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
        ///         Creates a <see cref="IGenerator{T}" /> which uses the same <paramref name="source" /> parameter.
        ///     </para>
        /// </summary>
        /// <param name="source">
        ///     The type to generate from.
        /// </param>
        /// <typeparam name="TSource">
        ///     The type of argument <paramref name="source" />
        /// </typeparam>
        /// <returns>
        ///     A <typeparamref name="TSource"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<TSource> Create<TSource>(TSource source) => new Fun<TSource>(() => source);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> which uses the same value from the <paramref name="lazy" /> parameter.
        ///     </para>
        /// </summary>
        /// <param name="lazy">
        ///     The lazy evaluated value.
        /// </param>
        /// <typeparam name="TSource">
        ///     The type of <paramref name="lazy" />.
        /// </typeparam>
        /// <returns>
        ///     A <typeparamref name="TSource"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="lazy" /> is null.</exception>
        public static IGenerator<TSource> Lazy<TSource>(Lazy<TSource> lazy) => lazy == null
            ? throw new ArgumentNullException(nameof(lazy))
            : new Fun<TSource>(() => lazy.Value);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> which will turn the <paramref name="fn" /> parameter into a
        ///         <see cref="System.Lazy{TSource}" /> who will then use the same value.
        ///     </para>
        /// </summary>
        /// <param name="fn">
        ///     The function which will be turned into <see cref="System.Lazy{TSource}" />.
        /// </param>
        /// <typeparam name="TSource">
        ///     The type of <paramref name="fn" />.
        /// </typeparam>
        /// <returns>
        ///     A <typeparamref name="TSource"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="fn" /> is null.</exception>
        public static IGenerator<TSource> Lazy<TSource>(Func<TSource> fn) => Lazy(new Lazy<TSource>(fn));

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> whose generations will
        ///         invoke <paramref name="fn" /> parameter.
        ///     </para>
        /// </summary>
        /// <param name="fn">
        ///     The function to invoke for each generation.
        /// </param>
        /// <typeparam name="TSource">
        ///     The type returned from <paramref name="fn" /> parameter.
        /// </typeparam>
        /// <returns>
        ///     A <typeparamref name="TSource"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="fn" /> is null.</exception>
        public static IGenerator<TSource> Function<TSource>(Func<TSource> fn) => new Fun<TSource>(fn);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> based on an <see cref="IEnumerable{T}" /> which restarts if the end is
        ///         reached.
        ///     </para>
        /// </summary>
        /// <param name="enumerable">
        ///     The <see cref="IEnumerable{T}" /> to create a <see cref="IGenerator{T}" /> from.
        /// </param>
        /// <typeparam name="TSource">
        ///     The type of the <paramref name="enumerable" />.
        /// </typeparam>
        /// <returns>
        ///     A <typeparamref name="TSource"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">Argument <paramref name="enumerable" /> is null.</exception>
        public static IGenerator<TSource> CircularSequence<TSource>(IEnumerable<TSource> enumerable) =>
            enumerable == null
                ? throw new ArgumentNullException(nameof(enumerable))
                : new Seq<TSource>(enumerable);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> which randomizes <see cref="int" />.
        ///     </para>
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned. Argument <paramref name="max" /> must be greater than or
        ///     equal to
        ///     argument <paramref name="min" />.
        /// </param>
        /// <param name="random">
        ///     The <see cref="Random" /> used for randomizing.
        /// </param>
        /// <returns>
        ///     A <see cref="int" /> <see cref="IGenerator{T}" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of creating a <see cref="IGenerator{T}" /> which randomizes between 0 and 100.
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
        ///         Creates a <see cref="IGenerator{T}" /> which randomizes <see cref="long" />.
        ///     </para>
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned. Argument <paramref name="max" /> must be greater than or
        ///     equal to
        ///     argument <paramref name="min" />.
        /// </param>
        /// <param name="random">
        ///     The <see cref="Random" /> used for randomizing.
        /// </param>
        /// <returns>
        ///     A <see cref="long" /> <see cref="IGenerator{T}" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of creating a <see cref="IGenerator{T}" /> which randomizes <see cref="long.MinValue" /> and
        ///         <see cref="long.MaxValue" />
        ///     </para>
        ///     <code language="c#">
        ///         IGenerator&lt;int&gt; = Generator.Randomizer(long.MinValue, long.MaxValue);
        ///     </code>
        /// </example>
        public static IGenerator<long> Randomizer(long min, long max, Random random = null) => max <= min
            ? throw new ArgumentOutOfRangeException(nameof(max), @"max must be > min!")
            : Create((Urange:(ulong) (max - min), Rnd:random ?? new Random())).Select(tuple => {
                //Prevent a modulo bias; see http://stackoverflow.com/a/10984975/238419
                //for more information.
                //In the worst case, the expected number of calls is 2 (though usually it's
                //much closer to 1) so this loop doesn't really hurt performance at all.
                ulong ulongRand;
                do {
                    var buf = new byte[8];
                    tuple.Rnd.NextBytes(buf);
                    ulongRand = (ulong) BitConverter.ToInt64(buf, 0);
                } while (ulongRand > ulong.MaxValue - (ulong.MaxValue % tuple.Urange + 1) % tuple.Urange);

                return (long) (ulongRand % tuple.Urange) + min;
            });

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> who creates <see cref="System.Guid" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A <see cref="System.Guid" /> <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<Guid> Guid() => Function(System.Guid.NewGuid);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> who increments <see cref="int" />.
        ///     </para>
        /// </summary>
        /// <exception cref="OverflowException">
        ///     If the incremented value exceeds <see cref="int.MaxValue" />.
        /// </exception>
        /// <param name="start">
        ///     The inclusive number to start at.
        /// </param>
        /// <returns>
        ///     A <see cref="int" /> <see cref="IGenerator{T}" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of creating a <see cref="IGenerator{T}" /> which inclusively increments from 20.
        ///     </para>
        ///     <code language="c#">
        ///         IGenerator&lt;int&gt; = Generator.Incrementer(20);
        ///     </code>
        /// </example>
        public static IGenerator<int> Incrementer(int start) => Function(() => checked(start++));

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> who decrements <see cref="int" />.
        ///     </para>
        /// </summary>
        /// <exception cref="OverflowException">
        ///     If the value gets below <see cref="int.MinValue" />.
        /// </exception>
        /// <param name="start">
        ///     The inclusive number to start at.
        /// </param>
        /// <returns>
        ///     A <see cref="int" /> <see cref="IGenerator{T}" />.
        /// </returns>
        /// <example>
        ///     <para>
        ///         Here's an example of creating a <see cref="IGenerator{T}" /> which inclusively decrements from 20.
        ///     </para>
        ///     <code language="c#">
        ///         IGenerator&lt;int&gt; = Generator.Incrementer(20);
        ///     </code>
        /// </example>
        public static IGenerator<int> Decrementer(int start) => Function(() => checked(start--));

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> who randomizes elements from the <paramref name="items" /> parameter.
        /// </summary>
        /// <typeparam name="T">The type of the arguments.</typeparam>
        /// <param name="random">The randomizer.</param>
        /// <param name="items">The arguments.</param>
        /// <returns>
        ///     A <typeparamref name="T"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<T> ListRandomizer<T>(Random random, IReadOnlyList<T> items) => random != null
            ? (items == null
                ? throw new ArgumentNullException(nameof(items))
                : Create(random).Select(r => items[r.Next(items.Count)]))
            : throw new ArgumentNullException(nameof(random));

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> who randomizes elements from the <paramref name="items" /> parameter.
        /// </summary>
        /// <typeparam name="T">The type of the arguments.</typeparam>
        /// <param name="items">The arguments.</param>
        /// <returns>
        ///     A <typeparamref name="T"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<T> ListRandomizer<T>(IReadOnlyList<T> items) => ListRandomizer(new Random(), items);

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> who randomizes from the parameters.
        /// </summary>
        /// <typeparam name="T">The type of the arguments.</typeparam>
        /// <param name="first">The first argument</param>
        /// <param name="second">The second argument</param>
        /// <param name="additional">Additional arguments.</param>
        /// <param name="random">The randomizer.</param>
        /// <returns>
        ///     A <typeparamref name="T"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<T> ArgumentRandomizer<T>(Random random, T first, T second, params T[] additional) =>
            Function(() => {
                var res = random.Next(-2, additional.Length);
                switch (res) {
                    case -2: return first;
                    case -1: return second;
                    default: return additional[res];
                }
            });

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}" /> who randomizes from the parameters.
        /// </summary>
        /// <typeparam name="T">The type of the arguments.</typeparam>
        /// <param name="first">The first argument</param>
        /// <param name="second">The second argument</param>
        /// <param name="additional">Additional arguments.</param>
        /// <returns>
        ///     A <typeparamref name="T"> </typeparamref> <see cref="IGenerator{T}" />.
        /// </returns>
        public static IGenerator<T> ArgumentRandomizer<T>(T first, T second, params T[] additional) =>
            ArgumentRandomizer(new Random(), first, second, additional);
    }
}