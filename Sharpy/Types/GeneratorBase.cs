using System;
using System.Collections.Generic;

namespace Sharpy.Types {
    public abstract class GeneratorBase<T, TFuncArg> {
        private Func<TFuncArg, int, T> FuncIterator { get; }
        private TFuncArg FuncArg { get; }
        private Func<TFuncArg, T> Func { get; }
        private int Iteratation { get; set; }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        protected GeneratorBase(Func<TFuncArg, T> func, TFuncArg funcArg) {
            Func = func;
            FuncArg = funcArg;
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        protected GeneratorBase(Func<TFuncArg, int, T> func, TFuncArg funcArg) {
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