using System;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    /// 
    /// </summary>
    public static class Factory {
        /// <summary>
        ///     This is the field which gets used if you use the method which do not ask for a randomizer
        /// </summary>
        private static readonly Randomizer DefaultRandomizer = new Randomizer(new Config());

        /// <summary>
        ///     Returns a Generator which you can use to create one instance or a collection of type given
        ///     For a type with a consturctor you can just new up an instance of the type and satisfy the constructor args.
        ///     if you don't have a constructor you can new up an instance and assign it's props/fields
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public static Generator<T> Generator<T>(Func<Randomizer, T> func) {
            var randomizer = new Randomizer(new Config());
            return new Generator<T>(() => func(randomizer), randomizer.Config);
        }


        /// <summary>
        ///    This overload also gives an int of the current iteration.
        ///    First iteration will be number 0 
        /// </summary>
        /// <returns></returns>
        public static Generator<T> Generator<T>(Func<Randomizer, int, T> func) {
            var randomizer = new Randomizer(new Config());
            var iteration = -1;
            return new Generator<T>(() => {
                iteration ++;
                return func(randomizer, iteration);
            }, randomizer.Config);
        }
    }
}