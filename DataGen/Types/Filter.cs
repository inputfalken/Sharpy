using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types {
    //Rename to modifier?
    public abstract class Filter<T> {
        protected IEnumerable<T> Result { get; }

        protected Filter(IEnumerable<T> result) {
            Result = result;
        }

        public static IEnumerable<T> RepeatedData(IEnumerable<T> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);
    }
}