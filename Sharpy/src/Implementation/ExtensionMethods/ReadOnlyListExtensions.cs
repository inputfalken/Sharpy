using System;
using System.Collections.Generic;

namespace Sharpy.Implementation.ExtensionMethods {
    internal static class ReadOnlyListExtensions {
        internal static T RandomItem<T>(this IReadOnlyList<T> list, Random random) => list[random.Next(list.Count)];
    }
}