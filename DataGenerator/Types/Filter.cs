using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Types {
    public static class Filter {
        /// <summary>
        ///     Filters repeated strings from argument
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static ImmutableList<T> RepeatedData<T>(IEnumerable<T> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key)
                .ToImmutableList();
    }
}