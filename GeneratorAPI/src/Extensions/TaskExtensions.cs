﻿using System;
using System.Threading.Tasks;

namespace GeneratorAPI.Extensions {
    /// <summary>
    /// </summary>
    public static class TaskExtensions {
        /// <summary>
        ///     <para>TODO</para>
        /// </summary>
        /// <typeparam name="TFirst">TODO</typeparam>
        /// <typeparam name="TSecond">TODO</typeparam>
        /// <typeparam name="TResult">TODO</typeparam>
        /// <param name="first">TODO</param>
        /// <param name="second">TODO</param>
        /// <param name="resultSelector">TODO</param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="first" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="second" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>TODO</returns>
        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(
            this IGenerator<Task<TFirst>> first,
            IGenerator<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector) {
            return first.Zip(second, async (l, r) => resultSelector(await l, r));
        }

        /// <summary>
        ///     <para>TODO</para>
        /// </summary>
        /// <typeparam name="TFirst">TODO</typeparam>
        /// <typeparam name="TSecond">TODO</typeparam>
        /// <typeparam name="TResult">TODO</typeparam>
        /// <param name="first">TODO</param>
        /// <param name="second">TODO</param>
        /// <param name="resultSelector">TODO</param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="first" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="second" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>TODO</returns>
        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(this IGenerator<TFirst> first,
            IGenerator<Task<TSecond>> second, Func<TFirst, TSecond, TResult> resultSelector) {
            return first.Zip(second, async (l, r) => resultSelector(l, await r));
        }

        /// <summary>
        ///     <para>TODO</para>
        /// </summary>
        /// <typeparam name="TFirst">TODO</typeparam>
        /// <typeparam name="TSecond">TODO</typeparam>
        /// <typeparam name="TResult">TODO</typeparam>
        /// <param name="first">TODO</param>
        /// <param name="second">TODO</param>
        /// <param name="resultSelector">TODO</param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="first" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="second" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="resultSelector" /> is null.</exception>
        /// <returns>TODO</returns>
        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(
            this IGenerator<Task<TFirst>> first,
            IGenerator<Task<TSecond>> second, Func<TFirst, TSecond, TResult> resultSelector) {
            return first.Zip(second, async (l, r) => resultSelector(await l, await r));
        }

        /// <summary>
        ///     <para>
        ///         TODO
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     TODO
        /// </typeparam>
        /// <param name="generator">TODO</param>
        /// <param name="predicate">TODO</param>
        /// <param name="threshold">TODO</param>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate" /> is null.</exception>
        /// <exception cref="ArgumentException">If the condition can't be matched within the <paramref name="threshold" />.</exception>
        /// <returns>
        ///     TODO
        /// </returns>
        public static IGenerator<Task<TSource>> Filter<TSource>(this IGenerator<Task<TSource>> generator,
            Func<TSource, bool> predicate, int threshold = 100000) {
            // predicate can't be named predicate because ambiguous usage of overloads....
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            // Duplicated from Generator.Where but with async.
            return Generator.Function(async () => {
                for (var i = 0; i < threshold; i++) {
                    var generation = await generator.Generate();
                    if (predicate(generation)) return generation;
                }
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts. ");
            });
        }

        /// <summary>
        ///     <para>
        ///         TODO
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     TODO
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     TODO
        /// </typeparam>
        /// <param name="generator">
        ///     TODO
        /// </param>
        /// <param name="selector">TODO</param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="selector" /> is null.</exception>
        /// <returns>
        ///     TODO
        /// </returns>
        public static IGenerator<Task<TResult>> FlatMapTask<TSource, TResult>(
            this IGenerator<TSource> generator,
            Func<TSource, Task<TResult>> selector) {
            return generator.SelectMany(source => Generator.Function(async () => await selector(source)));
        }

        /// <summary>
        ///     <para>
        ///         TODO
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     TODO
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     TODO
        /// </typeparam>
        /// <typeparam name="TCompose">
        ///     TODO
        /// </typeparam>
        /// <param name="generator">
        ///     TODO
        /// </param>
        /// <param name="selector">
        ///     TODO
        /// </param>
        /// <param name="composer">
        ///     TODO
        /// </param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="selector" /> is null.</exception>
        /// <returns>
        ///     TODO
        /// </returns>
        public static IGenerator<Task<TCompose>> FlatMapTask<TSource, TResult, TCompose>(
            this IGenerator<TSource> generator,
            Func<TSource, Task<TResult>> selector, Func<TSource, TResult, TCompose> composer) {
            return generator
                .SelectMany(s => Generator.Function(async () => await selector(s))
                    .SelectMany(r => Generator.Function(async () => composer(s, await r))));
        }

        /// <summary>
        ///     <para>
        ///         TODO
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     TODO
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     TODO
        /// </typeparam>
        /// <param name="generator">
        ///     TODO
        /// </param>
        /// <param name="selector">
        ///     TODO
        /// </param>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <returns>
        ///     TODO
        /// </returns>
        public static IGenerator<Task<TResult>> Map<TSource, TResult>(
            this IGenerator<Task<TSource>> generator,
            Func<TSource, TResult> selector) {
            return generator.Select(async s => selector(await s));
        }


        /// <summary>
        ///     <para>
        ///         TODO
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     TODO
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     TODO
        /// </typeparam>
        /// <param name="generator">
        ///     TODO
        /// </param>
        /// <param name="selector">
        ///     TODO
        ///     TODO
        /// </param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="selector" /> is null.</exception>
        /// <returns>
        ///     TODO
        /// </returns>
        public static IGenerator<Task<TResult>> FlatMapTask<TSource, TResult>(
            this IGenerator<Task<TSource>> generator,
            Func<TSource, Task<TResult>> selector) {
            return generator.FlatMapTask(async task => await selector(await task));
        }

        /// <summary>
        ///     <para>
        ///         TODO
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     TODO
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     TODO
        /// </typeparam>
        /// <typeparam name="TCompose">
        ///     TODO
        /// </typeparam>
        /// <param name="generator">
        ///     TODO
        /// </param>
        /// <param name="selector">
        ///     TODO
        /// </param>
        /// <param name="composer">
        ///     TODO
        /// </param>
        /// <exception cref="ArgumentNullException">When argument <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">When argument <paramref name="selector" /> is null.</exception>
        /// <returns>
        ///     TODO
        /// </returns>
        public static IGenerator<Task<TCompose>> FlatMapTask<TSource, TResult, TCompose>(
            this IGenerator<Task<TSource>> generator,
            Func<TSource, Task<TResult>> selector, Func<TSource, TResult, TCompose> composer) {
            return generator
                .SelectMany(s => Generator.Function(async () => await selector(await s))
                    .SelectMany(r => Generator.Function(async () => composer(await s, await r))));
        }


        /// <summary>
        ///     <para>
        ///         TODO
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">
        ///     TODO
        /// </typeparam>
        /// <param name="generator">TODO</param>
        /// <param name="task">TODO</param>
        /// <exception cref="ArgumentNullException">If <paramref name="generator" /> is null.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="task" /> is null.</exception>
        /// <returns>
        ///     TODO
        /// </returns>
        public static IGenerator<Task<TSource>> Do<TSource>(this IGenerator<Task<TSource>> generator,
            Action<TSource> task) {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            return generator.Do(async element => task(await element));
        }
    }
}