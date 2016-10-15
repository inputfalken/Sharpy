using System;
using System.Collections.Generic;

namespace Sharpy {
    public class Generator<TItem, TRandomizerType> {
        private Func<IRandomizer<TRandomizerType>, int, TItem> FuncIterator { get; }
        private IRandomizer<TRandomizerType> Randomizer { get; }
        private Func<IRandomizer<TRandomizerType>, TItem> Func { get; }
        private int Iteratation { get; set; }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer<TRandomizerType>, TItem> func, IRandomizer<TRandomizerType> randomizer) {
            Func = func;
            Randomizer = randomizer;
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer<TRandomizerType>, int, TItem> func, IRandomizer<TRandomizerType> randomizer) {
            FuncIterator = func;
            Randomizer = randomizer;
        }

        private TItem Generate(int i) {
            return FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, i);
        }

        /// <summary>
        ///     Will give back one instance of the specified Type
        /// </summary>
        /// <returns></returns>
        public TItem Generate() {
            return FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, Iteratation++);
        }

        /// <summary>
        ///     Will give back an IEnumerable with the specified type.
        ///     Which contains the ammount of elements.
        /// </summary>
        /// <param name="ammount"></param>
        public IEnumerable<TItem> GenerateEnumerable(int ammount) {
            for (var i = 0; i < ammount; i++)
                yield return Generate(i);
        }
    }
}