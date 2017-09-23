using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>
    ///         Randomizes <see cref="double" /> by <see cref="Random" />.
    ///     </para>
    /// </summary>
    public class DoubleRandomizer : IDoubleProvider {
        private readonly Random _random;

        /// <summary>
        ///     <para>
        ///         Randomizes doubles with the <see cref="Random" /> supplied.
        ///     </para>
        /// </summary>
        /// <param name="random">
        ///     The randomizer.
        /// </param>
        public DoubleRandomizer(Random random) => _random = random;

        /// <inheritdoc />
        public double Double() => _random.NextDouble();

        /// <inheritdoc />
        public double Double(double max) => Double(0, max);

        /// <inheritdoc />
        public double Double(double min, double max) {
            if (max <= min) throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.NextDouble() * (max - min) + min;
        }
    }
}