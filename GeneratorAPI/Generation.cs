using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    public class Generation<T> {
        private static IEnumerable<TResult> InfiniteEnumerable<TResult>(Func<TResult> fn) {
            while (true) yield return fn();
        }

        private readonly IEnumerable<T> _infiniteEnumerable;

        public Generation(IEnumerable<T> infiniteEnumerable) => _infiniteEnumerable = infiniteEnumerable;


        private Generation(Func<T> fn) : this(InfiniteEnumerable(fn)) { }

        public Generation<TResult> Select<TResult>(Func<T, TResult> fn) => new Generation<TResult>(_infiniteEnumerable
            .Select(fn));

        public T Take() => _infiniteEnumerable.First();

        public IEnumerable<T> Take(int count) => _infiniteEnumerable.Take(count);

        public Generation<TResult> SelectMany<TResult>(Func<T, Generation<TResult>> fn) =>
            // If SelectMany was used on _infiniteEnumerable<T> with _infiniteEnumerable<TResult> the following would happen.
            // The first generation of _infiniteEnumerable<T> would be used and we would iterate forever on _infiniteEnumerable<TResult>
            // With current approach a new Generation<TResult> is created and given a Func<TResult> to its constructor.
            // The Func passed Generates from the result.
            new Generation<TResult>(() => fn(Take()).Take());


        public Generation<TCompose> SelectMany<TResult, TCompose>(Func<T, Generation<TResult>> fn,
            Func<T, TResult, TCompose> composer) {
            return SelectMany(a => fn(a).SelectMany(r => new Generation<TCompose>(() => composer(a, r))));
        }

        public Generation<T> Where(Func<T, bool> predicate) => new Generation<T>(
            _infiniteEnumerable.Where(predicate));
    }
}