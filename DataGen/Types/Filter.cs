using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataGen.Types {
    public abstract class Filter<TData, TEnum> : IEnumerable<TData> {
        protected Filter(IEnumerable<TData> enumerable) {
            Enumerable = enumerable;
        }

        private IEnumerable<TData> Enumerable { get; }
        protected ICollection<TData> Collection { get; } = new Collection<TData>();

        public IEnumerable<TData> RemoveRepeatedData()
            => Enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);

        public IEnumerator<TData> GetEnumerator() => Enumerable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected virtual void PopulateCollectionWhere(Func<TData, bool> predicate) {
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
                if (predicate(enumerator.Current))
                    Collection.Add(enumerator.Current);
            enumerator.Dispose();
        }

        public abstract Filter<TData, TEnum> FilterBy(TEnum tenum, params string[] args);
    }
}