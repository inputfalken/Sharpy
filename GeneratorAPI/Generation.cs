using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    public class Generation<T> {
        /// <summary>
        ///     <para>
        ///         The generation.
        ///     </para>
        /// </summary>
        private readonly Func<T> _generation;

        /// <summary>
        ///     <para>
        ///         Infinite deferred invocations of _generation.
        ///     </para>
        /// </summary>
        private readonly IEnumerable<T> _generations;

        private Generation(IEnumerable<T> infiniteEnumerable) => _generations = infiniteEnumerable;


        public Generation(Func<T> fn) : this(InfiniteEnumerable(fn)) => _generation = fn ?? throw new ArgumentNullException(nameof(fn));

        private static IEnumerable<TResult> InfiniteEnumerable<TResult>(Func<TResult> fn) {
            while (true) yield return fn();
        }

        /// <summary>
        ///     <para>
        ///         Maps Generation&lt;T&gt; into Generation&lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<TResult> Select<TResult>(Func<T, TResult> fn)
            => new Generation<TResult>(_generations.Select(fn));


        /// <summary>
        ///     <para>
        ///         Gives &lt;T&gt;
        ///     </para>
        /// </summary>
        /// <returns></returns>
        public T Take() => _generation();

        /// <summary>
        ///     <para>
        ///         Yields count ammount of items into an IEnumerable&lt;T&gt;.
        ///     </para>
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<T> Take(int count) => _generations.Take(count);

        /// <summary>
        ///     <para>
        ///         Flattens Generation&lt;T&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<TResult> SelectMany<TResult>(Func<T, Generation<TResult>> fn) =>
            // If SelectMany was used on _infiniteEnumerable<T> with _infiniteEnumerable<TResult> the following would happen.
            // The first generation of _infiniteEnumerable<T> would be used and we would iterate forever on _infiniteEnumerable<TResult>
            // With current approach a new Generation<TResult> is created and given a Func<TResult> to its constructor.
            // The Func passed Generates from the result.
            new Generation<TResult>(() => fn(Take()).Take());

        /// <summary>
        ///     <para>
        ///         Flattens Generation&lt;T&gt;
        ///         With a compose function using &lt;T&gt; and &lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TCompose"></typeparam>
        /// <param name="fn"></param>
        /// <param name="composer"></param>
        /// <returns></returns>
        public Generation<TCompose> SelectMany<TResult, TCompose>(Func<T, Generation<TResult>> fn,
            Func<T, TResult, TCompose> composer)
            => SelectMany(a => fn(a).SelectMany(r => new Generation<TCompose>(() => composer(a, r))));

        /// <summary>
        ///     <para>
        ///         Filters the generation to fit the predicate.
        ///     </para>
        ///     <remarks>
        ///         Use with Caution: Bad predicates will make this method run forever.
        ///     </remarks>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Generation<T> Where(Func<T, bool> predicate)
            => new Generation<T>(_generations.Where(predicate));
    }
}