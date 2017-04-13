using System;
using GeneratorAPI;

namespace Sharpy {
    /// <summary>
    ///     A Generator using System.Random.
    /// </summary>
    public class RandomGenerator : IGenerator<Random> {
        private readonly Random _random;

        /// <summary>
        ///     Creates a RandomGenerator using the Random provided.
        /// </summary>
        /// <param name="random"></param>
        public RandomGenerator(Random random) {
            _random = random;
        }

        /// <summary>
        ///     Creates a RandomGenerator using System.Random with the seed provided.
        /// </summary>
        /// <param name="seed"></param>
        public RandomGenerator(int seed) : this(new Random(seed)) { }

        /// <summary>
        ///     Creates a RandomGenerator with a System.Random with it's seed based on ticks.
        /// </summary>
        public RandomGenerator() : this(new Random()) { }


        /// <summary>
        ///     <para>
        ///         Produces T
        ///     </para>
        /// </summary>
        /// <returns></returns>
        public Random GetProvider() {
            return _random;
        }

        /// <summary>
        ///     Returns the Generation of the Func.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="fn"></param>
        /// <returns></returns>
        public IGeneration<TResult> Generate<TResult>(Func<Random, TResult> fn) {
            return new DelegateGeneration<TResult>(() => fn(GetProvider()));
        }
    }
}