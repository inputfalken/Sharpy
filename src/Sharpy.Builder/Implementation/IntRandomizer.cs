using System;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     <para>
    ///         Randomizes <see cref="int" /> elements by using <see cref="Random" />.
    ///     </para>
    /// </summary>
    public sealed class IntRandomizer : IIntProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Creates a <see cref="IntRandomizer" />.
        /// </summary>
        public IntRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public int Int(in int max)
        {
            return _random.Next(max);
        }

        /// <inheritdoc />
        public int Int(in int min, in int max)
        {
            if (max < min)
                throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.Next(min, max);
        }

        /// <inheritdoc />
        public int Int()
        {
            return _random.Next();
        }
    }
}