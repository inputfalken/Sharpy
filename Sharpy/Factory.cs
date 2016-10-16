using System;
using Sharpy.Types;

namespace Sharpy {
    public static class Factory {
        public static Generator<T, Randomizer> CreateNew<T>(Func<Randomizer, T> func, Config config = null) {
            return new Generator<T, Randomizer>(func, new Randomizer(config));
        }
    }
}