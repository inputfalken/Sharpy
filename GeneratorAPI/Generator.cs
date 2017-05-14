using System;

namespace GeneratorAPI {
    public static class Generator {
        /// <summary>
        ///     <para>
        ///         Contains methods for creating Generators with various Providers.
        ///     </para>
        /// </summary>
        public static GeneratorFactory Factory { get; } = new GeneratorFactory();

        /// <summary>
        ///  Creates a generation by using the first argument as provider and second for the Generation result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="provider"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static Generation<TResult> Generate<T, TResult>(T provider, Func<T, TResult> fn) {
            return Factory.Create(provider).Generate(fn);
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    public class Generator<TProvider> {
        public Generator(TProvider provider) {
            if (provider == null) {
                throw new ArgumentNullException(nameof(provider));
            }
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
            if (fn == null) {
                throw new ArgumentNullException(nameof(fn));
            }
            return new Generation<TResult>(() => fn(Provider));
        }
    }


    /// <summary>
    ///     <para>
    ///         Contains methods for creating Generators. 
    ///     </para>
    ///     <remarks>
    ///         The point of this class is to contain extension methods from other libraries.
    ///     </remarks>
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