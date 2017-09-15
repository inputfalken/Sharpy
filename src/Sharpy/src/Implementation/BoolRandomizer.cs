using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    public class BoolRandomizer : IBoolProvider {
        private readonly Random _random;

        public BoolRandomizer(Random random) => _random = random;

        public bool Bool() => _random.Next(2) != 0;
    }
}