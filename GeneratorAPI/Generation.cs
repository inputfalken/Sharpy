using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    public class Generation<T> {
        /// <summary>
        ///     <para>
        ///         Infinite deferred invocations of _generation.
        ///     </para>
        /// </summary>
        private readonly IEnumerable<T> _generations;

        private readonly Lazy<IEnumerator<T>> _enumerator;

        private IEnumerator<T> Enumerator {
            get { return _enumerator.Value; }
        }

        private Generation(IEnumerable<T> infiniteEnumerable) {
            _generations = infiniteEnumerable;
            _enumerator = new Lazy<IEnumerator<T>>(_generations.GetEnumerator);
        }

        public Generation(Func<T> fn) {
            if (fn != null) {
                _generations = InfiniteEnumerable(fn);
                _enumerator = new Lazy<IEnumerator<T>>(_generations.GetEnumerator);
            }
            else throw new ArgumentNullException(nameof(fn));
        }

        private static IEnumerable<TResult> InfiniteEnumerable<TResult>(Func<TResult> fn) {
            while (true) yield return fn();
        }

        /// <summary>
        ///     <para>
        ///         Maps Generation&lt;T&gt; into Generation&lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<TResult> Select<TResult>(Func<T, TResult> fn) {
            return new Generation<TResult>(_generations.Select(fn));
        }


        /// <summary>
        ///     <para>
        ///         Gives &lt;T&gt;
        ///     </para>
        /// </summary>
        /// <returns></returns>
        public T Take() {
            Enumerator.MoveNext();
            return Enumerator.Current;
        }

        /// <summary>
        ///     <para>
        ///         Yields count ammount of items into an IEnumerable&lt;T&gt;.
        ///     </para>
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<T> Take(int count) {
            return _generations.Take(count);
        }

        /// <summary>
        ///     <para>
        ///         Flattens Generation&lt;T&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<TResult> SelectMany<TResult>(Func<T, Generation<TResult>> fn) {
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return new Generation<TResult>(() => fn(Take()).Take());
        }

        /// <summary>
        ///     <para>
        ///         Flattens Generation&lt;T&gt;
        ///         With a compose function using &lt;T&gt; and &lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TCompose"></typeparam>
        /// <param name="fn"></param>
        /// <param name="composer"></param>
        /// <returns></returns>
        public Generation<TCompose> SelectMany<TResult, TCompose>(Func<T, Generation<TResult>> fn,
            Func<T, TResult, TCompose> composer) {
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            if (composer == null) throw new ArgumentNullException(nameof(composer));
            return SelectMany(a => fn(a).SelectMany(r => new Generation<TCompose>(() => composer(a, r))));
        }

        /// <summary>
        ///     <para>
        ///         Filters the generation to fit the predicate.
        ///     </para>
        ///     <remarks>
        ///         Use with Caution: Bad predicates will make this method run forever.
        ///     </remarks>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Generation<T> Where(Func<T, bool> predicate) {
            return new Generation<T>(_generations.Where(predicate));
        }

        /// <summary>
        ///     <para>
        ///         Combine generation and compose the generation.
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <param name="generation"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<TResult> Zip<TResult, TSecond>(Generation<TSecond> generation, Func<T, TSecond, TResult> fn) {
            if (generation == null) throw new ArgumentNullException(nameof(generation));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return generation.Select(second => fn(Take(), second));
        }

        /// <summary>
        /// Exposes &lt;T&gt;.
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<T> Do(Action<T> fn) {
            if (fn == null) {
                throw new ArgumentNullException(nameof(fn));
            }
            return new Generation<T>(() => {
                var generation = _generation();
                fn(generation);
                return generation;
            });
        }
    }
}