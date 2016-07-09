using System;
using System.Collections.Generic;

namespace DataGenerator.Types
{
    public class RandomGenerator : IGenerator<string>
    {
        public string Generate(List<string> list)
            => list[HelperClass.Randomizer(list.Count)];

        public TF Generate<TF>(List<TF> list)
            => list[HelperClass.Randomizer(list.Count)];
    }
}