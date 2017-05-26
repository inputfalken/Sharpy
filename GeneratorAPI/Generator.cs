using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    /// <summary>
    ///     Contains various methods for creating IGenerator&lt;T&gt;
    /// </summary>
    public static class Generator {
        /// <summary>
        ///     Contains methods for creating Generators with various Providers.
        ///     <remarks>
        ///         The point of this class is to contain extension methods from other libraries.
        ///     </remarks>
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();

        /// <summary>
        ///     Creates a Generator&lt;T&gt; with the type provided.
        /// </summary>
        public static IGenerator<T> Create<T>(T t) {
            return new Fun<T>(() => t);
        }

        /// <summary>
        ///     Creates a lazy Generator&lt;T&gt; with the type provided.
        /// </summary>
        public static IGenerator<T> Lazy<T>(Lazy<T> lazy) {
            return new Fun<T>(() => lazy.Value);
        }

        /// <summary>
        ///     Creates a lazy Generator&lt;T&gt; with the type provided.
        /// </summary>
        public static IGenerator<T> Lazy<T>(Func<T> fn) {
            return Lazy(new Lazy<T>(fn));
        }


        /// <summary>
        ///     Creates a Generator&lt;T&gt; where each generation will invoke and use the function supplied.
        ///     <remarks>
        ///         Do not instantiate types here.
        ///         If you want to use a type with methods to get data use Generator.<see cref="Create{T}" />
        ///     </remarks>
        /// </summary>
        public static IGenerator<T> Function<T>(Func<T> fn) {
            return new Fun<T>(fn);
        }

        /// <summary>
        ///     Creates a Generator based on a Enumerable which resets if the end is reached.
        /// </summary>
        public static IGenerator<T> CircularSequence<T>(IEnumerable<T> enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return new Seq<T>(enumerable);
        }

        /// <summary>
        ///     Filters the generation to fit the predicate.
        ///     <remarks>
        ///         Use with Caution: Bad predicates cause the method to throw exception if threshold is reached.
        ///     </remarks>
        /// </summary>
        public static IGenerator<TSource> Where<TSource>(this IGenerator<TSource> generator,
            Func<TSource, bool> predicate,
            int threshold = 100000) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => {
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
        public static IGenerator<TSource> Do<TSource>(this IGenerator<TSource> generator, Action<TSource> action) {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => {
                var generation = generator.Generate();
                action(generation);
                return generation;
            });
        }

        /// <summary>
        ///     Maps Generator&lt;T&gt; into Generator&lt;TResult&gt;
        /// </summary>
        public static IGenerator<TResult> Select<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, TResult> generatorSelector) {
            if (generatorSelector == null) throw new ArgumentNullException(nameof(generatorSelector));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => generatorSelector(generator.Generate()));
        }

        /// <summary>
        ///     Yields count ammount of items into an IEnumerable&lt;T&gt;.
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
        ///     Flattens Generator&lt;T&gt;
        /// </summary>
        public static IGenerator<TResult> SelectMany<TSource, TResult>(
            this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TResult>> generatorSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (generatorSelector == null) throw new ArgumentNullException(nameof(generatorSelector));
            return Function(() => generatorSelector(generator.Generate()).Generate());
        }

        /// <summary>
        ///     Flattens Generator&lt;T&gt;
        ///     With a compose function using &lt;T&gt; and &lt;TResult&gt;
        /// </summary>
        public static IGenerator<TCompose> SelectMany<TSource, TResult, TCompose>(this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TResult>> generatorSelector,
            Func<TSource, TResult, TCompose> composer) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (generatorSelector == null) throw new ArgumentNullException(nameof(generatorSelector));
            if (composer == null) throw new ArgumentNullException(nameof(composer));
            return generator.SelectMany(a => generatorSelector(a).SelectMany(r => Function(() => composer(a, r))));
        }

        /// <summary>
        ///     Flattens a IGenerator with a IEnumerable
        /// </summary>
        public static IGenerator<TResult> SelectMany<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, IEnumerable<TResult>> enumerableSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (enumerableSelector == null) throw new ArgumentNullException(nameof(enumerableSelector));
            var sequence = new Seq<TResult>(() => enumerableSelector(generator.Generate()));
            return Function(() => sequence.Generate());
        }

        /// <summary>
        ///     Flattens a IGenerator with a IEnumerable And composes the values
        /// </summary>
        public static IGenerator<TCompose> SelectMany<TSource, TResult, TCompose>(this IGenerator<TSource> generator,
            Func<TSource, IEnumerable<TResult>> enumerableSelector, Func<TSource, TResult, TCompose> composer) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (enumerableSelector == null) throw new ArgumentNullException(nameof(enumerableSelector));
            if (composer == null) throw new ArgumentNullException(nameof(composer));
            return generator.SelectMany(source => enumerableSelector(source)
                .Select(result => composer(source, result)));
        }

        /// <summary>
        ///     Combine generation and compose the generation.
        /// </summary>
        public static IGenerator<TResult> Zip<TSource, TSecond, TResult>(this IGenerator<TSource> first,
            IGenerator<TSecond> second,
            Func<TSource, TSecond, TResult> fn) {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            return Function(() => fn(first.Generate(), second.Generate()));
        }

        /// <summary>
        ///     Creates a Dictionary with it's count equal to count argument. Key and Value will be defined in the following
        ///     functions.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(this IGenerator<TSource> generator,
            int count, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector) {
            return generator.Take(count).ToDictionary(keySelector, valueSelector);
        }

        /// <summary>
        ///     Creates a list with it's length equal to the count supplied.
        /// </summary>
        public static List<TSource> ToList<TSource>(this IGenerator<TSource> generator, int count) {
            return generator.Take(count).ToList();
        }

        /// <summary>
        ///     Creates an array with it's length equal to the lenght supplied.
        /// </summary>
        public static TSource[] ToArray<TSource>(this IGenerator<TSource> generator, int length) {
            return generator.Take(length).ToArray();
        }

        /// <summary>
        ///     A Generator using Func&lt;T&gt;
        /// </summary>
        private class Fun<T> : IGenerator<T> {
            private readonly Func<T> _fn;

            /// <summary>
            ///     Creates a Generator&lt;T&gt; where each generation will invoke the argument.
            ///     <remarks>
            ///         Do not instantiate types here.
            ///         If you want to instantiate types use  static method Generator.<see cref="Generator.Create{T}" />
            ///     </remarks>
            /// </summary>
            public Fun(Func<T> fn) {
                if (fn != null) _fn = fn;
                else throw new ArgumentNullException(nameof(fn));
            }


            /// <summary>
            ///     Gives &lt;T&gt;
            /// </summary>
            public T Generate() {
                return _fn();
            }
        }

        /// <summary>
        ///     A Generator using IEnumerable&lt;T&gt;
        /// </summary>
        private class Seq<T> : IGenerator<T> {
            private readonly Lazy<IEnumerator<T>> _lazyEnumerator;

            public Seq(IEnumerable<T> enumerable) {
                _lazyEnumerator = new Lazy<IEnumerator<T>>(enumerable.CacheGeneratedResults().GetEnumerator);
            }

            public Seq(Func<IEnumerable<T>> fn) : this(Invoker(fn)) { }

            private IEnumerator<T> Enumerator {
                get { return _lazyEnumerator.Value; }
            }

            public T Generate() {
                if (Enumerator.MoveNext()) return Enumerator.Current;
                Enumerator.Reset();
                Enumerator.MoveNext();
                return Enumerator.Current;
            }

            /// <summary>
            ///     QUAS WEX EXORT
            /// </summary>
            private static IEnumerable<T> Invoker(Func<IEnumerable<T>> fn) {
                while (true) foreach (var element in fn()) yield return element;
            }
        }
    }


    /// <summary>
    ///     Contains methods for creating Generators.
    ///     <remarks>
    ///         The point of this class is to contain extension methods from other libraries.
    ///     </remarks>
    /// </summary>
    public class GeneratorFactory {
        /// <summary>
        ///     A Generator using System.Random as provider.
        /// </summary>
        public IGenerator<Random> Random(Random random) {
            return Generator.Create(random);
        }

        /// <summary>
        ///     A Guid Generator
        /// </summary>
        public IGenerator<Guid> Guid() {
            return Generator.Function(System.Guid.NewGuid);
        }
    }
}