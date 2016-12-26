using System;
using System.Collections.Generic;

namespace Sharpy {
    /// <summary>
    /// </summary>
    public static class GeneratorExtensions {
        /// <summary>
        ///     <para>Creates a &lt;TResult&gt;of the type returned from the function</para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator.</param>
        public static TResult Generate<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, TResult> func) where TGenerator : Generator => func(generator);

        /// <summary>
        ///     <para>Creates an IEnumerable&lt;TResult&gt; of the type returned from the function.</para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator.</param>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        public static IEnumerable<TResult> GenerateSequence<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, TResult> func, int count) where TGenerator : Generator {
            for (var i = 0; i < count; i++) yield return func(generator);
        }

        /// <summary>
        ///     <para>Creates an IEnumerable&lt;TResult&gt; of the type returned from the function.</para>
        ///     <para>Includes an integer containing the current iteration.</para>
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
        ///     <para>Creates an IEnumerable&lt;TResult&gt; of the type returned from the function.</para>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator combined with &lt;TSource&gt; from the source parameter.</param>
        /// <param name="source">TheIEnumerable&lt;TSource&gt; that will be iterated through</param>
        /// </summary>
        public static IEnumerable<TResult> GenerateBySequence<TGenerator, TSource, TResult>(this TGenerator generator,
            IEnumerable<TSource> source, Func<TGenerator, TSource, TResult> func) where TGenerator : Generator {
            foreach (var element in source) yield return func(generator, element);
        }

        /// <summary>
        ///     <para>
        ///         Iterates through IEnumerable&lt;TSource&gt; exposing each element together with a generator.
        ///     </para>
        ///     <para>Creates an IEnumerable&lt;TResult&gt; of the type returned from the function.</para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// <param name="generator"></param>
        /// <param name="func">Supplies a generator combined with &lt;TSource&gt; from the source parameter and an integer tracking the current iteration</param>
        /// <param name="source">The IEnumerable&lt;TSource&gt; that will be iterated through</param>
        /// </summary>
        public static IEnumerable<TResult> GenerateBySequence<TGenerator, TSource, TResult>(this TGenerator generator,
            IEnumerable<TSource> source, Func<TGenerator, TSource, int, TResult> func) where TGenerator : Generator {
            var i = 0;
            foreach (var element in source) yield return func(generator, element, i++);
        }
    }
}