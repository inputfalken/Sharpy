﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpy.ExtensionMethods {
    internal static class ListExtensions {
        internal static void Shuffle<T>(this IList<T> list, Random random) {
            var n = list.Count;
            while (n > 1) {
                n--;
                var k = random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    internal static class EnumerableExtensions {
        internal static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, Random rnd)
            => source.OrderBy(item => rnd.Next());
    }
}