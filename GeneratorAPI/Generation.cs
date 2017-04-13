using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    /// <summary>
    /// </summary>
    public static class Generation {
        /// <summary>
        ///     Returns a IEnumerable with the same ammount as given.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="generation"></param>
        /// <param name="ammount"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Take<TSource>(this IGeneration<TSource> generation, int ammount) {
            for (var index = 0; index < ammount; index++) yield return generation.Take();
        }

        /// <summary>
        ///     Returns an array with same lenght as argument
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="generation"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static TSource[] ToArray<TSource>(this IGeneration<TSource> generation, int length) {
            return generation.Take(length).ToArray();
        }

        /// <summary>
        ///     Returns a list with same count as argument
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="generation"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<TSource> ToList<TSource>(this IGeneration<TSource> generation, int count) {
            return generation.Take(count).ToList();
        }

        public static IGeneration<TResult> Select<TSource, TResult>(this IGeneration<TSource> generation,
            Func<TSource, TResult> fn) {
            return new DelegateGeneration<TResult>(() => fn(generation.Take()));
        }

        public static IGeneration<TResult> SelectMany<TSource, TResult>(this IGeneration<TSource> generation,
            Func<TSource, IGeneration<TResult>> fn) {
            return new DelegateGeneration<TResult>(() => fn(generation.Take()).Take());
        }

        public static IGeneration<TCompose> SelectMany<TSource, TResult, TCompose>(this IGeneration<TSource> generation,
            Func<TSource, IGeneration<TResult>> fn, Func<TSource, TResult, TCompose> composer) {
            return generation.SelectMany(source => fn(source)
                .SelectMany(result => new DelegateGeneration<TCompose>(() => composer(source, result))));
        }


        /// <summary>
        ///     <para>
        ///         Turn an IEnumerable into a IGeneration.
        ///     </para>
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        private static IGeneration<T> Enumerable<T>(IEnumerable<T> enumerable) {
            return new EnumerableGeneration<T>(enumerable.CacheGeneratedResults());
        }


        private class EnumerableGeneration<T> : IGeneration<T>, IEnumerable<T> {
            private readonly IEnumerable<T> _enumerable;
            private readonly Lazy<IEnumerator<T>> _enumerator;

            public EnumerableGeneration(IEnumerable<T> enumerable) {
                if (enumerable.Any()) _enumerable = enumerable;
                else throw new ArgumentException($"{nameof(enumerable)} cannot be empty");
                _enumerator = new Lazy<IEnumerator<T>>(enumerable.GetEnumerator);
            }

            public IEnumerator<T> GetEnumerator() {
                return _enumerable.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return ((IEnumerable) _enumerable).GetEnumerator();
            }

            public T Take() {
                while (true) {
                    if (_enumerator.Value.MoveNext()) return _enumerator.Value.Current;
                    _enumerator.Value.Reset();
                }
            }
        }
    }
}