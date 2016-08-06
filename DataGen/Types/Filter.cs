using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataGen.Types.NameCollection;

namespace DataGen.Types {
    public abstract class Filter<TData, TEnum> : IEnumerable<TData> {
        protected Filter(IEnumerable<TData> enumerable) {
            Enumerable = enumerable;
        }

        private IEnumerable<TData> Enumerable { get; }

        public IEnumerable<TData> RemoveRepeatedData()
            => Enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);

        public IEnumerator<TData> GetEnumerator() => Enumerable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected abstract Filter<TData, TEnum> Where(Func<TData, bool> predicate);

        public abstract Filter<TData, TEnum> FilterBy(TEnum tenum, params string[] args);
    }
}