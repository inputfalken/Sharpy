using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;

namespace DataGen {
    public static class ExtensionMethods {
        public static IEnumerable<TSource> Sequence<TSource>(this IList<TSource> list, int ammount,
            Func<TSource, int, TSource> func, TSource sequenceValue = default(TSource)) {
            foreach (var i in Range(1, ammount))
                list.Add(func(sequenceValue, i));
            return list;
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> enumerable, Action<TSource> func) {
            foreach (var source in enumerable)
                func(source);
        }
    }
}