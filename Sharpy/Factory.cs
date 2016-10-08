using System;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    ///     Is responsible for initiating the nescesarry objects to create Generators.
    /// </summary>
    public static class Factory {
        /// <summary>
        ///     Returns a Generator which you can use to create one instance or a collection of type given
        ///     For a type with a consturctor you can just new up an instance of the type and satisfy the constructor args.
        ///     if you don't have a constructor you can new up an instance and assign it's props/fields
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public static Generator<T> CreateGenerator<T>(Generator<T> generator) {
            return generator;
        }
    }
}