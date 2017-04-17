using System;
using System.Collections.Generic;

namespace GeneratorAPI {
    /// <summary>
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    public class Generator<TProvider> {
        private readonly TProvider _provider;

        public Generator(TProvider provider) => _provider = provider;

        private IEnumerable<TResult> InfiniteEnumerable<TResult>(Func<TProvider, TResult> fn) {
            while (true) yield return fn(_provider);
        }

        public Generation<TResult> Generate<TResult>(Func<TProvider, TResult> fn) => new Generation<TResult>(
            InfiniteEnumerable(fn));
    }
}