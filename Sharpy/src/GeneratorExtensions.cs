using System;
using System.Collections.Generic;

namespace Sharpy {
    /// <summary>
    /// </summary>
    public static class GeneratorExtensions {
        /// <summary>
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static T Generate<TSource, T>(this TSource generator,
            Func<TSource, T> func) where TSource : GeneratorBase => func(generator);

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt; </para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="generator"></param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateSequence<TSource, T>(this TSource generator,
            Func<TSource, T> func, int count = 10) where TSource : GeneratorBase {
            for (var i = 0; i < count; i++)
                yield return func(generator);
        }

        /// <summary>
        ///     <para>Generates a IEnumerable&lt;T&gt; </para>
        ///     <para>Includes an integer containing the current iteration.</para>
        /// </summary>
        /// <param name="count">Count of IEnumerable&lt;T&gt;</param>
        /// <param name="generator"></param>
        /// <param name="func">The argument supplied is used to get the data. The item returned will be generated.</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GenerateSequence<TSource, T>(this TSource generator,
            Func<TSource, int, T> func, int count = 10) where TSource : GeneratorBase {
            for (var i = 0; i < count; i++)
                yield return func(generator, i);
        }
    }
}