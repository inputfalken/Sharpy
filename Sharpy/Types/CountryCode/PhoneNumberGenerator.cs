using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpy.Types.CountryCode {
    // ReSharper disable once ClassNeverInstantiated.Global
    // Is generated from json
    /// <summary>
    /// </summary>
    internal sealed class PhoneNumberGenerator : Unique<string> {
        internal PhoneNumberGenerator(CountryCode countryCode, Random random, int length, bool unique = false)
            : base(50, random) {
            CountryCode = countryCode;
            Length = length;
            Unique = unique;
            Min = (int) Math.Pow(10, Length - 1);
            Max = Min*10 - 1;
        }

        private CountryCode CountryCode { get; }
        private int Length { get; }


        private bool Unique { get; }


        private readonly HashSet<int> _numbers = new HashSet<int>();

        private int Min { get; }
        private int Max { get; }

        /// <summary>
        ///     Will create a phone number by randoming numbers including country code
        ///     <param name="preNumber">Optional number that will be used before the random numbers</param>
        /// </summary>
        public string RandomNumber(string preNumber = null) {
            var next = Random.Next(Min, Max);
            if (!Unique) return Build(CountryCode.Code, preNumber, next.ToString());
            while (_numbers.Contains(next)) {
                if (next == Max)
                    next = Min;
                next++;
            }
            _numbers.Add(next);
            return Build(CountryCode.Code, preNumber, next.ToString());
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