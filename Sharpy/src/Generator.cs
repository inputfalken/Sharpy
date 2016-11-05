using System;
using System.Collections.Generic;

namespace Sharpy {
    /// <summary>
    ///     <para>Represents an abstract class which Generators will derive from.</para>
    ///     <para>To use this class you have to create your own implementation of  GeneratorSource. Then Create a class which inherits from this class.</para>
    /// </summary>
    /// <typeparam name="TStringArg">Argument for the method String in IGenerator</typeparam>
    public abstract class Generator<TStringArg> {
        /// <summary>
        /// <para>Pass your Implementation of GeneratorSource</para>
        /// </summary>
        /// <param name="generatorSource"></param>
        protected Generator(IGeneratorSource<TStringArg> generatorSource) {
            GeneratorSource = generatorSource;
        }

        private IGeneratorSource<TStringArg> GeneratorSource { get; }


        private T Instance<T>(Func<IGeneratorSource<TStringArg>, int, T> func, int i) => func(GeneratorSource, i);
        private T Instance<T>(Func<IGeneratorSource<TStringArg>, T> func) => func(GeneratorSource);

        /// <summary>
        ///     <para>Will generate a &lt;T&gt;</para>
        /// </summary>
        /// <returns></returns>
        public virtual T Generate<T>(Func<IGeneratorSource<TStringArg>, T> func) => Instance(func);


        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt; </para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        public virtual IEnumerable<T> GenerateMany<T>(Func<IGeneratorSource<TStringArg>, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Instance(func);
        }

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt; </para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        public virtual IEnumerable<T> GenerateMany<T>(Func<IGeneratorSource<TStringArg>, int, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Instance(func, i);
        }
    }
}