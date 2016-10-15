using System;
using System.ComponentModel;
using Sharpy.Enums;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    ///    Supply the class you want to be generated.
    ///    Use the randomizer to give you data.
    /// </summary>
    public static class GeneratorFactory {
        public static Generator<T, StringType> CreateNew<T>(Func<IRandomizer<StringType>, int, T> func,
            Config config = null) => new Generator<T, StringType>(func, new Randomizer(config ?? new Config()));

        public static Generator<T, StringType> CreateNew<T>(Func<IRandomizer<StringType>, T> func, Config config = null)
            => new Generator<T, StringType>(func, new Randomizer(config ?? new Config()));

        /// <summary>
        ///    Requires a class which implements IRandomizer&lt;StringType&gt; as a secondary argument.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TStringArg"></typeparam>
        /// <returns></returns>
        public static Generator<T, TStringArg> CreateNew<T, TStringArg>(Func<IRandomizer<TStringArg>, T> func,
            IRandomizer<TStringArg> arg) => new Generator<T, TStringArg>(func, arg);

        /// <summary>
        ///    Requires a class which implements IRandomizer&lt;StringType&gt; as a secondary argument.
        ///    Includes an integer which is equal to ammount of iterations done. starts on number zero.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TStringArg"></typeparam>
        /// <returns></returns>
        public static Generator<T, TStringArg> CreateNew<T, TStringArg>(Func<IRandomizer<TStringArg>, int, T> func,
            IRandomizer<TStringArg> arg) => new Generator<T, TStringArg>(func, arg);
    }
}