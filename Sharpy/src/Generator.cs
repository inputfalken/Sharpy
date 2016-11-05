using System;
using System.Collections.Generic;

namespace Sharpy {
    /// <summary>
    ///     <para>Is used to create one or many of &lt;T&gt; By using methods from this class.</para>
    /// </summary>
    /// <typeparam name="TStringArg">Argument for the method String in IGenerator</typeparam>
    public class Generator<TStringArg> {
        protected Generator(IGenerator<TStringArg> source) {
            Source = source;
        }

        private IGenerator<TStringArg> Source { get; }


        private T Instance<T>(Func<IGenerator<TStringArg>, int, T> func, int i) => func(Source, i);
        private T Instance<T>(Func<IGenerator<TStringArg>, T> func) => func(Source);

        /// <summary>
        ///     <para>Will generate a &lt;T&gt;</para>
        /// </summary>
        /// <returns></returns>
        public virtual T Generate<T>(Func<IGenerator<TStringArg>, T> func) => Instance(func);


        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt; </para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        public virtual IEnumerable<T> GenerateMany<T>(Func<IGenerator<TStringArg>, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Instance(func);
        }

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt; </para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        public virtual IEnumerable<T> GenerateMany<T>(Func<IGenerator<TStringArg>, int, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Instance(func, i);
        }

        /// <summary>
        ///     <para>Creates a Generator.</para>
        /// </summary>
        /// <typeparam name="TStringArg">&lt;TSource&gt; will be passed to all delagates in the generation methods</typeparam>
        /// <returns></returns>
        public static Generator<TStringArg> Custom(IGenerator<TStringArg> tSource)
            => new Generator<TStringArg>(tSource);
    }
}