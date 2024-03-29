using System;
using RandomExtended;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder.Implementation
{
    /// <inheritdoc />
    public class FloatRandomizer : IFloatProvider
    {
        private readonly Random _random;

        public FloatRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public float Float(in float min, in float max)
        {
            return _random.Float(in min, in max);
        }

        /// <inheritdoc />
        public float Float(in float max)
        {
            return _random.Float(0, in max);
        }

        /// <inheritdoc />
        public float Float()
        {
            return _random.Float(0, float.MaxValue);
        }
    }
}