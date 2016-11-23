using System;
using Sharpy.Implementation.ExtensionMethods;

namespace Sharpy.Implementation {
    internal class SecurityNumberGen : Unique<long> {
        public SecurityNumberGen(Random random) : base(random) {}


        internal long SecurityNumber(int controlNumber, string dateNumber) {
            var number = long.Parse(dateNumber + controlNumber);
            //OnDuplicate will only manipulate control number, DateNumber will be the same all the time.
            var resets = 0;
            while (HashSet.Contains(number)) {
                if (controlNumber != 9999) controlNumber++;
                else {
                    controlNumber = 0;
                    if (resets++ == 2) return -1;
                }
                number = long.Parse(dateNumber.Append(controlNumber));
            }
            HashSet.Add(number);
            return number;
        }
    }
}