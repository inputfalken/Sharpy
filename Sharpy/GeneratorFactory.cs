using System;
using System.ComponentModel;
using Sharpy.Enums;
using Sharpy.Types;

namespace Sharpy {
    /// <summary>
    ///   Contains methods which creates instances of SharpyGenerator&lt;T&gt; and Generator&lt;T, TStringArg&gt;
    /// </summary>
    public static class GeneratorFactory {
        /// <summary>
        ///    Uses my own implementation of IRandomizer&lt;StringType&gt; Uses an optional config argument as secondary argument.
        ///    Includes an integer which is equal to ammount of iterations done. starts on number zero.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static SharpyGenerator<T> CreateNew<T>(Func<IRandomizer<StringType>, T> func, Config config = null)
            => new SharpyGenerator<T>(func, config);

        /// <summary>
        ///    Uses my own implementation of IRandomizer&lt;StringType&gt; Uses an optional config argument as secondary argument.
        ///    Includes an integer which is equal to ammount of iterations done. starts on number zero.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static SharpyGenerator<T> CreateNew<T>(Func<IRandomizer<StringType>, int, T> func, Config config = null)
            => new SharpyGenerator<T>(func, config);

        /// <summary>
        ///    Requires a class which implements IRandomizer&lt;StringType&gt; with StringType enum as T as a secondary argument.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="randomizer"></param>
        public static SharpyGenerator<T> CreateNew<T>(Func<IRandomizer<StringType>, T> func,
            IRandomizer<StringType> randomizer) => new SharpyGenerator<T>(func, randomizer);

        /// <summary>
        ///    Includes an integer which is equal to ammount of iterations done. starts on number zero.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="randomizer"></param>
        public static SharpyGenerator<T> CreateNew<T>(Func<IRandomizer<StringType>, int, T> func,
            IRandomizer<StringType> randomizer) => new SharpyGenerator<T>(func, randomizer);


        /// <summary>
        ///    Requires a class which implements IRandomizer&lt;StringType&gt; as a secondary argument.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        public static Generator<T, TStringArg> Custom<T, TStringArg>(Func<IRandomizer<TStringArg>, T> func,
            IRandomizer<TStringArg> arg) => new Generator<T, TStringArg>(func, arg);


        /// <summary>
        ///    Requires a class which implements IRandomizer&lt;StringType&gt; as a secondary argument.
        ///    Includes an integer which is equal to ammount of iterations done. starts on number zero.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        public static Generator<T, TStringArg> Custom<T, TStringArg>(Func<IRandomizer<TStringArg>, int, T> func,
            IRandomizer<TStringArg> arg) => new Generator<T, TStringArg>(func, arg);
    }
}