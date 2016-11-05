using System;
using System.Collections.Generic;
using Sharpy.Enums;
using Sharpy.Implementation;

namespace Sharpy {
    /// <summary>
    ///     <para>Static generator derived from Generator</para>
    ///     <para>Can also create instances of the generator</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </summary>
    /// <returns></returns>
    public sealed class SharpyGenerator : Generator<StringType> {
        static SharpyGenerator() {
            Gen = Create();
            Configurement = Gen.Config;
        }

        private SharpyGenerator(Config config) : base(new Generator(config)) {
            Config = config;
        }

        /// <summary>
        ///     <para>Configures Generator.</para>
        /// </summary>
        public Config Config { get; }

        private static SharpyGenerator Gen { get; }

        /// <summary>
        ///     <para>Configures Generator.</para>
        /// </summary>
        public static Config Configurement { get; }

        /// <summary>
        ///     <para>Creates a new instance of Generator.</para>
        /// </summary>
        /// <returns></returns>
        public static SharpyGenerator Create() => new SharpyGenerator(new Config());

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IGenerator<StringType>, T> func, int count = 10)
            => Gen.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt;.</para>
        ///     <para>Includes an integer counting iterations.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateEnumerable<T>(Func<IGenerator<StringType>, int, T> func, int count = 10)
            => Gen.GenerateMany(func, count);

        /// <summary>
        ///     <para>Generates a &lt;T&gt;.</para>
        /// </summary>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <returns></returns>
        public static T GenerateInstance<T>(Func<IGenerator<StringType>, T> func) => Gen.Generate(func);

        /// <inheritdoc />
        public override string ToString() => $"Configurement for Random Generator {Config}";
    }
}