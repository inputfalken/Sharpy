using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types {
    public abstract class Filter<TData, TEnum> : IEnumerable<TData> {
        protected Filter(IEnumerable<TData> result) {
            Result = result;
        }

        private IEnumerable<TData> Result { get; }

        public IEnumerable<TData> RemoveRepeatedData()
            => Result.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);

        public IEnumerator<TData> GetEnumerator() => Result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract Filter<TData, TEnum> FilterBy(TEnum tenum, params string[] args);
    }
}