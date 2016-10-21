using Sharpy.Enums;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    ///     Creates a simple generator using my Implementation of IRandomizer&lt;TStringArg&gt;
    /// </summary>
    /// <returns></returns>
    public class RandomGenerator : Generator<IRandomizer<StringType>> {
        /// <summary>
        /// 
        /// </summary>
        public Config Config { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        private RandomGenerator(Config config) : base(new Randomizer(config)) {
            Config = config;
        }

        /// <summary>
        ///   Returns a new instance of RandomGenerator.
        /// </summary>
        /// <returns></returns>
        public static RandomGenerator Create() => new RandomGenerator(new Config());

    }
}