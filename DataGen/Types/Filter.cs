using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types {
    public static class Filter {
        /// <summary>
        ///     Filters repeated strings from argument
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> RepeatedData<T>(IEnumerable<T> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);
    }
}