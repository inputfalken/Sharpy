using System;

namespace Sharpy {
    internal class Deferred<T> : Lazy<T>, IProductor<T> {
        public Deferred(Func<T> fn) : base(fn) { }

        public T Produce() {
            return Value;
        }
    }
}