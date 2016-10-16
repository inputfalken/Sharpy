using System;
using System.Collections.Generic;

namespace Sharpy {
    public class Generator<T, TStringArg> {
        private Func<IRandomizer<TStringArg>, int, T> FuncIterator { get; }
        private IRandomizer<TStringArg> Randomizer { get; }
        private Func<IRandomizer<TStringArg>, T> Func { get; }
        private int Iteratation { get; set; }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer<TStringArg>, T> func, IRandomizer<TStringArg> randomizer) {
            Func = func;
            Randomizer = randomizer;
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer<TStringArg>, int, T> func, IRandomizer<TStringArg> randomizer) {
            FuncIterator = func;
            Randomizer = randomizer;
        }

        private T Generate(int i) => FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, i);

        /// <summary>
        ///     Will give back one instance of the specified Type
        /// </summary>
        /// <returns></returns>
        public T Generate() => FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, Iteratation++);

        /// <summary>
        ///     Will give back an IEnumerable with the specified type.
        ///     Which contains the ammount of elements.
        /// </summary>
        /// <param name="ammount"></param>
        public IEnumerable<T> GenerateEnumerable(int ammount) {
            for (var i = 0; i < ammount; i++)
                yield return Generate(i);
        }
    }
}