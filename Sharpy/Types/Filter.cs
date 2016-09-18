using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sharpy.Types {
    ///<summary>
    ///     This class is responsible for Filtering and selecting random items
    ///     All filters used in this project are derived from this class.
    /// </summary>
    internal class Filter<TSource> : IEnumerable<TSource> {
        /// <summary>
        ///    Takes the IEnumerable and turns it into a lazy array which gets used only if needed.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <exception cref="ArgumentException"></exception>
        internal Filter(IEnumerable<TSource> enumerable) {
            // ReSharper disable PossibleMultipleEnumeration
            if (!enumerable.Any()) throw new ArgumentException("Sequence Is empty");
            Enumerable = enumerable;
            LazyArray = new Lazy<TSource[]>(this.ToArray);
        }

        private IEnumerable<TSource> Enumerable { get; }
        private Lazy<TSource[]> LazyArray { get; }


        internal TSource[] Array => LazyArray.Value;
        internal TSource RandomItem(Random random) => LazyArray.Value[random.Next(LazyArray.Value.Length)];
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TSource> GetEnumerator() => Enumerable.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="substring"></param>
        /// <returns></returns>
        protected static int IndexOf(string str, string substring)
            => str.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}