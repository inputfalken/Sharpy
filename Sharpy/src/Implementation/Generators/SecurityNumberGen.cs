using System;

namespace Sharpy.Implementation.Generators {
    internal class SecurityNumberGen : Unique<long> {
        public SecurityNumberGen(Random random) : base(random) {}

        internal long SecurityNumber(int controlNumber, string dateNumber) {
            var number = long.Parse(dateNumber + controlNumber);
            //OnDuplicate will only manipulate control number, DateNumber will be the same all the time.
            while (HashSet.Contains(number))
                number = long.Parse(dateNumber + ResolveIntDuplicate(ref controlNumber, 1000, 9999));
            HashSet.Add(number);
            return number;
        }
    }
}