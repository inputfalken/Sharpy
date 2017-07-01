using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    /// <summary>
    ///     Contains various methods for creating <see cref="IGenerator{T}"/>
    /// </summary>
    public static partial class Generator {
        /// <summary>
        ///     Contains various methods for creating <see cref="IGenerator{T}"/>
        ///     <remarks>
        ///         The point of this class is to contain extension methods from other libraries.
        ///     </remarks>
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}"/> where each invokation of <see cref="IGenerator{T}.Generate"/> will use the same &lt;T&gt;.
        ///     <para> </para>
        ///     This is useful if you want to instantiate a single object and call methods from it.
        /// </summary>
        public static IGenerator<T> Create<T>(T t) {
            return new Fun<T>(() => t);
        }

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}"/> where each invokation of <see cref="IGenerator{T}.Generate"/> will use the result of argument <see cref="System.Lazy{T}"/>.
        ///     <para> </para>
        ///     This is useful if you want to instantiate a single object lazily and call methods from it.
        /// </summary>
        public static IGenerator<T> Lazy<T>(Lazy<T> lazy) {
            if (lazy == null) {
                throw new ArgumentNullException(nameof(lazy));
            }
            return new Fun<T>(() => lazy.Value);
        }

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}"/> where each invokation of <see cref="IGenerator{T}.Generate"/> will use first result of argument <see cref=" Func&lt;T&gt;"/>.
        ///     <para> </para>
        ///     This is useful if you want to instantiate a single object lazily and call methods from it.
        /// </summary>
        public static IGenerator<T> Lazy<T>(Func<T> fn) {
            return Lazy(new Lazy<T>(fn));
        }


        /// <summary>
        ///     Creates a <see cref="IGenerator{T}"/> where each invokation of <see cref="IGenerator{T}.Generate"/> will invoke the argument <see cref=" Func&lt;T&gt;"/>.
        ///     <para> </para>
        ///     <remarks>
        ///         Do not instantiate types here.
        ///         If you want to use a type with methods to get data use Generator.<see cref="Create{T}" />
        ///     </remarks>
        /// </summary>
        public static IGenerator<T> Function<T>(Func<T> fn) {
            return new Fun<T>(fn);
        }

        /// <summary>
        ///     Creates a <see cref="IGenerator{T}"/> based on a <see cref="IEnumerable{T}"/>.
        ///     <para> </para>
        ///     <remarks>
        ///         If <see cref="IEnumerable{T}"/> ends it will reset and restart.
        ///     </remarks>
        /// </summary>
        public static IGenerator<T> CircularSequence<T>(IEnumerable<T> enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return new Seq<T>(enumerable);
        }

        /// <summary>
        ///     Creates a IGenerator&lt;object&gt; based on a <see cref="IEnumerable"/> which resets if the end is reached.
        /// </summary>
        public static IGenerator<object> CircularSequence(IEnumerable enumerable) {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            // TODO add proper implementation so cast can be skipped.
            return new Seq<object>(enumerable.Cast<object>());
        }

        /// <summary>
        ///     Casts each element of <see cref=" IGenerator&lt;TSource&gt;"/> to <see cref=" IGenerator&lt;TResult&gt;"/>
        /// </summary>
        public static IGenerator<TResult> Cast<TResult>(this IGenerator generator) {
            var result = generator as IGenerator<TResult>;
            if (result != null) return result;
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => (TResult) generator.Generate());
        }

        /// <summary>
        ///     Filters the <see cref=" IGenerator&lt;TSource&gt;"/> by the predicate.
        ///     <remarks>
        ///         Use with caution, bad <see cref=" Predicate&lt;TSource&gt;"/> usage causes the method to throw exception if threshold is reached.
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
        ///     Exposes TSource in <see cref=" IGenerator&lt;TSource&gt;"/>
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
        ///     Maps <see cref=" IGenerator&lt;TSource&gt;"/> to <see cref=" IGenerator&lt;TResult&gt;"/> 
        /// </summary>
        public static IGenerator<TResult> Select<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, TResult> generatorSelector) {
            if (generatorSelector == null) throw new ArgumentNullException(nameof(generatorSelector));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return Function(() => generatorSelector(generator.Generate()));
        }

        /// <summary>
        ///     Maps <see cref=" IGenerator&lt;TSource&gt;"/> to <see cref=" IGenerator&lt;TResult&gt;"/> 
        /// </summary>
        public static IGenerator<TResult> Select<TSource, TResult>(this IGenerator<TSource> generator,
            Func<TSource, int, TResult> generatorCountSelector) {
            var count = 0;
            return generator.Select(source => generatorCountSelector(source, count++));
        }

        /// <summary>
        ///      Creates a <see cref=" IEnumerable&lt;TSource&gt;"/> by invoking <see cref="IGenerator{T}.Generate"/> with count amount of times.
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
        ///     Flattens <see cref=" IGenerator&lt;TSource&gt;"/> with <see cref=" IGenerator&lt;TResult&gt;"/>
        /// </summary>
        public static IGenerator<TResult> SelectMany<TSource, TResult>(
            this IGenerator<TSource> generator,
            Func<TSource, IGenerator<TResult>> generatorSelector) {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (generatorSelector == null) throw new ArgumentNullException(nameof(generatorSelector));
            return Function(() => generatorSelector(generator.Generate()).Generate());
        }

        /// <summary>
        ///     Flattens <see cref=" IGenerator&lt;TSource&gt;"/> with <see cref=" IGenerator&lt;TResult&gt;"/>
        ///     With a compose function using <see cref="IGenerator{T}.Generate"/> from <see cref=" IGenerator&lt;TSource&gt;"/> and <see cref=" IGenerator&lt;TResult&gt;"/>
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
        ///     Combine <see cref=" IGenerator&lt;TSource&gt;"/> with <see cref=" IGenerator&lt;TSecond&gt;"/> and composes <see cref="IGenerator{T}.Generate"/> from both to <see cref=" IGenerator&lt;TResult&gt;"/>
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
        ///     Creates a <see cref="Dictionary{TKey,TValue}"/> by invoking <see cref="IGenerator{T}.Generate"/> with count amount of times.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(this IGenerator<TSource> generator,
            int count, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector) {
            return generator.Take(count).ToDictionary(keySelector, valueSelector);
        }

        /// <summary>
        ///     Creates a <see cref="List{T}"/> by invoking <see cref="IGenerator{T}.Generate"/> with count amount of times.
        /// </summary>
        public static List<TSource> ToList<TSource>(this IGenerator<TSource> generator, int count) {
            return generator.Take(count).ToList();
        }

        /// <summary>
        ///     Creates an <see cref="Array"/> by invoking <see cref="IGenerator{T}.Generate"/> with length amount of times.
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

            object IGenerator.Generate() {
                return Generate();
            }
        }

        /// <summary>
        ///     A Generator using <see cref="IEnumerable{T}"/>
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

            object IGenerator.Generate() {
                return Generate();
            }
        }
    }


    /// <summary>
    ///     Contains methods for creating <see cref="IGenerator{T}"/>.
    ///     <remarks>
    ///         The point of this class is to contain extension methods from other libraries.
    ///     </remarks>
    /// </summary>
    public class GeneratorFactory {
        /// <summary>
        ///     Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="int"/>.
        ///     Which randomizes the value by using <see cref="Random"/> for each invokation of <see cref="IGenerator{T}.Generate"/>.
        ///     <para>   </para>
        ///     Argument min is the min value you want to randomize.
        ///     Argument max is the max value you want to randomize.
        ///     The seed argument will make <see cref="Random"/> more deterministic and always give the same results.
        /// </summary>
        public IGenerator<int> Randomizer(int min, int max, int? seed = null) {
            return Generator
                .Create(CreateRandom(seed))
                .Select(rnd => rnd.Next(min, max));
        }

        /// <summary>
        ///     Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="int"/>.
        ///     Which randomizes the value by using <see cref="Random"/> for each invokation of <see cref="IGenerator{T}.Generate"/>.
        ///     <para>   </para>
        ///     Argument max is the max value you want to randomize.
        ///     The seed argument will make <see cref="Random"/> more deterministic and always give the same results.
        /// </summary>
        public IGenerator<int> Randomizer(int max, int? seed = null) {
            return Generator
                .Create(CreateRandom(seed))
                .Select(rnd => rnd.Next(max));
        }

        /// <summary>
        ///     Creates <see cref="Random"/> with seed if it's not set to null.
        /// </summary>
        private static Random CreateRandom(int? seed) {
            return seed == null
                ? new Random()
                : new Random(seed.Value);
        }

        /// <summary>
        ///     Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="int"/>.
        ///     Which randomizes the value by using <see cref="Random"/> for each invokation of <see cref="IGenerator{T}.Generate"/>.
        ///     <para>   </para>
        ///     The seed argument will make <see cref="Random"/> more deterministic and always give the same results.
        /// </summary>
        public IGenerator<int> Randomizer(int? seed = null) {
            return Generator
                .Create(CreateRandom(seed))
                .Select(rnd => rnd.Next());
        }


        /// <summary>
        ///     Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="System.Guid"/>
        ///     which generates new a <see cref="System.Guid"/> for every invokation of <see cref="IGenerator{T}.Generate"/>.
        /// </summary>
        public IGenerator<Guid> Guid() {
            return Generator.Function(System.Guid.NewGuid);
        }

        /// <summary>
        ///     Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="int"/>
        ///     which increments by 1 for every invokation of <see cref="IGenerator{T}.Generate"/>.
        ///     <para> </para>
        ///     The start argument will change the starting point for the incrementation.
        ///     <para> </para>
        ///     <remarks>
        ///         Throws <see cref="OverflowException"/> if the value exceeds <see cref="int"/>.<see cref="int.MaxValue"/>.
        ///     </remarks>
        /// </summary>
        public IGenerator<int> Incrementer(int start = 0) {
            return Generator.Function(() => checked(start++));
        }

        /// <summary>
        ///     Creates <see cref="IGenerator{T}"/> whose generic argument is <see cref="int"/>
        ///     which decrements the value by one for each generation and starts at the argument.
        ///     <para> </para>
        ///     The start argument will change the starting point for the decrementation.
        ///     <para> </para>
        ///     <remarks>
        ///         Throws <see cref="OverflowException"/> if the value gets below <see cref="int"/>.<see cref="int.MinValue"/>.
        ///     </remarks>
        /// </summary>
        public IGenerator<int> Decrementer(int start = 0) {
            return Generator.Function(() => checked(start--));
        }
    }
}