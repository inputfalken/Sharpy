using System;
using Sharpy.Implementation.ExtensionMethods;

namespace Sharpy {
    internal class DoubleRandomizer : IDouble {
        private readonly Random _random;

        public DoubleRandomizer(Random random) {
            _random = random;
        }

        public double Double() => _random.NextDouble();

        public double Double(double max) => _random.NextDouble(max);

        public double Double(double min, double max) => _random.NextDouble(min, max);
    }
}