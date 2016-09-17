using System;
using System.Collections.Generic;

namespace Sharpy {
    /// <summary>
    ///     Contains methods for generating the supplied type
    ///     And a config to set settings for how the supplied type should get generated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Generator<T> {
        internal Generator(Func<T> func, Config config) {
            Config = config;
            Func = func;
        }

        /// <summary>
        ///     Can be used to change settings for the randomizer
        /// </summary>
        public Config Config { get; }

        private Func<T> Func { get; }

        /// <summary>
        ///     Will give back one instance of the specified Type
        /// </summary>
        /// <returns></returns>
        public T Generate() => Func();

        /// <summary>
        ///     Will give back an IEnumerable with the specified type.
        ///     Which contains the ammount of elements.
        /// </summary>
        /// <param name="ammount"></param>
        public IEnumerable<T> Generate(int ammount) {
            for (var i = 0; i < ammount; i++)
                yield return Func();
        }
    }
}