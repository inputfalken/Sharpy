using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    public class Generation<TResult> {
        private readonly IEnumerable<TResult> _infiniteEnumerable;

        public Generation(IEnumerable<TResult> infiniteEnumerable) => _infiniteEnumerable = infiniteEnumerable;

        public Generation<T> Select<T>(Func<TResult, T> fn) => new Generation<T>(_infiniteEnumerable.Select(fn));

        public TResult Take() => _infiniteEnumerable.First();

        public IEnumerable<TResult> Take(int count) => _infiniteEnumerable.Take(count);

        public Generation<T> SelectMany<T>(Func<TResult, Generation<T>> fn) {
            return new Generation<T>(
                _infiniteEnumerable.SelectMany(result => fn(result)._infiniteEnumerable));
        }

        public Generation<TCompose> SelectMany<T, TCompose>(Func<TResult, Generation<T>> fn,
            Func<TResult, T, TCompose> composer) {
            return new Generation<TCompose>(
                _infiniteEnumerable.SelectMany(result => fn(result)._infiniteEnumerable, composer));
        }

        public Generation<TResult> Where(Func<TResult, bool> predicate) => new Generation<TResult>(
            _infiniteEnumerable.Where(predicate));
    }
}