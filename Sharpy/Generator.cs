using System;
using System.Collections.Generic;

namespace Sharpy {
    public class Generator<TSource> {
        /// <summary>
        ///     <para>Creates a Generator which you can use to create one instance or a collection of the given type</para>
        ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
        /// </summary>
        public Generator(TSource source) {
            Source = source;
        }

        private TSource Source { get; }


        private T Generate<T>(Func<TSource, int, T> func, int i) => func(Source, i);

        /// <summary>
        ///     <para>Will generate a new &lt;T&gt;</para>
        /// </summary>
        /// <returns></returns>
        public T Generate<T>(Func<TSource, T> func) => func(Source);


        /// <summary>
        ///     <para>Gives a IEnumerable&lt;T&gt; of generated items.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The item to be returned will be generated.</param>
        public IEnumerable<T> GenerateEnumerable<T>(Func<TSource, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Generate(func);
        }

        /// <summary>
        ///     <para>Will give back an IEnumerable&lt;T&gt; of generated items.</para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="func">The item to be returned will be generated.</param>
        public IEnumerable<T> GenerateEnumerable<T>(Func<TSource, int, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Generate(func, i);
        }
    }
}