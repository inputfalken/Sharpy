using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    internal class IntRandomizer : IIntegerProvider {
        private readonly Random _random;

        public IntRandomizer(Random random) {
            _random = random;
        }

        public int Integer(int max) => Integer(0, max);

        public int Integer(int min, int max) {
            if (max <= min)
                throw new ArgumentOutOfRangeException($"{nameof(max)} must be > {nameof(min)}");
            return _random.Next(min, max);
        }

        public int Integer() => Integer(int.MinValue, int.MaxValue);
    }
}