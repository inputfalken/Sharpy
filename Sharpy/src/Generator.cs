using System;
using System.Collections.Generic;

namespace Sharpy {
    /// <summary>
    ///     <para>This class is used to create one or many instances of&lt;T&gt;</para>
    /// </summary>
    /// <typeparam name="TSource">The type which will be passed to all delagates in the generation methods</typeparam>
    public class Generator<TSource> {
        /// <summary>
        ///     <para>Sets the TSource of this Generator instance</para>
        ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
        /// </summary>
        /// <typeparam>The type which will be passed as a in parameter to all generation methods arguments</typeparam>
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
        ///     <para>Gives a IEnumerable&lt;T&gt; of generated items.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The item to be returned will be generated.</param>
        public IEnumerable<T> GenerateMany<T>(Func<TSource, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Generate(func);
        }

        /// <summary>
        ///     <para>Gives a IEnumerable&lt;T&gt; of generated items.</para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The item to be returned will be generated.</param>
        public IEnumerable<T> GenerateMany<T>(Func<TSource, int, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Instance(func, i);
        }

        /// <summary>
        /// </summary>
        /// <param name="tSource"></param>
        /// <returns></returns>
        public static Generator<TSource> Custom(TSource tSource)
            => new Generator<TSource>(tSource);
    }
}