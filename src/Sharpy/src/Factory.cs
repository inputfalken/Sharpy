using System;
using System.Collections.Generic;
using Sharpy.Core;
using Sharpy.Core.Linq;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using static Sharpy.Core.Generator;

namespace Sharpy {
    /// <summary>
    ///     Provides a set of static methods for creating <see cref="IGenerator{T}" />.
    /// </summary>
    public static class Factory {
        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         first name.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             If an implementation of <see cref="INameProvider" /> is not supplied the implementation will get
        ///             defaulted to <see cref="NameByOrigin" />
        ///         </para>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Function(provider.FirstName);
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> which generates strings with numbers whose length is equal to argument:
        ///         <paramref name="length" />.
        ///         This is a static <see cref="IGenerator{T}" /> version of method: <see cref="Provider.NumberByLength" />.
        ///     </para>
        /// </summary>
        /// <param name="length">The length of the generated <see cref="string" />.</param>
        /// <param name="random">The random to generate the numbers with.</param>
        /// <param name="unique">If the numbers need to be unique.</param>
        /// <returns>
        ///     <see cref="IGenerator{T}" /> which generates strings with numbers.
        /// </returns>
        /// <exception cref="Exception">
        ///     Reached maximum amount of combinations for the argument <paramref name="length" />.
        ///     This should only occur if argument: <paramref name="unique" /> is set to true.
        /// </exception>
        public static IGenerator<string> NumberByLength(int length, Random random, bool unique) {
            var exceptionMessage = $"Reached maximum amount of combinations for the argument '{nameof(length)}'.";
            return
                Create((NumberGenerator: new NumberGenerator(random), Pow: (int) Math.Pow(10, length) - 1))
                    .Select(arg => arg.NumberGenerator.RandomNumber(0, arg.Pow, unique))
                    .Select(number => number == -1
                        ? throw new Exception(exceptionMessage)
                        : number.ToString())
                    .Select(number => number.Length != length ? number.Prefix(length - number.Length) : number);
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         first name based on argument <see cref="Gender" />.
        ///     </para>
        ///     <remarks>
        ///         <para>
        ///             If an implementation of <see cref="INameProvider" /> is not supplied the implementation will get
        ///             defaulted to <see cref="NameByOrigin" />
        ///         </para>
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> FirstName(Gender gender,
            INameProvider provider = null) => Create(provider ?? new NameByOrigin()).Select(p => p.FirstName(gender));

        /// <summary>
        ///     Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.
        ///     Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a last
        ///     name.
        ///     <para> </para>
        ///     <remarks>
        ///         If an implementation of <see cref="INameProvider" /> is not supplied the implementation will get defaulted to
        ///         <see cref="NameByOrigin" />
        ///     </remarks>
        /// </summary>
        public static IGenerator<string> LastName(
            INameProvider lastNameProvider = null) =>
            Create(lastNameProvider ?? new NameByOrigin()).Select(p => p.LastName());

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         user name.
        ///     </para>
        ///     <param name="seed">
        ///         A number used to calculate a starting value for the pseudo-random number sequence. If a negative
        ///         number is specified, the absolute value of the number is used
        ///     </param>
        /// </summary>
        public static IGenerator<string> Username(int seed) => Create(new Provider(seed))
            .Select(p => p.UserName());

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         user name.
        ///     </para>
        /// </summary>
        public static IGenerator<string> Username() => Create(new Provider())
            .Select(p => p.UserName());

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
        /// <param name="seed">
        ///     A number used to calculate a starting value for the pseudo-random number sequence. If a negative
        ///     number is specified, the absolute value of the number is used.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> who randomizes a <see cref="int" /> that is greater than or equal to argument
        ///     <paramref name="min" /> and less than argument <paramref name="max" /> when invoking
        ///     <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="RandomizerThreeArg" source="Examples\GeneratorFactory.cs" />
        /// </example>
        public static IGenerator<int> Randomizer(int min, int max, int? seed = null) => max > min
            ? Create(CreateRandom(seed)).Select(rnd => rnd.Next(min, max))
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
        /// <param name="seed">
        ///     A number used to calculate a starting value for the pseudo-random number sequence. If a negative
        ///     number is specified, the absolute value of the number is used.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> who randomizes a <see cref="long" /> that is greater than or equal to argument
        ///     <paramref name="min" /> and less than argument <paramref name="max" /> when invoking
        ///     <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        public static IGenerator<long> Randomizer(long min, long max, int? seed = null) {
            if (max <= min)
                throw new ArgumentOutOfRangeException(nameof(max), @"max must be > min!");
            //Working with ulong so that modulo works correctly with values > long.MaxValue
            var uRange = (ulong) (max - min);
            var random = CreateRandom(seed);
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
        ///     <para>Creates <see cref="Random" /> with seed if it's not set to null.</para>
        /// </summary>
        private static Random CreateRandom(int? seed) => seed == null
            ? new Random()
            : new Random(seed.Value);

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
        /// <example>
        ///     <code language="C#" region="Guid" source="Examples\GeneratorFactory.cs" />
        /// </example>
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
        ///     <code language="C#" region="Incrementer" source="Examples\GeneratorFactory.cs" />
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
        ///     <code language="C#" region="Decrementer" source="Examples\GeneratorFactory.cs" />
        /// </example>
        public static IGenerator<int> Decrementer(int start) => Function(() => checked(start--));

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static IGenerator<T> ListRandomizer<T>(IReadOnlyList<T> items, Random rnd = null) => items == null
            ? throw new ArgumentNullException(nameof(items))
            : Create(rnd ?? new Random()).Select(items.RandomItem);

        public static IGenerator<TResult> Provider<TResult>(Func<Provider, TResult> selector,
            Configurement configurement) => configurement != null
            ? selector != null
                ? Create(new Provider(configurement)).Select(selector)
                : throw new ArgumentNullException(nameof(selector))
            : throw new ArgumentNullException(nameof(configurement));

        public static IGenerator<TResult> Provider<TResult>(Func<Provider, TResult> selector) =>
            Provider(selector, new Configurement());

        public static IGenerator<T> ArgumentRandomizer<T>(params T[] items) => ArgumentRandomizer(null, items: items);

        public static IGenerator<T> ArgumentRandomizer<T>(Random rnd = null, params T[] items) => items != null
            ? ListRandomizer(items, rnd)
            : throw new ArgumentNullException(nameof(items));
    }
}