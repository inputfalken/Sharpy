using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types {
    public abstract class Filter<TData> {
        protected Filter(IEnumerable<TData> result) {
            Result = result;
        }

        public IEnumerable<TData> Result { get; }

        public static IEnumerable<TData> RepeatedData(IEnumerable<TData> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);
    }
}