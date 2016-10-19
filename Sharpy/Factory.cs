using System;
using Sharpy.Types;

namespace Sharpy {
    public static class Factory {
        public static Generator<Randomizer> RandomGenerator(Config config = null)
            => new Generator<Randomizer>(new Randomizer(config));

        public static Generator<IRandomizer<TStringArg>> CustomRandomGenerator<TStringArg>(IRandomizer<TStringArg> iRandomizer)
            => new Generator<IRandomizer<TStringArg>>(iRandomizer);
    }
}