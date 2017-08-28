using System;
using System.Collections.Generic;
using Sharpy.Generator.Utils;

namespace Sharpy.Generator.Implementations {
    /// <summary>
    ///     <para>A Generator using <see cref="IEnumerable{T}" /></para>
    /// </summary>
    internal sealed class Seq<T> : IGenerator<T> {
        private readonly Lazy<IEnumerator<T>> _lazyEnumerator;

        public Seq(IEnumerable<T> enumerable) =>
            _lazyEnumerator = new Lazy<IEnumerator<T>>(enumerable.CacheGeneratedResults().GetEnumerator);

        public Seq(Func<IEnumerable<T>> fn) : this(Invoker(fn)) { }

        private IEnumerator<T> Enumerator => _lazyEnumerator.Value;

        public T Generate() {
            if (Enumerator.MoveNext()) return Enumerator.Current;
            Enumerator.Reset();
            Enumerator.MoveNext();
            return Enumerator.Current;
        }

        object IGenerator.Generate() => Generate();

        /// <summary>
        ///     <para>QUAS WEX EXORT</para>
        /// </summary>
        private static IEnumerable<T> Invoker(Func<IEnumerable<T>> fn) {
            while (true) foreach (var element in fn()) yield return element;
        }
    }
}