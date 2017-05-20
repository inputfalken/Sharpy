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
        public static IGenerator<T> CreateWithProvider<T>(T t) {
            return new Generator<T>(() => t);
        }

        /// <summary>
        ///     Creates a lazy Generator&lt;T&gt; by using the same reference of &lt;T&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IGenerator<T> CreateLazy<T>(Lazy<T> lazy) {
            return new Generator<T>(() => lazy.Value);
        }

        /// <summary>
        ///     Creates a lazy Generator&lt;T&gt; by using the same reference of &lt;T&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IGenerator<T> CreateLazy<T>(Func<T> fn) {
            var lazy = new Lazy<T>(fn);
            return CreateLazy(lazy);
        }


        /// <summary>
        ///     Creates a Generator&lt;T&gt; where each generation will invoke <see cref="fn" />
        ///     <remarks>
        ///         Do not instantiate types here.
        ///         <para />
        ///         If you want to instantiate types use static method Generator.<see cref="CreateWithProvider{T}" />
        ///     </remarks>
        /// </summary>
        /// <param name="fn"></param>
        public static IGenerator<T> Create<T>(Func<T> fn) {
            return new Generator<T>(fn);
        }


        /// <summary>
        ///     <para>
        ///         Filters the generation to fit the predicate.
        ///     </para>
        ///     <remarks>
        ///         Use with Caution: Bad predicates will make this method run forever.
        ///     </remarks>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="predicate"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static IGenerator<TSource> Where<TSource>(this IGenerator<TSource> generator,
            Func<TSource, bool> predicate,
            int threshold = 100000) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return new Generator<TSource>(() => {
                for (var i = 0; i < threshold; i++) {
                    var generation = generator.Generate();
                    if (predicate(generation)) return generation;
                }
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts. ");
            });
        }


        /// <summary>
        ///     Exposes &lt;T&gt;.
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IGenerator<TSource> Do<TSource>(this IGenerator<TSource> generator, Action<TSource> fn) {
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return new Generator<TSource>(() => {
                var generation = generator.Generate();
                fn(generation);
                return generation;
            });
        }

        /// <summary>
        ///     <para>
        ///         Maps Generation&lt;T&gt; into Generation&lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="generator"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IGenerator<TResult> Select<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, TResult> fn) {
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return new Generator<TResult>(() => fn(generator.Generate()));
        }

        /// <summary>
        ///     <para>
        ///         Yields count ammount of items into an IEnumerable&lt;T&gt;.
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Take<TSource>(this IGenerator<TSource> generator, int count) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (count <= 0) throw new ArgumentException($"{nameof(count)} Must be more than zero");
            //Is needed so the above if statement is checked.
            return Iterator(count, generator);
        }

        private static IEnumerable<TSource> Iterator<TSource>(int count, IGenerator<TSource> generator) {
            for (var i = 0; i < count; i++) yield return generator.Generate();
        }

        /// <summary>
        ///     <para>
        ///         Flattens Generation&lt;T&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IGenerator<TResult> SelectMany<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TResult>> fn) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return new Generator<TResult>(() => fn(generator.Generate()).Generate());
        }

        /// <summary>
        ///     <para>
        ///         Flattens Generation&lt;T&gt;
        ///         With a compose function using &lt;T&gt; and &lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TCompose"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="generator"></param>
        /// <param name="fn"></param>
        /// <param name="composer"></param>
        /// <returns></returns>
        public static IGenerator<TCompose> SelectMany<TSource, TResult, TCompose>(this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TResult>> fn,
            Func<TSource, TResult, TCompose> composer) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            if (composer == null) throw new ArgumentNullException(nameof(composer));
            return generator.SelectMany(a => fn(a).SelectMany(r => new Generator<TCompose>(() => composer(a, r))));
        }

        /// <summary>
        ///     <para>
        ///         Combine generation and compose the generation.
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="firstGenerator"></param>
        /// <param name="secondGenerator"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static IGenerator<TResult> Zip<TResult, TSecond, TSource>(this IGenerator<TSource> firstGenerator,
            IGenerator<TSecond> secondGenerator,
            Func<TSource, TSecond, TResult> fn) {
            if (firstGenerator == null) throw new ArgumentNullException(nameof(firstGenerator));
            if (secondGenerator == null) throw new ArgumentNullException(nameof(secondGenerator));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return new Generator<TResult>(() => fn(firstGenerator.Generate(), secondGenerator.Generate()));
        }
    }

    internal class Generator<T> : IGenerator<T> {
        private readonly Func<T> _fn;

        /// <summary>
        ///     Creates a Generator&lt;T&gt; where each generation will invoke <see cref="fn" />
        ///     <remarks>
        ///         Do not instantiate types here.
        ///         <para />
        ///         If you want to instantiate types use  static method Generator.<see cref="Generator.CreateWithProvider{T}" />
        ///     </remarks>
        /// </summary>
        /// <param name="fn"></param>
        public Generator(Func<T> fn) {
            if (fn != null) _fn = fn;
            else throw new ArgumentNullException(nameof(fn));
        }


        /// <summary>
        ///     <para>
        ///         Gives &lt;T&gt;
        ///     </para>
        /// </summary>
        /// <returns></returns>
        public T Generate() {
            return _fn();
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
        public IGenerator<Random> Random(Random random) {
            return Generator.CreateWithProvider(random);
        }

        /// <summary>
        ///     <para>
        ///         A Guid Generator
        ///     </para>
        /// </summary>
        /// <returns></returns>
        public IGenerator<Guid> Guid() {
            return new Generator<Guid>(System.Guid.NewGuid);
        }
    }
}