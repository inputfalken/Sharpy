using System;
using System.Collections.Generic;

namespace GeneratorAPI {
    internal class CircularSequence<T> : IGenerator<T> {
        private readonly Lazy<IEnumerator<T>> _lazyEnumerator;

        private IEnumerator<T> Enumerator {
            get { return _lazyEnumerator.Value; }
        }

        public CircularSequence(IEnumerable<T> enumerable) {
            _lazyEnumerator = new Lazy<IEnumerator<T>>(enumerable.CacheGeneratedResults().GetEnumerator);
        }

        public CircularSequence(Func<IEnumerable<T>> fn) : this(Invoker(fn)) { }

        /// <summary>
        /// QUAS WEX EXORT
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        private static IEnumerable<T> Invoker(Func<IEnumerable<T>> fn) {
            while (true) {
                foreach (var element in fn()) {
                    yield return element;
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