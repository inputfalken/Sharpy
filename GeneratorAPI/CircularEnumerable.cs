using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneratorAPI {
    internal class CircularEnumerable<T> : IGenerator<T> {
        private readonly Lazy<IEnumerator<T>> _lazyEnumerator;

        private IEnumerator<T> Enumerator {
            get { return _lazyEnumerator.Value; }
        }

        public CircularEnumerable(IEnumerable<T> enumerable) {
            if (enumerable.Any())
                _lazyEnumerator = new Lazy<IEnumerator<T>>(enumerable.CacheGeneratedResults().GetEnumerator);
            else throw new ArgumentException($"{nameof(enumerable)} can't be empty.");
        }

        public T Generate() {
            while (true) {
                if (Enumerator.MoveNext()) {
                    return Enumerator.Current;
                }
                Enumerator.Reset();
            }
        }
    }
}