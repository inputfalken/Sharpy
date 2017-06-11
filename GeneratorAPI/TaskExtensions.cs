using System;
using System.Threading.Tasks;

namespace GeneratorAPI {
    /// <summary>
    /// </summary>
    public static partial class Generator {
        /// <summary>
        ///   Combine a Task Generator with another regular Generator and compose the result.
        /// </summary>
        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(
            this IGenerator<Task<TFirst>> firstTaskGenerator,
            IGenerator<TSecond> second, Func<TFirst, TSecond, TResult> composer) {
            return firstTaskGenerator.Zip(second, async (l, r) => composer(await l, r));
        }

        /// <summary>
        ///   Combine a Generator with another a Task Generator and compose the result.
        /// </summary>
        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(this IGenerator<TFirst> first,
            IGenerator<Task<TSecond>> secondTaskGenerator, Func<TFirst, TSecond, TResult> composer) {
            return first.Zip(secondTaskGenerator, async (l, r) => composer(l, await r));
        }

        /// <summary>
        ///   Combine an async Generator with another a Task Generator and compose the result.
        /// </summary>
        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(
            this IGenerator<Task<TFirst>> firstTaskGenerator,
            IGenerator<Task<TSecond>> secondTaskGenerator, Func<TFirst, TSecond, TResult> composer) {
            return firstTaskGenerator.Zip(secondTaskGenerator, async (l, r) => composer(await l, await r));
        }

        /// <summary>
        ///     Filter the Task generator by the predicate.
        ///     <remarks>
        ///         Use with Caution: Bad predicates cause the method to throw exception if threshold is reached.
        ///     </remarks>
        /// </summary>
        public static IGenerator<Task<TSource>> Where<TSource>(this IGenerator<Task<TSource>> taskGenerator,
            Func<TSource, bool> predicate, int threshold = 100000) {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (taskGenerator == null) throw new ArgumentNullException(nameof(taskGenerator));
            // Duplicated from Generator.Where but with async.
            return Generator.Function(async () => {
                for (var i = 0; i < threshold; i++) {
                    var generation = await taskGenerator.Generate();
                    if (predicate(generation)) return generation;
                }
                throw new ArgumentException($"Could not match the predicate with {threshold} attempts. ");
            });
        }

        /// <summary>
        ///     <para>
        ///         Maps Generator&lt;&lt;Task&gt;T&gt; into Generator&lt;&lt;Task&gt;T&gt;
        ///     </para>
        /// </summary>
        public static IGenerator<Task<TResult>> SelectMany<TSource, TResult>(
            this IGenerator<TSource> taskGenerator,
            Func<TSource, Task<TResult>> taskSelector) {
            return taskGenerator.SelectMany(source => Function(async () => await taskSelector(source)));
        }

        /// <summary>
        /// 
        /// </summary>
        public static IGenerator<Task<TCompose>> SelectMany<TSource, TResult, TCompose>(
            this IGenerator<TSource> taskGenerator,
            Func<TSource, Task<TResult>> taskSelector, Func<TSource, TResult, TCompose> composer) {
            return taskGenerator
                .SelectMany(s => Function(async () => await taskSelector(s))
                    .SelectMany(r => Function(async () => composer(s, await r))));
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
            return taskGenerator.SelectMany(async task => await taskSelector(await task));
        }

        /// <summary>
        /// 
        /// </summary>
        public static IGenerator<Task<TCompose>> SelectMany<TSource, TResult, TCompose>(
            this IGenerator<Task<TSource>> taskGenerator,
            Func<TSource, Task<TResult>> taskSelector, Func<TSource, TResult, TCompose>composer) {
            return taskGenerator
                .SelectMany(s => Function(async () => await taskSelector(await s))
                    .SelectMany(r => Function(async () => composer(await s, await r))));
        }


        /// <summary>
        ///     Exposes TSource from IGenerator&lt;Task&lt;TSource&gt;&gt;.
        /// </summary>
        public static IGenerator<Task<TSource>> Do<TSource>(this IGenerator<Task<TSource>> taskGenerator,
            Action<TSource> actionTask) {
            if (actionTask == null) throw new ArgumentNullException(nameof(actionTask));
            if (taskGenerator == null) throw new ArgumentNullException(nameof(taskGenerator));
            return taskGenerator.Do(async element => actionTask(await element));
        }
    }
}