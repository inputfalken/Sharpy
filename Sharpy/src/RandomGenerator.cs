﻿using System;
using System.Collections.Generic;
using Sharpy.Enums;
using Sharpy.Randomizer;

namespace Sharpy {
    /// <summary>
    ///     <para>Static generator using my Implementation of IRandomizer&lt;TStringArg&gt;</para>
    ///     <para>
    ///         Can also give instances of the same generator. Is useful if you want to generate same data by setting the same
    ///         seed on seperate generators.
    ///     </para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </summary>
    /// <returns></returns>
    public sealed class RandomGenerator : Generator<IRandomize<StringType>> {
        static RandomGenerator() {
            Generator = Create();
            Configurement = Generator.Config;
        }

        private RandomGenerator(Config config) : base(new Randomizer.Randomize(config)) {
            Config = config;
        }

        /// <summary>
        ///     <para>Is used for configuring the generator to act different when calling Generation methods.</para>
        /// </summary>
        public Config Config { get; }

        private static RandomGenerator Generator { get; }

        /// <summary>
        ///     <para>Is used for configuring the generator to act different when calling Generation methods.</para>
        /// </summary>
        public static Config Configurement { get; }

        /// <summary>
        ///     <para>Creates a new instance of Randomgenerator.</para>
        /// </summary>
        /// <returns></returns>
        public static RandomGenerator Create() => new RandomGenerator(new Config());

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IRandomize<StringType>, T> func, int count = 10)
            => Generator.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        ///     <para>Includes an integer counting iterations.</para>
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IRandomize<StringType>, int, T> func, int count = 10)
            => Generator.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a &lt;T&gt;.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GenerateInstance<T>(Func<IRandomize<StringType>, T> func) {
            return Generator.Generate(func);
        }

        public override string ToString() {
            return $"Configurement for Random Generator{Config}";
        }
    }
}