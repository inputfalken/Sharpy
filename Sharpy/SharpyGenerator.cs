using System;
using Sharpy.Enums;
using Sharpy.Types;

namespace Sharpy {
    public class SharpyGenerator<T> : Generator<T, StringType> {
        public SharpyGenerator(Func<IRandomizer<StringType>, int, T> func, Config config = null)
            : base(func, new Randomizer(config ?? new Config())) {}

        public SharpyGenerator(Func<IRandomizer<StringType>, T> func, Config config = null)
            : base(func, new Randomizer(config ?? new Config())) {}

        public SharpyGenerator(Func<IRandomizer<StringType>, T> func, IRandomizer<StringType> randomizer)
            : base(func, randomizer) {}

        public SharpyGenerator(Func<IRandomizer<StringType>, int, T> func, IRandomizer<StringType> randomizer)
            : base(func, randomizer) {}
    }
}