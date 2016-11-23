using System;
using Sharpy.Implementation.ExtensionMethods;

namespace Sharpy {
    internal class LongRandomizer : ILongProvider {
        private readonly Random _random;

        public LongRandomizer(Random random) {
            _random = random;
        }

        public long Long(long min, long max) => _random.NextLong(min, max);

        public long Long(long max) => _random.NextLong(max);

        public long Long() => _random.NextLong();
    }
}