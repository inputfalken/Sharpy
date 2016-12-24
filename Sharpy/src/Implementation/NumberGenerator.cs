using System;

namespace Sharpy.Implementation {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class NumberGenerator : Unique<int> {
        internal NumberGenerator(Random random)
            : base(random) {
        }


        internal int RandomNumber(int min, int max, bool unique = false) {
            var next = Random.Next(min, max);
            return unique ? CreateUniqueNumber(next, min, max) : next;
        }

        private int CreateUniqueNumber(int number, int min, int max) {
            var resets = 0;
            while (HashSet.Contains(number))
                if (number < max) number++;
                else {
                    number = min;
                    if (resets++ == 2) return -1;
                }
            HashSet.Add(number);
            return number;
        }
    }
}