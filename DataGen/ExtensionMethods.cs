using System;
using System.Collections.Generic;
using static System.Linq.Enumerable;

namespace DataGen {
    public static class ExtensionMethods {
        public static IEnumerable<TSource> Sequence<TSource>(this IList<TSource> list, int length,
            Func<TSource, int, TSource> func, TSource start = default(TSource)) {
            foreach (var i in Range(1, length))
                list.Add(func(start, i));
            return list;
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> enumerable, Action<TSource> func) {
            foreach (var source in enumerable)
                func(source);
        }
    }
}