using System;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    ///    Supply the class you want to be generated.
    ///    Use the randomizer to give you data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Generator<T> : GeneratorBase<T, IRandomizer> {
        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer, T> func, IRandomizer randomizer = null, Config config = null)
            : base(func, randomizer) {
            FuncArg = randomizer ?? new Randomizer<T>(this);
            _config = config ?? new Config();
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer, int, T> func, IRandomizer randomizer = null, Config config = null)
            : base(func, randomizer) {
            FuncArg = randomizer ?? new Randomizer<T>(this);
            _config = config ?? new Config();
        }

        private readonly Config _config;


        internal Config Config {
            get {
                if (FuncArg is Randomizer<T>) return _config;
                throw new Exception("You cannot use this property with a custom randomizer.");
            }
        }
    }
}