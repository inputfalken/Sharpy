using System;
using DataGen.Types.Mail;

namespace DataGen {
    public static class Sharpy {
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
        public static Generator<T> CreateGenerator<T>(Func<Randomizer, T> func)
            => new Generator<T>(() => func(DefaultRandomizer), DefaultRandomizer.Config);

        /// <summary>
        ///     Gives a new instance of the type used
        /// </summary>
        public static T Generate<T>(Func<Randomizer, T> func) => func(DefaultRandomizer);
    }
}