using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    public class Generation<T> {
        private readonly IEnumerable<T> _infiniteEnumerable;

        public Generation(IEnumerable<T> infiniteEnumerable) => _infiniteEnumerable = infiniteEnumerable;

        public Generation<TResult> Select<TResult>(Func<T, TResult> fn) => new Generation<TResult>(_infiniteEnumerable
            .Select(fn));

        public T Take() => _infiniteEnumerable.First();

        public IEnumerable<T> Take(int count) => _infiniteEnumerable.Take(count);

        public Generation<TResult> SelectMany<TResult>(Func<T, Generation<TResult>> fn) {
            return new Generation<TResult>(
                _infiniteEnumerable.SelectMany(result => fn(result)._infiniteEnumerable));
        }

        //BUG Runs forever since _infiniteEnumerable never ends.
        public Generation<TCompose> SelectMany<TResult, TCompose>(Func<T, Generation<TResult>> fn,
            Func<T, TResult, TCompose> composer) {
            return new Generation<TCompose>(
                _infiniteEnumerable.SelectMany(result => fn(result)._infiniteEnumerable, composer));
        }

        public Generation<T> Where(Func<T, bool> predicate) => new Generation<T>(
            _infiniteEnumerable.Where(predicate));
    }
}