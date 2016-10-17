using System;
using System.Collections.Generic;

namespace Sharpy {
    public class Generator<T, TSource> {
        private Func<TSource, int, T> FuncIterator { get; }
        private TSource Source { get; }
        private Func<TSource, T> Func { get; }
        private int Iteratation { get; set; }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<TSource, T> func, TSource source) {
            Func = func;
            Source = source;
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<TSource, int, T> func, TSource source) {
            FuncIterator = func;
            Source = source;
        }

        private T Generate(int i) => FuncIterator == null ? Func(Source) : FuncIterator(Source, i);

        /// <summary>
        ///     Will give back one instance of the specified Type
        /// </summary>
        /// <returns></returns>
        public T Generate() => FuncIterator == null ? Func(Source) : FuncIterator(Source, Iteratation++);

        /// <summary>
        ///     Will give back an IEnumerable with the specified type.
        ///     Which contains the ammount of elements.
        /// </summary>
        /// <param name="ammount"></param>
        public IEnumerable<T> GenerateEnumerable(int ammount) {
            for (var i = 0; i < ammount; i++)
                yield return Generate(i);
        }
    }
}