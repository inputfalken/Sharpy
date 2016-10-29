﻿using System;
using NodaTime;

namespace Sharpy.Randomizer.Generators {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class NumberGenerator : Unique<long> {
        internal NumberGenerator(Random random, int length, string prefix,
            bool unique = false)
            : base(random) {
            Prefix = prefix;
            Unique = unique;
            Min = (int) Math.Pow(10, length - 1);
            Max = Min*10 - 1;
        }

        private string Prefix { get; }
        private bool Unique { get; }
        private int Min { get; }
        private int Max { get; }

        internal string RandomNumber() {
            var next = Random.Next(Min, Max);
            if (!Unique) return Build(Prefix, next.ToString());
            var number = CreateUniqueNumber(next, OnDuppplicate);
            return Build(Prefix, number.ToString());
        }


        private long CreateUniqueNumber(long startNumber, Func<long, long> func) {
            var number = func(startNumber);
            while (HashSet.Contains(number)) number = func(number);
            HashSet.Add(number);
            return number;
        }

        internal string SocialSecurity(LocalDate date, bool formated) {
            var month = date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();
            var year = date.YearOfCentury < 10 ? $"0{date.YearOfCentury}" : date.YearOfCentury.ToString();
            var day = date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
            var controlNumber = Random.Next(Min, Max);
            var securityNumber = CreateUniqueNumber(long.Parse(Build(year, month, day, controlNumber.ToString())),
                OnDuppplicate);
            return formated ? securityNumber.ToString().Insert(6, "-") : securityNumber.ToString();
        }

        protected override long OnDuppplicate(long x) {
            if (x == Max) x = Min;
            else x++;
            return x;
        }
    }
}