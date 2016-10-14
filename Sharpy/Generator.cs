﻿using System;
using Sharpy.Enums;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    ///    Supply the class you want to be generated.
    ///    Use the randomizer to give you data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Generator<T> : GeneratorBase<T, IRandomizer<StringType>> {
        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer<StringType>, T> func, IRandomizer<StringType> randomizer)
            : base(func, randomizer) {}

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer<StringType>, int, T> func, IRandomizer<StringType> randomizer)
            : base(func, randomizer) {}

        public Generator(Func<IRandomizer<StringType>, int, T> func, Config config = null)
            : base(func, new Randomizer(config ?? new Config())) {}

        public Generator(Func<IRandomizer<StringType>, T> func, Config config = null)
            : base(func, new Randomizer(config ?? new Config())) {}
    }
}