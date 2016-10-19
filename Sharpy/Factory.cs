using System;
using Sharpy.Types;

namespace Sharpy {
    public static class Factory {
        /// <summary>
        ///    Creates a simple generator using my Implementation of IRandomizer&lt;TStringArg&gt;
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static Generator<Randomizer> RandomGenerator(Config config = null)
            => new Generator<Randomizer>(new Randomizer(config));
    }
}