using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
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
        public float Float(float min, float max)
        {
            return _random.Float(min, max);
        }

        /// <inheritdoc />
        public float Float(float max)
        {
            return _random.Float(0, max);
        }

        /// <inheritdoc />
        public float Float()
        {
            return _random.Float(0, float.MaxValue);
        }
    }
}