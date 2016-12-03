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
        /// <typeparam name="TResult">Your object.</typeparam>
        /// <typeparam name="TSource">Data provider.</typeparam>
        /// <returns></returns>
        public static TResult Generate<TSource, TResult>(this TSource generator,
            Func<TSource, TResult> func) where TSource : Generator => func(generator);

        /// <summary>
        ///     <para>Generates an IEnumerable&lt;TResult&gt; </para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;TResult&gt;</param>
        /// <param name="generator"></param>
        /// <param name="func">The generator supplied is used to provide data for your object.</param>
        /// <typeparam name="TResult">Your object.</typeparam>
        /// <typeparam name="TSource">Data Provider.</typeparam>
        /// <returns></returns>
        public static IEnumerable<TResult> GenerateSequence<TSource, TResult>(this TSource generator,
            Func<TSource, TResult> func, int count = 10) where TSource : Generator {
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
        /// <typeparam name="TResult">Your object.</typeparam>
        /// <typeparam name="TSource">Data provider.</typeparam>
        /// <returns></returns>
        public static IEnumerable<TResult> GenerateSequence<TSource, TResult>(this TSource generator,
            Func<TSource, int, TResult> func, int count = 10) where TSource : Generator {
            for (var i = 0; i < count; i++)
                yield return func(generator, i);
        }
    }
}