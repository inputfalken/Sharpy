using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    public static class Generator {
        /// <summary>
        ///     <para>
        ///         Contains methods for creating Generators with various Providers.
        ///     </para>
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();

        public static Generator<TResult> Create<TResult>(TResult t) {
            return new Generator<TResult>(() => t);
        }
    }

    public class Generator<T> {
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

        private Generator(IEnumerable<T> infiniteEnumerable) {
            _generations = infiniteEnumerable;
            _enumerator = new Lazy<IEnumerator<T>>(_generations.GetEnumerator);
        }

        public Generator(Func<T> fn) {
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
        public Generator<TResult> Select<TResult>(Func<T, TResult> fn) {
            return new Generator<TResult>(_generations.Select(fn));
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
        public Generator<TResult> SelectMany<TResult>(Func<T, Generator<TResult>> fn) {
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return new Generator<TResult>(() => fn(Take()).Take());
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
        public Generator<TCompose> SelectMany<TResult, TCompose>(Func<T, Generator<TResult>> fn,
            Func<T, TResult, TCompose> composer) {
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            if (composer == null) throw new ArgumentNullException(nameof(composer));
            return SelectMany(a => fn(a).SelectMany(r => new Generator<TCompose>(() => composer(a, r))));
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
        public Generator<T> Where(Func<T, bool> predicate) {
            return new Generator<T>(_generations.Where(predicate));
        }

        /// <summary>
        ///     <para>
        ///         Combine generation and compose the generation.
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <param name="generator"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generator<TResult> Zip<TResult, TSecond>(Generator<TSecond> generator, Func<T, TSecond, TResult> fn) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return generator.Select(second => fn(Take(), second));
        }

        /// <summary>
        /// Exposes &lt;T&gt;.
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generator<T> Do(Action<T> fn) {
            if (fn == null) {
                throw new ArgumentNullException(nameof(fn));
            }
            return new Generator<T>(() => {
                var generation = Take();
                fn(generation);
                return generation;
            });
        }
    }

    /// <summary>
    ///     <para>
    ///         Contains methods for creating Generators with various Providers.
    ///     </para>
    /// </summary>
    public class GeneratorFactory {
        /// <summary>
        ///     <para>
        ///         A Generator using System.Random as provider.
        ///     </para>
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public Generator<Random> Random(Random random) {
            return Create(random);
        }

        /// <summary>
        ///     <para>
        ///         Create a Generator with TProivder as provider.
        ///     </para>
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public Generator<TProvider> Create<TProvider>(TProvider provider) {
            return new Generator<TProvider>(() => provider);
        }
    }
}