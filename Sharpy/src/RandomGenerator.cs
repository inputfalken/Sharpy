using System;
using System.Collections.Generic;
using Sharpy.Enums;
using Sharpy.Randomizer;

namespace Sharpy {
    /// <summary>
    ///     Uses a static generator using my Implementation of IRandomizer&lt;TStringArg&gt;
    ///<para></para>
    ///     Can also give instances of the same generator. Is useful if you want to generate same data by setting the same  seed on seperate generators.
    /// </summary>
    /// <returns></returns>
    public sealed class RandomGenerator : Generator<IRandomizer<StringType>> {
        private RandomGenerator(Config config) : base(new Randomizer.Randomizer(config)) {
            Config = config;
        }


        /// <summary>
        ///     Is used for configuring the generator to act different when calling Generation methods.
        /// </summary>
        public Config Config { get; }


        static RandomGenerator() {
            Generator = Create();
            Configurement = Generator.Config;
        }

        private static RandomGenerator Generator { get; }

        /// <summary>
        ///     Creates a new instance of Randomgenerator.
        /// </summary>
        /// <returns></returns>
        public static RandomGenerator Create() => new RandomGenerator(new Config());

        /// <summary>
        ///     Generates a IEnumerable&lt;T&gt;.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IRandomizer<StringType>, T> func, int count = 20)
            => Generator.GenerateMany(func, count);

        /// <summary>
        ///     Generates a &lt;T&gt;.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GenerateInstance<T>(Func<IRandomizer<StringType>, T> func) => Generator.Generate(func);

        /// <summary>
        ///  Configures the Generator.
        /// </summary>
        public static Config Configurement { get; }
    }
}