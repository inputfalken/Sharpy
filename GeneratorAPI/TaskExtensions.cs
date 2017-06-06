using System;
using System.Threading.Tasks;

namespace GeneratorAPI {
    /// <summary>
    /// </summary>
    public static class TaskExtensions {
        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(
            this IGenerator<Task<TFirst>> firstTaskGenerator,
            IGenerator<TSecond> second, Func<TFirst, TSecond, TResult> composer) {
            return firstTaskGenerator.Zip(second, async (l, r) => composer(await l, r));
        }

        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(this IGenerator<TFirst> first,
            IGenerator<Task<TSecond>> secondTaskGenerator, Func<TFirst, TSecond, TResult> composer) {
            return first.Zip(secondTaskGenerator, async (l, r) => composer(l, await r));
        }

        public static IGenerator<Task<TResult>> Zip<TFirst, TSecond, TResult>(
            this IGenerator<Task<TFirst>> firstTaskGenerator,
            IGenerator<Task<TSecond>> secondTaskGenerator, Func<TFirst, TSecond, TResult> composer) {
            return firstTaskGenerator.Zip(secondTaskGenerator, async (l, r) => composer(await l, await r));
        }

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