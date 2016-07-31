using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types {
    public abstract class Filter {
        public static IEnumerable<T> RepeatedData<T>(IEnumerable<T> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);
    }
}