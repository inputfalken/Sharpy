using System;
using System.Collections.Generic;

namespace Sharpy {
    internal static class ExtensionMethods {
        /// <summary>
        ///     Will perform a foreach loop
        /// </summary>
        internal static void ForEach<TSource>(this IEnumerable<TSource> enumerable, Action<TSource> func) {
            foreach (var source in enumerable)
                func(source);
        }
    }
}