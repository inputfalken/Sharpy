using System;

namespace Sharpy.Randomize.Generators {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class NumberGenerator : Unique<int> {
        internal NumberGenerator(Random random)
            : base(random) {}


        internal int RandomNumber(int min, int max, bool unique = false) {
            var next = Random.Next(min, max);
            return unique ? CreateUniqueNumber(next, min, max, ResolveDuplicate) : next;
        }


        private int CreateUniqueNumber(int startNumber, int min, int max, Func<int, int, int, int> func) {
            var number = func(startNumber, max, min);
            while (HashSet.Contains(number)) number = func(number, max, min);
            HashSet.Add(number);
            return number;
        }

        private static int ResolveDuplicate(int item, int min, int max) {
            if (item == max) item = min;
            else item++;
            return item;
        }
    }
}