﻿using System;
using System.Collections.Generic;
using Sharpy.Enums;
using Sharpy.Randomizer;

namespace Sharpy {
    /// <summary>
    ///     <para>Static generator using my Implementation of IRandomizer&lt;TStringArg&gt;</para>
    ///     <para>Can also create instances of the implementation</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </summary>
    /// <returns></returns>
    public sealed class RandomGenerator : Generator<IRandomizer<StringType>> {
        static RandomGenerator() {
            Generator = Create();
            Configurement = Generator.Config;
        }

        private RandomGenerator(Config config) : base(new Randomizer.Randomizer(config)) {
            Config = config;
        }

        /// <summary>
        ///     <para>For configuring Randomizer.</para>
        /// </summary>
        public Config Config { get; }

        private static RandomGenerator Generator { get; }

        /// <summary>
        ///     <para>For configuring Randomizer;.</para>
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
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IRandomizer<StringType>, T> func, int count = 10)
            => Generator.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        ///     <para>Includes an integer counting iterations.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IRandomizer<StringType>, int, T> func, int count = 10)
            => Generator.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a &lt;T&gt;.</para>
        /// </summary>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <returns></returns>
        public static T GenerateInstance<T>(Func<IRandomizer<StringType>, T> func) => Generator.Generate(func);

        public override string ToString() => $"Configurement for Random Generator {Config}";
    }
}