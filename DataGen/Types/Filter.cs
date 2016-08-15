using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.String;

namespace DataGen.Types {
    ///<summary>
    ///     This class is responsible for Filtering and selecting random items
    ///     All filters used in this project are derived from this class.
    /// </summary>
    public class Filter<TSource> : IEnumerable<TSource> {
        protected Filter(IEnumerable<TSource> enumerable) {
            // ReSharper disable PossibleMultipleEnumeration
            if (!enumerable.Any()) throw new ArgumentException("Sequence Is empty");
            Enumerable = enumerable;
            LazyArray = new Lazy<TSource[]>(this.ToArray);
        }

        private IEnumerable<TSource> Enumerable { get; }
        private Lazy<TSource[]> LazyArray { get; }

        public TSource RandomItem => LazyArray.Value[HelperClass.Randomizer(LazyArray.Value.Length)];

        protected static IEnumerable<TSource> RemoveRepeatedData(IEnumerable<TSource> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);


        public IEnumerator<TSource> GetEnumerator() => Enumerable.GetEnumerator();

        public StringFilter ToStringFilter(Func<TSource, string> func)
            => new StringFilter(this.Select(func));

        public StringFilter ToStringFilter()
            => new StringFilter(this.Select(source => source.ToString()));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}