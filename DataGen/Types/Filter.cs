using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static DataGen.Types.HelperClass;

namespace DataGen.Types {
    ///<summary>
    ///     This class is responsible for Filtering and selecting random items
    ///     All filters used in this project are derived from this class.
    /// </summary>
    public abstract class Filter<TSource> : IEnumerable<TSource> {
        protected Filter(IEnumerable<TSource> enumerable) {
            // ReSharper disable PossibleMultipleEnumeration
            if (!enumerable.Any()) throw new ArgumentException("Sequence Is empty");
            Enumerable = enumerable;
            LazyArray = new Lazy<TSource[]>(this.ToArray);
            Console.WriteLine("test");
        }

        private IEnumerable<TSource> Enumerable { get; }
        private Lazy<TSource[]> LazyArray { get; }

        internal TSource RandomItem => LazyArray.Value[Randomizer(LazyArray.Value.Length)];

        public IEnumerator<TSource> GetEnumerator() => Enumerable.GetEnumerator();

        protected static int IndexOf(string str, string substring)
            => str.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}