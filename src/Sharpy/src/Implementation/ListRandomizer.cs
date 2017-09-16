using System;
using System.Collections.Generic;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    public class ListRandomizer : IListElementPicker {
        private readonly Random _random;

        public ListRandomizer(Random random) => _random = random ?? throw new ArgumentNullException(nameof(random));

        public T TakeElement<T>(IReadOnlyList<T> list) => list.RandomItem(_random);

        public T TakeArgument<T>(params T[] arguments) => arguments.RandomItem(_random);
    }
}