using System;
using System.Collections.Generic;

namespace Sharpy.Builder.Implementation.ExtensionMethods {
    internal static class ReadOnlyListExtensions {
        internal static T RandomItem<T>(this IReadOnlyList<T> list, Random random) => list != null
            ? random != null
                ? list[random.Next(list.Count)]
                : throw new ArgumentNullException(nameof(random))
            : throw new ArgumentNullException(nameof(list));
    }
}