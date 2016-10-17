using System;
using Sharpy.Types;

namespace Sharpy {
    public static class Factory {
        public static Generator<T, Randomizer> RandomGenerator<T>(Func<Randomizer, T> func, Config config = null)
            => new Generator<T, Randomizer>(func, new Randomizer(config ?? new Config()));

        public static Generator<T, Randomizer> RandomGenerator<T>(Func<Randomizer, int, T> func, Config config = null)
            => new Generator<T, Randomizer>(func, new Randomizer(config ?? new Config()));
    }
}