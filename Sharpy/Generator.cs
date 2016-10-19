using System;
using System.Collections.Generic;

namespace Sharpy {
    public class Generator<TSource> {
        private TSource Source { get; }
        private int Iteratation { get; set; }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(TSource source) {
            Source = source;
        }


        private T Generate<T>(Func<TSource, int, T> func, int i) => func(Source, i);

        /// <summary>
        ///     Will give back one instance of the specified Type
        /// </summary>
        /// <returns></returns>
        public T Generate<T>(Func<TSource, T> func) => func(Source);

        public T Generate<T>(Func<TSource, int, T> func) => func(Source, Iteratation++);

        /// <summary>
        ///     Will give back an IEnumerable with the specified type.
        ///     Which contains the ammount of elements.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="func"></param>
        public IEnumerable<T> GenerateEnumerable<T>(Func<TSource, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Generate(func);
        }

        /// <summary>
        ///     Will give back an IEnumerable with the specified type.
        ///     Which contains the ammount of elements.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="func"></param>
        public IEnumerable<T> GenerateEnumerable<T>(Func<TSource, int, T> func, int count = 10) {
            for (var i = 0; i < count; i++)
                yield return Generate(func, i);
        }
    }
}