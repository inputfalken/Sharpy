using System;

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
        public Generator(TProvider provider) {
            Provider = provider;
        }

        public TProvider Provider { get; }

        /// <summary>
        ///     <para>
        ///         Turn Generator into Generation&lt;TResult&gt;
        ///     </para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public Generation<TResult> Generate<TResult>(Func<TProvider, TResult> fn) {
            return new Generation<TResult>(() => fn(Provider));
        }
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
        public Generator<Random> RandomGenerator(Random random) {
            return Create(random);
        }

        /// <summary>
        ///     <para>
        ///         Create a Generator with TProivder as provider.
        ///     </para>
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public Generator<TProvider> Create<TProvider>(TProvider provider) {
            return new Generator<TProvider>(provider);
        }
    }
}