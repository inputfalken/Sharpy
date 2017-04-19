﻿using System;
using System.Collections.Generic;

namespace GeneratorAPI {
    public static class Generator {
        /// <summary>
        ///     <para>
        ///         Contains methods for creating Generators with various Providers.
        ///     </para>
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    public class Generator<TProvider> {
        public TProvider Provider { get; }

        public Generator(TProvider provider) => Provider = provider;

        private IEnumerable<TResult> InfiniteEnumerable<TResult>(Func<TProvider, TResult> fn) {
            while (true) yield return fn(Provider);
        }

        /// <summary>
        ///     <para>
        ///         Turn Generator into Generation&lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<TResult> Generate<TResult>(Func<TProvider, TResult> fn) => new Generation<TResult>(
            InfiniteEnumerable(fn));
    }


    /// <summary>
    ///     <para>
    ///         Contains methods for creating Generators with various Providers.
    ///     </para>
    /// </summary>
    public class GeneratorFactory {
        /// <summary>
        ///     <para>
        ///         A Generator using System.Random as provider.
        ///     </para>
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public Generator<Random> RandomGenerator(Random random) => Create(random);

        /// <summary>
        ///     <para>
        ///         Create a Generator with TProivder as provider.
        ///     </para>
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public Generator<TProvider> Create<TProvider>(TProvider provider) => new Generator<TProvider>(provider);
    }
}