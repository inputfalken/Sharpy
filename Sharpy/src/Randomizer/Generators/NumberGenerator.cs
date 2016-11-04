using System;

namespace Sharpy.Randomizer.Generators {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class NumberGenerator : Unique<int> {
        internal NumberGenerator(Random random)
            : base(random) {}


        internal int RandomNumber(int min, int max, bool unique = false) {
            var next = Random.Next(min, max);
            return unique ? CreateUniqueNumber(next, min, max) : next;
        }


        private int CreateUniqueNumber(int number, int min, int max) {
            while (HashSet.Contains(number)) number = ResolveIntDuplicate(ref number, min, max);
            HashSet.Add(number);
            return number;
        }

    }
}