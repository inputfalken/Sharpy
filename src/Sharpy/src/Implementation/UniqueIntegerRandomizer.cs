﻿using System;

namespace Sharpy.Implementation {
    internal sealed class UniqueRandomizerIntegerRandomizer : UniqueRandomizer<int> {
        internal UniqueRandomizerIntegerRandomizer(Random random)
            : base(random) { }

        internal int RandomNumber(int min, int max, bool unique = false) {
            var next = Random.Next(min, max);
            return unique ? CreateUniqueNumber(next, min, max) : next;
        }

        private int CreateUniqueNumber(int number, int min, int max) {
            var resets = 0;
            while (HashSet.Contains(number))
                if (number < max) {
                    number++;
                }
                else {
                    number = min;
                    if (resets++ == 2) return -1;
                }
            HashSet.Add(number);
            return number;
        }
    }
}