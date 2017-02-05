using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpy {
    /// <summary>
    /// </summary>
    public static class GeneratorExtensions {
        /// <summary>
        ///     <para>
        ///         Generates the result from the Func argument.
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator.</param>
        public static TResult Generate<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, TResult> func) where TGenerator : Generator => func(generator);

        /// <summary>
        ///     <para>
        ///         Generates an IEnumerable of the result from the Func.
        ///     </para>
        ///     <para>
        ///         The count argument is the Count of the IEnumerable&lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator.</param>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        public static IEnumerable<TResult> GenerateSequence<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, TResult> func, int count) where TGenerator : Generator {
            for (var i = 0; i < count; i++) yield return func(generator);
        }

        /// <summary>
        ///     <para>
        ///         Generates an IEnumerable of the result from the Func.
        ///     </para>
        ///     <para>
        ///         Includes an integer containing the current iteration.
        ///     </para>
        ///     <para>
        ///         The count argument is the Count of the IEnumerable&lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator combines with an integer tracking the current iteration.</param>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        public static IEnumerable<TResult> GenerateSequence<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, int, TResult> func, int count) where TGenerator : Generator {
            for (var i = 0; i < count; i++) yield return func(generator, i);
        }

        /// <summary>
        ///     <para>
        ///         Iterates through IEnumerable&lt;TSource&gt; exposing each element together with a generator.
        ///     </para>
        ///     <para>
        ///         Generates an IEnumerable of the result from the Func.
        ///     </para>
        ///     <para>
        ///         The count will be the same as the Count of IEnumerable&lt;TSource&gt;
        ///     </para>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator combined with &lt;TSource&gt; from the source parameter.</param>
        /// <param name="source">TheIEnumerable&lt;TSource&gt; that will be iterated through</param>
        /// </summary>
        public static IEnumerable<TResult> GenerateBySequence<TGenerator, TSource, TResult>(this TGenerator generator,
            IEnumerable<TSource> source, Func<TGenerator, TSource, TResult> func) where TGenerator : Generator => source.Select(element => func(generator, element));

        /// <summary>
        ///     <para>
        ///         Iterates through IEnumerable&lt;TSource&gt; exposing each element together with a generator.
        ///     </para>
        ///     <para>
        ///         Generates an IEnumerable of the result from the Func.
        ///     </para>
        ///     <para>
        ///         The count will be the same as the Count of IEnumerable&lt;TSource&gt;
        ///     </para>
        ///     <para>
        ///         Includes an integer containing the current iteration.
        ///     </para>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator combined with &lt;TSource&gt; from the source parameter and an integer tracking the current iteration</param>
        /// <param name="source">The IEnumerable&lt;TSource&gt; that will be iterated through</param>
        /// </summary>
        public static IEnumerable<TResult> GenerateBySequence<TGenerator, TSource, TResult>(this TGenerator generator,
            IEnumerable<TSource> source, Func<TGenerator, TSource, int, TResult> func) where TGenerator : Generator => source.Select((element, i) => func(generator, element, i));


        /// <summary>
        ///     <para>
        ///         Turns the Generator into delegate Generator&lt;out T&gt;
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static Generator<TResult> ToDelegate<TSource, TResult>(this TSource generator,
            Func<TSource, TResult> func) where TSource : Generator => () => generator.Generate(func);

        /// <summary>
        ///     <para>
        ///         Maps Generator&lt;out TSource&gt; to Generator&lt;out TResult&gt;.
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static Generator<TResult> Map<TSource, TResult>(this Generator<TSource> generator,
            Func<TSource, TResult> func) => () => func(generator());

        /// <summary>
        ///     <para>
        ///         Flattens nested Generator&lt;out T&gt;
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        public static Generator<TResult> Bind<TSource, TResult>(this Generator<TSource> generator,
            Func<TSource, Generator<TResult>> func) => () => func(generator())();

        /// <summary>
        ///     <para>
        ///         Flattens nested Generator&lt;out T&gt;
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func"></param>
        /// <param name="func2"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static Generator<TResult> Bind<TSource, TResult, T>(this Generator<TSource> generator,
            Func<TSource, Generator<T>> func, Func<TSource, T, TResult> func2) {
            return () => {
                var invoke = generator();
                return func2(invoke, func(invoke)());
            };
        }

        private const int Threshold = 10000;

        /// <summary>
        ///     <para>
        ///         Invokes Generator&lt;out TSource&gt;, and passes the result to the predicate. If it succeeds the result is returned.
        ///         If it fails it will invoke Generator&lt;out TSource&gt; and repeat the process til threshold is reached.
        ///     </para>
        ///     <para>
        ///         Threshold is 10000 in this overload. You can specify it yourself in the other overload.
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="prediciate"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        public static Generator<TSource> Filter<TSource>(this Generator<TSource> generator,
            Func<TSource, bool> prediciate) {
            return () => {
                for (var i = 0; i < Threshold; i++) {
                    var result = generator();

                    if (prediciate(result)) return result;
                }
                //Is called if the for loop does not succeed in getting a true predicate
                throw new ArgumentException(
                    $"Could not match the predicate with {Threshold} attempts. Try using the overload where you can set your own threshold");
            };
        }

        /// <summary>
        ///     <para>
        ///         Invokes Generator&lt;out TSource&gt;, and passes the result to the predicate. If it succeeds the result is returned.
        ///         If it fails it will invoke Generator&lt;out TSource&gt; and repeat the process til threshold is reached.
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="prediciate"></param>
        /// <param name="threshold"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        public static Generator<TSource> Filter<TSource>(this Generator<TSource> generator,
            Func<TSource, bool> prediciate, int threshold) {
            return () => {
                for (var i = 0; i < threshold; i++) {
                    var result = generator();
                    if (prediciate(result)) return result;
                }
                //Is called if the for loop does not succeed in getting a true predicate.
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts.");
            };
        }

        /// <summary>
        ///     <para>
        ///         Creates an IEnumerable&lt;TResult&gt;
        ///     </para>
        ///     <para>
        ///         The count argument is the Count of the IEnumerable&lt;TSource&gt;
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="count"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TSource> GenerateSequence<TSource>(this Generator<TSource> generator, int count) {
            for (var i = 0; i < count; i++)
                yield return generator();
        }

        /// <summary>
        ///     <para>
        ///         Invokes Generator&lt;out TSource&gt;.
        ///     </para>
        /// </summary>
        public static TSource Generate<TSource>(this Generator<TSource> generator) => generator();

        /// <summary>
        ///     <para>
        ///         Exposes &lt;TSource&gt;.
        ///     </para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="action"></param>
        /// <typeparam name="TSource"></typeparam>
        public static Generator<TSource> Do<TSource>(this Generator<TSource> generator,
            Action<TSource> action) {
            return () => {
                var source = generator();
                action(source);
                return source;
            };
        }
    }
}
