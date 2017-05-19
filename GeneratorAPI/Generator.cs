using System;
using System.Collections.Generic;

namespace GeneratorAPI {
    public static class Generator {
        /// <summary>
        ///     <para>
        ///         Contains methods for creating Generators with various Providers.
        ///     </para>
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();

        /// <summary>
        ///     Creates a Generator&lt;T&gt; by using the same reference of &lt;T&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Generator<T> Create<T>(T t) {
            return new Generator<T>(() => t);
        }


        /// <summary>
        ///     Creates a lazy Generator&lt;T&gt; by using the same reference of &lt;T&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static Generator<T> Create<T>(Func<T> fn) {
            var lazy = new Lazy<T>(fn);
            return new Generator<T>(() => lazy.Value);
        }
    }

    public class Generator<T> {
        private readonly Func<T> _fn;

        /// <summary>
        ///     Creates a Generator&lt;T&gt; where each generation will invoke <see cref="fn" />
        ///     <remarks>
        ///         Do not instantiate types here.
        ///         <para />
        ///         If you want to instantiate types use  static method Generator.<see cref="Generator.Create{T}(T)" />
        ///     </remarks>
        /// </summary>
        /// <param name="fn"></param>
        public Generator(Func<T> fn) {
            if (fn != null) _fn = fn;
            else throw new ArgumentNullException(nameof(fn));
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
            if (fn != null) return new Generator<TResult>(() => fn(_fn()));
            throw new ArgumentNullException(nameof(fn));
        }


        /// <summary>
        ///     <para>
        ///         Gives &lt;T&gt;
        ///     </para>
        /// </summary>
        /// <returns></returns>
        public T Take() {
            return _fn();
        }

        /// <summary>
        ///     <para>
        ///         Yields count ammount of items into an IEnumerable&lt;T&gt;.
        ///     </para>
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<T> Take(int count) {
            if (count <= 0) throw new ArgumentException($"{nameof(count)} Must be more than zero");
            //Is needed so the above if statement is checked.
            return Iterator(count);
        }

        private IEnumerable<T> Iterator(int count) {
            for (var i = 0; i < count; i++) yield return Take();
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
        /// <param name="threshold"></param>
        /// <returns></returns>
        public Generator<T> Where(Func<T, bool> predicate, int threshold = 100000) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return new Generator<T>(() => {
                for (var i = 0; i < threshold; i++) {
                    var generation = Take();
                    if (predicate(generation)) return generation;
                }
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts. ");
            });
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
        ///     Exposes &lt;T&gt;.
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generator<T> Do(Action<T> fn) {
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return new Generator<T>(() => {
                var generation = Take();
                fn(generation);
                return generation;
            });
        }
    }

    /// <summary>
    ///     <para>
    ///         Contains methods for creating Generators.
    ///     </para>
    ///     <remarks>
    ///         The point of this class is to contain extension methods from other libraries.
    ///     </remarks>
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
            return Generator.Create(random);
        }

        /// <summary>
        ///     <para>
        ///         A Guid Generator
        ///     </para>
        /// </summary>
        /// <returns></returns>
        public Generator<Guid> Guid() {
            return new Generator<Guid>(System.Guid.NewGuid);
        }
    }
}