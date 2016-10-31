using System;

namespace Sharpy.Randomizer.Generators {
    internal class SecurityNumberGen : Unique<long> {
        internal long SecurityNumber(int controlNumber, string dateNumber) {
            var number = long.Parse(dateNumber + controlNumber);
            //OnDuplicate will only manipulate control number, DateNumber will be the same all the time.
            while (HashSet.Contains(number))
                number = long.Parse(dateNumber + OnDuplicate(controlNumber, 1000, 9999));
            HashSet.Add(number);
            return number;
        }

        private static int OnDuplicate(int item, int min, int max) {
            if (item == max) item = min;
            else item++;
            return item;
        }

        public SecurityNumberGen(Random random) : base(random) {}
    }
}