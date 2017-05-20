using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    /// <summary>
    ///     Contains various methods for creating IGenerator&lt;T&gt;
    /// </summary>
    public static class Generator {
        /// <summary>
        ///     <para>
        ///         Contains methods for creating Generators with various Providers.
        ///     </para>
        ///     <remarks>
        ///         The point of this class is to contain extension methods from other libraries.
        ///     </remarks>
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();

        /// <summary>
        ///     Creates a Generator&lt;T&gt; with the type provided.
        /// </summary>
        public static IGenerator<T> Create<T>(T t) {
            return new Generator<T>(() => t);
        }

        /// <summary>
        ///     Creates a lazy Generator&lt;T&gt; with the type provided.
        /// </summary>
        public static IGenerator<T> Lazy<T>(Lazy<T> lazy) {
            return new Generator<T>(() => lazy.Value);
        }

        /// <summary>
        ///     Creates a lazy Generator&lt;T&gt; with the type provided.
        /// </summary>
        public static IGenerator<T> Lazy<T>(Func<T> fn) {
            var lazy = new Lazy<T>(fn);
            return Lazy(lazy);
        }


        /// <summary>
        ///     Creates a Generator&lt;T&gt; where each generation will invoke and use the function supplied.
        ///     <remarks>
        ///         Do not instantiate types here.
        ///         <para />
        ///         If you want to use a type with methods to get data use Generator.<see cref="Create{T}" />
        ///     </remarks>
        /// </summary>
        public static IGenerator<T> Function<T>(Func<T> fn) {
            return new Generator<T>(fn);
        }

        /// <summary>
        ///     Creates a Generator based on a Enumerable which resets if the end is reached.
        /// </summary>
        public static IGenerator<T> CircularSequence<T>(IEnumerable<T> enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return new CircularEnumerable<T>(enumerable);
        }

        /// <summary>
        ///     <para>
        ///         Filters the generation to fit the predicate.
        ///     </para>
        ///     <remarks>
        ///         Use with Caution: Bad predicates cause the method to throw exception if threshold is reached.
        ///     </remarks>
        /// </summary>
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
        public static IGenerator<TResult> Zip<TResult, TSecond, TSource>(this IGenerator<TSource> firstGenerator,
            IGenerator<TSecond> secondGenerator,
            Func<TSource, TSecond, TResult> fn) {
            if (firstGenerator == null) throw new ArgumentNullException(nameof(firstGenerator));
            if (secondGenerator == null) throw new ArgumentNullException(nameof(secondGenerator));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return new Generator<TResult>(() => fn(firstGenerator.Generate(), secondGenerator.Generate()));
        }

        /// <summary>
        ///     Creates a Dictionary with it's count equal to count argument. Key and Value will be defined in the following functions.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(this IGenerator<TSource> generator,
            int count, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector) {
            return generator.Take(count).ToDictionary(keySelector, valueSelector);
        }
    }

    internal class Generator<T> : IGenerator<T> {
        private readonly Func<T> _fn;

        /// <summary>
        ///     Creates a Generator&lt;T&gt; where each generation will invoke the argument.
        ///     <remarks>
        ///         Do not instantiate types here.
        ///         <para />
        ///         If you want to instantiate types use  static method Generator.<see cref="Generator.Create{T}" />
        ///     </remarks>
        /// </summary>
        public Generator(Func<T> fn) {
            if (fn != null) _fn = fn;
            else throw new ArgumentNullException(nameof(fn));
        }


        /// <summary>
        ///     <para>
        ///         Gives &lt;T&gt;
        ///     </para>
        /// </summary>
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
        public IGenerator<Random> Random(Random random) {
            return Generator.Create(random);
        }

        /// <summary>
        ///     <para>
        ///         A Guid Generator
        ///     </para>
        /// </summary>
        public IGenerator<Guid> Guid() {
            return new Generator<Guid>(System.Guid.NewGuid);
        }
    }
}