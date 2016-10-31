﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.ExtensionMethods;

namespace Sharpy.Randomizer.Generators {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class NumberGenerator : Unique<long> {
        private readonly int _length;

        internal NumberGenerator(Random random, int length, bool unique = false)
            : base(random) {
            _length = length;
            Unique = unique;
            Min = (int) Math.Pow(10, length - 1);
            Max = Min*10 - 1;
        }

        private bool Unique { get; }
        private int Min { get; }
        private int Max { get; }

        internal long RandomNumber() {
            var next = Random.Next(Min, Max);
            return Unique ? CreateUniqueNumber(next, OnDuplicate) : next;
        }

        internal long SecurityNumber(int controlNumber, string dateNumber) {
            var number = long.Parse(dateNumber + controlNumber);
            //OnDuplicate will only manipulate control number, DateNumber will be the same all the time.
            while (HashSet.Contains(number))
                number = long.Parse(dateNumber + OnDuplicate(controlNumber));
            HashSet.Add(number);
            return number;
        }


        private long CreateUniqueNumber(long startNumber, Func<long, long> func) {
            var number = func(startNumber);
            while (HashSet.Contains(number)) number = func(number);
            HashSet.Add(number);
            return number;
        }

        private long OnDuplicate(long item) {
            if (item == Max) item = Min;
            else item++;
            return item;
        }

        private int OnDuplicate(int item) {
            if (item == Max) item = Min;
            else item++;
            return item;
        }

        public override string ToString() => $"Length: {_length}, Unique: {Unique}";
    }
}