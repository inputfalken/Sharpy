using System.Collections.Generic;

namespace DataGenerator.Types {
    public class RandomGenerator : IGenerator {
        public T Generate<T>(List<T> list)
            => list[HelperClass.Randomizer(list.Count)];
    }
}