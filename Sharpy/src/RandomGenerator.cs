using System;
using System.Collections.Generic;
using Sharpy.Enums;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    ///     Creates a simple generator using my Implementation of IRandomizer&lt;TStringArg&gt;
    /// </summary>
    /// <returns></returns>
    public class RandomGenerator : Generator<IRandomizer<StringType>> {
        private RandomGenerator(Config config) : base(new Randomizer(config)) {
            Config = config;
        }

        private static RandomGenerator Generator { get; } = Create();

        /// <summary>
        ///     Is used for configuring the generator to act different when calling Generation methods.
        /// </summary>
        public Config Config { get; }

        /// <summary>
        ///     Gives a new instance of Randomgenerator where you can configure the generator.
        /// </summary>
        /// <returns></returns>
        public static RandomGenerator Create() => new RandomGenerator(new Config());

        /// <summary>
        ///     Can be used if you just want a IEnumerable&lt;T&gt;.
        ///     Calls GenerateMany from a private Generator.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IRandomizer<StringType>, T> func, int count = 20)
            => Generator.GenerateMany(func, count);

        /// <summary>
        ///     Can be used if you just want an instance of &lt;T&gt;.
        ///     Calls Generate from a private Generator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GenerateInstance<T>(Func<IRandomizer<StringType>, T> func) => Generator.Generate(func);
    }
}