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
        public static SharpyGenerator<T> Default<T>(Func<IRandomizer<StringType>, T> func, Config config = null)
            => new SharpyGenerator<T>(func, config);

        public static SharpyGenerator<T> Default<T>(Func<IRandomizer<StringType>, int, T> func, Config config = null)
            => new SharpyGenerator<T>(func, config);

        public static Generator<T, TStringArg> CreateNew<T, TStringArg>(Func<IRandomizer<TStringArg>, T> func,
            IRandomizer<TStringArg> arg) => new Generator<T, TStringArg>(func, arg);

        public static Generator<T, TStringArg> CreateNew<T, TStringArg>(Func<IRandomizer<TStringArg>, int, T> func,
            IRandomizer<TStringArg> arg) => new Generator<T, TStringArg>(func, arg);
    }
}