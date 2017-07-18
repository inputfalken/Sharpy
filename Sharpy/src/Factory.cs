using System;
using GeneratorAPI;
using GeneratorAPI.Linq;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.IProviders;

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
            return Generator.Function(provider.FirstName);
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
            INameProvider provider = null) {
            provider = provider ?? new NameByOrigin();
            return Generator.Function(() => provider.FirstName(gender));
        }

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
            INameProvider lastNameProvider = null) {
            lastNameProvider = lastNameProvider ?? new NameByOrigin();
            return Generator.Function(lastNameProvider.LastName);
        }

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
        public static IGenerator<string> Username(int seed) {
            return Generator
                .Create(new Provider(seed))
                .Select(p => p.UserName());
        }

        /// <summary>
        ///     <para>Creates <see cref="IGenerator{T}" /> whose generic argument is <see cref="string" />.</para>
        ///     <para>
        ///         Each invocation of <see cref="IGenerator{T}.Generate" /> will return a <see cref="string" /> representing a
        ///         user name.
        ///     </para>
        /// </summary>
        public static IGenerator<string> Username() {
            return Generator
                .Create(new Provider())
                .Select(p => p.UserName());
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
        public static IGenerator<int> Randomizer(int min, int max, int? seed = null) {
            return Generator.Create(CreateRandom(seed)).Select(rnd => rnd.Next(min, max));
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> where each invocation of <see cref="IGenerator{T}.Generate" /> will
        ///         return a randomized <see cref="int" /> that is
        ///         greater than or equal to 0 and less than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="max">
        ///     The exclusive upper bound of the random number to be generated. Argument <paramref name="max" /> must be greater
        ///     than or equal to 0.
        /// </param>
        /// <param name="seed">
        ///     A number used to calculate a starting value for the pseudo-random number sequence. If a negative
        ///     number is specified, the absolute value of the number is used.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> who randomizes a <see cref="int" /> that is greater than or equal to 0
        ///     and less than argument <paramref name="max" /> when invoking
        ///     <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="RandomizerTwoArg" source="Examples\GeneratorFactory.cs" />
        /// </example>
        public static IGenerator<int> Randomizer(int max, int? seed = null) {
            return Generator.Create(CreateRandom(seed))
                .Select(rnd => rnd.Next(max));
        }

        /// <summary>
        ///     <para>Creates <see cref="Random" /> with seed if it's not set to null.</para>
        /// </summary>
        private static Random CreateRandom(int? seed) {
            return seed == null
                ? new Random()
                : new Random(seed.Value);
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> where each invocation of <see cref="IGenerator{T}.Generate" /> will
        ///         return a randomized <see cref="int" /> that is
        ///         greater than or equal to 0 and less than <see cref="int" />.<see cref="int.MaxValue" />.
        ///     </para>
        /// </summary>
        /// <param name="seed">
        ///     A number used to calculate a starting value for the pseudo-random number sequence. If a negative
        ///     number is specified, the absolute value of the number is used.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> who randomizes a <see cref="int" /> that is greater than or equal to 0
        ///     and less than <see cref="int.MaxValue" /> when invoking
        ///     <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="RandomizerOneArg" source="Examples\GeneratorFactory.cs" />
        /// </example>
        public static IGenerator<int> Randomizer(int? seed = null) {
            return Generator.Create(CreateRandom(seed))
                .Select(rnd => rnd.Next());
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
        /// <example>
        ///     <code language="C#" region="Guid" source="Examples\GeneratorFactory.cs" />
        /// </example>
        public static IGenerator<Guid> Guid() {
            return Generator.Function(System.Guid.NewGuid);
        }

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
        public static IGenerator<int> Incrementer(int start) {
            return Generator.Function(() => checked(start++));
        }

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
        public static IGenerator<int> Decrementer(int start) {
            return Generator.Function(() => checked(start--));
        }
    }
}