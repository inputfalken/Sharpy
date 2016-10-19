using System;
using Sharpy.Types;

namespace Sharpy {
    public static class Factory {
        public static Generator<Randomizer> RandomGenerator(Config config = null)
            => new Generator<Randomizer>(new Randomizer(config));

        /// <summary>
        ///  Requires a implementation of IRandomizer&lt;TStringArg&gt;
        /// </summary>
        /// <typeparam name="TStringArg"></typeparam>
        /// <param name="iRandomizer"></param>
        /// <returns></returns>
        public static Generator<IRandomizer<TStringArg>> CustomRandomGenerator<TStringArg>(IRandomizer<TStringArg> iRandomizer)
            => new Generator<IRandomizer<TStringArg>>(iRandomizer);
    }
}