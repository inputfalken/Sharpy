using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types.String;

namespace DataGen.Types {
    public abstract class Filter<TData, TArg> : IEnumerable<TData> {
        protected Filter(IEnumerable<TData> enumerable) {
            // ReSharper disable PossibleMultipleEnumeration
            if (!enumerable.Any()) throw new ArgumentException("Sequence Is empty");
            Enumerable = enumerable;
            LazyList = new Lazy<List<TData>>(this.ToList);
        }

        private IEnumerable<TData> Enumerable { get; }

        public TData RandomItem => LazyList.Value[HelperClass.Randomizer(LazyList.Value.Count)];

        public IEnumerable<TData> RemoveRepeatedData()
            => this.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);


        private Lazy<List<TData>> LazyList { get; }
        public IEnumerator<TData> GetEnumerator() => Enumerable.GetEnumerator();

        public StringFilter ToStringFilter(Func<TData, string> func)
            => new StringFilter(this.Select(func));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public abstract Filter<TData, TArg> FilterBy(TArg tArg, params string[] args);
    }
}