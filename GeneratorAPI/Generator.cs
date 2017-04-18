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

        /// <summary>
        /// Turn Generator into Generation&lt;TResult&gt;
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<TResult> Generate<TResult>(Func<TProvider, TResult> fn) => new Generation<TResult>(
            InfiniteEnumerable(fn));
    }

    public class Generator {
        /// <summary>
        /// Contains methods for creating Generators with various Providers.
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();
    }

    /// <summary>
    /// Contains methods for creating Generators with various Providers.
    /// </summary>
    public class GeneratorFactory {
        /// <summary>
        /// A Generator using System.Random as provider.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public Generator<Random> RandomGenerator(Random random) => Create(random);

        /// <summary>
        /// Create a Generator with TProivder as provider.
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public Generator<TProvider> Create<TProvider>(TProvider provider) => new Generator<TProvider>(provider);
    }
}