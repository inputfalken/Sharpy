using System;
using System.Collections.Generic;

namespace DataGen {
    public class Generator<T> {
        public Generator(Func<T> func, Config config) {
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
        /// <returns></returns>
        public IEnumerable<T> Generate(int ammount) {
            for (var i = 0; i < ammount; i++)
                yield return Func();
        }
    }
}