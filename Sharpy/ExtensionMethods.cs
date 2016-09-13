using System;
using System.Collections.Generic;

namespace Sharpy {
    public static class ExtensionMethods {
        /// <summary>
        ///     Will perform a foreach loop
        /// </summary>
        public static void ForEach<TSource>(this IEnumerable<TSource> enumerable, Action<TSource> func) {
            foreach (var source in enumerable)
                func(source);
        }
    }
}