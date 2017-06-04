﻿using System;
using System.Threading.Tasks;

namespace GeneratorAPI {
    /// <summary>
    /// </summary>
    public static class TaskExtensions {
        /// <summary>
        ///     <para>
        ///         Maps Generator&lt;&lt;Task&gt;T&gt; into Generator&lt;&lt;Task&gt;T&gt;
        ///     </para>
        /// </summary>
        public static IGenerator<Task<TResult>> SelectMany<TSource, TResult>(
            this IGenerator<TSource> taskGenerator,
            Func<TSource, Task<TResult>> taskSelector) {
            return taskGenerator.Select(async source => await taskSelector(source));
        }

        /// <summary>
        ///     <para>
        ///         Maps Generator&lt;Task&lt;TSource&gt;&gt; into Generator&lt;Task&lt;TResult&gt;&gt;
        ///     </para>
        /// </summary>
        public static IGenerator<Task<TResult>> Select<TSource, TResult>(
            this IGenerator<Task<TSource>> taskGenerator,
            Func<TSource, TResult> selector) {
            return taskGenerator.Select(async s => selector(await s));
        }


        /// <summary>
        ///     <para>
        ///         Flattens Generator with nested tasks to Generator&lt;Task&lt;TResult&gt;&gt;
        ///     </para>
        /// </summary>
        public static IGenerator<Task<TResult>> SelectMany<TSource, TResult>(
            this IGenerator<Task<TSource>> taskGenerator,
            Func<TSource, Task<TResult>> taskSelector) {
            return taskGenerator.Select(async task => await taskSelector(await task));
        }
    }
}