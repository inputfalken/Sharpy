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
        /// <typeparam name="TResult">Mapping result.</typeparam>
        /// <typeparam name="TGenerator">Data provider.</typeparam>
        /// <returns></returns>
        public static TResult Generate<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, TResult> func) where TGenerator : Generator => func(generator);

        /// <summary>
        ///     <para>Generates an IEnumerable&lt;TResult&gt; </para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        /// <param name="generator"></param>
        /// <param name="func">The generator supplied is used to provide data for your object.</param>
        /// <typeparam name="TResult">Mapping result.</typeparam>
        /// <typeparam name="TGenerator">Data Provider.</typeparam>
        /// <returns></returns>
        public static IEnumerable<TResult> GenerateSequence<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, TResult> func, int count) where TGenerator : Generator {
            for (var i = 0; i < count; i++)
                yield return func(generator);
        }

        /// <summary>
        ///     <para>Generates an IEnumerable&lt;TResult&gt; </para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        /// <param name="generator"></param>
        /// <param name="func">The argument supplied is used to provide data for your object.</param>
        /// <typeparam name="TResult">Mapping result.</typeparam>
        /// <typeparam name="TGenerator">Data provider.</typeparam>
        /// <returns></returns>
        public static IEnumerable<TResult> GenerateSequence<TGenerator, TResult>(this TGenerator generator,
            Func<TGenerator, int, TResult> func, int count) where TGenerator : Generator {
            for (var i = 0; i < count; i++)
                yield return func(generator, i);
        }

        /// <summary>
        ///     <para>
        ///         Iterates through IEnumerable&lt;TSource&gt; exposing each element together with a generator.
        ///     </para>
        /// </summary>
        /// <typeparam name="TGenerator">Data provider.</typeparam>
        /// <typeparam name="TSource">Your Collection</typeparam>
        /// <typeparam name="TResult">Mapping result.</typeparam>
        /// <returns></returns>
        public static IEnumerable<TResult> GenerateBySequence<TGenerator, TSource, TResult>(this TGenerator generator,
            IEnumerable<TSource> source, Func<TGenerator, TSource, TResult> func) where TGenerator : Generator {
            foreach (var element in source) yield return func(generator, element);
        }
    }
}