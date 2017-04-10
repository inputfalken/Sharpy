using System;

namespace Sharpy {
    internal class Function<T> : IProductor<T> {
        private readonly Func<T> _fn;

        public Function(Func<T> fn) {
            _fn = fn;
        }

        public T Produce() {
            return _fn.Invoke();
        }
    }
}