using System;
using Sharpy.Types;

namespace Sharpy {
    public static class Factory {
        public static Generator<Randomizer> RandomGenerator(Config config = null)
            => new Generator<Randomizer>(new Randomizer(config));
    }
}