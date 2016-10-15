﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpy.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class PhoneNumberGenerator : Unique<int> {
        internal PhoneNumberGenerator(CountryCode countryCode, Random random, int length, string prefix,
            bool unique = false)
            : base(50, random) {
            CountryCode = countryCode;
            Prefix = prefix;
            Unique = unique;
            Min = (int) Math.Pow(10, length - 1);
            Max = Min*10 - 1;
        }

        private CountryCode CountryCode { get; }
        private string Prefix { get; }
        private bool Unique { get; }
        private int Min { get; }
        private int Max { get; }

        /// <summary>
        ///     Will create a phone number by randoming numbers including country code
        /// </summary>
        public string RandomNumber() {
            var next = Random.Next(Min, Max);
            if (!Unique) return Build(CountryCode.Code, Prefix, next.ToString());
            while (HashSet.Contains(next)) {
                if (next == Max)
                    next = Min;
                next++;
            }
            HashSet.Add(next);
            return Build(CountryCode.Code, next.ToString());
        }

        private static string Build(params string[] strings) {
            foreach (var s in strings)
                Builder.Append(s);
            var str = Builder.ToString();
            Builder.Clear();
            return str;
        }
    }
}