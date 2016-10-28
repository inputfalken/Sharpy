using System;
using System.Collections.Generic;

namespace Sharpy {
    /// <summary>
    ///     <para>Is used to create one or many of &lt;T&gt; By using methods from this class.</para>
    /// </summary>
    /// <typeparam name="TSource">Will be passed to all delagates in the generation methods.</typeparam>
    public class Generator<TSource> {
        /// <summary>
        ///     <para>Sets the TSource of this Generator instance.</para>
        ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
        /// </summary>
        /// <typeparam>The in parameter for the delegates used in the Generation methods.</typeparam>
        protected Generator(TSource source) {
            Source = source;
        }

        private TSource Source { get; }


        private T Instance<T>(Func<TSource, int, T> func, int i) => func(Source, i);

        /// <summary>
        ///     <para>Will generate a &lt;T&gt;</para>
        /// </summary>
        /// <returns></returns>
        public T Generate<T>(Func<TSource, T> func) => func(Source);


        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt; </para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The item to be returned will be generated.</param>
        public IEnumerable<T> GenerateMany<T>(Func<TSource, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Generate(func);
        }

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt; </para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The item to be returned will be generated.</param>
        public IEnumerable<T> GenerateMany<T>(Func<TSource, int, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Instance(func, i);
        }

        /// <summary>
        ///     <para>Creates a Generator.</para>
        /// </summary>
        /// <typeparam name="TSource">&lt;TSource&gt; will be passed to all delagates in the generation methods</typeparam>
        /// <returns></returns>
        public static Generator<TSource> Custom(TSource tSource)
            => new Generator<TSource>(tSource);
    }
}