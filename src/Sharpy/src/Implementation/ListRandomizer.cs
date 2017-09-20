using System;
using System.Collections.Generic;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    public class ListRandomizer : IReadListElementProvider {
        private readonly Random _random;

        public ListRandomizer(Random random) => _random = random ?? throw new ArgumentNullException(nameof(random));

        public T Element<T>(IReadOnlyList<T> list) => list.RandomItem(_random);

        public T Argument<T>(params T[] arguments) => arguments.RandomItem(_random);
    }
}