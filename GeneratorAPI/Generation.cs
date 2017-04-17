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

        public Generation<TResult> SelectMany<TResult>(Func<T, Generation<TResult>> fn) => new Generation<TResult>(
            () => fn(Take()).Take());

        public Generation<TCompose> SelectMany<TResult, TCompose>(Func<T, Generation<TResult>> fn,
            Func<T, TResult, TCompose> composer) => SelectMany(
            arg => fn(arg).SelectMany(result => new Generation<TCompose>(() => composer(arg, result))));

        public Generation<T> Where(Func<T, bool> predicate) => new Generation<T>(
            _infiniteEnumerable.Where(predicate));
    }
}