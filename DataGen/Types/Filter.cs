using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataGen.Types.NameCollection;

namespace DataGen.Types {
    public abstract class Filter<TData, TArg> : IEnumerable<TData> {
        protected Filter(IEnumerable<TData> enumerable) {
            Enumerable = enumerable;
        }

        private IEnumerable<TData> Enumerable { get; }

        public TData RandomItem => FetchRandomElement();

        public IEnumerable<TData> RemoveRepeatedData()
            => this.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);


        private TData FetchRandomElement() {
            var current = default(TData);
            var count = 0;
            foreach (var element in this) {
                count++;
                if (HelperClass.Randomizer(count) == 0)
                    current = element;
            }
            if (count == 0)
                throw new InvalidOperationException("Sequence was empty");
            return current;
        }


        public IEnumerator<TData> GetEnumerator() => Enumerable.GetEnumerator();

        public StringFilter ToStringFilter(Func<TData, string> func)
            => new StringFilter(this.Select(func));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public abstract Filter<TData, TArg> FilterBy(TArg tArg, params string[] args);
    }
}