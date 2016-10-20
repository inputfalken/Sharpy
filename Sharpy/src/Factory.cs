using Sharpy.Enums;
using Sharpy.Types;

namespace Sharpy {
    public static class Factory {
        public static RandomGenerator RandomGenerator() => new RandomGenerator(new Config());

        /// <summary>
        ///     Requires a implementation of IRandomizer&lt;TStringArg&gt;
        /// </summary>
        /// <typeparam name="TStringArg"></typeparam>
        /// <param name="iRandomizer"></param>
        /// <returns></returns>
        public static Generator<IRandomizer<TStringArg>> CustomRandomGenerator<TStringArg>(
                IRandomizer<TStringArg> iRandomizer)
            => new Generator<IRandomizer<TStringArg>>(iRandomizer);
    }

    /// <summary>
    ///     Creates a simple generator using my Implementation of IRandomizer&lt;TStringArg&gt;
    /// </summary>
    /// <returns></returns>
    public class RandomGenerator : Generator<Randomizer> {
        public Config Config { get; }

        public RandomGenerator(Config config) : base(new Randomizer(config)) {
            Config = config;
        }
    }
}