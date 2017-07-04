using System;
using GeneratorAPI.Extensions;

namespace GeneratorAPI {
    /// <summary>
    ///     <para>Contains methods for creating <see cref="IGenerator{T}" />.</para>
    ///     <remarks>
    ///         <para>The point of this class is to contain extension methods from other libraries.</para>
    ///     </remarks>
    /// </summary>
    public sealed class GeneratorFactory {
        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> where each invokation of <see cref="IGenerator{T}.Generate" /> will
        ///         return a randomized <see cref="int" /> that is
        ///         greater than or equal to argument <paramref name="min" /> and less than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal to
        ///     minValue.
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
        ///     <code language="C#" region="RandomizerThreeArg" source="..\Examples\GeneratorFactory.cs" />
        /// </example>
        public IGenerator<int> Randomizer(int min, int max, int? seed = null) {
            return Generator
                .Create(CreateRandom(seed))
                .Select(rnd => rnd.Next(min, max));
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> where each invokation of <see cref="IGenerator{T}.Generate" /> will
        ///         return a randomized <see cref="int" /> that is
        ///         greater than or equal to 0 and less than argument <paramref name="max" />.
        ///     </para>
        /// </summary>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal to
        ///     minValue.
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
        ///     <code language="C#" region="RandomizerTwoArg" source="..\Examples\GeneratorFactory.cs" />
        /// </example>
        public IGenerator<int> Randomizer(int max, int? seed = null) {
            return Generator
                .Create(CreateRandom(seed))
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
        ///         Returns a <see cref="IGenerator{T}" /> where each invokation of <see cref="IGenerator{T}.Generate" /> will
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
        ///     <code language="C#" region="RandomizerOneArg" source="..\Examples\GeneratorFactory.cs" />
        /// </example>
        public IGenerator<int> Randomizer(int? seed = null) {
            return Generator
                .Create(CreateRandom(seed))
                .Select(rnd => rnd.Next());
        }


        /// <summary>
        ///     <para>
        ///         Returns a <see cref="IGenerator{T}" /> where each invokation of <see cref="IGenerator{T}.Generate" /> will
        ///         return a new <see cref="System.Guid" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> where each invokation of <see cref="IGenerator{T}.Generate" /> will
        ///     return a new <see cref="System.Guid" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Guid" source="..\Examples\GeneratorFactory.cs" />
        /// </example>
        public IGenerator<Guid> Guid() {
            return Generator.Function(System.Guid.NewGuid);
        }

        /// <summary>
        ///     <para>
        ///         Returns a incrementation <see cref="IGenerator{T}" /> who starts on argument <paramref name="start" /> and
        ///         returns a new <see cref="int" /> which is incremented by 1 for each invokation of
        ///         <see cref="IGenerator{T}.Generate" />.
        ///     </para>
        /// </summary>
        /// <exception cref="OverflowException">If the value exceeds <see cref="int" />.<see cref="int.MaxValue" />.</exception>
        /// <param name="start">The inclusive number to start at.</param>
        /// <returns>
        ///     A incrementation <see cref="IGenerator{T}" /> who starts on argument <paramref name="start" /> and returns a new
        ///     <see cref="int" /> which is incremented by 1 for each invokation of <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Incrementer" source="..\Examples\GeneratorFactory.cs" />
        /// </example>
        public IGenerator<int> Incrementer(int start = 0) {
            return Generator.Function(() => checked(start++));
        }

        /// <summary>
        ///     <para>
        ///         Returns a decrementation <see cref="IGenerator{T}" /> who starts on argument <paramref name="start" /> and
        ///         returns a new <see cref="int" /> which is decremented by 1 for each invokation of
        ///         <see cref="IGenerator{T}.Generate" />.
        ///     </para>
        /// </summary>
        /// <exception cref="OverflowException">If the value gets below <see cref="int" />.<see cref="int.MinValue" />.</exception>
        /// <param name="start">The inclusive number to start at.</param>
        /// <returns>
        ///     A decrementation <see cref="IGenerator{T}" /> who starts on argument <paramref name="start" /> and returns a new
        ///     <see cref="int" /> which is decremented by 1 for each invokation of <see cref="IGenerator{T}.Generate" />.
        /// </returns>
        /// <example>
        ///     <code language="C#" region="Decrementer" source="..\Examples\GeneratorFactory.cs" />
        /// </example>
        public IGenerator<int> Decrementer(int start = 0) {
            return Generator.Function(() => checked(start--));
        }
    }
}