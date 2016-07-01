using System;
using System.Collections.Generic;

namespace DataGenerator.Types
{
    public class RandomGenerator : IGenerator<string>
    {
        public string Generate(List<string> list)
            => list[HelperClass.Randomer(list.Count)];

        public Tuple<string, int> GenerateWithIndex(List<string> list) {
            var currentIndex = HelperClass.Randomer(list.Count);
            return new Tuple<string, int>(list[currentIndex], currentIndex);
        }
    }
}