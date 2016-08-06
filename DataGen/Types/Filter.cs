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
        //Todo add method to change the filter type which is similiar to select that will work in the current class

        private IEnumerable<TData> Enumerable { get; }

        public IEnumerable<TData> RemoveRepeatedData()
            => Enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);

        public IEnumerator<TData> GetEnumerator() => Enumerable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public abstract Filter<TData, TArg> FilterBy(TArg tenum, params string[] args);
    }
}