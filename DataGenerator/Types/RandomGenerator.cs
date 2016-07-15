using System.Collections.Generic;
using System.Collections.Immutable;

namespace DataGenerator.Types {
    public class RandomGenerator : IGenerator {
        public T Generate<T>(ImmutableList<T> list)
            => list[HelperClass.Randomizer(list.Count)];
    }
}