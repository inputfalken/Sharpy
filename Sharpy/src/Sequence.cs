using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace Sharpy {
    internal class Sequence<T> : IProductor<T>, IEnumerable<T> {
        private readonly IEnumerable<T> _enumerable;
        private readonly Lazy<IEnumerator<T>> _enumerator;

        public Sequence(IEnumerable<T> enumerable) {
            if (enumerable.Any()) _enumerable = enumerable;
            else throw new ArgumentException($"{nameof(enumerable)} cannot be empty");
            _enumerator = new Lazy<IEnumerator<T>>(enumerable.GetEnumerator);
        }

        public IEnumerator<T> GetEnumerator() {
            return _enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable) _enumerable).GetEnumerator();
        }

        public T Produce() {
            while (true) {
                if (_enumerator.Value.MoveNext()) return _enumerator.Value.Current;
                _enumerator.Value.Reset();
            }
        }
    }
}