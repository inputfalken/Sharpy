using System;
using System.Collections.Generic;
using Sharpy.Enums;
using Sharpy.ExtensionMethods;
using Sharpy.Randomizer;

namespace Sharpy {
    /// <summary>
    ///     <para>Static generator using my Implementation of IRandomizer&lt;TStringArg&gt;</para>
    ///     <para>Can also give instances of the same generator. Is useful if you want to generate same data by setting the same
    ///     seed on seperate generators.</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </summary>
    /// <returns></returns>
    public sealed class RandomGenerator : Generator<IRandomizer<StringType>> {
        private Randomizer.Randomizer Randomizer { get; }

        static RandomGenerator() {
            Generator = Create();
            Configurement = Generator.Config;
        }

        internal RandomGenerator(Randomizer.Randomizer randomizer) : base(randomizer) {
            Randomizer = randomizer;
            Config = randomizer.Config;
        }


        /// <summary>
        ///     <para>Is used for configuring the generator to act different when calling Generation methods.</para>
        /// </summary>
        public Config Config { get; }

        public override T Generate<T>(Func<IRandomizer<StringType>, T> func) {
            Randomizer.MaxAmmount = 1;
            return base.Generate(func);
        }

        public override IEnumerable<T> GenerateMany<T>(Func<IRandomizer<StringType>, T> func, int count = 10) {
            Randomizer.MaxAmmount = count;
            return base.GenerateMany(func, count);
        }

        public override IEnumerable<T> GenerateMany<T>(Func<IRandomizer<StringType>, int, T> func, int count = 10) {
            Randomizer.MaxAmmount = count;
            return base.GenerateMany(func, count);
        }

        private static RandomGenerator Generator { get; }

        /// <summary>
        ///     <para>Is used for configuring the generator to act different when calling Generation methods.</para>
        /// </summary>
        public static Config Configurement { get; }

        /// <summary>
        ///     <para>Creates a new instance of Randomgenerator.</para>
        /// </summary>
        /// <returns></returns>
        public static RandomGenerator Create() => new RandomGenerator(new Randomizer.Randomizer(new Config()));

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IRandomizer<StringType>, T> func, int count = 10)
            => Generator.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        ///     <para>Includes an integer counting iterations.</para>
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IRandomizer<StringType>, int, T> func, int count = 10)
            => Generator.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a &lt;T&gt;.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GenerateInstance<T>(Func<IRandomizer<StringType>, T> func) {
            Generator.Randomizer.MaxAmmount = 1;
            return Generator.Generate(func);
        }

        public override string ToString() {
            return $"Configurement for Random Generator{Config}";
        }
    }
}