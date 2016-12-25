using System;
using System.Collections.Generic;

namespace Sharpy {
    /// <summary>
    /// </summary>
    public static class GeneratorExtensions {
        /// <summary>
        ///     <para>Generates an instance of TResult</para>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func">The generator supplied is used to provide data for your object.</param>
        public static TResult Generate<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, TResult> func) where TGenerator : Generator => func(generator);

        /// <summary>
        ///     <para>Creates an IEnumerable&lt;TResult&gt; by using your mapping instruction.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        /// <param name="generator"></param>
        /// <param name="func">The generator supplied is used to provide data for your object.</param>
        public static IEnumerable<TResult> GenerateSequence<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, TResult> func, int count) where TGenerator : Generator {
            for (var i = 0; i < count; i++)
                yield return func(generator);
        }

        /// <summary>
        ///     <para>Creates an IEnumerable&lt;TResult&gt; by using your mapping instruction.</para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        /// <param name="generator"></param>
        /// <param name="func">The argument supplied is used to provide data for your object.</param>
        public static IEnumerable<TResult> GenerateSequence<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, int, TResult> func, int count) where TGenerator : Generator {
            for (var i = 0; i < count; i++) yield return func(generator, i);
        }

        /// <summary>
        ///     <para>
        ///         Iterates through IEnumerable&lt;TSource&gt; exposing each element together with a generator.
        ///     </para>
        /// </summary>
        public static IEnumerable<TResult> GenerateBySequence<TGenerator, TSource, TResult>(this TGenerator generator,
            IEnumerable<TSource> source, Func<TGenerator, TSource, TResult> func) where TGenerator : Generator {
            foreach (var element in source) yield return func(generator, element);
        }

        /// <summary>
        ///     <para>
        ///         Iterates through IEnumerable&lt;TSource&gt; exposing each element together with a generator.
        ///     </para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        public static IEnumerable<TResult> GenerateBySequence<TGenerator, TSource, TResult>(this TGenerator generator,
            IEnumerable<TSource> source, Func<TGenerator, TSource, int, TResult> func) where TGenerator : Generator {
            var i = 0;
            foreach (var element in source) yield return func(generator, element, i++);
        }
    }
}