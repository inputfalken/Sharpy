using System;

namespace Sharpy {
    internal class Defered<T> : IProductor<T> {
        private readonly Func<T> _fn;

        public Defered(Func<T> fn) {
            _fn = fn;
        }

        public T Produce() {
            return _fn.Invoke();
        }
    }
}