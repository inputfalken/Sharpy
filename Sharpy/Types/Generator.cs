using System;
using System.Collections.Generic;

namespace Sharpy.Types {
    public class Generator<T, TFuncArg> {
        private Func<IRandomizer<TFuncArg>, int, T> FuncIterator { get; }
        private IRandomizer<TFuncArg> FuncArg { get; }
        private Func<IRandomizer<TFuncArg>, T> Func { get; }
        private int Iteratation { get; set; }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer<TFuncArg>, T> func, IRandomizer<TFuncArg> funcArg) {
            Func = func;
            FuncArg = funcArg;
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer<TFuncArg>, int, T> func, IRandomizer<TFuncArg> funcArg) {
            FuncIterator = func;
            FuncArg = funcArg;
        }

        private T Generate(int i) {
            return FuncIterator == null ? Func(FuncArg) : FuncIterator(FuncArg, i);
        }

        /// <summary>
        ///     Will give back one instance of the specified Type
        /// </summary>
        /// <returns></returns>
        public T Generate() {
            return FuncIterator == null ? Func(FuncArg) : FuncIterator(FuncArg, Iteratation++);
        }

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