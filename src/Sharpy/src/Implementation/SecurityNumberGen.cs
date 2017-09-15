﻿using System;
using Sharpy.Implementation.ExtensionMethods;

namespace Sharpy.Implementation {
    internal class SecurityNumberGen : Unique<long> {
        internal SecurityNumberGen(Random random) : base(random) { }

        internal long SecurityNumber(string dateNumber) {
            var controlNumber = Random.Next(10000);
            var number = long.Parse(dateNumber + controlNumber);
            var resets = 0;
            while (HashSet.Contains(number)) {
                if (controlNumber < 9999) {
                    controlNumber++;
                }
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