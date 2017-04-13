using System;
using GeneratorAPI;

namespace Sharpy {
    /// <summary>
    /// </summary>
    public class Generator : IGenerator<Provider> {
        private readonly Provider _provider;

        /// <summary>
        ///     Creates a Generator using the Provider.
        /// </summary>
        /// <param name="provider"></param>
        public Generator(Provider provider) {
            _provider = provider;
        }

        /// <summary>
        ///     Creates a Generator using a provider with default configuration.
        /// </summary>
        public Generator() : this(new Provider()) { }

        /// <inheritdoc />
        public Provider GetProvider() {
            return _provider;
        }

        /// <inheritdoc />
        public IGeneration<TResult> Generate<TResult>(Func<Provider, TResult> fn) {
            return new DelegateGeneration<TResult>(() => fn(GetProvider()));
        }
    }
}