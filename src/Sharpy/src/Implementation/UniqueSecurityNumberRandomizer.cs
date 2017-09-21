﻿using System;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    /// Builds strings representing security numbers.
    /// </summary>
    public class UniqueSecurityNumberBuilder : UniqueRandomizer<long> {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="random"></param>
        protected UniqueSecurityNumberBuilder(Random random) : base(random) { }

        private long SecurityNumber(string dateNumber) {
            var controlNumber = Random.Next(10000);
            var number = long.Parse(dateNumber + controlNumber);
            var resets = 0;
            while (HashSet.Contains(number)) {
                if (controlNumber < 9999) {
                    controlNumber++;
                }
                else {
                    controlNumber = 0;
                    if (resets++ == 2)
                        throw new Exception("You have reached the maximum possible combinations for a control number");
                }
                number = long.Parse(dateNumber.Append(controlNumber));
            }
            HashSet.Add(number);
            return number;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="formated"></param>
        /// <returns></returns>
        protected string SecurityNumber(DateTime date, bool formated) {
            var result = SecurityNumber(FormatDigit(date.Year % 100)
                    .Append(FormatDigit(date.Month), FormatDigit(date.Day)))
                .ToString();
            var securityNumber = result;
            if (securityNumber.Length != 10)
                securityNumber = result.Prefix(10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected DateTime RandomizeDate() {
            var dateTime = DateTime.Now;
            var year = Random.Next(1900, dateTime.Year);
            var month = year == dateTime.Year ? Random.Next(0, dateTime.Month) : Random.Next(1, 13);
            var date = year == dateTime.Year && month == dateTime.Month
                ? Random.Next(1, dateTime.Day + 1)
                : Random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, date);
        }

        private static string FormatDigit(int i) => i < 10 ? i.Prefix(1) : i.ToString();
    }

    /// <summary>
    /// Creates unique formatted security numbers using a dash before the control number.
    /// </summary>
    public sealed class UniqueFormattedSecurityBuilder : UniqueSecurityNumberBuilder, ISecurityNumberProvider {
        internal UniqueFormattedSecurityBuilder(Random random) : base(random) { }

        /// <inheritdoc />
        public string SecurityNumber(DateTime date) => SecurityNumber(date, true);

        /// <inheritdoc />
        public string SecurityNumber() => SecurityNumber(RandomizeDate());
    }

    /// <summary>
    /// Creates unique security numbers.
    /// </summary>
    public sealed class UniqueSecurityBuilder : UniqueSecurityNumberBuilder, ISecurityNumberProvider {
        internal UniqueSecurityBuilder(Random random) : base(random) { }

        /// <inheritdoc />
        public string SecurityNumber(DateTime date) => SecurityNumber(date, false);

        /// <inheritdoc />
        public string SecurityNumber() => SecurityNumber(RandomizeDate());
    }
}