using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen {
    public class Generator<T> {
        public Config Config { get; }
        private readonly Func<T> _func;

        public Generator(Func<T> func, Config config) {
            Config = config;
            _func = func;
        }

        public T Generate() => _func();

        public IEnumerable<T> Generate(int ammount) => Enumerable.Range(0, ammount).Select(i => _func());
    }
}