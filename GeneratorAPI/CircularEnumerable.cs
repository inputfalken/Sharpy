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
            _lazyEnumerator = new Lazy<IEnumerator<T>>(enumerable.CacheGeneratedResults().GetEnumerator);
        }

        public CircularEnumerable(Func<IEnumerable<T>> fn) : this(Invoker(fn)) { }

        private static IEnumerable<T> Invoker(Func<IEnumerable<T>> fn) {
            while (true) {
                foreach (var x1 in fn()) {
                    yield return x1;
                }
            }
        }

        public T Generate() {
            if (Enumerator.MoveNext()) return Enumerator.Current;
            Enumerator.Reset();
            Enumerator.MoveNext();
            return Enumerator.Current;
        }
    }
}