using System;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Randomizes from the arguments.
    /// </summary>
    public class ArgumentRandomizer : IArgumentProvider {
        private readonly Random _random;

        /// <summary>
        ///     The <see cref="Random" /> for randomizing arguments.
        /// </summary>
        public ArgumentRandomizer(Random random) => _random = random ?? throw new ArgumentNullException(nameof(random));

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
        /// <param name="additional">
        ///     Additional arguments after argument <paramref name="first" /> and argument <paramref name="second" /> has been
        ///     supplied.
        /// </param>
        /// <returns>
        ///     One of the arguments supplied.
        /// </returns>
        public T Argument<T>(T first, T second, params T[] additional) {
            var res = _random.Next(-2, additional.Length);
            switch (res) {
                case -2: return first;
                case -1: return second;
                default: return additional[res];
            }
        }
    }
}