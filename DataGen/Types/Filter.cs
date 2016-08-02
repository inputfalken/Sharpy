using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types {
    //Rename to modifier?
    public abstract class Filter<TData> {
        public IEnumerable<TData> Result { get; }

        protected Filter(IEnumerable<TData> result) {
            Result = result;
        }

        public static IEnumerable<TData> RepeatedData(IEnumerable<TData> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);
    }
}